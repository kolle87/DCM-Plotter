using Microsoft.Data.Analysis;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DCM_Plotter.Utils
{
    internal class DataAnalysis
    {
        public static string DataFolder;
        public static List<string> FileList;

        public static DataFrame wice_common_indications_diesel;
        public static DataFrame wice_cylinder_indications_diesel;
        public static DataFrame wice_common_failures_diesel;
        public static DataFrame wice_cylinder_failures_diesel;
        public static DataFrame wice_common_indications_gas;
        public static DataFrame wice_cylinder_indications_gas;
        public static DataFrame wice_common_failures_gas;
        public static DataFrame wice_cylinder_failures_gas;
        public static DataFrame wice_common_info;
        public static DataFrame wice_cylinder_info;
        public static DataFrame signals;
        public static DataFrame failures;

        public static async Task<bool> LoadDatafromCsv()
        {

            return false;
        }

        public static DataFrame MergeFiles(string DataFolder, List<string> FileList, string Identifier)
        {
            var filtered_filelist = FileList.FindAll(x => x.Contains(Identifier));
            if (filtered_filelist.Count > 0)
            {
                var temp_dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Temp");

                Directory.CreateDirectory(temp_dir);

                List<string> header = new List<string>();
                IEnumerable<string> mergedData = new List<string>();
                header.Add(File.ReadLines(Path.Combine(DataFolder, filtered_filelist.First())).First());

                foreach (string item in filtered_filelist)
                {
                    var s = File.ReadLines(Path.Combine(DataFolder, item)).Skip(1);
                    mergedData = mergedData.Concat(s);
                }
                var merged = header.Concat(mergedData);
                File.WriteAllLines(Path.Combine(temp_dir, "temp_data.csv"), merged);
                var df = DataFrame.LoadCsv(Path.Combine(temp_dir, "temp_data.csv"));
                File.Delete(Path.Combine(temp_dir, "temp_data.csv"));
                return df;
            }
            else
                return null;
            
        }

    }
}
