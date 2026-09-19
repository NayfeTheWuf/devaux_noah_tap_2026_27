using System.Collections.Generic;
using System;

namespace activity_00_tap_26_27.Events
{
    public class EventManager
    {
        private readonly Dictionary<Type, List<Action<IGameEvent>>> _eventTypeTable = new Dictionary<Type, List<Action<IGameEvent>>>();
        private Queue<IGameEvent> _eventQueue = new Queue<IGameEvent>();

        //Enregistre un évènement dans la table d'évènement
        public void RegisterToEvent<TYPE>(Action<IGameEvent> action) where TYPE : IGameEvent
        {
            Type event_type = typeof(TYPE);

            if (!_eventTypeTable.ContainsKey(event_type))
            {
                _eventTypeTable.Add(event_type, new List<Action<IGameEvent>>());
            }

            _eventTypeTable[event_type].Add(action);
        }

        //Supprime un évènement  dans la table d'évènement
        public void UnregisterFromEvent<TYPE>(Action<IGameEvent> action) where TYPE : IGameEvent
        {
            Type event_type = typeof(TYPE);

            if (_eventTypeTable.ContainsKey(event_type))
            {
                _eventTypeTable[event_type].Remove(action);
            }
        }

        //Exécute un évènement spécifique
        public void TriggerEvent(IGameEvent game_event)
        {
            Type event_type = game_event.GetType();

            if (_eventTypeTable.ContainsKey(event_type))
            {
                for (int event_index = 0; event_index < _eventTypeTable[event_type].Count; event_index++)
                {
                    Action<IGameEvent> action = _eventTypeTable[event_type][event_index];
                    action(game_event);
                }
            }
        }

        //Exécute un évènement spécifique à un moment voulu
        public void DelayedTriggerEvent (IGameEvent game_event)
        {
            _eventQueue.Enqueue(game_event);
        }

        //Traités les évènments au moment choisi
        public void ProcessEvent()
        {
            while(_eventQueue.Count > 0)
            {
                IGameEvent game_event = _eventQueue.Dequeue();

                if (_eventTypeTable.TryGetValue(game_event.GetType(), out List<Action<IGameEvent>> actions)) 
                { 
                    foreach (Action<IGameEvent> action in actions) 
                    { 
                        action(game_event); 
                    } 
                }
            }
        }
    }
}