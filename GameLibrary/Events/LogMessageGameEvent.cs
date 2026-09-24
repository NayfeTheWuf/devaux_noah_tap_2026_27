namespace GameLibrary.Events
{
    public class LogMessageGameEvent : IGameEvent
    {
        public string _message;

        public LogMessageGameEvent(string message)
        {
            _message = message;
        }
    }
}