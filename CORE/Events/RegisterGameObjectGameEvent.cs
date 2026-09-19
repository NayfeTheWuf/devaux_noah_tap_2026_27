using System.Collections.Generic;
using System;

namespace activity_00_tap_26_27.Events
{
    public class RegisterGameObjectGameEvent : IGameEvent
    {
        public readonly GameObject _gameObject;

        public RegisterGameObjectGameEvent(GameObject game_object)
        {
            _gameObject = game_object;
        }
    }
}