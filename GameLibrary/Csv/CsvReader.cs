using System;
using System.IO;

namespace GameLibrary.Csv
{
    public class CsvReader
    {
        //Lit la table de data tout en signalant et ignorant les lignes erronées 
        public CsvTable Read(string file_name, string content, ContentReport report)
        {
            CsvTable csv_table = new CsvTable(file_name);
            string[] line_table = content.Split('\n');

            //Lire la table
            for (int line_index = 0; line_index < line_table.Length; line_index++)
            {
                string line = line_table[line_index].Trim();
                int line_number = line_index + 1;

                if (line.Length > 0)
                {
                    string[] cells = line.Split(';');

                    //Ajoute un Header
                    if (csv_table.GetHasHeader() == false)
                    {
                        csv_table.SetHeader(cells);
                    }
                    //Si une erreur est détectée
                    else if (cells.Length != csv_table.GetRowCount())
                    {
                        report.AddError(file_name, line_number, $"{cells.Length} values found, " + $"{csv_table.GetRowCount()} expected.");
                    }
                    //Créer une rangée
                    else
                    {
                        csv_table.AddRow(new CsvRow(line_number, cells));
                    }
                }
            }
            return csv_table;
        }
    }
}
