using activity_00_tap_26_27.Events;
using activity_00_tap_26_27.CORE.Components;

namespace activity_00_tap_26_27.CORE.Events
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
