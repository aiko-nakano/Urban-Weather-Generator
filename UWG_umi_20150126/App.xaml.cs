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
using System.Data;

namespace UWG
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            UWGInputs main = new UWGInputs();
                if (e.Args.Length == 8)
                {
                    main.avgBldgHeight.Text = e.Args[0].ToString();
                    main.charLength.Text = e.Args[1].ToString();
                    main.hBDensity.Text = e.Args[2].ToString();
                    main.vHRatios.Text = e.Args[3].ToString();
                    main.wwr.Content = e.Args[4].ToString();
                    main.uValue.Content = e.Args[5].ToString();
                    main.SHGC.Content = e.Args[6].ToString();
                    //main.Refresh();
                    main.Show();
                }
                else
                {

                    MessageBox.Show("Parameters are incomplete, please call UWG from umi properly!", "UWG");
                    //main.Refresh();
                    main.Show();
                }
        }
    }
}
