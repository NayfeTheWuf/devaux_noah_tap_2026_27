using activity_00_tap_26_27.CORE.Components;
using activity_00_tap_26_27.CORE.Events;
using activity_00_tap_26_27.Events;
using System.Collections.Generic;

namespace activity_00_tap_26_27
{
    public class GameManager
    {
        //Var de base de donnée
        private readonly List<GameObject> _gameObjectTable = new List<GameObject>();

        //Var d'action
        private List<GameActionType> _gameDestinationTable = new List<GameActionType>();
        private int _selectedDestinationIndex = 0;
        private bool _isMoving = false;
        private bool _shouldQuit = false;

        //Var de location
        private LocationComponent _currentLocation;
        private LocationComponent _destinationLocation;

        //Enregistrement et utilisation des events
        public GameManager(EventManager event_manager)
        {
            event_manager.RegisterToEvent<RegisterGameObjectGameEvent>(OnRegisterGameObjectGameEvent);
            event_manager.RegisterToEvent<UnregisterGameObjectGameEvent>(OnUnregisterGameObjectGameEvent);
            event_manager.RegisterToEvent<GameActionGameEvent>(OnGameActionGameEvent);

            //Objet de test
            GameObject object_testing = new GameObject("test_object");
            event_manager.DelayedTriggerEvent(new RegisterGameObjectGameEvent(object_testing));

            WorldBuilderManager world_builder = new WorldBuilderManager();
            world_builder.BuildWorld();

            //Enregistrer chaque location
            for (int object_index = 0; object_index < world_builder._locationGameObjects.Count; object_index++)
            {
                event_manager.DelayedTriggerEvent(new RegisterGameObjectGameEvent(world_builder._locationGameObjects[object_index]));
            }

            _currentLocation = world_builder._startingLocation;
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

        //Réaction au input 
        private void OnGameActionGameEvent(IGameEvent game_event)
        {
            GameActionGameEvent game_action_game_event = game_event as GameActionGameEvent;

            switch (game_action_game_event._gameActionType)
            {
                case GameActionType.NAVIGATE_UP:
                    NavigateSelection(-1);
                    
                    break;

                case GameActionType.NAVIGATE_DOWN:
                    NavigateSelection(1);
                    break;

                case GameActionType.CONFIRM:
                    ConfirmSelection();
                    break;

                case GameActionType.CANCEL:
                    CancelSelection();
                    break;

                case GameActionType.QUIT:
                    _shouldQuit = true;
                    break;
            }
        }

        //Change la destination sélectionner
        private void NavigateSelection(int direction)
        {
            int new_direction_index = _selectedDestinationIndex + direction;

            //Pendant un déplacement, les actions de naviguation sont ignorées
            if (_isMoving)
            {
                return;
            }
            //Si aucune destination enregistrée
            if (_gameDestinationTable.Count == 0)
            {
                return;
            }

            //Si aucune, prendre la première
            if (new_direction_index < 0)
            {
                new_direction_index = 0;
            }
            //Aussi non, prendre nouvelle direction
            else if (new_direction_index >= _gameObjectTable.Count)
            {
                new_direction_index = _gameObjectTable.Count - 1;
            }

            _selectedDestinationIndex = new_direction_index;
        }

        //Lance le déplacement vers la destination sélectionnée, si aucune sélection ne rien faire
        private void ConfirmSelection()
        {
            if (_selectedDestinationIndex == 0 || _isMoving)
            {
                return;
            }

            _destinationLocation = _currentLocation.GetLiaisonDestination(_selectedDestinationIndex);
            _isMoving = true;
        }

        //Annule la sélection en cours
        private void CancelSelection()
        {
            _selectedDestinationIndex = 0;
        }

        //Retourne l'arrêt
        public bool GetShouldQuit()
        {
            return _shouldQuit;
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