using System;
using System.IO;
using ConsoleApp.Events;

namespace ConsoleApp
{
    public class LogManager
    {
        //Chemin d'écriture du fichier de Log
        private readonly string LOG_PATH = "../../../debug.log";

        //Enregistrement dans un évènement
        public LogManager(EventManager event_manager)
        {
            event_manager.RegisterToEvent<LogMessageGameEvent>(OnLogMessageGameEvent);
        }

        //Utilisation de l'évènement 
        private void OnLogMessageGameEvent(IGameEvent game_event)
        {
            LogMessageGameEvent log_message = game_event as LogMessageGameEvent;
            //Ecrit le message avec le temps et le message
            string log_entry = $"{DateTime.Now} {log_message._message}\n";
            //Ajout du texte dans le fichier de Log
            File.AppendAllText(LOG_PATH, log_entry);
        }
    }
}