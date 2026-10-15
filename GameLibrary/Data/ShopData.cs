using System;

namespace GameLibrary.Data
{
    public class ShopData
    {
        //Var
        public string _locationId;
        public int _openingHour;
        public int _closingHour;

        //Constructeur
        public ShopData(string location_id, int opening_hour, int closing_hour) 
        {
            _locationId = location_id;
            _openingHour = opening_hour;
            _closingHour = closing_hour;
        }
    }
}
