using GameLibrary.Events;

namespace GameLibrary.Logs
{
    public class LogManager
    {
        //Var
        private readonly ILogWriter _logWriter;

        //Enregistrement dans un évènement
        public LogManager(EventManager event_manager, ILogWriter log_writer)
        {
            _logWriter = log_writer;

            event_manager.RegisterToEvent<LogMessageGameEvent>(OnLogMessageGameEvent);
        }

        //Utilisation de l'évènement 
        private void OnLogMessageGameEvent(IGameEvent game_event)
        {
            LogMessageGameEvent log_message = game_event as LogMessageGameEvent;
            string log_line = $"{DateTime.Now} {log_message._message}";

            _logWriter.WriteLine(log_line);
        }
    }
}