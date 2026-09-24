using ConsoleApp.Events;
using ConsoleApp.CORE.Components;

namespace ConsoleApp.CORE.Events
{
    public class GameActionGameEvent : IGameEvent
    {
        public GameActionType _gameActionType;

        public GameActionGameEvent(GameActionType game_action_type)
        {
            _gameActionType = game_action_type;
        }
    }
}
