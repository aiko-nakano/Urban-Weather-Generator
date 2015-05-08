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

namespace UWG
{

    public partial class UWGInputs : Window
    {
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
        public String DialogName;
        simuParam p = new simuParam();
        private XmlDocument defxml = new XmlDocument();


        public UWGInputs()
        {
            try
            {
                defxml.Load("TemplateLibrary.xml");
            }
            catch
            {
                MessageBox.Show("Initializing failed, TemplateLibrary.xml missing!", "UWG");
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
                        if(ma.SelectSingleNode("Name").InnerText==roofLayer1Material.Content)
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
                        if (ma.SelectSingleNode("Name").InnerText == roofLayer2Material.Content)
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
                        if (ma.SelectSingleNode("Name").InnerText == roofLayer3Material.Content)
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
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer1Material.Content)
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
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer2Material.Content)
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
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer3Material.Content)
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
                        if (ma.SelectSingleNode("Name").InnerText == wallLayer4Material.Content)
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
                if (ma.SelectSingleNode("Name").InnerText == massLayer1Material.Content)
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
                if (ma.SelectSingleNode("Name").InnerText == urbanRoadMaterial.Content)
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
                if (ma.SelectSingleNode("Name").InnerText == ruralRoadMaterial.Content)
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
        private void run_Click(object sender, RoutedEventArgs e)
        {
            RunWindow run = new RunWindow();
            run.ShowDialog();
            run.Closed += new EventHandler(Enable);
        }

