using GameLibrary.Components;
using GameLibrary.Events;
using GameLibrary.States;

namespace GameLibrary
{
    public class GameManager
    {
        //Var de base de donnée
        private readonly List<GameObject> _gameObjectTable = new List<GameObject>();


        //Var d'états
        private readonly EventManager _eventManager;
        private readonly StateMachine _gameFlowStateStateMachine;

        //Var d'action
        private bool _shouldQuit = false;
        private bool _isMenuStateActive = true;

        //Var de location
        private TravelComponent _heroesTravelComponent;
        private int _selectedDestinationIndex = -1;

        //Enregistrement et utilisation des events
        public GameManager(EventManager event_manager)
        {
            _eventManager = event_manager;

            //Event
            event_manager.RegisterToEvent<RegisterGameObjectGameEvent>(OnRegisterGameObjectGameEvent);
            event_manager.RegisterToEvent<UnregisterGameObjectGameEvent>(OnUnregisterGameObjectGameEvent);
            event_manager.RegisterToEvent<TravelGameEvent>(OnTravelGameEvent);

            //Etat menu principal 
            _gameFlowStateStateMachine = new StateMachine(event_manager);
            _gameFlowStateStateMachine.SetInitialState(new MainMenuState(this, event_manager));
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

        //Réagit à l'arrivée du groupe de héros à destination
        private void OnTravelGameEvent(IGameEvent game_event)
        {
            _selectedDestinationIndex = -1;
        }

        //Change la destination sélectionner
        public void NavigateSelection(int direction)
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
                else if (new_direction_index >= current_location.GetLocationTableCount())
                {
                    new_direction_index = current_location.GetLocationTableCount() - 1;
                }

                //Nouvelle direction
                _selectedDestinationIndex = new_direction_index;
            }
        }

        //Lance le déplacement vers la destination sélectionnée, si aucune sélection ne rien faire
        public void ConfirmSelection()
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
        public void CancelSelection()
        {
            _selectedDestinationIndex = -1;
        }

        //Construit un monde et donne les droits à l'exploration
        public void StartExploration()
        {
            //Commencé de 0
            _gameObjectTable.Clear();
            _selectedDestinationIndex = -1;

            //Créer le monde
            WorldBuilderManager world_builder = new WorldBuilderManager();
            world_builder.BuildWorld();

            //Donner les locations
            LocationComponent starting_location = world_builder.GetStartingLocation();
            List<GameObject> location_game_objects = world_builder.GetLocationGameObjects();

            //Enregistrer chaque location
            for (int object_index = 0; object_index < location_game_objects.Count; object_index++)
            {
                _eventManager.DelayedTriggerEvent(new RegisterGameObjectGameEvent(location_game_objects[object_index]));
            }

            //Groupe de héros
            GameObject heroes_object = new GameObject("Heroes");
            _heroesTravelComponent = new TravelComponent(starting_location, _eventManager);
            heroes_object.AddComponent(_heroesTravelComponent);
            heroes_object.SetIsActive(true);
            _eventManager.DelayedTriggerEvent(new RegisterGameObjectGameEvent(heroes_object));

            //Gérer l'état du jeu
            _isMenuStateActive = false;
            _gameFlowStateStateMachine.ChangeState(new ExploringState(this, _eventManager));
        }

        //Retourne l'arrêt
        public bool RequestQuit()
        {
            return _shouldQuit;
        }

        //Remonte au lieu parent ou retourne au menu si on y est déjà
        public void RequestQuitFromExploration()
        {
            if (_heroesTravelComponent.GetIsMoving())
            {
                return;
            }

            LocationComponent current_location = _heroesTravelComponent.GetCurrentLocation();
            LocationComponent parent_location = current_location.GetParentLocation();

            //Retour au menu
            if (parent_location == null)
            {
                _isMenuStateActive = true;
                _gameFlowStateStateMachine.ChangeState(new MainMenuState(this, _eventManager));
                return;
            }

            float duration_to_parent = GetDurationToNeighbor(current_location, parent_location);

            _heroesTravelComponent.StartTravel(parent_location, duration_to_parent);
        }


        //Donne la durée à un voisin
        private float GetDurationToNeighbor(LocationComponent current_location, LocationComponent neighbor_location)
        {
            for (int destination_index = 0; destination_index < current_location.GetLocationTableCount(); destination_index++)
            {
                if (current_location.GetLiaisonDestination(destination_index) == neighbor_location)
                {
                    return current_location.GetLiaisonDuration(destination_index);
                }
            }

            return 0f;
        }

        //Retourne l'arrêt
        public bool GetShouldQuit()
        {
            return _shouldQuit;
        }

        //Retourne si le menu principal est actif
        public bool GetIsMenuStateActive()
        {
            return _isMenuStateActive;
        }


        //Retourne le nom du lieu actuel
        public string GetCurrentLocationName()
        {
            return _heroesTravelComponent.GetCurrentLocation().GetLocationName();
        }

        //Retourne le nombre de destinations accessibles
        public int GetDestinationCount()
        {
            LocationComponent current_location = _heroesTravelComponent.GetCurrentLocation();
            return current_location.GetLocationTableCount();
        }

        //Retourne le nom de la destination demandée
        public string GetDestinationName(int destination_index)
        {
            LocationComponent current_location = _heroesTravelComponent.GetCurrentLocation();
            return current_location.GetLiaisonDestination(destination_index).GetLocationName();
        }

        //Retourne la durée du trajet vers la destination demandée
        public float GetDestinationDuration(int destination_index)
        {
            LocationComponent current_location = _heroesTravelComponent.GetCurrentLocation();
            return current_location.GetLiaisonDuration(destination_index);
        }

        //Retourne l'index actuellement sélectionné
        public int GetSelectedDestinationIndex()
        {
            return _selectedDestinationIndex;
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