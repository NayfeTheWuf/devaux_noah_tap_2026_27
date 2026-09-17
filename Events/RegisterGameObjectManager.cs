using System.Collections.Generic;
using System;

namespace activity_00_tap_26_27.Events
{
    public class RegisterGameObjectGameEventManager
    {           
        //Enregistrer l'évènement
        public void RegisterGameObjectGameEvent (EventManager event_manager)
        {
            event_manager.RegisterToEvent<RegisterGameObjectGameEvent>(OnGameObjectRegister);
        }
        
        //Retirer l'enregistrement de l'évènement
        public void UnregisterGameObjectGameEvent (EventManager event_manager)
        {
            event_manager.UnregisterFromEvent<RegisterGameObjectGameEvent>(OnGameObjectRegister);
        }

        //Excécute l'enregistrement par l'évènement 
        private void OnGameObjectRegister(IGameEvent game_event)
        {
            RegisterGameObjectGameEvent registered_game_object = game_event as RegisterGameObjectGameEvent;

            if(registered_game_object._gameObject is GameObject)
            {
                //GameObject game_object_table = new GameObject(registered_game_object._gameObject);
            }
        }

        //Exécute l'enlèvement par l'évènement
        private void OnGameObjectUnregister(IGameEvent game_event)
        {
            UnregisterGameObjectGameEvent unregistered_game_object = game_event as UnregisterGameObjectGameEvent;

            if(unregistered_game_object._gameObject is GameObject)
            {
                //Inverse de l'autre
            }
        }
    }
}