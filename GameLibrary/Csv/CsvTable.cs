using System;
using System.IO;

namespace GameLibrary.Csv
{
    public class CsvTable
    {
        private readonly string _fileName;
        private readonly List<CsvRow> _rowTable = new List<CsvRow>();
        private List<string> _rowHeaderTable;
        private bool _hasHeader = false;

        public CsvTable(string file_name)
        {
            _fileName = file_name;
        }

        //Retourne le nom du fichier
        public string GetFileName()
        {
            return _fileName;
        }

        //Retourne si un Header est existant
        public bool GetHasHeader()
        {
            return _hasHeader;
        }

        //Ajoute un Header
        public void SetHeader(string[] row_header_table)
        {
            _rowHeaderTable = new List<string>(row_header_table);
            _hasHeader = true;
        }

        //Retourne le nombre de colonne
        public int GetColumnCount()
        {
            return _rowHeaderTable.Count;
        }

        //Retourne l'index d'un nom de colonne spécifique
        public int GetColumnIndex(string column_name)
        {
            //Si absente, définir à -1
            int column_index = -1;

            for (int header_column_index = 0; header_column_index < _rowHeaderTable.Count; header_column_index++)
            {
                if (_rowHeaderTable[header_column_index].Contains(column_name))
                {
                    column_index = header_column_index;
                }
            }

            return column_index;
        }
        
        //Retourne le nom d'une colonne à un index spécifique
        public string GetColumnNameAtIndex(int column_index)
        {
            return _rowHeaderTable[column_index];
        }

        //Ajoute une rangée
        public void AddRow(CsvRow row)
        {
            _rowTable.Add(row);
        }

        //Retourne le nombre de rangée
        public int GetRowCount()
        {
            return _rowTable.Count();
        }

        //Retourne la rangée de l'index spécifié
        public CsvRow GetRowAtIndex(int row_index)
        {
            return _rowTable[row_index];
        }
    }
}
