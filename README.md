# Urban-Weather-Generator

This repo is for developing the front-end GUI for Urban Weather Generator. The Executable file is downloadable from [Urban Microclimate website] (http://urbanmicroclimate.scripts.mit.edu/uwg.php).

The software has been developed and tested in Windows 7 operating system (32 bit).

## Folder Structure
```UWG_Standalone_20150119``` is the main repo. The ```UWG_umi_20150226``` is the one with slightly different code for umi (App.xaml.cs 
has the code with console interface, MainWindow files are different from the other folder because they hide parameters that no longer require separate user inputs).

These two folders should be merged in the future for ease of maintenance. 

```UWG_Standalone_20150119/package``` folder contains the necessary install files for compiling the executable. The settings are specified within ```UWG_Standalone_20150119/UWG_installer```.

## Note on Building the Project
If you get an error along the lines of "The tag 'Chart' does not exist in XML namespace 'clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit'.", try uninstalling and reinstalling the WPF Toolkit Charting Controls and turn off code signing.

## Version 3.0.0 unresolved issues (to be fixed in version 3.0.1)

Error check does not close automatically, but simulation is completed once the Matlab progress bar is completed [and the epw file is created in the designated location]

