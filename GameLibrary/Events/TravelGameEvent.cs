using GameLibrary.Components;

namespace GameLibrary.Events
{
    public class TravelGameEvent : IGameEvent
    {
        public readonly TravelComponent _travelComponent;

        public TravelGameEvent(TravelComponent travel_component)
        {
            _travelComponent = travel_component;
        }
    }
}
