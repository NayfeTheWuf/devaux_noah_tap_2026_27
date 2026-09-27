namespace GameLibrary.Components
{
    public class ConnectionComponent
    {
        //Var
        private readonly LocationComponent _destination;
        private readonly float _duration;

        public ConnectionComponent(LocationComponent destination, float duration)
        {
            _destination = destination;
            _duration = duration;
        }

        //Retourne le lieu voisin
        public LocationComponent GetDestination()
        {
            return _destination;
        }

        //Retourne la durée du trajet du lieu voisin
        public float GetDuration()
        {
            return _duration;
        }
    }
}
