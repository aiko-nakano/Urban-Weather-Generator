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
    public partial class UTCIWindow : Window
    {
        StreamWriter sw;
        StreamReader epw_read;
        StreamReader uwg_Trad_output;
        string working_folder = "C:\\Users\\anakano\\Dropbox\\research\\mit case study\\MITcasestudy_uwg_results\\";
        bool refinedBin = true;

        public UTCIWindow()
        {
            InitializeComponent();
            
            //epw file list
            //List<string> epw_file_list = new List<string>();
            //epw_file_list.Add("MIT30_2");
            //epw_file_list.Add("MIT30_2_CASE1");
            //epw_file_list.Add("MIT30_2_CASE2");
            //epw_file_list.Add("MIT30_2_CASE3_INSULATION");
            //epw_file_list.Add("MIT30_2_CASE4_new_designs_for_new_bldgs");
            //epw_file_list.Add("MIT30_2_CASE5_case4mproved");
            //epw_file_list.Add("MIT30_2_CASE6_case5mproved_thickerIns,urbanroadvegupby0.25,greenroof50%,veg");
            //epw_file_list.Add("MIT30_CURRENT(only_for_geometries,_rest_from_MIT30)");
            //epw_file_list.Add("CurrentMIT-A2-2020");
            //epw_file_list.Add("CurrentMIT-A2-2050");
            //epw_file_list.Add("Logan(uses MIT30_CURRENT)");
            //epw_file_list.Add("MIT30_2_CASE6_2020");
            //epw_file_list.Add("MIT30_2_CASE6_2050");

            //foreach (string epw_file in epw_file_list) {
            //   sw = new StreamWriter(working_folder + "UTCI_" + epw_file + ".csv");
            //   epw_read = new StreamReader(File.OpenRead(working_folder + epw_file + ".csv"));
            //   uwg_Trad_output = new StreamReader(File.OpenRead(working_folder + "Trad_" + epw_file + ".csv"));

            //   UTCI_from_UWG();
            //}
        }

        public string UTCI_calc(string working_folder, string input_file)
        {
            string csv_filename = "UTCI_" + input_file;
            string fullpath = System.IO.Path.Combine(working_folder, csv_filename);
            sw = new StreamWriter(fullpath);
            epw_read = new StreamReader(File.OpenRead(System.IO.Path.Combine(working_folder, input_file)));
            string Trad_out = "Trad_" + input_file;
            uwg_Trad_output = new StreamReader(File.OpenRead(System.IO.Path.Combine(working_folder, Trad_out)));
            UTCI_from_UWG();
            return csv_filename;
        }

        public void EndSimulation()
        {
            epw_read.Close();
            uwg_Trad_output.Close();
            sw.Close();
            Console.WriteLine("calculation completed");
        }

        public void UTCI_from_UWG() {
            sw.Flush();
            sw.AutoFlush = true;
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

            int binNum_UTCICategory;
            string[] UTCI_category;

            if (refinedBin)
            {
                binNum_UTCICategory = 18;
                UTCI_category = new string[] {
                    "below -40: extreme cold stress",
                    "-40 to -33.5: very strong cold stress",
                    "-33.5 to -27: very strong cold stress",
                    "-27 to -20: strong cold stress",
                    "-20 to -13: strong cold stress",
                    "-13 to -6.5: moderate cold stress",
                    "-6.5 to 0: moderate cold stress",
                    "+0 to +4.5: slight cold stress",
                    "+4.5 to +9: slight cold stress",
                    "+9 to +17.5: no thermal stress",
                    "+17.5 to +26: no thermal stress",
                    "+26 to +29: moderate heat stress",
                    "+29 to +32: moderate heat stress",
                    "+32 to +35: strong heat stress",
                    "+35 to +38: strong heat stress",
                    "+38 to +42: very strong heat stress",
                    "+42 to +46: very strong heat stress",
                    "above +46: extreme heat stress",
                    };
            }
            else {
                binNum_UTCICategory = 10;
                UTCI_category = new string[] {
                    "below -40: extreme cold stress",
                    "-27 to -40: very strong cold stress",
                    "-13 to -27: strong cold stress",
                    "-13.0 to 0: moderate cold stress",
                    "+0 to +9: slight cold stress",
                    "+9 to +26: no thermal stress",
                    "+26 to +32: moderate heat stress",
                    "+32 to +38: strong heat stress",
                    "+38 to +46: very strong heat stress",
                    "above +46: extreme heat stress"
                };
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

            for (int i = 0; i < binNum_UTCICategory; i++)
            {
                UTCI_bin_side.Add(0);
                UTCI_bin.Add(0);
            }

            //for diurnal plots:  first array is annual, then monthly. put the UTCI temps in the hourly bin from 0:00 - 23:00
            int binNum_diurnal = 24;
            int monthly = 13;
            double[,] UTCI_bin_diurnal = new double[13,24];
            double[,] UTCI_bin_diurnal_side = new double[13,24];
            string[,] UTCI_diurnal = new string[13,24];

            for (int i = 0; i < binNum_diurnal; i++)
            {
                UTCI_diurnal[0, i] = "UTCI_Annual:" + i + "hr";
                UTCI_diurnal[1, i] = "UTCI_January:" + i + "hr";
                UTCI_diurnal[2, i] = "UTCI_February:" + i + "hr";
                UTCI_diurnal[3, i] = "UTCI_March:" + i + "hr";
                UTCI_diurnal[4, i] = "UTCI_April:" + i + "hr";
                UTCI_diurnal[5, i] = "UTCI_May:" + i + "hr";
                UTCI_diurnal[6, i] = "UTCI_June:" + i + "hr";
                UTCI_diurnal[7, i] = "UTCI_July:" + i + "hr";
                UTCI_diurnal[8, i] = "UTCI_August:" + i + "hr";
                UTCI_diurnal[9, i] = "UTCI_September:" + i + "hr";
                UTCI_diurnal[10, i] = "UTCI_October:" + i + "hr";
                UTCI_diurnal[11, i] = "UTCI_November:" + i + "hr";
                UTCI_diurnal[12, i] = "UTCI_December:" + i + "hr";
            }

            //Tair
            double[,] Tair_bin_diurnal = new double[13, 24];
            double[,] Tair_bin_diurnal_side = new double[13, 24];
            string[,] Tair_diurnal = new string[13, 24];

            for (int i = 0; i < binNum_diurnal; i++)
            {
                Tair_diurnal[0, i] = "Tair_Annual:" + i + "hr";
                Tair_diurnal[1, i] = "Tair_January:" + i + "hr";
                Tair_diurnal[2, i] = "Tair_February:" + i + "hr";
                Tair_diurnal[3, i] = "Tair_March:" + i + "hr";
                Tair_diurnal[4, i] = "Tair_April:" + i + "hr";
                Tair_diurnal[5, i] = "Tair_May:" + i + "hr";
                Tair_diurnal[6, i] = "Tair_June:" + i + "hr";
                Tair_diurnal[7, i] = "Tair_July:" + i + "hr";
                Tair_diurnal[8, i] = "Tair_August:" + i + "hr";
                Tair_diurnal[9, i] = "Tair_September:" + i + "hr";
                Tair_diurnal[10, i] = "Tair_October:" + i + "hr";
                Tair_diurnal[11, i] = "Tair_November:" + i + "hr";
                Tair_diurnal[12, i] = "Tair_December:" + i + "hr";

            }

            //Degree Days Calcuation
            //monthly temperature average
            
            double[] someDoubles = { 1, 2, 3, 4 };
            double average = someDoubles.Average();
            double sumOfSquaresOfDifferences = someDoubles.Select(val => (val - average) * (val - average)).Sum();
            double sd = Math.Sqrt(sumOfSquaresOfDifferences / someDoubles.Length);

            //Console.WriteLine("sd: " + sd);


            //make histogram
            for (int i = 0; i < Tair.Count; i++)
            {
                double Tair_double = Double.Parse(Tair[i]);
                double RH_double = Double.Parse(RH[i]);
                double V_double = Double.Parse(V[i]);
                //Usite ~0.25 Umet, per ASHRAE Fundamentals
                V_double *= 1.5 * Math.Pow((2.0 / 460.0), 0.33);

                //sidewalk
                utci newUTCI_side = new utci(Tair_double, Trad_double_side_list[i], RH_double, V_double);
                newUTCI_value = newUTCI_side.calc_UTCI();
                UTCI_side.Add(newUTCI_value);

                makeUTCIHistogram(newUTCI_value, UTCI_bin_side);
                makeDiurnalHistogram(i, binNum_diurnal, UTCI_bin_diurnal_side, newUTCI_value);
                
                //middle of canyon 
                utci newUTCI = new utci(Tair_double, Trad_double_list[i], RH_double, V_double);
                newUTCI_value = newUTCI.calc_UTCI();
                UTCI.Add(newUTCI_value);
                //Console.WriteLine("UTCI: " + newUTCI_value);

                makeUTCIHistogram(newUTCI_value, UTCI_bin); 
                makeDiurnalHistogram(i, binNum_diurnal, UTCI_bin_diurnal, newUTCI_value);
                makeDiurnalHistogram(i, binNum_diurnal, Tair_bin_diurnal, Tair_double);
            }

            for (int i = 0; i < Tair.Count; i++)
            {
                sw.Write(UTCI_side[i] + "," + UTCI[i] + "," + ",");
                for (int j = 0; j < monthly; j++) { 
                    if (i < binNum_diurnal)
                    {
                        //UTCI
                        if (j == 0)
                        {
                            sw.Write((double)(UTCI_bin_diurnal_side[j, i] / 365) + ","
                            + (double)(UTCI_bin_diurnal[j, i] / 365) + ","
                            + UTCI_diurnal[j, i] + ","
                            + (double)(Tair_bin_diurnal[j, i] / 365) + ","
                            + Tair_diurnal[j, i] + "," + ",");
                        }
                        if (j == 1 || j == 3 || j == 5 || j == 7 || j == 8 || j == 10 || j == 12)
                        {
                            sw.Write((double)(UTCI_bin_diurnal_side[j, i] / 31) + ","
                            + (double)(UTCI_bin_diurnal[j, i] / 31) + ","
                            + UTCI_diurnal[j, i] + ","
                            + (double)(Tair_bin_diurnal[j, i] / 31) + ","
                            + Tair_diurnal[j, i] + "," + ",");
                        }
                        if (j == 2)
                        {
                            sw.Write((double)(UTCI_bin_diurnal_side[j, i] / 28) + ","
                            + (double)(UTCI_bin_diurnal[j, i] / 28) + ","
                            + UTCI_diurnal[j, i] + ","
                            + (double)(Tair_bin_diurnal[j, i] / 28) + ","
                            + Tair_diurnal[j, i] + "," + ",");
                        }
                        if (j == 4 || j == 6 || j == 9|| j == 11)
                        {
                            sw.Write((double)(UTCI_bin_diurnal_side[j, i] / 30) + ","
                            + (double)(UTCI_bin_diurnal[j, i] / 30) + ","
                            + UTCI_diurnal[j, i] + ","
                            + (double)(Tair_bin_diurnal[j, i] / 30) + ","
                            + Tair_diurnal[j, i] + "," + ",");
                        }
                    }
                }

                if (i < UTCI_bin.Count)
                {
                    sw.Write("," + "," +
                        + UTCI_bin_side[i] + ","
                        + String.Format("{0:P0}", ((double)UTCI_bin_side[i] / (double)Tair.Count)) + ","

                        + UTCI_bin[i] + ","
                        + String.Format("{0:P0}", ((double)UTCI_bin[i] / (double)Tair.Count)) + ","
                        + UTCI_category[i]);
                }
                sw.WriteLine();
            }
            EndSimulation();
        }

        public double  calc_T_rad(double Fwall, double Fsky, double Froad, double Twall, double Tsky, double Troad)
        {
            double T_rad_4 = Fwall * Math.Pow(Twall, 4.0)
                + Fsky * Math.Pow(Tsky, 4.0)
                + Froad * Math.Pow(Troad, 4.0);
            double T_radiative = Math.Pow(T_rad_4, 0.25);
            return T_radiative;
        }

        public void makeUTCIHistogram(double newUTCI_value, List<int> UTCI_bin) 
        {
            if (refinedBin)
            {
                if (newUTCI_value > 46.0) UTCI_bin[17] += 1;
                else if (newUTCI_value > 42.0) UTCI_bin[16] += 1;
                else if (newUTCI_value > 38.0) UTCI_bin[15] += 1;
                else if (newUTCI_value > 35.0) UTCI_bin[14] += 1;
                else if (newUTCI_value > 32.0) UTCI_bin[13] += 1;
                else if (newUTCI_value > 29.0) UTCI_bin[12] += 1;
                else if (newUTCI_value > 26.0) UTCI_bin[11] += 1;
                else if (newUTCI_value > 17.5) UTCI_bin[10] += 1;
                else if (newUTCI_value > 9.0) UTCI_bin[9] += 1;
                else if (newUTCI_value > 4.5) UTCI_bin[8] += 1;
                else if (newUTCI_value > 0.0) UTCI_bin[7] += 1;
                else if (newUTCI_value > -6.5) UTCI_bin[6] += 1;
                else if (newUTCI_value > -13.0) UTCI_bin[5] += 1;
                else if (newUTCI_value > -20.0) UTCI_bin[4] += 1;
                else if (newUTCI_value > -27.0) UTCI_bin[3] += 1;
                else if (newUTCI_value > -33.5) UTCI_bin[2] += 1;
                else if (newUTCI_value > -40.0) UTCI_bin[1] += 1;
                else UTCI_bin[0] += 1;
            }
            else
            {
                if (newUTCI_value > 46.0) UTCI_bin[9] += 1;
                else if (newUTCI_value > 38.0) UTCI_bin[8] += 1;
                else if (newUTCI_value > 32.0) UTCI_bin[7] += 1;
                else if (newUTCI_value > 26.0) UTCI_bin[6] += 1;
                else if (newUTCI_value > 9.0) UTCI_bin[5] += 1;
                else if (newUTCI_value > 0.0) UTCI_bin[4] += 1;
                else if (newUTCI_value > -13.0) UTCI_bin[3] += 1;
                else if (newUTCI_value > -27.0) UTCI_bin[2] += 1;
                else if (newUTCI_value > -40.0) UTCI_bin[1] += 1;
                else UTCI_bin[0] += 1;
            }
        }

        public void makeDiurnalHistogram(int i, int binNum, double[,] bin, double output)
        {
            //diurnal, annual
            for (int j = 0; j < binNum; j++)
            {
                if (i % binNum == j) bin[0, j] += output;
            }

            //monthly
            if (i <= (24 * 31))
            {
                for (int j = 0; j < binNum; j++)
                {
                    if (i % binNum == j) bin[1, j] += output;
                }
            }
            else if (i <= (31 + 28) * 24)
            {
                for (int j = 0; j < binNum; j++)
                {
                    if (i % binNum == j) bin[2, j] += output;
                }
            }
            else if (i <= (31 + 28 + 31) * 24)
            {
                for (int j = 0; j < binNum; j++)
                {
                    if (i % binNum == j) bin[3, j] += output;
                }
            }
            else if (i <= (31 + 28 + 31 + 30) * 24)
            {
                for (int j = 0; j < binNum; j++)
                {
                    if (i % binNum == j) bin[4, j] += output;
                }
            }
            else if (i <= (31 + 28 + 31 + 30 + 31) * 24)
            {
                for (int j = 0; j < binNum; j++)
                {
                    if (i % binNum == j) bin[5, j] += output;
                }
            }
            else if (i <= (31 + 28 + 31 + 30 + 31 + 30) * 24)
            {
                for (int j = 0; j < binNum; j++)
                {
                    if (i % binNum == j) bin[6, j] += output;
                }
            }
            else if (i <= (31 + 28 + 31 + 30 + 31 + 30 + 31) * 24)
            {
                for (int j = 0; j < binNum; j++)
                {
                    if (i % binNum == j) bin[7, j] += output;
                }
            }
            else if (i <= (31 + 28 + 31 + 30 + 31 + 30 + 31 + 31) * 24)
            {
                for (int j = 0; j < binNum; j++)
                {
                    if (i % binNum == j) bin[8, j] += output;
                }
            }
            else if (i <= (31 + 28 + 31 + 30 + 31 + 30 + 31 + 31 + 30) * 24)
            {
                for (int j = 0; j < binNum; j++)
                {
                    if (i % binNum == j) bin[9, j] += output;
                }
            }
            else if (i <= (31 + 28 + 31 + 30 + 31 + 30 + 31 + 31 + 30 + 31) * 24)
            {
                for (int j = 0; j < binNum; j++)
                {
                    if (i % binNum == j) bin[10, j] += output;
                }
            }
            else if (i <= (31 + 28 + 31 + 30 + 31 + 30 + 31 + 31 + 30 + 31 + 30) * 24)
            {
                for (int j = 0; j < binNum; j++)
                {
                    if (i % binNum == j) bin[11, j] += output;
                }
            }
            else
            {
                for (int j = 0; j < binNum; j++)
                {
                    if (i % binNum == j) bin[12, j] += output;
                }
            }
        }
    }
}