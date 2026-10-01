using GameLibrary.Components;

namespace GameLibrary
{
    public class WorldBuilderManager
    {
        //Var de création du monde
        private List<GameObject> _locationGameObjects = new List<GameObject>();
        private LocationComponent _startingLocation;

        //Retourner la première location de départ
        public LocationComponent GetStartingLocation()
        {
            return _startingLocation;
        }

        //Retourner la liste de location
        public List<GameObject> GetLocationGameObjects()
        {
            return _locationGameObjects;
        }

        //Créer le monde du jeu
        public void BuildWorld()
        {
            LocationComponent world_location = CreateLocation("The World");

            LocationComponent region_location = CreateLocation("Brumes Island");
            world_location.LinkTo(region_location, 10f);

            LocationComponent town_location = CreateLocation("FireTown");
            region_location.LinkTo(town_location, 5f);

            LocationComponent shop_location = CreateLocation("Klerck Shop");
            town_location.LinkTo(shop_location, 1f);
            LocationComponent inn_location = CreateLocation("Hostel");
            town_location.LinkTo(inn_location, 1f);

            LocationComponent dungeon_location = CreateLocation("Forgotten Dungeon");
            region_location.LinkTo(dungeon_location, 10f);

            LocationComponent dungeon_floor_1_location = CreateLocation("Dungeon - Floor 1");
            dungeon_location.LinkTo(dungeon_floor_1_location, 2f);
            LocationComponent dungeon_floor_2_location = CreateLocation("Dungeon - Floor 2");
            dungeon_floor_1_location.LinkTo(dungeon_floor_2_location, 3f);

            //Le joueur doit commencer dans la ville
            _startingLocation = town_location;
        }

        //Crée un GameObject pour assigner les locations
        private LocationComponent CreateLocation(string location_name)
        {
            GameObject location_object = new GameObject(location_name);
            LocationComponent location_component = new LocationComponent(location_name);

            location_object.AddComponent(location_component);
            location_object.SetIsActive(true);

            _locationGameObjects.Add(location_object);
            return location_component;
        }
    }
}