        private void sim_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Simulation page still needs to be connected.");
            //SimWindow sim = new SimWindow();
            //sim.ShowDialog();
            //sim.Closed += new EventHandler(Enable);
        }

        private void makeFile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Make File to be made");
        }

        private void importUMI(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Importing to be added");
        }

        private void addTypology(object sender, RoutedEventArgs e)
        {
            minusTypButton.IsEnabled = true;
            numberOfTypology++;
            if(numberOfTypology==2)
            {
                typLabel1.Visibility = System.Windows.Visibility.Visible;
                typology1Type.Visibility = System.Windows.Visibility.Visible;
                typology1Dist.Visibility = System.Windows.Visibility.Visible;
                typPerc1.Visibility = System.Windows.Visibility.Visible;
                typTab1.Visibility = System.Windows.Visibility.Visible;


            }
            else if (numberOfTypology == 3)
            {
                typLabel2.Visibility = System.Windows.Visibility.Visible;
                typology2Type.Visibility = System.Windows.Visibility.Visible;
                typology2Dist.Visibility = System.Windows.Visibility.Visible;
                typPerc2.Visibility = System.Windows.Visibility.Visible;
                typTab2.Visibility = System.Windows.Visibility.Visible;

            }
            else if (numberOfTypology == 4)
            {
                typLabel3.Visibility = System.Windows.Visibility.Visible;
                typology3Type.Visibility = System.Windows.Visibility.Visible;
                typology3Dist.Visibility = System.Windows.Visibility.Visible;
                typPerc3.Visibility = System.Windows.Visibility.Visible;
                typTab3.Visibility = System.Windows.Visibility.Visible;
                addTypButton.IsEnabled = false;
                typologyCheck();

            }


        }
        private void minusTypology(object sender, RoutedEventArgs e)
        {
            addTypButton.IsEnabled = true;
            if (numberOfTypology == 2)
            {
                typLabel1.Visibility = System.Windows.Visibility.Collapsed;
                typology1Type.Visibility = System.Windows.Visibility.Collapsed;
                typology1Dist.Visibility = System.Windows.Visibility.Collapsed;
                typPerc1.Visibility = System.Windows.Visibility.Collapsed;
                typTab1.Visibility = System.Windows.Visibility.Collapsed;
                minusTypButton.IsEnabled = false;


            }
            else if (numberOfTypology == 3)
            {
                typLabel2.Visibility = System.Windows.Visibility.Collapsed;
                typology2Type.Visibility = System.Windows.Visibility.Collapsed;
                typology2Dist.Visibility = System.Windows.Visibility.Collapsed;
                typPerc2.Visibility = System.Windows.Visibility.Collapsed;
                typTab2.Visibility = System.Windows.Visibility.Collapsed;

            }
            else if (numberOfTypology == 4)
            {
                typLabel3.Visibility = System.Windows.Visibility.Collapsed;
                typology3Type.Visibility = System.Windows.Visibility.Collapsed;
                typology3Dist.Visibility = System.Windows.Visibility.Collapsed;
                typPerc3.Visibility = System.Windows.Visibility.Collapsed;
                typTab3.Visibility = System.Windows.Visibility.Collapsed;
                addTypButton.IsEnabled = false;
                typologyCheck();
            }
            numberOfTypology--;



        }

        private void typologyCheck()
        {
            MessageBox.Show("Still need to validate data!");
        }
        
        private void Enable(object sender, EventArgs e)
        {
            this.IsEnabled = true;
        }
        //Save Button
        private void buttonSave_Click1(object sender, RoutedEventArgs e)
        {
            if (this.xmlFileName == "")
            {
                button1_Click(sender, e);
            }
            else
            {
                buttonSave_Click(sender, e);
                MessageBox.Show("Saving succeeded!");
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

                        XDocument doc = new XDocument(new XElement("xml_input",
                                                            new XElement("construction",
                                                                new XElement("wall",
                                                                    new XElement("albedo", wallAlbedo.Text),
                                                                    new XElement("emissivity", wallEmissivity.Text),
                                                                    new XElement("materials",
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
                                                                    new XElement("glazingRatio", wwr.Content),
                                                                    new XElement("windowUvalue", uValue.Content),
                                                                    new XElement("windowSHGC", SHGC.Content)),

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
                                                                    new XElement("vegetationCoverage", urbanRoadVegFraction.Content),
                                                                    new XElement("inclination", 1),
                                                                    new XElement("initialTemperature", 20)),

                                                                new XElement("rural",
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
                                                                    new XElement("vegetationCoverage", ruralRoadVegFraction.Content),
                                                                    new XElement("inclination", 1),
                                                                    new XElement("initialTemperature", 20))
                                                            ),

                                                            //BUILDING GEOMETRY:
                                                            new XElement("building",
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
                                                            ),

                                                            new XElement("urbanArea",
                                                                new XElement("averageBuildingHeight", avgBldgHeight.Text),
                                                                new XElement("horizontalBuildingDensity", hBDensity.Text),
                                                                new XElement("verticalToHorizontalUrbanAreaRatio", vHRatios.Text),
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
                                                                new XElement("refHeight", refHeight.Text)
                                                            ),
                                                            new XElement("referenceSite",
                                                                new XElement("latitude", latitude.Text),
                                                                new XElement("longitude", longitude.Text),
                                                                new XElement("averageObstacleHeight", avgObstacleHeight.Text)
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
            DialogName = "Save As";
            button1_Click(sender, e);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
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

                        XDocument doc = new XDocument(new XElement("xml_input",
                                                            new XElement("construction",
                                                                new XElement("wall",
                                                                    new XElement("albedo", wallAlbedo.Text),
                                                                    new XElement("emissivity", wallEmissivity.Text),
                                                                    new XElement("materials",
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
                                                                    new XElement("glazingRatio", wwr.Content),
                                                                    new XElement("windowUvalue", uValue.Content),
                                                                    new XElement("windowSHGC", SHGC.Content)),

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
                                                                    new XElement("vegetationCoverage", urbanRoadVegFraction.Content),
                                                                    new XElement("inclination", 1),
                                                                    new XElement("initialTemperature", 20)),

                                                                new XElement("rural",
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
                                                                    new XElement("vegetationCoverage", ruralRoadVegFraction.Content),
                                                                    new XElement("inclination", 1),
                                                                    new XElement("initialTemperature", 20))
                                                            ),

                                                            //BUILDING GEOMETRY:
                                                            new XElement("building",
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
                                                            ),

                                                            new XElement("urbanArea",
                                                                new XElement("averageBuildingHeight", avgBldgHeight.Text),
                                                                new XElement("horizontalBuildingDensity", hBDensity.Text),
                                                                new XElement("verticalToHorizontalUrbanAreaRatio", vHRatios.Text),
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
                                                                new XElement("refHeight", refHeight.Text)
                                                            ),
                                                            new XElement("referenceSite",
                                                                new XElement("latitude", latitude.Text),
                                                                new XElement("longitude", longitude.Text),
                                                                new XElement("averageObstacleHeight", avgObstacleHeight.Text)
                                                            ),
                                                            new XElement("parameter",
                                                                new XElement("tempHeight", tempHeight.Text),
                                                                new XElement("windHeight", windHeight.Text),
                                                                new XElement("circCoeff", 1.2),
                                                                new XElement("dayThreshold", 200),
                                                                new XElement("nightThreshold", 50),
                                                                new XElement("windMin", 0.1),
                                                                new XElement("windMax", 10),
                                                                new XElement("wgmax", 0.005),
                                                                new XElement("exCoeff", 0.3)
                                                            )
                                                        )
                                                    );
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
                    } //CLOSES: Change each sensitivity analysis parameter one at a time
            } //closes TRY

            catch (Exception error)
            {
                textBox1.Text = error.ToString();
            }
        } //closes BUTTON1_CLICK [xml] 

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
                if (epwFileName == null)
                {
                    MessageBox.Show("Please select epw file");
                }     
                ProcessStartInfo startUWG = new ProcessStartInfo();

                //startUWG.FileName = "C:\\Users\\anakano\\Documents\\Research\\UWG2.1\\For_Installer\\UWGv2.0.exe";
                startUWG.FileName = "UWGEngine.exe";
                //make sure there is space in between each of the four inputs and that folder extension ends with \\
                startUWG.Arguments = this.epwPath + "\\ " + this.epwFileName + " " + this.xmlUWGPath + "\\ " + this.xmlUWGFileName;
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
            var result = MessageBox.Show(message, captain, buttons);
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
      
            XmlNodeList elemList = parseXml.GetElementsByTagName("thickness");
            List<string> wall = new List<string>();
            List<string> roof = new List<string>();
            List<string> mass = new List<string>();
            List<string> urbanRoad = new List<string>();
            List<string> ruralRoad = new List<string>();
            for (int i=0; i < elemList.Count; i++)
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
                    
                    // Urban Road
                    if (i == 3)
                    {
                        urbanRoad.Add(value.Trim(new Char[] {'[', ']' }));
                    }
                    // Rural
                    if (i == 4)
                    {
                       ruralRoad.Add(value.Trim(new Char[] { '[', ']' }));
                    }
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
            //urbanRoad
            int desiredIndexUrban = 0;
            if (desiredIndexUrban < urbanRoad.Count)
            {
                urbanRoadThickness.Content = urbanRoad[desiredIndexUrban];
            }
            //Rural
            System.Diagnostics.Debug.WriteLine(ruralRoad[0]);
            int desiredIndexRural = 0;
            if (desiredIndexRural < ruralRoad.Count)
            {
                ruralRoadThickness.Content = ruralRoad[desiredIndexRural];
            }
        }

        //New xml = default to default_input.xml
        private void newXml_Click(object sender, RoutedEventArgs e)
        {
            string message = "Please make sure to save your current XML file before continue.";
            string captain = "UWG";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            var result= MessageBox.Show(message, captain, buttons);
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

            XmlNodeList elemList = parseXml.GetElementsByTagName("thickness");
            List<string> wall = new List<string>();
            List<string> roof = new List<string>();
            List<string> mass = new List<string>();
            List<string> urbanRoad = new List<string>();
            List<string> ruralRoad = new List<string>();
            for (int i = 0; i < elemList.Count; i++)
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

                    // Urban Road
                    if (i == 3)
                    {
                        urbanRoad.Add(value.Trim(new Char[] { '[', ']' }));
                    }
                    // Rural
                    if (i == 4)
                    {
                        ruralRoad.Add(value.Trim(new Char[] { '[', ']' }));
                    }
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
            //urbanRoad
            int desiredIndexUrban = 0;
            if (desiredIndexUrban < urbanRoad.Count)
            {
                urbanRoadThickness.Content = urbanRoad[desiredIndexUrban];
            }
            //Rural
            System.Diagnostics.Debug.WriteLine(ruralRoad[0]);
            int desiredIndexRural = 0;
            if (desiredIndexRural < ruralRoad.Count)
            {
                ruralRoadThickness.Content = ruralRoad[desiredIndexRural];
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


    }
}