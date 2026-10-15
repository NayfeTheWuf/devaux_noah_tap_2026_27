using System;
using System.IO;

namespace GameLibrary.Csv
{
    public class CsvRow
    {
        //Var
        private readonly int _lineNumber;
        private readonly string[] _cellTable;

        public CsvRow(int line_number, string[] cell_table) 
        { 
            _lineNumber = line_number;
            _cellTable = cell_table;
        }

        //Retourne le nombre de ligne
        public int GetLineNumber()
        {
            return _lineNumber;
        }

        //Retourne le nombre de cellule
        public int GetCellCount()
        {
            return _cellTable.Length;
        }

        //Retourne une cellule à un index spécifique
        public string GetCellAtIndex(int cell_index)
        {
            return _cellTable[cell_index].Trim();
        }
    }
}
