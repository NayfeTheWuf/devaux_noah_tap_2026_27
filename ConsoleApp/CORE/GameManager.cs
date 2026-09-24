using ConsoleApp.CORE.Components;
using ConsoleApp.CORE.Events;
using ConsoleApp.Events;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApp
{
    public class GameManager
    {
        //Var de base de donnée
        private readonly List<GameObject> _gameObjectTable = new List<GameObject>();
        private List<GameActionType> _gameDestinationTable = new List<GameActionType>();

        //Var d'action
        private bool _isMoving = false;
        private bool _shouldQuit = false;


        //Var de location
        private TravelComponent _heroesTravelComponent;
        private LocationComponent _currentLocation;
        private LocationComponent _destinationLocation;
        private int _selectedDestinationIndex = -1;

        //Enregistrement et utilisation des events
        public GameManager(EventManager event_manager)
        {
            //Event
            event_manager.RegisterToEvent<RegisterGameObjectGameEvent>(OnRegisterGameObjectGameEvent);
            event_manager.RegisterToEvent<UnregisterGameObjectGameEvent>(OnUnregisterGameObjectGameEvent);
            event_manager.RegisterToEvent<GameActionGameEvent>(OnGameActionGameEvent);
            event_manager.RegisterToEvent<TravelGameEvent>(OnTravelGameEvent);

            //WorldMapping
            WorldBuilderManager world_builder = new WorldBuilderManager();
            world_builder.BuildWorld();
            //Enregistrer chaque location
            for (int object_index = 0; object_index < world_builder._locationGameObjects.Count; object_index++)
            {
                event_manager.DelayedTriggerEvent(new RegisterGameObjectGameEvent(world_builder._locationGameObjects[object_index]));
            }

            //Groupe de héros
            GameObject heroes_object = new GameObject("Heroes");
            _heroesTravelComponent = new TravelComponent(world_builder._startingLocation, event_manager);
            heroes_object.AddComponent(_heroesTravelComponent);
            heroes_object.SetIsActive(true);
            event_manager.DelayedTriggerEvent(new RegisterGameObjectGameEvent(heroes_object));
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

        //Réagit à l'arrivée du groupe de héros à destination
        private void OnTravelGameEvent(IGameEvent game_event)
        {
            _selectedDestinationIndex = -1;
        }

        //Change la destination sélectionner
        private void NavigateSelection(int direction)
        {
            int new_direction_index = _selectedDestinationIndex + direction;
            LocationComponent current_location = _heroesTravelComponent.GetCurrentLocation();

            //Héros est en train de se déplacer
            if (_heroesTravelComponent.GetIsMoving())
            {
                return;
            }

            //Si le nombre de liaison est null
            if (current_location.GetLocationTableCount() == 0)
            {
                return;
            }

            if (_selectedDestinationIndex == -1)
            {
                _selectedDestinationIndex = 0;
            }
            else
            {
                //Si aucune, prendre la première
                if (new_direction_index < 0)
                {
                    new_direction_index = 0;
                }
                //Aussi non, prendre le dernier
                else if (new_direction_index >= _gameObjectTable.Count)
                {
                    new_direction_index = _gameObjectTable.Count - 1;
                }

                //Nouvelle direction
                _selectedDestinationIndex = new_direction_index;
            }
        }

        //Lance le déplacement vers la destination sélectionnée, si aucune sélection ne rien faire
        private void ConfirmSelection()
        {
            if (_selectedDestinationIndex == -1 || _heroesTravelComponent.GetIsMoving())
            {
                return;
            }

            LocationComponent current_location = _heroesTravelComponent.GetCurrentLocation();
            LocationComponent destination = current_location.GetLiaisonDestination(_selectedDestinationIndex);
            float duration = current_location.GetLiaisonDuration(_selectedDestinationIndex);

            _heroesTravelComponent.StartTravel(destination, duration);
        }

        //Annule la sélection en cours
        private void CancelSelection()
        {
            _selectedDestinationIndex = -1;
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