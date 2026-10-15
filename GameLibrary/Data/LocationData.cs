using System;

namespace GameLibrary.Data
{
    public class LocationData
    {
        //Var
        public readonly string _id;
        public readonly string _name;
        public readonly string _parentId;
        public readonly int _travelMinutes;

        //Constructeur
        public LocationData(string id, string name, string parent_id, int travel_minutes)
        {
            _id = id;
            _name = name;
            _parentId = parent_id;
            _travelMinutes = travel_minutes;
        }
    }
}
