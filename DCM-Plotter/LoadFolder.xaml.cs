using DCM_Plotter.Utils;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using static System.Net.WebRequestMethods;

namespace DCM_Plotter
{
    public sealed partial class LoadFolder : Page
    {
        private StorageFolder folder;
        private List<string> List_WiCE_ME1 = new List<string>();
        private List<string> List_WiCE_ME2 = new List<string>();
        private List<string> List_UNIC_ME1 = new List<string>();
        private List<string> List_UNIC_ME2 = new List<string>();
        private List<string> List_WECS_ME1 = new List<string>();
        private List<string> List_WECS_ME2 = new List<string>();
        private List<string> List_SCR1 = new List<string>();
        private List<string> List_SCR2 = new List<string>();
        private List<string> List_ME1 = new List<string>();
        private List<string> List_ME2 = new List<string>();
        private List<string> List_AMS = new List<string>();

        public LoadFolder()
        {
            this.InitializeComponent();
        }

        private async void Btn_SelectFolder(object sender, RoutedEventArgs e)
        {
            SystemPanel.Visibility = Visibility.Collapsed;
            SelectSystem_WiCE1.IsEnabled = false; 
            SelectSystem_WiCE2.IsEnabled = false; 
            SelectSystem_UNIC1.IsEnabled = false; 
            SelectSystem_UNIC2.IsEnabled = false; 
            SelectSystem_WECS1.IsEnabled = false; 
            SelectSystem_WECS2.IsEnabled = false; 
            SelectSystem_SCR1.IsEnabled = false; 
            SelectSystem_SCR2.IsEnabled = false; 
            SelectSystem_Generic1.IsEnabled = false; 
            SelectSystem_Generic2.IsEnabled = false; 
            SelectSystem_AMS.IsEnabled = false; 

            folder = await Helpers.GetFolder();
            if (folder != null)
            {
                FolderTextBox.Text = folder.Path; 
                ScanFolderButton.IsEnabled = true;
                DataAnalysis.DataFolder = folder.Path;
            }
            else
            {
                FolderTextBox.Text = "No Folder Selected";
                ScanFolderButton.IsEnabled = false;
            }          
        }

        private async void Btn_ScanFolder(object sender, RoutedEventArgs e)
        {            
            var fileList = await Helpers.GetFolderFiles(folder);

            if (fileList != null)
            {
                foreach (StorageFile file in fileList)
                {
                    if (file.Name.Contains("ems_1_wice")) { List_WiCE_ME1.Add(file.Name); SelectSystem_WiCE1.IsEnabled = true; }
                    else if (file.Name.Contains("ems_2_wice")) { List_WiCE_ME2.Add(file.Name); SelectSystem_WiCE2.IsEnabled = true; }
                    else if (file.Name.Contains("ems_1")) { List_UNIC_ME1.Add(file.Name); SelectSystem_UNIC1.IsEnabled = true; }
                    else if (file.Name.Contains("ems_2")) { List_UNIC_ME2.Add(file.Name); SelectSystem_UNIC2.IsEnabled = true; }
                    else if (file.Name.Contains("wecs_1")) { List_WECS_ME1.Add(file.Name); SelectSystem_WECS1.IsEnabled = true; }
                    else if (file.Name.Contains("wecs_2")) { List_WECS_ME2.Add(file.Name); SelectSystem_WECS2.IsEnabled = true; }
                    else if (file.Name.Contains("scr_1")) { List_SCR1.Add(file.Name); SelectSystem_SCR1.IsEnabled = true; }
                    else if (file.Name.Contains("scr_1")) { List_SCR2.Add(file.Name); SelectSystem_SCR2.IsEnabled = true; }
                    else if (file.Name.Contains("me_1")) { List_ME1.Add(file.Name); SelectSystem_Generic1.IsEnabled = true; }
                    else if (file.Name.Contains("me_2")) { List_ME2.Add(file.Name); SelectSystem_Generic2.IsEnabled = true; }
                    else if (file.Name.Contains("ams")) { List_AMS.Add(file.Name); SelectSystem_AMS.IsEnabled = true; }
                }   
                SystemPanel.Visibility = Visibility.Visible;
            }
            else
            {
                // no usable files found
            }
        }

        private void NavigateAhead()
        {
            Helpers.MainFrame.Navigate(typeof(PrepareData));
            Helpers.NavigationBar.SelectedItem = Helpers.NavigationBar.MenuItems[1];
        }

        private void Btn_SelectWiCE1(object sender, RoutedEventArgs e)
        {
            DataAnalysis.FileList = List_WiCE_ME1;
            NavigateAhead();
        }

        private void Btn_SelectWiCE2(object sender, RoutedEventArgs e)
        {
            DataAnalysis.FileList = List_WiCE_ME2;
            NavigateAhead();
        }

        private void Btn_SelectUNIC1(object sender, RoutedEventArgs e)
        {
            DataAnalysis.FileList = List_UNIC_ME1;
            NavigateAhead();
        }

        private void Btn_SelectUNIC2(object sender, RoutedEventArgs e)
        {
            DataAnalysis.FileList = List_UNIC_ME2;
            NavigateAhead();
        }

        private void Btn_SelectWECS1(object sender, RoutedEventArgs e)
        {
            DataAnalysis.FileList = List_WECS_ME1;
            NavigateAhead();
        }

        private void Btn_SelectWECS2(object sender, RoutedEventArgs e)
        {
            DataAnalysis.FileList = List_WECS_ME2;
            NavigateAhead();
        }

        private void Btn_SelectSCR1(object sender, RoutedEventArgs e)
        {
            DataAnalysis.FileList = List_SCR1;
            NavigateAhead();
        }

        private void Btn_SelectSCR2(object sender, RoutedEventArgs e)
        {
            DataAnalysis.FileList = List_SCR2;
            NavigateAhead();
        }

        private void Btn_SelectGeneric1(object sender, RoutedEventArgs e)
        {
            DataAnalysis.FileList = List_ME1;
            NavigateAhead();
        }

        private void Btn_SelectGeneric2(object sender, RoutedEventArgs e)
        {
            DataAnalysis.FileList = List_ME2;
            NavigateAhead();
        }

        private void Btn_SelectAMS(object sender, RoutedEventArgs e)
        {
            DataAnalysis.FileList = List_AMS;
            NavigateAhead();
        }
    }
}
