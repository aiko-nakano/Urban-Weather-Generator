using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Xml;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml.XPath;
//using Excel = Microsoft.Office.Interop.Excel;

namespace UWG
{

    public partial class UWGInputs : Window
    {
        double totalDist = 0.0;
        simuParam p = new simuParam();
        int numberOfSimulation = 1;
        int numberOfTypology = 1;
        private String xmlPath = "";
        private String xmlFileName = "";
        private String filename_xml = "";
        private String xmlFilePath = "";
        private int wallboxi = 0;
        private int massboxi = 0;
        private int urbanRoadboxi = 0;
        private int roofboxi = 0;
        private int glazingboxi = 0;
        private int ruralboxi = 0;
        private int heatboxi = 0;

        private int wallboxiTyp2 = 0;
        private int massboxiTyp2 = 0;
        private int urbanRoadboxiTyp2 = 0;
        private int roofboxiTyp2 = 0;
        private int glazingboxiTyp2 = 0;
        private int ruralboxiTyp2 = 0;
        private int heatboxiTyp2 = 0;

        private int wallboxiTyp3 = 0;
        private int massboxiTyp3 = 0;
        private int urbanRoadboxiTyp3 = 0;
        private int roofboxiTyp3 = 0;
        private int glazingboxiTyp3 = 0;
        private int ruralboxiTyp3 = 0;
        private int heatboxiTyp3 = 0;

        private int wallboxiTyp4 = 0;
        private int massboxiTyp4 = 0;
        private int urbanRoadboxiTyp4 = 0;
        private int roofboxiTyp4 = 0;
        private int glazingboxiTyp4 = 0;
        private int ruralboxiTyp4 = 0;
        private int heatboxiTyp4 = 0;

        public String DialogName;
        private XmlDocument defxml = new XmlDocument();
        private String epwPathRun = "";
        private String epwFileNameRun = "";
        private String epwFileName1 = "";
        private String epwFileName2 = "";
        private String epwFileName3 = "";
        private String epwFileName4 = "";
        private String epwFileName5 = "";
        private String filenameRun = "";
        private String xmlUWGPathRun = "";
        private String xmlUWGFileNameRun = "";
        private String filename_xmlUWGRun = "";
        private String xmlUWGPath1 = "";
        private String xmlUWGFileName1 = "";
        private String filename_xmlUWG1 = "";
        private String xmlUWGPath2 = "";
        private String xmlUWGFileName2 = "";
        private String filename_xmlUWG2 = "";
        private String xmlUWGPath3 = "";
        private String xmlUWGFileName3 = "";
        private String filename_xmlUWG3 = "";
        private String xmlUWGPath4 = "";
        private String xmlUWGFileName4 = "";
        private String filename_xmlUWG4 = "";
        private String xmlUWGPath5 = "";
        private String xmlUWGFileName5 = "";
        private String filename_xmlUWG5 = "";
        private String resultPath="";
        private String resultFileName="";
        private String resultFilePath="";
        private Boolean isTypTab1Visible = false;
        private Boolean isTypTab2Visible = false;
        private Boolean isTypTab3Visible = false;
        private Boolean isTypTab4Visible = false;
        private Boolean isSim1Visible = false;
        private Boolean isSim2Visible = false;
        private Boolean isSim3Visible = false;
        private Boolean isSim4Visible = false;
        private Boolean isSim5Visible = false;
        private Boolean isSim6Visible = false;

        String[,] sim1Data = new String[24, 88];
        String[,] sim2Data = new String[24, 88];
        String[,] sim3Data = new String[24, 88];
        String[,] sim4Data = new String[24, 88];
        String[,] sim5Data = new String[24, 88];
        String[,] sim6Data = new String[24, 88];

        String pathSim1 = "";
        String pathSim2 = "";
        String pathSim3 = "";
        String pathSim4 = "";
        String pathSim5 = "";
        String pathSim6 = "";

        bool loadTyp1 = false;
        bool loadTyp2 = false;
        bool loadTyp3 = false;
        bool loadTyp4 = false;

        public UWGInputs()
        {
            try
            {
                defxml.Load("TemplateLibrary.xml");
            }
            catch
            {
                System.Windows.MessageBox.Show("Initializing failed, TemplateLibrary.xml missing!", "UWG");
                this.Close();
            }
            InitializeComponent();
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "default_input.xml";
            object sender = new object();
            RoutedEventArgs e = new RoutedEventArgs();
            buttonSave_Click(sender, e);
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
            p.simuStartMonthValidate = "1";
            p.simuStartDayValidate = "1";
            p.simuDurationValidate = "365";
            uwgRun.DataContext = p;
            makeFileMenuItem.BorderBrush = new SolidColorBrush(Color.FromArgb(10, 0, 255, 180));
            makeFileMenuItem.BorderThickness = new Thickness(0,0,0,3);
        }
        private void OnChange(object sender, EventArgs e)
        {
            defxml.Load("TemplateLibrary.xml");
            this.IsEnabled = true;
            RoutedEventArgs e1 = new RoutedEventArgs();
            roofbox_load(sender, e1);
            wallbox_load(sender, e1);
            massbox_load(sender, e1);
            urbanRoadbox_load(sender, e1);
            ruralbox_load(sender, e1);
            glazingbox_load(sender, e1);
            heatbox_load(sender, e1);

            roofboxTyp2_load(sender, e1);
            wallboxTyp2_load(sender, e1);
            massboxTyp2_load(sender, e1);
            //urbanRoadboxTyp2_load(sender, e1);
            //ruralboxTyp2_load(sender, e1);
            glazingboxTyp2_load(sender, e1);
            heatboxTyp2_load(sender, e1);

            roofboxTyp3_load(sender, e1);
            wallboxTyp3_load(sender, e1);
            massboxTyp3_load(sender, e1);
            //urbanRoadboxTyp3_load(sender, e1);
            //ruralboxTyp3_load(sender, e1);
            glazingboxTyp3_load(sender, e1);
            heatboxTyp3_load(sender, e1);

            roofboxTyp4_load(sender, e1);
            wallboxTyp4_load(sender, e1);
            massboxTyp4_load(sender, e1);
            //urbanRoadboxTyp4_load(sender, e1);
            //ruralboxTyp4_load(sender, e1);
            glazingboxTyp4_load(sender, e1);
            heatboxTyp4_load(sender, e1);



        }
        private void heatbox_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList heatx = defxml.GetElementsByTagName("HeatTemplate");
            List<string> heatlist = new List<string>();
            heatlist.Add("Blank");
            foreach(XmlNode no in heatx)
            {
                string temp = no.SelectSingleNode("Name").InnerText;
                heatlist.Add(temp);
            }
            heatlist.Add("New Template");
            heatbox.ItemsSource = heatlist;
            if (heatlist[heatboxi] == "New Template") heatboxi = 0;
            if(heatboxi!=0)
            {
                heatbox.SelectedIndex = heatboxi - 1;
                heatbox.SelectedIndex = heatboxi + 1;
            }
            else heatbox.SelectedIndex = 0;
        }
        private void wallbox_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList wallx = defxml.GetElementsByTagName("OpaqueConstruction");
            List<string> walllist = new List<string>();
            walllist.Add("Blank");
            foreach(XmlNode no in wallx)
            {
                if (no.SelectSingleNode("Type").InnerText == "Wall")
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    walllist.Add(temp);
                }
            }
            walllist.Add("New Template");
            wallbox.ItemsSource = walllist;
            if (walllist[wallboxi] == "New Template") wallboxi = 0;
            if (wallboxi != 0)
            {
                wallbox.SelectedIndex = wallboxi - 1;
                wallbox.SelectedIndex = wallboxi + 1;
            }
            else wallbox.SelectedIndex = 0;
        }
        private void massbox_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList massx = defxml.GetElementsByTagName("OpaqueConstruction");
            List<string> masslist = new List<string>();
            masslist.Add("Blank");
            foreach (XmlNode no in massx)
            {
                if (no.SelectSingleNode("Type").InnerText == "Internal Mass")
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    masslist.Add(temp);
                }
            }
            masslist.Add("New Template");
            massbox.ItemsSource = masslist;
            if (masslist[massboxi] == "New Template") massboxi = 0;
            if (massboxi != 0)
            {
                massbox.SelectedIndex = massboxi - 1;
                massbox.SelectedIndex = massboxi + 1;
            }
            else massbox.SelectedIndex = 0;
        }
        private void urbanRoadbox_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList massx = defxml.GetElementsByTagName("OpaqueConstruction");
            List<string> masslist = new List<string>();
            masslist.Add("Default");
            foreach (XmlNode no in massx)
            {
                if (no.SelectSingleNode("Type").InnerText == "Urban Road")
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    masslist.Add(temp);
                }
            }
            masslist.Add("New Template");
            urbanRoadbox.ItemsSource = masslist;
            if (masslist[urbanRoadboxi] == "New Template") urbanRoadboxi = 0;
            if (urbanRoadboxi != 0)
            {
                urbanRoadbox.SelectedIndex = urbanRoadboxi - 1;
                urbanRoadbox.SelectedIndex = urbanRoadboxi + 1;
            }
            else urbanRoadbox.SelectedIndex = 0;
        }
        private void ruralbox_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList massx = defxml.GetElementsByTagName("OpaqueConstruction");
            List<string> masslist = new List<string>();
            masslist.Add("Default");
            foreach (XmlNode no in massx)
            {
                if (no.SelectSingleNode("Type").InnerText == "Rural Road")
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    masslist.Add(temp);
                }
            }
            masslist.Add("New Template");
            ruralbox.ItemsSource = masslist;
            if (masslist[ruralboxi] == "New Template") ruralboxi = 0;
            if (ruralboxi != 0)
            {
                ruralbox.SelectedIndex = ruralboxi - 1;
                ruralbox.SelectedIndex = ruralboxi + 1;
            }
            else ruralbox.SelectedIndex = 0;
        }
        private void roofbox_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList roofx = defxml.GetElementsByTagName("OpaqueConstruction");
            List<string> rooflist = new List<string>();
            rooflist.Add("Blank");
            foreach (XmlNode no in roofx)
            {
                if (no.SelectSingleNode("Type").InnerText == "Roof")
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    rooflist.Add(temp);
                }
            }
            rooflist.Add("New Template");
            roofbox.ItemsSource = rooflist;
            if (rooflist[roofboxi] == "New Template") roofboxi = 0;
            if (roofboxi != 0)
            {
                roofbox.SelectedIndex = roofboxi - 1;
                roofbox.SelectedIndex = roofboxi + 1;
            }
            else roofbox.SelectedIndex = 0;
        }
        private void glazingbox_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList glazingx = defxml.GetElementsByTagName("GlazingConstruction");
            List<string> glazinglist = new List<string>();
            glazinglist.Add("Blank");
            foreach (XmlNode no in glazingx)
            {
                if (no.SelectNodes("WWR").Count != 0)
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    glazinglist.Add(temp);
                }
            }
            glazinglist.Add("New Template");
            glazingbox.ItemsSource = glazinglist;
            if (glazinglist[glazingboxi] == "New Template") glazingboxi = 0;
            if (glazingboxi != 0)
            {
                glazingbox.SelectedIndex = glazingboxi - 1;
                glazingbox.SelectedIndex = glazingboxi + 1;
            }
            else glazingbox.SelectedIndex = 0;
        } 
        private void roofbox_change(object sender, RoutedEventArgs e)
        {
            int ind = roofbox.SelectedIndex;
            roofLayer1K.Content = "";
            roofLayer2K.Content = "";
            roofLayer3K.Content = "";
            roofLayer1VHC.Content = "";
            roofLayer2VHC.Content = "";
            roofLayer3VHC.Content = "";
            if (ind == 0)
            {
                roofLayer1Material.Content = "";
                roofLayer1Thickness.Content = "";
                roofLayer2Material.Content = "";
                roofLayer2Thickness.Content = "";
                roofLayer3Material.Content = "";
                roofLayer3Thickness.Content = "";
                roofLayer1Material.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                roofLayer1Material.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                roofLayer1Material.Margin = new Thickness(4, 4, 4, 4);
                roofLayer1Material.Padding = new Thickness(0, 0, 0, 0);
                roofLayer2Material.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                roofLayer2Material.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                roofLayer2Material.Margin = new Thickness(4, 4, 4, 4);
                roofLayer2Material.Padding = new Thickness(0, 0, 0, 0);
                roofLayer3Material.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                roofLayer3Material.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                roofLayer3Material.Margin = new Thickness(4, 4, 4, 4);
                roofLayer3Material.Padding = new Thickness(0, 0, 0, 0);
                roofboxi = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            int i = 0;
            if (ind == 0) return;
            XmlNodeList roofx = defxml.GetElementsByTagName("OpaqueConstruction");
            XmlNode croof = defxml.CreateElement("OpaqueConstruction");
            int j = 0;
            foreach(XmlNode nod in roofx)
            {
                if(nod.SelectSingleNode("Type").InnerText=="Roof")
                {
                    i++;
                }
                if(i==ind)
                {
                    croof = nod;
                    break;
                }
                j++;
                if (j == roofx.Count && ind == i + 1)
                {
                    roofbox.SelectedIndex = roofboxi;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= roofx.Count)
                {
                    roofbox.SelectedIndex = 0;
                    return;
                }
            }
            roofboxi = ind;
            XmlNodeList names = croof.SelectNodes("Layers/OpaqueLayer");
            XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
            for (ind = 0; ind != names.Count; ind++)
            {
                if (ind == 0)
                {
                    roofLayer1Material.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    roofLayer1Thickness.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach(XmlNode ma in mat)
                    {
                        if(ma.SelectSingleNode("Name").InnerText == roofLayer1Material.Content.ToString())
                        {
                            roofLayer1K.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            roofLayer1VHC.Content = ma.SelectSingleNode("VHC").InnerText;
                            roofAlbedo.Text = ma.SelectSingleNode("Albedo").InnerText;
                            roofEmissivity.Text = ma.SelectSingleNode("Emissivity").InnerText;
                        }
                    }
                }
                if (ind == 1)
                {
                    roofLayer2Material.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    roofLayer2Thickness.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == roofLayer2Material.Content.ToString())
                        {
                            roofLayer2K.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            roofLayer2VHC.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
                if (ind == 2)
                {
                    roofLayer3Material.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    roofLayer3Thickness.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == roofLayer3Material.Content.ToString())
                        {
                            roofLayer3K.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            roofLayer3VHC.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
            }
            for (ind = names.Count; ind <= 2; ind++)
            {
                if (ind == 0)
                {
                    roofLayer1Material.Content = "";
                    roofLayer1Thickness.Content = "";
                    roofLayer1K.Content = "";
                    roofLayer1VHC.Content = "";
                }
                if (ind == 1)
                {
                    roofLayer2Material.Content = "";
                    roofLayer2Thickness.Content = "";
                    roofLayer2K.Content = "";
                    roofLayer2VHC.Content = "";
                }
                if (ind == 2)
                {
                    roofLayer3Material.Content = "";
                    roofLayer3Thickness.Content = "";
                    roofLayer3K.Content = "";
                    roofLayer3VHC.Content = "";
                }
            }
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        private void wallbox_change(object sender, RoutedEventArgs e)
        {
            int ind = wallbox.SelectedIndex;
            wallLayer1K.Content = "";
            wallLayer2K.Content = "";
            wallLayer3K.Content = "";
            wallLayer4K.Content = "";
            wallLayer1VHC.Content = "";
            wallLayer2VHC.Content = "";
            wallLayer3VHC.Content = "";
            wallLayer4VHC.Content = "";
            if (ind == 0)
            {
                wallLayer1Material.Content = "";
                wallLayer1Thickness.Content = "";
                wallLayer2Material.Content = "";
                wallLayer2Thickness.Content = "";
                wallLayer3Material.Content = "";
                wallLayer3Thickness.Content = "";
                wallLayer4Material.Content = "";
                wallLayer4Thickness.Content = "";
                wallLayer1Material.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wallLayer2Material.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wallLayer3Material.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wallLayer4Material.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wallboxi = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            int i = 0;
            if (ind == 0) return;
            XmlNodeList wallx = defxml.GetElementsByTagName("OpaqueConstruction");
            XmlNode cwall = defxml.CreateElement("OpaqueConstruction");
            int j = 0;
            foreach (XmlNode nod in wallx)
            {
                if (nod.SelectSingleNode("Type").InnerText == "Wall")
                {
                    i++;
                }
                if (i == ind)
                {
                    cwall = nod;
                    break;
                }
                j++;
                if (j == wallx.Count && ind == i + 1)
                {
                    wallbox.SelectedIndex = wallboxi;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= wallx.Count)
                {
                    wallbox.SelectedIndex = 0;
                    return;
                }
            }
            wallboxi = ind;
            XmlNodeList names = cwall.SelectNodes("Layers/OpaqueLayer");
            XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
            for (ind = 0; ind != names.Count; ind++)
            {
                if (ind == 0)
                {
                    wallLayer1Material.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    wallLayer1Thickness.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer1Material.Content.ToString())
                        {
                            wallLayer1K.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            wallLayer1VHC.Content = ma.SelectSingleNode("VHC").InnerText;
                            wallAlbedo.Text = ma.SelectSingleNode("Albedo").InnerText;
                            wallEmissivity.Text = ma.SelectSingleNode("Emissivity").InnerText;
                        }
                    }
                }
                if (ind == 1)
                {
                    wallLayer2Material.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    wallLayer2Thickness.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer2Material.Content.ToString())
                        {
                            wallLayer2K.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            wallLayer2VHC.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
                if (ind == 2)
                {
                    wallLayer3Material.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    wallLayer3Thickness.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer3Material.Content.ToString())
                        {
                            wallLayer3K.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            wallLayer3VHC.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
                if (ind == 3)
                {
                    wallLayer4Material.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    wallLayer4Thickness.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer4Material.Content.ToString())
                        {
                            wallLayer4K.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            wallLayer4VHC.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
            }
            for (ind = names.Count; ind <= 3; ind++)
            {
                if (ind == 0)
                {
                    wallLayer1Material.Content = "";
                    wallLayer1Thickness.Content = "";
                    wallLayer1K.Content = "";
                    wallLayer1VHC.Content = "";
                }
                if (ind == 1)
                {
                    wallLayer2Material.Content = "";
                    wallLayer2Thickness.Content = "";
                    wallLayer2K.Content = "";
                    wallLayer2VHC.Content = "";
                }
                if (ind == 2)
                {
                    wallLayer3Material.Content = "";
                    wallLayer3Thickness.Content = "";
                    wallLayer3K.Content = "";
                    wallLayer3VHC.Content = "";
                }
                if (ind == 3)
                {
                    wallLayer4Material.Content = "";
                    wallLayer4Thickness.Content = "";
                    wallLayer4K.Content = "";
                    wallLayer4VHC.Content = "";
                }
            }
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        private void massbox_change(object sender, RoutedEventArgs e)
        {
            int ind = massbox.SelectedIndex;
            int i = 0;
            if (ind == 0)
            {
                massLayer1Material.Content = "";
                massLayer1Thickness.Content = "";
                massLayer1Material.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                massboxi = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            XmlNodeList massx = defxml.GetElementsByTagName("OpaqueConstruction");
            XmlNode cmass = defxml.CreateElement("OpaqueConstruction");
            int j = 0;
            foreach (XmlNode nod in massx)
            {
                if (nod.SelectSingleNode("Type").InnerText == "Internal Mass")
                {
                    i++;
                }
                if (i == ind)
                {
                    cmass = nod;
                    break;
                }
                j++;
                if (j == massx.Count && ind == i + 1)
                {
                    massbox.SelectedIndex = massboxi;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= massx.Count)
                {
                    massbox.SelectedIndex = 0;
                    return;
                }
            }
            massboxi = ind;
            XmlNodeList names = cmass.SelectNodes("Layers/OpaqueLayer");
            XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
            ind = 0;
            massLayer1Material.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
            massLayer1Thickness.Content = names[ind].SelectSingleNode("Thickness").InnerText;
            foreach (XmlNode ma in mat)
            {
                if (ma.SelectSingleNode("Name").InnerText == massLayer1Material.Content.ToString())
                {
                    massLayer1K.Content = ma.SelectSingleNode("Conductivity").InnerText;
                    massLayer1VHC.Content = ma.SelectSingleNode("VHC").InnerText;
                    massAlbedo.Content = ma.SelectSingleNode("Albedo").InnerText;
                    massEmissivity.Content = ma.SelectSingleNode("Emissivity").InnerText;
                }
            }
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        private void urbanRoadbox_change(object sender, RoutedEventArgs e)
        {
            int ind = urbanRoadbox.SelectedIndex;
            int i = 0;
            if (ind == 0)
            {
                urbanRoadMaterial.Content = "asphalt";
                urbanRoadThickness.Content = "1.25";
                urbanRoadVegFraction.Text = "0.5";
                urbanRoadVHC.Content = "1600000";
                urbanRoadK.Content = "1";
                urbanRoadEmissivity.Content = "0.95";
                urbanRoadAlbedo.Content = "0.165";
                urbanRoadboxi = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            XmlNodeList uRoadx = defxml.GetElementsByTagName("OpaqueConstruction");
            XmlNode cuRoad = defxml.CreateElement("OpaqueConstruction");
            int j=0;
            foreach (XmlNode nod in uRoadx)
            {
                if (nod.SelectSingleNode("Type").InnerText == "Urban Road")
                {
                    i++;
                }
                if (i == ind)
                {
                    cuRoad = nod;
                    break;
                }
                j++;
                if (j == uRoadx.Count && ind == i + 1)
                {
                    urbanRoadbox.SelectedIndex = urbanRoadboxi;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= uRoadx.Count)
                {
                    urbanRoadbox.SelectedIndex = 0;
                    return;
                }
            }
            urbanRoadboxi = ind;
            XmlNodeList names = cuRoad.SelectNodes("Layers/OpaqueLayer");
            XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
            ind = 0;
            urbanRoadMaterial.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
            urbanRoadThickness.Content = names[ind].SelectSingleNode("Thickness").InnerText;
            foreach (XmlNode ma in mat)
            {
                if (ma.SelectSingleNode("Name").InnerText == urbanRoadMaterial.Content.ToString())
                {
                    urbanRoadK.Content = ma.SelectSingleNode("Conductivity").InnerText;
                    urbanRoadVHC.Content = ma.SelectSingleNode("VHC").InnerText;
                    urbanRoadAlbedo.Content = ma.SelectSingleNode("Albedo").InnerText;
                    urbanRoadEmissivity.Content = ma.SelectSingleNode("Emissivity").InnerText;
                }
            }
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        private void ruralbox_change(object sender, RoutedEventArgs e)
        {
            int ind = ruralbox.SelectedIndex;
            int i = 0;
            if (ind == 0)
            {
                ruralRoadMaterial.Content = "asphalt";
                ruralRoadThickness.Content = "1.25";
                ruralRoadVHC.Content = "1600000";
                ruralRoadK.Content = "1";
                ruralRoadEmissivity.Content = "0.95";
                ruralRoadAlbedo.Content = "0.165";
                ruralRoadVegFraction.Text = "0.5";
                ruralboxi = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            XmlNodeList rRoadx = defxml.GetElementsByTagName("OpaqueConstruction");
            XmlNode crRoad = defxml.CreateElement("OpaqueConstruction");
            int j = 0;
            foreach (XmlNode nod in rRoadx)
            {
                if (nod.SelectSingleNode("Type").InnerText == "Rural Road")
                {
                    i++;
                }
                if (i == ind)
                {
                    crRoad = nod;
                    break;
                }
                j++;
                if (j == rRoadx.Count && ind == i + 1)
                {
                    ruralbox.SelectedIndex = ruralboxi;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= rRoadx.Count)
                {
                    ruralbox.SelectedIndex = 0;
                    return;
                }
            }
            ruralboxi = ind;
            XmlNodeList names = crRoad.SelectNodes("Layers/OpaqueLayer");
            XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
            ind = 0;
            ruralRoadMaterial.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
            ruralRoadThickness.Content = names[ind].SelectSingleNode("Thickness").InnerText;
            foreach (XmlNode ma in mat)
            {
                if (ma.SelectSingleNode("Name").InnerText == ruralRoadMaterial.Content.ToString())
                {
                    ruralRoadK.Content = ma.SelectSingleNode("Conductivity").InnerText;
                    ruralRoadVHC.Content = ma.SelectSingleNode("VHC").InnerText;
                    ruralRoadAlbedo.Content = ma.SelectSingleNode("Albedo").InnerText;
                    ruralRoadEmissivity.Content = ma.SelectSingleNode("Emissivity").InnerText;
                }
            }
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        private void glazingbox_change(object sender, RoutedEventArgs e)
        {
            int ind = glazingbox.SelectedIndex;
            if (ind == 0)
            {
                uValue.Content = "";
                wwr.Content = "";
                SHGC.Content = "";
                glazingboxi = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            int i = 0;
            XmlNodeList rRoadx = defxml.GetElementsByTagName("GlazingConstruction");
            XmlNode crRoad = defxml.CreateElement("GlazingConstruction");
            int j = 0;
            foreach (XmlNode nod in rRoadx)
            {
                if (nod.SelectNodes("WWR").Count != 0)
                {
                    i++;
                }
                if (i == ind)
                {
                    crRoad = nod;
                    break;
                }
                j++;
                if (j == rRoadx.Count && ind==i+1)
                {
                    glazingbox.SelectedIndex = glazingboxi;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 2;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= rRoadx.Count)
                {
                    glazingbox.SelectedIndex = 0;
                    return;
                }
            }
            glazingboxi = ind;
            wwr.Content = crRoad.SelectSingleNode("WWR").InnerText;
            uValue.Content = crRoad.SelectSingleNode("UValue").InnerText;
            SHGC.Content = crRoad.SelectSingleNode("SHGC").InnerText;
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        private void heatbox_change(object sender, RoutedEventArgs e)
        {
            int ind = heatbox.SelectedIndex;
            if (ind == 0)
            {
                dayInternalHeatGain.Content = "";
                nightInternalHeatGain.Content = "";
                infiltration.Content = "";
                ventilation.Content = "";
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            int i = 0;
            XmlNodeList heatx = defxml.GetElementsByTagName("HeatTemplate");
            XmlNode cheat = defxml.CreateElement("HeatTemplate");
            int j = 0;
            foreach (XmlNode nod in heatx)
            {
                i++;
                if (i == ind)
                {
                    cheat = nod;
                    break;
                }
                j++;
                if (j == heatx.Count && ind == i + 1)
                {
                    heatbox.SelectedIndex = heatboxi;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 1;
                    s.buildingTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= heatx.Count)
                {
                    heatbox.SelectedIndex = 0;
                    return;
                }
            }
            heatboxi = ind;
            string Os = cheat.SelectSingleNode("OccupancySched").InnerText;
            string Ls = cheat.SelectSingleNode("LightsSched").InnerText;
            string Es = cheat.SelectSingleNode("EquipSched").InnerText;
            string Is = cheat.SelectSingleNode("InfiltrationSched").InnerText;
            string Vs = cheat.SelectSingleNode("VentilationSched").InnerText;
            double Od = 0;
            double On = 0;
            double Ld = 0;
            double Ln = 0;
            double Ed = 0;
            double En = 0;
            double I = 0;
            double V = 0;
            foreach(XmlNode no in defxml.GetElementsByTagName("WeekSchedule"))
            {
                if(no.SelectSingleNode("Name").InnerText==Os)
                {
                    foreach(XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach(XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if(nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach(XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    if (k < 7 || k > 17) On += Convert.ToDouble(sd.InnerText)/13.0;
                                    else Od += Convert.ToDouble(sd.InnerText)/11.0;
                                    k++;
                                }
                            }
                        }
                    }
                    On = On / 7.0;
                    Od = Od / 7.0;
                }
                if (no.SelectSingleNode("Name").InnerText == Ls)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    if (k < 7 || k > 17) Ln += Convert.ToDouble(sd.InnerText)/13.0;
                                    else Ld += Convert.ToDouble(sd.InnerText)/11.0;
                                    k++;
                                }
                            }
                        }
                    }
                    Ln = Ln / 7.0;
                    Ld = Ld / 7.0;
                }
                if (no.SelectSingleNode("Name").InnerText == Es)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    if (k < 7 || k > 17) En += Convert.ToDouble(sd.InnerText)/13.0;
                                    else Ed += Convert.ToDouble(sd.InnerText)/11.0;
                                    k++;
                                }
                            }
                        }
                    }
                    En = En / 7.0;
                    Ed = Ed / 7.0;
                }
                if (no.SelectSingleNode("Name").InnerText == Is)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    I += Convert.ToDouble(sd.InnerText)/24.0;
                                }
                            }
                        }
                    }
                    I = I / 7.0;
                }
                if (no.SelectSingleNode("Name").InnerText == Vs)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    V += Convert.ToDouble(sd.InnerText)/24.0;
                                }
                            }
                        }
                    }
                    V = V / 7.0;
                }
            }
            Od = Od * Convert.ToDouble(cheat.SelectSingleNode("OccupancyMax").InnerText);
            On = On * Convert.ToDouble(cheat.SelectSingleNode("OccupancyMax").InnerText);
            Ld = Ld * Convert.ToDouble(cheat.SelectSingleNode("LightsMax").InnerText);
            Ln = Ln * Convert.ToDouble(cheat.SelectSingleNode("LightsMax").InnerText);
            Ed = Ed * Convert.ToDouble(cheat.SelectSingleNode("EquipMax").InnerText);
            En = En * Convert.ToDouble(cheat.SelectSingleNode("EquipMax").InnerText);
            I = I * Convert.ToDouble(cheat.SelectSingleNode("InfiltrationMax").InnerText);
            V = V * Convert.ToDouble(cheat.SelectSingleNode("VentilationMax").InnerText);
            dayInternalHeatGain.Content = Convert.ToString(Od + Ld + Ed);
            nightInternalHeatGain.Content = Convert.ToString(On + Ln + En);
            infiltration.Content = Convert.ToString(I);
            ventilation.Content = Convert.ToString(V);
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }

        private void heatboxTyp2_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList heatx = defxml.GetElementsByTagName("HeatTemplate");
            List<string> heatlist = new List<string>();
            heatlist.Add("Blank");
            foreach (XmlNode no in heatx)
            {
                string temp = no.SelectSingleNode("Name").InnerText;
                heatlist.Add(temp);
            }
            heatlist.Add("New Template");
            heatboxTyp2.ItemsSource = heatlist;
            if (heatlist[heatboxiTyp2] == "New Template") heatboxiTyp2 = 0;
            if (heatboxiTyp2 != 0)
            {
                heatboxTyp2.SelectedIndex = heatboxiTyp2 - 1;
                heatboxTyp2.SelectedIndex = heatboxiTyp2 + 1;
            }
            else heatboxTyp2.SelectedIndex = 0;
        }
        private void wallboxTyp2_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList wallx = defxml.GetElementsByTagName("OpaqueConstruction");
            List<string> walllist = new List<string>();
            walllist.Add("Blank");
            foreach (XmlNode no in wallx)
            {
                if (no.SelectSingleNode("Type").InnerText == "Wall")
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    walllist.Add(temp);
                }
            }
            walllist.Add("New Template");
            wallboxTyp2.ItemsSource = walllist;
            if (walllist[wallboxiTyp2] == "New Template") wallboxiTyp2 = 0;
            if (wallboxiTyp2 != 0)
            {
                wallboxTyp2.SelectedIndex = wallboxiTyp2 - 1;
                wallboxTyp2.SelectedIndex = wallboxiTyp2 + 1;
            }
            else wallboxTyp2.SelectedIndex = 0;
        }
        private void massboxTyp2_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList massx = defxml.GetElementsByTagName("OpaqueConstruction");
            List<string> masslist = new List<string>();
            masslist.Add("Blank");
            foreach (XmlNode no in massx)
            {
                if (no.SelectSingleNode("Type").InnerText == "Internal Mass")
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    masslist.Add(temp);
                }
            }
            masslist.Add("New Template");
            massboxTyp2.ItemsSource = masslist;
            if (masslist[massboxiTyp2] == "New Template") massboxiTyp2 = 0;
            if (massboxiTyp2 != 0)
            {
                massboxTyp2.SelectedIndex = massboxiTyp2 - 1;
                massboxTyp2.SelectedIndex = massboxiTyp2 + 1;
            }
            else massboxTyp2.SelectedIndex = 0;
        }
        //private void urbanRoadboxTyp2_load(object sender, RoutedEventArgs e)
        //{
        //    XmlNodeList massx = defxml.GetElementsByTagName("OpaqueConstruction");
        //    List<string> masslist = new List<string>();
        //    masslist.Add("Default");
        //    foreach (XmlNode no in massx)
        //    {
        //        if (no.SelectSingleNode("Type").InnerText == "Urban Road")
        //        {
        //            string temp = no.SelectSingleNode("Name").InnerText;
        //            masslist.Add(temp);
        //        }
        //    }
        //    masslist.Add("New Template");
        //    urbanRoadboxTyp2.ItemsSource = masslist;
        //    if (masslist[urbanRoadboxiTyp2] == "New Template") urbanRoadboxiTyp2 = 0;
        //    if (urbanRoadboxiTyp2 != 0)
        //    {
        //        urbanRoadboxTyp2.SelectedIndex = urbanRoadboxiTyp2 - 1;
        //        urbanRoadboxTyp2.SelectedIndex = urbanRoadboxiTyp2 + 1;
        //    }
        //    else urbanRoadboxTyp2.SelectedIndex = 0;
        //}
        //private void ruralboxTyp2_load(object sender, RoutedEventArgs e)
        //{
        //    XmlNodeList massx = defxml.GetElementsByTagName("OpaqueConstruction");
        //    List<string> masslist = new List<string>();
        //    masslist.Add("Default");
        //    foreach (XmlNode no in massx)
        //    {
        //        if (no.SelectSingleNode("Type").InnerText == "Rural Road")
        //        {
        //            string temp = no.SelectSingleNode("Name").InnerText;
        //            masslist.Add(temp);
        //        }
        //    }
        //    masslist.Add("New Template");
        //    ruralboxTyp2.ItemsSource = masslist;
        //    if (masslist[ruralboxiTyp2] == "New Template") ruralboxiTyp2 = 0;
        //    if (ruralboxiTyp2 != 0)
        //    {
        //        ruralboxTyp2.SelectedIndex = ruralboxiTyp2 - 1;
        //        ruralboxTyp2.SelectedIndex = ruralboxiTyp2 + 1;
        //    }
        //    else ruralboxTyp2.SelectedIndex = 0;
        //}
        private void roofboxTyp2_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList roofx = defxml.GetElementsByTagName("OpaqueConstruction");
            List<string> rooflist = new List<string>();
            rooflist.Add("Blank");
            foreach (XmlNode no in roofx)
            {
                if (no.SelectSingleNode("Type").InnerText == "Roof")
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    rooflist.Add(temp);
                }
            }
            rooflist.Add("New Template");
            roofboxTyp2.ItemsSource = rooflist;
            if (rooflist[roofboxiTyp2] == "New Template") roofboxiTyp2 = 0;
            if (roofboxiTyp2 != 0)
            {
                roofboxTyp2.SelectedIndex = roofboxiTyp2 - 1;
                roofboxTyp2.SelectedIndex = roofboxiTyp2 + 1;
            }
            else roofboxTyp2.SelectedIndex = 0;
        }
        private void glazingboxTyp2_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList glazingx = defxml.GetElementsByTagName("GlazingConstruction");
            List<string> glazinglist = new List<string>();
            glazinglist.Add("Blank");
            foreach (XmlNode no in glazingx)
            {
                if (no.SelectNodes("WWR").Count != 0)
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    glazinglist.Add(temp);
                }
            }
            glazinglist.Add("New Template");
            glazingboxTyp2.ItemsSource = glazinglist;
            if (glazinglist[glazingboxiTyp2] == "New Template") glazingboxiTyp2 = 0;
            if (glazingboxiTyp2 != 0)
            {
                glazingboxTyp2.SelectedIndex = glazingboxiTyp2 - 1;
                glazingboxTyp2.SelectedIndex = glazingboxiTyp2 + 1;
            }
            else glazingboxTyp2.SelectedIndex = 0;
        }
        private void roofboxTyp2_change(object sender, RoutedEventArgs e)
        {
            int ind = roofboxTyp2.SelectedIndex;
            roofLayer1KTyp2.Content = "";
            roofLayer2KTyp2.Content = "";
            roofLayer3KTyp2.Content = "";
            roofLayer1VHCTyp2.Content = "";
            roofLayer2VHCTyp2.Content = "";
            roofLayer3VHCTyp2.Content = "";
            if (ind == 0)
            {
                roofLayer1MaterialTyp2.Content = "";
                roofLayer1ThicknessTyp2.Content = "";
                roofLayer2MaterialTyp2.Content = "";
                roofLayer2ThicknessTyp2.Content = "";
                roofLayer3MaterialTyp2.Content = "";
                roofLayer3ThicknessTyp2.Content = "";
                roofLayer1MaterialTyp2.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                roofLayer1MaterialTyp2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                roofLayer1MaterialTyp2.Margin = new Thickness(4, 4, 4, 4);
                roofLayer1MaterialTyp2.Padding = new Thickness(0, 0, 0, 0);
                roofLayer2MaterialTyp2.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                roofLayer2MaterialTyp2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                roofLayer2MaterialTyp2.Margin = new Thickness(4, 4, 4, 4);
                roofLayer2MaterialTyp2.Padding = new Thickness(0, 0, 0, 0);
                roofLayer3MaterialTyp2.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                roofLayer3MaterialTyp2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                roofLayer3MaterialTyp2.Margin = new Thickness(4, 4, 4, 4);
                roofLayer3MaterialTyp2.Padding = new Thickness(0, 0, 0, 0);
                roofboxiTyp2 = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            int i = 0;
            if (ind == 0) return;
            XmlNodeList roofx = defxml.GetElementsByTagName("OpaqueConstruction");
            XmlNode croof = defxml.CreateElement("OpaqueConstruction");
            int j = 0;
            foreach (XmlNode nod in roofx)
            {
                if (nod.SelectSingleNode("Type").InnerText == "Roof")
                {
                    i++;
                }
                if (i == ind)
                {
                    croof = nod;
                    break;
                }
                j++;
                if (j == roofx.Count && ind == i + 1)
                {
                    roofboxTyp2.SelectedIndex = roofboxiTyp2;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= roofx.Count)
                {
                    roofboxTyp2.SelectedIndex = 0;
                    return;
                }
            }
            roofboxiTyp2 = ind;
            XmlNodeList names = croof.SelectNodes("Layers/OpaqueLayer");
            XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
            for (ind = 0; ind != names.Count; ind++)
            {
                if (ind == 0)
                {
                    roofLayer1MaterialTyp2.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    roofLayer1ThicknessTyp2.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == roofLayer1MaterialTyp2.Content.ToString())
                        {
                            roofLayer1KTyp2.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            roofLayer1VHCTyp2.Content = ma.SelectSingleNode("VHC").InnerText;
                            roofAlbedoTyp2.Text = ma.SelectSingleNode("Albedo").InnerText;
                            roofEmissivityTyp2.Text = ma.SelectSingleNode("Emissivity").InnerText;
                        }
                    }
                }
                if (ind == 1)
                {
                    roofLayer2MaterialTyp2.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    roofLayer2ThicknessTyp2.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == roofLayer2MaterialTyp2.Content.ToString())
                        {
                            roofLayer2KTyp2.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            roofLayer2VHCTyp2.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
                if (ind == 2)
                {
                    roofLayer3MaterialTyp2.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    roofLayer3ThicknessTyp2.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == roofLayer3MaterialTyp2.Content.ToString())
                        {
                            roofLayer3KTyp2.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            roofLayer3VHCTyp2.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
            }
            for (ind = names.Count; ind <= 2; ind++)
            {
                if (ind == 0)
                {
                    roofLayer1MaterialTyp2.Content = "";
                    roofLayer1ThicknessTyp2.Content = "";
                    roofLayer1KTyp2.Content = "";
                    roofLayer1VHCTyp2.Content = "";
                }
                if (ind == 1)
                {
                    roofLayer2MaterialTyp2.Content = "";
                    roofLayer2ThicknessTyp2.Content = "";
                    roofLayer2KTyp2.Content = "";
                    roofLayer2VHCTyp2.Content = "";
                }
                if (ind == 2)
                {
                    roofLayer3MaterialTyp2.Content = "";
                    roofLayer3ThicknessTyp2.Content = "";
                    roofLayer3KTyp2.Content = "";
                    roofLayer3VHCTyp2.Content = "";
                }
            }
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        private void wallboxTyp2_change(object sender, RoutedEventArgs e)
        {
            int ind = wallboxTyp2.SelectedIndex;
            wallLayer1KTyp2.Content = "";
            wallLayer2KTyp2.Content = "";
            wallLayer3KTyp2.Content = "";
            wallLayer4KTyp2.Content = "";
            wallLayer1VHCTyp2.Content = "";
            wallLayer2VHCTyp2.Content = "";
            wallLayer3VHCTyp2.Content = "";
            wallLayer4VHCTyp2.Content = "";
            if (ind == 0)
            {
                wallLayer1MaterialTyp2.Content = "";
                wallLayer1ThicknessTyp2.Content = "";
                wallLayer2MaterialTyp2.Content = "";
                wallLayer2ThicknessTyp2.Content = "";
                wallLayer3MaterialTyp2.Content = "";
                wallLayer3ThicknessTyp2.Content = "";
                wallLayer4MaterialTyp2.Content = "";
                wallLayer4ThicknessTyp2.Content = "";
                wallLayer1MaterialTyp2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wallLayer2MaterialTyp2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wallLayer3MaterialTyp2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wallLayer4MaterialTyp2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wallboxiTyp2 = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            int i = 0;
            if (ind == 0) return;
            XmlNodeList wallx = defxml.GetElementsByTagName("OpaqueConstruction");
            XmlNode cwall = defxml.CreateElement("OpaqueConstruction");
            int j = 0;
            foreach (XmlNode nod in wallx)
            {
                if (nod.SelectSingleNode("Type").InnerText == "Wall")
                {
                    i++;
                }
                if (i == ind)
                {
                    cwall = nod;
                    break;
                }
                j++;
                if (j == wallx.Count && ind == i + 1)
                {
                    wallboxTyp2.SelectedIndex = wallboxiTyp2;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= wallx.Count)
                {
                    wallboxTyp2.SelectedIndex = 0;
                    return;
                }
            }
            wallboxiTyp2 = ind;
            XmlNodeList names = cwall.SelectNodes("Layers/OpaqueLayer");
            XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
            for (ind = 0; ind != names.Count; ind++)
            {
                if (ind == 0)
                {
                    wallLayer1MaterialTyp2.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    wallLayer1ThicknessTyp2.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer1MaterialTyp2.Content.ToString())
                        {
                            wallLayer1KTyp2.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            wallLayer1VHCTyp2.Content = ma.SelectSingleNode("VHC").InnerText;
                            wallAlbedoTyp2.Text = ma.SelectSingleNode("Albedo").InnerText;
                            wallEmissivityTyp2.Text = ma.SelectSingleNode("Emissivity").InnerText;
                        }
                    }
                }
                if (ind == 1)
                {
                    wallLayer2MaterialTyp2.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    wallLayer2ThicknessTyp2.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer2MaterialTyp2.Content.ToString())
                        {
                            wallLayer2KTyp2.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            wallLayer2VHCTyp2.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
                if (ind == 2)
                {
                    wallLayer3MaterialTyp2.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    wallLayer3ThicknessTyp2.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer3MaterialTyp2.Content.ToString())
                        {
                            wallLayer3KTyp2.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            wallLayer3VHCTyp2.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
                if (ind == 3)
                {
                    wallLayer4MaterialTyp2.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    wallLayer4ThicknessTyp2.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer4MaterialTyp2.Content.ToString())
                        {
                            wallLayer4KTyp2.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            wallLayer4VHCTyp2.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
            }
            for (ind = names.Count; ind <= 3; ind++)
            {
                if (ind == 0)
                {
                    wallLayer1MaterialTyp2.Content = "";
                    wallLayer1ThicknessTyp2.Content = "";
                    wallLayer1KTyp2.Content = "";
                    wallLayer1VHCTyp2.Content = "";
                }
                if (ind == 1)
                {
                    wallLayer2MaterialTyp2.Content = "";
                    wallLayer2ThicknessTyp2.Content = "";
                    wallLayer2KTyp2.Content = "";
                    wallLayer2VHCTyp2.Content = "";
                }
                if (ind == 2)
                {
                    wallLayer3MaterialTyp2.Content = "";
                    wallLayer3ThicknessTyp2.Content = "";
                    wallLayer3KTyp2.Content = "";
                    wallLayer3VHCTyp2.Content = "";
                }
                if (ind == 3)
                {
                    wallLayer4MaterialTyp2.Content = "";
                    wallLayer4ThicknessTyp2.Content = "";
                    wallLayer4KTyp2.Content = "";
                    wallLayer4VHCTyp2.Content = "";
                }
            }
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        private void massboxTyp2_change(object sender, RoutedEventArgs e)
        {
            int ind = massboxTyp2.SelectedIndex;
            int i = 0;
            if (ind == 0)
            {
                massLayer1MaterialTyp2.Content = "";
                massLayer1ThicknessTyp2.Content = "";
                massLayer1MaterialTyp2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                massboxiTyp2 = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            XmlNodeList massx = defxml.GetElementsByTagName("OpaqueConstruction");
            XmlNode cmass = defxml.CreateElement("OpaqueConstruction");
            int j = 0;
            foreach (XmlNode nod in massx)
            {
                if (nod.SelectSingleNode("Type").InnerText == "Internal Mass")
                {
                    i++;
                }
                if (i == ind)
                {
                    cmass = nod;
                    break;
                }
                j++;
                if (j == massx.Count && ind == i + 1)
                {
                    massboxTyp2.SelectedIndex = massboxiTyp2;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= massx.Count)
                {
                    massboxTyp2.SelectedIndex = 0;
                    return;
                }
            }
            massboxiTyp2 = ind;
            XmlNodeList names = cmass.SelectNodes("Layers/OpaqueLayer");
            XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
            ind = 0;
            massLayer1MaterialTyp2.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
            massLayer1ThicknessTyp2.Content = names[ind].SelectSingleNode("Thickness").InnerText;
            foreach (XmlNode ma in mat)
            {
                if (ma.SelectSingleNode("Name").InnerText == massLayer1MaterialTyp2.Content.ToString())
                {
                    massLayer1KTyp2.Content = ma.SelectSingleNode("Conductivity").InnerText;
                    massLayer1VHCTyp2.Content = ma.SelectSingleNode("VHC").InnerText;
                    massAlbedoTyp2.Content = ma.SelectSingleNode("Albedo").InnerText;
                    massEmissivityTyp2.Content = ma.SelectSingleNode("Emissivity").InnerText;
                }
            }
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        //private void urbanRoadboxTyp2_change(object sender, RoutedEventArgs e)
        //{
        //    int ind = urbanRoadboxTyp2.SelectedIndex;
        //    int i = 0;
        //    if (ind == 0)
        //    {
        //        urbanRoadMaterialTyp2.Content = "asphalt";
        //        urbanRoadThicknessTyp2.Content = "1.25";
        //        urbanRoadVHCTyp2.Content = "1600000";
        //        urbanRoadKTyp2.Content = "1";
        //        urbanRoadEmissivityTyp2.Content = "0.95";
        //        urbanRoadAlbedoTyp2.Content = "0.165";
        //        urbanRoadboxiTyp2 = 0;
        //        string pathx = Directory.GetCurrentDirectory();
        //        string temp1x = this.xmlPath;
        //        string temp2x = this.xmlFileName;
        //        this.xmlPath = pathx;
        //        this.xmlFileName = "$temp.xml";
        //        buttonSave_Click(sender, e);
        //        var providerx = (XmlDataProvider)this.DataContext;
        //        providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
        //        providerx.Refresh();
        //        this.xmlPath = temp1x;
        //        this.xmlFileName = temp2x;
        //        return;
        //    }
        //    XmlNodeList uRoadx = defxml.GetElementsByTagName("OpaqueConstruction");
        //    XmlNode cuRoad = defxml.CreateElement("OpaqueConstruction");
        //    int j = 0;
        //    foreach (XmlNode nod in uRoadx)
        //    {
        //        if (nod.SelectSingleNode("Type").InnerText == "Urban Road")
        //        {
        //            i++;
        //        }
        //        if (i == ind)
        //        {
        //            cuRoad = nod;
        //            break;
        //        }
        //        j++;
        //        if (j == uRoadx.Count && ind == i + 1)
        //        {
        //            urbanRoadboxTyp2.SelectedIndex = urbanRoadboxiTyp2;
        //            Editor s = new Editor();
        //            s.Top.SelectedIndex = 0;
        //            s.constructionTab.SelectedIndex = 1;
        //            try
        //            {
        //                s.Show();
        //            }
        //            catch { };
        //            this.IsEnabled = false;
        //            s.Closed += new EventHandler(OnChange);
        //            return;
        //        }
        //        if (j >= uRoadx.Count)
        //        {
        //            urbanRoadboxTyp2.SelectedIndex = 0;
        //            return;
        //        }
        //    }
        //    urbanRoadboxiTyp2 = ind;
        //    XmlNodeList names = cuRoad.SelectNodes("Layers/OpaqueLayer");
        //    XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
        //    ind = 0;
        //    urbanRoadMaterialTyp2.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
        //    urbanRoadThicknessTyp2.Content = names[ind].SelectSingleNode("Thickness").InnerText;
        //    foreach (XmlNode ma in mat)
        //    {
        //        if (ma.SelectSingleNode("Name").InnerText == urbanRoadMaterial.Content)
        //        {
        //            urbanRoadKTyp2.Content = ma.SelectSingleNode("Conductivity").InnerText;
        //            urbanRoadVHCTyp2.Content = ma.SelectSingleNode("VHC").InnerText;
        //            urbanRoadAlbedoTyp2.Content = ma.SelectSingleNode("Albedo").InnerText;
        //            urbanRoadEmissivityTyp2.Content = ma.SelectSingleNode("Emissivity").InnerText;
        //        }
        //    }
        //    string path = Directory.GetCurrentDirectory();
        //    string temp1 = this.xmlPath;
        //    string temp2 = this.xmlFileName;
        //    this.xmlPath = path;
        //    this.xmlFileName = "$temp.xml";
        //    buttonSave_Click(sender, e);
        //    var provider = (XmlDataProvider)this.DataContext;
        //    provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
        //    provider.Refresh();
        //    this.xmlPath = temp1;
        //    this.xmlFileName = temp2;
        //}
        //private void ruralboxTyp2_change(object sender, RoutedEventArgs e)
        //{
        //    int ind = ruralboxTyp2.SelectedIndex;
        //    int i = 0;
        //    if (ind == 0)
        //    {
        //        ruralRoadMaterialTyp2.Content = "asphalt";
        //        ruralRoadThicknessTyp2.Content = "1.25";
        //        ruralRoadVHCTyp2.Content = "1600000";
        //        ruralRoadKTyp2.Content = "1";
        //        ruralRoadEmissivityTyp2.Content = "0.95";
        //        ruralRoadAlbedoTyp2.Content = "0.165";
        //        ruralboxiTyp2 = 0;
        //        string pathx = Directory.GetCurrentDirectory();
        //        string temp1x = this.xmlPath;
        //        string temp2x = this.xmlFileName;
        //        this.xmlPath = pathx;
        //        this.xmlFileName = "$temp.xml";
        //        buttonSave_Click(sender, e);
        //        var providerx = (XmlDataProvider)this.DataContext;
        //        providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
        //        providerx.Refresh();
        //        this.xmlPath = temp1x;
        //        this.xmlFileName = temp2x;
        //        return;
        //    }
        //    XmlNodeList rRoadx = defxml.GetElementsByTagName("OpaqueConstruction");
        //    XmlNode crRoad = defxml.CreateElement("OpaqueConstruction");
        //    int j = 0;
        //    foreach (XmlNode nod in rRoadx)
        //    {
        //        if (nod.SelectSingleNode("Type").InnerText == "Rural Road")
        //        {
        //            i++;
        //        }
        //        if (i == ind)
        //        {
        //            crRoad = nod;
        //            break;
        //        }
        //        j++;
        //        if (j == rRoadx.Count && ind == i + 1)
        //        {
        //            ruralboxTyp2.SelectedIndex = ruralboxiTyp2;
        //            Editor s = new Editor();
        //            s.Top.SelectedIndex = 0;
        //            s.constructionTab.SelectedIndex = 1;
        //            try
        //            {
        //                s.Show();
        //            }
        //            catch { };
        //            this.IsEnabled = false;
        //            s.Closed += new EventHandler(OnChange);
        //            return;
        //        }
        //        if (j >= rRoadx.Count)
        //        {
        //            ruralbox.SelectedIndex = 0;
        //            return;
        //        }
        //    }
        //    ruralboxiTyp2 = ind;
        //    XmlNodeList names = crRoad.SelectNodes("Layers/OpaqueLayer");
        //    XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
        //    ind = 0;
        //    ruralRoadMaterialTyp2.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
        //    ruralRoadThicknessTyp2.Content = names[ind].SelectSingleNode("Thickness").InnerText;
        //    foreach (XmlNode ma in mat)
        //    {
        //        if (ma.SelectSingleNode("Name").InnerText == ruralRoadMaterial.Content)
        //        {
        //            ruralRoadKTyp2.Content = ma.SelectSingleNode("Conductivity").InnerText;
        //            ruralRoadVHCTyp2.Content = ma.SelectSingleNode("VHC").InnerText;
        //            ruralRoadAlbedoTyp2.Content = ma.SelectSingleNode("Albedo").InnerText;
        //            ruralRoadEmissivityTyp2.Content = ma.SelectSingleNode("Emissivity").InnerText;
        //        }
        //    }
        //    string path = Directory.GetCurrentDirectory();
        //    string temp1 = this.xmlPath;
        //    string temp2 = this.xmlFileName;
        //    this.xmlPath = path;
        //    this.xmlFileName = "$temp.xml";
        //    buttonSave_Click(sender, e);
        //    var provider = (XmlDataProvider)this.DataContext;
        //    provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
        //    provider.Refresh();
        //    this.xmlPath = temp1;
        //    this.xmlFileName = temp2;
        //}
        private void glazingboxTyp2_change(object sender, RoutedEventArgs e)
        {
            int ind = glazingboxTyp2.SelectedIndex;
            if (ind == 0)
            {
                uValueTyp2.Content = "";
                wwrTyp2.Content = "";
                SHGCTyp2.Content = "";
                glazingboxiTyp2 = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            int i = 0;
            XmlNodeList rRoadx = defxml.GetElementsByTagName("GlazingConstruction");
            XmlNode crRoad = defxml.CreateElement("GlazingConstruction");
            int j = 0;
            foreach (XmlNode nod in rRoadx)
            {
                if (nod.SelectNodes("WWR").Count != 0)
                {
                    i++;
                }
                if (i == ind)
                {
                    crRoad = nod;
                    break;
                }
                j++;
                if (j == rRoadx.Count && ind == i + 1)
                {
                    glazingboxTyp2.SelectedIndex = glazingboxiTyp2;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 2;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= rRoadx.Count)
                {
                    glazingboxTyp2.SelectedIndex = 0;
                    return;
                }
            }
            glazingboxiTyp2 = ind;
            wwrTyp2.Content = crRoad.SelectSingleNode("WWR").InnerText;
            uValueTyp2.Content = crRoad.SelectSingleNode("UValue").InnerText;
            SHGCTyp2.Content = crRoad.SelectSingleNode("SHGC").InnerText;
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        private void heatboxTyp2_change(object sender, RoutedEventArgs e)
        {
            int ind = heatboxTyp2.SelectedIndex;
            if (ind == 0)
            {
                dayInternalHeatGainTyp2.Content = "";
                nightInternalHeatGainTyp2.Content = "";
                infiltrationTyp2.Content = "";
                ventilationTyp2.Content = "";
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            int i = 0;
            XmlNodeList heatx = defxml.GetElementsByTagName("HeatTemplate");
            XmlNode cheat = defxml.CreateElement("HeatTemplate");
            int j = 0;
            foreach (XmlNode nod in heatx)
            {
                i++;
                if (i == ind)
                {
                    cheat = nod;
                    break;
                }
                j++;
                if (j == heatx.Count && ind == i + 1)
                {
                    heatboxTyp2.SelectedIndex = heatboxiTyp2;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 1;
                    s.buildingTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= heatx.Count)
                {
                    heatboxTyp2.SelectedIndex = 0;
                    return;
                }
            }
            heatboxiTyp2 = ind;
            string Os = cheat.SelectSingleNode("OccupancySched").InnerText;
            string Ls = cheat.SelectSingleNode("LightsSched").InnerText;
            string Es = cheat.SelectSingleNode("EquipSched").InnerText;
            string Is = cheat.SelectSingleNode("InfiltrationSched").InnerText;
            string Vs = cheat.SelectSingleNode("VentilationSched").InnerText;
            double Od = 0;
            double On = 0;
            double Ld = 0;
            double Ln = 0;
            double Ed = 0;
            double En = 0;
            double I = 0;
            double V = 0;
            foreach (XmlNode no in defxml.GetElementsByTagName("WeekSchedule"))
            {
                if (no.SelectSingleNode("Name").InnerText == Os)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    if (k < 7 || k > 17) On += Convert.ToDouble(sd.InnerText) / 13.0;
                                    else Od += Convert.ToDouble(sd.InnerText) / 11.0;
                                    k++;
                                }
                            }
                        }
                    }
                    On = On / 7.0;
                    Od = Od / 7.0;
                }
                if (no.SelectSingleNode("Name").InnerText == Ls)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    if (k < 7 || k > 17) Ln += Convert.ToDouble(sd.InnerText) / 13.0;
                                    else Ld += Convert.ToDouble(sd.InnerText) / 11.0;
                                    k++;
                                }
                            }
                        }
                    }
                    Ln = Ln / 7.0;
                    Ld = Ld / 7.0;
                }
                if (no.SelectSingleNode("Name").InnerText == Es)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    if (k < 7 || k > 17) En += Convert.ToDouble(sd.InnerText) / 13.0;
                                    else Ed += Convert.ToDouble(sd.InnerText) / 11.0;
                                    k++;
                                }
                            }
                        }
                    }
                    En = En / 7.0;
                    Ed = Ed / 7.0;
                }
                if (no.SelectSingleNode("Name").InnerText == Is)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    I += Convert.ToDouble(sd.InnerText) / 24.0;
                                }
                            }
                        }
                    }
                    I = I / 7.0;
                }
                if (no.SelectSingleNode("Name").InnerText == Vs)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    V += Convert.ToDouble(sd.InnerText) / 24.0;
                                }
                            }
                        }
                    }
                    V = V / 7.0;
                }
            }
            Od = Od * Convert.ToDouble(cheat.SelectSingleNode("OccupancyMax").InnerText);
            On = On * Convert.ToDouble(cheat.SelectSingleNode("OccupancyMax").InnerText);
            Ld = Ld * Convert.ToDouble(cheat.SelectSingleNode("LightsMax").InnerText);
            Ln = Ln * Convert.ToDouble(cheat.SelectSingleNode("LightsMax").InnerText);
            Ed = Ed * Convert.ToDouble(cheat.SelectSingleNode("EquipMax").InnerText);
            En = En * Convert.ToDouble(cheat.SelectSingleNode("EquipMax").InnerText);
            I = I * Convert.ToDouble(cheat.SelectSingleNode("InfiltrationMax").InnerText);
            V = V * Convert.ToDouble(cheat.SelectSingleNode("VentilationMax").InnerText);
            dayInternalHeatGainTyp2.Content = Convert.ToString(Od + Ld + Ed);
            nightInternalHeatGainTyp2.Content = Convert.ToString(On + Ln + En);
            infiltrationTyp2.Content = Convert.ToString(I);
            ventilationTyp2.Content = Convert.ToString(V);
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }

        private void heatboxTyp3_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList heatx = defxml.GetElementsByTagName("HeatTemplate");
            List<string> heatlist = new List<string>();
            heatlist.Add("Blank");
            foreach (XmlNode no in heatx)
            {
                string temp = no.SelectSingleNode("Name").InnerText;
                heatlist.Add(temp);
            }
            heatlist.Add("New Template");
            heatboxTyp3.ItemsSource = heatlist;
            if (heatlist[heatboxiTyp3] == "New Template") heatboxiTyp3 = 0;
            if (heatboxiTyp3 != 0)
            {
                heatboxTyp3.SelectedIndex = heatboxiTyp3 - 1;
                heatboxTyp3.SelectedIndex = heatboxiTyp3 + 1;
            }
            else heatboxTyp3.SelectedIndex = 0;
        }
        private void wallboxTyp3_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList wallx = defxml.GetElementsByTagName("OpaqueConstruction");
            List<string> walllist = new List<string>();
            walllist.Add("Blank");
            foreach (XmlNode no in wallx)
            {
                if (no.SelectSingleNode("Type").InnerText == "Wall")
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    walllist.Add(temp);
                }
            }
            walllist.Add("New Template");
            wallboxTyp3.ItemsSource = walllist;
            if (walllist[wallboxiTyp3] == "New Template") wallboxiTyp3 = 0;
            if (wallboxiTyp3 != 0)
            {
                wallboxTyp3.SelectedIndex = wallboxiTyp3 - 1;
                wallboxTyp3.SelectedIndex = wallboxiTyp3 + 1;
            }
            else wallboxTyp3.SelectedIndex = 0;
        }
        private void massboxTyp3_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList massx = defxml.GetElementsByTagName("OpaqueConstruction");
            List<string> masslist = new List<string>();
            masslist.Add("Blank");
            foreach (XmlNode no in massx)
            {
                if (no.SelectSingleNode("Type").InnerText == "Internal Mass")
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    masslist.Add(temp);
                }
            }
            masslist.Add("New Template");
            massboxTyp3.ItemsSource = masslist;
            if (masslist[massboxiTyp3] == "New Template") massboxiTyp3 = 0;
            if (massboxiTyp3 != 0)
            {
                massboxTyp3.SelectedIndex = massboxiTyp3 - 1;
                massboxTyp3.SelectedIndex = massboxiTyp3 + 1;
            }
            else massboxTyp3.SelectedIndex = 0;
        }
        //private void urbanRoadboxTyp3_load(object sender, RoutedEventArgs e)
        //{
        //    XmlNodeList massx = defxml.GetElementsByTagName("OpaqueConstruction");
        //    List<string> masslist = new List<string>();
        //    masslist.Add("Default");
        //    foreach (XmlNode no in massx)
        //    {
        //        if (no.SelectSingleNode("Type").InnerText == "Urban Road")
        //        {
        //            string temp = no.SelectSingleNode("Name").InnerText;
        //            masslist.Add(temp);
        //        }
        //    }
        //    masslist.Add("New Template");
        //    urbanRoadboxTyp3.ItemsSource = masslist;
        //    if (masslist[urbanRoadboxiTyp3] == "New Template") urbanRoadboxiTyp3 = 0;
        //    if (urbanRoadboxiTyp3 != 0)
        //    {
        //        urbanRoadboxTyp3.SelectedIndex = urbanRoadboxiTyp3 - 1;
        //        urbanRoadboxTyp3.SelectedIndex = urbanRoadboxiTyp3 + 1;
        //    }
        //    else urbanRoadboxTyp3.SelectedIndex = 0;
        //}
        //private void ruralboxTyp3_load(object sender, RoutedEventArgs e)
        //{
        //    XmlNodeList massx = defxml.GetElementsByTagName("OpaqueConstruction");
        //    List<string> masslist = new List<string>();
        //    masslist.Add("Default");
        //    foreach (XmlNode no in massx)
        //    {
        //        if (no.SelectSingleNode("Type").InnerText == "Rural Road")
        //        {
        //            string temp = no.SelectSingleNode("Name").InnerText;
        //            masslist.Add(temp);
        //        }
        //    }
        //    masslist.Add("New Template");
        //    ruralboxTyp3.ItemsSource = masslist;
        //    if (masslist[ruralboxiTyp3] == "New Template") ruralboxiTyp3 = 0;
        //    if (ruralboxiTyp3 != 0)
        //    {
        //        ruralboxTyp3.SelectedIndex = ruralboxiTyp3 - 1;
        //        ruralboxTyp3.SelectedIndex = ruralboxiTyp3 + 1;
        //    }
        //    else ruralboxTyp3.SelectedIndex = 0;
        //}
        private void roofboxTyp3_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList roofx = defxml.GetElementsByTagName("OpaqueConstruction");
            List<string> rooflist = new List<string>();
            rooflist.Add("Blank");
            foreach (XmlNode no in roofx)
            {
                if (no.SelectSingleNode("Type").InnerText == "Roof")
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    rooflist.Add(temp);
                }
            }
            rooflist.Add("New Template");
            roofboxTyp3.ItemsSource = rooflist;
            if (rooflist[roofboxiTyp3] == "New Template") roofboxiTyp3 = 0;
            if (roofboxiTyp3 != 0)
            {
                roofboxTyp3.SelectedIndex = roofboxiTyp3 - 1;
                roofboxTyp3.SelectedIndex = roofboxiTyp3 + 1;
            }
            else roofboxTyp3.SelectedIndex = 0;
        }
        private void glazingboxTyp3_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList glazingx = defxml.GetElementsByTagName("GlazingConstruction");
            List<string> glazinglist = new List<string>();
            glazinglist.Add("Blank");
            foreach (XmlNode no in glazingx)
            {
                if (no.SelectNodes("WWR").Count != 0)
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    glazinglist.Add(temp);
                }
            }
            glazinglist.Add("New Template");
            glazingboxTyp3.ItemsSource = glazinglist;
            if (glazinglist[glazingboxiTyp3] == "New Template") glazingboxiTyp3 = 0;
            if (glazingboxiTyp3 != 0)
            {
                glazingboxTyp3.SelectedIndex = glazingboxiTyp3 - 1;
                glazingboxTyp3.SelectedIndex = glazingboxiTyp3 + 1;
            }
            else glazingboxTyp3.SelectedIndex = 0;
        }
        private void roofboxTyp3_change(object sender, RoutedEventArgs e)
        {
            int ind = roofboxTyp3.SelectedIndex;
            roofLayer1KTyp3.Content = "";
            roofLayer2KTyp3.Content = "";
            roofLayer3KTyp3.Content = "";
            roofLayer1VHCTyp3.Content = "";
            roofLayer2VHCTyp3.Content = "";
            roofLayer3VHCTyp3.Content = "";
            if (ind == 0)
            {
                roofLayer1MaterialTyp3.Content = "";
                roofLayer1ThicknessTyp3.Content = "";
                roofLayer2MaterialTyp3.Content = "";
                roofLayer2ThicknessTyp3.Content = "";
                roofLayer3MaterialTyp3.Content = "";
                roofLayer3ThicknessTyp3.Content = "";
                roofLayer1MaterialTyp3.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                roofLayer1MaterialTyp3.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                roofLayer1MaterialTyp3.Margin = new Thickness(4, 4, 4, 4);
                roofLayer1MaterialTyp3.Padding = new Thickness(0, 0, 0, 0);
                roofLayer2MaterialTyp3.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                roofLayer2MaterialTyp3.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                roofLayer2MaterialTyp3.Margin = new Thickness(4, 4, 4, 4);
                roofLayer2MaterialTyp3.Padding = new Thickness(0, 0, 0, 0);
                roofLayer3MaterialTyp3.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                roofLayer3MaterialTyp3.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                roofLayer3MaterialTyp3.Margin = new Thickness(4, 4, 4, 4);
                roofLayer3MaterialTyp3.Padding = new Thickness(0, 0, 0, 0); 
                roofboxiTyp3 = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            int i = 0;
            if (ind == 0) return;
            XmlNodeList roofx = defxml.GetElementsByTagName("OpaqueConstruction");
            XmlNode croof = defxml.CreateElement("OpaqueConstruction");
            int j = 0;
            foreach (XmlNode nod in roofx)
            {
                if (nod.SelectSingleNode("Type").InnerText == "Roof")
                {
                    i++;
                }
                if (i == ind)
                {
                    croof = nod;
                    break;
                }
                j++;
                if (j == roofx.Count && ind == i + 1)
                {
                    roofboxTyp3.SelectedIndex = roofboxiTyp3;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= roofx.Count)
                {
                    roofboxTyp3.SelectedIndex = 0;
                    return;
                }
            }
            roofboxiTyp3 = ind;
            XmlNodeList names = croof.SelectNodes("Layers/OpaqueLayer");
            XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
            for (ind = 0; ind != names.Count; ind++)
            {
                if (ind == 0)
                {
                    roofLayer1MaterialTyp3.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    roofLayer1ThicknessTyp3.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == roofLayer1MaterialTyp3.Content.ToString())
                        {
                            roofLayer1KTyp3.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            roofLayer1VHCTyp3.Content = ma.SelectSingleNode("VHC").InnerText;
                            roofAlbedoTyp3.Text = ma.SelectSingleNode("Albedo").InnerText;
                            roofEmissivityTyp3.Text = ma.SelectSingleNode("Emissivity").InnerText;
                        }
                    }
                }
                if (ind == 1)
                {
                    roofLayer2MaterialTyp3.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    roofLayer2ThicknessTyp3.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == roofLayer2MaterialTyp3.Content.ToString())
                        {
                            roofLayer2KTyp3.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            roofLayer2VHCTyp3.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
                if (ind == 2)
                {
                    roofLayer3MaterialTyp3.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    roofLayer3ThicknessTyp3.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == roofLayer3MaterialTyp3.Content.ToString())
                        {
                            roofLayer3KTyp3.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            roofLayer3VHCTyp3.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
            }
            for (ind = names.Count; ind <= 2; ind++)
            {
                if (ind == 0)
                {
                    roofLayer1MaterialTyp3.Content = "";
                    roofLayer1ThicknessTyp3.Content = "";
                    roofLayer1KTyp3.Content = "";
                    roofLayer1VHCTyp3.Content = "";
                }
                if (ind == 1)
                {
                    roofLayer2MaterialTyp3.Content = "";
                    roofLayer2ThicknessTyp3.Content = "";
                    roofLayer2KTyp3.Content = "";
                    roofLayer2VHCTyp3.Content = "";
                }
                if (ind == 2)
                {
                    roofLayer3MaterialTyp3.Content = "";
                    roofLayer3ThicknessTyp3.Content = "";
                    roofLayer3KTyp3.Content = "";
                    roofLayer3VHCTyp3.Content = "";
                }
            }
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        private void wallboxTyp3_change(object sender, RoutedEventArgs e)
        {
            int ind = wallboxTyp3.SelectedIndex;
            wallLayer1KTyp3.Content = "";
            wallLayer2KTyp3.Content = "";
            wallLayer3KTyp3.Content = "";
            wallLayer4KTyp3.Content = "";
            wallLayer1VHCTyp3.Content = "";
            wallLayer2VHCTyp3.Content = "";
            wallLayer3VHCTyp3.Content = "";
            wallLayer4VHCTyp3.Content = "";
            if (ind == 0)
            {
                wallLayer1MaterialTyp3.Content = "";
                wallLayer1ThicknessTyp3.Content = "";
                wallLayer2MaterialTyp3.Content = "";
                wallLayer2ThicknessTyp3.Content = "";
                wallLayer3MaterialTyp3.Content = "";
                wallLayer3ThicknessTyp3.Content = "";
                wallLayer4MaterialTyp3.Content = "";
                wallLayer4ThicknessTyp3.Content = "";
                wallLayer1MaterialTyp3.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wallLayer2MaterialTyp3.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wallLayer3MaterialTyp3.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wallLayer4MaterialTyp3.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wallboxiTyp3 = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            int i = 0;
            if (ind == 0) return;
            XmlNodeList wallx = defxml.GetElementsByTagName("OpaqueConstruction");
            XmlNode cwall = defxml.CreateElement("OpaqueConstruction");
            int j = 0;
            foreach (XmlNode nod in wallx)
            {
                if (nod.SelectSingleNode("Type").InnerText == "Wall")
                {
                    i++;
                }
                if (i == ind)
                {
                    cwall = nod;
                    break;
                }
                j++;
                if (j == wallx.Count && ind == i + 1)
                {
                    wallboxTyp3.SelectedIndex = wallboxiTyp3;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= wallx.Count)
                {
                    wallboxTyp3.SelectedIndex = 0;
                    return;
                }
            }
            wallboxiTyp3 = ind;
            XmlNodeList names = cwall.SelectNodes("Layers/OpaqueLayer");
            XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
            for (ind = 0; ind != names.Count; ind++)
            {
                if (ind == 0)
                {
                    wallLayer1MaterialTyp3.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    wallLayer1ThicknessTyp3.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer1MaterialTyp3.Content.ToString())
                        {
                            wallLayer1KTyp3.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            wallLayer1VHCTyp3.Content = ma.SelectSingleNode("VHC").InnerText;
                            wallAlbedoTyp3.Text = ma.SelectSingleNode("Albedo").InnerText;
                            wallEmissivityTyp3.Text = ma.SelectSingleNode("Emissivity").InnerText;
                        }
                    }
                }
                if (ind == 1)
                {
                    wallLayer2MaterialTyp3.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    wallLayer2ThicknessTyp3.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer2MaterialTyp3.Content.ToString())
                        {
                            wallLayer2KTyp3.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            wallLayer2VHCTyp3.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
                if (ind == 2)
                {
                    wallLayer3MaterialTyp3.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    wallLayer3ThicknessTyp3.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer3MaterialTyp3.Content.ToString())
                        {
                            wallLayer3KTyp3.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            wallLayer3VHCTyp3.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
                if (ind == 3)
                {
                    wallLayer4MaterialTyp3.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    wallLayer4ThicknessTyp3.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer4MaterialTyp3.Content.ToString())
                        {
                            wallLayer4KTyp3.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            wallLayer4VHCTyp3.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
            }
            for (ind = names.Count; ind <= 3; ind++)
            {
                if (ind == 0)
                {
                    wallLayer1MaterialTyp3.Content = "";
                    wallLayer1ThicknessTyp3.Content = "";
                    wallLayer1KTyp3.Content = "";
                    wallLayer1VHCTyp3.Content = "";
                }
                if (ind == 1)
                {
                    wallLayer2MaterialTyp3.Content = "";
                    wallLayer2ThicknessTyp3.Content = "";
                    wallLayer2KTyp3.Content = "";
                    wallLayer2VHCTyp3.Content = "";
                }
                if (ind == 2)
                {
                    wallLayer3MaterialTyp3.Content = "";
                    wallLayer3ThicknessTyp3.Content = "";
                    wallLayer3KTyp3.Content = "";
                    wallLayer3VHCTyp3.Content = "";
                }
                if (ind == 3)
                {
                    wallLayer4MaterialTyp3.Content = "";
                    wallLayer4ThicknessTyp3.Content = "";
                    wallLayer4KTyp3.Content = "";
                    wallLayer4VHCTyp3.Content = "";
                }
            }
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        private void massboxTyp3_change(object sender, RoutedEventArgs e)
        {
            int ind = massboxTyp3.SelectedIndex;
            int i = 0;
            if (ind == 0)
            {
                massLayer1MaterialTyp3.Content = "";
                massLayer1ThicknessTyp3.Content = "";
                massLayer1MaterialTyp3.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                massboxiTyp3 = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            XmlNodeList massx = defxml.GetElementsByTagName("OpaqueConstruction");
            XmlNode cmass = defxml.CreateElement("OpaqueConstruction");
            int j = 0;
            foreach (XmlNode nod in massx)
            {
                if (nod.SelectSingleNode("Type").InnerText == "Internal Mass")
                {
                    i++;
                }
                if (i == ind)
                {
                    cmass = nod;
                    break;
                }
                j++;
                if (j == massx.Count && ind == i + 1)
                {
                    massboxTyp3.SelectedIndex = massboxiTyp3;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= massx.Count)
                {
                    massboxTyp3.SelectedIndex = 0;
                    return;
                }
            }
            massboxiTyp3 = ind;
            XmlNodeList names = cmass.SelectNodes("Layers/OpaqueLayer");
            XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
            ind = 0;
            massLayer1MaterialTyp3.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
            massLayer1ThicknessTyp3.Content = names[ind].SelectSingleNode("Thickness").InnerText;
            foreach (XmlNode ma in mat)
            {
                if (ma.SelectSingleNode("Name").InnerText == massLayer1MaterialTyp3.Content.ToString())
                {
                    massLayer1KTyp3.Content = ma.SelectSingleNode("Conductivity").InnerText;
                    massLayer1VHCTyp3.Content = ma.SelectSingleNode("VHC").InnerText;
                    massAlbedoTyp3.Content = ma.SelectSingleNode("Albedo").InnerText;
                    massEmissivityTyp3.Content = ma.SelectSingleNode("Emissivity").InnerText;
                }
            }
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        //private void urbanRoadboxTyp3_change(object sender, RoutedEventArgs e)
        //{
        //    int ind = urbanRoadboxTyp3.SelectedIndex;
        //    int i = 0;
        //    if (ind == 0)
        //    {
        //        urbanRoadMaterialTyp3.Content = "asphalt";
        //        urbanRoadThicknessTyp3.Content = "1.25";
        //        urbanRoadVHCTyp3.Content = "1600000";
        //        urbanRoadKTyp3.Content = "1";
        //        urbanRoadEmissivityTyp3.Content = "0.95";
        //        urbanRoadAlbedoTyp3.Content = "0.165";
        //        urbanRoadboxiTyp3 = 0;
        //        string pathx = Directory.GetCurrentDirectory();
        //        string temp1x = this.xmlPath;
        //        string temp2x = this.xmlFileName;
        //        this.xmlPath = pathx;
        //        this.xmlFileName = "$temp.xml";
        //        buttonSave_Click(sender, e);
        //        var providerx = (XmlDataProvider)this.DataContext;
        //        providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
        //        providerx.Refresh();
        //        this.xmlPath = temp1x;
        //        this.xmlFileName = temp2x;
        //        return;
        //    }
        //    XmlNodeList uRoadx = defxml.GetElementsByTagName("OpaqueConstruction");
        //    XmlNode cuRoad = defxml.CreateElement("OpaqueConstruction");
        //    int j = 0;
        //    foreach (XmlNode nod in uRoadx)
        //    {
        //        if (nod.SelectSingleNode("Type").InnerText == "Urban Road")
        //        {
        //            i++;
        //        }
        //        if (i == ind)
        //        {
        //            cuRoad = nod;
        //            break;
        //        }
        //        j++;
        //        if (j == uRoadx.Count && ind == i + 1)
        //        {
        //            urbanRoadboxTyp3.SelectedIndex = urbanRoadboxiTyp3;
        //            Editor s = new Editor();
        //            s.Top.SelectedIndex = 0;
        //            s.constructionTab.SelectedIndex = 1;
        //            try
        //            {
        //                s.Show();
        //            }
        //            catch { };
        //            this.IsEnabled = false;
        //            s.Closed += new EventHandler(OnChange);
        //            return;
        //        }
        //        if (j >= uRoadx.Count)
        //        {
        //            urbanRoadboxTyp3.SelectedIndex = 0;
        //            return;
        //        }
        //    }
        //    urbanRoadboxiTyp3 = ind;
        //    XmlNodeList names = cuRoad.SelectNodes("Layers/OpaqueLayer");
        //    XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
        //    ind = 0;
        //    urbanRoadMaterialTyp3.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
        //    urbanRoadThicknessTyp3.Content = names[ind].SelectSingleNode("Thickness").InnerText;
        //    foreach (XmlNode ma in mat)
        //    {
        //        if (ma.SelectSingleNode("Name").InnerText == urbanRoadMaterial.Content)
        //        {
        //            urbanRoadKTyp3.Content = ma.SelectSingleNode("Conductivity").InnerText;
        //            urbanRoadVHCTyp3.Content = ma.SelectSingleNode("VHC").InnerText;
        //            urbanRoadAlbedoTyp3.Content = ma.SelectSingleNode("Albedo").InnerText;
        //            urbanRoadEmissivityTyp3.Content = ma.SelectSingleNode("Emissivity").InnerText;
        //        }
        //    }
        //    string path = Directory.GetCurrentDirectory();
        //    string temp1 = this.xmlPath;
        //    string temp2 = this.xmlFileName;
        //    this.xmlPath = path;
        //    this.xmlFileName = "$temp.xml";
        //    buttonSave_Click(sender, e);
        //    var provider = (XmlDataProvider)this.DataContext;
        //    provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
        //    provider.Refresh();
        //    this.xmlPath = temp1;
        //    this.xmlFileName = temp2;
        //}
        //private void ruralboxTyp3_change(object sender, RoutedEventArgs e)
        //{
        //    int ind = ruralboxTyp3.SelectedIndex;
        //    int i = 0;
        //    if (ind == 0)
        //    {
        //        ruralRoadMaterialTyp3.Content = "asphalt";
        //        ruralRoadThicknessTyp3.Content = "1.25";
        //        ruralRoadVHCTyp3.Content = "1600000";
        //        ruralRoadKTyp3.Content = "1";
        //        ruralRoadEmissivityTyp3.Content = "0.95";
        //        ruralRoadAlbedoTyp3.Content = "0.165";
        //        ruralboxiTyp3 = 0;
        //        string pathx = Directory.GetCurrentDirectory();
        //        string temp1x = this.xmlPath;
        //        string temp2x = this.xmlFileName;
        //        this.xmlPath = pathx;
        //        this.xmlFileName = "$temp.xml";
        //        buttonSave_Click(sender, e);
        //        var providerx = (XmlDataProvider)this.DataContext;
        //        providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
        //        providerx.Refresh();
        //        this.xmlPath = temp1x;
        //        this.xmlFileName = temp2x;
        //        return;
        //    }
        //    XmlNodeList rRoadx = defxml.GetElementsByTagName("OpaqueConstruction");
        //    XmlNode crRoad = defxml.CreateElement("OpaqueConstruction");
        //    int j = 0;
        //    foreach (XmlNode nod in rRoadx)
        //    {
        //        if (nod.SelectSingleNode("Type").InnerText == "Rural Road")
        //        {
        //            i++;
        //        }
        //        if (i == ind)
        //        {
        //            crRoad = nod;
        //            break;
        //        }
        //        j++;
        //        if (j == rRoadx.Count && ind == i + 1)
        //        {
        //            ruralboxTyp3.SelectedIndex = ruralboxiTyp3;
        //            Editor s = new Editor();
        //            s.Top.SelectedIndex = 0;
        //            s.constructionTab.SelectedIndex = 1;
        //            try
        //            {
        //                s.Show();
        //            }
        //            catch { };
        //            this.IsEnabled = false;
        //            s.Closed += new EventHandler(OnChange);
        //            return;
        //        }
        //        if (j >= rRoadx.Count)
        //        {
        //            ruralbox.SelectedIndex = 0;
        //            return;
        //        }
        //    }
        //    ruralboxiTyp3 = ind;
        //    XmlNodeList names = crRoad.SelectNodes("Layers/OpaqueLayer");
        //    XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
        //    ind = 0;
        //    ruralRoadMaterialTyp3.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
        //    ruralRoadThicknessTyp3.Content = names[ind].SelectSingleNode("Thickness").InnerText;
        //    foreach (XmlNode ma in mat)
        //    {
        //        if (ma.SelectSingleNode("Name").InnerText == ruralRoadMaterial.Content)
        //        {
        //            ruralRoadKTyp3.Content = ma.SelectSingleNode("Conductivity").InnerText;
        //            ruralRoadVHCTyp3.Content = ma.SelectSingleNode("VHC").InnerText;
        //            ruralRoadAlbedoTyp3.Content = ma.SelectSingleNode("Albedo").InnerText;
        //            ruralRoadEmissivityTyp3.Content = ma.SelectSingleNode("Emissivity").InnerText;
        //        }
        //    }
        //    string path = Directory.GetCurrentDirectory();
        //    string temp1 = this.xmlPath;
        //    string temp2 = this.xmlFileName;
        //    this.xmlPath = path;
        //    this.xmlFileName = "$temp.xml";
        //    buttonSave_Click(sender, e);
        //    var provider = (XmlDataProvider)this.DataContext;
        //    provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
        //    provider.Refresh();
        //    this.xmlPath = temp1;
        //    this.xmlFileName = temp2;
        //}
        private void glazingboxTyp3_change(object sender, RoutedEventArgs e)
        {
            int ind = glazingboxTyp3.SelectedIndex;
            if (ind == 0)
            {
                uValueTyp3.Content = "";
                wwrTyp3.Content = "";
                SHGCTyp3.Content = "";
                glazingboxiTyp3 = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            int i = 0;
            XmlNodeList rRoadx = defxml.GetElementsByTagName("GlazingConstruction");
            XmlNode crRoad = defxml.CreateElement("GlazingConstruction");
            int j = 0;
            foreach (XmlNode nod in rRoadx)
            {
                if (nod.SelectNodes("WWR").Count != 0)
                {
                    i++;
                }
                if (i == ind)
                {
                    crRoad = nod;
                    break;
                }
                j++;
                if (j == rRoadx.Count && ind == i + 1)
                {
                    glazingboxTyp3.SelectedIndex = glazingboxiTyp3;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 2;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= rRoadx.Count)
                {
                    glazingboxTyp3.SelectedIndex = 0;
                    return;
                }
            }
            glazingboxiTyp3 = ind;
            wwrTyp3.Content = crRoad.SelectSingleNode("WWR").InnerText;
            uValueTyp3.Content = crRoad.SelectSingleNode("UValue").InnerText;
            SHGCTyp3.Content = crRoad.SelectSingleNode("SHGC").InnerText;
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        private void heatboxTyp3_change(object sender, RoutedEventArgs e)
        {
            int ind = heatboxTyp3.SelectedIndex;
            if (ind == 0)
            {
                dayInternalHeatGainTyp3.Content = "";
                nightInternalHeatGainTyp3.Content = "";
                infiltrationTyp3.Content = "";
                ventilationTyp3.Content = "";
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            int i = 0;
            XmlNodeList heatx = defxml.GetElementsByTagName("HeatTemplate");
            XmlNode cheat = defxml.CreateElement("HeatTemplate");
            int j = 0;
            foreach (XmlNode nod in heatx)
            {
                i++;
                if (i == ind)
                {
                    cheat = nod;
                    break;
                }
                j++;
                if (j == heatx.Count && ind == i + 1)
                {
                    heatboxTyp3.SelectedIndex = heatboxiTyp3;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 1;
                    s.buildingTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= heatx.Count)
                {
                    heatboxTyp3.SelectedIndex = 0;
                    return;
                }
            }
            heatboxiTyp3 = ind;
            string Os = cheat.SelectSingleNode("OccupancySched").InnerText;
            string Ls = cheat.SelectSingleNode("LightsSched").InnerText;
            string Es = cheat.SelectSingleNode("EquipSched").InnerText;
            string Is = cheat.SelectSingleNode("InfiltrationSched").InnerText;
            string Vs = cheat.SelectSingleNode("VentilationSched").InnerText;
            double Od = 0;
            double On = 0;
            double Ld = 0;
            double Ln = 0;
            double Ed = 0;
            double En = 0;
            double I = 0;
            double V = 0;
            foreach (XmlNode no in defxml.GetElementsByTagName("WeekSchedule"))
            {
                if (no.SelectSingleNode("Name").InnerText == Os)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    if (k < 7 || k > 17) On += Convert.ToDouble(sd.InnerText) / 13.0;
                                    else Od += Convert.ToDouble(sd.InnerText) / 11.0;
                                    k++;
                                }
                            }
                        }
                    }
                    On = On / 7.0;
                    Od = Od / 7.0;
                }
                if (no.SelectSingleNode("Name").InnerText == Ls)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    if (k < 7 || k > 17) Ln += Convert.ToDouble(sd.InnerText) / 13.0;
                                    else Ld += Convert.ToDouble(sd.InnerText) / 11.0;
                                    k++;
                                }
                            }
                        }
                    }
                    Ln = Ln / 7.0;
                    Ld = Ld / 7.0;
                }
                if (no.SelectSingleNode("Name").InnerText == Es)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    if (k < 7 || k > 17) En += Convert.ToDouble(sd.InnerText) / 13.0;
                                    else Ed += Convert.ToDouble(sd.InnerText) / 11.0;
                                    k++;
                                }
                            }
                        }
                    }
                    En = En / 7.0;
                    Ed = Ed / 7.0;
                }
                if (no.SelectSingleNode("Name").InnerText == Is)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    I += Convert.ToDouble(sd.InnerText) / 24.0;
                                }
                            }
                        }
                    }
                    I = I / 7.0;
                }
                if (no.SelectSingleNode("Name").InnerText == Vs)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    V += Convert.ToDouble(sd.InnerText) / 24.0;
                                }
                            }
                        }
                    }
                    V = V / 7.0;
                }
            }
            Od = Od * Convert.ToDouble(cheat.SelectSingleNode("OccupancyMax").InnerText);
            On = On * Convert.ToDouble(cheat.SelectSingleNode("OccupancyMax").InnerText);
            Ld = Ld * Convert.ToDouble(cheat.SelectSingleNode("LightsMax").InnerText);
            Ln = Ln * Convert.ToDouble(cheat.SelectSingleNode("LightsMax").InnerText);
            Ed = Ed * Convert.ToDouble(cheat.SelectSingleNode("EquipMax").InnerText);
            En = En * Convert.ToDouble(cheat.SelectSingleNode("EquipMax").InnerText);
            I = I * Convert.ToDouble(cheat.SelectSingleNode("InfiltrationMax").InnerText);
            V = V * Convert.ToDouble(cheat.SelectSingleNode("VentilationMax").InnerText);
            dayInternalHeatGainTyp3.Content = Convert.ToString(Od + Ld + Ed);
            nightInternalHeatGainTyp3.Content = Convert.ToString(On + Ln + En);
            infiltrationTyp3.Content = Convert.ToString(I);
            ventilationTyp3.Content = Convert.ToString(V);
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }

        private void heatboxTyp4_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList heatx = defxml.GetElementsByTagName("HeatTemplate");
            List<string> heatlist = new List<string>();
            heatlist.Add("Blank");
            foreach (XmlNode no in heatx)
            {
                string temp = no.SelectSingleNode("Name").InnerText;
                heatlist.Add(temp);
            }
            heatlist.Add("New Template");
            heatboxTyp4.ItemsSource = heatlist;
            if (heatlist[heatboxiTyp4] == "New Template") heatboxiTyp4 = 0;
            if (heatboxiTyp4 != 0)
            {
                heatboxTyp4.SelectedIndex = heatboxiTyp4 - 1;
                heatboxTyp4.SelectedIndex = heatboxiTyp4 + 1;
            }
            else heatboxTyp4.SelectedIndex = 0;
        }
        private void wallboxTyp4_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList wallx = defxml.GetElementsByTagName("OpaqueConstruction");
            List<string> walllist = new List<string>();
            walllist.Add("Blank");
            foreach (XmlNode no in wallx)
            {
                if (no.SelectSingleNode("Type").InnerText == "Wall")
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    walllist.Add(temp);
                }
            }
            walllist.Add("New Template");
            wallboxTyp4.ItemsSource = walllist;
            if (walllist[wallboxiTyp4] == "New Template") wallboxiTyp4 = 0;
            if (wallboxiTyp4 != 0)
            {
                wallboxTyp4.SelectedIndex = wallboxiTyp4 - 1;
                wallboxTyp4.SelectedIndex = wallboxiTyp4 + 1;
            }
            else wallboxTyp4.SelectedIndex = 0;
        }
        private void massboxTyp4_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList massx = defxml.GetElementsByTagName("OpaqueConstruction");
            List<string> masslist = new List<string>();
            masslist.Add("Blank");
            foreach (XmlNode no in massx)
            {
                if (no.SelectSingleNode("Type").InnerText == "Internal Mass")
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    masslist.Add(temp);
                }
            }
            masslist.Add("New Template");
            massboxTyp4.ItemsSource = masslist;
            if (masslist[massboxiTyp4] == "New Template") massboxiTyp4 = 0;
            if (massboxiTyp4 != 0)
            {
                massboxTyp4.SelectedIndex = massboxiTyp4 - 1;
                massboxTyp4.SelectedIndex = massboxiTyp4 + 1;
            }
            else massboxTyp4.SelectedIndex = 0;
        }
        //private void urbanRoadboxTyp4_load(object sender, RoutedEventArgs e)
        //{
        //    XmlNodeList massx = defxml.GetElementsByTagName("OpaqueConstruction");
        //    List<string> masslist = new List<string>();
        //    masslist.Add("Default");
        //    foreach (XmlNode no in massx)
        //    {
        //        if (no.SelectSingleNode("Type").InnerText == "Urban Road")
        //        {
        //            string temp = no.SelectSingleNode("Name").InnerText;
        //            masslist.Add(temp);
        //        }
        //    }
        //    masslist.Add("New Template");
        //    urbanRoadboxTyp4.ItemsSource = masslist;
        //    if (masslist[urbanRoadboxiTyp4] == "New Template") urbanRoadboxiTyp4 = 0;
        //    if (urbanRoadboxiTyp4 != 0)
        //    {
        //        urbanRoadboxTyp4.SelectedIndex = urbanRoadboxiTyp4 - 1;
        //        urbanRoadboxTyp4.SelectedIndex = urbanRoadboxiTyp4 + 1;
        //    }
        //    else urbanRoadboxTyp4.SelectedIndex = 0;
        //}
        //private void ruralboxTyp4_load(object sender, RoutedEventArgs e)
        //{
        //    XmlNodeList massx = defxml.GetElementsByTagName("OpaqueConstruction");
        //    List<string> masslist = new List<string>();
        //    masslist.Add("Default");
        //    foreach (XmlNode no in massx)
        //    {
        //        if (no.SelectSingleNode("Type").InnerText == "Rural Road")
        //        {
        //            string temp = no.SelectSingleNode("Name").InnerText;
        //            masslist.Add(temp);
        //        }
        //    }
        //    masslist.Add("New Template");
        //    ruralboxTyp4.ItemsSource = masslist;
        //    if (masslist[ruralboxiTyp4] == "New Template") ruralboxiTyp4 = 0;
        //    if (ruralboxiTyp4 != 0)
        //    {
        //        ruralboxTyp4.SelectedIndex = ruralboxiTyp4 - 1;
        //        ruralboxTyp4.SelectedIndex = ruralboxiTyp4 + 1;
        //    }
        //    else ruralboxTyp4.SelectedIndex = 0;
        //}
        private void roofboxTyp4_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList roofx = defxml.GetElementsByTagName("OpaqueConstruction");
            List<string> rooflist = new List<string>();
            rooflist.Add("Blank");
            foreach (XmlNode no in roofx)
            {
                if (no.SelectSingleNode("Type").InnerText == "Roof")
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    rooflist.Add(temp);
                }
            }
            rooflist.Add("New Template");
            roofboxTyp4.ItemsSource = rooflist;
            if (rooflist[roofboxiTyp4] == "New Template") roofboxiTyp4 = 0;
            if (roofboxiTyp4 != 0)
            {
                roofboxTyp4.SelectedIndex = roofboxiTyp4 - 1;
                roofboxTyp4.SelectedIndex = roofboxiTyp4 + 1;
            }
            else roofboxTyp4.SelectedIndex = 0;
        }
        private void glazingboxTyp4_load(object sender, RoutedEventArgs e)
        {
            XmlNodeList glazingx = defxml.GetElementsByTagName("GlazingConstruction");
            List<string> glazinglist = new List<string>();
            glazinglist.Add("Blank");
            foreach (XmlNode no in glazingx)
            {
                if (no.SelectNodes("WWR").Count != 0)
                {
                    string temp = no.SelectSingleNode("Name").InnerText;
                    glazinglist.Add(temp);
                }
            }
            glazinglist.Add("New Template");
            glazingboxTyp4.ItemsSource = glazinglist;
            if (glazinglist[glazingboxiTyp4] == "New Template") glazingboxiTyp4 = 0;
            if (glazingboxiTyp4 != 0)
            {
                glazingboxTyp4.SelectedIndex = glazingboxiTyp4 - 1;
                glazingboxTyp4.SelectedIndex = glazingboxiTyp4 + 1;
            }
            else glazingboxTyp4.SelectedIndex = 0;
        }
        private void roofboxTyp4_change(object sender, RoutedEventArgs e)
        {
            int ind = roofboxTyp4.SelectedIndex;
            roofLayer1KTyp4.Content = "";
            roofLayer2KTyp4.Content = "";
            roofLayer3KTyp4.Content = "";
            roofLayer1VHCTyp4.Content = "";
            roofLayer2VHCTyp4.Content = "";
            roofLayer3VHCTyp4.Content = "";
            if (ind == 0)
            {
                roofLayer1MaterialTyp4.Content = "";
                roofLayer1ThicknessTyp4.Content = "";
                roofLayer2MaterialTyp4.Content = "";
                roofLayer2ThicknessTyp4.Content = "";
                roofLayer3MaterialTyp4.Content = "";
                roofLayer3ThicknessTyp4.Content = "";
                roofLayer1MaterialTyp4.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                roofLayer1MaterialTyp4.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                roofLayer1MaterialTyp4.Margin = new Thickness(4, 4, 4, 4);
                roofLayer1MaterialTyp4.Padding = new Thickness(0, 0, 0, 0);
                roofLayer2MaterialTyp4.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                roofLayer2MaterialTyp4.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                roofLayer2MaterialTyp4.Margin = new Thickness(4, 4, 4, 4);
                roofLayer2MaterialTyp4.Padding = new Thickness(0, 0, 0, 0);
                roofLayer3MaterialTyp4.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                roofLayer3MaterialTyp4.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                roofLayer3MaterialTyp4.Margin = new Thickness(4, 4, 4, 4);
                roofLayer3MaterialTyp4.Padding = new Thickness(0, 0, 0, 0);
                roofboxiTyp4 = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            int i = 0;
            if (ind == 0) return;
            XmlNodeList roofx = defxml.GetElementsByTagName("OpaqueConstruction");
            XmlNode croof = defxml.CreateElement("OpaqueConstruction");
            int j = 0;
            foreach (XmlNode nod in roofx)
            {
                if (nod.SelectSingleNode("Type").InnerText == "Roof")
                {
                    i++;
                }
                if (i == ind)
                {
                    croof = nod;
                    break;
                }
                j++;
                if (j == roofx.Count && ind == i + 1)
                {
                    roofboxTyp4.SelectedIndex = roofboxiTyp4;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= roofx.Count)
                {
                    roofboxTyp4.SelectedIndex = 0;
                    return;
                }
            }
            roofboxiTyp4 = ind;
            XmlNodeList names = croof.SelectNodes("Layers/OpaqueLayer");
            XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
            for (ind = 0; ind != names.Count; ind++)
            {
                if (ind == 0)
                {
                    roofLayer1MaterialTyp4.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    roofLayer1ThicknessTyp4.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == roofLayer1MaterialTyp4.Content.ToString())
                        {
                            roofLayer1KTyp4.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            roofLayer1VHCTyp4.Content = ma.SelectSingleNode("VHC").InnerText;
                            roofAlbedoTyp4.Text = ma.SelectSingleNode("Albedo").InnerText;
                            roofEmissivityTyp4.Text = ma.SelectSingleNode("Emissivity").InnerText;
                        }
                    }
                }
                if (ind == 1)
                {
                    roofLayer2MaterialTyp4.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    roofLayer2ThicknessTyp4.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == roofLayer2MaterialTyp4.Content.ToString())
                        {
                            roofLayer2KTyp4.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            roofLayer2VHCTyp4.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
                if (ind == 2)
                {
                    roofLayer3MaterialTyp4.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    roofLayer3ThicknessTyp4.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == roofLayer3MaterialTyp4.Content.ToString())
                        {
                            roofLayer3KTyp4.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            roofLayer3VHCTyp4.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
            }
            for (ind = names.Count; ind <= 2; ind++)
            {
                if (ind == 0)
                {
                    roofLayer1MaterialTyp4.Content = "";
                    roofLayer1ThicknessTyp4.Content = "";
                    roofLayer1KTyp4.Content = "";
                    roofLayer1VHCTyp4.Content = "";
                }
                if (ind == 1)
                {
                    roofLayer2MaterialTyp4.Content = "";
                    roofLayer2ThicknessTyp4.Content = "";
                    roofLayer2KTyp4.Content = "";
                    roofLayer2VHCTyp4.Content = "";
                }
                if (ind == 2)
                {
                    roofLayer3MaterialTyp4.Content = "";
                    roofLayer3ThicknessTyp4.Content = "";
                    roofLayer3KTyp4.Content = "";
                    roofLayer3VHCTyp4.Content = "";
                }
            }
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        private void wallboxTyp4_change(object sender, RoutedEventArgs e)
        {
            int ind = wallboxTyp4.SelectedIndex;
            wallLayer1KTyp4.Content = "";
            wallLayer2KTyp4.Content = "";
            wallLayer3KTyp4.Content = "";
            wallLayer4KTyp4.Content = "";
            wallLayer1VHCTyp4.Content = "";
            wallLayer2VHCTyp4.Content = "";
            wallLayer3VHCTyp4.Content = "";
            wallLayer4VHCTyp4.Content = "";
            if (ind == 0)
            {
                wallLayer1MaterialTyp4.Content = "";
                wallLayer1ThicknessTyp4.Content = "";
                wallLayer2MaterialTyp4.Content = "";
                wallLayer2ThicknessTyp4.Content = "";
                wallLayer3MaterialTyp4.Content = "";
                wallLayer3ThicknessTyp4.Content = "";
                wallLayer4MaterialTyp4.Content = "";
                wallLayer4ThicknessTyp4.Content = "";
                wallLayer1MaterialTyp4.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wallLayer2MaterialTyp4.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wallLayer3MaterialTyp4.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wallLayer4MaterialTyp4.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wallboxiTyp4 = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            int i = 0;
            if (ind == 0) return;
            XmlNodeList wallx = defxml.GetElementsByTagName("OpaqueConstruction");
            XmlNode cwall = defxml.CreateElement("OpaqueConstruction");
            int j = 0;
            foreach (XmlNode nod in wallx)
            {
                if (nod.SelectSingleNode("Type").InnerText == "Wall")
                {
                    i++;
                }
                if (i == ind)
                {
                    cwall = nod;
                    break;
                }
                j++;
                if (j == wallx.Count && ind == i + 1)
                {
                    wallboxTyp4.SelectedIndex = wallboxiTyp4;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= wallx.Count)
                {
                    wallboxTyp4.SelectedIndex = 0;
                    return;
                }
            }
            wallboxiTyp4 = ind;
            XmlNodeList names = cwall.SelectNodes("Layers/OpaqueLayer");
            XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
            for (ind = 0; ind != names.Count; ind++)
            {
                if (ind == 0)
                {
                    wallLayer1MaterialTyp4.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    wallLayer1ThicknessTyp4.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer1MaterialTyp4.Content.ToString())
                        {
                            wallLayer1KTyp4.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            wallLayer1VHCTyp4.Content = ma.SelectSingleNode("VHC").InnerText;
                            wallAlbedoTyp4.Text = ma.SelectSingleNode("Albedo").InnerText;
                            wallEmissivityTyp4.Text = ma.SelectSingleNode("Emissivity").InnerText;
                        }
                    }
                }
                if (ind == 1)
                {
                    wallLayer2MaterialTyp4.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    wallLayer2ThicknessTyp4.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer2MaterialTyp4.Content.ToString())
                        {
                            wallLayer2KTyp4.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            wallLayer2VHCTyp4.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
                if (ind == 2)
                {
                    wallLayer3MaterialTyp4.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    wallLayer3ThicknessTyp4.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer3MaterialTyp4.Content.ToString())
                        {
                            wallLayer3KTyp4.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            wallLayer3VHCTyp4.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
                if (ind == 3)
                {
                    wallLayer4MaterialTyp4.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
                    wallLayer4ThicknessTyp4.Content = names[ind].SelectSingleNode("Thickness").InnerText;
                    foreach (XmlNode ma in mat)
                    {
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer4MaterialTyp4.Content.ToString())
                        {
                            wallLayer4KTyp4.Content = ma.SelectSingleNode("Conductivity").InnerText;
                            wallLayer4VHCTyp4.Content = ma.SelectSingleNode("VHC").InnerText;
                        }
                    }
                }
            }
            for (ind = names.Count; ind <= 3; ind++)
            {
                if (ind == 0)
                {
                    wallLayer1MaterialTyp4.Content = "";
                    wallLayer1ThicknessTyp4.Content = "";
                    wallLayer1KTyp4.Content = "";
                    wallLayer1VHCTyp4.Content = "";
                }
                if (ind == 1)
                {
                    wallLayer2MaterialTyp4.Content = "";
                    wallLayer2ThicknessTyp4.Content = "";
                    wallLayer2KTyp4.Content = "";
                    wallLayer2VHCTyp4.Content = "";
                }
                if (ind == 2)
                {
                    wallLayer3MaterialTyp4.Content = "";
                    wallLayer3ThicknessTyp4.Content = "";
                    wallLayer3KTyp4.Content = "";
                    wallLayer3VHCTyp4.Content = "";
                }
                if (ind == 3)
                {
                    wallLayer4MaterialTyp4.Content = "";
                    wallLayer4ThicknessTyp4.Content = "";
                    wallLayer4KTyp4.Content = "";
                    wallLayer4VHCTyp4.Content = "";
                }
            }
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        private void massboxTyp4_change(object sender, RoutedEventArgs e)
        {
            int ind = massboxTyp4.SelectedIndex;
            int i = 0;
            if (ind == 0)
            {
                massLayer1MaterialTyp4.Content = "";
                massLayer1ThicknessTyp4.Content = "";
                massLayer1MaterialTyp4.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                massboxiTyp4 = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            XmlNodeList massx = defxml.GetElementsByTagName("OpaqueConstruction");
            XmlNode cmass = defxml.CreateElement("OpaqueConstruction");
            int j = 0;
            foreach (XmlNode nod in massx)
            {
                if (nod.SelectSingleNode("Type").InnerText == "Internal Mass")
                {
                    i++;
                }
                if (i == ind)
                {
                    cmass = nod;
                    break;
                }
                j++;
                if (j == massx.Count && ind == i + 1)
                {
                    massboxTyp4.SelectedIndex = massboxiTyp4;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= massx.Count)
                {
                    massboxTyp4.SelectedIndex = 0;
                    return;
                }
            }
            massboxiTyp4 = ind;
            XmlNodeList names = cmass.SelectNodes("Layers/OpaqueLayer");
            XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
            ind = 0;
            massLayer1MaterialTyp4.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
            massLayer1ThicknessTyp4.Content = names[ind].SelectSingleNode("Thickness").InnerText;
            foreach (XmlNode ma in mat)
            {
                if (ma.SelectSingleNode("Name").InnerText == massLayer1MaterialTyp4.Content.ToString())
                {
                    massLayer1KTyp4.Content = ma.SelectSingleNode("Conductivity").InnerText;
                    massLayer1VHCTyp4.Content = ma.SelectSingleNode("VHC").InnerText;
                    massAlbedoTyp4.Content = ma.SelectSingleNode("Albedo").InnerText;
                    massEmissivityTyp4.Content = ma.SelectSingleNode("Emissivity").InnerText;
                }
            }
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        //private void urbanRoadboxTyp4_change(object sender, RoutedEventArgs e)
        //{
        //    int ind = urbanRoadboxTyp4.SelectedIndex;
        //    int i = 0;
        //    if (ind == 0)
        //    {
        //        urbanRoadMaterialTyp4.Content = "asphalt";
        //        urbanRoadThicknessTyp4.Content = "1.25";
        //        urbanRoadVHCTyp4.Content = "1600000";
        //        urbanRoadKTyp4.Content = "1";
        //        urbanRoadEmissivityTyp4.Content = "0.95";
        //        urbanRoadAlbedoTyp4.Content = "0.165";
        //        urbanRoadboxiTyp4 = 0;
        //        string pathx = Directory.GetCurrentDirectory();
        //        string temp1x = this.xmlPath;
        //        string temp2x = this.xmlFileName;
        //        this.xmlPath = pathx;
        //        this.xmlFileName = "$temp.xml";
        //        buttonSave_Click(sender, e);
        //        var providerx = (XmlDataProvider)this.DataContext;
        //        providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
        //        providerx.Refresh();
        //        this.xmlPath = temp1x;
        //        this.xmlFileName = temp2x;
        //        return;
        //    }
        //    XmlNodeList uRoadx = defxml.GetElementsByTagName("OpaqueConstruction");
        //    XmlNode cuRoad = defxml.CreateElement("OpaqueConstruction");
        //    int j = 0;
        //    foreach (XmlNode nod in uRoadx)
        //    {
        //        if (nod.SelectSingleNode("Type").InnerText == "Urban Road")
        //        {
        //            i++;
        //        }
        //        if (i == ind)
        //        {
        //            cuRoad = nod;
        //            break;
        //        }
        //        j++;
        //        if (j == uRoadx.Count && ind == i + 1)
        //        {
        //            urbanRoadboxTyp4.SelectedIndex = urbanRoadboxiTyp4;
        //            Editor s = new Editor();
        //            s.Top.SelectedIndex = 0;
        //            s.constructionTab.SelectedIndex = 1;
        //            try
        //            {
        //                s.Show();
        //            }
        //            catch { };
        //            this.IsEnabled = false;
        //            s.Closed += new EventHandler(OnChange);
        //            return;
        //        }
        //        if (j >= uRoadx.Count)
        //        {
        //            urbanRoadboxTyp4.SelectedIndex = 0;
        //            return;
        //        }
        //    }
        //    urbanRoadboxiTyp4 = ind;
        //    XmlNodeList names = cuRoad.SelectNodes("Layers/OpaqueLayer");
        //    XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
        //    ind = 0;
        //    urbanRoadMaterialTyp4.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
        //    urbanRoadThicknessTyp4.Content = names[ind].SelectSingleNode("Thickness").InnerText;
        //    foreach (XmlNode ma in mat)
        //    {
        //        if (ma.SelectSingleNode("Name").InnerText == urbanRoadMaterial.Content)
        //        {
        //            urbanRoadKTyp4.Content = ma.SelectSingleNode("Conductivity").InnerText;
        //            urbanRoadVHCTyp4.Content = ma.SelectSingleNode("VHC").InnerText;
        //            urbanRoadAlbedoTyp4.Content = ma.SelectSingleNode("Albedo").InnerText;
        //            urbanRoadEmissivityTyp4.Content = ma.SelectSingleNode("Emissivity").InnerText;
        //        }
        //    }
        //    string path = Directory.GetCurrentDirectory();
        //    string temp1 = this.xmlPath;
        //    string temp2 = this.xmlFileName;
        //    this.xmlPath = path;
        //    this.xmlFileName = "$temp.xml";
        //    buttonSave_Click(sender, e);
        //    var provider = (XmlDataProvider)this.DataContext;
        //    provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
        //    provider.Refresh();
        //    this.xmlPath = temp1;
        //    this.xmlFileName = temp2;
        //}
        //private void ruralboxTyp4_change(object sender, RoutedEventArgs e)
        //{
        //    int ind = ruralboxTyp4.SelectedIndex;
        //    int i = 0;
        //    if (ind == 0)
        //    {
        //        ruralRoadMaterialTyp4.Content = "asphalt";
        //        ruralRoadThicknessTyp4.Content = "1.25";
        //        ruralRoadVHCTyp4.Content = "1600000";
        //        ruralRoadKTyp4.Content = "1";
        //        ruralRoadEmissivityTyp4.Content = "0.95";
        //        ruralRoadAlbedoTyp4.Content = "0.165";
        //        ruralboxiTyp4 = 0;
        //        string pathx = Directory.GetCurrentDirectory();
        //        string temp1x = this.xmlPath;
        //        string temp2x = this.xmlFileName;
        //        this.xmlPath = pathx;
        //        this.xmlFileName = "$temp.xml";
        //        buttonSave_Click(sender, e);
        //        var providerx = (XmlDataProvider)this.DataContext;
        //        providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
        //        providerx.Refresh();
        //        this.xmlPath = temp1x;
        //        this.xmlFileName = temp2x;
        //        return;
        //    }
        //    XmlNodeList rRoadx = defxml.GetElementsByTagName("OpaqueConstruction");
        //    XmlNode crRoad = defxml.CreateElement("OpaqueConstruction");
        //    int j = 0;
        //    foreach (XmlNode nod in rRoadx)
        //    {
        //        if (nod.SelectSingleNode("Type").InnerText == "Rural Road")
        //        {
        //            i++;
        //        }
        //        if (i == ind)
        //        {
        //            crRoad = nod;
        //            break;
        //        }
        //        j++;
        //        if (j == rRoadx.Count && ind == i + 1)
        //        {
        //            ruralboxTyp4.SelectedIndex = ruralboxiTyp4;
        //            Editor s = new Editor();
        //            s.Top.SelectedIndex = 0;
        //            s.constructionTab.SelectedIndex = 1;
        //            try
        //            {
        //                s.Show();
        //            }
        //            catch { };
        //            this.IsEnabled = false;
        //            s.Closed += new EventHandler(OnChange);
        //            return;
        //        }
        //        if (j >= rRoadx.Count)
        //        {
        //            ruralbox.SelectedIndex = 0;
        //            return;
        //        }
        //    }
        //    ruralboxiTyp4 = ind;
        //    XmlNodeList names = crRoad.SelectNodes("Layers/OpaqueLayer");
        //    XmlNodeList mat = defxml.GetElementsByTagName("OpaqueMaterial");
        //    ind = 0;
        //    ruralRoadMaterialTyp4.Content = names[ind].SelectSingleNode("MaterialName").InnerText;
        //    ruralRoadThicknessTyp4.Content = names[ind].SelectSingleNode("Thickness").InnerText;
        //    foreach (XmlNode ma in mat)
        //    {
        //        if (ma.SelectSingleNode("Name").InnerText == ruralRoadMaterial.Content)
        //        {
        //            ruralRoadKTyp4.Content = ma.SelectSingleNode("Conductivity").InnerText;
        //            ruralRoadVHCTyp4.Content = ma.SelectSingleNode("VHC").InnerText;
        //            ruralRoadAlbedoTyp4.Content = ma.SelectSingleNode("Albedo").InnerText;
        //            ruralRoadEmissivityTyp4.Content = ma.SelectSingleNode("Emissivity").InnerText;
        //        }
        //    }
        //    string path = Directory.GetCurrentDirectory();
        //    string temp1 = this.xmlPath;
        //    string temp2 = this.xmlFileName;
        //    this.xmlPath = path;
        //    this.xmlFileName = "$temp.xml";
        //    buttonSave_Click(sender, e);
        //    var provider = (XmlDataProvider)this.DataContext;
        //    provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
        //    provider.Refresh();
        //    this.xmlPath = temp1;
        //    this.xmlFileName = temp2;
        //}
        private void glazingboxTyp4_change(object sender, RoutedEventArgs e)
        {
            int ind = glazingboxTyp4.SelectedIndex;
            if (ind == 0)
            {
                uValueTyp4.Content = "";
                wwrTyp4.Content = "";
                SHGCTyp4.Content = "";
                glazingboxiTyp4 = 0;
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            int i = 0;
            XmlNodeList rRoadx = defxml.GetElementsByTagName("GlazingConstruction");
            XmlNode crRoad = defxml.CreateElement("GlazingConstruction");
            int j = 0;
            foreach (XmlNode nod in rRoadx)
            {
                if (nod.SelectNodes("WWR").Count != 0)
                {
                    i++;
                }
                if (i == ind)
                {
                    crRoad = nod;
                    break;
                }
                j++;
                if (j == rRoadx.Count && ind == i + 1)
                {
                    glazingboxTyp4.SelectedIndex = glazingboxiTyp4;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 0;
                    s.constructionTab.SelectedIndex = 2;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= rRoadx.Count)
                {
                    glazingboxTyp4.SelectedIndex = 0;
                    return;
                }
            }
            glazingboxiTyp4 = ind;
            wwrTyp4.Content = crRoad.SelectSingleNode("WWR").InnerText;
            uValueTyp4.Content = crRoad.SelectSingleNode("UValue").InnerText;
            SHGCTyp4.Content = crRoad.SelectSingleNode("SHGC").InnerText;
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }
        private void heatboxTyp4_change(object sender, RoutedEventArgs e)
        {
            int ind = heatboxTyp4.SelectedIndex;
            if (ind == 0)
            {
                dayInternalHeatGainTyp4.Content = "";
                nightInternalHeatGainTyp4.Content = "";
                infiltrationTyp4.Content = "";
                ventilationTyp4.Content = "";
                string pathx = Directory.GetCurrentDirectory();
                string temp1x = this.xmlPath;
                string temp2x = this.xmlFileName;
                this.xmlPath = pathx;
                this.xmlFileName = "$temp.xml";
                buttonSave_Click(sender, e);
                var providerx = (XmlDataProvider)this.DataContext;
                providerx.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
                providerx.Refresh();
                this.xmlPath = temp1x;
                this.xmlFileName = temp2x;
                return;
            }
            int i = 0;
            XmlNodeList heatx = defxml.GetElementsByTagName("HeatTemplate");
            XmlNode cheat = defxml.CreateElement("HeatTemplate");
            int j = 0;
            foreach (XmlNode nod in heatx)
            {
                i++;
                if (i == ind)
                {
                    cheat = nod;
                    break;
                }
                j++;
                if (j == heatx.Count && ind == i + 1)
                {
                    heatboxTyp4.SelectedIndex = heatboxiTyp4;
                    Editor s = new Editor();
                    s.Top.SelectedIndex = 1;
                    s.buildingTab.SelectedIndex = 1;
                    try
                    {
                        s.Show();
                    }
                    catch { };
                    this.IsEnabled = false;
                    s.Closed += new EventHandler(OnChange);
                    return;
                }
                if (j >= heatx.Count)
                {
                    heatboxTyp4.SelectedIndex = 0;
                    return;
                }
            }
            heatboxiTyp4 = ind;
            string Os = cheat.SelectSingleNode("OccupancySched").InnerText;
            string Ls = cheat.SelectSingleNode("LightsSched").InnerText;
            string Es = cheat.SelectSingleNode("EquipSched").InnerText;
            string Is = cheat.SelectSingleNode("InfiltrationSched").InnerText;
            string Vs = cheat.SelectSingleNode("VentilationSched").InnerText;
            double Od = 0;
            double On = 0;
            double Ld = 0;
            double Ln = 0;
            double Ed = 0;
            double En = 0;
            double I = 0;
            double V = 0;
            foreach (XmlNode no in defxml.GetElementsByTagName("WeekSchedule"))
            {
                if (no.SelectSingleNode("Name").InnerText == Os)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    if (k < 7 || k > 17) On += Convert.ToDouble(sd.InnerText) / 13.0;
                                    else Od += Convert.ToDouble(sd.InnerText) / 11.0;
                                    k++;
                                }
                            }
                        }
                    }
                    On = On / 7.0;
                    Od = Od / 7.0;
                }
                if (no.SelectSingleNode("Name").InnerText == Ls)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    if (k < 7 || k > 17) Ln += Convert.ToDouble(sd.InnerText) / 13.0;
                                    else Ld += Convert.ToDouble(sd.InnerText) / 11.0;
                                    k++;
                                }
                            }
                        }
                    }
                    Ln = Ln / 7.0;
                    Ld = Ld / 7.0;
                }
                if (no.SelectSingleNode("Name").InnerText == Es)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    if (k < 7 || k > 17) En += Convert.ToDouble(sd.InnerText) / 13.0;
                                    else Ed += Convert.ToDouble(sd.InnerText) / 11.0;
                                    k++;
                                }
                            }
                        }
                    }
                    En = En / 7.0;
                    Ed = Ed / 7.0;
                }
                if (no.SelectSingleNode("Name").InnerText == Is)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    I += Convert.ToDouble(sd.InnerText) / 24.0;
                                }
                            }
                        }
                    }
                    I = I / 7.0;
                }
                if (no.SelectSingleNode("Name").InnerText == Vs)
                {
                    foreach (XmlNode nd in no.SelectNodes("Days/string"))
                    {
                        foreach (XmlNode dd in defxml.GetElementsByTagName("DaySchedule"))
                        {
                            if (nd.InnerText == dd.SelectSingleNode("Name").InnerText)
                            {
                                int k = 0;
                                foreach (XmlNode sd in dd.SelectNodes("Values/double"))
                                {
                                    V += Convert.ToDouble(sd.InnerText) / 24.0;
                                }
                            }
                        }
                    }
                    V = V / 7.0;
                }
            }
            Od = Od * Convert.ToDouble(cheat.SelectSingleNode("OccupancyMax").InnerText);
            On = On * Convert.ToDouble(cheat.SelectSingleNode("OccupancyMax").InnerText);
            Ld = Ld * Convert.ToDouble(cheat.SelectSingleNode("LightsMax").InnerText);
            Ln = Ln * Convert.ToDouble(cheat.SelectSingleNode("LightsMax").InnerText);
            Ed = Ed * Convert.ToDouble(cheat.SelectSingleNode("EquipMax").InnerText);
            En = En * Convert.ToDouble(cheat.SelectSingleNode("EquipMax").InnerText);
            I = I * Convert.ToDouble(cheat.SelectSingleNode("InfiltrationMax").InnerText);
            V = V * Convert.ToDouble(cheat.SelectSingleNode("VentilationMax").InnerText);
            dayInternalHeatGainTyp4.Content = Convert.ToString(Od + Ld + Ed);
            nightInternalHeatGainTyp4.Content = Convert.ToString(On + Ln + En);
            infiltrationTyp4.Content = Convert.ToString(I);
            ventilationTyp4.Content = Convert.ToString(V);
            string path = Directory.GetCurrentDirectory();
            string temp1 = this.xmlPath;
            string temp2 = this.xmlFileName;
            this.xmlPath = path;
            this.xmlFileName = "$temp.xml";
            buttonSave_Click(sender, e);
            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri(this.xmlFilePath, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = temp1;
            this.xmlFileName = temp2;
        }



        private void utciSim1_change(object sender, RoutedEventArgs e)
        {
            int ind = utciChangeboxSim1.SelectedIndex;
            int sidewalkStressColumnNumber = 84;
            int canyonStressColumnNumber = 86;
            int descTextColNum = 87;
            if (ind == 0)
            {
                try
                {
                    List<KeyValuePair<string, int>> utciStressDaysSidewalk = new List<KeyValuePair<string, int>>();
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[0,descTextColNum], Int32.Parse(sim1Data[0, sidewalkStressColumnNumber].Substring(0, sim1Data[0, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[1,descTextColNum], Int32.Parse(sim1Data[1, sidewalkStressColumnNumber].Substring(0, sim1Data[1, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[2, descTextColNum], Int32.Parse(sim1Data[2, sidewalkStressColumnNumber].Substring(0, sim1Data[2, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[3, descTextColNum], Int32.Parse(sim1Data[3, sidewalkStressColumnNumber].Substring(0, sim1Data[3, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[4, descTextColNum], Int32.Parse(sim1Data[4, sidewalkStressColumnNumber].Substring(0, sim1Data[4, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[5, descTextColNum], Int32.Parse(sim1Data[5, sidewalkStressColumnNumber].Substring(0, sim1Data[5, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[6, descTextColNum], Int32.Parse(sim1Data[6, sidewalkStressColumnNumber].Substring(0, sim1Data[6, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[7, descTextColNum], Int32.Parse(sim1Data[7, sidewalkStressColumnNumber].Substring(0, sim1Data[7, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[8, descTextColNum], Int32.Parse(sim1Data[8, sidewalkStressColumnNumber].Substring(0, sim1Data[8, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[9, descTextColNum], Int32.Parse(sim1Data[9, sidewalkStressColumnNumber].Substring(0, sim1Data[9, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[10, descTextColNum], Int32.Parse(sim1Data[10, sidewalkStressColumnNumber].Substring(0, sim1Data[10, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[11, descTextColNum], Int32.Parse(sim1Data[11, sidewalkStressColumnNumber].Substring(0, sim1Data[11, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[12, descTextColNum], Int32.Parse(sim1Data[12, sidewalkStressColumnNumber].Substring(0, sim1Data[12, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[13, descTextColNum], Int32.Parse(sim1Data[13, sidewalkStressColumnNumber].Substring(0, sim1Data[13, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[14, descTextColNum], Int32.Parse(sim1Data[14, sidewalkStressColumnNumber].Substring(0, sim1Data[14, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[15, descTextColNum], Int32.Parse(sim1Data[15, sidewalkStressColumnNumber].Substring(0, sim1Data[15, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[16, descTextColNum], Int32.Parse(sim1Data[16, sidewalkStressColumnNumber].Substring(0, sim1Data[16, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim1Data[17, descTextColNum], Int32.Parse(sim1Data[17, sidewalkStressColumnNumber].Substring(0, sim1Data[17, sidewalkStressColumnNumber].Length - 1))));
                    utciChartSim1.DataContext = utciStressDaysSidewalk;
                    

                }
                catch
                {

                }

            }
            else
            {
                List<KeyValuePair<string, int>> utciStressDaysUrbanCanyon = new List<KeyValuePair<string, int>>();
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[0, descTextColNum], Int32.Parse(sim1Data[0, canyonStressColumnNumber].Substring(0, sim1Data[0, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[1, descTextColNum], Int32.Parse(sim1Data[1, canyonStressColumnNumber].Substring(0, sim1Data[1, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[2, descTextColNum], Int32.Parse(sim1Data[2, canyonStressColumnNumber].Substring(0, sim1Data[2, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[3, descTextColNum], Int32.Parse(sim1Data[3, canyonStressColumnNumber].Substring(0, sim1Data[3, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[4, descTextColNum], Int32.Parse(sim1Data[4, canyonStressColumnNumber].Substring(0, sim1Data[4, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[5, descTextColNum], Int32.Parse(sim1Data[5, canyonStressColumnNumber].Substring(0, sim1Data[5, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[6, descTextColNum], Int32.Parse(sim1Data[6, canyonStressColumnNumber].Substring(0, sim1Data[6, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[7, descTextColNum], Int32.Parse(sim1Data[7, canyonStressColumnNumber].Substring(0, sim1Data[7, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[8, descTextColNum], Int32.Parse(sim1Data[8, canyonStressColumnNumber].Substring(0, sim1Data[8, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[9, descTextColNum], Int32.Parse(sim1Data[9, canyonStressColumnNumber].Substring(0, sim1Data[9, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[10, descTextColNum], Int32.Parse(sim1Data[10, canyonStressColumnNumber].Substring(0, sim1Data[10, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[11, descTextColNum], Int32.Parse(sim1Data[11, canyonStressColumnNumber].Substring(0, sim1Data[11, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[12, descTextColNum], Int32.Parse(sim1Data[12, canyonStressColumnNumber].Substring(0, sim1Data[12, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[13, descTextColNum], Int32.Parse(sim1Data[13, canyonStressColumnNumber].Substring(0, sim1Data[13, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[14, descTextColNum], Int32.Parse(sim1Data[14, canyonStressColumnNumber].Substring(0, sim1Data[14, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[15, descTextColNum], Int32.Parse(sim1Data[15, canyonStressColumnNumber].Substring(0, sim1Data[15, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[16, descTextColNum], Int32.Parse(sim1Data[16, canyonStressColumnNumber].Substring(0, sim1Data[16, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim1Data[17, descTextColNum], Int32.Parse(sim1Data[17, canyonStressColumnNumber].Substring(0, sim1Data[17, canyonStressColumnNumber].Length - 1))));
                    
                try
                {
                    utciChartSim1.DataContext = utciStressDaysUrbanCanyon; //keeps giving an error, but definitely exists in the XAML
                }
                catch
                {

                }


            }




        }
        private void utciSim2_change(object sender, RoutedEventArgs e)
        {
            int ind = utciChangeboxSim2.SelectedIndex;
            int sidewalkStressColumnNumber = 84;
            int canyonStressColumnNumber = 86;
            int descTextColNum = 87;
            if (ind == 0)
            {
                try
                {
                    List<KeyValuePair<string, int>> utciStressDaysSidewalk = new List<KeyValuePair<string, int>>();
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[0, descTextColNum], Int32.Parse(sim2Data[0, sidewalkStressColumnNumber].Substring(0, sim2Data[0, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[1, descTextColNum], Int32.Parse(sim2Data[1, sidewalkStressColumnNumber].Substring(0, sim2Data[1, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[2, descTextColNum], Int32.Parse(sim2Data[2, sidewalkStressColumnNumber].Substring(0, sim2Data[2, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[3, descTextColNum], Int32.Parse(sim2Data[3, sidewalkStressColumnNumber].Substring(0, sim2Data[3, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[4, descTextColNum], Int32.Parse(sim2Data[4, sidewalkStressColumnNumber].Substring(0, sim2Data[4, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[5, descTextColNum], Int32.Parse(sim2Data[5, sidewalkStressColumnNumber].Substring(0, sim2Data[5, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[6, descTextColNum], Int32.Parse(sim2Data[6, sidewalkStressColumnNumber].Substring(0, sim2Data[6, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[7, descTextColNum], Int32.Parse(sim2Data[7, sidewalkStressColumnNumber].Substring(0, sim2Data[7, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[8, descTextColNum], Int32.Parse(sim2Data[8, sidewalkStressColumnNumber].Substring(0, sim2Data[8, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[9, descTextColNum], Int32.Parse(sim2Data[9, sidewalkStressColumnNumber].Substring(0, sim2Data[9, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[10, descTextColNum], Int32.Parse(sim2Data[10, sidewalkStressColumnNumber].Substring(0, sim2Data[10, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[11, descTextColNum], Int32.Parse(sim2Data[11, sidewalkStressColumnNumber].Substring(0, sim2Data[11, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[12, descTextColNum], Int32.Parse(sim2Data[12, sidewalkStressColumnNumber].Substring(0, sim2Data[12, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[13, descTextColNum], Int32.Parse(sim2Data[13, sidewalkStressColumnNumber].Substring(0, sim2Data[13, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[14, descTextColNum], Int32.Parse(sim2Data[14, sidewalkStressColumnNumber].Substring(0, sim2Data[14, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[15, descTextColNum], Int32.Parse(sim2Data[15, sidewalkStressColumnNumber].Substring(0, sim2Data[15, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[16, descTextColNum], Int32.Parse(sim2Data[16, sidewalkStressColumnNumber].Substring(0, sim2Data[16, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim2Data[17, descTextColNum], Int32.Parse(sim2Data[17, sidewalkStressColumnNumber].Substring(0, sim2Data[17, sidewalkStressColumnNumber].Length - 1))));
                    utciChartSim2.DataContext = utciStressDaysSidewalk;


                }
                catch
                {

                }

            }
            else
            {
                List<KeyValuePair<string, int>> utciStressDaysUrbanCanyon = new List<KeyValuePair<string, int>>();
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[0, descTextColNum], Int32.Parse(sim2Data[0, canyonStressColumnNumber].Substring(0, sim2Data[0, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[1, descTextColNum], Int32.Parse(sim2Data[1, canyonStressColumnNumber].Substring(0, sim2Data[1, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[2, descTextColNum], Int32.Parse(sim2Data[2, canyonStressColumnNumber].Substring(0, sim2Data[2, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[3, descTextColNum], Int32.Parse(sim2Data[3, canyonStressColumnNumber].Substring(0, sim2Data[3, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[4, descTextColNum], Int32.Parse(sim2Data[4, canyonStressColumnNumber].Substring(0, sim2Data[4, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[5, descTextColNum], Int32.Parse(sim2Data[5, canyonStressColumnNumber].Substring(0, sim2Data[5, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[6, descTextColNum], Int32.Parse(sim2Data[6, canyonStressColumnNumber].Substring(0, sim2Data[6, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[7, descTextColNum], Int32.Parse(sim2Data[7, canyonStressColumnNumber].Substring(0, sim2Data[7, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[8, descTextColNum], Int32.Parse(sim2Data[8, canyonStressColumnNumber].Substring(0, sim2Data[8, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[9, descTextColNum], Int32.Parse(sim2Data[9, canyonStressColumnNumber].Substring(0, sim2Data[9, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[10, descTextColNum], Int32.Parse(sim2Data[10, canyonStressColumnNumber].Substring(0, sim2Data[10, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[11, descTextColNum], Int32.Parse(sim2Data[11, canyonStressColumnNumber].Substring(0, sim2Data[11, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[12, descTextColNum], Int32.Parse(sim2Data[12, canyonStressColumnNumber].Substring(0, sim2Data[12, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[13, descTextColNum], Int32.Parse(sim2Data[13, canyonStressColumnNumber].Substring(0, sim2Data[13, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[14, descTextColNum], Int32.Parse(sim2Data[14, canyonStressColumnNumber].Substring(0, sim2Data[14, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[15, descTextColNum], Int32.Parse(sim2Data[15, canyonStressColumnNumber].Substring(0, sim2Data[15, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[16, descTextColNum], Int32.Parse(sim2Data[16, canyonStressColumnNumber].Substring(0, sim2Data[16, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim2Data[17, descTextColNum], Int32.Parse(sim2Data[17, canyonStressColumnNumber].Substring(0, sim2Data[17, canyonStressColumnNumber].Length - 1))));

                try
                {
                    utciChartSim2.DataContext = utciStressDaysUrbanCanyon; //keeps giving an error, but definitely exists in the XAML
                }
                catch
                {

                }


            }




        }
        private void utciSim3_change(object sender, RoutedEventArgs e)
        {
            int ind = utciChangeboxSim3.SelectedIndex;
            int sidewalkStressColumnNumber = 84;
            int canyonStressColumnNumber = 86;
            int descTextColNum = 87;
            if (ind == 0)
            {
                try
                {
                    List<KeyValuePair<string, int>> utciStressDaysSidewalk = new List<KeyValuePair<string, int>>();
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[0, descTextColNum], Int32.Parse(sim3Data[0, sidewalkStressColumnNumber].Substring(0, sim3Data[0, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[1, descTextColNum], Int32.Parse(sim3Data[1, sidewalkStressColumnNumber].Substring(0, sim3Data[1, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[2, descTextColNum], Int32.Parse(sim3Data[2, sidewalkStressColumnNumber].Substring(0, sim3Data[2, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[3, descTextColNum], Int32.Parse(sim3Data[3, sidewalkStressColumnNumber].Substring(0, sim3Data[3, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[4, descTextColNum], Int32.Parse(sim3Data[4, sidewalkStressColumnNumber].Substring(0, sim3Data[4, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[5, descTextColNum], Int32.Parse(sim3Data[5, sidewalkStressColumnNumber].Substring(0, sim3Data[5, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[6, descTextColNum], Int32.Parse(sim3Data[6, sidewalkStressColumnNumber].Substring(0, sim3Data[6, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[7, descTextColNum], Int32.Parse(sim3Data[7, sidewalkStressColumnNumber].Substring(0, sim3Data[7, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[8, descTextColNum], Int32.Parse(sim3Data[8, sidewalkStressColumnNumber].Substring(0, sim3Data[8, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[9, descTextColNum], Int32.Parse(sim3Data[9, sidewalkStressColumnNumber].Substring(0, sim3Data[9, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[10, descTextColNum], Int32.Parse(sim3Data[10, sidewalkStressColumnNumber].Substring(0, sim3Data[10, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[11, descTextColNum], Int32.Parse(sim3Data[11, sidewalkStressColumnNumber].Substring(0, sim3Data[11, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[12, descTextColNum], Int32.Parse(sim3Data[12, sidewalkStressColumnNumber].Substring(0, sim3Data[12, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[13, descTextColNum], Int32.Parse(sim3Data[13, sidewalkStressColumnNumber].Substring(0, sim3Data[13, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[14, descTextColNum], Int32.Parse(sim3Data[14, sidewalkStressColumnNumber].Substring(0, sim3Data[14, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[15, descTextColNum], Int32.Parse(sim3Data[15, sidewalkStressColumnNumber].Substring(0, sim3Data[15, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[16, descTextColNum], Int32.Parse(sim3Data[16, sidewalkStressColumnNumber].Substring(0, sim3Data[16, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim3Data[17, descTextColNum], Int32.Parse(sim3Data[17, sidewalkStressColumnNumber].Substring(0, sim3Data[17, sidewalkStressColumnNumber].Length - 1))));
                    utciChartSim3.DataContext = utciStressDaysSidewalk;


                }
                catch
                {

                }

            }
            else
            {
                List<KeyValuePair<string, int>> utciStressDaysUrbanCanyon = new List<KeyValuePair<string, int>>();
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[0, descTextColNum], Int32.Parse(sim3Data[0, canyonStressColumnNumber].Substring(0, sim3Data[0, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[1, descTextColNum], Int32.Parse(sim3Data[1, canyonStressColumnNumber].Substring(0, sim3Data[1, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[2, descTextColNum], Int32.Parse(sim3Data[2, canyonStressColumnNumber].Substring(0, sim3Data[2, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[3, descTextColNum], Int32.Parse(sim3Data[3, canyonStressColumnNumber].Substring(0, sim3Data[3, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[4, descTextColNum], Int32.Parse(sim3Data[4, canyonStressColumnNumber].Substring(0, sim3Data[4, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[5, descTextColNum], Int32.Parse(sim3Data[5, canyonStressColumnNumber].Substring(0, sim3Data[5, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[6, descTextColNum], Int32.Parse(sim3Data[6, canyonStressColumnNumber].Substring(0, sim3Data[6, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[7, descTextColNum], Int32.Parse(sim3Data[7, canyonStressColumnNumber].Substring(0, sim3Data[7, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[8, descTextColNum], Int32.Parse(sim3Data[8, canyonStressColumnNumber].Substring(0, sim3Data[8, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[9, descTextColNum], Int32.Parse(sim3Data[9, canyonStressColumnNumber].Substring(0, sim3Data[9, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[10, descTextColNum], Int32.Parse(sim3Data[10, canyonStressColumnNumber].Substring(0, sim3Data[10, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[11, descTextColNum], Int32.Parse(sim3Data[11, canyonStressColumnNumber].Substring(0, sim3Data[11, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[12, descTextColNum], Int32.Parse(sim3Data[12, canyonStressColumnNumber].Substring(0, sim3Data[12, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[13, descTextColNum], Int32.Parse(sim3Data[13, canyonStressColumnNumber].Substring(0, sim3Data[13, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[14, descTextColNum], Int32.Parse(sim3Data[14, canyonStressColumnNumber].Substring(0, sim3Data[14, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[15, descTextColNum], Int32.Parse(sim3Data[15, canyonStressColumnNumber].Substring(0, sim3Data[15, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[16, descTextColNum], Int32.Parse(sim3Data[16, canyonStressColumnNumber].Substring(0, sim3Data[16, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim3Data[17, descTextColNum], Int32.Parse(sim3Data[17, canyonStressColumnNumber].Substring(0, sim3Data[17, canyonStressColumnNumber].Length - 1))));

                try
                {
                    utciChartSim3.DataContext = utciStressDaysUrbanCanyon; //keeps giving an error, but definitely exists in the XAML
                }
                catch
                {

                }


            }




        }
        private void utciSim4_change(object sender, RoutedEventArgs e)
        {
            int ind = utciChangeboxSim4.SelectedIndex;
            int sidewalkStressColumnNumber = 84;
            int canyonStressColumnNumber = 86;
            int descTextColNum = 87;
            if (ind == 0)
            {
                try
                {
                    List<KeyValuePair<string, int>> utciStressDaysSidewalk = new List<KeyValuePair<string, int>>();
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[0, descTextColNum], Int32.Parse(sim4Data[0, sidewalkStressColumnNumber].Substring(0, sim4Data[0, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[1, descTextColNum], Int32.Parse(sim4Data[1, sidewalkStressColumnNumber].Substring(0, sim4Data[1, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[2, descTextColNum], Int32.Parse(sim4Data[2, sidewalkStressColumnNumber].Substring(0, sim4Data[2, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[3, descTextColNum], Int32.Parse(sim4Data[3, sidewalkStressColumnNumber].Substring(0, sim4Data[3, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[4, descTextColNum], Int32.Parse(sim4Data[4, sidewalkStressColumnNumber].Substring(0, sim4Data[4, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[5, descTextColNum], Int32.Parse(sim4Data[5, sidewalkStressColumnNumber].Substring(0, sim4Data[5, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[6, descTextColNum], Int32.Parse(sim4Data[6, sidewalkStressColumnNumber].Substring(0, sim4Data[6, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[7, descTextColNum], Int32.Parse(sim4Data[7, sidewalkStressColumnNumber].Substring(0, sim4Data[7, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[8, descTextColNum], Int32.Parse(sim4Data[8, sidewalkStressColumnNumber].Substring(0, sim4Data[8, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[9, descTextColNum], Int32.Parse(sim4Data[9, sidewalkStressColumnNumber].Substring(0, sim4Data[9, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[10, descTextColNum], Int32.Parse(sim4Data[10, sidewalkStressColumnNumber].Substring(0, sim4Data[10, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[11, descTextColNum], Int32.Parse(sim4Data[11, sidewalkStressColumnNumber].Substring(0, sim4Data[11, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[12, descTextColNum], Int32.Parse(sim4Data[12, sidewalkStressColumnNumber].Substring(0, sim4Data[12, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[13, descTextColNum], Int32.Parse(sim4Data[13, sidewalkStressColumnNumber].Substring(0, sim4Data[13, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[14, descTextColNum], Int32.Parse(sim4Data[14, sidewalkStressColumnNumber].Substring(0, sim4Data[14, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[15, descTextColNum], Int32.Parse(sim4Data[15, sidewalkStressColumnNumber].Substring(0, sim4Data[15, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[16, descTextColNum], Int32.Parse(sim4Data[16, sidewalkStressColumnNumber].Substring(0, sim4Data[16, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim4Data[17, descTextColNum], Int32.Parse(sim4Data[17, sidewalkStressColumnNumber].Substring(0, sim4Data[17, sidewalkStressColumnNumber].Length - 1))));
                    utciChartSim4.DataContext = utciStressDaysSidewalk;


                }
                catch
                {

                }

            }
            else
            {
                List<KeyValuePair<string, int>> utciStressDaysUrbanCanyon = new List<KeyValuePair<string, int>>();
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[0, descTextColNum], Int32.Parse(sim4Data[0, canyonStressColumnNumber].Substring(0, sim4Data[0, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[1, descTextColNum], Int32.Parse(sim4Data[1, canyonStressColumnNumber].Substring(0, sim4Data[1, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[2, descTextColNum], Int32.Parse(sim4Data[2, canyonStressColumnNumber].Substring(0, sim4Data[2, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[3, descTextColNum], Int32.Parse(sim4Data[3, canyonStressColumnNumber].Substring(0, sim4Data[3, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[4, descTextColNum], Int32.Parse(sim4Data[4, canyonStressColumnNumber].Substring(0, sim4Data[4, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[5, descTextColNum], Int32.Parse(sim4Data[5, canyonStressColumnNumber].Substring(0, sim4Data[5, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[6, descTextColNum], Int32.Parse(sim4Data[6, canyonStressColumnNumber].Substring(0, sim4Data[6, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[7, descTextColNum], Int32.Parse(sim4Data[7, canyonStressColumnNumber].Substring(0, sim4Data[7, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[8, descTextColNum], Int32.Parse(sim4Data[8, canyonStressColumnNumber].Substring(0, sim4Data[8, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[9, descTextColNum], Int32.Parse(sim4Data[9, canyonStressColumnNumber].Substring(0, sim4Data[9, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[10, descTextColNum], Int32.Parse(sim4Data[10, canyonStressColumnNumber].Substring(0, sim4Data[10, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[11, descTextColNum], Int32.Parse(sim4Data[11, canyonStressColumnNumber].Substring(0, sim4Data[11, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[12, descTextColNum], Int32.Parse(sim4Data[12, canyonStressColumnNumber].Substring(0, sim4Data[12, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[13, descTextColNum], Int32.Parse(sim4Data[13, canyonStressColumnNumber].Substring(0, sim4Data[13, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[14, descTextColNum], Int32.Parse(sim4Data[14, canyonStressColumnNumber].Substring(0, sim4Data[14, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[15, descTextColNum], Int32.Parse(sim4Data[15, canyonStressColumnNumber].Substring(0, sim4Data[15, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[16, descTextColNum], Int32.Parse(sim4Data[16, canyonStressColumnNumber].Substring(0, sim4Data[16, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim4Data[17, descTextColNum], Int32.Parse(sim4Data[17, canyonStressColumnNumber].Substring(0, sim4Data[17, canyonStressColumnNumber].Length - 1))));

                try
                {
                    utciChartSim4.DataContext = utciStressDaysUrbanCanyon; //keeps giving an error, but definitely exists in the XAML
                }
                catch
                {

                }


            }




        }
        private void utciSim5_change(object sender, RoutedEventArgs e)
        {
            int ind = utciChangeboxSim5.SelectedIndex;
            int sidewalkStressColumnNumber = 84;
            int canyonStressColumnNumber = 86;
            int descTextColNum = 87;
            if (ind == 0)
            {
                try
                {
                    List<KeyValuePair<string, int>> utciStressDaysSidewalk = new List<KeyValuePair<string, int>>();
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[0, descTextColNum], Int32.Parse(sim5Data[0, sidewalkStressColumnNumber].Substring(0, sim5Data[0, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[1, descTextColNum], Int32.Parse(sim5Data[1, sidewalkStressColumnNumber].Substring(0, sim5Data[1, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[2, descTextColNum], Int32.Parse(sim5Data[2, sidewalkStressColumnNumber].Substring(0, sim5Data[2, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[3, descTextColNum], Int32.Parse(sim5Data[3, sidewalkStressColumnNumber].Substring(0, sim5Data[3, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[4, descTextColNum], Int32.Parse(sim5Data[4, sidewalkStressColumnNumber].Substring(0, sim5Data[4, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[5, descTextColNum], Int32.Parse(sim5Data[5, sidewalkStressColumnNumber].Substring(0, sim5Data[5, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[6, descTextColNum], Int32.Parse(sim5Data[6, sidewalkStressColumnNumber].Substring(0, sim5Data[6, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[7, descTextColNum], Int32.Parse(sim5Data[7, sidewalkStressColumnNumber].Substring(0, sim5Data[7, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[8, descTextColNum], Int32.Parse(sim5Data[8, sidewalkStressColumnNumber].Substring(0, sim5Data[8, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[9, descTextColNum], Int32.Parse(sim5Data[9, sidewalkStressColumnNumber].Substring(0, sim5Data[9, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[10, descTextColNum], Int32.Parse(sim5Data[10, sidewalkStressColumnNumber].Substring(0, sim5Data[10, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[11, descTextColNum], Int32.Parse(sim5Data[11, sidewalkStressColumnNumber].Substring(0, sim5Data[11, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[12, descTextColNum], Int32.Parse(sim5Data[12, sidewalkStressColumnNumber].Substring(0, sim5Data[12, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[13, descTextColNum], Int32.Parse(sim5Data[13, sidewalkStressColumnNumber].Substring(0, sim5Data[13, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[14, descTextColNum], Int32.Parse(sim5Data[14, sidewalkStressColumnNumber].Substring(0, sim5Data[14, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[15, descTextColNum], Int32.Parse(sim5Data[15, sidewalkStressColumnNumber].Substring(0, sim5Data[15, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[16, descTextColNum], Int32.Parse(sim5Data[16, sidewalkStressColumnNumber].Substring(0, sim5Data[16, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim5Data[17, descTextColNum], Int32.Parse(sim5Data[17, sidewalkStressColumnNumber].Substring(0, sim5Data[17, sidewalkStressColumnNumber].Length - 1))));
                    utciChartSim5.DataContext = utciStressDaysSidewalk;


                }
                catch
                {

                }

            }
            else
            {
                List<KeyValuePair<string, int>> utciStressDaysUrbanCanyon = new List<KeyValuePair<string, int>>();
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[0, descTextColNum], Int32.Parse(sim5Data[0, canyonStressColumnNumber].Substring(0, sim5Data[0, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[1, descTextColNum], Int32.Parse(sim5Data[1, canyonStressColumnNumber].Substring(0, sim5Data[1, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[2, descTextColNum], Int32.Parse(sim5Data[2, canyonStressColumnNumber].Substring(0, sim5Data[2, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[3, descTextColNum], Int32.Parse(sim5Data[3, canyonStressColumnNumber].Substring(0, sim5Data[3, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[4, descTextColNum], Int32.Parse(sim5Data[4, canyonStressColumnNumber].Substring(0, sim5Data[4, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[5, descTextColNum], Int32.Parse(sim5Data[5, canyonStressColumnNumber].Substring(0, sim5Data[5, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[6, descTextColNum], Int32.Parse(sim5Data[6, canyonStressColumnNumber].Substring(0, sim5Data[6, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[7, descTextColNum], Int32.Parse(sim5Data[7, canyonStressColumnNumber].Substring(0, sim5Data[7, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[8, descTextColNum], Int32.Parse(sim5Data[8, canyonStressColumnNumber].Substring(0, sim5Data[8, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[9, descTextColNum], Int32.Parse(sim5Data[9, canyonStressColumnNumber].Substring(0, sim5Data[9, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[10, descTextColNum], Int32.Parse(sim5Data[10, canyonStressColumnNumber].Substring(0, sim5Data[10, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[11, descTextColNum], Int32.Parse(sim5Data[11, canyonStressColumnNumber].Substring(0, sim5Data[11, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[12, descTextColNum], Int32.Parse(sim5Data[12, canyonStressColumnNumber].Substring(0, sim5Data[12, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[13, descTextColNum], Int32.Parse(sim5Data[13, canyonStressColumnNumber].Substring(0, sim5Data[13, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[14, descTextColNum], Int32.Parse(sim5Data[14, canyonStressColumnNumber].Substring(0, sim5Data[14, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[15, descTextColNum], Int32.Parse(sim5Data[15, canyonStressColumnNumber].Substring(0, sim5Data[15, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[16, descTextColNum], Int32.Parse(sim5Data[16, canyonStressColumnNumber].Substring(0, sim5Data[16, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim5Data[17, descTextColNum], Int32.Parse(sim5Data[17, canyonStressColumnNumber].Substring(0, sim5Data[17, canyonStressColumnNumber].Length - 1))));

                try
                {
                    utciChartSim5.DataContext = utciStressDaysUrbanCanyon; //keeps giving an error, but definitely exists in the XAML
                }
                catch
                {

                }


            }




        }
        private void utciSim6_change(object sender, RoutedEventArgs e)
        {
            int ind = utciChangeboxSim6.SelectedIndex;
            int sidewalkStressColumnNumber = 84;
            int canyonStressColumnNumber = 86;
            int descTextColNum = 87;
            if (ind == 0)
            {
                try
                {
                    List<KeyValuePair<string, int>> utciStressDaysSidewalk = new List<KeyValuePair<string, int>>();
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[0, descTextColNum], Int32.Parse(sim6Data[0, sidewalkStressColumnNumber].Substring(0, sim6Data[0, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[1, descTextColNum], Int32.Parse(sim6Data[1, sidewalkStressColumnNumber].Substring(0, sim6Data[1, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[2, descTextColNum], Int32.Parse(sim6Data[2, sidewalkStressColumnNumber].Substring(0, sim6Data[2, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[3, descTextColNum], Int32.Parse(sim6Data[3, sidewalkStressColumnNumber].Substring(0, sim6Data[3, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[4, descTextColNum], Int32.Parse(sim6Data[4, sidewalkStressColumnNumber].Substring(0, sim6Data[4, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[5, descTextColNum], Int32.Parse(sim6Data[5, sidewalkStressColumnNumber].Substring(0, sim6Data[5, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[6, descTextColNum], Int32.Parse(sim6Data[6, sidewalkStressColumnNumber].Substring(0, sim6Data[6, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[7, descTextColNum], Int32.Parse(sim6Data[7, sidewalkStressColumnNumber].Substring(0, sim6Data[7, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[8, descTextColNum], Int32.Parse(sim6Data[8, sidewalkStressColumnNumber].Substring(0, sim6Data[8, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[9, descTextColNum], Int32.Parse(sim6Data[9, sidewalkStressColumnNumber].Substring(0, sim6Data[9, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[10, descTextColNum], Int32.Parse(sim6Data[10, sidewalkStressColumnNumber].Substring(0, sim6Data[10, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[11, descTextColNum], Int32.Parse(sim6Data[11, sidewalkStressColumnNumber].Substring(0, sim6Data[11, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[12, descTextColNum], Int32.Parse(sim6Data[12, sidewalkStressColumnNumber].Substring(0, sim6Data[12, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[13, descTextColNum], Int32.Parse(sim6Data[13, sidewalkStressColumnNumber].Substring(0, sim6Data[13, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[14, descTextColNum], Int32.Parse(sim6Data[14, sidewalkStressColumnNumber].Substring(0, sim6Data[14, sidewalkStressColumnNumber].Length - 1))));

                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[15, descTextColNum], Int32.Parse(sim6Data[15, sidewalkStressColumnNumber].Substring(0, sim6Data[15, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[16, descTextColNum], Int32.Parse(sim6Data[16, sidewalkStressColumnNumber].Substring(0, sim6Data[16, sidewalkStressColumnNumber].Length - 1))));
                    utciStressDaysSidewalk.Add(new KeyValuePair<string, int>(sim6Data[17, descTextColNum], Int32.Parse(sim6Data[17, sidewalkStressColumnNumber].Substring(0, sim6Data[17, sidewalkStressColumnNumber].Length - 1))));
                    utciChartSim6.DataContext = utciStressDaysSidewalk;


                }
                catch
                {

                }

            }
            else
            {
                List<KeyValuePair<string, int>> utciStressDaysUrbanCanyon = new List<KeyValuePair<string, int>>();
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[0, descTextColNum], Int32.Parse(sim6Data[0, canyonStressColumnNumber].Substring(0, sim6Data[0, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[1, descTextColNum], Int32.Parse(sim6Data[1, canyonStressColumnNumber].Substring(0, sim6Data[1, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[2, descTextColNum], Int32.Parse(sim6Data[2, canyonStressColumnNumber].Substring(0, sim6Data[2, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[3, descTextColNum], Int32.Parse(sim6Data[3, canyonStressColumnNumber].Substring(0, sim6Data[3, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[4, descTextColNum], Int32.Parse(sim6Data[4, canyonStressColumnNumber].Substring(0, sim6Data[4, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[5, descTextColNum], Int32.Parse(sim6Data[5, canyonStressColumnNumber].Substring(0, sim6Data[5, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[6, descTextColNum], Int32.Parse(sim6Data[6, canyonStressColumnNumber].Substring(0, sim6Data[6, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[7, descTextColNum], Int32.Parse(sim6Data[7, canyonStressColumnNumber].Substring(0, sim6Data[7, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[8, descTextColNum], Int32.Parse(sim6Data[8, canyonStressColumnNumber].Substring(0, sim6Data[8, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[9, descTextColNum], Int32.Parse(sim6Data[9, canyonStressColumnNumber].Substring(0, sim6Data[9, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[10, descTextColNum], Int32.Parse(sim6Data[10, canyonStressColumnNumber].Substring(0, sim6Data[10, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[11, descTextColNum], Int32.Parse(sim6Data[11, canyonStressColumnNumber].Substring(0, sim6Data[11, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[12, descTextColNum], Int32.Parse(sim6Data[12, canyonStressColumnNumber].Substring(0, sim6Data[12, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[13, descTextColNum], Int32.Parse(sim6Data[13, canyonStressColumnNumber].Substring(0, sim6Data[13, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[14, descTextColNum], Int32.Parse(sim6Data[14, canyonStressColumnNumber].Substring(0, sim6Data[14, canyonStressColumnNumber].Length - 1))));

                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[15, descTextColNum], Int32.Parse(sim6Data[15, canyonStressColumnNumber].Substring(0, sim6Data[15, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[16, descTextColNum], Int32.Parse(sim6Data[16, canyonStressColumnNumber].Substring(0, sim6Data[16, canyonStressColumnNumber].Length - 1))));
                utciStressDaysUrbanCanyon.Add(new KeyValuePair<string, int>(sim6Data[17, descTextColNum], Int32.Parse(sim6Data[17, canyonStressColumnNumber].Substring(0, sim6Data[17, canyonStressColumnNumber].Length - 1))));

                try
                {
                    utciChartSim6.DataContext = utciStressDaysUrbanCanyon; //keeps giving an error, but definitely exists in the XAML
                }
                catch
                {

                }


            }




        }

        private void tAirSim1_change(object sender, RoutedEventArgs e)
        {
            int ind = tAirChangeboxSim1.SelectedIndex;
            List<KeyValuePair<string, double>> averageTemperaturePerHour = new List<KeyValuePair<string, double>>();
            int column = 12+ind*6;
            try
            {
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("0h", Double.Parse(sim1Data[0, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("1h", Double.Parse(sim1Data[1, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("2h", Double.Parse(sim1Data[2, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("3h", Double.Parse(sim1Data[3, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("4h", Double.Parse(sim1Data[4, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("5h", Double.Parse(sim1Data[5, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("6h", Double.Parse(sim1Data[6, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("7h", Double.Parse(sim1Data[7, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("8h", Double.Parse(sim1Data[8, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("9h", Double.Parse(sim1Data[9, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("10h", Double.Parse(sim1Data[10, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("11h", Double.Parse(sim1Data[11, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("12h", Double.Parse(sim1Data[12, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("13h", Double.Parse(sim1Data[13, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("14h", Double.Parse(sim1Data[14, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("15h", Double.Parse(sim1Data[15, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("16h", Double.Parse(sim1Data[16, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("17h", Double.Parse(sim1Data[17, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("18h", Double.Parse(sim1Data[18, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("19h", Double.Parse(sim1Data[19, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("20h", Double.Parse(sim1Data[20, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("21h", Double.Parse(sim1Data[21, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("22h", Double.Parse(sim1Data[22, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("23h", Double.Parse(sim1Data[23, column])));

                tAirChartSim1.DataContext = averageTemperaturePerHour;  //keeps giving an error, but definitely exists in the XAML
                
            }
            catch
            {

            }

        }
        private void tAirSim2_change(object sender, RoutedEventArgs e)
        {
            int ind = tAirChangeboxSim2.SelectedIndex;
            List<KeyValuePair<string, double>> averageTemperaturePerHour = new List<KeyValuePair<string, double>>();
            int column = 12 + ind * 6;
            try
            {
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("0h", Double.Parse(sim2Data[0, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("1h", Double.Parse(sim2Data[1, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("2h", Double.Parse(sim2Data[2, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("3h", Double.Parse(sim2Data[3, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("4h", Double.Parse(sim2Data[4, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("5h", Double.Parse(sim2Data[5, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("6h", Double.Parse(sim2Data[6, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("7h", Double.Parse(sim2Data[7, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("8h", Double.Parse(sim2Data[8, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("9h", Double.Parse(sim2Data[9, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("10h", Double.Parse(sim2Data[10, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("11h", Double.Parse(sim2Data[11, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("12h", Double.Parse(sim2Data[12, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("13h", Double.Parse(sim2Data[13, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("14h", Double.Parse(sim2Data[14, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("15h", Double.Parse(sim2Data[15, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("16h", Double.Parse(sim2Data[16, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("17h", Double.Parse(sim2Data[17, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("18h", Double.Parse(sim2Data[18, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("19h", Double.Parse(sim2Data[19, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("20h", Double.Parse(sim2Data[20, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("21h", Double.Parse(sim2Data[21, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("22h", Double.Parse(sim2Data[22, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("23h", Double.Parse(sim2Data[23, column])));

                tAirChartSim2.DataContext = averageTemperaturePerHour;  //keeps giving an error, but definitely exists in the XAML

            }
            catch
            {

            }

        }
        private void tAirSim3_change(object sender, RoutedEventArgs e)
        {
            int ind = tAirChangeboxSim3.SelectedIndex;
            List<KeyValuePair<string, double>> averageTemperaturePerHour = new List<KeyValuePair<string, double>>();
            int column = 12 + ind * 6;
            try
            {
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("0h", Double.Parse(sim3Data[0, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("1h", Double.Parse(sim3Data[1, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("2h", Double.Parse(sim3Data[2, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("3h", Double.Parse(sim3Data[3, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("4h", Double.Parse(sim3Data[4, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("5h", Double.Parse(sim3Data[5, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("6h", Double.Parse(sim3Data[6, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("7h", Double.Parse(sim3Data[7, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("8h", Double.Parse(sim3Data[8, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("9h", Double.Parse(sim3Data[9, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("10h", Double.Parse(sim3Data[10, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("11h", Double.Parse(sim3Data[11, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("12h", Double.Parse(sim3Data[12, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("13h", Double.Parse(sim3Data[13, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("14h", Double.Parse(sim3Data[14, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("15h", Double.Parse(sim3Data[15, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("16h", Double.Parse(sim3Data[16, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("17h", Double.Parse(sim3Data[17, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("18h", Double.Parse(sim3Data[18, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("19h", Double.Parse(sim3Data[19, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("20h", Double.Parse(sim3Data[20, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("21h", Double.Parse(sim3Data[21, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("22h", Double.Parse(sim3Data[22, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("23h", Double.Parse(sim3Data[23, column])));

                tAirChartSim3.DataContext = averageTemperaturePerHour;  //keeps giving an error, but definitely exists in the XAML

            }
            catch
            {

            }

        }
        private void tAirSim4_change(object sender, RoutedEventArgs e)
        {
            int ind = tAirChangeboxSim4.SelectedIndex;
            List<KeyValuePair<string, double>> averageTemperaturePerHour = new List<KeyValuePair<string, double>>();
            int column = 12 + ind * 6;
            try
            {
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("0h", Double.Parse(sim4Data[0, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("1h", Double.Parse(sim4Data[1, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("2h", Double.Parse(sim4Data[2, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("3h", Double.Parse(sim4Data[3, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("4h", Double.Parse(sim4Data[4, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("5h", Double.Parse(sim4Data[5, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("6h", Double.Parse(sim4Data[6, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("7h", Double.Parse(sim4Data[7, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("8h", Double.Parse(sim4Data[8, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("9h", Double.Parse(sim4Data[9, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("10h", Double.Parse(sim4Data[10, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("11h", Double.Parse(sim4Data[11, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("12h", Double.Parse(sim4Data[12, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("13h", Double.Parse(sim4Data[13, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("14h", Double.Parse(sim4Data[14, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("15h", Double.Parse(sim4Data[15, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("16h", Double.Parse(sim4Data[16, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("17h", Double.Parse(sim4Data[17, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("18h", Double.Parse(sim4Data[18, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("19h", Double.Parse(sim4Data[19, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("20h", Double.Parse(sim4Data[20, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("21h", Double.Parse(sim4Data[21, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("22h", Double.Parse(sim4Data[22, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("23h", Double.Parse(sim4Data[23, column])));

                tAirChartSim4.DataContext = averageTemperaturePerHour;  //keeps giving an error, but definitely exists in the XAML

            }
            catch
            {

            }

        }
        private void tAirSim5_change(object sender, RoutedEventArgs e)
        {
            int ind = tAirChangeboxSim5.SelectedIndex;
            List<KeyValuePair<string, double>> averageTemperaturePerHour = new List<KeyValuePair<string, double>>();
            int column = 12 + ind * 6;
            try
            {
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("0h", Double.Parse(sim5Data[0, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("1h", Double.Parse(sim5Data[1, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("2h", Double.Parse(sim5Data[2, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("3h", Double.Parse(sim5Data[3, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("4h", Double.Parse(sim5Data[4, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("5h", Double.Parse(sim5Data[5, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("6h", Double.Parse(sim5Data[6, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("7h", Double.Parse(sim5Data[7, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("8h", Double.Parse(sim5Data[8, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("9h", Double.Parse(sim5Data[9, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("10h", Double.Parse(sim5Data[10, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("11h", Double.Parse(sim5Data[11, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("12h", Double.Parse(sim5Data[12, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("13h", Double.Parse(sim5Data[13, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("14h", Double.Parse(sim5Data[14, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("15h", Double.Parse(sim5Data[15, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("16h", Double.Parse(sim5Data[16, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("17h", Double.Parse(sim5Data[17, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("18h", Double.Parse(sim5Data[18, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("19h", Double.Parse(sim5Data[19, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("20h", Double.Parse(sim5Data[20, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("21h", Double.Parse(sim5Data[21, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("22h", Double.Parse(sim5Data[22, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("23h", Double.Parse(sim5Data[23, column])));

                tAirChartSim5.DataContext = averageTemperaturePerHour;  //keeps giving an error, but definitely exists in the XAML

            }
            catch
            {

            }

        }
        private void tAirSim6_change(object sender, RoutedEventArgs e)
        {
            int ind = tAirChangeboxSim6.SelectedIndex;
            List<KeyValuePair<string, double>> averageTemperaturePerHour = new List<KeyValuePair<string, double>>();
            int column = 12 + ind * 6;
            try
            {
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("0h", Double.Parse(sim6Data[0, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("1h", Double.Parse(sim6Data[1, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("2h", Double.Parse(sim6Data[2, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("3h", Double.Parse(sim6Data[3, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("4h", Double.Parse(sim6Data[4, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("5h", Double.Parse(sim6Data[5, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("6h", Double.Parse(sim6Data[6, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("7h", Double.Parse(sim6Data[7, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("8h", Double.Parse(sim6Data[8, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("9h", Double.Parse(sim6Data[9, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("10h", Double.Parse(sim6Data[10, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("11h", Double.Parse(sim6Data[11, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("12h", Double.Parse(sim6Data[12, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("13h", Double.Parse(sim6Data[13, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("14h", Double.Parse(sim6Data[14, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("15h", Double.Parse(sim6Data[15, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("16h", Double.Parse(sim6Data[16, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("17h", Double.Parse(sim6Data[17, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("18h", Double.Parse(sim6Data[18, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("19h", Double.Parse(sim6Data[19, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("20h", Double.Parse(sim6Data[20, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("21h", Double.Parse(sim6Data[21, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("22h", Double.Parse(sim6Data[22, column])));
                averageTemperaturePerHour.Add(new KeyValuePair<string, double>("23h", Double.Parse(sim6Data[23, column])));

                tAirChartSim6.DataContext = averageTemperaturePerHour;  //keeps giving an error, but definitely exists in the XAML

            }
            catch
            {

            }

        }

        private void run_Click(object sender, RoutedEventArgs e)
        {
            if (typologyCheck(sender, e) == false)
            {
                //typologyCheck will present error message if there is a problem
                //System.Windows.MessageBox.Show("Distributions don't add to 100%. Please adjust appropriately.");
                return;
            }
            urbanCanyonTab.Visibility = System.Windows.Visibility.Collapsed;
            typTab1.Visibility = System.Windows.Visibility.Collapsed;
            typTab2.Visibility = System.Windows.Visibility.Collapsed;
            typTab3.Visibility = System.Windows.Visibility.Collapsed;
            typTab4.Visibility = System.Windows.Visibility.Collapsed;
            refSiteTab.Visibility = System.Windows.Visibility.Collapsed;
            runSimTab.Visibility = System.Windows.Visibility.Visible;
            mainTabControl.SelectedItem = runSimTab;
            simTab1.Visibility = System.Windows.Visibility.Collapsed;
            simTab2.Visibility = System.Windows.Visibility.Collapsed;
            simTab3.Visibility = System.Windows.Visibility.Collapsed;
            simTab4.Visibility = System.Windows.Visibility.Collapsed;
            simTab5.Visibility = System.Windows.Visibility.Collapsed;
            simTab6.Visibility = System.Windows.Visibility.Collapsed;
            viewSimTab.Visibility = System.Windows.Visibility.Collapsed;
            
            makeFileMenuItem.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            runMenuItem.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 0, 255, 180));
            runMenuItem.BorderThickness = new Thickness(0, 0, 0, 20);
            simMenuItem.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

            //runMenuItem.Background = new SolidColorBrush(Colors.Black);
            //RunWindow run = new RunWindow();
            //run.ShowDialog();
            //run.Closed += new EventHandler(Enable);
        }

        private void sim_Click(object sender, RoutedEventArgs e)
        {
            if (pathSim1 != "")
            {
                loadDataSim1(sender, e);
            }
            if (pathSim2 != "")
            {
                loadDataSim2(sender, e);
            }
            if (pathSim3 != "")
            {
                loadDataSim3(sender, e);
            }
            if (pathSim4 != "")
            {
                loadDataSim4(sender, e);
            }
            if (pathSim5 != "")
            {
                loadDataSim5(sender, e);
            }
            if (pathSim6 != "")
            {
                loadDataSim6(sender, e);
            }

            //this.epwPathRun = System.IO.Path.GetDirectoryName(filenameRun);
            //Get only the file name
            //this.epwFileNameRun = System.IO.Path.GetFileName(filenameRun);
            urbanCanyonTab.Visibility = System.Windows.Visibility.Collapsed;
            typTab1.Visibility = System.Windows.Visibility.Collapsed;
            typTab2.Visibility = System.Windows.Visibility.Collapsed;
            typTab3.Visibility = System.Windows.Visibility.Collapsed;
            typTab4.Visibility = System.Windows.Visibility.Collapsed; 
            refSiteTab.Visibility = System.Windows.Visibility.Collapsed;
            runSimTab.Visibility = System.Windows.Visibility.Collapsed;
            viewSimTab.Visibility = System.Windows.Visibility.Visible;
            mainTabControl.SelectedItem = viewSimTab;

            if (isSim1Visible) 
            {
                simTab1.Visibility = System.Windows.Visibility.Visible;
            }
            if (isSim2Visible)
            {
                simTab2.Visibility = System.Windows.Visibility.Visible;
            }
            if (isSim3Visible)
            {
                simTab3.Visibility = System.Windows.Visibility.Visible;
            }
            if (isSim4Visible)
            {
                simTab4.Visibility = System.Windows.Visibility.Visible;
            }
            if (isSim5Visible)
            {
                simTab5.Visibility = System.Windows.Visibility.Visible;
            }
            if (isSim6Visible)
            {
                simTab6.Visibility = System.Windows.Visibility.Visible;
            }
            makeFileMenuItem.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            runMenuItem.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            simMenuItem.BorderBrush = new SolidColorBrush(Color.FromArgb(10, 0, 255, 180));
            simMenuItem.BorderThickness = new Thickness(0, 0, 0, 3);
            //sim1DegDays.Content = "100";



        }

        private void loadDataSim1(object sender, RoutedEventArgs e)
        {
            //grab info for first simulation
            //String sim1File = resultName.Text;
            //String origPath1 = System.IO.Path.Combine(resultPathText.Text, sim1File); //path to the simulation output
            //String csvPathSim1 = System.IO.Path.ChangeExtension(origPath1, ".csv");
            try
            {
                String csvPathInput = System.IO.Path.ChangeExtension(pathSim1, ".csv");
                String csvFilename = System.IO.Path.GetFileName(csvPathInput);
                if (!File.Exists(csvPathInput))
                {
                    File.Copy(pathSim1, csvPathInput);
                }
                UTCI_calc.UTCIWindow utci = new UTCI_calc.UTCIWindow();

                string working_folder = "";
                string csv_file = "";
                working_folder = System.IO.Path.GetDirectoryName(pathSim1);
                string csv_filenameUTCI = "UTCI_" + csvFilename;
                string csv_filePath = System.IO.Path.Combine(working_folder, csv_filenameUTCI);
                if (System.IO.Path.GetExtension(pathSim1) == ".csv")
                {
                    csv_filePath = csvPathInput;
                }

                if (!File.Exists(csv_filePath))
                {
                    if (!File.Exists(System.IO.Path.Combine(working_folder, "Trad_" + csvFilename)))
                    {
                        System.Windows.MessageBox.Show("Other files generated from UWG simulation not found. Please run UWG simulation first before visualizing results.");
                        return;
                    }
                    else
                    {
                        csv_file = utci.UTCI_calc(working_folder, csvFilename);
                        csv_filePath = System.IO.Path.Combine(working_folder, csv_file);
                    }
                }

                if (csv_filePath != "")
                {
                    //String utciOutputSim1 = utci_sim1();

                    //need to run UTCI on this csv at fullPath#
                    //then take output of UTCI and grab data
                    var reader = new StreamReader(File.OpenRead(csv_filePath));

                    int row = 0;
                    int column = 0;
                    while (row < 24)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        while (!reader.EndOfStream && column < 88)
                        {
                            if (column < values.Length)
                            {
                                if (column == 87)
                                {
                                    sim1Data[row, column] = values[column].Split(':')[0];
                                }
                                else
                                {
                                    sim1Data[row, column] = values[column];
                                }
                                column += 1;

                            }
                            else
                            {
                                break;
                            }
                        }
                        row += 1;
                        column = 0;

                    }
                    utciSim1_change(sender, e);
                    tAirSim1_change(sender, e);
                    sim1Hours.Content = (Int32.Parse(sim1Data[9, 86].Substring(0, sim1Data[9, 86].Length - 1)) + Int32.Parse(sim1Data[10, 86].Substring(0, sim1Data[10, 86].Length - 1))).ToString() + "%";
                    simTab1.Visibility = System.Windows.Visibility.Visible;
                    labelSim1.Content = System.IO.Path.GetFileName(pathSim1);
                    labelSim1.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    labelSim1.Margin = new Thickness(4, 4, 4, 4);
                    labelSim1.Padding = new Thickness(0, 0, 0, 0);

                    simTab1.Visibility = System.Windows.Visibility.Visible;
                    this.isSim1Visible = true;
                    
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Second exception caught.", ex);
            }
        }
        private void loadDataSim2(object sender, RoutedEventArgs e)
        {
            //grab info for first simulation
            //String sim2File = resultName.Text;
            //String origPath1 = System.IO.Path.Combine(resultPathText.Text, sim2File); //path to the simulation output
            //String csvPathSim2 = System.IO.Path.ChangeExtension(origPath1, ".csv");
            try
            {

                String csvPathInput = System.IO.Path.ChangeExtension(pathSim2, ".csv");
                String csvFilename = System.IO.Path.GetFileName(csvPathInput);
                if (!File.Exists(csvPathInput))
                {
                    File.Copy(pathSim2, csvPathInput);
                }
                UTCI_calc.UTCIWindow utci = new UTCI_calc.UTCIWindow();

                string working_folder = "";
                string csv_file = "";
                working_folder = System.IO.Path.GetDirectoryName(pathSim2);
                string csv_filenameUTCI = "UTCI_" + csvFilename;
                string csv_filePath = System.IO.Path.Combine(working_folder, csv_filenameUTCI);
                if (System.IO.Path.GetExtension(pathSim2) == ".csv")
                {
                    csv_filePath = csvPathInput;
                }
                if (!File.Exists(csv_filePath))
                {
                    if (!File.Exists(System.IO.Path.Combine(working_folder, "Trad_" + csvFilename)))
                    {
                        System.Windows.MessageBox.Show("Other files generated from UWG simulation not found. Please run UWG simulation first before visualizing results.");
                        return;
                    }
                    else
                    {
                        csv_file = utci.UTCI_calc(working_folder, csvFilename);
                        csv_filePath = System.IO.Path.Combine(working_folder, csv_file);
                    }
                }
                if (csv_filePath != "")
                {
                    //String utciOutputSim2 = utci_sim2();

                    //need to run UTCI on this csv at fullPath#
                    //then take output of UTCI and grab data
                    var reader = new StreamReader(File.OpenRead(csv_filePath));

                    int row = 0;
                    int column = 0;
                    while (row < 24)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        while (!reader.EndOfStream && column < 88)
                        {
                            if (column < values.Length)
                            {
                                if (column == 87)
                                {
                                    sim2Data[row, column] = values[column].Split(':')[0];
                                }
                                else
                                {
                                    sim2Data[row, column] = values[column];
                                }
                                column += 1;

                            }
                            else
                            {
                                break;
                            }
                        }
                        row += 1;
                        column = 0;

                    }
                    utciSim2_change(sender, e);
                    tAirSim2_change(sender, e);
                    sim2Hours.Content = (Int32.Parse(sim2Data[9, 86].Substring(0, sim2Data[9, 86].Length - 1)) + Int32.Parse(sim2Data[10, 86].Substring(0, sim2Data[10, 86].Length - 1))).ToString() + "%";
                    simTab2.Visibility = System.Windows.Visibility.Visible;
                    labelSim2.Content = System.IO.Path.GetFileName(pathSim2);
                    labelSim2.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    labelSim2.Margin = new Thickness(4, 4, 4, 4);
                    labelSim2.Padding = new Thickness(0, 0, 0, 0);

                    simTab2.Visibility = System.Windows.Visibility.Visible;
                    this.isSim2Visible = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Second exception caught.", ex);
            }
        }
        private void loadDataSim3(object sender, RoutedEventArgs e)
        {
            //grab info for first simulation
            //String sim3File = resultName.Text;
            //String origPath1 = System.IO.Path.Combine(resultPathText.Text, sim3File); //path to the simulation output
            //String csvPathSim3 = System.IO.Path.ChangeExtension(origPath1, ".csv");
            try
            {

                String csvPathInput = System.IO.Path.ChangeExtension(pathSim3, ".csv");
                String csvFilename = System.IO.Path.GetFileName(csvPathInput);
                if (!File.Exists(csvPathInput))
                {
                    File.Copy(pathSim3, csvPathInput);
                }
                UTCI_calc.UTCIWindow utci = new UTCI_calc.UTCIWindow();

                string working_folder = "";
                string csv_file = "";
                working_folder = System.IO.Path.GetDirectoryName(pathSim3);
                string csv_filenameUTCI = "UTCI_" + csvFilename;
                string csv_filePath = System.IO.Path.Combine(working_folder, csv_filenameUTCI);
                if (System.IO.Path.GetExtension(pathSim3) == ".csv")
                {
                    csv_filePath = csvPathInput;
                }
                if (!File.Exists(csv_filePath))
                {
                    if (!File.Exists(System.IO.Path.Combine(working_folder, "Trad_" + csvFilename)))
                    {
                        System.Windows.MessageBox.Show("Other files generated from UWG simulation not found. Please run UWG simulation first before visualizing results.");
                        return;
                    }
                    else
                    {
                        csv_file = utci.UTCI_calc(working_folder, csvFilename);
                        csv_filePath = System.IO.Path.Combine(working_folder, csv_file);
                    }
                }
                if (csv_filePath != "")
                {
                    //String utciOutputSim3 = utci_sim3();

                    //need to run UTCI on this csv at fullPath#
                    //then take output of UTCI and grab data
                    var reader = new StreamReader(File.OpenRead(csv_filePath));

                    int row = 0;
                    int column = 0;
                    while (row < 24)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        while (!reader.EndOfStream && column < 88)
                        {
                            if (column < values.Length)
                            {
                                if (column == 87)
                                {
                                    sim3Data[row, column] = values[column].Split(':')[0];
                                }
                                else
                                {
                                    sim3Data[row, column] = values[column];
                                }
                                column += 1;

                            }
                            else
                            {
                                break;
                            }
                        }
                        row += 1;
                        column = 0;

                    }
                    utciSim3_change(sender, e);
                    tAirSim3_change(sender, e);
                    sim3Hours.Content = (Int32.Parse(sim3Data[9, 86].Substring(0, sim3Data[9, 86].Length - 1)) + Int32.Parse(sim3Data[10, 86].Substring(0, sim3Data[10, 86].Length - 1))).ToString() + "%";
                    simTab3.Visibility = System.Windows.Visibility.Visible;
                    labelSim3.Content = System.IO.Path.GetFileName(pathSim3);
                    labelSim3.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    labelSim3.Margin = new Thickness(4, 4, 4, 4);
                    labelSim3.Padding = new Thickness(0, 0, 0, 0);

                    simTab3.Visibility = System.Windows.Visibility.Visible;
                    this.isSim3Visible = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Second exception caught.", ex);
            }
        }
        private void loadDataSim4(object sender, RoutedEventArgs e)
        {
            //grab info for first simulation
            //String sim4File = resultName.Text;
            //String origPath1 = System.IO.Path.Combine(resultPathText.Text, sim4File); //path to the simulation output
            //String csvPathSim4 = System.IO.Path.ChangeExtension(origPath1, ".csv");
            try
            {

                String csvPathInput = System.IO.Path.ChangeExtension(pathSim4, ".csv");
                String csvFilename = System.IO.Path.GetFileName(csvPathInput);
                if (!File.Exists(csvPathInput))
                {
                    File.Copy(pathSim4, csvPathInput);
                }
                UTCI_calc.UTCIWindow utci = new UTCI_calc.UTCIWindow();

                string working_folder = "";
                string csv_file = "";
                working_folder = System.IO.Path.GetDirectoryName(pathSim4);
                string csv_filenameUTCI = "UTCI_" + csvFilename;
                string csv_filePath = System.IO.Path.Combine(working_folder, csv_filenameUTCI);
                if (System.IO.Path.GetExtension(pathSim4) == ".csv")
                {
                    csv_filePath = csvPathInput;
                }
                if (!File.Exists(csv_filePath))
                {
                    if (!File.Exists(System.IO.Path.Combine(working_folder, "Trad_" + csvFilename)))
                    {
                        System.Windows.MessageBox.Show("Other files generated from UWG simulation not found. Please run UWG simulation first before visualizing results.");
                        return;
                    }
                    else
                    {
                        csv_file = utci.UTCI_calc(working_folder, csvFilename);
                        csv_filePath = System.IO.Path.Combine(working_folder, csv_file);
                    }
                }
                if (csv_filePath != "")
                {
                    //String utciOutputSim4 = utci_sim4();

                    //need to run UTCI on this csv at fullPath#
                    //then take output of UTCI and grab data
                    var reader = new StreamReader(File.OpenRead(csv_filePath));

                    int row = 0;
                    int column = 0;
                    while (row < 24)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        while (!reader.EndOfStream && column < 88)
                        {
                            if (column < values.Length)
                            {
                                if (column == 87)
                                {
                                    sim4Data[row, column] = values[column].Split(':')[0];
                                }
                                else
                                {
                                    sim4Data[row, column] = values[column];
                                }
                                column += 1;

                            }
                            else
                            {
                                break;
                            }
                        }
                        row += 1;
                        column = 0;

                    }
                    utciSim4_change(sender, e);
                    tAirSim4_change(sender, e);
                    sim4Hours.Content = (Int32.Parse(sim4Data[9, 86].Substring(0, sim4Data[9, 86].Length - 1)) + Int32.Parse(sim4Data[10, 86].Substring(0, sim4Data[10, 86].Length - 1))).ToString() + "%";
                    simTab4.Visibility = System.Windows.Visibility.Visible;
                    labelSim4.Content = System.IO.Path.GetFileName(pathSim4);
                    labelSim4.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    labelSim4.Margin = new Thickness(4, 4, 4, 4);
                    labelSim4.Padding = new Thickness(0, 0, 0, 0);

                    simTab4.Visibility = System.Windows.Visibility.Visible;
                    this.isSim4Visible = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Second exception caught.", ex);
            }
        }
        private void loadDataSim5(object sender, RoutedEventArgs e)
        {
            //grab info for first simulation
            //String sim5File = resultName.Text;
            //String origPath1 = System.IO.Path.Combine(resultPathText.Text, sim5File); //path to the simulation output
            //String csvPathSim5 = System.IO.Path.ChangeExtension(origPath1, ".csv");
            try
            {

                String csvPathInput = System.IO.Path.ChangeExtension(pathSim5, ".csv");
                String csvFilename = System.IO.Path.GetFileName(csvPathInput);
                if (!File.Exists(csvPathInput))
                {
                    File.Copy(pathSim5, csvPathInput);
                }
                UTCI_calc.UTCIWindow utci = new UTCI_calc.UTCIWindow();

                string working_folder = "";
                string csv_file = "";
                working_folder = System.IO.Path.GetDirectoryName(pathSim5);
                string csv_filenameUTCI = "UTCI_" + csvFilename;
                string csv_filePath = System.IO.Path.Combine(working_folder, csv_filenameUTCI);
                if (System.IO.Path.GetExtension(pathSim5) == ".csv")
                {
                    csv_filePath = csvPathInput;
                }
                if (!File.Exists(csv_filePath))
                {
                    if (!File.Exists(System.IO.Path.Combine(working_folder, "Trad_" + csvFilename)))
                    {
                        System.Windows.MessageBox.Show("Other files generated from UWG simulation not found. Please run UWG simulation first before visualizing results.");
                        return;
                    }
                    else
                    {
                        csv_file = utci.UTCI_calc(working_folder, csvFilename);
                        csv_filePath = System.IO.Path.Combine(working_folder, csv_file);
                    }
                }
                if (csv_filePath != "")
                {
                    //String utciOutputSim5 = utci_sim5();

                    //need to run UTCI on this csv at fullPath#
                    //then take output of UTCI and grab data
                    var reader = new StreamReader(File.OpenRead(csv_filePath));

                    int row = 0;
                    int column = 0;
                    while (row < 24)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        while (!reader.EndOfStream && column < 88)
                        {
                            if (column < values.Length)
                            {
                                if (column == 87)
                                {
                                    sim5Data[row, column] = values[column].Split(':')[0];
                                }
                                else
                                {
                                    sim5Data[row, column] = values[column];
                                }
                                column += 1;

                            }
                            else
                            {
                                break;
                            }
                        }
                        row += 1;
                        column = 0;

                    }
                    utciSim5_change(sender, e);
                    tAirSim5_change(sender, e);
                    sim5Hours.Content = (Int32.Parse(sim5Data[9, 86].Substring(0, sim5Data[9, 86].Length - 1)) + Int32.Parse(sim5Data[10, 86].Substring(0, sim5Data[10, 86].Length - 1))).ToString() + "%";
                    simTab5.Visibility = System.Windows.Visibility.Visible;
                    labelSim5.Content = System.IO.Path.GetFileName(pathSim5);
                    labelSim5.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    labelSim5.Margin = new Thickness(4, 4, 4, 4);
                    labelSim5.Padding = new Thickness(0, 0, 0, 0);

                    simTab5.Visibility = System.Windows.Visibility.Visible;
                    this.isSim5Visible = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Second exception caught.", ex);
            }
        }
        private void loadDataSim6(object sender, RoutedEventArgs e)
        {
            //grab info for first simulation
            //String sim6File = resultName.Text;
            //String origPath1 = System.IO.Path.Combine(resultPathText.Text, sim6File); //path to the simulation output
            //String csvPathSim6 = System.IO.Path.ChangeExtension(origPath1, ".csv");
            try
            {

                String csvPathInput = System.IO.Path.ChangeExtension(pathSim6, ".csv");
                String csvFilename = System.IO.Path.GetFileName(csvPathInput);
                if (!File.Exists(csvPathInput))
                {
                    File.Copy(pathSim6, csvPathInput);
                }
                UTCI_calc.UTCIWindow utci = new UTCI_calc.UTCIWindow();

                string working_folder = "";
                string csv_file = "";
                working_folder = System.IO.Path.GetDirectoryName(pathSim6);
                string csv_filenameUTCI = "UTCI_" + csvFilename;
                string csv_filePath = System.IO.Path.Combine(working_folder, csv_filenameUTCI);
                if (System.IO.Path.GetExtension(pathSim6) == ".csv")
                {
                    csv_filePath = csvPathInput;
                }
                if (!File.Exists(csv_filePath))
                {
                    if (!File.Exists(System.IO.Path.Combine(working_folder, "Trad_" + csvFilename)))
                    {
                        System.Windows.MessageBox.Show("Other files generated from UWG simulation not found. Please run UWG simulation first before visualizing results.");
                        return;
                    }
                    else
                    {
                        csv_file = utci.UTCI_calc(working_folder, csvFilename);
                        csv_filePath = System.IO.Path.Combine(working_folder, csv_file);
                    }
                }
                if (csv_filePath != "")
                {
                    //String utciOutputSim6 = utci_sim6();

                    //need to run UTCI on this csv at fullPath#
                    //then take output of UTCI and grab data
                    var reader = new StreamReader(File.OpenRead(csv_filePath));

                    int row = 0;
                    int column = 0;
                    while (row < 24)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        while (!reader.EndOfStream && column < 88)
                        {
                            if (column < values.Length)
                            {
                                if (column == 87)
                                {
                                    sim6Data[row, column] = values[column].Split(':')[0];
                                }
                                else
                                {
                                    sim6Data[row, column] = values[column];
                                }
                                column += 1;

                            }
                            else
                            {
                                break;
                            }
                        }
                        row += 1;
                        column = 0;

                    }
                    utciSim6_change(sender, e);
                    tAirSim6_change(sender, e);
                    sim6Hours.Content = (Int32.Parse(sim6Data[9, 86].Substring(0, sim6Data[9, 86].Length - 1)) + Int32.Parse(sim6Data[10, 86].Substring(0, sim6Data[10, 86].Length - 1))).ToString() + "%";
                    simTab6.Visibility = System.Windows.Visibility.Visible;
                    labelSim6.Content = System.IO.Path.GetFileName(pathSim6);
                    labelSim6.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    labelSim6.Margin = new Thickness(4, 4, 4, 4);
                    labelSim6.Padding = new Thickness(0, 0, 0, 0);

                    simTab6.Visibility = System.Windows.Visibility.Visible;
                    this.isSim6Visible = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Second exception caught.", ex);
            }
        }



        private void makeFile_Click(object sender, RoutedEventArgs e)
        {
            urbanCanyonTab.Visibility = System.Windows.Visibility.Visible;
            if (this.isTypTab1Visible || typTab1.Visibility == System.Windows.Visibility.Visible)
            {
                typTab1.Visibility = System.Windows.Visibility.Visible;
            }
            else 
            { 
                typTab1.Visibility = System.Windows.Visibility.Collapsed; 
            }
            if (this.isTypTab2Visible || typTab2.Visibility == System.Windows.Visibility.Visible)
            {
                typTab2.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                typTab2.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (this.isTypTab3Visible || typTab3.Visibility == System.Windows.Visibility.Visible)
            {
                typTab3.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                typTab3.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (this.isTypTab4Visible || typTab4.Visibility == System.Windows.Visibility.Visible)
            {
                typTab4.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                typTab4.Visibility = System.Windows.Visibility.Collapsed;
            } 
            refSiteTab.Visibility = System.Windows.Visibility.Visible;
            runSimTab.Visibility = System.Windows.Visibility.Collapsed;
            viewSimTab.Visibility = System.Windows.Visibility.Collapsed;
            simTab1.Visibility = System.Windows.Visibility.Collapsed;
            simTab2.Visibility = System.Windows.Visibility.Collapsed;
            simTab3.Visibility = System.Windows.Visibility.Collapsed;
            simTab4.Visibility = System.Windows.Visibility.Collapsed;
            simTab5.Visibility = System.Windows.Visibility.Collapsed;
            simTab6.Visibility = System.Windows.Visibility.Collapsed;

            makeFileMenuItem.BorderBrush = new SolidColorBrush(Color.FromArgb(10, 0, 255, 180));
            makeFileMenuItem.BorderThickness = new Thickness(0,0,0,3);
            runMenuItem.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            simMenuItem.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));

            if (mainTabControl.SelectedItem == urbanCanyonTab || mainTabControl.SelectedItem == refSiteTab)
            {
                makeFileContextMenu.IsOpen = true;
            }
            if (mainTabControl.SelectedItem != refSiteTab)
            {
                mainTabControl.SelectedItem = urbanCanyonTab;
            }
        }

        private void importUMI(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Importing to be added");
        }

        private String utci_sim1()
        {
            return "";

        }

        private void addTypology(object sender, RoutedEventArgs e)
        {
            minusTypButton.IsEnabled = true;
            numberOfTypology++;
            if(numberOfTypology==2)
            {
                typLabel2.Visibility = System.Windows.Visibility.Visible;
                typology2Type.Visibility = System.Windows.Visibility.Visible;
                typology2Dist.Visibility = System.Windows.Visibility.Visible;
                typPercLabel2.Visibility = System.Windows.Visibility.Visible;
                //typTab2.Visibility = System.Windows.Visibility.Visible;
                addTypButton.IsEnabled = true;
            }
            else if (numberOfTypology == 3)
            {
                typLabel3.Visibility = System.Windows.Visibility.Visible;
                typology3Type.Visibility = System.Windows.Visibility.Visible;
                typology3Dist.Visibility = System.Windows.Visibility.Visible;
                typPercLabel3.Visibility = System.Windows.Visibility.Visible;
                //typTab3.Visibility = System.Windows.Visibility.Visible;
                addTypButton.IsEnabled = true;

            }
            else if (numberOfTypology == 4)
            {
                typLabel4.Visibility = System.Windows.Visibility.Visible;
                typology4Type.Visibility = System.Windows.Visibility.Visible;
                typology4Dist.Visibility = System.Windows.Visibility.Visible;
                typPercLabel4.Visibility = System.Windows.Visibility.Visible;
                //typTab4.Visibility = System.Windows.Visibility.Visible;
                addTypButton.IsEnabled = false;

            }
            typologyCheck(sender, e);


        }
        private void minusTypology(object sender, RoutedEventArgs e)
        {
            addTypButton.IsEnabled = true;
            if (numberOfTypology == 2)
            {
                typLabel2.Visibility = System.Windows.Visibility.Collapsed;
                typology2Type.Visibility = System.Windows.Visibility.Collapsed;
                typology2Dist.Visibility = System.Windows.Visibility.Collapsed;
                typPercLabel2.Visibility = System.Windows.Visibility.Collapsed;
                this.isTypTab2Visible = false;
                typTab1.Visibility = System.Windows.Visibility.Visible;
                minusTypButton.IsEnabled = false;
                typology2Dist.Text = "";

            }
            else if (numberOfTypology == 3)
            {
                typLabel3.Visibility = System.Windows.Visibility.Collapsed;
                typology3Type.Visibility = System.Windows.Visibility.Collapsed;
                typology3Dist.Visibility = System.Windows.Visibility.Collapsed;
                typPercLabel3.Visibility = System.Windows.Visibility.Collapsed;
                this.isTypTab3Visible = false;
                typology3Dist.Text = "";
            }
            else if (numberOfTypology == 4)
            {
                typLabel4.Visibility = System.Windows.Visibility.Collapsed;
                typology4Type.Visibility = System.Windows.Visibility.Collapsed;
                typology4Dist.Visibility = System.Windows.Visibility.Collapsed;
                typPercLabel4.Visibility = System.Windows.Visibility.Collapsed;
                typTab4.Visibility = System.Windows.Visibility.Collapsed;
                this.isTypTab4Visible = false;
                typology4Dist.Text = "";
            }
            typologyCheck(sender, e);
            numberOfTypology--;



        }

        private Boolean typologyCheck(object sender, RoutedEventArgs e)
        {
            //convert dists to longs, add to make sure equal 1
            totalDist = 0.0;
            double dist = 0.0;
            try
            {
                totalDist += double.Parse(typology1Dist.Text);
                if (typLabel2.Visibility == System.Windows.Visibility.Visible)
                {
                    dist = double.Parse(typology2Dist.Text);
                    totalDist += dist;
                }
                if (typLabel3.Visibility == System.Windows.Visibility.Visible)
                {
                    dist = double.Parse(typology3Dist.Text);
                    totalDist += dist;
                }
                if (typLabel4.Visibility == System.Windows.Visibility.Visible)
                {
                    dist = double.Parse(typology4Dist.Text);
                    totalDist += dist;
                }
                if (totalDist == 0.0)
                {
                    if (!(e.OriginalSource.Equals(saveButton)))
                    {
                        return true;
                    }
                }
                if (totalDist != 100.0)
                {
                    System.Windows.MessageBox.Show("Distributions don't add to 100%. Please adjust appropriately.");
                    return false;
                }

                return true;
            }
            catch
            {
                System.Windows.MessageBox.Show("Distributions don't add to 100%. It may be due to non-numbers in the input. Please adjust appropriately.");
                return false;
            }
        }
        
        private void Enable(object sender, EventArgs e)
        {
            this.IsEnabled = true;
        }
        //Save Button
        private void buttonSave_Click1(object sender, RoutedEventArgs e)
        {
            if (typologyCheck(sender, e) == false)
            {
                //will display message if typology doesn't sum to 100%
            }
            else
            {
                if (this.xmlFileName == "")
                {
                    button1_Click(sender, e);
                }
                else
                {
                    buttonSave_Click(sender, e);
                    System.Windows.MessageBox.Show("Saving succeeded!");
                }
            }
        }
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            DialogName = "Save";
            if (this.xmlFileName == "")
            {
                button1_Click(sender, e);
            }
            else
            {
                try
                {
                    //List<City> cities = Parse_CSV();
                    //List<SensAnalysis> sensA = Parse_CSV_Sens();
                    //cities represent the combination of city and their building types 
                    //for (int i = 0; i < this.numCities; i++)
                    //Change each sensitivity analysis parameter one at a time
                    //for (int j = 0; j < this.numSens; j++)
                    {
                        // Form input and building template insertion for XML file creation
                        // WALL MATERIALS:
                        XDocument doc = new XDocument(new XElement("xml_input"));
                        doc.Root.Add(typ1XML_create());
                        doc.Root.Add(typ2XML_create());
                        doc.Root.Add(typ3XML_create());
                        doc.Root.Add(typ4XML_create());

                        doc.Root.Add(new XElement("urbanArea",
                                                                new XElement("averageBuildingHeight", avgBldgHeight.Text),
                                                                new XElement("siteCoverageRatio", siteCoverageRatio.Text),
                                                                new XElement("facadeToSiteRatio", facadeToSiteRatio.Text),
                                                                new XElement("treeCoverage", treeCoverage.Text),
                                                                new XElement("nonBldgSensibleHeat", sensibleAnthroHeat.Text),
                                                                new XElement("nonBldgLatentAnthropogenicHeat", latentAnthroHeat.Text),
                                                                new XElement("charLength", charLength.Text),
                                                                new XElement("treeLatent", latentTrees.Text),
                                                                new XElement("grassLatent", latentGrass.Text),
                                                                new XElement("vegAlbedo", vegAlbedo.Text),
                                                                new XElement("vegStart", vegStart.Text),
                                                                new XElement("vegEnd", vegEnd.Text),
                                                                new XElement("daytimeBLHeight", daytimeBLHeight.Text),
                                                                new XElement("nighttimeBLHeight", nighttimeBLHeight.Text),
                                                                new XElement("refHeight", refHeight.Text),
                                                                new XElement("urbanRoad",
                                                                    new XElement("albedo", urbanRoadAlbedo.Content),
                                                                    new XElement("emissivity", urbanRoadEmissivity.Content),
                                                                    new XElement("materials",
                                                                        new XAttribute("name", urbanRoadbox.Text),
                                                                        new XElement("names",
                                                                            new XElement("item", urbanRoadMaterial.Content)),
                                                                        new XElement("thermalConductivity",
                                                                            new XElement("item", urbanRoadK.Content)),
                                                                        new XElement("volumetricHeatCapacity",
                                                                            new XElement("item", urbanRoadVHC.Content)),
                                                                        new XElement("thickness", urbanRoadThickness.Content)),
                                                                    new XElement("vegetationCoverage", urbanRoadVegFraction.Text),
                                                                    new XElement("inclination", 1),
                                                                    new XElement("initialTemperature", 20))
                                                            ),
                                                            new XElement("referenceSite",
                                                                new XElement("latitude", latitude.Text),
                                                                new XElement("longitude", longitude.Text),
                                                                new XElement("averageObstacleHeight", avgObstacleHeight.Text),
                                                                new XElement("ruralRoad",
                                                                    new XElement("albedo", ruralRoadAlbedo.Content),
                                                                    new XElement("emissivity", ruralRoadEmissivity.Content),
                                                                    new XElement("materials",
                                                                        new XAttribute("name", ruralbox.Text),
                                                                        new XElement("names",
                                                                            new XElement("item", ruralRoadMaterial.Content)),
                                                                        new XElement("thermalConductivity",
                                                                            new XElement("item", ruralRoadK.Content)),
                                                                        new XElement("volumetricHeatCapacity",
                                                                            new XElement("item", ruralRoadVHC.Content)),
                                                                        new XElement("thickness", ruralRoadThickness.Content)),
                                                                    new XElement("vegetationCoverage", ruralRoadVegFraction.Text),
                                                                    new XElement("inclination", 1),
                                                                    new XElement("initialTemperature", 20))
                                                                ),
                                                            new XElement("parameter",
                                                                new XElement("tempHeight", tempHeight.Text),
                                                                new XElement("windHeight", windHeight.Text),
                                                                new XElement("circCoeff", 1.2),
                                                                new XElement("dayThreshold", 200),
                                                                new XElement("nightThreshold", 50),
                                                                new XElement("windMin", 0.1),
                                                                new XElement("windMax", 10),
                                                                new XElement("wgmax", 0.05),
                                                                new XElement("exCoeff", 0.3)
                                                            )
                                                    );


                        this.xmlFilePath = this.xmlPath + "\\" + this.xmlFileName;
                        doc.Save(xmlFilePath);
                    } //CLOSES: Change each sensitivity analysis parameter one at a time
                } //closes TRY

                catch (Exception error)
                {
                    textBox1.Text = error.ToString();
                }
            }
        }

        //Save As Button
        private void buttonSaveAs_Click(object sender, RoutedEventArgs e)
        {
            if (typologyCheck(sender, e) == false)
            {
                //will display error message
            }
            else {

                DialogName = "Save As";
                button1_Click(sender, e);
            }
        }

        private void checkIfPercEquals0(object sender, RoutedEventArgs e)
        {
            var inputTyp = sender as System.Windows.Controls.TextBox;
            var visibility = System.Windows.Visibility.Visible;
            //if (inputTyp.Text == "0" || inputTyp.Text == "")
            //{
            //    visibility = System.Windows.Visibility.Collapsed;
            //}
            try
            {
                if (typology1Dist.Text != "0" && typology1Dist.Text != "")
                {
                    this.isTypTab1Visible = true;
                    typTab1.Visibility = System.Windows.Visibility.Visible;
                }
                if (typology2Dist.Text != "0" && typology2Dist.Text != "")
                {
                    this.isTypTab2Visible = true;
                    typTab2.Visibility = visibility;
                }
                if (typology3Dist.Text != "0" && typology3Dist.Text != "")
                {
                    this.isTypTab3Visible = true; 
                    typTab3.Visibility = visibility;
                }
                if (typology4Dist.Text != "0" && typology4Dist.Text != "")
                {
                    this.isTypTab4Visible = true; 
                    typTab4.Visibility = visibility;
                }
            }
            catch
            {
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XDocument doc = new XDocument(new XElement("xml_input"));
                if (typology1Dist.Text == "0" || typology1Dist.Text == "")
                {
                    typTab1.Visibility = System.Windows.Visibility.Collapsed;
                }
                if (typology2Dist.Text == "0" || typology2Dist.Text == "")
                {
                    typTab2.Visibility = System.Windows.Visibility.Collapsed;
                }
                if (typology3Dist.Text == "0" || typology3Dist.Text == "")
                {
                    typTab3.Visibility = System.Windows.Visibility.Collapsed;
                }
                if (typology4Dist.Text == "0" || typology4Dist.Text == "")
                {
                    typTab4.Visibility = System.Windows.Visibility.Collapsed;
                }

                doc.Root.Add(typ1XML_create());
                doc.Root.Add(typ2XML_create());
                doc.Root.Add(typ3XML_create());
                doc.Root.Add(typ4XML_create());

                doc.Root.Add(new XElement("urbanArea",
                    new XElement("averageBuildingHeight", avgBldgHeight.Text),
                    new XElement("siteCoverageRatio", siteCoverageRatio.Text),
                    new XElement("facadeToSiteRatio", facadeToSiteRatio.Text),
                    new XElement("treeCoverage", treeCoverage.Text),
                    new XElement("nonBldgSensibleHeat", sensibleAnthroHeat.Text),
                    new XElement("nonBldgLatentAnthropogenicHeat", latentAnthroHeat.Text),
                    new XElement("charLength", charLength.Text),
                    new XElement("treeLatent", latentTrees.Text),
                    new XElement("grassLatent", latentGrass.Text),
                    new XElement("vegAlbedo", vegAlbedo.Text),
                    new XElement("vegStart", vegStart.Text),
                    new XElement("vegEnd", vegEnd.Text),
                    new XElement("daytimeBLHeight", daytimeBLHeight.Text),
                    new XElement("nighttimeBLHeight", nighttimeBLHeight.Text),
                    new XElement("refHeight", refHeight.Text),
                    new XElement("urbanRoad",
                        new XElement("albedo", urbanRoadAlbedo.Content),
                        new XElement("emissivity", urbanRoadEmissivity.Content),
                        new XElement("materials",
                            new XElement("names",
                                new XElement("item", urbanRoadMaterial.Content)),
                            new XElement("thermalConductivity",
                                new XElement("item", urbanRoadK.Content)),
                            new XElement("volumetricHeatCapacity",
                                new XElement("item", urbanRoadVHC.Content)),
                            new XElement("thickness", urbanRoadThickness.Content)),
                        new XElement("vegetationCoverage", urbanRoadVegFraction.Text),
                        new XElement("inclination", 1),
                        new XElement("initialTemperature", 20))
                ),
                new XElement("referenceSite",
                    new XElement("latitude", latitude.Text),
                    new XElement("longitude", longitude.Text),
                    new XElement("averageObstacleHeight", avgObstacleHeight.Text),
                    new XElement("ruralRoad",
                        new XElement("albedo", ruralRoadAlbedo.Content),
                        new XElement("emissivity", ruralRoadEmissivity.Content),
                        new XElement("materials",
                            new XElement("names",
                                new XElement("item", ruralRoadMaterial.Content)),
                            new XElement("thermalConductivity",
                                new XElement("item", ruralRoadK.Content)),
                            new XElement("volumetricHeatCapacity",
                                new XElement("item", ruralRoadVHC.Content)),
                            new XElement("thickness", ruralRoadThickness.Content)),
                        new XElement("vegetationCoverage", ruralRoadVegFraction.Text),
                        new XElement("inclination", 1),
                        new XElement("initialTemperature", 20))
                ),
                new XElement("parameter",
                    new XElement("tempHeight", 2),
                    new XElement("windHeight", 10),
                    new XElement("circCoeff", 1.2),
                    new XElement("dayThreshold", 200),
                    new XElement("nightThreshold", 50),
                    new XElement("windMin", 0.1),
                    new XElement("windMax", 10),
                    new XElement("wgmax", 0.005),
                    new XElement("exCoeff", 0.3),
                    new XElement("simuStartMonth" , p.simuStartMonthValidate),
                    new XElement("simuStartDay", p.simuStartDayValidate),
                    new XElement("simuDuration", p.simuDurationValidate)
                                
                ));

                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.Title = DialogName;
                dlg.FileName = "uwgParameters.xml"; // Default file name
                dlg.DefaultExt = ".xml"; // Default file extension
                dlg.Filter = "XML files (.xml)|*.xml"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    filename_xml = dlg.FileName;
                    //To display in textbox
                    //xmlFileNameTextBox.Text = filename_xml;
                    //Get directory path only
                    this.xmlPath = System.IO.Path.GetDirectoryName(filename_xml);
                    //Get only the file name
                    this.xmlFileName = System.IO.Path.GetFileName(filename_xml);
                }
                else
                {
                    return;
                }

                //CREATE XML FILE//
                //next two lines for sensitivity analysis
                //xmlFilename = cities[i].cityNameType + "-" + sensA[j].sensParam + "_" + sensA[j].sensValue + ".xml";
                //xmlPath = path + city + "\\xml\\" + xmlFilename;
                this.xmlFilePath = this.xmlPath + "\\" + this.xmlFileName;
                doc.Save(xmlFilePath);
            } //closes TRY

            catch (Exception error)
            {
                textBox1.Text = error.ToString();
            }
        } //closes BUTTON1_CLICK [xml]

        private XElement typ1XML_create()
        {
            try
            {
                XAttribute perc = new XAttribute("dist", typology1Dist.Text);
                XAttribute name = new XAttribute("name", typology1Type.Text);
                // Form input and building template insertion for XML file creation
                // WALL MATERIALS:
                XElement wallMaterialsNames;
                if (wallLayer2Material.Content.Equals(""))
                {
                    wallMaterialsNames = new XElement("names",
                        new XElement("item", wallLayer1Material.Content));
                }
                else
                {
                    if (wallLayer3Material.Content.Equals(""))
                    {
                        wallMaterialsNames = new XElement("names",
                            new XElement("item", wallLayer1Material.Content),
                            new XElement("item", wallLayer2Material.Content));
                    }
                    else
                    {
                        if (wallLayer4Material.Content.Equals(""))
                        {

                            wallMaterialsNames = new XElement("names",
                                new XElement("item", wallLayer1Material.Content),
                                new XElement("item", wallLayer2Material.Content),
                                new XElement("item", wallLayer3Material.Content));
                        }
                        else
                        {
                            wallMaterialsNames = new XElement("names",
                                new XElement("item", wallLayer1Material.Content),
                                new XElement("item", wallLayer2Material.Content),
                                new XElement("item", wallLayer3Material.Content),
                                new XElement("item", wallLayer4Material.Content));
                        }
                    }
                }

                XElement wallMaterialsThermalConductivity;
                if (wallLayer2K.Content.Equals(""))
                {
                    wallMaterialsThermalConductivity = new XElement("thermalConductivity",
                        new XElement("item", wallLayer1K.Content));
                }
                else
                {
                    if (wallLayer3K.Content.Equals(""))
                    {
                        wallMaterialsThermalConductivity = new XElement("thermalConductivity",
                            new XElement("item", wallLayer1K.Content),
                            new XElement("item", wallLayer2K.Content));
                    }
                    else
                    {
                        if (wallLayer4K.Content.Equals(""))
                        {

                            wallMaterialsThermalConductivity = new XElement("thermalConductivity",
                                new XElement("item", wallLayer1K.Content),
                                new XElement("item", wallLayer2K.Content),
                                new XElement("item", wallLayer3K.Content));
                        }
                        else
                        {
                            wallMaterialsThermalConductivity = new XElement("thermalConductivity",
                                new XElement("item", wallLayer1K.Content),
                                new XElement("item", wallLayer2K.Content),
                                new XElement("item", wallLayer3K.Content),
                                new XElement("item", wallLayer4K.Content));
                        }
                    }
                }

                XElement wallMaterialsVolumetricHeatCapacity;
                if (wallLayer2VHC.Content.Equals(""))
                {
                    wallMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                        new XElement("item", wallLayer1VHC.Content));
                }
                else
                {
                    if (wallLayer3VHC.Equals(""))
                    {
                        wallMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                            new XElement("item", wallLayer1VHC.Content),
                            new XElement("item", wallLayer2VHC.Content));
                    }
                    else
                    {
                        if (wallLayer4VHC.Content.Equals(""))
                        {

                            wallMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                                new XElement("item", wallLayer1VHC.Content),
                                new XElement("item", wallLayer2VHC.Content),
                                new XElement("item", wallLayer3VHC.Content));
                        }
                        else
                        {
                            wallMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                                new XElement("item", wallLayer1VHC.Content),
                                new XElement("item", wallLayer2VHC.Content),
                                new XElement("item", wallLayer3VHC.Content),
                                new XElement("item", wallLayer4VHC.Content));
                        }
                    }
                }
                XElement wallMaterialsThickness;
                if (wallLayer2Thickness.Content.Equals(""))
                {
                    wallMaterialsThickness = new XElement("thickness", "[" + wallLayer1Thickness.Content + "]");
                }
                else
                {
                    if (wallLayer3Thickness.Equals(""))
                    {
                        wallMaterialsThickness = new XElement("thickness", "[" + wallLayer1Thickness.Content + "," + wallLayer2Thickness.Content + "]");
                    }
                    else
                    {
                        if (wallLayer4Thickness.Content.Equals(""))
                        {
                            wallMaterialsThickness = new XElement("thickness", "[" + wallLayer1Thickness.Content + "," + wallLayer2Thickness.Content + "," + wallLayer3Thickness.Content + "]");
                        }
                        else
                        {
                            wallMaterialsThickness = new XElement("thickness", "[" + wallLayer1Thickness.Content + "," + wallLayer2Thickness.Content + "," + wallLayer3Thickness.Content + "," + wallLayer4Thickness.Content + "]");
                        }
                    }
                }

                //ROOF MATERIALS:
                XElement roofMaterialsNames;
                if (roofLayer2Material.Content.Equals(""))
                {
                    roofMaterialsNames = new XElement("names",
                        new XElement("item", roofLayer1Material.Content));
                }
                else
                {
                    if (roofLayer3Material.Equals(""))
                    {
                        roofMaterialsNames = new XElement("names",
                            new XElement("item", roofLayer1Material.Content),
                            new XElement("item", roofLayer2Material.Content));
                    }
                    else
                    {
                        roofMaterialsNames = new XElement("names",
                            new XElement("item", roofLayer1Material.Content),
                            new XElement("item", roofLayer2Material.Content),
                            new XElement("item", roofLayer3Material.Content));
                    }
                }

                XElement roofMaterialsThermalConductivity;
                if (roofLayer2K.Content.Equals(""))
                {
                    roofMaterialsThermalConductivity = new XElement("thermalConductivity",
                        new XElement("item", roofLayer1K.Content));
                }
                else
                {
                    if (roofLayer3K.Content.Equals(""))
                    {
                        roofMaterialsThermalConductivity = new XElement("thermalConductivity",
                            new XElement("item", roofLayer1K.Content),
                            new XElement("item", roofLayer2K.Content));
                    }
                    else
                    {
                        roofMaterialsThermalConductivity = new XElement("thermalConductivity",
                            new XElement("item", roofLayer1K.Content),
                            new XElement("item", roofLayer2K.Content),
                            new XElement("item", roofLayer3K.Content));
                    }
                }

                XElement roofMaterialsVolumetricHeatCapacity;
                if (roofLayer2VHC.Content.Equals(""))
                {
                    roofMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                        new XElement("item", roofLayer1VHC.Content));
                }
                else
                {
                    if (roofLayer3VHC.Content.Equals(""))
                    {
                        roofMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                            new XElement("item", roofLayer1VHC.Content),
                            new XElement("item", roofLayer2VHC.Content));
                    }
                    else
                    {
                        roofMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                            new XElement("item", roofLayer1VHC.Content),
                            new XElement("item", roofLayer2VHC.Content),
                            new XElement("item", roofLayer3VHC.Content));
                    }
                }
                XElement roofMaterialsThickness;
                if (roofLayer2Thickness.Content.Equals(""))
                {
                    roofMaterialsThickness = new XElement("thickness", "[" + roofLayer1Thickness.Content + "]");
                }
                else
                {
                    if (roofLayer3Thickness.Content.Equals(""))
                    {
                        roofMaterialsThickness = new XElement("thickness", "[" + roofLayer1Thickness.Content + "," + roofLayer2Thickness.Content + "]");
                    }
                    else
                    {
                        roofMaterialsThickness = new XElement("thickness", "[" + roofLayer1Thickness.Content + "," + roofLayer2Thickness.Content + "," + roofLayer3Thickness.Content + "]");
                    }
                }

                XElement elem = new XElement("typology1",
                                                    new XElement("dist", typology1Dist.Text),
                                                    new XElement("construction",
                                                        new XElement("wall",
                                                            new XElement("albedo", wallAlbedo.Text),
                                                            new XElement("emissivity", wallEmissivity.Text),
                                                            new XElement("materials",
                                                                new XAttribute("name", wallbox.SelectedItem),
                                                                wallMaterialsNames,
                                                                wallMaterialsThermalConductivity,
                                                                wallMaterialsVolumetricHeatCapacity,
                                                                wallMaterialsThickness),
                                                            new XElement("vegetationCoverage", wallVegCoverage.Text),
                                                            new XElement("inclination", wallInclination.Text),
                                                            new XElement("initialTemperature", 20)),

                                                        new XElement("roof",
                                                            new XElement("albedo", roofAlbedo.Text),
                                                            new XElement("emissivity", roofEmissivity.Text),
                                                            new XElement("materials",
                                                                new XAttribute("name", roofbox.SelectedItem), 
                                                                roofMaterialsNames,
                                                                roofMaterialsThermalConductivity,
                                                                roofMaterialsVolumetricHeatCapacity,
                                                                roofMaterialsThickness),
                                                            new XElement("vegetationCoverage", roofVegCoverage.Text),
                                                            new XElement("inclination", roofInclination.Text),
                                                            new XElement("initialTemperature", 20)),

                                                        new XElement("mass",
                                                            new XElement("albedo", massAlbedo.Content),
                                                                new XElement("emissivity", massEmissivity.Content),
                                                                new XElement("materials",
                                                                    new XAttribute("name", massbox.Text),
                                                                    new XElement("names",
                                                                        new XElement("item", massLayer1Material.Content)),
                                                                    new XElement("thermalConductivity",
                                                                        new XElement("item", massLayer1K.Content)),
                                                                    new XElement("volumetricHeatCapacity",
                                                                        new XElement("item", massLayer1VHC.Content)),
                                                                    new XElement("thickness", "[" + massLayer1Thickness.Content + "]")),
                                                                        new XElement("vegetationCoverage", 0),
                                                                        new XElement("inclination", 1),
                                                                         new XElement("initialTemperature", 20)),

                                                        new XElement("glazing",
                                                            new XAttribute("name", glazingbox.Text), 
                                                            new XElement("glazingRatio", wwr.Content),
                                                            new XElement("windowUvalue", uValue.Content),
                                                            new XElement("windowSHGC", SHGC.Content))
                                                    ),

                                                    //BUILDING GEOMETRY:
                                                    new XElement("building",
                                                        new XAttribute("name", heatbox.SelectedItem),
                                                        new XElement("floorHeight", floorHeight.Text),
                                                        new XElement("dayInternalGains", dayInternalHeatGain.Content),
                                                        new XElement("nightInternalGains", nightInternalHeatGain.Content),
                                                        new XElement("radiantFraction", radiantFraction.Text),
                                                        new XElement("latentFraction", latentFraction.Text),
                                                        new XElement("infiltration", infiltration.Content),
                                                        new XElement("ventilation", ventilation.Content),
                                                        new XElement("coolingSystemType", coolingSystemType.Text),
                                                        new XElement("coolingCOP", coolingCOP.Text),
                                                        new XElement("daytimeCoolingSetPoint", daytimeCoolingSetpoint.Text),
                                                        new XElement("nighttimeCoolingSetPoint", nighttimeCoolingSetpoint.Text),
                                                        new XElement("daytimeHeatingSetPoint", daytimeHeatingSetPoint.Text),
                                                        new XElement("nighttimeHeatingSetPoint", nighttimeHeatingSetPoint.Text),
                                                        new XElement("coolingCapacity", coolingCapacity.Text),
                                                        new XElement("heatingEfficiency", heatingEfficiency.Text),
                                                        new XElement("nightSetStart", nightSetStart.Text),
                                                        new XElement("nightSetEnd", nightSetEnd.Text),
                                                        new XElement("heatReleasedToCanyon", 0),
                                                        new XElement("initialT", 20)
                                                    )
                                            );
                elem.Add(perc);
                elem.Add(name);
                return elem;
            } //closes TRY

            catch (Exception error)
            {
                textBox1.Text = error.ToString();
                return null;
            }

        }
        private XElement typ2XML_create()
        {
            try
            {
                XAttribute perc = new XAttribute("dist", typology2Dist.Text);
                XAttribute name = new XAttribute("name", typology2Type.Text);
                // Form input and building template insertion for XML file creation
                // WALL MATERIALS:
                XElement wallMaterialsNames;
                if (wallLayer2MaterialTyp2.Content.Equals(""))
                {
                    wallMaterialsNames = new XElement("names",
                        new XElement("item", wallLayer1MaterialTyp2.Content));
                }
                else
                {
                    if (wallLayer3MaterialTyp2.Content.Equals(""))
                    {
                        wallMaterialsNames = new XElement("names",
                            new XElement("item", wallLayer1MaterialTyp2.Content),
                            new XElement("item", wallLayer2MaterialTyp2.Content));
                    }
                    else
                    {
                        if (wallLayer4MaterialTyp2.Content.Equals(""))
                        {

                            wallMaterialsNames = new XElement("names",
                                new XElement("item", wallLayer1MaterialTyp2.Content),
                                new XElement("item", wallLayer2MaterialTyp2.Content),
                                new XElement("item", wallLayer3MaterialTyp2.Content));
                        }
                        else
                        {
                            wallMaterialsNames = new XElement("names",
                                new XElement("item", wallLayer1MaterialTyp2.Content),
                                new XElement("item", wallLayer2MaterialTyp2.Content),
                                new XElement("item", wallLayer3MaterialTyp2.Content),
                                new XElement("item", wallLayer4MaterialTyp2.Content));
                        }
                    }
                }

                XElement wallMaterialsThermalConductivity;
                if (wallLayer2KTyp2.Content.Equals(""))
                {
                    wallMaterialsThermalConductivity = new XElement("thermalConductivity",
                        new XElement("item", wallLayer1KTyp2.Content));
                }
                else
                {
                    if (wallLayer3KTyp2.Content.Equals(""))
                    {
                        wallMaterialsThermalConductivity = new XElement("thermalConductivity",
                            new XElement("item", wallLayer1KTyp2.Content),
                            new XElement("item", wallLayer2KTyp2.Content));
                    }
                    else
                    {
                        if (wallLayer4KTyp2.Content.Equals(""))
                        {

                            wallMaterialsThermalConductivity = new XElement("thermalConductivity",
                                new XElement("item", wallLayer1KTyp2.Content),
                                new XElement("item", wallLayer2KTyp2.Content),
                                new XElement("item", wallLayer3KTyp2.Content));
                        }
                        else
                        {
                            wallMaterialsThermalConductivity = new XElement("thermalConductivity",
                                new XElement("item", wallLayer1KTyp2.Content),
                                new XElement("item", wallLayer2KTyp2.Content),
                                new XElement("item", wallLayer3KTyp2.Content),
                                new XElement("item", wallLayer4KTyp2.Content));
                        }
                    }
                }

                XElement wallMaterialsVolumetricHeatCapacity;
                if (wallLayer2VHCTyp2.Content.Equals(""))
                {
                    wallMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                        new XElement("item", wallLayer1VHCTyp2.Content));
                }
                else
                {
                    if (wallLayer3VHCTyp2.Equals(""))
                    {
                        wallMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                            new XElement("item", wallLayer1VHCTyp2.Content),
                            new XElement("item", wallLayer2VHCTyp2.Content));
                    }
                    else
                    {
                        if (wallLayer4VHCTyp2.Content.Equals(""))
                        {

                            wallMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                                new XElement("item", wallLayer1VHCTyp2.Content),
                                new XElement("item", wallLayer2VHCTyp2.Content),
                                new XElement("item", wallLayer3VHCTyp2.Content));
                        }
                        else
                        {
                            wallMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                                new XElement("item", wallLayer1VHCTyp2.Content),
                                new XElement("item", wallLayer2VHCTyp2.Content),
                                new XElement("item", wallLayer3VHCTyp2.Content),
                                new XElement("item", wallLayer4VHCTyp2.Content));
                        }
                    }
                }
                XElement wallMaterialsThickness;
                if (wallLayer2ThicknessTyp2.Content.Equals(""))
                {
                    wallMaterialsThickness = new XElement("thickness", "[" + wallLayer1ThicknessTyp2.Content + "]");
                }
                else
                {
                    if (wallLayer3ThicknessTyp2.Equals(""))
                    {
                        wallMaterialsThickness = new XElement("thickness", "[" + wallLayer1ThicknessTyp2.Content + "," + wallLayer2ThicknessTyp2.Content + "]");
                    }
                    else
                    {
                        if (wallLayer4ThicknessTyp2.Content.Equals(""))
                        {
                            wallMaterialsThickness = new XElement("thickness", "[" + wallLayer1ThicknessTyp2.Content + "," + wallLayer2ThicknessTyp2.Content + "," + wallLayer3ThicknessTyp2.Content + "]");
                        }
                        else
                        {
                            wallMaterialsThickness = new XElement("thickness", "[" + wallLayer1ThicknessTyp2.Content + "," + wallLayer2ThicknessTyp2.Content + "," + wallLayer3ThicknessTyp2.Content + "," + wallLayer4ThicknessTyp2.Content + "]");
                        }
                    }
                }

                //ROOF MATERIALS:
                XElement roofMaterialsNames;
                if (roofLayer2MaterialTyp2.Content.Equals(""))
                {
                    roofMaterialsNames = new XElement("names",
                        new XElement("item", roofLayer1MaterialTyp2.Content));
                }
                else
                {
                    if (roofLayer3MaterialTyp2.Equals(""))
                    {
                        roofMaterialsNames = new XElement("names",
                            new XElement("item", roofLayer1MaterialTyp2.Content),
                            new XElement("item", roofLayer2MaterialTyp2.Content));
                    }
                    else
                    {
                        roofMaterialsNames = new XElement("names",
                            new XElement("item", roofLayer1MaterialTyp2.Content),
                            new XElement("item", roofLayer2MaterialTyp2.Content),
                            new XElement("item", roofLayer3MaterialTyp2.Content));
                    }
                }

                XElement roofMaterialsThermalConductivity;
                if (roofLayer2KTyp2.Content.Equals(""))
                {
                    roofMaterialsThermalConductivity = new XElement("thermalConductivity",
                        new XElement("item", roofLayer1KTyp2.Content));
                }
                else
                {
                    if (roofLayer3KTyp2.Content.Equals(""))
                    {
                        roofMaterialsThermalConductivity = new XElement("thermalConductivity",
                            new XElement("item", roofLayer1KTyp2.Content),
                            new XElement("item", roofLayer2KTyp2.Content));
                    }
                    else
                    {
                        roofMaterialsThermalConductivity = new XElement("thermalConductivity",
                            new XElement("item", roofLayer1KTyp2.Content),
                            new XElement("item", roofLayer2KTyp2.Content),
                            new XElement("item", roofLayer3KTyp2.Content));
                    }
                }

                XElement roofMaterialsVolumetricHeatCapacity;
                if (roofLayer2VHCTyp2.Content.Equals(""))
                {
                    roofMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                        new XElement("item", roofLayer1VHCTyp2.Content));
                }
                else
                {
                    if (roofLayer3VHCTyp2.Content.Equals(""))
                    {
                        roofMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                            new XElement("item", roofLayer1VHCTyp2.Content),
                            new XElement("item", roofLayer2VHCTyp2.Content));
                    }
                    else
                    {
                        roofMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                            new XElement("item", roofLayer1VHCTyp2.Content),
                            new XElement("item", roofLayer2VHCTyp2.Content),
                            new XElement("item", roofLayer3VHCTyp2.Content));
                    }
                }
                XElement roofMaterialsThickness;
                if (roofLayer2ThicknessTyp2.Content.Equals(""))
                {
                    roofMaterialsThickness = new XElement("thickness", "[" + roofLayer1ThicknessTyp2.Content + "]");
                }
                else
                {
                    if (roofLayer3ThicknessTyp2.Content.Equals(""))
                    {
                        roofMaterialsThickness = new XElement("thickness", "[" + roofLayer1ThicknessTyp2.Content + "," + roofLayer2ThicknessTyp2.Content + "]");
                    }
                    else
                    {
                        roofMaterialsThickness = new XElement("thickness", "[" + roofLayer1ThicknessTyp2.Content + "," + roofLayer2ThicknessTyp2.Content + "," + roofLayer3ThicknessTyp2.Content + "]");
                    }
                }

                XElement elem = new XElement("typology2",
                                                    new XElement("dist", typology2Dist.Text), 
                                                    new XElement("construction",
                                                        new XElement("wall",
                                                            new XElement("albedo", wallAlbedoTyp2.Text),
                                                            new XElement("emissivity", wallEmissivityTyp2.Text),
                                                            new XElement("materials",
                                                                new XAttribute("name", wallboxTyp2.SelectedItem),
                                                                wallMaterialsNames,
                                                                wallMaterialsThermalConductivity,
                                                                wallMaterialsVolumetricHeatCapacity,
                                                                wallMaterialsThickness),
                                                            new XElement("vegetationCoverage", wallVegCoverageTyp2.Text),
                                                            new XElement("inclination", wallInclinationTyp2.Text),
                                                            new XElement("initialTemperature", 20)),

                                                        new XElement("roof",
                                                            new XElement("albedo", roofAlbedoTyp2.Text),
                                                            new XElement("emissivity", roofEmissivityTyp2.Text),
                                                            new XElement("materials",
                                                                new XAttribute("name", roofboxTyp2.SelectedItem),
                                                                roofMaterialsNames,
                                                                roofMaterialsThermalConductivity,
                                                                roofMaterialsVolumetricHeatCapacity,
                                                                roofMaterialsThickness),
                                                            new XElement("vegetationCoverage", roofVegCoverageTyp2.Text),
                                                            new XElement("inclination", roofInclinationTyp2.Text),
                                                            new XElement("initialTemperature", 20)),

                                                        new XElement("mass",
                                                            new XElement("albedo", massAlbedoTyp2.Content),
                                                                new XElement("emissivity", massEmissivityTyp2.Content),
                                                                new XElement("materials",
                                                                    new XAttribute("name", massboxTyp2.Text),
                                                                    new XElement("names",
                                                                    new XElement("item", massLayer1MaterialTyp2.Content)),
                                                                    new XElement("thermalConductivity",
                                                                        new XElement("item", massLayer1KTyp2.Content)),
                                                                    new XElement("volumetricHeatCapacity",
                                                                        new XElement("item", massLayer1VHCTyp2.Content)),
                                                                    new XElement("thickness", "[" + massLayer1ThicknessTyp2.Content + "]")),
                                                                        new XElement("vegetationCoverage", 0),
                                                                        new XElement("inclination", 1),
                                                                         new XElement("initialTemperature", 20)),

                                                        new XElement("glazing",
                                                            new XAttribute("name", glazingboxTyp2.Text),
                                                            new XElement("glazingRatio", wwrTyp2.Content),
                                                            new XElement("windowUvalue", uValueTyp2.Content),
                                                            new XElement("windowSHGC", SHGCTyp2.Content))
                                                    ),

                                                    //BUILDING GEOMETRY:
                                                    new XElement("building",
                                                        new XAttribute("name", heatbox.SelectedItem), 
                                                        new XElement("floorHeight", floorHeightTyp2.Text),
                                                        new XElement("dayInternalGains", dayInternalHeatGainTyp2.Content),
                                                        new XElement("nightInternalGains", nightInternalHeatGainTyp2.Content),
                                                        new XElement("radiantFraction", radiantFractionTyp2.Text),
                                                        new XElement("latentFraction", latentFractionTyp2.Text),
                                                        new XElement("infiltration", infiltrationTyp2.Content),
                                                        new XElement("ventilation", ventilationTyp2.Content),
                                                        new XElement("coolingSystemType", coolingSystemTypeTyp2.Text),
                                                        new XElement("coolingCOP", coolingCOPTyp2.Text),
                                                        new XElement("daytimeCoolingSetPoint", daytimeCoolingSetpointTyp2.Text),
                                                        new XElement("nighttimeCoolingSetPoint", nighttimeCoolingSetpointTyp2.Text),
                                                        new XElement("daytimeHeatingSetPoint", daytimeHeatingSetPointTyp2.Text),
                                                        new XElement("nighttimeHeatingSetPoint", nighttimeHeatingSetPointTyp2.Text),
                                                        new XElement("coolingCapacity", coolingCapacityTyp2.Text),
                                                        new XElement("heatingEfficiency", heatingEfficiencyTyp2.Text),
                                                        new XElement("nightSetStart", nightSetStartTyp2.Text),
                                                        new XElement("nightSetEnd", nightSetEndTyp2.Text),
                                                        new XElement("heatReleasedToCanyon", 0),
                                                        new XElement("initialT", 20)
                                                    )
                                            );
                elem.Add(perc);
                elem.Add(name);
                return elem;
            } //closes TRY

            catch (Exception error)
            {
                textBox1.Text = error.ToString();
                return null;
            }

        }
        private XElement typ3XML_create()
        {
            try
            {
                XAttribute perc = new XAttribute("dist", typology3Dist.Text);
                XAttribute name = new XAttribute("name", typology3Type.Text);
                // Form input and building template insertion for XML file creation
                // WALL MATERIALS:
                XElement wallMaterialsNames;
                if (wallLayer2MaterialTyp3.Content.Equals(""))
                {
                    wallMaterialsNames = new XElement("names",
                        new XElement("item", wallLayer1MaterialTyp3.Content));
                }
                else
                {
                    if (wallLayer3MaterialTyp3.Content.Equals(""))
                    {
                        wallMaterialsNames = new XElement("names",
                            new XElement("item", wallLayer1MaterialTyp3.Content),
                            new XElement("item", wallLayer2MaterialTyp3.Content));
                    }
                    else
                    {
                        if (wallLayer4MaterialTyp3.Content.Equals(""))
                        {

                            wallMaterialsNames = new XElement("names",
                                new XElement("item", wallLayer1MaterialTyp3.Content),
                                new XElement("item", wallLayer2MaterialTyp3.Content),
                                new XElement("item", wallLayer3MaterialTyp3.Content));
                        }
                        else
                        {
                            wallMaterialsNames = new XElement("names",
                                new XElement("item", wallLayer1MaterialTyp3.Content),
                                new XElement("item", wallLayer2MaterialTyp3.Content),
                                new XElement("item", wallLayer3MaterialTyp3.Content),
                                new XElement("item", wallLayer4MaterialTyp3.Content));
                        }
                    }
                }

                XElement wallMaterialsThermalConductivity;
                if (wallLayer2KTyp3.Content.Equals(""))
                {
                    wallMaterialsThermalConductivity = new XElement("thermalConductivity",
                        new XElement("item", wallLayer1KTyp3.Content));
                }
                else
                {
                    if (wallLayer3KTyp3.Content.Equals(""))
                    {
                        wallMaterialsThermalConductivity = new XElement("thermalConductivity",
                            new XElement("item", wallLayer1KTyp3.Content),
                            new XElement("item", wallLayer2KTyp3.Content));
                    }
                    else
                    {
                        if (wallLayer4KTyp3.Content.Equals(""))
                        {

                            wallMaterialsThermalConductivity = new XElement("thermalConductivity",
                                new XElement("item", wallLayer1KTyp3.Content),
                                new XElement("item", wallLayer2KTyp3.Content),
                                new XElement("item", wallLayer3KTyp3.Content));
                        }
                        else
                        {
                            wallMaterialsThermalConductivity = new XElement("thermalConductivity",
                                new XElement("item", wallLayer1KTyp3.Content),
                                new XElement("item", wallLayer2KTyp3.Content),
                                new XElement("item", wallLayer3KTyp3.Content),
                                new XElement("item", wallLayer4KTyp3.Content));
                        }
                    }
                }

                XElement wallMaterialsVolumetricHeatCapacity;
                if (wallLayer2VHCTyp3.Content.Equals(""))
                {
                    wallMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                        new XElement("item", wallLayer1VHCTyp3.Content));
                }
                else
                {
                    if (wallLayer3VHCTyp3.Equals(""))
                    {
                        wallMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                            new XElement("item", wallLayer1VHCTyp3.Content),
                            new XElement("item", wallLayer2VHCTyp3.Content));
                    }
                    else
                    {
                        if (wallLayer4VHCTyp3.Content.Equals(""))
                        {

                            wallMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                                new XElement("item", wallLayer1VHCTyp3.Content),
                                new XElement("item", wallLayer2VHCTyp3.Content),
                                new XElement("item", wallLayer3VHCTyp3.Content));
                        }
                        else
                        {
                            wallMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                                new XElement("item", wallLayer1VHCTyp3.Content),
                                new XElement("item", wallLayer2VHCTyp3.Content),
                                new XElement("item", wallLayer3VHCTyp3.Content),
                                new XElement("item", wallLayer4VHCTyp3.Content));
                        }
                    }
                }
                XElement wallMaterialsThickness;
                if (wallLayer2ThicknessTyp3.Content.Equals(""))
                {
                    wallMaterialsThickness = new XElement("thickness", "[" + wallLayer1ThicknessTyp3.Content + "]");
                }
                else
                {
                    if (wallLayer3ThicknessTyp3.Equals(""))
                    {
                        wallMaterialsThickness = new XElement("thickness", "[" + wallLayer1ThicknessTyp3.Content + "," + wallLayer2ThicknessTyp3.Content + "]");
                    }
                    else
                    {
                        if (wallLayer4ThicknessTyp3.Content.Equals(""))
                        {
                            wallMaterialsThickness = new XElement("thickness", "[" + wallLayer1ThicknessTyp3.Content + "," + wallLayer2ThicknessTyp3.Content + "," + wallLayer3ThicknessTyp3.Content + "]");
                        }
                        else
                        {
                            wallMaterialsThickness = new XElement("thickness", "[" + wallLayer1ThicknessTyp3.Content + "," + wallLayer2ThicknessTyp3.Content + "," + wallLayer3ThicknessTyp3.Content + "," + wallLayer4ThicknessTyp3.Content + "]");
                        }
                    }
                }

                //ROOF MATERIALS:
                XElement roofMaterialsNames;
                if (roofLayer2MaterialTyp3.Content.Equals(""))
                {
                    roofMaterialsNames = new XElement("names",
                        new XElement("item", roofLayer1MaterialTyp3.Content));
                }
                else
                {
                    if (roofLayer3MaterialTyp3.Equals(""))
                    {
                        roofMaterialsNames = new XElement("names",
                            new XElement("item", roofLayer1MaterialTyp3.Content),
                            new XElement("item", roofLayer2MaterialTyp3.Content));
                    }
                    else
                    {
                        roofMaterialsNames = new XElement("names",
                            new XElement("item", roofLayer1MaterialTyp3.Content),
                            new XElement("item", roofLayer2MaterialTyp3.Content),
                            new XElement("item", roofLayer3MaterialTyp3.Content));
                    }
                }

                XElement roofMaterialsThermalConductivity;
                if (roofLayer2KTyp3.Content.Equals(""))
                {
                    roofMaterialsThermalConductivity = new XElement("thermalConductivity",
                        new XElement("item", roofLayer1KTyp3.Content));
                }
                else
                {
                    if (roofLayer3KTyp3.Content.Equals(""))
                    {
                        roofMaterialsThermalConductivity = new XElement("thermalConductivity",
                            new XElement("item", roofLayer1KTyp3.Content),
                            new XElement("item", roofLayer2KTyp3.Content));
                    }
                    else
                    {
                        roofMaterialsThermalConductivity = new XElement("thermalConductivity",
                            new XElement("item", roofLayer1KTyp3.Content),
                            new XElement("item", roofLayer2KTyp3.Content),
                            new XElement("item", roofLayer3KTyp3.Content));
                    }
                }

                XElement roofMaterialsVolumetricHeatCapacity;
                if (roofLayer2VHCTyp3.Content.Equals(""))
                {
                    roofMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                        new XElement("item", roofLayer1VHCTyp3.Content));
                }
                else
                {
                    if (roofLayer3VHCTyp3.Content.Equals(""))
                    {
                        roofMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                            new XElement("item", roofLayer1VHCTyp3.Content),
                            new XElement("item", roofLayer2VHCTyp3.Content));
                    }
                    else
                    {
                        roofMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                            new XElement("item", roofLayer1VHCTyp3.Content),
                            new XElement("item", roofLayer2VHCTyp3.Content),
                            new XElement("item", roofLayer3VHCTyp3.Content));
                    }
                }
                XElement roofMaterialsThickness;
                if (roofLayer2ThicknessTyp3.Content.Equals(""))
                {
                    roofMaterialsThickness = new XElement("thickness", "[" + roofLayer1ThicknessTyp3.Content + "]");
                }
                else
                {
                    if (roofLayer3ThicknessTyp3.Content.Equals(""))
                    {
                        roofMaterialsThickness = new XElement("thickness", "[" + roofLayer1ThicknessTyp3.Content + "," + roofLayer2ThicknessTyp3.Content + "]");
                    }
                    else
                    {
                        roofMaterialsThickness = new XElement("thickness", "[" + roofLayer1ThicknessTyp3.Content + "," + roofLayer2ThicknessTyp3.Content + "," + roofLayer3ThicknessTyp3.Content + "]");
                    }
                }

                XElement elem = new XElement("typology3",
                                                    new XElement("dist", typology3Dist.Text), 
                                                    new XElement("construction",
                                                        new XElement("wall",
                                                            new XElement("albedo", wallAlbedoTyp3.Text),
                                                            new XElement("emissivity", wallEmissivityTyp3.Text),
                                                            new XElement("materials",
                                                                new XAttribute("name", wallboxTyp3.SelectedItem),
                                                                wallMaterialsNames,
                                                                wallMaterialsThermalConductivity,
                                                                wallMaterialsVolumetricHeatCapacity,
                                                                wallMaterialsThickness),
                                                            new XElement("vegetationCoverage", wallVegCoverageTyp3.Text),
                                                            new XElement("inclination", wallInclinationTyp3.Text),
                                                            new XElement("initialTemperature", 20)),

                                                        new XElement("roof",
                                                            new XElement("albedo", roofAlbedoTyp3.Text),
                                                            new XElement("emissivity", roofEmissivityTyp3.Text),
                                                            new XElement("materials",
                                                                new XAttribute("name", roofboxTyp3.SelectedItem),
                                                                roofMaterialsNames,
                                                                roofMaterialsThermalConductivity,
                                                                roofMaterialsVolumetricHeatCapacity,
                                                                roofMaterialsThickness),
                                                            new XElement("vegetationCoverage", roofVegCoverageTyp3.Text),
                                                            new XElement("inclination", roofInclinationTyp3.Text),
                                                            new XElement("initialTemperature", 20)),

                                                        new XElement("mass",
                                                            new XElement("albedo", massAlbedoTyp3.Content),
                                                                new XElement("emissivity", massEmissivityTyp3.Content),
                                                                new XElement("materials",
                                                                    new XAttribute("name", massboxTyp3.Text),
                                                                    new XElement("names",
                                                                    new XElement("item", massLayer1MaterialTyp3.Content)),
                                                                    new XElement("thermalConductivity",
                                                                        new XElement("item", massLayer1KTyp3.Content)),
                                                                    new XElement("volumetricHeatCapacity",
                                                                        new XElement("item", massLayer1VHCTyp3.Content)),
                                                                    new XElement("thickness", "[" + massLayer1ThicknessTyp3.Content + "]")),
                                                                        new XElement("vegetationCoverage", 0),
                                                                        new XElement("inclination", 1),
                                                                         new XElement("initialTemperature", 20)),

                                                        new XElement("glazing",
                                                            new XAttribute("name", glazingboxTyp3.Text), 
                                                            new XElement("glazingRatio", wwrTyp3.Content),
                                                            new XElement("windowUvalue", uValueTyp3.Content),
                                                            new XElement("windowSHGC", SHGCTyp3.Content))
                                                    ),

                                                    //BUILDING GEOMETRY:
                                                    new XElement("building",
                                                        new XAttribute("name", heatbox.SelectedItem), 
                                                        new XElement("floorHeight", floorHeightTyp3.Text),
                                                        new XElement("dayInternalGains", dayInternalHeatGainTyp3.Content),
                                                        new XElement("nightInternalGains", nightInternalHeatGainTyp3.Content),
                                                        new XElement("radiantFraction", radiantFractionTyp3.Text),
                                                        new XElement("latentFraction", latentFractionTyp3.Text),
                                                        new XElement("infiltration", infiltrationTyp3.Content),
                                                        new XElement("ventilation", ventilationTyp3.Content),
                                                        new XElement("coolingSystemType", coolingSystemTypeTyp3.Text),
                                                        new XElement("coolingCOP", coolingCOPTyp3.Text),
                                                        new XElement("daytimeCoolingSetPoint", daytimeCoolingSetpointTyp3.Text),
                                                        new XElement("nighttimeCoolingSetPoint", nighttimeCoolingSetpointTyp3.Text),
                                                        new XElement("daytimeHeatingSetPoint", daytimeHeatingSetPointTyp3.Text),
                                                        new XElement("nighttimeHeatingSetPoint", nighttimeHeatingSetPointTyp3.Text),
                                                        new XElement("coolingCapacity", coolingCapacityTyp3.Text),
                                                        new XElement("heatingEfficiency", heatingEfficiencyTyp3.Text),
                                                        new XElement("nightSetStart", nightSetStartTyp3.Text),
                                                        new XElement("nightSetEnd", nightSetEndTyp3.Text),
                                                        new XElement("heatReleasedToCanyon", 0),
                                                        new XElement("initialT", 20)
                                                    )
                                            );
                elem.Add(perc);
                elem.Add(name);
                return elem;
            } //closes TRY

            catch (Exception error)
            {
                textBox1.Text = error.ToString();
                return null;
            }

        }
        private XElement typ4XML_create()
        {
            try
            {
                XAttribute perc = new XAttribute("dist", typology4Dist.Text);
                XAttribute name = new XAttribute("name", typology4Type.Text);
                // Form input and building template insertion for XML file creation
                // WALL MATERIALS:
                XElement wallMaterialsNames;
                if (wallLayer2MaterialTyp4.Content.Equals(""))
                {
                    wallMaterialsNames = new XElement("names",
                        new XElement("item", wallLayer1MaterialTyp4.Content));
                }
                else
                {
                    if (wallLayer3MaterialTyp4.Content.Equals(""))
                    {
                        wallMaterialsNames = new XElement("names",
                            new XElement("item", wallLayer1MaterialTyp4.Content),
                            new XElement("item", wallLayer2MaterialTyp4.Content));
                    }
                    else
                    {
                        if (wallLayer4MaterialTyp4.Content.Equals(""))
                        {

                            wallMaterialsNames = new XElement("names",
                                new XElement("item", wallLayer1MaterialTyp4.Content),
                                new XElement("item", wallLayer2MaterialTyp4.Content),
                                new XElement("item", wallLayer3MaterialTyp4.Content));
                        }
                        else
                        {
                            wallMaterialsNames = new XElement("names",
                                new XElement("item", wallLayer1MaterialTyp4.Content),
                                new XElement("item", wallLayer2MaterialTyp4.Content),
                                new XElement("item", wallLayer3MaterialTyp4.Content),
                                new XElement("item", wallLayer4MaterialTyp4.Content));
                        }
                    }
                }

                XElement wallMaterialsThermalConductivity;
                if (wallLayer2KTyp4.Content.Equals(""))
                {
                    wallMaterialsThermalConductivity = new XElement("thermalConductivity",
                        new XElement("item", wallLayer1KTyp4.Content));
                }
                else
                {
                    if (wallLayer3KTyp4.Content.Equals(""))
                    {
                        wallMaterialsThermalConductivity = new XElement("thermalConductivity",
                            new XElement("item", wallLayer1KTyp4.Content),
                            new XElement("item", wallLayer2KTyp4.Content));
                    }
                    else
                    {
                        if (wallLayer4KTyp4.Content.Equals(""))
                        {

                            wallMaterialsThermalConductivity = new XElement("thermalConductivity",
                                new XElement("item", wallLayer1KTyp4.Content),
                                new XElement("item", wallLayer2KTyp4.Content),
                                new XElement("item", wallLayer3KTyp4.Content));
                        }
                        else
                        {
                            wallMaterialsThermalConductivity = new XElement("thermalConductivity",
                                new XElement("item", wallLayer1KTyp4.Content),
                                new XElement("item", wallLayer2KTyp4.Content),
                                new XElement("item", wallLayer3KTyp4.Content),
                                new XElement("item", wallLayer4KTyp4.Content));
                        }
                    }
                }

                XElement wallMaterialsVolumetricHeatCapacity;
                if (wallLayer2VHCTyp4.Content.Equals(""))
                {
                    wallMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                        new XElement("item", wallLayer1VHCTyp4.Content));
                }
                else
                {
                    if (wallLayer3VHCTyp4.Equals(""))
                    {
                        wallMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                            new XElement("item", wallLayer1VHCTyp4.Content),
                            new XElement("item", wallLayer2VHCTyp4.Content));
                    }
                    else
                    {
                        if (wallLayer4VHCTyp4.Content.Equals(""))
                        {

                            wallMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                                new XElement("item", wallLayer1VHCTyp4.Content),
                                new XElement("item", wallLayer2VHCTyp4.Content),
                                new XElement("item", wallLayer3VHCTyp4.Content));
                        }
                        else
                        {
                            wallMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                                new XElement("item", wallLayer1VHCTyp4.Content),
                                new XElement("item", wallLayer2VHCTyp4.Content),
                                new XElement("item", wallLayer3VHCTyp4.Content),
                                new XElement("item", wallLayer4VHCTyp4.Content));
                        }
                    }
                }
                XElement wallMaterialsThickness;
                if (wallLayer2ThicknessTyp4.Content.Equals(""))
                {
                    wallMaterialsThickness = new XElement("thickness", "[" + wallLayer1ThicknessTyp4.Content + "]");
                }
                else
                {
                    if (wallLayer3ThicknessTyp4.Equals(""))
                    {
                        wallMaterialsThickness = new XElement("thickness", "[" + wallLayer1ThicknessTyp4.Content + "," + wallLayer2ThicknessTyp4.Content + "]");
                    }
                    else
                    {
                        if (wallLayer4ThicknessTyp4.Content.Equals(""))
                        {
                            wallMaterialsThickness = new XElement("thickness", "[" + wallLayer1ThicknessTyp4.Content + "," + wallLayer2ThicknessTyp4.Content + "," + wallLayer3ThicknessTyp4.Content + "]");
                        }
                        else
                        {
                            wallMaterialsThickness = new XElement("thickness", "[" + wallLayer1ThicknessTyp4.Content + "," + wallLayer2ThicknessTyp4.Content + "," + wallLayer3ThicknessTyp4.Content + "," + wallLayer4ThicknessTyp4.Content + "]");
                        }
                    }
                }

                //ROOF MATERIALS:
                XElement roofMaterialsNames;
                if (roofLayer2MaterialTyp4.Content.Equals(""))
                {
                    roofMaterialsNames = new XElement("names",
                        new XElement("item", roofLayer1MaterialTyp4.Content));
                }
                else
                {
                    if (roofLayer3MaterialTyp4.Equals(""))
                    {
                        roofMaterialsNames = new XElement("names",
                            new XElement("item", roofLayer1MaterialTyp4.Content),
                            new XElement("item", roofLayer2MaterialTyp4.Content));
                    }
                    else
                    {
                        roofMaterialsNames = new XElement("names",
                            new XElement("item", roofLayer1MaterialTyp4.Content),
                            new XElement("item", roofLayer2MaterialTyp4.Content),
                            new XElement("item", roofLayer3MaterialTyp4.Content));
                    }
                }

                XElement roofMaterialsThermalConductivity;
                if (roofLayer2KTyp4.Content.Equals(""))
                {
                    roofMaterialsThermalConductivity = new XElement("thermalConductivity",
                        new XElement("item", roofLayer1KTyp4.Content));
                }
                else
                {
                    if (roofLayer3KTyp4.Content.Equals(""))
                    {
                        roofMaterialsThermalConductivity = new XElement("thermalConductivity",
                            new XElement("item", roofLayer1KTyp4.Content),
                            new XElement("item", roofLayer2KTyp4.Content));
                    }
                    else
                    {
                        roofMaterialsThermalConductivity = new XElement("thermalConductivity",
                            new XElement("item", roofLayer1KTyp4.Content),
                            new XElement("item", roofLayer2KTyp4.Content),
                            new XElement("item", roofLayer3KTyp4.Content));
                    }
                }

                XElement roofMaterialsVolumetricHeatCapacity;
                if (roofLayer2VHCTyp4.Content.Equals(""))
                {
                    roofMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                        new XElement("item", roofLayer1VHCTyp4.Content));
                }
                else
                {
                    if (roofLayer3VHCTyp4.Content.Equals(""))
                    {
                        roofMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                            new XElement("item", roofLayer1VHCTyp4.Content),
                            new XElement("item", roofLayer2VHCTyp4.Content));
                    }
                    else
                    {
                        roofMaterialsVolumetricHeatCapacity = new XElement("volumetricHeatCapacity",
                            new XElement("item", roofLayer1VHCTyp4.Content),
                            new XElement("item", roofLayer2VHCTyp4.Content),
                            new XElement("item", roofLayer3VHCTyp4.Content));
                    }
                }
                XElement roofMaterialsThickness;
                if (roofLayer2ThicknessTyp4.Content.Equals(""))
                {
                    roofMaterialsThickness = new XElement("thickness", "[" + roofLayer1ThicknessTyp4.Content + "]");
                }
                else
                {
                    if (roofLayer3ThicknessTyp4.Content.Equals(""))
                    {
                        roofMaterialsThickness = new XElement("thickness", "[" + roofLayer1ThicknessTyp4.Content + "," + roofLayer2ThicknessTyp4.Content + "]");
                    }
                    else
                    {
                        roofMaterialsThickness = new XElement("thickness", "[" + roofLayer1ThicknessTyp4.Content + "," + roofLayer2ThicknessTyp4.Content + "," + roofLayer3ThicknessTyp4.Content + "]");
                    }
                }

                XElement elem = new XElement("typology4",
                                                    new XElement("dist", typology4Dist.Text), 
                                                    new XElement("construction",
                                                        new XElement("wall",
                                                            new XElement("albedo", wallAlbedoTyp4.Text),
                                                            new XElement("emissivity", wallEmissivityTyp4.Text),
                                                            new XElement("materials",
                                                                new XAttribute("name", wallboxTyp4.SelectedItem),
                                                                wallMaterialsNames,
                                                                wallMaterialsThermalConductivity,
                                                                wallMaterialsVolumetricHeatCapacity,
                                                                wallMaterialsThickness),
                                                            new XElement("vegetationCoverage", wallVegCoverageTyp4.Text),
                                                            new XElement("inclination", wallInclinationTyp4.Text),
                                                            new XElement("initialTemperature", 20)),

                                                        new XElement("roof",
                                                            new XElement("albedo", roofAlbedoTyp4.Text),
                                                            new XElement("emissivity", roofEmissivityTyp4.Text),
                                                            new XElement("materials",
                                                                new XAttribute("name", roofboxTyp4.SelectedItem),
                                                                roofMaterialsNames,
                                                                roofMaterialsThermalConductivity,
                                                                roofMaterialsVolumetricHeatCapacity,
                                                                roofMaterialsThickness),
                                                            new XElement("vegetationCoverage", roofVegCoverageTyp4.Text),
                                                            new XElement("inclination", roofInclinationTyp4.Text),
                                                            new XElement("initialTemperature", 20)),

                                                        new XElement("mass",
                                                            new XElement("albedo", massAlbedoTyp4.Content),
                                                                new XElement("emissivity", massEmissivityTyp4.Content),
                                                                new XElement("materials",
                                                                    new XAttribute("name", massboxTyp4.Text), 
                                                                    new XElement("names",
                                                                        new XElement("item", massLayer1MaterialTyp4.Content)),
                                                                    new XElement("thermalConductivity",
                                                                        new XElement("item", massLayer1KTyp4.Content)),
                                                                    new XElement("volumetricHeatCapacity",
                                                                        new XElement("item", massLayer1VHCTyp4.Content)),
                                                                    new XElement("thickness", "[" + massLayer1ThicknessTyp4.Content + "]")),
                                                                        new XElement("vegetationCoverage", 0),
                                                                        new XElement("inclination", 1),
                                                                         new XElement("initialTemperature", 20)),

                                                        new XElement("glazing",
                                                            new XAttribute("name", glazingboxTyp4.Text), 
                                                            new XElement("glazingRatio", wwrTyp4.Content),
                                                            new XElement("windowUvalue", uValueTyp4.Content),
                                                            new XElement("windowSHGC", SHGCTyp4.Content))
                                                    ),

                                                    //BUILDING GEOMETRY:
                                                    new XElement("building",
                                                        new XAttribute("name", heatbox.SelectedItem), 
                                                        new XElement("floorHeight", floorHeightTyp4.Text),
                                                        new XElement("dayInternalGains", dayInternalHeatGainTyp4.Content),
                                                        new XElement("nightInternalGains", nightInternalHeatGainTyp4.Content),
                                                        new XElement("radiantFraction", radiantFractionTyp4.Text),
                                                        new XElement("latentFraction", latentFractionTyp4.Text),
                                                        new XElement("infiltration", infiltrationTyp4.Content),
                                                        new XElement("ventilation", ventilationTyp4.Content),
                                                        new XElement("coolingSystemType", coolingSystemTypeTyp4.Text),
                                                        new XElement("coolingCOP", coolingCOPTyp4.Text),
                                                        new XElement("daytimeCoolingSetPoint", daytimeCoolingSetpointTyp4.Text),
                                                        new XElement("nighttimeCoolingSetPoint", nighttimeCoolingSetpointTyp4.Text),
                                                        new XElement("daytimeHeatingSetPoint", daytimeHeatingSetPointTyp4.Text),
                                                        new XElement("nighttimeHeatingSetPoint", nighttimeHeatingSetPointTyp4.Text),
                                                        new XElement("coolingCapacity", coolingCapacityTyp4.Text),
                                                        new XElement("heatingEfficiency", heatingEfficiencyTyp4.Text),
                                                        new XElement("nightSetStart", nightSetStartTyp4.Text),
                                                        new XElement("nightSetEnd", nightSetEndTyp4.Text),
                                                        new XElement("heatReleasedToCanyon", 0),
                                                        new XElement("initialT", 20)
                                                    )
                                            );
                elem.Add(perc);
                elem.Add(name);
                return elem;
            } //closes TRY

            catch (Exception error)
            {
                textBox1.Text = error.ToString();
                return null;
            }

        }

        private void startEditor(object sender, RoutedEventArgs e)
        {
            Editor edit = new Editor();
            try
            {
                edit.ShowDialog();
            }
            catch { };
            edit.Closed += new EventHandler(OnChange);
        }
        //START UWG2.0
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (epwFileNameRun == null)
                {
                    System.Windows.MessageBox.Show("Please select epw file");
                }     
                ProcessStartInfo startUWG = new ProcessStartInfo();

                //startUWG.FileName = "C:\\Users\\anakano\\Documents\\Research\\UWG2.1\\For_Installer\\UWGv2.0.exe";
                startUWG.FileName = "UWGEngine.exe";
                //make sure there is space in between each of the four inputs and that folder extension ends with \\
                startUWG.Arguments = epwPathRun + "\\ " + this.epwFileNameRun + " " + this.xmlUWGPathRun + "\\ " + this.xmlUWGFileNameRun;
                //startUWG.Arguments = this.epwPath + this.epwFileName + this.xmlPath + this.xmlFilename;
                Process.Start(startUWG);
                        
                //next line for sensitivity analysis 
                //i++;
            }

            catch (Exception error)
            {
                textBox1.Text = error.ToString();
            }
            //MessageBox.Show("UWG has finished running. Your urban weather file is created");
        }

        
        //SELECT EPW FILE FOR UWG
        private String epwPath = "";
        private String epwFileName = "";
        private String filename = "";



        private String xmlUWGPath = "1";
        private String xmlUWGFileName = "";
        private String filename_xmlUWG = "";

        //OPEN XML FILE FOR EDITING
        private void openXml_Click(object sender, RoutedEventArgs e)
        {
            string message = "Please make sure to save your current XML file before loading.";
            string captain = "UWG";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            var result = System.Windows.MessageBox.Show(message, captain, buttons);
            if (result == MessageBoxResult.Cancel) return;

            var provider = (XmlDataProvider)this.DataContext;
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.DefaultExt = ".xml";
            dialog.Filter = "XML files (.xml)|*.xml";
            if (!(bool)dialog.ShowDialog(this))
                return;
            
            provider.Source = new Uri(dialog.FileName, UriKind.Absolute);
            provider.Refresh();
            this.xmlPath = System.IO.Path.GetDirectoryName(dialog.FileName);
            this.xmlFileName = System.IO.Path.GetFileName(dialog.FileName);

            //parse xml to load existing xml
            XmlDocument parseXml = new XmlDocument();
            parseXml.Load(dialog.FileName);

            int numOfTypsInXML = 0;
            IEnumerator xmlEnum = parseXml.GetElementsByTagName("xml_input")[0].GetEnumerator();
            XmlNode typNode;
            int numThickPerTyp = 3;
            int startTyp2 = 0;
            int startTyp3 = 0;
            int startTyp4 = 0;

            int endTyp2 = numThickPerTyp;
            int endTyp3 = numThickPerTyp;
            int endTyp4 = numThickPerTyp;

            while (xmlEnum.MoveNext())
            {
                typNode = (XmlNode)xmlEnum.Current;
                if (typNode.Name == "typology1")
                {
                    loadTyp1 = true;
                    typology1Dist.Text = typNode.Attributes["dist"].Value.ToString();
                    typology1Type.Text = typNode.Attributes["name"].Value.ToString();
                    wallbox.Text = typNode.Attributes["name"].Value.ToString();
                    wallboxTyp2.Text = typNode.Attributes["name"].Value.ToString();
                    wallboxTyp3.Text = typNode.Attributes["name"].Value.ToString();
                    wallboxTyp4.Text = typNode.Attributes["name"].Value.ToString();
                    roofbox.Text = typNode.Attributes["name"].Value.ToString();
                    roofboxTyp2.Text = typNode.Attributes["name"].Value.ToString();
                    roofboxTyp3.Text = typNode.Attributes["name"].Value.ToString();
                    roofboxTyp4.Text = typNode.Attributes["name"].Value.ToString();
                    massbox.Text = typNode.Attributes["name"].Value.ToString();
                    massboxTyp2.Text = typNode.Attributes["name"].Value.ToString();
                    massboxTyp3.Text = typNode.Attributes["name"].Value.ToString();
                    massboxTyp4.Text = typNode.Attributes["name"].Value.ToString();
                    glazingbox.Text = typNode.Attributes["name"].Value.ToString();
                    glazingboxTyp2.Text = typNode.Attributes["name"].Value.ToString();
                    glazingboxTyp3.Text = typNode.Attributes["name"].Value.ToString();
                    glazingboxTyp4.Text = typNode.Attributes["name"].Value.ToString();
                    heatbox.Text = typNode.Attributes["name"].Value.ToString();
                    heatboxTyp2.Text = typNode.Attributes["name"].Value.ToString();
                    heatboxTyp3.Text = typNode.Attributes["name"].Value.ToString();
                    heatboxTyp4.Text = typNode.Attributes["name"].Value.ToString();
                    urbanRoadbox.Text = typNode.Attributes["name"].Value.ToString();
                    ruralbox.Text = typNode.Attributes["name"].Value.ToString();
                    
                    numOfTypsInXML++;
                    startTyp2 += numThickPerTyp;
                    startTyp3 += numThickPerTyp;
                    startTyp4 += numThickPerTyp;

                    endTyp2 += numThickPerTyp;
                    endTyp3 += numThickPerTyp;
                    endTyp4 += numThickPerTyp;

                    //make sure the combo box is loaded
                    XmlNodeList nodeListurban = parseXml.SelectNodes("/xml_input/urbanArea/urbanRoad/materials");
                    foreach (XmlNode myNode in nodeListurban)
                    {
                        urbanRoadbox.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    XmlNodeList nodeListrural = parseXml.SelectNodes("/xml_input/referenceSite/ruralRoad/materials");
                    foreach (XmlNode myNode in nodeListrural)
                    {
                        ruralbox.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    
                    XmlNodeList nodeListwall1 = parseXml.SelectNodes("/xml_input/typology1/construction/wall/materials");
                    foreach (XmlNode myNode in nodeListwall1)
                    {
                        wallbox.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    XmlNodeList nodeListroof1 = parseXml.SelectNodes("/xml_input/typology1/construction/roof/materials");
                    foreach (XmlNode myNode in nodeListroof1)
                    {
                        roofbox.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    XmlNodeList nodeListmass1 = parseXml.SelectNodes("/xml_input/typology1/construction/mass/materials");
                    foreach (XmlNode myNode in nodeListmass1)
                    {
                        massbox.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    XmlNodeList nodeListglazing1 = parseXml.SelectNodes("/xml_input/typology1/construction/glazing");
                    foreach (XmlNode myNode in nodeListglazing1)
                    {
                        glazingbox.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    XmlNodeList nodeListheat1 = parseXml.SelectNodes("/xml_input/typology1/building");
                    foreach (XmlNode myNode in nodeListheat1)
                    {
                        heatbox.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    //binding layer names
                    XmlNodeList nodeList = parseXml.SelectNodes("/xml_input/typology1/construction/wall/materials/names/item[1]");
                    foreach (XmlNode myNode in nodeList)
                    {
                        wallLayer1Material.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeList2 = parseXml.SelectNodes("/xml_input/typology1/construction/wall/materials/names/item[2]");
                    foreach (XmlNode myNode in nodeList2)
                    {
                        wallLayer2Material.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeList3 = parseXml.SelectNodes("/xml_input/typology1/construction/wall/materials/names/item[3]");
                    foreach (XmlNode myNode in nodeList3)
                    {
                        wallLayer3Material.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeList4 = parseXml.SelectNodes("/xml_input/typology1/construction/wall/materials/names/item[4]");
                    foreach (XmlNode myNode in nodeList4)
                    {
                        wallLayer4Material.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeListRoof = parseXml.SelectNodes("/xml_input/typology1/construction/roof/materials/names/item[1]");
                    foreach (XmlNode myNode in nodeListRoof)
                    {
                        roofLayer1Material.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeListRoof2 = parseXml.SelectNodes("/xml_input/typology1/construction/roof/materials/names/item[2]");
                    foreach (XmlNode myNode in nodeListRoof2)
                    {
                        roofLayer2Material.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeListRoof3 = parseXml.SelectNodes("/xml_input/typology1/construction/roof/materials/names/item[3]");
                    foreach (XmlNode myNode in nodeListRoof3)
                    {
                        roofLayer3Material.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeListMass = parseXml.SelectNodes("/xml_input/typology1/construction/mass/materials/names/item[1]");
                    foreach (XmlNode myNode in nodeListMass)
                    {
                        massLayer1Material.Content = myNode.InnerText;
                    } 
                    roofLayer1Material.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    roofLayer1Material.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    roofLayer1Material.Margin = new Thickness(8, 4, 4, 4);
                    roofLayer1Material.Padding = new Thickness(0, 4, 0, 0);
                    roofLayer2Material.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    roofLayer2Material.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    roofLayer2Material.Margin = new Thickness(8, 4, 4, 4);
                    roofLayer2Material.Padding = new Thickness(0, 4, 0, 0);
                    roofLayer3Material.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    roofLayer3Material.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    roofLayer3Material.Margin = new Thickness(8, 4, 4, 4);
                    roofLayer3Material.Padding = new Thickness(0, 4, 0, 0);

                }
                else if (typNode.Name == "typology2")
                {
                    loadTyp2 = true;
                    typology2Dist.Text = typNode.Attributes["dist"].Value.ToString();
                    typology2Type.Text = typNode.Attributes["name"].Value.ToString();
                    numOfTypsInXML++;
                    startTyp3 += numThickPerTyp;
                    startTyp4 += numThickPerTyp;

                    endTyp3 += numThickPerTyp;
                    endTyp4 += numThickPerTyp;
                    //make sure the combo box is loaded
                    XmlNodeList nodeListwall2 = parseXml.SelectNodes("/xml_input/typology2/construction/wall/materials");
                    foreach (XmlNode myNode in nodeListwall2)
                    {
                        wallboxTyp2.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    XmlNodeList nodeListroof2 = parseXml.SelectNodes("/xml_input/typology2/construction/roof/materials");
                    foreach (XmlNode myNode in nodeListroof2)
                    {
                        roofboxTyp2.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    XmlNodeList nodeListmass2 = parseXml.SelectNodes("/xml_input/typology2/construction/mass/materials");
                    foreach (XmlNode myNode in nodeListmass2)
                    {
                        massboxTyp2.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    XmlNodeList nodeListglazing2 = parseXml.SelectNodes("/xml_input/typology2/construction/glazing");
                    foreach (XmlNode myNode in nodeListglazing2)
                    {
                        glazingboxTyp2.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    XmlNodeList nodeListheat3 = parseXml.SelectNodes("/xml_input/typology3/building");
                    foreach (XmlNode myNode in nodeListheat3)
                    {
                        heatboxTyp3.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    //binding layer names
                    XmlNodeList nodeList = parseXml.SelectNodes("/xml_input/typology2/construction/wall/materials/names/item[1]");
                    foreach (XmlNode myNode in nodeList)
                    {
                        wallLayer1MaterialTyp2.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeList2 = parseXml.SelectNodes("/xml_input/typology2/construction/wall/materials/names/item[2]");
                    foreach (XmlNode myNode in nodeList2)
                    {
                        wallLayer2MaterialTyp2.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeList3 = parseXml.SelectNodes("/xml_input/typology2/construction/wall/materials/names/item[3]");
                    foreach (XmlNode myNode in nodeList3)
                    {
                        wallLayer3MaterialTyp2.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeList4 = parseXml.SelectNodes("/xml_input/typology2/construction/wall/materials/names/item[4]");
                    foreach (XmlNode myNode in nodeList4)
                    {
                        wallLayer4MaterialTyp2.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeListRoof = parseXml.SelectNodes("/xml_input/typology2/construction/roof/materials/names/item[1]");
                    foreach (XmlNode myNode in nodeListRoof)
                    {
                        roofLayer1MaterialTyp2.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeListRoof2 = parseXml.SelectNodes("/xml_input/typology2/construction/roof/materials/names/item[2]");
                    foreach (XmlNode myNode in nodeListRoof2)
                    {
                        roofLayer2MaterialTyp2.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeListRoof3 = parseXml.SelectNodes("/xml_input/typology2/construction/roof/materials/names/item[3]");
                    foreach (XmlNode myNode in nodeListRoof3)
                    {
                        roofLayer3MaterialTyp2.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeListMass = parseXml.SelectNodes("/xml_input/typology2/construction/mass/materials/names/item[1]");
                    foreach (XmlNode myNode in nodeListMass)
                    {
                        massLayer1MaterialTyp2.Content = myNode.InnerText;
                    }
                    roofLayer1MaterialTyp2.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    roofLayer1MaterialTyp2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    roofLayer1MaterialTyp2.Margin = new Thickness(8, 4, 4, 4);
                    roofLayer1MaterialTyp2.Padding = new Thickness(0, 4, 0, 0);
                    roofLayer2MaterialTyp2.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    roofLayer2MaterialTyp2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    roofLayer2MaterialTyp2.Margin = new Thickness(8, 4, 4, 4);
                    roofLayer2MaterialTyp2.Padding = new Thickness(0, 4, 0, 0);
                    roofLayer3MaterialTyp2.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    roofLayer3MaterialTyp2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    roofLayer3MaterialTyp2.Margin = new Thickness(8, 4, 4, 4);
                    roofLayer3MaterialTyp2.Padding = new Thickness(0, 4, 0, 0);
                }
                else if (typNode.Name == "typology3")
                {
                    loadTyp3 = true;
                    typology3Dist.Text = typNode.Attributes["dist"].Value.ToString();
                    typology3Type.Text = typNode.Attributes["name"].Value.ToString();
                    numOfTypsInXML++;
                    startTyp4 += numThickPerTyp;

                    endTyp4 += numThickPerTyp;
                    //make sure the combo box is loaded
                    XmlNodeList nodeListwall3 = parseXml.SelectNodes("/xml_input/typology3/construction/wall/materials");
                    foreach (XmlNode myNode in nodeListwall3)
                    {
                        wallboxTyp3.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    XmlNodeList nodeListroof3 = parseXml.SelectNodes("/xml_input/typology3/construction/roof/materials");
                    foreach (XmlNode myNode in nodeListroof3)
                    {
                        roofboxTyp3.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    XmlNodeList nodeListmass3 = parseXml.SelectNodes("/xml_input/typology3/construction/mass/materials");
                    foreach (XmlNode myNode in nodeListmass3)
                    {
                        massboxTyp3.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    XmlNodeList nodeListglazing3 = parseXml.SelectNodes("/xml_input/typology3/construction/glazing");
                    foreach (XmlNode myNode in nodeListglazing3)
                    {
                        glazingboxTyp3.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    XmlNodeList nodeListheat3 = parseXml.SelectNodes("/xml_input/typology3/building");
                    foreach (XmlNode myNode in nodeListheat3)
                    {
                        heatboxTyp3.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    //binding layer names
                    XmlNodeList nodeList = parseXml.SelectNodes("/xml_input/typology3/construction/wall/materials/names/item[1]");
                    foreach (XmlNode myNode in nodeList)
                    {
                        wallLayer1MaterialTyp3.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeList2 = parseXml.SelectNodes("/xml_input/typology3/construction/wall/materials/names/item[2]");
                    foreach (XmlNode myNode in nodeList2)
                    {
                        wallLayer2MaterialTyp3.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeList3 = parseXml.SelectNodes("/xml_input/typology3/construction/wall/materials/names/item[3]");
                    foreach (XmlNode myNode in nodeList3)
                    {
                        wallLayer3MaterialTyp3.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeList4 = parseXml.SelectNodes("/xml_input/typology3/construction/wall/materials/names/item[4]");
                    foreach (XmlNode myNode in nodeList4)
                    {
                        wallLayer4MaterialTyp3.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeListRoof = parseXml.SelectNodes("/xml_input/typology3/construction/roof/materials/names/item[1]");
                    foreach (XmlNode myNode in nodeListRoof)
                    {
                        roofLayer1MaterialTyp3.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeListRoof2 = parseXml.SelectNodes("/xml_input/typology3/construction/roof/materials/names/item[2]");
                    foreach (XmlNode myNode in nodeListRoof2)
                    {
                        roofLayer2MaterialTyp3.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeListRoof3 = parseXml.SelectNodes("/xml_input/typology3/construction/roof/materials/names/item[3]");
                    foreach (XmlNode myNode in nodeListRoof3)
                    {
                        roofLayer3MaterialTyp3.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeListMass = parseXml.SelectNodes("/xml_input/typology3/construction/mass/materials/names/item[1]");
                    foreach (XmlNode myNode in nodeListMass)
                    {
                        massLayer1MaterialTyp3.Content = myNode.InnerText;
                    }
                    roofLayer1MaterialTyp3.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    roofLayer1MaterialTyp3.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    roofLayer1MaterialTyp3.Margin = new Thickness(8, 4, 4, 4);
                    roofLayer1MaterialTyp3.Padding = new Thickness(0, 4, 0, 0);
                    roofLayer2MaterialTyp3.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    roofLayer2MaterialTyp3.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    roofLayer2MaterialTyp3.Margin = new Thickness(8, 4, 4, 4);
                    roofLayer2MaterialTyp3.Padding = new Thickness(0, 4, 0, 0);
                    roofLayer3MaterialTyp3.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    roofLayer3MaterialTyp3.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    roofLayer3MaterialTyp3.Margin = new Thickness(8, 4, 4, 4);
                    roofLayer3MaterialTyp3.Padding = new Thickness(0, 4, 0, 0);
                }
                else if (typNode.Name == "typology4")
                {
                    loadTyp4 = true;
                    typology4Dist.Text = typNode.Attributes["dist"].Value.ToString();
                    typology4Type.Text = typNode.Attributes["name"].Value.ToString();
                    numOfTypsInXML++;
                    //make sure the combo box is loaded
                    XmlNodeList nodeListwall4 = parseXml.SelectNodes("/xml_input/typology4/construction/wall/materials");
                    foreach (XmlNode myNode in nodeListwall4)
                    {
                        wallboxTyp4.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    XmlNodeList nodeListroof4 = parseXml.SelectNodes("/xml_input/typology4/construction/roof/materials");
                    foreach (XmlNode myNode in nodeListroof4)
                    {
                        roofboxTyp4.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    XmlNodeList nodeListmass4 = parseXml.SelectNodes("/xml_input/typology4/construction/mass/materials");
                    foreach (XmlNode myNode in nodeListmass4)
                    {
                        massboxTyp4.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    XmlNodeList nodeListglazing4 = parseXml.SelectNodes("/xml_input/typology4/construction/glazing");
                    foreach (XmlNode myNode in nodeListglazing4)
                    {
                        glazingboxTyp4.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    XmlNodeList nodeListheat4 = parseXml.SelectNodes("/xml_input/typology4/building");
                    foreach (XmlNode myNode in nodeListheat4)
                    {
                        heatboxTyp4.SelectedItem = myNode.Attributes["name"].Value;
                    }
                    //binding layer names
                    XmlNodeList nodeList = parseXml.SelectNodes("/xml_input/typology4/construction/wall/materials/names/item[1]");
                    foreach (XmlNode myNode in nodeList)
                    {
                        wallLayer1MaterialTyp4.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeList2 = parseXml.SelectNodes("/xml_input/typology4/construction/wall/materials/names/item[2]");
                    foreach (XmlNode myNode in nodeList2)
                    {
                        wallLayer2MaterialTyp4.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeList3 = parseXml.SelectNodes("/xml_input/typology4/construction/wall/materials/names/item[3]");
                    foreach (XmlNode myNode in nodeList3)
                    {
                        wallLayer3MaterialTyp4.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeList4 = parseXml.SelectNodes("/xml_input/typology4/construction/wall/materials/names/item[4]");
                    foreach (XmlNode myNode in nodeList4)
                    {
                        wallLayer4MaterialTyp4.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeListRoof = parseXml.SelectNodes("/xml_input/typology4/construction/roof/materials/names/item[1]");
                    foreach (XmlNode myNode in nodeListRoof)
                    {
                        roofLayer1MaterialTyp4.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeListRoof2 = parseXml.SelectNodes("/xml_input/typology4/construction/roof/materials/names/item[2]");
                    foreach (XmlNode myNode in nodeListRoof2)
                    {
                        roofLayer2MaterialTyp4.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeListRoof3 = parseXml.SelectNodes("/xml_input/typology4/construction/roof/materials/names/item[3]");
                    foreach (XmlNode myNode in nodeListRoof3)
                    {
                        roofLayer3MaterialTyp4.Content = myNode.InnerText;
                    }
                    XmlNodeList nodeListMass = parseXml.SelectNodes("/xml_input/typology4/construction/mass/materials/names/item[1]");
                    foreach (XmlNode myNode in nodeListMass)
                    {
                        massLayer1MaterialTyp4.Content = myNode.InnerText;
                    }
                    roofLayer1MaterialTyp4.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    roofLayer1MaterialTyp4.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    roofLayer1MaterialTyp4.Margin = new Thickness(8, 4, 4, 4);
                    roofLayer1MaterialTyp4.Padding = new Thickness(0, 4, 0, 0);
                    roofLayer2MaterialTyp4.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    roofLayer2MaterialTyp4.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    roofLayer2MaterialTyp4.Margin = new Thickness(8, 4, 4, 4);
                    roofLayer2MaterialTyp4.Padding = new Thickness(0, 4, 0, 0);
                    roofLayer3MaterialTyp4.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    roofLayer3MaterialTyp4.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    roofLayer3MaterialTyp4.Margin = new Thickness(8, 4, 4, 4);
                    roofLayer3MaterialTyp4.Padding = new Thickness(0, 4, 0, 0);
                }
            }
            XmlNodeList elemList = parseXml.GetElementsByTagName("thickness");

            if (loadTyp1 == true)
            {
                List<string> wall = new List<string>();
                List<string> roof = new List<string>();
                List<string> mass = new List<string>();
                List<string> urbanRoad = new List<string>();
                List<string> ruralRoad = new List<string>();
                for (int i = 0; i < numThickPerTyp; i++)
                {
                    //display all strings 
                    System.Diagnostics.Debug.WriteLine(elemList[i].InnerXml);
                    string[] element = elemList[i].InnerXml.Split(',');
                    foreach (string value in element)
                    {
                        // Wall
                        if (i == 0)
                        {
                            wall.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                        // Roof
                        if (i == 1)
                        {
                            roof.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                        // Mass
                        if (i == 2)
                        {
                            mass.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                    }
                }
                //wall
                int desiredIndex = 0;
                if (desiredIndex < wall.Count)
                {
                    wallLayer1Thickness.Content = wall[desiredIndex];
                }
                else { wallLayer1Thickness.Content = ""; }
                desiredIndex = 1;
                if (desiredIndex < wall.Count)
                {
                    wallLayer2Thickness.Content = wall[desiredIndex];
                }
                else { wallLayer2Thickness.Content = ""; }
                desiredIndex = 2;
                if (desiredIndex < wall.Count)
                {
                    wallLayer3Thickness.Content = wall[desiredIndex];
                }
                else { wallLayer3Thickness.Content = ""; }
                desiredIndex = 3;
                if (desiredIndex < wall.Count)
                {
                    wallLayer4Thickness.Content = wall[desiredIndex];
                }
                else { wallLayer4Thickness.Content = ""; }
                //roof
                int desiredIndexRoof = 0;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer1Thickness.Content = roof[desiredIndexRoof];
                }
                else { roofLayer1Thickness.Content = ""; }
                desiredIndexRoof = 1;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer2Thickness.Content = roof[desiredIndexRoof];
                }
                else { roofLayer2Thickness.Content = ""; }
                desiredIndexRoof = 2;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer3Thickness.Content = roof[desiredIndexRoof];
                }
                else { roofLayer3Thickness.Content = ""; }
                //mass
                System.Diagnostics.Debug.WriteLine(mass[0]);
                int desiredIndexMass = 0;
                if (desiredIndexMass < mass.Count)
                {
                    massLayer1Thickness.Content = mass[desiredIndexMass];
                }
            }

            if (loadTyp2 == true)
            {
                List<string> wall = new List<string>();
                List<string> roof = new List<string>();
                List<string> mass = new List<string>();
                List<string> urbanRoad = new List<string>();
                List<string> ruralRoad = new List<string>();
                for (int i = startTyp2; i < endTyp2; i++)
                {
                    //display all strings 
                    System.Diagnostics.Debug.WriteLine(elemList[i].InnerXml);
                    string[] element = elemList[i].InnerXml.Split(',');
                    foreach (string value in element)
                    {
                        // Wall
                        if (i == startTyp2)
                        {
                            wall.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                        // Roof
                        if (i == startTyp2 + 1)
                        {
                            roof.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                        // Mass
                        if (i == startTyp2 + 2)
                        {
                            mass.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                    }
                }
                //wall
                int desiredIndex = 0;
                if (desiredIndex < wall.Count)
                {
                    wallLayer1ThicknessTyp2.Content = wall[desiredIndex];
                }
                else { wallLayer1ThicknessTyp2.Content = ""; }
                desiredIndex = 1;
                if (desiredIndex < wall.Count)
                {
                    wallLayer2ThicknessTyp2.Content = wall[desiredIndex];
                }
                else { wallLayer2ThicknessTyp2.Content = ""; }
                desiredIndex = 2;
                if (desiredIndex < wall.Count)
                {
                    wallLayer3ThicknessTyp2.Content = wall[desiredIndex];
                }
                else { wallLayer3ThicknessTyp2.Content = ""; }
                desiredIndex = 3;
                if (desiredIndex < wall.Count)
                {
                    wallLayer4ThicknessTyp2.Content = wall[desiredIndex];
                }
                else { wallLayer4ThicknessTyp2.Content = ""; }
                //roof
                int desiredIndexRoof = 0;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer1ThicknessTyp2.Content = roof[desiredIndexRoof];
                }
                else { roofLayer1ThicknessTyp2.Content = ""; }
                desiredIndexRoof = 1;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer2ThicknessTyp2.Content = roof[desiredIndexRoof];
                }
                else { roofLayer2ThicknessTyp2.Content = ""; }
                desiredIndexRoof = 2;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer3ThicknessTyp2.Content = roof[desiredIndexRoof];
                }
                else { roofLayer3ThicknessTyp2.Content = ""; }
                //mass
                System.Diagnostics.Debug.WriteLine(mass[0]);
                int desiredIndexMass = 0;
                if (desiredIndexMass < mass.Count)
                {
                    massLayer1ThicknessTyp2.Content = mass[desiredIndexMass];
                }
            }

            if (loadTyp3 == true)
            {
                List<string> wall = new List<string>();
                List<string> roof = new List<string>();
                List<string> mass = new List<string>();
                List<string> urbanRoad = new List<string>();
                List<string> ruralRoad = new List<string>();
                for (int i = startTyp3; i < endTyp3; i++)
                {
                    //display all strings 
                    System.Diagnostics.Debug.WriteLine(elemList[i].InnerXml);
                    string[] element = elemList[i].InnerXml.Split(',');
                    foreach (string value in element)
                    {
                        // Wall
                        if (i == startTyp3)
                        {
                            wall.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                        // Roof
                        if (i == startTyp3 + 1)
                        {
                            roof.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                        // Mass
                        if (i == startTyp3 + 2)
                        {
                            mass.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                    }
                }
                //wall
                int desiredIndex = 0;
                if (desiredIndex < wall.Count)
                {
                    wallLayer1ThicknessTyp3.Content = wall[desiredIndex];
                }
                else { wallLayer1ThicknessTyp3.Content = ""; }
                desiredIndex = 1;
                if (desiredIndex < wall.Count)
                {
                    wallLayer2ThicknessTyp3.Content = wall[desiredIndex];
                }
                else { wallLayer2ThicknessTyp3.Content = ""; }
                desiredIndex = 2;
                if (desiredIndex < wall.Count)
                {
                    wallLayer3ThicknessTyp3.Content = wall[desiredIndex];
                }
                else { wallLayer3ThicknessTyp3.Content = ""; }
                desiredIndex = 3;
                if (desiredIndex < wall.Count)
                {
                    wallLayer4ThicknessTyp3.Content = wall[desiredIndex];
                }
                else { wallLayer4ThicknessTyp3.Content = ""; }
                //roof
                int desiredIndexRoof = 0;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer1ThicknessTyp3.Content = roof[desiredIndexRoof];
                }
                else { roofLayer1ThicknessTyp3.Content = ""; }
                desiredIndexRoof = 1;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer2ThicknessTyp3.Content = roof[desiredIndexRoof];
                }
                else { roofLayer2ThicknessTyp3.Content = ""; }
                desiredIndexRoof = 2;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer3ThicknessTyp3.Content = roof[desiredIndexRoof];
                }
                else { roofLayer3ThicknessTyp3.Content = ""; }
                //mass
                System.Diagnostics.Debug.WriteLine(mass[0]);
                int desiredIndexMass = 0;
                if (desiredIndexMass < mass.Count)
                {
                    massLayer1ThicknessTyp3.Content = mass[desiredIndexMass];
                }
            }

            if (loadTyp4 == true)
            {
                List<string> wall = new List<string>();
                List<string> roof = new List<string>();
                List<string> mass = new List<string>();
                List<string> urbanRoad = new List<string>();
                List<string> ruralRoad = new List<string>();
                for (int i = startTyp4; i < endTyp4; i++)
                {
                    //display all strings 
                    System.Diagnostics.Debug.WriteLine(elemList[i].InnerXml);
                    string[] element = elemList[i].InnerXml.Split(',');
                    foreach (string value in element)
                    {
                        // Wall
                        if (i == startTyp4)
                        {
                            wall.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                        // Roof
                        if (i == startTyp4 + 1)
                        {
                            roof.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                        // Mass
                        if (i == startTyp4 + 2)
                        {
                            mass.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                    }
                }
                //wall
                int desiredIndex = 0;
                if (desiredIndex < wall.Count)
                {
                    wallLayer1ThicknessTyp4.Content = wall[desiredIndex];
                }
                else { wallLayer1ThicknessTyp4.Content = ""; }
                desiredIndex = 1;
                if (desiredIndex < wall.Count)
                {
                    wallLayer2ThicknessTyp4.Content = wall[desiredIndex];
                }
                else { wallLayer2ThicknessTyp4.Content = ""; }
                desiredIndex = 2;
                if (desiredIndex < wall.Count)
                {
                    wallLayer3ThicknessTyp4.Content = wall[desiredIndex];
                }
                else { wallLayer3ThicknessTyp4.Content = ""; }
                desiredIndex = 3;
                if (desiredIndex < wall.Count)
                {
                    wallLayer4ThicknessTyp4.Content = wall[desiredIndex];
                }
                else { wallLayer4ThicknessTyp4.Content = ""; }
                //roof
                int desiredIndexRoof = 0;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer1ThicknessTyp4.Content = roof[desiredIndexRoof];
                }
                else { roofLayer1ThicknessTyp4.Content = ""; }
                desiredIndexRoof = 1;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer2ThicknessTyp4.Content = roof[desiredIndexRoof];
                }
                else { roofLayer2ThicknessTyp4.Content = ""; }
                desiredIndexRoof = 2;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer3ThicknessTyp4.Content = roof[desiredIndexRoof];
                }
                else { roofLayer3ThicknessTyp4.Content = ""; }
                //mass
                System.Diagnostics.Debug.WriteLine(mass[0]);
                int desiredIndexMass = 0;
                if (desiredIndexMass < mass.Count)
                {
                    massLayer1ThicknessTyp4.Content = mass[desiredIndexMass];
                }
            }
        }

        //New xml = default to default_input.xml
        private void newXml_Click(object sender, RoutedEventArgs e)
        {
            string message = "Please make sure to save your current XML file before continue.";
            string captain = "UWG";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            var result= System.Windows.MessageBox.Show(message, captain, buttons);
            if (result == MessageBoxResult.Cancel) return;
            this.xmlPath = "";
            this.xmlFileName = "";
            this.filename_xml = "";
            this.xmlFilePath = "";
            wallbox.SelectedIndex = 0;
            roofbox.SelectedIndex = 0;
            ruralbox.SelectedIndex = 0;
            urbanRoadbox.SelectedIndex = 0;
            glazingbox.SelectedIndex = 0;
            massbox.SelectedIndex = 0;
            heatbox.SelectedIndex = 0;

            wallboxTyp2.SelectedIndex = 0;
            roofboxTyp2.SelectedIndex = 0;
            //ruralboxTyp2.SelectedIndex = 0;
            //urbanRoadboxTyp2.SelectedIndex = 0;
            glazingboxTyp2.SelectedIndex = 0;
            massboxTyp2.SelectedIndex = 0;
            heatboxTyp2.SelectedIndex = 0;

            wallboxTyp3.SelectedIndex = 0;
            roofboxTyp3.SelectedIndex = 0;
            //ruralboxTyp3.SelectedIndex = 0;
            //urbanRoadboxTyp3.SelectedIndex = 0;
            glazingboxTyp3.SelectedIndex = 0;
            massboxTyp3.SelectedIndex = 0;
            heatboxTyp3.SelectedIndex = 0;
            
            wallboxTyp4.SelectedIndex = 0;
            roofboxTyp4.SelectedIndex = 0;
            //ruralboxTyp4.SelectedIndex = 0;
            //urbanRoadboxTyp4.SelectedIndex = 0;
            glazingboxTyp4.SelectedIndex = 0;
            massboxTyp4.SelectedIndex = 0;
            heatboxTyp4.SelectedIndex = 0;

            typology1Dist.Text = "0";
            typology2Dist.Text = "0";
            typology3Dist.Text = "0";
            typology4Dist.Text = "0";

            typology1Type.Text = "";
            typology2Type.Text = "";
            typology3Type.Text = "";
            typology4Type.Text = "";

            var provider = (XmlDataProvider)this.DataContext;
            provider.Source = new Uri("default_input.xml", UriKind.RelativeOrAbsolute);
            provider.Refresh();
            //parse xml for new default xml

                /*for (int i=0; i < elemList_new.Count; i++)
            {
                //display all strings 
                System.Diagnostics.Debug.WriteLine(elemList_new[i].InnerXml);
                string[] element_new = elemList_new[i].InnerXml.Split(',');
                foreach (string value in element_new)
                {
                    urbanRoad.Content = (value.Trim(new Char[] { '[', ']' }));
                    rural.Content = (value.Trim(new Char[] { '[', ']' }));
                    System.Diagnostics.Debug.WriteLine(value.Trim(new Char[] { '[', ']' }));
                }
            }*/
            this.xmlPath = System.IO.Path.GetDirectoryName("default_input.xml");
            this.xmlFileName = System.IO.Path.GetFileName("default_input.xml");

            //parse xml to load existing xml
            XmlDocument parseXml = new XmlDocument();
            parseXml.Load("default_input.xml");

            int numOfTypsInXML = 0;
            IEnumerator xmlEnum = parseXml.GetElementsByTagName("xml_input")[0].GetEnumerator();
            XmlNode typNode;
            int numThickPerTyp = 4;
            bool loadTyp1 = false;
            bool loadTyp2 = false;
            bool loadTyp3 = false;
            bool loadTyp4 = false;
            int startTyp2 = 0;
            int startTyp3 = 0;
            int startTyp4 = 0;

            int endTyp2 = numThickPerTyp;
            int endTyp3 = numThickPerTyp;
            int endTyp4 = numThickPerTyp;

            while (xmlEnum.MoveNext())
            {
                typNode = (XmlNode)xmlEnum.Current;
                if (typNode.Name == "typology1")
                {
                    loadTyp1 = true;
                    typology1Dist.Text = typNode.Attributes["dist"].Value.ToString();
                    typology1Type.Text = typNode.Attributes["name"].Value.ToString();
                    numOfTypsInXML++;
                    startTyp2 += numThickPerTyp;
                    startTyp3 += numThickPerTyp;
                    startTyp4 += numThickPerTyp;

                    endTyp2 += numThickPerTyp;
                    endTyp3 += numThickPerTyp;
                    endTyp4 += numThickPerTyp;
                }
                else if (typNode.Name == "typology2")
                {
                    loadTyp2 = true;
                    typology2Dist.Text = typNode.Attributes["dist"].Value.ToString();
                    typology2Type.Text = typNode.Attributes["name"].Value.ToString();
                    numOfTypsInXML++;
                    startTyp3 += numThickPerTyp;
                    startTyp4 += numThickPerTyp;

                    endTyp3 += numThickPerTyp;
                    endTyp4 += numThickPerTyp;

                }
                else if (typNode.Name == "typology3")
                {
                    loadTyp3 = true;
                    typology3Dist.Text = typNode.Attributes["dist"].Value.ToString();
                    typology3Type.Text = typNode.Attributes["name"].Value.ToString();
                    numOfTypsInXML++;
                    startTyp4 += numThickPerTyp;

                    endTyp4 += numThickPerTyp;
                }
                else if (typNode.Name == "typology4")
                {
                    loadTyp4 = true;
                    typology4Dist.Text = typNode.Attributes["dist"].Value.ToString();
                    typology4Type.Text = typNode.Attributes["name"].Value.ToString();
                    numOfTypsInXML++;
                }

            }

            XmlNodeList elemList = parseXml.GetElementsByTagName("thickness");
            

            if (loadTyp1 == true)
            {
                List<string> wall = new List<string>();
                List<string> roof = new List<string>();
                List<string> mass = new List<string>();
                List<string> urbanRoad = new List<string>();
                List<string> ruralRoad = new List<string>();
                for (int i = 0; i < numThickPerTyp; i++)
                {
                    //display all strings 
                    System.Diagnostics.Debug.WriteLine(elemList[i].InnerXml);
                    string[] element = elemList[i].InnerXml.Split(',');
                    foreach (string value in element)
                    {
                        // Wall
                        if (i == 0)
                        {
                            wall.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                        // Roof
                        if (i == 1)
                        {
                            roof.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                        // Mass
                        if (i == 2)
                        {
                            mass.Add(value.Trim(new Char[] { '[', ']' }));
                        }

                        //// Urban Road
                        //if (i == 3)
                        //{
                        //    urbanRoad.Add(value.Trim(new Char[] { '[', ']' }));
                        //}
                        //// Rural
                        //if (i == 4)
                        //{
                        //    ruralRoad.Add(value.Trim(new Char[] { '[', ']' }));
                        //}
                        //System.Diagnostics.Debug.WriteLine(value.Trim(new Char[] { '[', ']' }));
                    }
                }
                //wall
                int desiredIndex = 0;
                if (desiredIndex < wall.Count)
                {
                    wallLayer1Thickness.Content = wall[desiredIndex];
                }
                else { wallLayer1Thickness.Content = ""; }
                desiredIndex = 1;
                if (desiredIndex < wall.Count)
                {
                    wallLayer2Thickness.Content = wall[desiredIndex];
                }
                else { wallLayer2Thickness.Content = ""; }
                desiredIndex = 2;
                if (desiredIndex < wall.Count)
                {
                    wallLayer3Thickness.Content = wall[desiredIndex];
                }
                else { wallLayer3Thickness.Content = ""; }
                desiredIndex = 3;
                if (desiredIndex < wall.Count)
                {
                    wallLayer4Thickness.Content = wall[desiredIndex];
                }
                else { wallLayer4Thickness.Content = ""; }
                //roof
                int desiredIndexRoof = 0;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer1Thickness.Content = roof[desiredIndexRoof];
                }
                else { roofLayer1Thickness.Content = ""; }
                desiredIndexRoof = 1;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer2Thickness.Content = roof[desiredIndexRoof];
                }
                else { roofLayer2Thickness.Content = ""; }
                desiredIndexRoof = 2;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer3Thickness.Content = roof[desiredIndexRoof];
                }
                else { roofLayer3Thickness.Content = ""; }
                //mass
                System.Diagnostics.Debug.WriteLine(mass[0]);
                int desiredIndexMass = 0;
                if (desiredIndexMass < mass.Count)
                {
                    massLayer1Thickness.Content = mass[desiredIndexMass];
                }
                ////urbanRoad
                //int desiredIndexUrban = 0;
                //if (desiredIndexUrban < urbanRoad.Count)
                //{
                //    urbanRoadThickness.Content = urbanRoad[desiredIndexUrban];
                //}
                ////Rural
                //System.Diagnostics.Debug.WriteLine(ruralRoad[0]);
                //int desiredIndexRural = 0;
                //if (desiredIndexRural < ruralRoad.Count)
                //{
                //    ruralRoadThickness.Content = ruralRoad[desiredIndexRural];
                //}
            }

            if (loadTyp2 == true)
            {
                List<string> wall = new List<string>();
                List<string> roof = new List<string>();
                List<string> mass = new List<string>();
                List<string> urbanRoad = new List<string>();
                List<string> ruralRoad = new List<string>();
                for (int i = startTyp2; i < endTyp2 + 1; i++)
                {
                    //display all strings 
                    System.Diagnostics.Debug.WriteLine(elemList[i].InnerXml);
                    string[] element = elemList[i].InnerXml.Split(',');
                    foreach (string value in element)
                    {
                        // Wall
                        if (i == startTyp2)
                        {
                            wall.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                        // Roof
                        if (i == startTyp2 + 1)
                        {
                            roof.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                        // Mass
                        if (i == startTyp2 + 2)
                        {
                            mass.Add(value.Trim(new Char[] { '[', ']' }));
                        }

                        //// Urban Road
                        //if (i == startTyp2 + 3)
                        //{
                        //    urbanRoad.Add(value.Trim(new Char[] { '[', ']' }));
                        //}
                        //// Rural
                        //if (i == startTyp2 + 4)
                        //{
                        //    ruralRoad.Add(value.Trim(new Char[] { '[', ']' }));
                        //}
                        //System.Diagnostics.Debug.WriteLine(value.Trim(new Char[] { '[', ']' }));
                    }
                }
                //wall
                int desiredIndex = 0;
                if (desiredIndex < wall.Count)
                {
                    wallLayer1ThicknessTyp2.Content = wall[desiredIndex];
                }
                else { wallLayer1ThicknessTyp2.Content = ""; }
                desiredIndex = 1;
                if (desiredIndex < wall.Count)
                {
                    wallLayer2ThicknessTyp2.Content = wall[desiredIndex];
                }
                else { wallLayer2ThicknessTyp2.Content = ""; }
                desiredIndex = 2;
                if (desiredIndex < wall.Count)
                {
                    wallLayer3ThicknessTyp2.Content = wall[desiredIndex];
                }
                else { wallLayer3ThicknessTyp2.Content = ""; }
                desiredIndex = 3;
                if (desiredIndex < wall.Count)
                {
                    wallLayer4ThicknessTyp2.Content = wall[desiredIndex];
                }
                else { wallLayer4ThicknessTyp2.Content = ""; }
                //roof
                int desiredIndexRoof = 0;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer1ThicknessTyp2.Content = roof[desiredIndexRoof];
                }
                else { roofLayer1ThicknessTyp2.Content = ""; }
                desiredIndexRoof = 1;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer2ThicknessTyp2.Content = roof[desiredIndexRoof];
                }
                else { roofLayer2ThicknessTyp2.Content = ""; }
                desiredIndexRoof = 2;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer3ThicknessTyp2.Content = roof[desiredIndexRoof];
                }
                else { roofLayer3ThicknessTyp2.Content = ""; }
                //mass
                System.Diagnostics.Debug.WriteLine(mass[0]);
                int desiredIndexMass = 0;
                if (desiredIndexMass < mass.Count)
                {
                    massLayer1ThicknessTyp2.Content = mass[desiredIndexMass];
                }
                ////urbanRoad
                //int desiredIndexUrban = 0;
                //if (desiredIndexUrban < urbanRoad.Count)
                //{
                //    urbanRoadThicknessTyp2.Content = urbanRoad[desiredIndexUrban];
                //}
                ////Rural
                //System.Diagnostics.Debug.WriteLine(ruralRoad[0]);
                //int desiredIndexRural = 0;
                //if (desiredIndexRural < ruralRoad.Count)
                //{
                //    ruralRoadThicknessTyp2.Content = ruralRoad[desiredIndexRural];
                //}
            }

            if (loadTyp3 == true)
            {
                List<string> wall = new List<string>();
                List<string> roof = new List<string>();
                List<string> mass = new List<string>();
                List<string> urbanRoad = new List<string>();
                List<string> ruralRoad = new List<string>();
                for (int i = startTyp3; i < endTyp3 + 1; i++)
                {
                    //display all strings 
                    System.Diagnostics.Debug.WriteLine(elemList[i].InnerXml);
                    string[] element = elemList[i].InnerXml.Split(',');
                    foreach (string value in element)
                    {
                        // Wall
                        if (i == startTyp3)
                        {
                            wall.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                        // Roof
                        if (i == startTyp3 + 1)
                        {
                            roof.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                        // Mass
                        if (i == startTyp3 + 2)
                        {
                            mass.Add(value.Trim(new Char[] { '[', ']' }));
                        }

                        // Urban Road
                        //if (i == startTyp3 + 3)
                        //{
                        //    urbanRoad.Add(value.Trim(new Char[] { '[', ']' }));
                        //}
                        //// Rural
                        //if (i == startTyp3 + 4)
                        //{
                        //    ruralRoad.Add(value.Trim(new Char[] { '[', ']' }));
                        //}
                        //System.Diagnostics.Debug.WriteLine(value.Trim(new Char[] { '[', ']' }));
                    }
                }
                //wall
                int desiredIndex = 0;
                if (desiredIndex < wall.Count)
                {
                    wallLayer1ThicknessTyp3.Content = wall[desiredIndex];
                }
                else { wallLayer1ThicknessTyp3.Content = ""; }
                desiredIndex = 1;
                if (desiredIndex < wall.Count)
                {
                    wallLayer2ThicknessTyp3.Content = wall[desiredIndex];
                }
                else { wallLayer2ThicknessTyp3.Content = ""; }
                desiredIndex = 2;
                if (desiredIndex < wall.Count)
                {
                    wallLayer3ThicknessTyp3.Content = wall[desiredIndex];
                }
                else { wallLayer3ThicknessTyp3.Content = ""; }
                desiredIndex = 3;
                if (desiredIndex < wall.Count)
                {
                    wallLayer4ThicknessTyp3.Content = wall[desiredIndex];
                }
                else { wallLayer4ThicknessTyp3.Content = ""; }
                //roof
                int desiredIndexRoof = 0;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer1ThicknessTyp3.Content = roof[desiredIndexRoof];
                }
                else { roofLayer1ThicknessTyp3.Content = ""; }
                desiredIndexRoof = 1;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer2ThicknessTyp3.Content = roof[desiredIndexRoof];
                }
                else { roofLayer2ThicknessTyp3.Content = ""; }
                desiredIndexRoof = 2;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer3ThicknessTyp3.Content = roof[desiredIndexRoof];
                }
                else { roofLayer3ThicknessTyp3.Content = ""; }
                //mass
                System.Diagnostics.Debug.WriteLine(mass[0]);
                int desiredIndexMass = 0;
                if (desiredIndexMass < mass.Count)
                {
                    massLayer1ThicknessTyp3.Content = mass[desiredIndexMass];
                }
                ////urbanRoad
                //int desiredIndexUrban = 0;
                //if (desiredIndexUrban < urbanRoad.Count)
                //{
                //    urbanRoadThicknessTyp3.Content = urbanRoad[desiredIndexUrban];
                //}
                ////Rural
                //System.Diagnostics.Debug.WriteLine(ruralRoad[0]);
                //int desiredIndexRural = 0;
                //if (desiredIndexRural < ruralRoad.Count)
                //{
                //    ruralRoadThicknessTyp3.Content = ruralRoad[desiredIndexRural];
                //}
            }

            if (loadTyp4 == true)
            {
                List<string> wall = new List<string>();
                List<string> roof = new List<string>();
                List<string> mass = new List<string>();
                List<string> urbanRoad = new List<string>();
                List<string> ruralRoad = new List<string>();
                for (int i = startTyp4; i < endTyp4 + 1; i++)
                {
                    //display all strings 
                    System.Diagnostics.Debug.WriteLine(elemList[i].InnerXml);
                    string[] element = elemList[i].InnerXml.Split(',');
                    foreach (string value in element)
                    {
                        // Wall
                        if (i == startTyp4)
                        {
                            wall.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                        // Roof
                        if (i == startTyp4 + 1)
                        {
                            roof.Add(value.Trim(new Char[] { '[', ']' }));
                        }
                        // Mass
                        if (i == startTyp4 + 2)
                        {
                            mass.Add(value.Trim(new Char[] { '[', ']' }));
                        }

                        //// Urban Road
                        //if (i == startTyp4 + 3)
                        //{
                        //    urbanRoad.Add(value.Trim(new Char[] { '[', ']' }));
                        //}
                        //// Rural
                        //if (i == startTyp4 + 4)
                        //{
                        //    ruralRoad.Add(value.Trim(new Char[] { '[', ']' }));
                        //}
                        //System.Diagnostics.Debug.WriteLine(value.Trim(new Char[] { '[', ']' }));
                    }
                }
                //wall
                int desiredIndex = 0;
                if (desiredIndex < wall.Count)
                {
                    wallLayer1ThicknessTyp4.Content = wall[desiredIndex];
                }
                else { wallLayer1ThicknessTyp4.Content = ""; }
                desiredIndex = 1;
                if (desiredIndex < wall.Count)
                {
                    wallLayer2ThicknessTyp4.Content = wall[desiredIndex];
                }
                else { wallLayer2ThicknessTyp4.Content = ""; }
                desiredIndex = 2;
                if (desiredIndex < wall.Count)
                {
                    wallLayer3ThicknessTyp4.Content = wall[desiredIndex];
                }
                else { wallLayer3ThicknessTyp4.Content = ""; }
                desiredIndex = 3;
                if (desiredIndex < wall.Count)
                {
                    wallLayer4ThicknessTyp4.Content = wall[desiredIndex];
                }
                else { wallLayer4ThicknessTyp4.Content = ""; }
                //roof
                int desiredIndexRoof = 0;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer1ThicknessTyp4.Content = roof[desiredIndexRoof];
                }
                else { roofLayer1ThicknessTyp4.Content = ""; }
                desiredIndexRoof = 1;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer2ThicknessTyp4.Content = roof[desiredIndexRoof];
                }
                else { roofLayer2ThicknessTyp4.Content = ""; }
                desiredIndexRoof = 2;
                if (desiredIndexRoof < roof.Count)
                {
                    roofLayer3ThicknessTyp4.Content = roof[desiredIndexRoof];
                }
                else { roofLayer3ThicknessTyp4.Content = ""; }
                //mass
                System.Diagnostics.Debug.WriteLine(mass[0]);
                int desiredIndexMass = 0;
                if (desiredIndexMass < mass.Count)
                {
                    massLayer1ThicknessTyp4.Content = mass[desiredIndexMass];
                }
                ////urbanRoad
                //int desiredIndexUrban = 0;
                //if (desiredIndexUrban < urbanRoad.Count)
                //{
                //    urbanRoadThicknessTyp4.Content = urbanRoad[desiredIndexUrban];
                //}
                ////Rural
                //System.Diagnostics.Debug.WriteLine(ruralRoad[0]);
                //int desiredIndexRural = 0;
                //if (desiredIndexRural < ruralRoad.Count)
                //{
                //    ruralRoadThickness.Content = ruralRoad[desiredIndexRural];
                //}
            }

            this.xmlPath = "";
            this.xmlFileName = "";
            this.filename_xml = "";
            this.xmlFilePath = "";
        }

        //handle Hyperlinks 
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        //Go to UWG website for param descriptions
        private void uwgWebsite_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://urbanmicroclimate.scripts.mit.edu/uwg_parameters.php");
        }

        //copied from RunWindow.xaml.cs

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            numberOfSimulation++;
            minus.IsEnabled = true;
            if(numberOfSimulation==2)
            {
                rs1.Visibility = System.Windows.Visibility.Visible;
                bb1.Visibility = System.Windows.Visibility.Visible;
                bx1.Visibility = System.Windows.Visibility.Visible;
                sim2FNLabel.Visibility = System.Windows.Visibility.Visible;
            }
            if (numberOfSimulation == 3)
            {
                rs2.Visibility = System.Windows.Visibility.Visible;
                bb2.Visibility = System.Windows.Visibility.Visible;
                bx2.Visibility = System.Windows.Visibility.Visible;
                sim3FNLabel.Visibility = System.Windows.Visibility.Visible;
            }
            if (numberOfSimulation == 4)
            {
                rs3.Visibility = System.Windows.Visibility.Visible;
                bb3.Visibility = System.Windows.Visibility.Visible;
                bx3.Visibility = System.Windows.Visibility.Visible;
                sim4FNLabel.Visibility = System.Windows.Visibility.Visible;
            }
            if (numberOfSimulation == 5)
            {
                rs4.Visibility = System.Windows.Visibility.Visible;
                bb4.Visibility = System.Windows.Visibility.Visible;
                bx4.Visibility = System.Windows.Visibility.Visible;
                sim5FNLabel.Visibility = System.Windows.Visibility.Visible;
            }
            if (numberOfSimulation == 6)
            {
                rs5.Visibility = System.Windows.Visibility.Visible;
                bb5.Visibility = System.Windows.Visibility.Visible;
                bx5.Visibility = System.Windows.Visibility.Visible;
                sim6FNLabel.Visibility = System.Windows.Visibility.Visible;
                plus.IsEnabled = false;
            }
            check();
        }
        private void minus_Click(object sender, RoutedEventArgs e)
        {
            plus.IsEnabled = true;
            if (numberOfSimulation == 2)
            {
                rs1.Visibility = System.Windows.Visibility.Collapsed;
                bb1.Visibility = System.Windows.Visibility.Collapsed;
                bx1.Visibility = System.Windows.Visibility.Collapsed;
                sim2FNLabel.Visibility = System.Windows.Visibility.Collapsed;
                minus.IsEnabled = false;
            }
            if (numberOfSimulation == 3)
            {
                rs2.Visibility = System.Windows.Visibility.Collapsed;
                bb2.Visibility = System.Windows.Visibility.Collapsed;
                bx2.Visibility = System.Windows.Visibility.Collapsed;
                sim3FNLabel.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (numberOfSimulation == 4)
            {
                rs3.Visibility = System.Windows.Visibility.Collapsed;
                bb3.Visibility = System.Windows.Visibility.Collapsed;
                bx3.Visibility = System.Windows.Visibility.Collapsed;
                sim4FNLabel.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (numberOfSimulation == 5)
            {
                rs4.Visibility = System.Windows.Visibility.Collapsed;
                bb4.Visibility = System.Windows.Visibility.Collapsed;
                bx4.Visibility = System.Windows.Visibility.Collapsed;
                sim5FNLabel.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (numberOfSimulation == 6)
            {
                rs5.Visibility = System.Windows.Visibility.Collapsed;
                bb5.Visibility = System.Windows.Visibility.Collapsed;
                bx5.Visibility = System.Windows.Visibility.Collapsed;
                sim6FNLabel.Visibility = System.Windows.Visibility.Collapsed;
            }
            numberOfSimulation--;
            check();
        }
        private void check_(object sender, RoutedEventArgs e)
        {
            check();
        }
        private void back(object sender, EventArgs e)
        {
            this.IsEnabled = true;
        }
        private void startUWG_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String mon = p.simuStartMonthValidate;
                String day = p.simuStartDayValidate;
                String dur = p.simuDurationValidate;

                feedBack feed = new feedBack();
                feed.runNum = numberOfSimulation;
                feed.ep = this.epwPathRun;
                feed.ef = this.epwFileNameRun;
                feed.ef1 = "$1" + this.epwFileNameRun;
                feed.ef2 = "$2" + this.epwFileNameRun;
                feed.ef3 = "$3" + this.epwFileNameRun;
                feed.ef4 = "$4" + this.epwFileNameRun;
                feed.ef5 = "$5" + this.epwFileNameRun;
                feed.xp = this.xmlUWGPathRun;
                feed.xf = this.xmlUWGFileNameRun;
                feed.xp1 = this.xmlUWGPath1;
                feed.xf1 = this.xmlUWGFileName1;
                feed.xp2 = this.xmlUWGPath2;
                feed.xf2 = this.xmlUWGFileName2;
                feed.xp3 = this.xmlUWGPath3;
                feed.xf3 = this.xmlUWGFileName3;
                feed.xp4 = this.xmlUWGPath4;
                feed.xf4 = this.xmlUWGFileName4;
                feed.xp5 = this.xmlUWGPath5;
                feed.xf5 = this.xmlUWGFileName5;
                feed.day = day;
                feed.mon = mon;
                feed.dur = dur;
                feed.rp = resultPathText.Text;
                resultPath = resultPathText.Text;
                feed.rf = resultName.Text + ".epw";
                feed.rf1 = resultName1.Text + ".epw";
                feed.rf2 = resultName2.Text + ".epw";
                feed.rf3 = resultName3.Text + ".epw";
                feed.rf4 = resultName4.Text + ".epw";
                feed.rf5 = resultName5.Text + ".epw";
                feed.Show();
                feed.startrun();
                if(numberOfSimulation > 1)
                {
                    System.IO.File.Delete(System.IO.Path.Combine(feed.ep, feed.ef1));
                    System.IO.File.Copy(System.IO.Path.Combine(feed.ep, feed.ef), System.IO.Path.Combine(feed.ep, feed.ef1));
                    feed.startrun1();
                    //System.IO.Path.Combine(feed.ep, feed.ef1) is where the epw for simulation 1 is saved
                    //we want to send that to UTCI to get the output file
                }
                if (numberOfSimulation > 2)
                {
                    System.IO.File.Delete(System.IO.Path.Combine(feed.ep, feed.ef2));
                    System.IO.File.Copy(System.IO.Path.Combine(feed.ep, feed.ef), System.IO.Path.Combine(feed.ep, feed.ef2));
                    feed.startrun2();
                }
                if (numberOfSimulation > 3)
                {
                    System.IO.File.Delete(System.IO.Path.Combine(feed.ep, feed.ef3));
                    System.IO.File.Copy(System.IO.Path.Combine(feed.ep, feed.ef), System.IO.Path.Combine(feed.ep, feed.ef3));
                    feed.startrun3();
                }
                if (numberOfSimulation > 4)
                {
                    System.IO.File.Delete(System.IO.Path.Combine(feed.ep, feed.ef4));
                    System.IO.File.Copy(System.IO.Path.Combine(feed.ep, feed.ef), System.IO.Path.Combine(feed.ep, feed.ef4));
                    feed.startrun4();
                }
                if (numberOfSimulation > 5)
                {
                    System.IO.File.Delete(System.IO.Path.Combine(feed.ep, feed.ef5));
                    System.IO.File.Copy(System.IO.Path.Combine(feed.ep, feed.ef), System.IO.Path.Combine(feed.ep, feed.ef5));
                    feed.startrun5();
                }
                feed.Closed += new EventHandler(back);
          //      ProcessStartInfo startUWG = new ProcessStartInfo();
          //      //startUWG.FileName = "C:\\Users\\anakano\\Documents\\Research\\UWG2.1\\For_Installer\\UWGv2.0.exe";
          //      startUWG.FileName = "UWGEngine.exe";
          //      //make sure there is space in between each of the four inputs and that folder extension ends with \\
          //      startUWG.Arguments = this.epwPath + "\\ " + this.epwFileName + " " + this.xmlUWGPath + "\\ " + this.xmlUWGFileName + " " + this.resultPath + "\\ " + this.resultFileName + " " + mon + " " + day + " " + dur;
          //      //startUWG.Arguments = this.epwPath + this.epwFileName + this.xmlPath + this.xmlFilename;
          //      startUWG.UseShellExecute = false;
          //      startUWG.RedirectStandardOutput = true;
          //      UWGAbort.IsEnabled = true;
          //      UWGStart.IsEnabled = false;
          //      Process UWGs = Process.Start(startUWG);
          //      StreamReader UWGreader=UWGs.StandardOutput;  
          //      String UWGreaderst = UWGreader.ReadLine();
          //      RunningInfo.Text = UWGreaderst;
            }

            catch (Exception error)
            {
                textBoxRun.Text = error.ToString();
            }
            //MessageBox.Show("UWG has finished running. Your urban weather file is created");
        }

        //SELECT EPW FILE FOR UWG

        // Create OpenFileDialog
        private void browse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".epw";
            dlg.Filter = "Weather Files (.epw)|*.epw";
            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                filenameRun = dlg.FileName;
                //epwFileEmpty.Text = filenameRun;
                //Get directory path only
                this.epwPathRun = System.IO.Path.GetDirectoryName(filenameRun);
                //Get only the file name
                this.epwFileNameRun = System.IO.Path.GetFileName(filenameRun);
                epwFileEmpty.Text = epwFileNameRun;
                check();
            };
        }

        //SELECT XML FILE FOR UWG
        private void check()
        {
            if(epwPathRun=="" || xmlUWGFileNameRun == "" || resultPathText.Text=="" || resultName.Text=="")
            {
                UWGStart.IsEnabled = false;
                return;
            }
            if(numberOfSimulation > 1 && (xmlUWGFileName1 == "" || resultName1.Text=="" ))
            {
                UWGStart.IsEnabled = false;
                return;
            }
            if (numberOfSimulation > 2 && (xmlUWGFileName2 == "" || resultName2.Text == ""))
            {
                UWGStart.IsEnabled = false;
                return;
            }
            if (numberOfSimulation > 3 && (xmlUWGFileName3 == "" || resultName3.Text == ""))
            {
                UWGStart.IsEnabled = false;
                return;
            }
            if (numberOfSimulation > 4 && (xmlUWGFileName4 == "" || resultName4.Text == ""))
            {
                UWGStart.IsEnabled = false;
                return;
            }
            if (numberOfSimulation > 5 && (xmlUWGFileName5 == "" || resultName5.Text == ""))
            {
                UWGStart.IsEnabled = false;
                return;
            }
            UWGStart.IsEnabled = true;
        }
        private void xmlBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML files (.xml)|*.xml";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                filename_xmlUWGRun = dlg.FileName;
                //xmlFileEmpty.Text = filename_xmlUWG;
                //Get directory path only
                this.xmlUWGPathRun = System.IO.Path.GetDirectoryName(filename_xmlUWGRun);
                //Get only the file name
                this.xmlUWGFileNameRun = System.IO.Path.GetFileName(filename_xmlUWGRun);
                xmlFileEmpty.Text = xmlUWGFileNameRun;
                check();
            };
        }
        private void xmlBrowse_Click1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML files (.xml)|*.xml";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                filename_xmlUWG1 = dlg.FileName;
                xmlFileEmpty1.Text = filename_xmlUWG1;
                //Get directory path only
                this.xmlUWGPath1 = System.IO.Path.GetDirectoryName(filename_xmlUWG1);
                //Get only the file name
                this.xmlUWGFileName1 = System.IO.Path.GetFileName(filename_xmlUWG1);
                xmlFileEmpty1.Text = this.xmlUWGFileName1;
                check();
            };
        }
        private void xmlBrowse_Click2(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML files (.xml)|*.xml";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                filename_xmlUWG2 = dlg.FileName;
                xmlFileEmpty2.Text = filename_xmlUWG2;
                //Get directory path only
                this.xmlUWGPath2 = System.IO.Path.GetDirectoryName(filename_xmlUWG2);
                //Get only the file name
                this.xmlUWGFileName2 = System.IO.Path.GetFileName(filename_xmlUWG2);
                xmlFileEmpty2.Text = this.xmlUWGFileName2;
                check();
            }
        }
        private void xmlBrowse_Click3(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML files (.xml)|*.xml";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                filename_xmlUWG3 = dlg.FileName;
                xmlFileEmpty3.Text = filename_xmlUWG3;
                //Get directory path only
                this.xmlUWGPath3 = System.IO.Path.GetDirectoryName(filename_xmlUWG3);
                //Get only the file name
                this.xmlUWGFileName3 = System.IO.Path.GetFileName(filename_xmlUWG3);
                xmlFileEmpty3.Text = this.xmlUWGFileName3;
                check();
            };
        }
        private void xmlBrowse_Click4(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML files (.xml)|*.xml";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                filename_xmlUWG4 = dlg.FileName;
                xmlFileEmpty4.Text = filename_xmlUWG4;
                //Get directory path only
                this.xmlUWGPath4 = System.IO.Path.GetDirectoryName(filename_xmlUWG4);
                //Get only the file name
                this.xmlUWGFileName4 = System.IO.Path.GetFileName(filename_xmlUWG4);
                xmlFileEmpty4.Text = this.xmlUWGFileName4;
                check();
            };
        }
        private void xmlBrowse_Click5(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML files (.xml)|*.xml";

            // Display OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                filename_xmlUWG5 = dlg.FileName;
                xmlFileEmpty5.Text = filename_xmlUWG5;
                //Get directory path only
                this.xmlUWGPath5 = System.IO.Path.GetDirectoryName(filename_xmlUWG5);
                //Get only the file name
                this.xmlUWGFileName5 = System.IO.Path.GetFileName(filename_xmlUWG5);
                xmlFileEmpty5.Text = this.xmlUWGFileName5;
                check();
            };
        }

        private void select(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowDialog();
            resultPathText.Text = dlg.SelectedPath;
            check();
        }

        private void selectSim(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Weather Files (.epw)|*.epw";
            DialogResult userClickedOK = dlg.ShowDialog();
            if (userClickedOK == System.Windows.Forms.DialogResult.OK)
            {
                if (dlg.CheckFileExists)
                {
                    var buttonSim = sender as System.Windows.Controls.Button;
                    if (buttonSim.Name == "pathbrowseSim1")
                    {
                        pathSim1 = dlg.FileName;
                        loadDataSim1(sender, e);

                    }
                    else if (buttonSim.Name == "pathbrowseSim2")
                    {
                        pathSim2 = dlg.FileName;
                        loadDataSim2(sender, e);

                    }
                    else if (buttonSim.Name == "pathbrowseSim3")
                    {
                        pathSim3 = dlg.FileName;
                        loadDataSim3(sender, e);

                    }
                    else if (buttonSim.Name == "pathbrowseSim4")
                    {
                        pathSim4 = dlg.FileName;
                        loadDataSim4(sender, e);

                    }
                    else if (buttonSim.Name == "pathbrowseSim5")
                    {
                        pathSim5 = dlg.FileName;
                        loadDataSim5(sender, e);

                    }
                    else if (buttonSim.Name == "pathbrowseSim6")
                    {
                        pathSim6 = dlg.FileName;
                        loadDataSim6(sender, e);

                    }
                }
            }
        }

        //trigger valiation at beginning
        public void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // we manually fire the bindings so we get the validation initially
            simuStartMonth.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateSource();
            simuStartDay.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateSource();
            simuDuration.GetBindingExpression(System.Windows.Controls.TextBox.TextProperty).UpdateSource();
        }

    }
}