using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace UTCI_calc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StreamWriter sw;
        StreamReader epw_read;
        StreamReader uwg_Trad_output;
        string working_folder = "C:\\Users\\anakano\\Dropbox\\Research\\mit case study\\MITcasestudy_uwg_results\\";
        
        public MainWindow()
        {
            InitializeComponent();
            
            //epw file list
            List<string> epw_file_list = new List<string>();
            epw_file_list.Add("MIT30_2"); 
            epw_file_list.Add("MIT30_2_CASE1");
            epw_file_list.Add("MIT30_2_CASE2");
            epw_file_list.Add("MIT30_2_CASE3_INSULATION");
            epw_file_list.Add("MIT30_2_CASE4_new_designs_for_new_bldgs");
            epw_file_list.Add("MIT30_2_CASE5_case4mproved");
            epw_file_list.Add("MIT30_2_CASE6_case5mproved_thickerIns,urbanroadvegupby0.25,greenroof50%,veg");
            epw_file_list.Add("MIT30_CURRENT(only_for_geometries,_rest_from_MIT30)");
            epw_file_list.Add("CurrentMIT-A2-2020");
            epw_file_list.Add("CurrentMIT-A2-2050");

            foreach (string epw_file in epw_file_list) {
               sw = new StreamWriter(working_folder + "UTCI_" + epw_file + ".csv");
               epw_read = new StreamReader(File.OpenRead(working_folder + epw_file + ".csv"));
               uwg_Trad_output = new StreamReader(File.OpenRead(working_folder + "Trad_" + epw_file + ".csv"));

               UTCI_from_UWG();
            }
        }

        public void UTCI_from_UWG() {
            sw.Flush();
            //.................................................................calculate Trad
            List<string> Twall = new List<string>();
            List<string> Tsky = new List<string>();
            List<string> Troad = new List<string>();
            List<string> canHeight = new List<string>();
            List<string> canWidth = new List<string>();
            List<double> Trad_double_side_list = new List<double>();
            List<double> Trad_double_list = new List<double>();
            
            while (!uwg_Trad_output.EndOfStream)
            {
                string line = uwg_Trad_output.ReadLine();
                var values = line.Split(',');

                Twall.Add(values[0]);
                Tsky.Add(values[1]);
                Troad.Add(values[2]);
                canHeight.Add(values[3]);
                canWidth.Add(values[4]);
            }

            double canHeight_double = Double.Parse(canHeight[0]);
            double canWidth_double = Double.Parse(canWidth[0]);

            for (int k = 0; k < Twall.Count; k++)
            {
                double Twall_double = Double.Parse(Twall[k]);
                double Tsky_double = Double.Parse(Tsky[k]);
                double Troad_double = Double.Parse(Troad[k]);
                
                ViewFactor F_wall = new ViewFactor(canHeight_double, canWidth_double);
                ViewFactor F_sky = new ViewFactor(canHeight_double, canWidth_double);
                ViewFactor F_road = new ViewFactor(canHeight_double, canWidth_double);

                //sidewalk
                double Fwall_side = F_wall.calc_F_wall(true);
                double Fsky_side = F_sky.calc_F_sky(true);
                double Froad_side = F_road.calc_F_road(true);
                double Fsum_side = Fwall_side + Fsky_side + Froad_side;

                //middle of canyon
                double Fwall = F_wall.calc_F_wall(false);
                double Fsky = F_sky.calc_F_sky(false);
                double Froad = F_road.calc_F_road(false);
                
                double Fsum = Fwall + Fsky + Froad;

                if (Fsum_side == 1.0 && Fsum == 1.0) return;
                else
                {
                    Fwall_side /= Fsum_side;
                    Fsky_side /= Fsum_side;
                    Froad_side /= Fsum_side;
                    Fsum_side = Fwall_side + Fsky_side + Froad_side;

                    Fwall /= Fsum;
                    Fsky /= Fsum;
                    Froad /= Fsum;
                    Fsum = Fwall + Fsky + Froad;
                }
                
                double Trad_double_side = calc_T_rad(Fwall_side, Fsky_side, Froad_side, Twall_double, Tsky_double, Troad_double) - 273.15;
                double Trad_double = calc_T_rad(Fwall, Fsky, Froad, Twall_double, Tsky_double, Troad_double) - 273.15;
                Trad_double_side_list.Add(Trad_double_side);
                Trad_double_list.Add(Trad_double);
            }

            //..................................................................UTCI
            List<string> Tair = new List<string>();
            List<string> Trad = new List<string>();
            List<string> RH = new List<string>();
            List<string> V = new List<string>();

            for (var j = 0; j < 8; j++)
            {
                epw_read.ReadLine();  //skip top 8 lines above the epw T,RH, V outputs
            }

            while (!epw_read.EndOfStream)
            {
                var line = epw_read.ReadLine();
                var values = line.Split(',');

                Tair.Add(values[6]);
                RH.Add(values[8]);
                V.Add(values[21]);
            }

            List<double> UTCI_side = new List<double>();
            List<double> UTCI = new List<double>();
            double newUTCI_value;
            List<int> UTCI_bin_side = new List<int>();
            List<int> UTCI_bin = new List<int>();
            int binNum = 17;
            string[] UTCI_category = new string[18] {
                "above +46: extreme heat stress",
                "+42 to +46: very strong heat stress",
                "+38 to +42: very strong heat stress",
                "+35 to +38: strong heat stress",
                "+32 to +35: strong heat stress",
                "+29 to +32: moderate heat stress",
                "+26 to +29: moderate heat stress",
                "+17.5 to +26: no thermal stress",
                "+9 to +17.5: no thermal stress",
                "+4.5 to +9: slight cold stress",
                "+0 to +4.5: slight cold stress",
                "-6.5 to 0: moderate cold stress",
                "-13.0 to -6.5: moderate cold stress",
                "-13 to -20: strong cold stress",
                "-20 to -27: strong cold stress",
                "-27 to -33.5: very strong cold stress",
                "-33.5 to -40: very strong cold stress",
                "below -40: extreme cold stress"};
            
            for (int i = 0; i < binNum; i++)
            {
                UTCI_bin_side.Add(0);
                UTCI_bin.Add(0);
            }

            /*
            above +46	extreme heat stress
            +38 to +46	very strong heat stress
            +32 to +38	strong heat stress
            +26 to +32	moderate heat stress
            +9 to +26	no thermal stress
            +9 to 0	    slight cold stress
            0 to -13	moderate cold stress
            -13 to -27	strong cold stress
            -27 to -40	very strong cold stress
            below -40	extreme cold stress
            */

            for (int i = 0; i < Tair.Count; i++)
            {
                double Tair_double = Double.Parse(Tair[i]);
                double RH_double = Double.Parse(RH[i]);
                double V_double = Double.Parse(V[i]);
                //Usite ~0.25 Umet, per ASHRAE Fundamentals
                V_double *= 1.5 * Math.Pow((2.0 / 460.0), 0.33);

                utci newUTCI_side = new utci(Tair_double, Trad_double_side_list[i], RH_double, V_double);
                newUTCI_value = newUTCI_side.calc_UTCI();
                UTCI_side.Add(newUTCI_value);

                //histogram
                /*if (newUTCI_value > 46.0) UTCI_bin_side[0] += 1;
                else if (newUTCI_value > 38.0) UTCI_bin_side[1] += 1;
                else if (newUTCI_value > 32.0) UTCI_bin_side[2] += 1;
                else if (newUTCI_value > 26.0) UTCI_bin_side[3] += 1;
                else if (newUTCI_value > 9.0) UTCI_bin_side[4] += 1;
                else if (newUTCI_value > 0.0) UTCI_bin_side[5] += 1;
                else if (newUTCI_value > -13.0) UTCI_bin_side[6] += 1;
                else if (newUTCI_value > -27.0) UTCI_bin_side[7] += 1;
                else if (newUTCI_value > -40.0) UTCI_bin_side[8] += 1;
                else UTCI_bin_side[9] += 1;

                utci newUTCI = new utci(Tair_double, Trad_double_list[i], RH_double, V_double);
                newUTCI_value = newUTCI.calc_UTCI();
                UTCI.Add(newUTCI_value);

                if (newUTCI_value > 46.0) UTCI_bin[0] += 1;
                else if (newUTCI_value > 38.0) UTCI_bin[1] += 1;
                else if (newUTCI_value > 32.0) UTCI_bin[2] += 1;
                else if (newUTCI_value > 26.0) UTCI_bin[3] += 1;
                else if (newUTCI_value > 9.0) UTCI_bin[4] += 1;
                else if (newUTCI_value > 0.0) UTCI_bin[5] += 1;
                else if (newUTCI_value > -13.0) UTCI_bin[6] += 1;
                else if (newUTCI_value > -27.0) UTCI_bin[7] += 1;
                else if (newUTCI_value > -40.0) UTCI_bin[8] += 1;
                else UTCI_bin[9] += 1;
                */
                if (newUTCI_value > 46.0) UTCI_bin_side[0] += 1;
                else if (newUTCI_value > 42.0) UTCI_bin_side[1] += 1;
                else if (newUTCI_value > 38.0) UTCI_bin_side[2] += 1;
                else if (newUTCI_value > 35.0) UTCI_bin_side[3] += 1;
                else if (newUTCI_value > 32.0) UTCI_bin_side[4] += 1;
                else if (newUTCI_value > 29.0) UTCI_bin_side[5] += 1;
                else if (newUTCI_value > 26.0) UTCI_bin_side[6] += 1;
                else if (newUTCI_value > 17.5) UTCI_bin_side[7] += 1;
                else if (newUTCI_value > 9.0) UTCI_bin_side[8] += 1;
                else if (newUTCI_value > 4.5) UTCI_bin_side[9] += 1;
                else if (newUTCI_value > 0.0) UTCI_bin_side[10] += 1;
                else if (newUTCI_value > -6.5) UTCI_bin_side[11] += 1;
                else if (newUTCI_value > -13.0) UTCI_bin_side[12] += 1;
                else if (newUTCI_value > -20.0) UTCI_bin_side[13] += 1;
                else if (newUTCI_value > -27.0) UTCI_bin_side[14] += 1;
                else if (newUTCI_value > -33.5) UTCI_bin_side[15] += 1;
                else if (newUTCI_value > -40.0) UTCI_bin_side[16] += 1;
                else UTCI_bin_side[17] += 1;

                utci newUTCI = new utci(Tair_double, Trad_double_list[i], RH_double, V_double);
                newUTCI_value = newUTCI.calc_UTCI();
                UTCI.Add(newUTCI_value);

                if (newUTCI_value > 46.0) UTCI_bin[0] += 1;
                else if (newUTCI_value > 42.0) UTCI_bin[1] += 1;
                else if (newUTCI_value > 38.0) UTCI_bin[2] += 1;
                else if (newUTCI_value > 35.0) UTCI_bin[3] += 1;
                else if (newUTCI_value > 32.0) UTCI_bin[4] += 1;
                else if (newUTCI_value > 29.0) UTCI_bin[5] += 1;
                else if (newUTCI_value > 26.0) UTCI_bin[6] += 1;
                else if (newUTCI_value > 17.5) UTCI_bin[7] += 1;
                else if (newUTCI_value > 9.0) UTCI_bin[8] += 1;
                else if (newUTCI_value > 4.5) UTCI_bin[9] += 1;
                else if (newUTCI_value > 0.0) UTCI_bin[10] += 1;
                else if (newUTCI_value > -6.5) UTCI_bin[11] += 1;
                else if (newUTCI_value > -13.0) UTCI_bin[12] += 1;
                else if (newUTCI_value > -20.0) UTCI_bin[13] += 1;
                else if (newUTCI_value > -27.0) UTCI_bin[14] += 1;
                else if (newUTCI_value > -33.5) UTCI_bin[15] += 1;
                else UTCI_bin[16] += 1;


            }

            for (int i = 0; i < Tair.Count; i++)
            {
                sw.Write(UTCI_side[i] + "," + UTCI[i]);
                if (i < UTCI_bin.Count)
                {
                    sw.Write("," + ","
                        + UTCI_bin_side[i] + ","
                        + String.Format("{0:P0}", ((double)UTCI_bin_side[i] / (double)Tair.Count)) + "," 
                        
                        + UTCI_bin[i] + "," 
                        + String.Format("{0:P0}",((double)UTCI_bin[i] / (double) Tair.Count)) + "," 
                        + UTCI_category[i]);
                }
                else
                {
                    sw.Write(",");
                }
                sw.WriteLine();
            }

            epw_read.Close();
            uwg_Trad_output.Close();
            sw.Close();
            Console.WriteLine("calculation completed");
        }

        public double calc_T_rad(double Fwall, double Fsky, double Froad, double Twall, double Tsky, double Troad)
        {
            double T_rad_4 = Fwall * Math.Pow(Twall, 4.0)
                + Fsky * Math.Pow(Tsky, 4.0)
                + Froad * Math.Pow(Troad, 4.0);
            double T_radiative = Math.Pow(T_rad_4, 0.25);
            return T_radiative;
        }
    }
}
