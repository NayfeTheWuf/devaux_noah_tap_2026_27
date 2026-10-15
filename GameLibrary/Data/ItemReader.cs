using GameLibrary.Csv;
using System;
using System.Globalization;

namespace GameLibrary.Data
{
    public class ItemReader
    {
        //Lit le fichier data des items et report les erreurs
        public static List<ItemData> Read(CsvTable csv_table, List<ShopData> shop_data_table, ContentReport report)
        {
            List<ItemData> item_data_table = new List<ItemData>();
            //Structure de donnée sans ordre particulier
            HashSet<string> item_reference_table = new HashSet<string>();

            for (int csv_row_index = 0; csv_row_index < csv_table.GetRowCount(); csv_row_index++)
            {
                int human_readable_row_number = csv_row_index + 2;
                CsvRow row_content = csv_table.GetRowAtIndex(csv_row_index);
                bool has_error = false;

                //Obtenir les identifications de l'item
                string item_id = row_content.GetCellAtIndex(0);
                string item_name = row_content.GetCellAtIndex(1);
                string shop_id = row_content.GetCellAtIndex(2);
                int price = -1;
                int quantity = -1;

                //Erreur si ItemId est null ou vide
                if (string.IsNullOrEmpty(item_id))
                {
                    report.AddError(csv_table.GetFileName(), human_readable_row_number, $"{item_id} is not a valid reference");
                    has_error = true;
                }

                //Erreur si ItemId possède une référence qui existe déjà
                if (!item_reference_table.Add(item_id))
                {
                    report.AddError(csv_table.GetFileName(), human_readable_row_number, $"{item_id} reference already exists.");
                    has_error = true;
                }

                //Erreur si ItemName est null ou vide
                if (string.IsNullOrEmpty(item_name))
                {
                    report.AddError(csv_table.GetFileName(), human_readable_row_number, $"{item_name} is not a valid name");
                    has_error = true;
                }

                //Erreur si le prix de l'item est négatif
                if (int.TryParse(row_content.GetCellAtIndex(3), NumberStyles.Integer, CultureInfo.InvariantCulture, out price))
                {
                    if (price < 0)
                    {
                        report.AddError(csv_table.GetFileName(), human_readable_row_number, $"Item price: {price} - should be >= 0");
                        has_error = true;
                    }
                }
                //Erreur si le prix de l'item n'est pas un int
                else
                {
                    report.AddError(csv_table.GetFileName(), human_readable_row_number, 
                        $"{row_content.GetCellAtIndex(3)} is not a valid price, it should be integer.");
                }

                //Erreur si la quantité de l'item est négatif
                if (int.TryParse(row_content.GetCellAtIndex(4), NumberStyles.Integer, CultureInfo.InvariantCulture, out quantity))
                {
                    if (quantity < 0)
                    {
                        report.AddError(csv_table.GetFileName(), human_readable_row_number, $"quantity >= 0");
                        has_error = true;
                    }
                }
                //Erreur si la quantité de l'item n'est pas un int
                else
                {
                    report.AddError(csv_table.GetFileName(), human_readable_row_number,
                                    $"{row_content.GetCellAtIndex(4)} is not a valid quantity, it should be integer.");
                }

                //Si la lecture ne contient pas d'erreur, le rajouté à la table de données des items
                if (!has_error)
                {
                    ItemData item_data = new ItemData(item_id, item_name, shop_id, price, quantity);

                    item_data_table.Add(item_data);
                }

                bool is_shop_found = false;

                //Trouve le shop
                for (int shop_index = 0; shop_index < shop_data_table.Count; shop_index++)
                {
                    if (shop_data_table[shop_index]._locationId == shop_id)
                    {
                        is_shop_found = true;
                    }
                }

                //Erreur si le shop n'est pas trouvé
                if (!is_shop_found)
                {
                    report.AddError(csv_table.GetFileName(), human_readable_row_number, $"Shop reference {shop_id} not found.");
                }
            }

            return item_data_table;

        }
    }
}
