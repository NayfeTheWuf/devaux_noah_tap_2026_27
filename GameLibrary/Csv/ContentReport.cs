using System;
using System.IO;

namespace GameLibrary.Csv
{
    public class ContentReport
    {
        //Var
        private string _filePath = Path.GetFullPath("../../../Data/locations.csv");
        private readonly List<ContentReport> _errors = new List<ContentReport>();

        //Ajoute l'erreur et la renvoie à l'utilisateur 
        public void AddError(string file_name, int line_number, string message)
        {
            if (!File.Exists(_filePath)) 
            { 
                Console.WriteLine($"file locations.csv not found at {_filePath}"); 
            }
            else
            {
                try { 
                    string content = File.ReadAllText(_filePath); 
                }
                catch (IOException)
                {
                    Console.WriteLine($"locations.csv cannot be read. Close it in your spreadsheet and try again.");
                }
                
            }
        }

        //Retourne le nombre d'erreur compté
        public void GetErrorCount()
        {

        }

        //Retourner le texte d'erreur de l'index choisit 
        public void GetErrorTextAtIndex(int error_index)
        {

        }
    }
}
