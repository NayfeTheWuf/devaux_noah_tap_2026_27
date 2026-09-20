using activity_00_tap_26_27.CORE.Components;
using activity_00_tap_26_27.Events;
using System;

namespace activity_00_tap_26_27.CORE.Events
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
