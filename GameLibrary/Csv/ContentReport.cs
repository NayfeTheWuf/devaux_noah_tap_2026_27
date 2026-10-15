using System;
using System.IO;

namespace GameLibrary.Csv
{
    public class ContentReport
    {
        //Données d'erreurs
        private class ErrorData
        {
            public string _fileName;
            public int _fileLine;
            public string _message;
        }
        
        //Var
        private readonly List<ErrorData> _errorTable = new List<ErrorData>();

        //Ajoute l'erreur et la renvoie à l'utilisateur 
        public void AddError(string file_name, int file_line, string message)
        {
            _errorTable.Add(new ErrorData { _fileName = file_name, _fileLine = file_line, _message = message });
        }

        //Retourne le nombre d'erreur compté
        public int GetErrorCount()
        {
            return _errorTable.Count();
        }

        //Retourner le texte d'erreur de l'index choisit 
        public string GetErrorTextAtIndex(int error_index)
        {
            ErrorData error_data = _errorTable[error_index];

            return $"{error_data._fileName}, line {error_data._fileLine}: {error_data._message}";
        }
    }
}
