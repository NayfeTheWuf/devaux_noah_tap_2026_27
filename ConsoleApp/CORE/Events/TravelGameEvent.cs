using ConsoleApp.CORE.Components;
using ConsoleApp.Events;
using System;

namespace ConsoleApp.CORE.Events
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
