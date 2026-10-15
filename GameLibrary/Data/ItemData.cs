using System;

namespace GameLibrary.Data
{
    public class ItemData
    {
        //Var
        public string _id;
        public string _name;
        public string _shopId;
        public int _price;
        public int _quantity;

        //Constructeur
        public ItemData(string id, string name, string shop_id, int price, int quantity)
        {
            _id = id;
            _name = name;
            _shopId = shop_id;
            _price = price;
            _quantity = quantity;
        }
    }
}
