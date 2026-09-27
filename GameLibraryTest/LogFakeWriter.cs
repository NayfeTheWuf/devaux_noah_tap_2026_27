using GameLibrary.Logs;

namespace GameLibraryTest
{
    public class LogFakeWriter : ILogWriter
    {
        private readonly List<string> _writtenLineTable = new List<string>();

        public void WriteLine(string line)
        {
            _writtenLineTable.Add(line);
        }

        //Nombre de lignes mémorisées
        public int GetWrittenLineCount()
        {
            return _writtenLineTable.Count;
        }

        //Retourne une ligne mémorisée
        public string GetWrittenLine(int line_index)
        {
            return _writtenLineTable[line_index];
        }
    }
}
