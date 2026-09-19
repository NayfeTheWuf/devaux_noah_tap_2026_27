using System.Collections.Generic;
using activity_00_tap_26_27.Events;

namespace activity_00_tap_26_27
{
    public class GameManager
    {
        //Var de base de donnée
        private readonly List<GameObject> _gameObjectTable = new List<GameObject>();

        //Enregistrement et utilisation des events
        public GameManager(EventManager event_manager)
        {
            event_manager.RegisterToEvent<RegisterGameObjectGameEvent>(OnRegisterGameObjectGameEvent);
            event_manager.RegisterToEvent<UnregisterGameObjectGameEvent>(OnUnregisterGameObjectGameEvent);

            //Objet de test
            GameObject object_testing = new GameObject("test_object");
            event_manager.DelayedTriggerEvent(new RegisterGameObjectGameEvent(object_testing));
        }

        //Enregistrer un objet dans un event
        private void OnRegisterGameObjectGameEvent(IGameEvent game_event)
        {
            RegisterGameObjectGameEvent register_game_object_game_event = game_event as RegisterGameObjectGameEvent;

            _gameObjectTable.Add(register_game_object_game_event._gameObject);
        }

        //Retirer un objet dans un event
        private void OnUnregisterGameObjectGameEvent(IGameEvent game_event)
        {
            UnregisterGameObjectGameEvent unregister_game_object_game_event = game_event as UnregisterGameObjectGameEvent;

            _gameObjectTable.Remove(unregister_game_object_game_event._gameObject);
        }

        //Mise à jour à intervalles de temps fixes        
        public void FixedUpdate(float fixed_elapsed_time)
        {
            for (int object_index = 0; object_index < _gameObjectTable.Count; object_index++)
            {
                GameObject game_object = _gameObjectTable[object_index];

                if (game_object.GetIsActive())
                {
                    game_object.FixedUpdate(fixed_elapsed_time);
                }
            }
        }

        //Mise à jour à chaque frame
        public void Update(float elapsed_time)
        {
            for (int object_index = 0; object_index < _gameObjectTable.Count; object_index++)
            {
                GameObject game_object = _gameObjectTable[object_index];

                if (game_object.GetIsActive())
                {
                    game_object.Update(elapsed_time);
                }
            }
        }
    }
}