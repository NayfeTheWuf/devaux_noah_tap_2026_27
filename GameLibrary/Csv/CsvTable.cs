using System;
using System.IO;

namespace GameLibrary.Csv
{
    public class CsvTable
    {
        private string _fileName;
        private bool _hasHeader;

        public CsvTable(string file_name)
        {
            _fileName = file_name;
        }

        //Ajoute un Header
        public void SetHeader(string cells)
        {
            cells = cells.Trim();
        }

        //Retourne si un Header est existant
        public bool GetHasHeader()
        {
            return _hasHeader;
        }

        //Retourne l'index de la colonne
        public int GetColumnIndex(string column_name)
        {
            //-1 si absente
            return;
        }

        //Ajoute une rangée
        public void AddRow(int lines_number, string cells)
        {

        }

        //Retourne le nombre de rangée
        public int GetRowCount()
        {
            return;
        }

        //Retourne la rangée de l'index spécifié
        public int GetRowAtIndex(int row_index)
        {
            return;
        }
    }
}
