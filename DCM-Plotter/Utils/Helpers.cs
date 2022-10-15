using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DCM_Plotter.Utils
{
    internal class Helpers
    {
        public static Frame MainFrame;
        public static NavigationView NavigationBar;

        public static async Task<StorageFolder> GetFolder()
        {
            var folderPicker = new Windows.Storage.Pickers.FolderPicker();
            var window = (Microsoft.UI.Xaml.Application.Current as App)?.m_window as MainWindow;
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
            folderPicker.FileTypeFilter.Add("*");
            WinRT.Interop.InitializeWithWindow.Initialize(folderPicker, hwnd);

            return await folderPicker.PickSingleFolderAsync();
        }

        public static async Task<IReadOnlyList<StorageFile>> GetFolderFiles(StorageFolder folder)
        {
            if (folder != null)
            {
                var folderQuery = folder.CreateFileQueryWithOptions(new Windows.Storage.Search.QueryOptions());
                return await folderQuery.GetFilesAsync();;
            }
            else
            {
                return null;
            }
        }
    }
}
