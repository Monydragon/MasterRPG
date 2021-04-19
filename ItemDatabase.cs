using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;

namespace MasterRPG
{
    public static class ItemDatabase
    {
        public static List<Item> Items = new List<Item>();
        public static string ItemDatabaseFilepath = Directory.GetCurrentDirectory() + @"\Database";
        public static void Initalize()
        {
            Items = new List<Item>()
            {
                new Item("Potion", "A Healing Potion", "Consumable Heal:20", 10),
                new Item("Super Potion", "A Strong Healing Potion", "Consumable Heal:50", 30),
                new Equipment("Sword", "A Basic Sword", "Equip", 25, EquipmentSlot.Weapon, 2,0,0,0,0,0),
                new Equipment("Bow", "A Basic Bow", "Equip", 25, EquipmentSlot.Weapon, 0,2,0,0,0,0),
                new Equipment("Staff", "A Basic Staff", "Equip", 25, EquipmentSlot.Weapon, 0,0,2,0,0,0),
                new Equipment("Shield", "A Basic Shield", "Equip", 25, EquipmentSlot.Shield, 0,0,0,1,2,-2),
                new Equipment("ChestPlate", "Basic ChestPlate", "Equip", 25, EquipmentSlot.Torso, 0,0,0,1,2,-2),
                new Equipment("Helm", "Basic Helm", "Equip", 25, EquipmentSlot.Head, 0,0,0,1,2,-2),
                new Equipment("PlateLegs", "Basic PlateLegs", "Equip", 25, EquipmentSlot.Legs, 0,0,0,1,2,-2),
                new Equipment("WizardHat", "A Simple Wizard hat", "Equip", 25, EquipmentSlot.Head, 0,0,0,1,-2,1),
                new Equipment("WizardRobe", "Basic Robetop", "Equip", 25, EquipmentSlot.Torso, 0,0,0,1,-2,1),
                new Equipment("WizardBottoms", "Basic RobeBottom", "Equip", 25, EquipmentSlot.Legs, 0,0,0,1,-2,1),
                new Equipment("Coif", "Basic Coif", "Equip", 25, EquipmentSlot.Head, 0,0,0,1,1,2),
                new Equipment("LeatherTop", "Basic Leather Top", "Equip", 25, EquipmentSlot.Torso, 0,0,0,1,1,2),
                new Equipment("LeatherBottoms", "Basic Leather Bottoms", "Equip", 25, EquipmentSlot.Legs, 0,0,0,1,1,2)

            };

            if (!Directory.Exists(ItemDatabaseFilepath))
            {
                Directory.CreateDirectory(ItemDatabaseFilepath);
            }

            var itemsJson = JsonConvert.SerializeObject(Items, Formatting.Indented);

            File.WriteAllText(ItemDatabaseFilepath +"\\Items.json", itemsJson);

        }

        public static Item GetItem(string name)
        {
            return Items.Find(x => x.Name.ToUpper() == name.ToUpper());
        }
    }
}
