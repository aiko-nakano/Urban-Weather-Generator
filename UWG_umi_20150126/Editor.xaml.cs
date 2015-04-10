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
using System.Collections.ObjectModel;
using System.Globalization;
namespace UWG
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public class MaterialType
    {
        public MaterialType()
        {
            mat = new ObservableCollection<Material>();
        }
        public string Name {get;set;}
        public ObservableCollection<Material> mat { get; set; }
    }
    public class Material
    {

        public string Name { get; set; }
        public string Type { get; set; }
        public string Conductivity { get; set; }
        public string VHC { get; set; }
        public string Albedo { get; set; }
        public string Emissivity { get; set; }
        public string Source { get; set; }
        public string Comments { get; set; }
    }
    public class AssemblyType
    {
        public AssemblyType()
        {
            ase = new ObservableCollection<Assembly>();
        }
        public string Name {get;set;}
        public ObservableCollection<Assembly> ase { get; set; }
    }
    public class Assembly
    {
        public Assembly()
        {
            Layer = new string[4];
            Thickness = new string[4];
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Comments { get; set; }
        public string[] Layer { get; set; }
        public string[] Thickness { get; set; }
    }
    public class Schedule
    {
        public Schedule()
        {
            Weekdays = new Double[24];
            Saturday = new Double[24];
            Sunday = new Double[24];
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string Comments { get; set; }
        public Double[] Weekdays { get; set; }
        public Double[] Saturday { get; set; }
        public Double[] Sunday { get; set; }
    }
    public class ScheduleType
    {
        public ScheduleType()
        {
            sch = new ObservableCollection<Schedule>();
        }
        public string Name { get; set; }
        public ObservableCollection<Schedule> sch { get; set; }
    }
    public class Glazing
    {
        public string Name { get; set; }
        public string WWR { get; set; }
        public string UValue { get; set; }
        public string SHGC { get; set; }
    }
    public class Heat
    {
        public string Name { get; set; }
        public string Source { get; set; }
        public string Comment { get; set; }
        public string OccupancySched { get; set; }
        public string OccupancyMax { get; set; }
        public string LightsSched { get; set; }
        public string LightsMax { get; set; }
        public string EquipSched { get; set; }
        public string EquipMax { get; set; }
        public string InfiltrationSched { get; set; }
        public string InfiltrationMax { get; set; }
        public string VentilationSched { get; set; }
        public string VentilationMax { get; set; }
    }

    public partial class Editor : Window
    {
        private XmlDocument template = new XmlDocument();
        public ObservableCollection<MaterialType> MaterialTypeList = new ObservableCollection<MaterialType>();
        public ObservableCollection<String> MaterialTypeNameList = new ObservableCollection<String>();
        public ObservableCollection<String> MaterialNameList = new ObservableCollection<String>();
        public ObservableCollection<String> ScheduleNameList = new ObservableCollection<String>();
        public ObservableCollection<String> ScheduleTypeNameList = new ObservableCollection<String>();
        public ObservableCollection<ScheduleType> ScheduleTypeList = new ObservableCollection<ScheduleType>();
        private bool materialChange = false;
        private bool assemblyChange = false;
        private bool glazingChange = false;
        private bool scheduleChange = false;
        private bool heatChange = false;
        private String currentMaterialName = "";
        public ObservableCollection<AssemblyType> AssemblyTypeList = new ObservableCollection<AssemblyType>();
        public ObservableCollection<String> AssemblyTypeNameList = new ObservableCollection<String>();
        private String currentAssemblyName = "";
        public ObservableCollection<Glazing> GlazingList = new ObservableCollection<Glazing>();
        public ObservableCollection<Heat> HeatList = new ObservableCollection<Heat>();
        private String currentGlzaingName = "";
        private String currentScheduleName = "";
        private bool scheduleSave = false;
        public void closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(materialChange==true || assemblyChange==true || glazingChange==true)
            {
                string message = "";
                if(materialChange==true && assemblyChange==true && glazingChange==true)
                    message = "You havn't saved your current material template, assembly template and glazing template, do you still want to leave?";
                if (materialChange == true && assemblyChange == true && glazingChange == false)
                    message = "You havn't saved your current material template and assembly template, do you still want to leave?";
                if (materialChange == true && assemblyChange == false && glazingChange == true)
                    message = "You havn't saved your current material template and glazing template, do you still want to leave?";
                if (materialChange == false && assemblyChange == true && glazingChange == true)
                    message = "You havn't saved your current assembly template and glazing template, do you still want to leave?";
                if (materialChange == true && assemblyChange == false && glazingChange == false)
                    message = "You havn't saved your current material template, do you still want to leave?";
                if (materialChange == false && assemblyChange == false && glazingChange == true)
                    message = "You havn't saved your current glazing template, do you still want to leave?";
                if (materialChange == false && assemblyChange == true && glazingChange == false)
                    message = "You havn't saved your current assembly template, do you still want to leave?";
                
                string captain = "UWG";
                MessageBoxButton buttons = MessageBoxButton.OKCancel;
                var result = MessageBox.Show(message, captain, buttons);
                if (result == MessageBoxResult.Cancel) e.Cancel = true;
            }
            if (scheduleChange == true || heatChange == true)
            {
                string message = "";
                if (scheduleChange == true && heatChange == true)
                    message = "You havn't saved your current schedule template and buidling template, do you still want to leave?";
                if (scheduleChange == true && heatChange == false)
                    message = "You havn't saved your current schedule template, do you still want to leave?";
                if (scheduleChange == false && heatChange == true)
                    message = "You havn't saved your current building template, do you still want to leave?";
                string captain = "UWG";
                MessageBoxButton buttons = MessageBoxButton.OKCancel;
                var result = MessageBox.Show(message, captain, buttons);
                if (result == MessageBoxResult.Cancel) e.Cancel = true;
            }
        }
        public void dataRefresh()
        {
            DataContext = new { MaterialTypeList, MaterialTypeNameList, AssemblyTypeList, AssemblyTypeNameList, MaterialNameList, GlazingList, ScheduleNameList, ScheduleTypeList, ScheduleTypeNameList, HeatList};
        }

        public Editor()
        {
            InitializeComponent();
            try
            {
                template.Load("TemplateLibrary.xml");
            }
            catch
            {
                MessageBox.Show("Invalid template file, please check the TemplateLibrary.xml file!");
                this.Close();
            }
            materialInitialize();
            assemblyInitialize();
            glazingInitialize();
            scheduleInitialize();
            heatInitialize();
            MaterialNameList.Add("");
            dataRefresh();
            materialChange = false;
            assemblyChange = false;
            glazingChange = false;
        }
        public void materialInitialize()
        {
            materialTreeView.Visibility = System.Windows.Visibility.Visible;
            MaterialTypeList = new ObservableCollection<MaterialType>();
            XmlNodeList materialList=this.template.GetElementsByTagName("OpaqueMaterial");
            foreach(XmlNode no in materialList)
            {
                XmlNodeList check = no.SelectNodes("VHC");
                if (check.Count == 1)
                {
                    bool a = false;
                    Material mo = new Material();
                    mo.Albedo = no.SelectSingleNode("Albedo").InnerText;
                    mo.Comments = no.SelectSingleNode("Comments").InnerText;
                    mo.Conductivity = no.SelectSingleNode("Conductivity").InnerText;
                    mo.Emissivity = no.SelectSingleNode("Emissivity").InnerText;
                    mo.Name = no.SelectSingleNode("Name").InnerText;
                    mo.Source = no.SelectSingleNode("DataSource").InnerText;
                    mo.Type = no.SelectSingleNode("Type").InnerText;
                    mo.VHC = no.SelectSingleNode("VHC").InnerText;
                    MaterialNameList.Add(mo.Name);
                    foreach (MaterialType mt in MaterialTypeList)
                    {
                        if (mo.Type == mt.Name)
                        {
                            mt.mat.Add(mo);
                            mt.mat.OrderBy(x => x.Name.GetHashCode());
                            a = true;
                        }
                    }
                    if (!a)
                    {
                        var s = new MaterialType()
                            {
                                mat = { mo }
                            };
                        s.Name = mo.Type;
                        MaterialTypeList.Add(s);
                        MaterialTypeNameList.Add(mo.Type);
                    }
                }
            }
        }
        public void assemblyInitialize()
        {
            AssemblyTypeList = new ObservableCollection<AssemblyType>();
            XmlNodeList assemblyList = this.template.GetElementsByTagName("OpaqueConstruction");
            foreach(XmlNode no in assemblyList)
            {
                bool a = false;
                Assembly nas = new Assembly();
                nas.Name = no.SelectSingleNode("Name").InnerText;
                nas.Type = no.SelectSingleNode("Type").InnerText;
                nas.Source = no.SelectSingleNode("DataSource").InnerText;
                nas.Comments = no.SelectSingleNode("Comments").InnerText;
                XmlNode layss = no.SelectSingleNode("Layers");
                XmlNodeList lays = layss.SelectNodes("OpaqueLayer");
                int i = 0;
                foreach (XmlNode lay in lays)
                {
                    nas.Layer[i] = lay.SelectSingleNode("MaterialName").InnerText;
                    nas.Thickness[i] = lay.SelectSingleNode("Thickness").InnerText;
                    i++;
                    if(i==4)
                    {
                        break;
                    }
                }
                foreach(AssemblyType asty in AssemblyTypeList)
                {
                    if(asty.Name==nas.Type)
                    {
                        asty.ase.Add(nas);
                        a = true;
                    }
                }
                if(!a)
                {
                    AssemblyType astyn = new AssemblyType();
                    astyn.Name = nas.Type;
                    astyn.ase.Add(nas);
                    AssemblyTypeList.Add(astyn);
                    AssemblyTypeNameList.Add(astyn.Name);
                }
            }
        }
        public void scheduleInitialize()
        {
            ScheduleTypeList = new ObservableCollection<ScheduleType>();
            XmlNodeList scheduleList = this.template.GetElementsByTagName("WeekSchedule");
            XmlNodeList dayList = this.template.GetElementsByTagName("DaySchedule");
            foreach (XmlNode no in scheduleList)
            {
                bool a = false;
                Schedule nas = new Schedule();
                nas.Name = no.SelectSingleNode("Name").InnerText;
                nas.Type = no.SelectSingleNode("Type").InnerText;
                nas.Source = no.SelectSingleNode("DataSource").InnerText;
                nas.Comments = no.SelectSingleNode("Comments").InnerText;
                XmlNode dayss = no.SelectSingleNode("Days");
                XmlNodeList days = dayss.SelectNodes("string");
                string wkd = days[0].InnerText;
                string sat = days[5].InnerText;
                string sun = days[6].InnerText;
                foreach(XmlNode ds in dayList)
                {
                    if(wkd==ds.SelectSingleNode("Name").InnerText)
                    {
                        XmlNodeList wkdv = ds.SelectSingleNode("Values").SelectNodes("double");
                        int i = 0;
                        foreach(XmlNode wkdvn in wkdv)
                        {
                            nas.Weekdays[i] = Convert.ToDouble(wkdvn.InnerText);
                            i++;
                        }
                        break;
                    }
                }
                foreach (XmlNode ds in dayList)
                {
                    if (sat == ds.SelectSingleNode("Name").InnerText)
                    {
                        XmlNodeList wkdv = ds.SelectSingleNode("Values").SelectNodes("double");
                        int i = 0;
                        foreach (XmlNode wkdvn in wkdv)
                        {
                            nas.Saturday[i] = Convert.ToDouble(wkdvn.InnerText);
                            i++;
                        }
                        break;
                    }
                }
                foreach (XmlNode ds in dayList)
                {
                    if (sun == ds.SelectSingleNode("Name").InnerText)
                    {
                        XmlNodeList wkdv = ds.SelectSingleNode("Values").SelectNodes("double");
                        int i = 0;
                        foreach (XmlNode wkdvn in wkdv)
                        {
                            nas.Sunday[i] = Convert.ToDouble(wkdvn.InnerText);
                            i++;
                        }
                        break;
                    }
                }
                ScheduleNameList.Add(nas.Name);
                foreach (ScheduleType schtp in ScheduleTypeList)
                {
                    if (schtp.Name == nas.Type)
                    {
                        schtp.sch.Add(nas);
                        a = true;
                    }
                }
                if (!a)
                {
                    ScheduleType schtp = new ScheduleType();
                    schtp.Name = nas.Type;
                    schtp.sch.Add(nas);
                    ScheduleTypeList.Add(schtp);
                    ScheduleTypeNameList.Add(nas.Type);
                }
            }
        }
        public void glazingInitialize()
        {
            GlazingList = new ObservableCollection<Glazing>();
            Glazing dglazing = new Glazing();
            dglazing.Name = "New Glazing";
            GlazingList.Add(dglazing);
            XmlNodeList glazingList = this.template.GetElementsByTagName("GlazingConstruction");
            foreach(XmlNode no in glazingList)
            {
                XmlNodeList check = no.SelectNodes("WWR");
                if(check.Count==1)
                {
                    Glazing ng = new Glazing();
                    ng.Name = no.SelectSingleNode("Name").InnerText;
                    ng.WWR = no.SelectSingleNode("WWR").InnerText;
                    ng.UValue = no.SelectSingleNode("UValue").InnerText;
                    ng.SHGC = no.SelectSingleNode("SHGC").InnerText;
                    GlazingList.Add(ng);
                }
            }
        }
        public void heatInitialize()
        {
            HeatList = new ObservableCollection<Heat>();
            Heat he = new Heat();
            he.Name = "New Building Template";
            HeatList.Add(he);
            XmlNodeList heatList = this.template.GetElementsByTagName("HeatTemplate");
            foreach (XmlNode no in heatList)
            {
                Heat ng = new Heat();
                ng.Name = no.SelectSingleNode("Name").InnerText;
                ng.Comment = no.SelectSingleNode("Comments").InnerText;
                ng.Source = no.SelectSingleNode("DataSource").InnerText;
                ng.OccupancySched = no.SelectSingleNode("OccupancySched").InnerText;
                ng.OccupancyMax = no.SelectSingleNode("OccupancyMax").InnerText;
                ng.LightsSched = no.SelectSingleNode("LightsSched").InnerText;
                ng.LightsMax = no.SelectSingleNode("LightsMax").InnerText;
                ng.EquipSched = no.SelectSingleNode("EquipSched").InnerText;
                ng.EquipMax = no.SelectSingleNode("EquipMax").InnerText;
                ng.VentilationSched = no.SelectSingleNode("VentilationSched").InnerText;
                ng.VentilationMax = no.SelectSingleNode("VentilationMax").InnerText;
                ng.InfiltrationSched = no.SelectSingleNode("InfiltrationSched").InnerText;
                ng.InfiltrationMax = no.SelectSingleNode("InfiltrationMax").InnerText;

                HeatList.Add(ng);
            }
        }

        public void materialSelect(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = materialTreeView.SelectedItem as Material;
            try
            {
                materialName.Text = item.Name;
                materialSource.Text = item.Source;
                materialType.Text = item.Type;
                materialVHC.Text = item.VHC;
                materialAlbedo.Text = item.Albedo;
                materialComment.Text = item.Comments;
                materialConductivity.Text = item.Conductivity;
                materialEmissivity.Text = item.Emissivity;
                currentMaterialName=item.Name;
                materialDeleteButton.IsEnabled = true;
                materialChange = false;
            }
            catch {
                materialName.Text = "";
                materialSource.Text = "";
                materialType.Text = "";
                materialVHC.Text = "";
                materialAlbedo.Text = "";
                materialComment.Text = "";
                materialConductivity.Text = "";
                materialEmissivity.Text = "";
                currentMaterialName = "";
                materialDeleteButton.IsEnabled = false;
                materialChange = false;
            }
        }
        public void assemblySelect(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = assemblyTreeView.SelectedItem as Assembly;
            try
            {
                assemblyName.Text = item.Name;
                assemblyType.Text = item.Type;
                assemblySource.Text = item.Source;
                assemblyComment.Text = item.Comments;
                assemblyLayer1.Text = item.Layer[0];
                assemblyLayer2.Text = item.Layer[1];
                assemblyLayer3.Text = item.Layer[2];
                assemblyLayer4.Text = item.Layer[3];
                assemblyThickness1.Text = item.Thickness[0];
                assemblyThickness2.Text = item.Thickness[1];
                assemblyThickness3.Text = item.Thickness[2];
                assemblyThickness4.Text = item.Thickness[3];
                assemblyDeleteButton.IsEnabled = true;
                currentAssemblyName = item.Name;
                assemblyChange = false;
            }
            catch
            {
                assemblyName.Text = "";
                assemblyType.Text = "";
                assemblySource.Text = "";
                assemblyComment.Text = "";
                assemblyLayer1.Text = "";
                assemblyLayer2.Text = "";
                assemblyLayer3.Text = "";
                assemblyLayer4.Text = "";
                assemblyThickness1.Text = "";
                assemblyThickness2.Text = "";
                assemblyThickness3.Text = "";
                assemblyThickness4.Text = "";
                currentAssemblyName = "";
                assemblyDeleteButton.IsEnabled = false;
                assemblyChange = false;
            }
        }
        public void glazingSelect(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = glazingTreeView.SelectedItem as Glazing;
            try
            {
                if (item.Name != "New Glazing")
                {
                    Name.Text = item.Name;
                    WWR.Text = item.WWR;
                    UValue.Text = item.UValue;
                    SHGC.Text = item.SHGC;
                    glazingChange = false;
                    currentGlzaingName = item.Name;
                    glazingDeleteButton.IsEnabled = true;
                }
                else
                {
                    Name.Text = "";
                    WWR.Text = "";
                    UValue.Text = "";
                    SHGC.Text = "";
                    glazingChange = false;
                    currentGlzaingName = "";
                    glazingDeleteButton.IsEnabled = false;
                }
            }
            catch
            {
                Name.Text = "";
                WWR.Text = "";
                UValue.Text = "";
                SHGC.Text = "";
                glazingChange = false;
                currentGlzaingName = "";
                glazingDeleteButton.IsEnabled = false;
            }
        }
        public void scheduleSelect(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (scheduleSave == true)
            {
                scheduleSave = false;
                return;
            }
            var item = scheduleTreeView.SelectedItem as Schedule;
            try
            {
                scheduleName.Text = item.Name;
                scheduleType.Text = item.Type;
                scheduleSource.Text = item.Source;
                scheduleComment.Text = item.Comments;
                int i = 0;
                foreach(Slider sd in weekdaysGrid.Children)
                {
                    sd.Value = item.Weekdays[i];
                    i++;
                    if (i == 24) break;
                }
                i = 0;
                foreach (Slider sd in SaturdayGrid.Children)
                {
                    sd.Value = item.Saturday[i];
                    i++;
                    if (i == 24) break;
                }
                i = 0;
                foreach (Slider sd in SundayGrid.Children)
                {
                    sd.Value = item.Sunday[i];
                    i++;
                    if (i == 24) break;
                }
                scheduleDeleteButton.IsEnabled = true;
                currentScheduleName = item.Name;
                scheduleChange = false;
            }
            catch
            {
                scheduleName.Text = "";
                scheduleType.Text = "";
                scheduleSource.Text = "";
                scheduleComment.Text = "";
                int i = 0;
                foreach (Slider sd in weekdaysGrid.Children)
                {
                    sd.Value = 0;
                    i++;
                    if (i == 24) break;
                }
                i = 0;
                foreach (Slider sd in SaturdayGrid.Children)
                {
                    sd.Value = 0;
                    i++;
                    if (i == 24) break;
                }
                i = 0;
                foreach (Slider sd in SundayGrid.Children)
                {
                    sd.Value = 0;
                    i++;
                    if (i == 24) break;
                }
                scheduleDeleteButton.IsEnabled = false;
                currentScheduleName = "";
                scheduleChange = false;
            }
        }
        public void heatSelect(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var item = heatTreeView.SelectedItem as Heat;
            try
            {
                if (item.Name != "New Building Template")
                {
                    heatName.Text = item.Name;
                    heatSource.Text = item.Source;
                    heatComment.Text = item.Comment;
                    Occupancy.Text = item.OccupancySched;
                    occupancyMax.Text = item.OccupancyMax;
                    Lights.Text = item.LightsSched;
                    lightsMax.Text = item.LightsMax;
                    Equipments.Text = item.EquipSched;
                    equipMax.Text = item.EquipMax;
                    Infiltration.Text = item.InfiltrationSched;
                    infiltrationMax.Text = item.InfiltrationMax;
                    Ventilation.Text = item.VentilationSched;
                    ventilationMax.Text = item.VentilationMax;
                    heatChange = false;
                    heatDeleteButton.IsEnabled = true;
                }
                else
                {
                    heatName.Text = "";
                    heatSource.Text = "";
                    heatComment.Text = "";
                    Occupancy.Text = "";
                    occupancyMax.Text = "";
                    Lights.Text = "";
                    lightsMax.Text = "";
                    Equipments.Text = "";
                    equipMax.Text = "";
                    Infiltration.Text = "";
                    infiltrationMax.Text = "";
                    Ventilation.Text = "";
                    ventilationMax.Text = "";
                    heatChange = false;
                    heatDeleteButton.IsEnabled = false;
                }
            }
            catch
            {
                heatName.Text = "";
                heatSource.Text = "";
                heatComment.Text = "";
                Occupancy.Text = "";
                occupancyMax.Text = "";
                Lights.Text = "";
                lightsMax.Text = "";
                Equipments.Text = "";
                equipMax.Text = "";
                Infiltration.Text = "";
                infiltrationMax.Text = "";
                Ventilation.Text = "";
                ventilationMax.Text = "";
                Name.Text = "";
                WWR.Text = "";
                UValue.Text = "";
                SHGC.Text = "";
                glazingChange = false;
                glazingDeleteButton.IsEnabled = false;
            }
        }
        private void materialSave_Click(object sender, RoutedEventArgs e)
        {
            if(materialName.Text=="")
            {
                MessageBox.Show("Please choose a name!");
                return;
            }
            if (materialType.Text == "")
            {
                MessageBox.Show("Please specify the type!");
                return;
            }
            if (materialVHC.Text == "")
            {
                MessageBox.Show("The VHC value is empty!");
                return;
            }
            if (materialAlbedo.Text == "")
            {
                MessageBox.Show("The Albedo value is empty!");
                return;
            }
            if (materialConductivity.Text == "")
            {
                MessageBox.Show("The Conductivity value is empty!");
                return;
            }
            if (materialEmissivity.Text == "")
            {
                MessageBox.Show("The Emissivity value is empty!");
                return;
            }
            bool ha = false;
            foreach(MaterialType mtp in MaterialTypeList)
            {
                foreach (Material mtr in mtp.mat)
                {
                    if (mtr.Name == materialName.Text)
                    {
                        string message = "There is already a material with this name, do you want to replace it?";
                        string captain = "UWG";
                        MessageBoxButton buttons = MessageBoxButton.OKCancel;
                        var result = MessageBox.Show(message, captain, buttons);
                        if (result == MessageBoxResult.Cancel) return;
                        if (result == MessageBoxResult.OK)
                        {

                            XmlNodeList xl = template.GetElementsByTagName("OpaqueMaterial");
                            foreach(XmlNode no in xl)
                            {
                                XmlNode NameA=no.SelectSingleNode("Name");
                                if(NameA.InnerText==mtr.Name)
                                {
                                    no.SelectSingleNode("Type").InnerText = materialType.Text;
                                    no.SelectSingleNode("Comments").InnerText = materialComment.Text;
                                    no.SelectSingleNode("DataSource").InnerText = materialSource.Text;
                                    no.SelectSingleNode("Conductivity").InnerText = materialConductivity.Text;
                                    no.SelectSingleNode("VHC").InnerText = materialVHC.Text;
                                    no.SelectSingleNode("Albedo").InnerText = materialAlbedo.Text;
                                    no.SelectSingleNode("Emissivity").InnerText = materialEmissivity.Text;
                                    ha = true;
                                    break;
                                }
                            }
                            if (mtr.Type == materialType.Text)
                            {
                                mtr.Albedo = materialAlbedo.Text;
                                mtr.Comments = materialComment.Text;
                                mtr.Conductivity = materialConductivity.Text;
                                mtr.Emissivity = materialEmissivity.Text;
                                mtr.Source = materialSource.Text;
                                mtr.Type = materialType.Text;
                                mtr.VHC = materialVHC.Text;
                            }
                            else
                            {
                                Material mtr2 = mtr;
                                mtr2.Albedo = materialAlbedo.Text;
                                mtr2.Comments = materialComment.Text;
                                mtr2.Conductivity = materialConductivity.Text;
                                mtr2.Emissivity = materialEmissivity.Text;
                                mtr2.Source = materialSource.Text;
                                mtr2.Type = materialType.Text;
                                mtr2.VHC = materialVHC.Text;
                                bool b1 = false;
                                foreach (MaterialType mtp9 in MaterialTypeList)
                                {
                                    if (mtp9.Name == mtr2.Type)
                                    {
                                        mtp9.mat.Add(mtr2);
                                        b1 = true;
                                    }
                                }
                                if (b1 == false)
                                {
                                    MaterialType mtp8 = new MaterialType();
                                    mtp8.Name = mtr2.Type;
                                    mtp8.mat.Add(mtr2);
                                    MaterialTypeList.Add(mtp8);
                                    MaterialTypeNameList.Add(mtp8.Name);
                                }
                                mtp.mat.Remove(mtr);
                            }
                            if (mtp.mat.Count==0)
                            {
                                foreach(String str in MaterialTypeNameList)
                                {
                                    if(str==mtp.Name)
                                    {
                                        MaterialTypeNameList.Remove(str);
                                        break;
                                    }
                                }
                                MaterialTypeList.Remove(mtp);
                            }
                            template.Save("TemplateLibrary.xml");
                            dataRefresh();
                            materialChange = false;
                            return;
                        }
                    }
                }

            }
            if (ha == false)
            {
                bool tem = false;
                foreach(MaterialType mtp2 in MaterialTypeList)
                {
                    if(mtp2.Name==materialType.Text)
                    {
                        Material mtr = new Material();
                        mtr.Name = materialName.Text;
                        mtr.Albedo = materialAlbedo.Text;
                        mtr.Comments = materialComment.Text;
                        mtr.Conductivity = materialConductivity.Text;
                        mtr.Emissivity = materialEmissivity.Text;
                        mtr.Source = materialSource.Text;
                        mtr.Type = materialType.Text;
                        mtr.VHC = materialVHC.Text;
                        mtp2.mat.Add(mtr);
                        tem = true;
                    }
                }
                if(tem==false)
                {
                    MaterialType mtp3 = new MaterialType();
                    mtp3.Name = materialType.Text;
                    MaterialTypeList.Add(mtp3);
                    MaterialTypeNameList.Add(mtp3.Name);
                    Material mtr = new Material();
                    mtr.Name = materialName.Text;
                    mtr.Albedo = materialAlbedo.Text;
                    mtr.Comments = materialComment.Text;
                    mtr.Conductivity = materialConductivity.Text;
                    mtr.Emissivity = materialEmissivity.Text;
                    mtr.Source = materialSource.Text;
                    mtr.Type = materialType.Text;
                    mtr.VHC = materialVHC.Text;
                    mtp3.mat.Add(mtr);
                }
                XmlNode op = template.SelectSingleNode("LibSerializable/OpaqueMaterials");
                XmlNode opan = template.CreateElement("OpaqueMaterial");
                XmlNodeList xl = template.GetElementsByTagName("OpaqueMaterial");
                bool b2 = false;
                foreach (XmlNode no in xl)
                {
                    XmlNode NameA = no.SelectSingleNode("Name");
                    if (NameA.InnerText == currentMaterialName)
                    {
                        opan = no.Clone();
                        b2 = true;
                        break;
                    }
                }
                if(b2==false)
                {
                    XmlNode n1 = template.CreateElement("Name");
                    XmlNode n2 = template.CreateElement("Type");
                    XmlNode n3 = template.CreateElement("Comments");
                    XmlNode n4 = template.CreateElement("VHC");
                    XmlNode n5 = template.CreateElement("DataSource");
                    XmlNode n6 = template.CreateElement("Conductivity");
                    XmlNode n7 = template.CreateElement("Emissivity");
                    XmlNode n8 = template.CreateElement("Albedo");
                    opan.AppendChild(n1);
                    opan.AppendChild(n2);
                    opan.AppendChild(n3);
                    opan.AppendChild(n4);
                    opan.AppendChild(n5);
                    opan.AppendChild(n6);
                    opan.AppendChild(n7);
                    opan.AppendChild(n8);
                }
                opan.SelectSingleNode("Name").InnerText = materialName.Text;
                opan.SelectSingleNode("Type").InnerText = materialType.Text;
                opan.SelectSingleNode("Comments").InnerText = materialComment.Text;
                opan.SelectSingleNode("DataSource").InnerText = materialSource.Text;
                opan.SelectSingleNode("Conductivity").InnerText = materialConductivity.Text;
                opan.SelectSingleNode("VHC").InnerText = materialVHC.Text;
                opan.SelectSingleNode("Albedo").InnerText = materialAlbedo.Text;
                opan.SelectSingleNode("Emissivity").InnerText = materialEmissivity.Text;
                op.AppendChild(opan);
                foreach (String k in MaterialNameList)
                {
                    if (k == "")
                    {
                        MaterialNameList.Remove(k);
                        break;
                    }
                }
                MaterialNameList.Add(materialName.Text);
                MaterialNameList.Add("");
            }
            foreach (MaterialType mtps in MaterialTypeList)
            {
                foreach (Material mtrs in mtps.mat)
                {
                    if (mtrs.Name == currentMaterialName)
                    {
                        Material mtr2 = mtrs;
                        mtps.mat.Remove(mtrs);
                        mtps.mat.Add(mtr2);
                        currentMaterialName = "";
                        break;
                    }
                }
            }
            template.Save("TemplateLibrary.xml");
            dataRefresh();
            materialChange = false;
        }
        private void assemblySave_Click(object sender, RoutedEventArgs e)
        {
            if (assemblyName.Text == "")
            {
                MessageBox.Show("Please choose a name!");
                return;
            }
            if (assemblyType.Text == "")
            {
                MessageBox.Show("Please specify the type!");
                return;
            }
            bool ha = false;
            foreach (AssemblyType mtp in AssemblyTypeList)
            {
                foreach (Assembly mtr in mtp.ase)
                {
                    if (mtr.Name == assemblyName.Text)
                    {
                        string message = "There is already an assembly with this name, do you want to replace it?";
                        string captain = "UWG";
                        MessageBoxButton buttons = MessageBoxButton.OKCancel;
                        var result = MessageBox.Show(message, captain, buttons);
                        if (result == MessageBoxResult.Cancel) return;
                        if (result == MessageBoxResult.OK)
                        {

                            XmlNodeList xl = template.GetElementsByTagName("OpaqueConstruction");
                            foreach (XmlNode no in xl)
                            {
                                XmlNode NameA = no.SelectSingleNode("Name");
                                if (NameA.InnerText == mtr.Name)
                                {
                                    no.SelectSingleNode("Type").InnerText = assemblyType.Text;
                                    no.SelectSingleNode("Comments").InnerText = assemblyComment.Text;
                                    no.SelectSingleNode("DataSource").InnerText = assemblySource.Text;
                                    //Layer!!!!
                                    XmlNode laye = template.CreateElement("Layers");
                                    int i = 0;
                                    for(i=0;i!=4;i++)
                                    {
                                        XmlNode la = template.CreateElement("OpaqueLayer");
                                        XmlNode ty = template.CreateElement("Type");
                                        ty.InnerText = "OpaqueMaterial";
                                        XmlNode mn = template.CreateElement("MaterialName");
                                        if (i == 0) mn.InnerText = assemblyLayer1.Text;
                                        if (i == 1) mn.InnerText = assemblyLayer2.Text;
                                        if (i == 2) mn.InnerText = assemblyLayer3.Text;
                                        if (i == 3) mn.InnerText = assemblyLayer4.Text;
                                        XmlNode th = template.CreateElement("Thickness");
                                        if (i == 0) th.InnerText = assemblyThickness1.Text;
                                        if (i == 1) th.InnerText = assemblyThickness2.Text;
                                        if (i == 2) th.InnerText = assemblyThickness3.Text;
                                        if (i == 3) th.InnerText = assemblyThickness4.Text;
                                        la.AppendChild(ty);
                                        la.AppendChild(mn);
                                        la.AppendChild(th);
                                        laye.AppendChild(la);
                                    }
                                    XmlNode olaye = no.SelectSingleNode("Layers");
                                    no.RemoveChild(olaye);
                                    no.AppendChild(laye);
                                    ha = true;
                                    break;
                                }
                            }
                            if (mtr.Type == assemblyType.Text)
                            {
                                mtr.Source = assemblySource.Text;
                                mtr.Type = assemblyType.Text;
                                mtr.Comments = assemblyComment.Text;
                                //Layer!!!!
                                mtr.Layer[0] = assemblyLayer1.Text;
                                mtr.Layer[1] = assemblyLayer2.Text;
                                mtr.Layer[2] = assemblyLayer3.Text;
                                mtr.Layer[3] = assemblyLayer4.Text;
                                mtr.Thickness[0] = assemblyThickness1.Text;
                                mtr.Thickness[1] = assemblyThickness2.Text;
                                mtr.Thickness[2] = assemblyThickness3.Text;
                                mtr.Thickness[3] = assemblyThickness4.Text;
                            }
                            else
                            {
                                Assembly mtr2 = mtr;
                                mtr2.Source = assemblySource.Text;
                                mtr2.Type = assemblyType.Text;
                                mtr2.Comments = assemblyComment.Text;
                                //Layer!!!!
                                mtr2.Layer[0] = assemblyLayer1.Text;
                                mtr2.Layer[1] = assemblyLayer2.Text;
                                mtr2.Layer[2] = assemblyLayer3.Text;
                                mtr2.Layer[3] = assemblyLayer4.Text;
                                mtr2.Thickness[0] = assemblyThickness1.Text;
                                mtr2.Thickness[1] = assemblyThickness2.Text;
                                mtr2.Thickness[2] = assemblyThickness3.Text;
                                mtr2.Thickness[3] = assemblyThickness4.Text;
                                bool b1 = false;
                                foreach (AssemblyType mtp9 in AssemblyTypeList)
                                {
                                    if (mtp9.Name == mtr2.Type)
                                    {
                                        mtp9.ase.Add(mtr2);
                                        b1 = true;
                                    }
                                }
                                if (b1 == false)
                                {
                                    AssemblyType mtp8 = new AssemblyType();
                                    mtp8.Name = mtr2.Type;
                                    mtp8.ase.Add(mtr2);
                                    AssemblyTypeList.Add(mtp8);
                                    AssemblyTypeNameList.Add(mtp8.Name);
                                }
                                mtp.ase.Remove(mtr);
                            }
                            if (mtp.ase.Count == 0)
                            {
                                foreach (String str in AssemblyTypeNameList)
                                {
                                    if (str == mtp.Name)
                                    {
                                        AssemblyTypeNameList.Remove(str);
                                        break;
                                    }
                                }
                                AssemblyTypeList.Remove(mtp);
                            }
                            template.Save("TemplateLibrary.xml");
                            dataRefresh();
                            assemblyChange = false;
                            return;
                        }
                    }
                }

            }
            if (ha == false)
            {
                bool tem = false;
                foreach (AssemblyType mtp2 in AssemblyTypeList)
                {
                    if (mtp2.Name == assemblyType.Text)
                    {
                        Assembly mtr = new Assembly();
                        mtr.Name = assemblyName.Text;
                        mtr.Source = assemblySource.Text;
                        mtr.Type = assemblyType.Text;
                        mtr.Comments = assemblyComment.Text;
                        mtr.Layer[0] = assemblyLayer1.Text;
                        mtr.Layer[1] = assemblyLayer2.Text;
                        mtr.Layer[2] = assemblyLayer3.Text;
                        mtr.Layer[3] = assemblyLayer4.Text;
                        mtr.Thickness[0] = assemblyThickness1.Text;
                        mtr.Thickness[1] = assemblyThickness2.Text;
                        mtr.Thickness[2] = assemblyThickness3.Text;
                        mtr.Thickness[3] = assemblyThickness4.Text;
                        mtp2.ase.Add(mtr);
                        tem = true;
                    }
                }
                if (tem == false)
                {
                    AssemblyType mtp3 = new AssemblyType();
                    mtp3.Name = assemblyType.Text;
                    AssemblyTypeList.Add(mtp3);
                    AssemblyTypeNameList.Add(mtp3.Name);
                    Assembly mtr = new Assembly();
                    mtr.Name = assemblyName.Text;
                    mtr.Source = assemblySource.Text;
                    mtr.Type = assemblyType.Text;
                    mtr.Comments = assemblyComment.Text;
                    mtr.Layer[0] = assemblyLayer1.Text;
                    mtr.Layer[1] = assemblyLayer2.Text;
                    mtr.Layer[2] = assemblyLayer3.Text;
                    mtr.Layer[3] = assemblyLayer4.Text;
                    mtr.Thickness[0] = assemblyThickness1.Text;
                    mtr.Thickness[1] = assemblyThickness2.Text;
                    mtr.Thickness[2] = assemblyThickness3.Text;
                    mtr.Thickness[3] = assemblyThickness4.Text;
                    mtp3.ase.Add(mtr);
                }
                XmlNode op = template.SelectSingleNode("LibSerializable/OpaqueConstructions");
                XmlNode opan = template.CreateElement("OpaqueConstruction");
                XmlNodeList xl = template.GetElementsByTagName("OpaqueConstruction");
                bool b2 = false;
                foreach (XmlNode no in xl)
                {
                    XmlNode NameA = no.SelectSingleNode("Name");
                    if (NameA.InnerText == currentAssemblyName)
                    {
                        opan = no.Clone();
                        XmlNode olaye = opan.SelectSingleNode("Layers");
                        opan.RemoveChild(olaye);
                        XmlNode laye = template.CreateElement("Layers");
                        int i = 0;
                        for (i = 0; i != 4; i++)
                        {
                            XmlNode la = template.CreateElement("OpaqueLayer");
                            XmlNode ty = template.CreateElement("Type");
                            ty.InnerText = "OpaqueMaterial";
                            XmlNode mn = template.CreateElement("MaterialName");
                            if (i == 0) mn.InnerText = assemblyLayer1.Text;
                            if (i == 1) mn.InnerText = assemblyLayer2.Text;
                            if (i == 2) mn.InnerText = assemblyLayer3.Text;
                            if (i == 3) mn.InnerText = assemblyLayer4.Text;
                            XmlNode th = template.CreateElement("Thickness");
                            if (i == 0) th.InnerText = assemblyThickness1.Text;
                            if (i == 1) th.InnerText = assemblyThickness2.Text;
                            if (i == 2) th.InnerText = assemblyThickness3.Text;
                            if (i == 3) th.InnerText = assemblyThickness4.Text;
                            la.AppendChild(ty);
                            la.AppendChild(mn);
                            la.AppendChild(th);
                            laye.AppendChild(la);
                        }
                        opan.AppendChild(laye);
                        b2 = true;
                        break;
                    }
                }
                if (b2 == false)
                {
                    XmlNode laye = template.CreateElement("Layers");
                    int i = 0;
                    for (i = 0; i != 4; i++)
                    {
                        XmlNode la = template.CreateElement("OpaqueLayer");
                        XmlNode ty = template.CreateElement("Type");
                        ty.InnerText = "OpaqueMaterial";
                        XmlNode mn = template.CreateElement("MaterialName");
                        if (i == 0) mn.InnerText = assemblyLayer1.Text;
                        if (i == 1) mn.InnerText = assemblyLayer2.Text;
                        if (i == 2) mn.InnerText = assemblyLayer3.Text;
                        if (i == 3) mn.InnerText = assemblyLayer4.Text;
                        XmlNode th = template.CreateElement("Thickness");
                        if (i == 0) th.InnerText = assemblyThickness1.Text;
                        if (i == 1) th.InnerText = assemblyThickness2.Text;
                        if (i == 2) th.InnerText = assemblyThickness3.Text;
                        if (i == 3) th.InnerText = assemblyThickness4.Text;
                        la.AppendChild(ty);
                        la.AppendChild(mn);
                        la.AppendChild(th);
                        laye.AppendChild(la);
                    }
                    opan.AppendChild(laye);
                    XmlNode n1 = template.CreateElement("Name");
                    XmlNode n2 = template.CreateElement("Type");
                    XmlNode n3 = template.CreateElement("Comments");
                    XmlNode n4 = template.CreateElement("DataSource");
                    opan.AppendChild(n1);
                    opan.AppendChild(n2);
                    opan.AppendChild(n3);
                    opan.AppendChild(n4);
                }
                opan.SelectSingleNode("Name").InnerText = assemblyName.Text;
                opan.SelectSingleNode("Type").InnerText = assemblyType.Text;
                opan.SelectSingleNode("Comments").InnerText = assemblyComment.Text;
                opan.SelectSingleNode("DataSource").InnerText = assemblySource.Text;
                op.AppendChild(opan);
            }
            foreach (AssemblyType mtps in AssemblyTypeList)
            {
                foreach (Assembly mtrs in mtps.ase)
                {
                    if (mtrs.Name == currentMaterialName)
                    {
                        Assembly mtr2 = mtrs;
                        mtps.ase.Remove(mtrs);
                        mtps.ase.Add(mtr2);
                        currentMaterialName = "";
                        break;
                    }
                }
            }
            template.Save("TemplateLibrary.xml");
            dataRefresh();
            assemblyChange = false;

        }
        private void glazingSave_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == "")
            {
                MessageBox.Show("Please choose a name!");
                return;
            }
            if (Name.Text == "New Glazing")
            {
                MessageBox.Show("Please change the glazing name! It can't be 'New Glazing'! ");
                return;
            }
            if (WWR.Text == "")
            {
                MessageBox.Show("Please set the Wall to Window Ratio!");
                return;
            }
            if (UValue.Text == "")
            {
                MessageBox.Show("Please set the Total U-Value!");
                return;
            }
            if (SHGC.Text == "")
            {
                MessageBox.Show("Please set the Total Heat Gain Coefficient!");
                return;
            }
            bool ha = false;
            foreach (Glazing mtp in GlazingList)
            {
                if (mtp.Name == Name.Text)
                {
                    string message = "There is already a glazing with this name, do you want to replace it?";
                    string captain = "UWG";
                    MessageBoxButton buttons = MessageBoxButton.OKCancel;
                    var result = MessageBox.Show(message, captain, buttons);
                    if (result == MessageBoxResult.Cancel) return;
                    if (result == MessageBoxResult.OK)
                    {

                        XmlNodeList xl = template.GetElementsByTagName("GlazingConstruction");
                        foreach (XmlNode no in xl)
                        {
                            XmlNode NameA = no.SelectSingleNode("Name");
                            if (NameA.InnerText == mtp.Name)
                            {
                                no.SelectSingleNode("WWR").InnerText = WWR.Text;
                                no.SelectSingleNode("UValue").InnerText = UValue.Text;
                                no.SelectSingleNode("SHGC").InnerText = SHGC.Text;
                                ha = true;
                                break;
                            }
                        }
                        mtp.WWR = WWR.Text;
                        mtp.UValue = UValue.Text;
                        mtp.SHGC = SHGC.Text;
                        template.Save("TemplateLibrary.xml");
                        dataRefresh();
                        glazingChange = false;
                        return;
                    }
                }
            }
            if (ha == false)
            {
                Glazing mtr = new Glazing();
                mtr.Name = Name.Text;
                mtr.WWR = WWR.Text;
                mtr.UValue = UValue.Text;
                mtr.SHGC = SHGC.Text;
                GlazingList.Add(mtr);
                XmlNode op = template.SelectSingleNode("LibSerializable/GlazingConstructions");
                XmlNode opan = template.CreateElement("GlazingConstruction");
                XmlNodeList xl = template.GetElementsByTagName("GlazingConstruction");
                opan = xl[0].Clone();
                opan.SelectSingleNode("Name").InnerText = Name.Text;
                opan.SelectSingleNode("WWR").InnerText = WWR.Text;
                opan.SelectSingleNode("UValue").InnerText = UValue.Text;
                opan.SelectSingleNode("SHGC").InnerText = SHGC.Text;
                op.AppendChild(opan);
            }
            template.Save("TemplateLibrary.xml");
            dataRefresh();
            glazingChange = false;
        }
        private void scheduleSave_Click(object sender, RoutedEventArgs e)
        {
            if (scheduleName.Text == "")
            {
                MessageBox.Show("Please choose a name!");
                return;
            }
            if (scheduleType.Text == "")
            {
                MessageBox.Show("Please specify the type!");
                return;
            }
            foreach (ScheduleType skt in ScheduleTypeList)
            {
                foreach (Schedule skd in skt.sch)
                {
                    if (skd.Name == scheduleName.Text)
                    {
                        string message = "There is already a schedule with this name, do you want to replace it?";
                        string captain = "UWG";
                        MessageBoxButton buttons = MessageBoxButton.OKCancel;
                        var result = MessageBox.Show(message, captain, buttons);
                        if (result == MessageBoxResult.Cancel) return;
                        if (result == MessageBoxResult.OK)
                        {
                            foreach(XmlNode xn in template.GetElementsByTagName("WeekSchedule"))
                            {
                                if (xn.SelectSingleNode("Name").InnerText == scheduleName.Text)
                                {
                                    xn.ParentNode.RemoveChild(xn);
                                    break;
                                }
                            }
                            foreach (XmlNode xn in template.GetElementsByTagName("DaySchedule"))
                            {
                                if (xn.SelectSingleNode("Name").InnerText == scheduleName.Text + "_Weekdays")
                                {
                                    xn.ParentNode.RemoveChild(xn);
                                    break;
                                }
                            }
                            foreach (XmlNode xn in template.GetElementsByTagName("DaySchedule"))
                            {
                                if (xn.SelectSingleNode("Name").InnerText == scheduleName.Text + "_Saturday")
                                {
                                    xn.ParentNode.RemoveChild(xn);
                                    break;
                                }
                            }
                            foreach (XmlNode xn in template.GetElementsByTagName("DaySchedule"))
                            {
                                if (xn.SelectSingleNode("Name").InnerText == scheduleName.Text + "_Sunday")
                                {
                                    xn.ParentNode.RemoveChild(xn);
                                    break;
                                }
                            }
                            foreach(String str in ScheduleNameList)
                            {
                                if(str==skd.Name)
                                {
                                    ScheduleNameList.Remove(str);
                                    break;
                                }
                            }
                            if (skd.Type != scheduleType.Text)
                            {
                                scheduleSave = true;
                                skt.sch.Remove(skd);
                            }
                            break;
                        }
                    }
                }
            }
            XmlNode no = template.GetElementsByTagName("WeekSchedule")[0].Clone();
            no.SelectSingleNode("Name").InnerText = scheduleName.Text;
            no.SelectSingleNode("Type").InnerText = scheduleType.Text;
            no.SelectSingleNode("Comments").InnerText = scheduleComment.Text;
            no.SelectSingleNode("DataSource").InnerText = scheduleSource.Text;
            no.SelectSingleNode("Days").SelectNodes("string")[0].InnerText = scheduleName.Text + "_Weekdays";
            no.SelectSingleNode("Days").SelectNodes("string")[1].InnerText = scheduleName.Text + "_Weekdays";
            no.SelectSingleNode("Days").SelectNodes("string")[2].InnerText = scheduleName.Text + "_Weekdays";
            no.SelectSingleNode("Days").SelectNodes("string")[3].InnerText = scheduleName.Text + "_Weekdays";
            no.SelectSingleNode("Days").SelectNodes("string")[4].InnerText = scheduleName.Text + "_Weekdays";
            no.SelectSingleNode("Days").SelectNodes("string")[5].InnerText = scheduleName.Text + "_Saturday";
            no.SelectSingleNode("Days").SelectNodes("string")[6].InnerText = scheduleName.Text + "_Sunday";
            template.GetElementsByTagName("WeekSchedules")[0].AppendChild(no);
            //Days!!!
            XmlNode wkd = template.CreateElement("DaySchedule");
            XmlNode wkdn = template.CreateElement("Name");
            XmlNode wkdt = template.CreateElement("Type");
            XmlNode wkdd = template.CreateElement("DataSource");
            XmlNode wkdc = template.CreateElement("Comments");
            XmlNode wkdv = template.CreateElement("Values");
            wkdt.InnerText = scheduleType.Text;
            wkdd.InnerText = scheduleSource.Text;
            wkd.AppendChild(wkdn);
            wkd.AppendChild(wkdt);
            wkd.AppendChild(wkdd);
            wkd.AppendChild(wkdc);
            wkd.AppendChild(wkdv);
            XmlNode std = wkd.Clone();
            XmlNode sud = wkd.Clone();
            wkd.SelectSingleNode("Name").InnerText = scheduleName.Text + "_Weekdays";
            std.SelectSingleNode("Name").InnerText = scheduleName.Text + "_Saturday";
            sud.SelectSingleNode("Name").InnerText = scheduleName.Text + "_Sunday";
            int i = 0;
            foreach (Slider sd in weekdaysGrid.Children)
            {
                XmlNode str = template.CreateElement("double");
                str.InnerText = Convert.ToString(sd.Value);
                wkd.SelectSingleNode("Values").AppendChild(str);
                i++;
                if (i == 24) break;
            }
            i = 0;
            foreach (Slider sd in SaturdayGrid.Children)
            {
                XmlNode str = template.CreateElement("double");
                str.InnerText = Convert.ToString(sd.Value);
                std.SelectSingleNode("Values").AppendChild(str);
                i++;
                if (i == 24) break;
            }
            i = 0;
            foreach (Slider sd in SundayGrid.Children)
            {
                XmlNode str = template.CreateElement("double");
                str.InnerText = Convert.ToString(sd.Value);
                sud.SelectSingleNode("Values").AppendChild(str);
                i++;
                if (i == 24) break;
            }
            template.GetElementsByTagName("DaySchedules")[0].AppendChild(wkd);
            template.GetElementsByTagName("DaySchedules")[0].AppendChild(std);
            template.GetElementsByTagName("DaySchedules")[0].AppendChild(sud);
            bool tem1 = false;
            foreach(ScheduleType tp in ScheduleTypeList)
            {
                foreach(Schedule mo in tp.sch)
                {
                    if(mo.Name == scheduleName.Text)
                    {
                        tem1 = true;
                        mo.Source = scheduleSource.Text;
                        mo.Type = scheduleType.Text;
                        mo.Comments = scheduleComment.Text;
                        i = 0;
                        foreach (Slider sd in weekdaysGrid.Children)
                        {
                            mo.Weekdays[i] = Convert.ToDouble(sd.Value);
                            i++;
                            if (i == 24) break;
                        }
                        i = 0;
                        foreach (Slider sd in SaturdayGrid.Children)
                        {
                            mo.Saturday[i] = Convert.ToDouble(sd.Value);
                            i++;
                            if (i == 24) break;
                        }
                        i = 0;
                        foreach (Slider sd in SundayGrid.Children)
                        {
                            mo.Sunday[i] = Convert.ToDouble(sd.Value);
                            i++;
                            if (i == 24) break;
                        }
                    }
                }
            }
            if (tem1 == false)
            {
                Schedule mo = new Schedule();

                mo.Name = scheduleName.Text;
                mo.Source = scheduleSource.Text;
                mo.Type = scheduleType.Text;
                mo.Comments = scheduleComment.Text;
                i = 0;
                foreach (Slider sd in weekdaysGrid.Children)
                {
                    mo.Weekdays[i] = Convert.ToDouble(sd.Value);
                    i++;
                    if (i == 24) break;
                }
                i = 0;
                foreach (Slider sd in SaturdayGrid.Children)
                {
                    mo.Saturday[i] = Convert.ToDouble(sd.Value);
                    i++;
                    if (i == 24) break;
                }
                i = 0;
                foreach (Slider sd in SundayGrid.Children)
                {
                    mo.Sunday[i] = Convert.ToDouble(sd.Value);
                    i++;
                    if (i == 24) break;
                }
                bool tem = false;
                foreach (ScheduleType tp in ScheduleTypeList)
                {
                    if (tp.Name == scheduleType.Text)
                    {
                        tp.sch.Add(mo);
                        tem = true;
                        break;
                    }
                }
                if (tem == false)
                {
                    ScheduleType tp = new ScheduleType();
                    tp.Name = mo.Type;
                    tp.sch.Add(mo);
                    ScheduleTypeList.Add(tp);
                    ScheduleTypeNameList.Add(mo.Type);
                }
                foreach (ScheduleType tp in ScheduleTypeList)
                {
                    if (tp.sch.Count == 0)
                    {
                        foreach (String str in ScheduleTypeNameList)
                        {
                            if (str == tp.Name)
                            {
                                ScheduleTypeNameList.Remove(str);
                                break;
                            }
                        }
                        ScheduleTypeList.Remove(tp);
                        break;
                    }
                }
            }


            template.Save("TemplateLibrary.xml");
            dataRefresh();
            scheduleChange = false;
            var item = scheduleTreeView.SelectedItem as Schedule;
            try
            {
                scheduleName.Text = item.Name;
                scheduleType.Text = item.Type;
                scheduleSource.Text = item.Source;
                scheduleComment.Text = item.Comments;
                i = 0;
                foreach (Slider sd in weekdaysGrid.Children)
                {
                    sd.Value = item.Weekdays[i];
                    i++;
                    if (i == 24) break;
                }
                i = 0;
                foreach (Slider sd in SaturdayGrid.Children)
                {
                    sd.Value = item.Saturday[i];
                    i++;
                    if (i == 24) break;
                }
                i = 0;
                foreach (Slider sd in SundayGrid.Children)
                {
                    sd.Value = item.Sunday[i];
                    i++;
                    if (i == 24) break;
                }
                currentScheduleName = item.Name;
                scheduleChange = false;
            }
            catch
            {
                scheduleName.Text = "";
                scheduleType.Text = "";
                scheduleSource.Text = "";
                scheduleComment.Text = "";
                i = 0;
                foreach (Slider sd in weekdaysGrid.Children)
                {
                    sd.Value = 0;
                    i++;
                    if (i == 24) break;
                }
                i = 0;
                foreach (Slider sd in SaturdayGrid.Children)
                {
                    sd.Value = 0;
                    i++;
                    if (i == 24) break;
                }
                i = 0;
                foreach (Slider sd in SundayGrid.Children)
                {
                    sd.Value = 0;
                    i++;
                    if (i == 24) break;
                }
                currentScheduleName = "";
                scheduleChange = false;
            }
        }
        private void heatSave_Click(object sender, RoutedEventArgs e)
        {
            if (heatName.Text == "")
            {
                MessageBox.Show("Please choose a name!");
                return;
            }
            if (heatName.Text == "New Building Template")
            {
                MessageBox.Show("Please change the glazing name! It can't be 'New Building Template'! ");
                return;
            }
            if (Occupancy.Text == "")
            {
                MessageBox.Show("Please choose an occupancy schedule!");
                return;
            }
            if (occupancyMax.Text == "")
            {
                MessageBox.Show("Please set the occupancy maximum value!");
                return;
            }
            if (Lights.Text == "")
            {
                MessageBox.Show("Please choose a lights schedule!");
                return;
            }
            if (lightsMax.Text == "")
            {
                MessageBox.Show("Please set the lights maximum value!");
                return;
            }
            if (Equipments.Text == "")
            {
                MessageBox.Show("Please choose an equipments schedule!");
                return;
            }
            if (equipMax.Text == "")
            {
                MessageBox.Show("Please set the equipments maximum value!");
                return;
            }
            if (Infiltration.Text == "")
            {
                MessageBox.Show("Please choose an infiltration schedule!");
                return;
            }
            if (infiltrationMax.Text == "")
            {
                MessageBox.Show("Please set the infiltration maximum value!");
                return;
            }
            if (Ventilation.Text == "")
            {
                MessageBox.Show("Please choose a ventilation schedule!");
                return;
            }
            if (ventilationMax.Text == "")
            {
                MessageBox.Show("Please set the ventilation maximum value!");
                return;
            }
            bool ha = false;
            foreach (Heat mtp in HeatList)
            {
                if (mtp.Name == heatName.Text)
                {
                    string message = "There is already a building template with this name, do you want to replace it?";
                    string captain = "UWG";
                    MessageBoxButton buttons = MessageBoxButton.OKCancel;
                    var result = MessageBox.Show(message, captain, buttons);
                    if (result == MessageBoxResult.Cancel) return;
                    if (result == MessageBoxResult.OK)
                    {

                        XmlNodeList xl = template.GetElementsByTagName("HeatTemplate");
                        foreach (XmlNode no in xl)
                        {
                            XmlNode NameA = no.SelectSingleNode("Name");
                            if (NameA.InnerText == mtp.Name)
                            {
                                no.SelectSingleNode("DataSource").InnerText = heatSource.Text;
                                no.SelectSingleNode("Comments").InnerText = heatComment.Text;
                                no.SelectSingleNode("OccupancySched").InnerText = Occupancy.Text;
                                no.SelectSingleNode("OccupancyMax").InnerText = occupancyMax.Text;
                                no.SelectSingleNode("LightsSched").InnerText = Lights.Text;
                                no.SelectSingleNode("LightsMax").InnerText = lightsMax.Text;
                                no.SelectSingleNode("EquipSched").InnerText = Equipments.Text;
                                no.SelectSingleNode("EquipMax").InnerText = equipMax.Text;
                                no.SelectSingleNode("InfiltrationSched").InnerText = Infiltration.Text;
                                no.SelectSingleNode("InfiltrationMax").InnerText = infiltrationMax.Text;
                                no.SelectSingleNode("VentilationSched").InnerText = Ventilation.Text;
                                no.SelectSingleNode("VentilationMax").InnerText = ventilationMax.Text;
                                ha = true;
                                break;
                            }
                        }
                        mtp.Source = heatSource.Text;
                        mtp.Comment = heatComment.Text;
                        mtp.OccupancySched = Occupancy.Text;
                        mtp.OccupancyMax = occupancyMax.Text;
                        mtp.LightsSched = Lights.Text;
                        mtp.LightsMax = lightsMax.Text;
                        mtp.EquipSched = Equipments.Text;
                        mtp.EquipMax = equipMax.Text;
                        mtp.InfiltrationSched = Infiltration.Text;
                        mtp.InfiltrationMax = infiltrationMax.Text;
                        mtp.VentilationSched = Ventilation.Text;
                        mtp.VentilationMax = ventilationMax.Text;
                        template.Save("TemplateLibrary.xml");
                        dataRefresh();
                        heatChange = false;
                        return;
                    }
                }
            }
            if (ha == false)
            {
                Heat mtr = new Heat();
                mtr.Source = heatSource.Text;
                mtr.Comment = heatComment.Text;
                mtr.OccupancySched = Occupancy.Text;
                mtr.OccupancyMax = occupancyMax.Text;
                mtr.LightsSched = Lights.Text;
                mtr.LightsMax = lightsMax.Text;
                mtr.EquipSched = Equipments.Text;
                mtr.EquipMax = equipMax.Text;
                mtr.InfiltrationSched = Infiltration.Text;
                mtr.InfiltrationMax = infiltrationMax.Text;
                mtr.VentilationSched = Ventilation.Text;
                mtr.VentilationMax = ventilationMax.Text;
                mtr.Name = heatName.Text;
                HeatList.Add(mtr);
                XmlNode op = template.SelectSingleNode("LibSerializable/HeatTemplates");
                XmlNode opan = template.CreateElement("HeatTemplate");
                XmlNodeList xl = template.GetElementsByTagName("HeatTemplate");
                opan = xl[0].Clone();
                opan.SelectSingleNode("DataSource").InnerText = heatSource.Text;
                opan.SelectSingleNode("Comments").InnerText = heatComment.Text;
                opan.SelectSingleNode("OccupancySched").InnerText = Occupancy.Text;
                opan.SelectSingleNode("OccupancyMax").InnerText = occupancyMax.Text;
                opan.SelectSingleNode("LightsSched").InnerText = Lights.Text;
                opan.SelectSingleNode("LightsMax").InnerText = lightsMax.Text;
                opan.SelectSingleNode("EquipSched").InnerText = Equipments.Text;
                opan.SelectSingleNode("EquipMax").InnerText = equipMax.Text;
                opan.SelectSingleNode("InfiltrationSched").InnerText = Infiltration.Text;
                opan.SelectSingleNode("InfiltrationMax").InnerText = infiltrationMax.Text;
                opan.SelectSingleNode("VentilationSched").InnerText = Ventilation.Text;
                opan.SelectSingleNode("VentilationMax").InnerText = ventilationMax.Text;
                opan.SelectSingleNode("Name").InnerText = heatName.Text;
                op.AppendChild(opan);
            }
            template.Save("TemplateLibrary.xml");
            dataRefresh();
            heatChange = false;
        }
        private void materialDelete_Click(object sender, RoutedEventArgs e)
        {
            string message = "Do you want to delete this material?";
            string captain = "UWG";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            var result = MessageBox.Show(message, captain, buttons);
            if (result == MessageBoxResult.Cancel) return;
            XmlNodeList xl = template.GetElementsByTagName("OpaqueMaterial");
            foreach (XmlNode no in xl)
            {
                XmlNode Na = no.SelectSingleNode("Name");
                if (Na.InnerText == materialName.Text)
                {
                    no.ParentNode.RemoveChild(no);
                    break;
                }
            }
            foreach(string mn in MaterialNameList)
            {
                if(mn==materialName.Text)
                {
                    MaterialNameList.Remove(mn);
                    break;
                }
            }
            template.Save("TemplateLibrary.xml");
     //       materialInitialize();
     //       materialTreeView.Items.Refresh();
            foreach(MaterialType mty in MaterialTypeList)
            {
                foreach(Material mtr in mty.mat)
                {
                    if (materialName.Text == mtr.Name)
                    {
                        mty.mat.Remove(mtr);
                        if (mty.mat.Count == 0)
                        {
                            foreach (String str in MaterialTypeNameList)
                            {
                                if (str == mty.Name)
                                {
                                    MaterialTypeNameList.Remove(str);
                                    break;
                                }
                            }
                            MaterialTypeList.Remove(mty);
                        }
                        dataRefresh();
                        materialChange = false;
                        return;
                    }
                }
            }
            dataRefresh();
            materialChange = false;
        }
        private void assemblyDelete_Click(object sender, RoutedEventArgs e)
        {
            string message = "Do you want to delete this assembly?";
            string captain = "UWG";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            var result = MessageBox.Show(message, captain, buttons);
            if (result == MessageBoxResult.Cancel) return;
            XmlNodeList xl = template.GetElementsByTagName("OpaqueConstruction");
            foreach (XmlNode no in xl)
            {
                XmlNode Na = no.SelectSingleNode("Name");
                if (Na.InnerText == assemblyName.Text)
                {
                    no.ParentNode.RemoveChild(no);
                    break;
                }
            }

            template.Save("TemplateLibrary.xml");
            //       materialInitialize();
            //       materialTreeView.Items.Refresh();
            foreach (AssemblyType mty in AssemblyTypeList)
            {
                foreach (Assembly mtr in mty.ase)
                {
                    if (assemblyName.Text == mtr.Name)
                    {
                        mty.ase.Remove(mtr);
                        if (mty.ase.Count == 0)
                        {
                            foreach (String str in AssemblyTypeNameList)
                            {
                                if (str == mty.Name)
                                {
                                    AssemblyTypeNameList.Remove(str);
                                    break;
                                }
                            }
                            AssemblyTypeList.Remove(mty);
                        }
                        dataRefresh();
                        assemblyChange = false;
                        return;
                    }
                }
            }
            dataRefresh();
            assemblyChange = false;
        }
        private void glazingDelete_Click(object sender, RoutedEventArgs e)
        {
            string message = "Do you want to delete this glazing?";
            string captain = "UWG";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            var result = MessageBox.Show(message, captain, buttons);
            if (result == MessageBoxResult.Cancel) return;
            XmlNodeList xl = template.GetElementsByTagName("GlazingConstruction");
            foreach (XmlNode no in xl)
            {
                XmlNode Na = no.SelectSingleNode("Name");
                if (Na.InnerText == Name.Text)
                {
                    no.ParentNode.RemoveChild(no);
                    break;
                }
            }

            template.Save("TemplateLibrary.xml");
            foreach (Glazing mty in GlazingList)
            {
                if (Name.Text == mty.Name)
                {
                    GlazingList.Remove(mty);
                    dataRefresh();
                    glazingChange = false;
                    return;
                }
            }
            dataRefresh();
            glazingChange = false;
        }
        private void scheduleDelete_Click(object sender, RoutedEventArgs e)
        {
            string message = "Do you want to delete this schedule?";
            string captain = "UWG";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            var result = MessageBox.Show(message, captain, buttons);
            if (result == MessageBoxResult.Cancel) return;
            foreach (XmlNode xn in template.GetElementsByTagName("WeekSchedule"))
            {
                if (xn.SelectSingleNode("Name").InnerText == scheduleName.Text)
                {
                    xn.ParentNode.RemoveChild(xn);
                    break;
                }
            }
            foreach (XmlNode xn in template.GetElementsByTagName("DaySchedule"))
            {
                if (xn.SelectSingleNode("Name").InnerText == scheduleName.Text + "_Weekdays")
                {
                    xn.ParentNode.RemoveChild(xn);
                    break;
                }
            }
            foreach (XmlNode xn in template.GetElementsByTagName("DaySchedule"))
            {
                if (xn.SelectSingleNode("Name").InnerText == scheduleName.Text + "_Saturday")
                {
                    xn.ParentNode.RemoveChild(xn);
                    break;
                }
            }
            foreach (XmlNode xn in template.GetElementsByTagName("DaySchedule"))
            {
                if (xn.SelectSingleNode("Name").InnerText == scheduleName.Text + "_Sunday")
                {
                    xn.ParentNode.RemoveChild(xn);
                    break;
                }
            }
            foreach (String str in ScheduleNameList)
            {
                if (str == scheduleName.Text)
                {
                    ScheduleNameList.Remove(str);
                    break;
                }
            }
            foreach (ScheduleType skt in ScheduleTypeList)
            {
                if (skt.Name == scheduleType.Text)
                {
                    foreach(Schedule skd in skt.sch)
                    {
                        if (skd.Name == scheduleName.Text)
                        {
                            skt.sch.Remove(skd);
                            break;
                        }
                    }
                    if (skt.sch.Count == 0)
                    {
                        foreach (String str in ScheduleTypeNameList)
                        {
                            if (str == skt.Name)
                            {
                                ScheduleTypeNameList.Remove(str);
                                break;
                            }
                        }
                        ScheduleTypeList.Remove(skt);
                        break;
                    }
                }
            }
            template.Save("TemplateLibrary.xml");
            dataRefresh();
            scheduleChange = false;
        }
        private void heatDelete_Click(object sender, RoutedEventArgs e)
        {
            string message = "Do you want to delete this building template?";
            string captain = "UWG";
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            var result = MessageBox.Show(message, captain, buttons);
            if (result == MessageBoxResult.Cancel) return;
            XmlNodeList xl = template.GetElementsByTagName("HeatTemplate");
            foreach (XmlNode no in xl)
            {
                XmlNode Na = no.SelectSingleNode("Name");
                if (Na.InnerText == heatName.Text)
                {
                    no.ParentNode.RemoveChild(no);
                    break;
                }
            }

            template.Save("TemplateLibrary.xml");
            foreach (Heat mty in HeatList)
            {
                if (heatName.Text == mty.Name)
                {
                    HeatList.Remove(mty);
                    dataRefresh();
                    heatChange = false;
                    return;
                }
            }
            dataRefresh();
            heatChange = false;
        }
        private void materialNew_Click(object sender, RoutedEventArgs e)
        {
            if(materialChange == true)
            {
                        string message = "You havn't saved your current template, do you want to start a new one?";
                        string captain = "UWG";
                        MessageBoxButton buttons = MessageBoxButton.OKCancel;
                        var result = MessageBox.Show(message, captain, buttons);
                        if (result == MessageBoxResult.Cancel) return;
            }
            materialName.Text = "";
            materialSource.Text = "";
            materialType.Text = "";
            materialVHC.Text = "";
            materialAlbedo.Text = "";
            materialComment.Text = "";
            materialConductivity.Text = "";
            materialEmissivity.Text = "";
            materialDeleteButton.IsEnabled = false;
            foreach(MaterialType mtp in MaterialTypeList)
            {
                foreach(Material mtr in mtp.mat)
                {
                    if(mtr.Name==currentMaterialName)
                    {
                        Material mtr2 = mtr;
                        mtp.mat.Remove(mtr);
                        mtp.mat.Add(mtr2);
                        currentMaterialName = "";
                        materialChange = false;
                        return;
                    }
                }
            }
            materialChange = false;
        }
        private void assemblyNew_Click(object sender, RoutedEventArgs e)
        {
            if (materialChange == true)
            {
                string message = "You havn't saved your current template, do you want to start a new one?";
                string captain = "UWG";
                MessageBoxButton buttons = MessageBoxButton.OKCancel;
                var result = MessageBox.Show(message, captain, buttons);
                if (result == MessageBoxResult.Cancel) return;
            }
            assemblyInitialize();
            dataRefresh();
            materialChange = false;
        }
        private void glazingNew_Click(object sender, RoutedEventArgs e)
        {
            if (glazingChange == true)
            {
                string message = "You havn't saved your current template, do you want to start a new one?";
                string captain = "UWG";
                MessageBoxButton buttons = MessageBoxButton.OKCancel;
                var result = MessageBox.Show(message, captain, buttons);
                if (result == MessageBoxResult.Cancel) return;
            }
            glazingInitialize();
            dataRefresh();
            glazingChange = false;
        }
        private void heatNew_Click(object sender, RoutedEventArgs e)
        {
            if (heatChange == true)
            {
                string message = "You havn't saved your current template, do you want to start a new one?";
                string captain = "UWG";
                MessageBoxButton buttons = MessageBoxButton.OKCancel;
                var result = MessageBox.Show(message, captain, buttons);
                if (result == MessageBoxResult.Cancel) return;
            }
            heatInitialize();
            dataRefresh();
            heatChange = false;
        }

        private void scheduleNew_Click(object sender, RoutedEventArgs e)
        {
            if (glazingChange == true)
            {
                string message = "You havn't saved your current template, do you want to start a new one?";
                string captain = "UWG";
                MessageBoxButton buttons = MessageBoxButton.OKCancel;
                var result = MessageBox.Show(message, captain, buttons);
                if (result == MessageBoxResult.Cancel) return;
            }
            scheduleInitialize();
            dataRefresh();
            scheduleChange = false;
        }
        private void materialValue_Change(object sender, RoutedEventArgs e)
        {
            materialChange = true;
            materialDeleteButton.IsEnabled = false;
        }
        private void assemblyValue_Change(object sender, RoutedEventArgs e)
        {
            assemblyChange = true;
            assemblyDeleteButton.IsEnabled = false;
        }
        private void glazingValue_Change(object sender, RoutedEventArgs e)
        {
            glazingChange = true;
            glazingDeleteButton.IsEnabled = false;
        }
        private void scheduleValue_Change(object sender, RoutedEventArgs e)
        {
            scheduleChange = true;
            scheduleDeleteButton.IsEnabled = false;
        }
        private void heatValue_Change(object sender, RoutedEventArgs e)
        {
            heatChange = true;
      //      scheduleDeleteButton.IsEnabled = false;
        }
        private void constructionSelect(object sender, RoutedEventArgs e)
        {
            if(constructionTab.SelectedIndex==0)
            {
                materialTreeView.Visibility = System.Windows.Visibility.Visible;
                assemblyTreeView.Visibility = System.Windows.Visibility.Hidden;
                glazingTreeView.Visibility = System.Windows.Visibility.Hidden;
            }
            if (constructionTab.SelectedIndex == 1)
            {
                materialTreeView.Visibility = System.Windows.Visibility.Hidden;
                assemblyTreeView.Visibility = System.Windows.Visibility.Visible;
                glazingTreeView.Visibility = System.Windows.Visibility.Hidden;
            }
            if (constructionTab.SelectedIndex == 2)
            {
                materialTreeView.Visibility = System.Windows.Visibility.Hidden;
                assemblyTreeView.Visibility = System.Windows.Visibility.Hidden;
                glazingTreeView.Visibility = System.Windows.Visibility.Visible;
            }
        }
        private void buildingSelect(object sender, RoutedEventArgs e)
        {
            if (buildingTab.SelectedIndex == 0)
            {
                scheduleTreeView.Visibility = System.Windows.Visibility.Visible;
                heatTreeView.Visibility = System.Windows.Visibility.Hidden;
            }
            if (buildingTab.SelectedIndex == 1)
            {
                scheduleTreeView.Visibility = System.Windows.Visibility.Hidden;
                heatTreeView.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}

  //          <HierarchicalDataTemplate.Triggers>
      //          <DataTrigger Binding="{Binding Path=Nodetype}" Value="Element">
       //             <Setter TargetName="text" Property="Text" Value="{Binding Path=Name}"/>
       //         </DataTrigger> 
      //      </HierarchicalDataTemplate.Triggers>




 //   <Window.Resources>
   //     <HierarchicalDataTemplate x:Key="Nodetemplate">
     //       <TextBlock x:Name="text" Text="{Binding Path=Name}"/>
     //       <HierarchicalDataTemplate.ItemsSource>
     //           <Binding XPath="child::node()"/>
     //       </HierarchicalDataTemplate.ItemsSource>
//
//        </HierarchicalDataTemplate>
//        <XmlDataProvider x:Key="xmlDataProvider"/>
//    </Window.Resources>