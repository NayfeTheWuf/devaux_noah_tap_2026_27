using System;

namespace ConsoleApp.Events
{
    public class UnregisterGameObjectGameEvent : IGameEvent
    {
        public readonly GameObject _gameObject;

        public UnregisterGameObjectGameEvent(GameObject game_object)
        {
            _gameObject = game_object;
        }
    }
}