using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Advanced_file_scanner
{
    class ExportList
    {
        public static void Export(List<string> ListToExport, string ExportFilePath)
        {
            StreamWriter Writer = new StreamWriter(ExportFilePath);
            foreach (var item in ListToExport)
            {
                Writer.WriteLine(item.ToString());
            }
            Writer.Close();
        }
    }
}
