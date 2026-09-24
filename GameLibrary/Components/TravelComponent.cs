using GameLibrary.Events;

namespace GameLibrary.Components
{
    public class TravelComponent : Component
    {
        private readonly EventManager _eventManager;

        //Var de location
        private LocationComponent _currentLocation;
        private LocationComponent _destinationLocation;
        private float _travelDuration;

        //Var d'informations
        private bool _isMoving = false;
        private float _elapsedTime;

        public TravelComponent(LocationComponent current_location, EventManager event_manager)
        {
            _currentLocation = current_location;
            _eventManager = event_manager;
        }

        //Retourne le lieu actuel
        public LocationComponent GetCurrentLocation()
        {
            return _currentLocation;
        }

        //Retourne le statut de déplacement
        public bool GetIsMoving()
        {
            return _isMoving;
        }

        //Retourne l'avancement du déplacement en cours
        public float GetProgress()
        {
            float progress_ratio = _elapsedTime / _travelDuration;

            //Si pas de déplacement
            if (_isMoving == false || _travelDuration <= 0f)
            {
                return 0f;
            }

            if (progress_ratio > 1f)
            {
                progress_ratio = 1f;
            }
            return progress_ratio;
        }

        //Démarre un déplacement
        public void StartTravel(LocationComponent destination, float duration)
        {
            _destinationLocation = destination;
            _travelDuration = duration;
            _elapsedTime = 0f;
            _isMoving = true;

            //Log tout propre
            _eventManager.TriggerEvent(new LogMessageGameEvent
                (   
                    $"Start {_currentLocation.GetLocationName()} " +
                    $"to {destination.GetLocationName()} " +
                    $"(time : {duration} seconds).")
                );
        }

        //Fait avancer le déplacement en cours
        public override void FixedUpdate(float elapsed_fixed_time)
        {
            _elapsedTime += elapsed_fixed_time;

            if (_isMoving == false)
            {
                return;
            }

            //Le trajet n'est pas encore terminé
            if (_elapsedTime < _travelDuration)
            {
                return;
            }

            //Le trajet est terminé
            _currentLocation = _destinationLocation;
            _destinationLocation = null;
            _isMoving = false;
            _elapsedTime = 0f;

            //Log
            _eventManager.TriggerEvent(new LogMessageGameEvent($"Enter in {_currentLocation.GetLocationName()}."));
            //Informe l'arriver
            _eventManager.TriggerEvent(new TravelGameEvent(this));
        }
    }
}
