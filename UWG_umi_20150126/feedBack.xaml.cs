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
using System.Threading;
using System.Windows.Media.Animation;

namespace UWG
{
    /// <summary>
    /// Interaction logic for feedBack.xaml
    /// </summary>
    public partial class feedBack : Window
    {
        public feedBack()
        {
            InitializeComponent();
        }
        public String ep;
        public String ef;
        public String ef1;
        public String ef2;
        public String ef3;
        public String ef4;
        public String ef5;
        public String xp;
        public String xf;
        public String xp1;
        public String xf1;
        public String xp2;
        public String xf2;
        public String xp3;
        public String xf3;
        public String xp4;
        public String xf4;
        public String xp5;
        public String xf5;
        public String mon;
        public String day;
        public String dur;
        public String rp;
        public String rf;
        public String rf1;
        public String rf2;
        public String rf3;
        public String rf4;
        public String rf5;
        public int runNum;
        public Process UWGs;
        public Process UWGs1;
        public Process UWGs2;
        public Process UWGs3;
        public Process UWGs4;
        public Process UWGs5;
        public int UWGst = 0;
        public int UWGst1 = 0;
        public int UWGst2 = 0;
        public int UWGst3 = 0;
        public int UWGst4 = 0;
        public int UWGst5 = 0;
        public int OnRun = 0;
        public void starten()
        {
            ProcessStartInfo startEn = new ProcessStartInfo();
            startEn.WorkingDirectory = "D:\\EnergyPlusV8-2-5";
            startEn.FileName = "EnergyPlus.exe";
            startEn.Arguments = "D:\\EnergyPlusV8-2-5\\ExampleFiles\\1ZoneEvapCooler.idf";
            Process.Start(startEn);
        }

        public void onClose(object sender, EventArgs e)
        {
            if(UWGst == -1)
            {
                return;
            }
            if(UWGst == 1)
            {
                OnRun--;
                UWGst++;
                MessageBox.Show("Simulation 1 run time error, please check input files!");
                return;
            }
            if (UWGst1 == 1)
            {
                OnRun--;
                UWGst1++;
                System.IO.File.Delete(System.IO.Path.Combine(ep, ef1));
                MessageBox.Show("Simulation 2 run time error, please check input files!");
                return;
            }
            if (UWGst2 == 1)
            {
                OnRun--;
                UWGst2++;
                System.IO.File.Delete(System.IO.Path.Combine(ep, ef2));
                MessageBox.Show("Simulation 3 run time error, please check input files!");
                return;
            }
            if (UWGst3 == 1)
            {
                OnRun--;
                UWGst3++;
                System.IO.File.Delete(System.IO.Path.Combine(ep, ef3));
                MessageBox.Show("Simulation 4 run time error, please check input files!");
                return;
            }
            if (UWGst4 == 1)
            {
                OnRun--;
                System.IO.File.Delete(System.IO.Path.Combine(ep, ef2));
                UWGst4++;
                MessageBox.Show("Simulation 5 run time error, please check input files!");
                return;
            }
            if (UWGst5 == 1)
            {
                OnRun--;
                UWGst5++;
                System.IO.File.Delete(System.IO.Path.Combine(ep, ef2));
                MessageBox.Show("Simulation 6 run time error, please check input files!");
                return;
            }
            OnRun--;
        }
    
