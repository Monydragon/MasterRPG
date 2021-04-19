using Newtonsoft.Json;
using System;
using System.IO;

namespace MasterRPG
{
    class Program
    {
        public static Actor Player;
        public static string saveDirectory = Directory.GetCurrentDirectory() + "\\Saves\\";
        static void Main(string[] args)
        {
            ItemDatabase.Initalize();
            EnterInput();
        }

        public static void EnterInput()
        {
            ENTER_INPUT:
            Console.WriteLine("Enter Input: NEW, LOAD");
            var input = Console.ReadLine();

            switch (input.ToUpper())
            {
                case "NEW":
                    Console.WriteLine("Started a new game");
                    Player = new Actor();
                    Console.WriteLine("Enter a name for the player");
                    var name = Console.ReadLine();
                    Player.Name = name;
                    Console.WriteLine("How old are you?");
                    Player.Age = int.Parse(Console.ReadLine());
                    Console.WriteLine("Describe yourself");
                    Player.Descripton = Console.ReadLine();
                    Console.WriteLine($"{Player.Name} Welcome to RPG World.");

                    Player.GainItem("Potion");
                    Player.GainItem("Potion");
                    Player.GainItem("Potion");
                    Player.GainItem("Sword");
                    Player.GainItem("ChestPlate");
                    Player.GainItem("Helm");
                    Player.GainItem("PlateLegs");
                    Player.GainItem("Shield");
                    Player.UseItem("Sword");
                    Player.UseItem("ChestPlate");
                    Player.UseItem("Helm");
                    Player.UseItem("Platelegs");
                    Player.UseItem("Shield");

                RPG_INPUT:
                    Console.WriteLine("What would you like to do? Fight, Explore, Status, Save");
                    var rpgInput = Console.ReadLine();

                    switch (rpgInput.ToUpper())
                    {
                        case "FIGHT":
                            var enemy = MonsterGenerator.GenerateMonster(Player.Level);
                            var battle = new BattleSystem(Player, enemy);
                            battle.BattleUpdate();
                            break;
                        case "EXPLORE":
                            Console.WriteLine("You Explore around");
                            break;
                        case "STATUS":
                            Console.WriteLine($"{Player.Name} Age: {Player.Age} Level: {Player.Level}");
                            Console.WriteLine($"HP: {Player.CurrentHealh} / {Player.MaxHealth}");
                            Console.WriteLine($"Melee Atk: {Player.MeleeAtk} Melee Def: {Player.MeleeDef}");
                            Console.WriteLine($"Ranged Atk: {Player.RangedAtk} Ranged Def: {Player.RangedDef}");
                            Console.WriteLine($"Magic Atk: {Player.MagicAtk} Magic Def: {Player.MagicDef}");
                            break;
                        case "SAVE":
                            Console.WriteLine("Saved Game");
                            var playerSave = JsonConvert.SerializeObject(Player, Formatting.Indented);
                            
                            if (!Directory.Exists(saveDirectory))
                            {
                                Directory.CreateDirectory(saveDirectory);
                            }
                            File.WriteAllText(saveDirectory + $"{Player.Name}.json", playerSave);
                            break;
                        default:
                            goto RPG_INPUT;
                            break;
                    }

                    goto RPG_INPUT;
                    break;
                case "LOAD":
                    Console.WriteLine("Load a save");
                    Console.WriteLine("Saves");
                    var saves = Directory.GetFiles(saveDirectory);
                    for (int i = 0; i < saves.Length; i++)
                    {
                        string item = saves[i];
                        Console.WriteLine($"{i} - Save: {item}");
                    }
                    Console.WriteLine("Please choose a save to load");
                    var selectedSave = Console.ReadLine();
                    var save = File.ReadAllText(saves[int.Parse(selectedSave)]);
                    Player = JsonConvert.DeserializeObject<Actor>(save);
                    goto RPG_INPUT;
                    break;

                 default:
                    goto ENTER_INPUT;
                    break;
            }
        }
    }
}
