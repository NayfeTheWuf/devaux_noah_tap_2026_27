using System;
using GameLibrary.Data;

namespace GameLibrary.Components
{
    public class ShopComponent : Component
    {
        //Var
        private readonly float GAME_MINUTES_PER_SECOND = 10.0f;

        private ShopData _shopData;
        private List<ItemData> _itemDataTable;
        private float _timeInMinutes;

        //Constructeur
        public ShopComponent(ShopData shop_data, List<ItemData> item_data_table, GameObject game_object) : base(game_object)
        {
            _shopData = shop_data;
            _itemDataTable = item_data_table;
            _timeInMinutes = 8 * 60;
        }

        //Retourne le temps en heure
        public int GetHour()
        {
            return (int)MathF.Floor(_timeInMinutes / 60.0f);
        }

        //Retourne le temps en minute
        public int GetMinute()
        {
            return (int)MathF.Floor(_timeInMinutes % 60);
        }

        //Méthode de mise à jour du temps à interval spécifique
        public override void FixedUpdate(float elapsed_fixed_time)
        {
            _timeInMinutes += elapsed_fixed_time * GAME_MINUTES_PER_SECOND;
            _timeInMinutes %= 24 * 60;
        }

        //Retourne les données du shop
        public ShopData GetShopData()
        {
            return _shopData;
        }

        //Retourne le statut du shop
        public bool GetIsOpen()
        {
            return GetHour() >= _shopData._openingHour && GetHour() <= _shopData._closingHour;
        }

        //Retourne le nombre d'item dans la table de données
        public int GetItemCount()
        {
            return _itemDataTable.Count;
        }

        //Retourne l'item à l'index spécifié
        public ItemData GetItemAtIndex(int item_index)
        {
            return _itemDataTable[item_index];
        }

        //Retourne la quantité à l'index spécifié
        public int GetQuantityAtIndex(int item_index)
        {
            return _itemDataTable[item_index]._quantity;
        }

        //Tente de retirer une quantité à l'index spécifié
        public bool TryRemoveOneAtIndex(int item_index)
        {
            if (_itemDataTable[item_index]._quantity > 0)
            {
                _itemDataTable[item_index]._quantity -= 1;

                return true;
            }

            return false;
        }
    }
}
