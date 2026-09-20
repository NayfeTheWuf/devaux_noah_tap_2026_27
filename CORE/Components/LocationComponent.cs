using activity_00_tap_26_27.Components;
using System.Collections.Generic;

namespace activity_00_tap_26_27.CORE.Components
{
    public class LocationComponent : Component
    {
        //Var de location
        private string _location_name;
        private List<(LocationComponent Destination, float Duration)> _liaisonLocationTable = new List<(LocationComponent, float)>();

        public LocationComponent(string location_name)
        {
            _location_name = location_name;
        }

        public string GetLocationName()
        {
            return _location_name;
        }

        //Nombre de liaison à un lieu // de voisin potentiel
        public int GetLocationTableCount()
        {
            return _liaisonLocationTable.Count;
        }

        //Retourne le lieu voisin voulu
        public LocationComponent GetLiaisonDestination(int destination_index)
        {
            return _liaisonLocationTable[destination_index].Destination;
        }

        //Retourne la durée du trajet 
        public float GetLiaisonDuration(int duration_index)
        {
            return _liaisonLocationTable[duration_index].Duration;
        }

        //Ajoute une liaison
        public void AddLiaison(LocationComponent destination, float duration)
        {
            _liaisonLocationTable.Add((destination, duration));
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
