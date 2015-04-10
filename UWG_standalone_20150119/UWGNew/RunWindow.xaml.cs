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

namespace UWG
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class RunWindow : Window
    {
        simuParam p = new simuParam();
        int numberOfSimulation = 1;
        public RunWindow()
        {
            InitializeComponent();
            p.simuStartMonthValidate = "1";
            p.simuStartDayValidate = "1";
            p.simuDurationValidate = "365";
            uwgRun.DataContext = p;
        }
        private String epwPath = "";
        private String epwFileName = "";
        private String epwFileName1 = "";
        private String epwFileName2 = "";
        private String epwFileName3 = "";
        private String epwFileName4 = "";
        private String epwFileName5 = "";
        private String filename = "";
        private String xmlUWGPath = "";
        private String xmlUWGFileName = "";
        private String filename_xmlUWG = "";
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

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            numberOfSimulation++;
            minus.IsEnabled = true;
            if(numberOfSimulation==2)
            {
                rs1.Visibility = System.Windows.Visibility.Visible;
                bb1.Visibility = System.Windows.Visibility.Visible;
                bx1.Visibility = System.Windows.Visibility.Visible;
            }
            if (numberOfSimulation == 3)
            {
                rs2.Visibility = System.Windows.Visibility.Visible;
                bb2.Visibility = System.Windows.Visibility.Visible;
                bx2.Visibility = System.Windows.Visibility.Visible;
            }
            if (numberOfSimulation == 4)
            {
                rs3.Visibility = System.Windows.Visibility.Visible;
                bb3.Visibility = System.Windows.Visibility.Visible;
                bx3.Visibility = System.Windows.Visibility.Visible;
            }
            if (numberOfSimulation == 5)
            {
                rs4.Visibility = System.Windows.Visibility.Visible;
                bb4.Visibility = System.Windows.Visibility.Visible;
                bx4.Visibility = System.Windows.Visibility.Visible;
            }
            if (numberOfSimulation == 6)
            {
                rs5.Visibility = System.Windows.Visibility.Visible;
                bb5.Visibility = System.Windows.Visibility.Visible;
                bx5.Visibility = System.Windows.Visibility.Visible;
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
                minus.IsEnabled = false;
            }
            if (numberOfSimulation == 3)
            {
                rs2.Visibility = System.Windows.Visibility.Collapsed;
                bb2.Visibility = System.Windows.Visibility.Collapsed;
                bx2.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (numberOfSimulation == 4)
            {
                rs3.Visibility = System.Windows.Visibility.Collapsed;
                bb3.Visibility = System.Windows.Visibility.Collapsed;
                bx3.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (numberOfSimulation == 5)
            {
                rs4.Visibility = System.Windows.Visibility.Collapsed;
                bb4.Visibility = System.Windows.Visibility.Collapsed;
                bx4.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (numberOfSimulation == 6)
            {
                rs5.Visibility = System.Windows.Visibility.Collapsed;
                bb5.Visibility = System.Windows.Visibility.Collapsed;
                bx5.Visibility = System.Windows.Visibility.Collapsed;
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
                feed.ef2 = "$2" + this.epwFileName;
                feed.ef3 = "$3" + this.epwFileName;
                feed.ef4 = "$4" + this.epwFileName;
                feed.ef5 = "$5" + this.epwFileName;
                feed.xp = this.xmlUWGPath;
                feed.xf = this.xmlUWGFileName;
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
                filename_xmlUWG = dlg.FileName;
                xmlFileEmpty.Text = filename_xmlUWG;
                //Get directory path only
                this.xmlUWGPath = System.IO.Path.GetDirectoryName(filename_xmlUWG);
                //Get only the file name
                this.xmlUWGFileName = System.IO.Path.GetFileName(filename_xmlUWG);
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
                this.xmlUWGFileName4 = System.IO.Path.GetFileName(filename_xmlUWG);
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
