namespace GameLibrary.Components
{
    public class LocationComponent : Component
    {
        //Var de location
        private string _locationName;
        private List<ConnectionComponent> _liaisonLocationTable = new List<ConnectionComponent>();

        public LocationComponent(string location_name)
        {
            _locationName = location_name;
        }

        //Retourne le nom de la location
        public string GetLocationName()
        {
            return _locationName;
        }

        //Retourne le nombre de liaison à un lieu // de voisin potentiel
        public int GetLocationTableCount()
        {
            return _liaisonLocationTable.Count;
        }

        //Retourne le lieu voisin voulu
        public LocationComponent GetLiaisonDestination(int destination_index)
        {
            return _liaisonLocationTable[destination_index].GetDestination();
        }

        //Retourne la durée du trajet 
        public float GetLiaisonDuration(int duration_index)
        {
            return _liaisonLocationTable[duration_index].GetDuration();
        }

        //Ajoute une liaison
        public void AddLiaison(LocationComponent destination, float duration)
        {
            _liaisonLocationTable.Add(new ConnectionComponent(destination, duration));
        }

        //Relie les lieux entre eux
        public void LinkTo(LocationComponent other_destination, float duration)
        {
            //Pouvoir aller d'un lieu à un autre lieu
            this.AddLiaison(other_destination, duration);
            //Pouvoir revenir de cet autre lieu au lieu précédant
            other_destination.AddLiaison(this, duration);
        }
    }
}
