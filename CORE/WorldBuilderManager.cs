using activity_00_tap_26_27.CORE.Components;
using System;
using System.Collections.Generic;

namespace activity_00_tap_26_27
{
    public class WorldBuilderManager
    {
        //Var de création du monde
        public List<GameObject> _locationGameObjects = new List<GameObject>();
        public LocationComponent _startingLocation;

        public void BuildWorld()
        {
            LocationComponent world_location = CreateLocation("The World");

            LocationComponent region_location = CreateLocation("Brumes Island");

            LocationComponent town_location = CreateLocation("FireTown");

            LocationComponent shop_location = CreateLocation("Klerck Shop");
            LocationComponent inn_location = CreateLocation("Hostel");
            
            LocationComponent dungeon_location = CreateLocation("Forgotten Dungeon");

            LocationComponent dungeon_floor_1_location = CreateLocation("Dungeon - Floor 1");
            LocationComponent dungeon_floor_2_location = CreateLocation("Dungeon - Floor 2");

            world_location.LinkTo(region_location, 10f);

            region_location.LinkTo(town_location, 5f);
            region_location.LinkTo(dungeon_location, 10f);

            town_location.LinkTo(shop_location, 1f);
            town_location.LinkTo(inn_location, 1f);

            dungeon_location.LinkTo(dungeon_floor_1_location, 2f);
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
