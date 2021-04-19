using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;

namespace MasterRPG
{
    public static class MonsterGenerator
    {
        public static List<string> MonsterNames = new List<string>()
        {
            "Goblin",
            "Scorpion",
            "Ogre",
            "Thief",
            "Wizard",
            "CaterSlime",
            "GiantSlime",
            "Shapeshifter",
            "GiantBat"
        };

        public static Actor GenerateMonster(int level)
        {
            var rand = new Random();
            var monster = new Actor();                                                                                                                                            
            var modHealth = level * 10;
            monster.Name = MonsterNames[rand.Next(0, MonsterNames.Count - 1)];
            switch (monster.Name.ToUpper()) 
            {
                case "GOBLIN":
                    monster.GainItem("Sword");
                    monster.GainItem("Helm");
                    monster.UseItem("Sword");
                    monster.UseItem("Helm");
                    break;
                case "SCORPION":
                    break;
                case "OGRE":
                    monster.GainItem("Shield");
                    monster.GainItem("ChestPlate");
                    monster.GainItem("PlateLegs");
                    monster.GainItem("Helm");
                    monster.GainItem("Sword");
                    monster.UseItem("Sword");
                    monster.UseItem("Shield");
                    monster.UseItem("ChestPlate");
                    monster.UseItem("Helm");
                    monster.UseItem("Platelegs");
                    break;
                case "THIEF":
                    monster.GainItem("Bow");
                    monster.UseItem("Bow");
                    monster.GainItem("LeatherTop");
                    monster.UseItem("LeatherTop");
                    monster.GainItem("LeatherBottoms");
                    monster.UseItem("LeatherBottoms");
                    monster.GainItem("Coif");
                    monster.UseItem("Coif");
                    break;
                case "WIZARD":
                    monster.GainItem("Staff");
                    monster.UseItem("Staff");
                    monster.GainItem("WizardRobe");
                    monster.GainItem("WizardBottoms");
                    monster.GainItem("WizardHat");
                    monster.UseItem("WizardRobe");
                    monster.UseItem("WizardBottoms");
                    monster.UseItem("WizardHat");
                    break;
                case "CATERSLIME":
                    break;
                case "GIANTSLIME":
                    break;
                case "SHAPESHIFTER":
                    break;
                case "GIANTBAT":
                    break;
            }


            monster.MaxHealth = modHealth;
            monster.CurrentHealh = monster.MaxHealth;
            monster.GainExp(ExpChart.ExpLevel[level + 1] );

            Console.WriteLine($"Monster Generated: {monster.Name} Health:{monster.MaxHealth} Exp:{monster.Exp}");
            return monster;
        }
    }
}
