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
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace DCM_Plotter
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SelectSignalsWiCE : Page
    {
        private ObservableCollection<string> headers_wice_common_indications_diesel = new();
        private ObservableCollection<string> crosscheck = new();
        public SelectSignalsWiCE()
        {
            this.InitializeComponent();
            //foreach (var column in DataAnalysis.wice_common_indications_diesel.Columns)
            //{
            //    headers_wice_common_indications_diesel.Add(column.Name);
            //}
            //wice_common_indications_diesel.ItemsSource = headers_wice_common_indications_diesel;
            for (var i = 0; i < 50; i++)
            {
                headers_wice_common_indications_diesel.Add("Item "+i.ToString());
            }
            list_wice_common_indications_diesel.ItemsSource = headers_wice_common_indications_diesel;
            crosscheck.Add("Selected Items");
            list_crosscheck.ItemsSource = crosscheck;
        }

        private void Item_Changed(object sender, RoutedEventArgs e)
        {
            crosscheck.Clear();
            foreach (var item in list_wice_common_indications_diesel.SelectedItems)
            {
                crosscheck.Add(item.ToString());
            }
        }
    }
}
