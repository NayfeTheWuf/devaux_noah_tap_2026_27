using System.IO;
using GameLibrary.Logs;

namespace ConsoleApp
{
    public class LogFileWriter : ILogWriter
    {
        //Chemin d'écriture du fichier de Log
        private readonly string LOG_PATH = "../../../debug.log";

        public void WriteLine(string line)
        {
            File.AppendAllText(LOG_PATH, line + "\n");
        }
    }
}
