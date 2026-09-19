using System.Collections.Generic;
using System;

namespace activity_00_tap_26_27.Events
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