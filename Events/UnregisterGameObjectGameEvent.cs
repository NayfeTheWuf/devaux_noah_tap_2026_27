using System.Collections.Generic;
using System;

namespace activity_00_tap_26_27.Events
{
    public class UnregisterGameObjectGameEvent : IGameEvent
    {
        public GameObject _gameObject;

        public UnregisterGameObjectGameEvent(GameObject game_object)
        {
            _gameObject = game_object;
        }
    }
}