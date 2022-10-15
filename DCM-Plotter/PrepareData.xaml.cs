using DCM_Plotter.Utils;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.Data.Analysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Data.SqlTypes;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DCM_Plotter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PrepareData : Page
    {
        private bool IsWiCE = false;
        public PrepareData()
        {
            this.InitializeComponent();
        }

        private void Btn_LoadData(object sender, RoutedEventArgs e)
        {
            LoadDataButton.IsEnabled = false;
            ProgressRing.IsActive = true;

            LoadData(); 
        }

        private void Btn_SelectSignals(object sender, RoutedEventArgs e)
        {
            if (IsWiCE)
            {
                Helpers.MainFrame.Navigate(typeof(SelectSignalsWiCE));
                Helpers.NavigationBar.SelectedItem = Helpers.NavigationBar.MenuItems[2];
            }
            else
            {
                Helpers.MainFrame.Navigate(typeof(SelectSignals));
                Helpers.NavigationBar.SelectedItem = Helpers.NavigationBar.MenuItems[2];
            }
            
        }

        private async void LoadData()
        {
            if (DataAnalysis.FileList.FindAll(x => x.Contains("wice")).Count>0)
            {
                IsWiCE = true;

                txt_progress.Text = txt_progress.Text + "(1/10) Loading \"wice_common_indications_diesel\"...";
                DataAnalysis.wice_common_indications_diesel = await Task.Run(() => DataAnalysis.MergeFiles(DataAnalysis.DataFolder, DataAnalysis.FileList, "wice_common_indications_diesel"));
                DataAnalysis.wice_common_indications_diesel.Columns.Remove("index");
                txt_progress.Text = txt_progress.Text + " Done" + "\n";

                ProgressBar.Value = 10;
                txt_progress.Text = txt_progress.Text + "(2/10) Loading \"wice_cylinder_indications_diesel\"...";
                DataAnalysis.wice_cylinder_indications_diesel = await Task.Run(() => DataAnalysis.MergeFiles(DataAnalysis.DataFolder, DataAnalysis.FileList, "wice_cylinder_indications_diesel"));
                txt_progress.Text = txt_progress.Text + " Done" + "\n";

                ProgressBar.Value = 20;
                txt_progress.Text = txt_progress.Text + "(3/10) Loading \"wice_common_failures_diesel\"...";
                DataAnalysis.wice_common_failures_diesel = await Task.Run(() => DataAnalysis.MergeFiles(DataAnalysis.DataFolder, DataAnalysis.FileList, "wice_common_failures_diesel"));
                txt_progress.Text = txt_progress.Text + " Done" + "\n";

                ProgressBar.Value = 30;
                txt_progress.Text = txt_progress.Text + "(4/10) Loading \"wice_cylinder_failures_diesel\"...";
                DataAnalysis.wice_cylinder_failures_diesel = await Task.Run(() => DataAnalysis.MergeFiles(DataAnalysis.DataFolder, DataAnalysis.FileList, "wice_cylinder_failures_diesel"));
                txt_progress.Text = txt_progress.Text + " Done" + "\n";

                ProgressBar.Value = 40;
                txt_progress.Text = txt_progress.Text + "(5/10) Loading \"wice_common_indications_gas\"...";
                DataAnalysis.wice_common_indications_gas = await Task.Run(() => DataAnalysis.MergeFiles(DataAnalysis.DataFolder, DataAnalysis.FileList, "wice_common_indications_gas"));
                txt_progress.Text = txt_progress.Text + " Done" + "\n";

                ProgressBar.Value = 50;
                txt_progress.Text = txt_progress.Text + "(6/10) Loading \"wice_cylinder_indications_gas\"...";
                DataAnalysis.wice_cylinder_indications_gas = await Task.Run(() => DataAnalysis.MergeFiles(DataAnalysis.DataFolder, DataAnalysis.FileList, "wice_cylinder_indications_gas"));
                txt_progress.Text = txt_progress.Text + " Done" + "\n";

                ProgressBar.Value = 60;
                txt_progress.Text = txt_progress.Text + "(7/10) Loading \"wice_common_failures_gas\"...";
                DataAnalysis.wice_common_failures_gas = await Task.Run(() => DataAnalysis.MergeFiles(DataAnalysis.DataFolder, DataAnalysis.FileList, "wice_common_failures_gas"));
                txt_progress.Text = txt_progress.Text + " Done" + "\n";

                ProgressBar.Value = 70;
                txt_progress.Text = txt_progress.Text + "(8/10) Loading \"wice_cylinder_failures_gas\"...";
                DataAnalysis.wice_cylinder_failures_gas = await Task.Run(() => DataAnalysis.MergeFiles(DataAnalysis.DataFolder, DataAnalysis.FileList, "wice_cylinder_failures_gas"));
                txt_progress.Text = txt_progress.Text + " Done" + "\n";

                ProgressBar.Value = 80;
                txt_progress.Text = txt_progress.Text + "(9/10) Loading \"wice_common_info\"...";
                DataAnalysis.wice_common_info = await Task.Run(() => DataAnalysis.MergeFiles(DataAnalysis.DataFolder, DataAnalysis.FileList, "wice_common_info"));
                txt_progress.Text = txt_progress.Text + " Done" + "\n";

                ProgressBar.Value = 90;
                txt_progress.Text = txt_progress.Text + "(10/10) Loading \"wice_cylinder_info\"...";
                DataAnalysis.wice_cylinder_info = await Task.Run(() => DataAnalysis.MergeFiles(DataAnalysis.DataFolder, DataAnalysis.FileList, "wice_cylinder_info"));
                txt_progress.Text = txt_progress.Text + " Done" + "\n";

                txt_progress.Text = txt_progress.Text + "\n All files loaded!";
                ProgressBar.Value = 100;
            }
            else
            {
                IsWiCE = false;

                txt_progress.Text = txt_progress.Text + "(1/2) Loading \"signals\"...";
                DataAnalysis.signals = await Task.Run(() => DataAnalysis.MergeFiles(DataAnalysis.DataFolder, DataAnalysis.FileList, "signals"));
                txt_progress.Text = txt_progress.Text + " Done" + "\n";

                ProgressBar.Value = 50;
                txt_progress.Text = txt_progress.Text + "(1/2) Loading \"failures\"...";
                DataAnalysis.failures = await Task.Run(() => DataAnalysis.MergeFiles(DataAnalysis.DataFolder, DataAnalysis.FileList, "failures"));
                txt_progress.Text = txt_progress.Text + " Done" + "\n";

                txt_progress.Text = "\n All files loaded!";
                ProgressBar.Value = 100;
            }
            ProgressRing.IsActive = false;
            SelectSignalsButton.IsEnabled = true;
        }
    }
}
