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
using Microsoft.Office.Interop.Excel;

namespace UWG
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SimWindow : Window
    {
        simuParam p = new simuParam();
        int numberOfSimulation = 1;
        public SimWindow()
        {
            InitializeComponent();
            p.simuStartMonthValidate = "1";
            p.simuStartDayValidate = "1";
            p.simuDurationValidate = "365";
            uwgRun.DataContext = p;
        }
//         private String epwPath = "";
//         private String epwFileName = "";
//         private String epwFileName1 = "";
//         private String epwFileName2 = "";
//         private String epwFileName3 = "";
//         private String epwFileName4 = "";
//         private String epwFileName5 = "";
//         private String filename = "";
//         private String xmlUWGPath = "";
//         private String xmlUWGFileName = "";
//         private String filename_xmlUWG = "";
//         private String xmlUWGPath1 = "";
//         private String xmlUWGFileName1 = "";
//         private String filename_xmlUWG1 = "";
//         private String xmlUWGPath2 = "";
//         private String xmlUWGFileName2 = "";
//         private String filename_xmlUWG2 = "";
//         private String xmlUWGPath3 = "";
//         private String xmlUWGFileName3 = "";
//         private String filename_xmlUWG3 = "";
//         private String xmlUWGPath4 = "";
//         private String xmlUWGFileName4 = "";
//         private String filename_xmlUWG4 = "";
//         private String xmlUWGPath5 = "";
//         private String xmlUWGFileName5 = "";
//         private String filename_xmlUWG5 = "";
//         private String resultPath="";
//         private String resultFileName="";
//         private String resultFilePath="";

       
        private void check_(object sender, RoutedEventArgs e)
        {
            check();
        }
        private void back(object sender, EventArgs e)
        {
            this.IsEnabled = true;
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String mon = p.simuStartMonthValidate;
                String day = p.simuStartDayValidate;
                String dur = p.simuDurationValidate;

                feedBack feed = new feedBack();
                feed.runNum = numberOfSimulation;
                feed.ep = this.epwPath;
                feed.ef = this.epwFileName;
                feed.ef1 = "$1" + this.epwFileName;

                feed.xp = this.xmlUWGPath;
                feed.xf = this.xmlUWGFileName;

                feed.day = day;
                feed.mon = mon;
                feed.dur = dur;
                feed.rp = resultPathText.Text;
                feed.rf = resultName.Text + ".epw";

                feed.Show();
                feed.startrun();
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
                textBox1.Text = error.ToString();
            }
            //MessageBox.Show("UWG has finished running. Your urban weather file is created");
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
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
                filename = dlg.FileName;
                epwFileEmpty.Text = filename;
                //Get directory path only
                this.epwPath = System.IO.Path.GetDirectoryName(filename);
                //Get only the file name
                this.epwFileName = System.IO.Path.GetFileName(filename);
                check();
            };
        }

        //SELECT XML FILE FOR UWG
        private void check()
        {
            if(epwPath=="" || xmlUWGFileName == "" || resultPathText.Text=="" || resultName.Text=="")
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
                filename_xmlUWG = dlg.FileName;
                xmlFileEmpty.Text = filename_xmlUWG;
                //Get directory path only
                this.xmlUWGPath = System.IO.Path.GetDirectoryName(filename_xmlUWG);
                //Get only the file name
                this.xmlUWGFileName = System.IO.Path.GetFileName(filename_xmlUWG);
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