        public void startrun()
        {
            OnRun++;
            ProcessStartInfo startUWG = new ProcessStartInfo();
            //startUWG.FileName = "C:\\Users\\anakano\\Documents\\Research\\UWG2.1\\For_Installer\\UWGv2.0.exe";
            startUWG.FileName = "UWGEngine.exe";
            //make sure there is space in between each of the four inputs and that folder extension ends with \\
            startUWG.Arguments = ep + "\\ " + ef + " " + xp + "\\ " + xf + " " + rp + "\\ " + rf;
            //startUWG.Arguments = ep + "\\ " + ef + " " + xp + "\\ " + xf + " " + rp + "\\ " + rf + " " + mon + " " + day + " " + dur;
            //startUWG.Arguments = this.epwPath + this.epwFileName + this.xmlPath + this.xmlFilename;
            startUWG.UseShellExecute = false;
            startUWG.CreateNoWindow = true;
            startUWG.RedirectStandardOutput = true;
            double maxx = System.Convert.ToDouble(dur);
            ProgressBar.Maximum = System.Convert.ToDouble(dur);
            UWGs = Process.Start(startUWG);
            UWGst = 1;
            UWGs.EnableRaisingEvents = true;
            UWGs.Exited += new EventHandler(onClose);
            StreamReader UWGreader = UWGs.StandardOutput;
            new Thread(() =>
            {
                String UWGreaderst;
                int value = 0;
                while (!UWGreader.EndOfStream)
                {
                    UWGreaderst = UWGreader.ReadLine();
                    this.Dispatcher.Invoke(new Action(() =>
                        {
                            if (UWGreaderst == "le")
                            {
                                ProgressBar.IsIndeterminate = false;
                                RunningInfo.Text = "Loading EPW file...";
                                DoubleAnimation ani = new DoubleAnimation(maxx, TimeSpan.FromSeconds(15));
                                ProgressBar.BeginAnimation(ProgressBar.ValueProperty, ani);
                            }
                            if (UWGreaderst == "lx")
                            {
                                RunningInfo.Text = "Loading XML file...";
                                DoubleAnimation ani1 = new DoubleAnimation(0, TimeSpan.FromSeconds(0));
                                ProgressBar.BeginAnimation(ProgressBar.ValueProperty, ani1);
                                DoubleAnimation ani2 = new DoubleAnimation(maxx, TimeSpan.FromSeconds(15));
                                ProgressBar.BeginAnimation(ProgressBar.ValueProperty, ani2);
                            }
                            if (UWGreaderst == "start")
                            {
                                DoubleAnimation ani1 = new DoubleAnimation(0, TimeSpan.FromSeconds(0));
                                ProgressBar.BeginAnimation(ProgressBar.ValueProperty, ani1);
                                ProgressBar.Value = 0;
                                RunningInfo.Text = "Generating Day " + System.Convert.ToString(1) + "...";
                            }
                            if (UWGreaderst == "+")
                            {
                                value++;
                                ProgressBar.Visibility = System.Windows.Visibility.Visible;
                                //                   ProgressBar.Value = value;
                                DoubleAnimation ani = new DoubleAnimation(value, TimeSpan.FromSeconds(.5));
                                ProgressBar.BeginAnimation(ProgressBar.ValueProperty, ani);
                                if (value == System.Convert.ToDouble(dur))
                                {
                                    value = value - 1;
                                }
                                RunningInfo.Text = "Generating Day " + System.Convert.ToString(value + 1) + "...";
                            }
                            if (UWGreaderst == "end")
                            {
                                RunningInfo.Text = "Writing new EPW file";
                            }
                            if (UWGreaderst == "over")
                            {
                                UWGst = 0;
                                RunningInfo.Text = "Generating finished!";
                            }
                        }));
                }
            }).Start();
        }
        public void startrun1()
        {
            OnRun++;
            sd1.Visibility = System.Windows.Visibility.Visible;
            sp1.Visibility = System.Windows.Visibility.Visible;

            this.Height += 74;
            ProcessStartInfo startUWG = new ProcessStartInfo();
            //startUWG.FileName = "C:\\Users\\anakano\\Documents\\Research\\UWG2.1\\For_Installer\\UWGv2.0.exe";
            startUWG.FileName = "UWGEngine.exe";
            //make sure there is space in between each of the four inputs and that folder extension ends with \\
            startUWG.Arguments = ep + "\\ " + ef1 + " " + xp1 + "\\ " + xf1 + " " + rp + "\\ " + rf1 + " " + mon + " " + day + " " + dur;
            //startUWG.Arguments = this.epwPath + this.epwFileName + this.xmlPath + this.xmlFilename;
            startUWG.UseShellExecute = false;
            startUWG.CreateNoWindow = true;
            startUWG.RedirectStandardOutput = true;
            double maxx = System.Convert.ToDouble(dur);
            ProgressBar1.Maximum = System.Convert.ToDouble(dur);
            UWGs1 = Process.Start(startUWG);
            UWGst1 = 1;
            UWGs1.EnableRaisingEvents = true;
            UWGs1.Exited += new EventHandler(onClose);
            StreamReader UWGreader = UWGs1.StandardOutput;
            new Thread(() =>
            {
                String UWGreaderst;
                int value = 0;
                while (!UWGreader.EndOfStream)
                {
                    UWGreaderst = UWGreader.ReadLine();
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        if (UWGreaderst == "le")
                        {
                            ProgressBar1.IsIndeterminate = false;
                            RunningInfo1.Text = "Loading EPW file...";
                            DoubleAnimation ani = new DoubleAnimation(maxx, TimeSpan.FromSeconds(15));
                            ProgressBar1.BeginAnimation(ProgressBar.ValueProperty, ani);
                        }
                        if (UWGreaderst == "lx")
                        {
                            RunningInfo1.Text = "Loading XML file...";
                            DoubleAnimation ani1 = new DoubleAnimation(0, TimeSpan.FromSeconds(0));
                            ProgressBar1.BeginAnimation(ProgressBar.ValueProperty, ani1);
                            DoubleAnimation ani2 = new DoubleAnimation(maxx, TimeSpan.FromSeconds(15));
                            ProgressBar1.BeginAnimation(ProgressBar.ValueProperty, ani2);
                        }
                        if (UWGreaderst == "start")
                        {
                            DoubleAnimation ani1 = new DoubleAnimation(0, TimeSpan.FromSeconds(0));
                            ProgressBar1.BeginAnimation(ProgressBar.ValueProperty, ani1);
                            ProgressBar1.Value = 0;
                            RunningInfo1.Text = "Generating Day " + System.Convert.ToString(1) + "...";
                        }
                        if (UWGreaderst == "+")
                        {
                            value++;
                            ProgressBar1.Visibility = System.Windows.Visibility.Visible;
                            //                   ProgressBar.Value = value;
                            DoubleAnimation ani = new DoubleAnimation(value, TimeSpan.FromSeconds(.5));
                            ProgressBar1.BeginAnimation(ProgressBar.ValueProperty, ani);
                            if (value == System.Convert.ToDouble(dur))
                            {
                                value = value - 1;
                            }
                            RunningInfo1.Text = "Generating Day " + System.Convert.ToString(value + 1) + "...";
                        }
                        if (UWGreaderst == "end")
                        {
                            RunningInfo1.Text = "Writing new EPW file";
                        }
                        if (UWGreaderst == "over")
                        {
                            UWGst1 = 0;
                            RunningInfo1.Text = "Generating finished!";
                            System.IO.File.Delete(System.IO.Path.Combine(ep, ef1));
                        }
                    }));
                }
            }).Start();
        }
        public void startrun2()
        {
            OnRun++;
            sd2.Visibility = System.Windows.Visibility.Visible;
            sp2.Visibility = System.Windows.Visibility.Visible;

            this.Height += 74;
            ProcessStartInfo startUWG = new ProcessStartInfo();
            //startUWG.FileName = "C:\\Users\\anakano\\Documents\\Research\\UWG2.1\\For_Installer\\UWGv2.0.exe";
            startUWG.FileName = "UWGEngine.exe";
            //make sure there is space in between each of the four inputs and that folder extension ends with \\
            startUWG.Arguments = ep + "\\ " + ef2 + " " + xp2 + "\\ " + xf2 + " " + rp + "\\ " + rf2 + " " + mon + " " + day + " " + dur;
            //startUWG.Arguments = this.epwPath + this.epwFileName + this.xmlPath + this.xmlFilename;
            startUWG.UseShellExecute = false;
            startUWG.CreateNoWindow = true;
            startUWG.RedirectStandardOutput = true;
            double maxx = System.Convert.ToDouble(dur);
            ProgressBar2.Maximum = System.Convert.ToDouble(dur);
            UWGs2 = Process.Start(startUWG);
            UWGst2 = 1;
            UWGs2.EnableRaisingEvents = true;
            UWGs2.Exited += new EventHandler(onClose);
            StreamReader UWGreader = UWGs2.StandardOutput;
            new Thread(() =>
            {
                String UWGreaderst;
                int value = 0;
                while (!UWGreader.EndOfStream)
                {
                    UWGreaderst = UWGreader.ReadLine();
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        if (UWGreaderst == "le")
                        {
                            ProgressBar2.IsIndeterminate = false;
                            RunningInfo2.Text = "Loading EPW file...";
                            DoubleAnimation ani = new DoubleAnimation(maxx, TimeSpan.FromSeconds(15));
                            ProgressBar2.BeginAnimation(ProgressBar.ValueProperty, ani);
                        }
                        if (UWGreaderst == "lx")
                        {
                            RunningInfo2.Text = "Loading XML file...";
                            DoubleAnimation ani1 = new DoubleAnimation(0, TimeSpan.FromSeconds(0));
                            ProgressBar2.BeginAnimation(ProgressBar.ValueProperty, ani1);
                            DoubleAnimation ani2 = new DoubleAnimation(maxx, TimeSpan.FromSeconds(15));
                            ProgressBar2.BeginAnimation(ProgressBar.ValueProperty, ani2);
                        }
                        if (UWGreaderst == "start")
                        {
                            DoubleAnimation ani1 = new DoubleAnimation(0, TimeSpan.FromSeconds(0));
                            ProgressBar2.BeginAnimation(ProgressBar.ValueProperty, ani1);
                            ProgressBar2.Value = 0;
                            RunningInfo2.Text = "Generating Day " + System.Convert.ToString(1) + "...";
                        }
                        if (UWGreaderst == "+")
                        {
                            value++;
                            ProgressBar2.Visibility = System.Windows.Visibility.Visible;
                            //                   ProgressBar.Value = value;
                            DoubleAnimation ani = new DoubleAnimation(value, TimeSpan.FromSeconds(.5));
                            ProgressBar2.BeginAnimation(ProgressBar.ValueProperty, ani);
                            if (value == System.Convert.ToDouble(dur))
                            {
                                value = value - 1;
                            }
                            RunningInfo2.Text = "Generating Day " + System.Convert.ToString(value + 1) + "...";
                        }
                        if (UWGreaderst == "end")
                        {
                            RunningInfo2.Text = "Writing new EPW file";
                        }
                        if (UWGreaderst == "over")
                        {
                            UWGst2 = 0;
                            RunningInfo2.Text = "Generating finished!";
                            System.IO.File.Delete(System.IO.Path.Combine(ep, ef2));
                        }
                    }));
                }
            }).Start();
        }
        public void startrun3()
        {
            OnRun++;
            sd3.Visibility = System.Windows.Visibility.Visible;
            sp3.Visibility = System.Windows.Visibility.Visible;

            this.Height += 74;
            ProcessStartInfo startUWG = new ProcessStartInfo();
            //startUWG.FileName = "C:\\Users\\anakano\\Documents\\Research\\UWG2.1\\For_Installer\\UWGv2.0.exe";
            startUWG.FileName = "UWGEngine.exe";
            //make sure there is space in between each of the four inputs and that folder extension ends with \\
            startUWG.Arguments = ep + "\\ " + ef3 + " " + xp3 + "\\ " + xf3 + " " + rp + "\\ " + rf3 + " " + mon + " " + day + " " + dur;
            //startUWG.Arguments = this.epwPath + this.epwFileName + this.xmlPath + this.xmlFilename;
            startUWG.UseShellExecute = false;
            startUWG.CreateNoWindow = true;
            startUWG.RedirectStandardOutput = true;
            double maxx = System.Convert.ToDouble(dur);
            ProgressBar3.Maximum = System.Convert.ToDouble(dur);
            UWGs3 = Process.Start(startUWG);
            UWGst3 = 1;
            UWGs3.EnableRaisingEvents = true;
            UWGs3.Exited += new EventHandler(onClose);
            StreamReader UWGreader = UWGs3.StandardOutput;
            new Thread(() =>
            {
                String UWGreaderst;
                int value = 0;
                while (!UWGreader.EndOfStream)
                {
                    UWGreaderst = UWGreader.ReadLine();
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        if (UWGreaderst == "le")
                        {
                            ProgressBar3.IsIndeterminate = false;
                            RunningInfo3.Text = "Loading EPW file...";
                            DoubleAnimation ani = new DoubleAnimation(maxx, TimeSpan.FromSeconds(15));
                            ProgressBar3.BeginAnimation(ProgressBar.ValueProperty, ani);
                        }
                        if (UWGreaderst == "lx")
                        {
                            RunningInfo3.Text = "Loading XML file...";
                            DoubleAnimation ani1 = new DoubleAnimation(0, TimeSpan.FromSeconds(0));
                            ProgressBar3.BeginAnimation(ProgressBar.ValueProperty, ani1);
                            DoubleAnimation ani2 = new DoubleAnimation(maxx, TimeSpan.FromSeconds(15));
                            ProgressBar3.BeginAnimation(ProgressBar.ValueProperty, ani2);
                        }
                        if (UWGreaderst == "start")
                        {
                            DoubleAnimation ani1 = new DoubleAnimation(0, TimeSpan.FromSeconds(0));
                            ProgressBar3.BeginAnimation(ProgressBar.ValueProperty, ani1);
                            ProgressBar3.Value = 0;
                            RunningInfo3.Text = "Generating Day " + System.Convert.ToString(1) + "...";
                        }
                        if (UWGreaderst == "+")
                        {
                            value++;
                            ProgressBar3.Visibility = System.Windows.Visibility.Visible;
                            //                   ProgressBar.Value = value;
                            DoubleAnimation ani = new DoubleAnimation(value, TimeSpan.FromSeconds(.5));
                            ProgressBar3.BeginAnimation(ProgressBar.ValueProperty, ani);
                            if (value == System.Convert.ToDouble(dur))
                            {
                                value = value - 1;
                            }
                            RunningInfo3.Text = "Generating Day " + System.Convert.ToString(value + 1) + "...";
                        }
                        if (UWGreaderst == "end")
                        {
                            RunningInfo3.Text = "Writing new EPW file";
                        }
                        if (UWGreaderst == "over")
                        {
                            UWGst3 = 0;
                            RunningInfo3.Text = "Generating finished!";
                            System.IO.File.Delete(System.IO.Path.Combine(ep, ef3));
                        }
                    }));
                }
            }).Start();
        }
        public void startrun4()
        {
            OnRun++;
            sd4.Visibility = System.Windows.Visibility.Visible;
            sp4.Visibility = System.Windows.Visibility.Visible;

            this.Height += 74;
            ProcessStartInfo startUWG = new ProcessStartInfo();
            //startUWG.FileName = "C:\\Users\\anakano\\Documents\\Research\\UWG2.1\\For_Installer\\UWGv2.0.exe";
            startUWG.FileName = "UWGEngine.exe";
            //make sure there is space in between each of the four inputs and that folder extension ends with \\
            startUWG.Arguments = ep + "\\ " + ef4 + " " + xp4 + "\\ " + xf4 + " " + rp + "\\ " + rf4 + " " + mon + " " + day + " " + dur;
            //startUWG.Arguments = this.epwPath + this.epwFileName + this.xmlPath + this.xmlFilename;
            startUWG.UseShellExecute = false;
            startUWG.CreateNoWindow = true;
            startUWG.RedirectStandardOutput = true;
            double maxx = System.Convert.ToDouble(dur);
            ProgressBar4.Maximum = System.Convert.ToDouble(dur);
            UWGs4 = Process.Start(startUWG);
            UWGst4 = 1;
            UWGs4.EnableRaisingEvents = true;
            UWGs4.Exited += new EventHandler(onClose);
            StreamReader UWGreader = UWGs4.StandardOutput;
            new Thread(() =>
            {
                String UWGreaderst;
                int value = 0;
                while (!UWGreader.EndOfStream)
                {
                    UWGreaderst = UWGreader.ReadLine();
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        if (UWGreaderst == "le")
                        {
                            ProgressBar4.IsIndeterminate = false;
                            RunningInfo4.Text = "Loading EPW file...";
                            DoubleAnimation ani = new DoubleAnimation(maxx, TimeSpan.FromSeconds(15));
                            ProgressBar4.BeginAnimation(ProgressBar.ValueProperty, ani);
                        }
                        if (UWGreaderst == "lx")
                        {
                            RunningInfo4.Text = "Loading XML file...";
                            DoubleAnimation ani1 = new DoubleAnimation(0, TimeSpan.FromSeconds(0));
                            ProgressBar4.BeginAnimation(ProgressBar.ValueProperty, ani1);
                            DoubleAnimation ani2 = new DoubleAnimation(maxx, TimeSpan.FromSeconds(15));
                            ProgressBar4.BeginAnimation(ProgressBar.ValueProperty, ani2);
                        }
                        if (UWGreaderst == "start")
                        {
                            DoubleAnimation ani1 = new DoubleAnimation(0, TimeSpan.FromSeconds(0));
                            ProgressBar4.BeginAnimation(ProgressBar.ValueProperty, ani1);
                            ProgressBar4.Value = 0;
                            RunningInfo4.Text = "Generating Day " + System.Convert.ToString(1) + "...";
                        }
                        if (UWGreaderst == "+")
                        {
                            value++;
                            ProgressBar4.Visibility = System.Windows.Visibility.Visible;
                            //                   ProgressBar.Value = value;
                            DoubleAnimation ani = new DoubleAnimation(value, TimeSpan.FromSeconds(.5));
                            ProgressBar4.BeginAnimation(ProgressBar.ValueProperty, ani);
                            if (value == System.Convert.ToDouble(dur))
                            {
                                value = value - 1;
                            }
                            RunningInfo4.Text = "Generating Day " + System.Convert.ToString(value + 1) + "...";
                        }
                        if (UWGreaderst == "end")
                        {
                            RunningInfo4.Text = "Writing new EPW file";
                        }
                        if (UWGreaderst == "over")
                        {
                            UWGst4 = 0;
                            RunningInfo4.Text = "Generating finished!";
                            System.IO.File.Delete(System.IO.Path.Combine(ep, ef4));
                        }
                    }));
                }
            }).Start();
        }
        public void startrun5()
        {
            OnRun++;
            sd5.Visibility = System.Windows.Visibility.Visible;
            sp5.Visibility = System.Windows.Visibility.Visible;

            this.Height += 74;
            ProcessStartInfo startUWG = new ProcessStartInfo();
            //startUWG.FileName = "C:\\Users\\anakano\\Documents\\Research\\UWG2.1\\For_Installer\\UWGv2.0.exe";
            startUWG.FileName = "UWGEngine.exe";
            //make sure there is space in between each of the four inputs and that folder extension ends with \\
            startUWG.Arguments = ep + "\\ " + ef5 + " " + xp5 + "\\ " + xf5 + " " + rp + "\\ " + rf5 + " " + mon + " " + day + " " + dur;
            //startUWG.Arguments = this.epwPath + this.epwFileName + this.xmlPath + this.xmlFilename;
            startUWG.UseShellExecute = false;
            startUWG.CreateNoWindow = true;
            startUWG.RedirectStandardOutput = true;
            double maxx = System.Convert.ToDouble(dur);
            ProgressBar5.Maximum = System.Convert.ToDouble(dur);
            UWGs5 = Process.Start(startUWG);
            UWGst5 = 1;
            UWGs5.EnableRaisingEvents = true;
            UWGs5.Exited += new EventHandler(onClose);
            StreamReader UWGreader = UWGs5.StandardOutput;
            new Thread(() =>
            {
                String UWGreaderst;
                int value = 0;
                while (!UWGreader.EndOfStream)
                {
                    UWGreaderst = UWGreader.ReadLine();
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        if (UWGreaderst == "le")
                        {
                            ProgressBar5.IsIndeterminate = false;
                            RunningInfo5.Text = "Loading EPW file...";
                            DoubleAnimation ani = new DoubleAnimation(maxx, TimeSpan.FromSeconds(15));
                            ProgressBar5.BeginAnimation(ProgressBar.ValueProperty, ani);
                        }
                        if (UWGreaderst == "lx")
                        {
                            RunningInfo5.Text = "Loading XML file...";
                            DoubleAnimation ani1 = new DoubleAnimation(0, TimeSpan.FromSeconds(0));
                            ProgressBar5.BeginAnimation(ProgressBar.ValueProperty, ani1);
                            DoubleAnimation ani2 = new DoubleAnimation(maxx, TimeSpan.FromSeconds(15));
                            ProgressBar5.BeginAnimation(ProgressBar.ValueProperty, ani2);
                        }
                        if (UWGreaderst == "start")
                        {
                            DoubleAnimation ani1 = new DoubleAnimation(0, TimeSpan.FromSeconds(0));
                            ProgressBar5.BeginAnimation(ProgressBar.ValueProperty, ani1);
                            ProgressBar5.Value = 0;
                            RunningInfo5.Text = "Generating Day " + System.Convert.ToString(1) + "...";
                        }
                        if (UWGreaderst == "+")
                        {
                            value++;
                            ProgressBar5.Visibility = System.Windows.Visibility.Visible;
                            //                   ProgressBar.Value = value;
                            DoubleAnimation ani = new DoubleAnimation(value, TimeSpan.FromSeconds(.5));
                            ProgressBar5.BeginAnimation(ProgressBar.ValueProperty, ani);
                            if (value == System.Convert.ToDouble(dur))
                            {
                                value = value - 1;
                            }
                            RunningInfo5.Text = "Generating Day " + System.Convert.ToString(value + 1) + "...";
                        }
                        if (UWGreaderst == "end")
                        {
                            RunningInfo5.Text = "Writing new EPW file";
                        }
                        if (UWGreaderst == "over")
                        {
                            UWGst5 = 0;
                            RunningInfo5.Text = "Generating finished!";
                            System.IO.File.Delete(System.IO.Path.Combine(ep, ef5));
                        }
                    }));
                }
            }).Start();
        }
        private void abort_Click(object sender, RoutedEventArgs e)
        {
            if(OnRun > 0)
            {
                string message = "Simulation is running, do you want to abort?";
                string captain = "UWG";
                MessageBoxButton buttons = MessageBoxButton.OKCancel;
                var result = MessageBox.Show(message, captain, buttons);
                if (result == MessageBoxResult.Cancel) return;
            }
            UWGst = -1;
            UWGst1 = -1;
            UWGst2 = -1;
            UWGst3 = -1;
            UWGst4 = -1;
            UWGst5 = -1;
            try
            {
                UWGs.Kill();
                UWGs1.Kill();
                UWGs2.Kill();
                UWGs3.Kill();
                UWGs4.Kill();
                UWGs5.Kill();
                this.Close();
            }
            catch
            {
                try
                {
                    this.Close();
                }
                catch
                {
                }
            }
        }
    }
}
