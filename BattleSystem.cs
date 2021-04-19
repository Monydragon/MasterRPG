using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MasterRPG
{
    public class BattleSystem
    {
        public Actor Player;
        public Actor Enemy;
        public GameState State;

        public BattleSystem(Actor player, Actor enemy)
        {
            Player = player;
            Enemy = enemy;
        }

        public void BattleUpdate()
        {
            var rand = new Random();
            Player.Initative = rand.Next(1, 20);
            Enemy.Initative = rand.Next(1, 20);
            Console.WriteLine($"Player Initative: {Player.Initative} Enemy Initative: {Enemy.Initative}");

            if(Player.Initative >= Enemy.Initative)
            {
                State = GameState.PlayerTurn;
            }
            else
            {
                State = GameState.EnemyTurn;
            }

            while (Player.isAlive || Enemy.isAlive)
            {
                switch (State)
                {
                    case GameState.PlayerTurn:
                    ACTION_SELECT:
                        Console.WriteLine("Please choose an action: Attack, Items, Flee");
                        var action = Console.ReadLine();
                        if (action.ToUpper() == "ATTACK")
                        {
                        ATTACK_TYPE_SELECT:
                            Console.WriteLine("Please choose an attack type: Melee, Ranged, Magic, Back");
                            var attackType = Console.ReadLine();
                            switch (attackType.ToUpper())
                            {
                                case "MELEE":
                                    Player.Attack(Enemy, AttackType.Melee);
                                    break;
                                case "RANGED":
                                    Player.Attack(Enemy, AttackType.Ranged);
                                    break;
                                case "MAGIC":
                                    Player.Attack(Enemy, AttackType.Magic);
                                    break;
                                case "BACK":
                                    goto ACTION_SELECT;
                                    break;
                                default:
                                    goto ATTACK_TYPE_SELECT;
                                    break;
                            }
                            if (Enemy.CurrentHealh <= 0)
                            {
                                State = GameState.Win;
                            }
                            else
                            {
                                State = GameState.EnemyTurn;
                            }
                        }
                        else if (action.ToUpper() == "ITEMS")
                        {
                            INVENTORY_ITEMS:
                            Console.WriteLine("Inventory");
                            foreach (var item in Player.Inventory)
                            {
                                Console.WriteLine($"{item.Name}:{item.Amount}");
                            }
                            Console.WriteLine("Type out the name of the item to use.");
                            var itemName = Console.ReadLine();
                            if(Player.Inventory.Find(x=> x.Name.ToUpper() == itemName.ToUpper()) != null)
                            {
                                Player.UseItem(itemName);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Entry try again");
                                goto INVENTORY_ITEMS;
                            }

                            State = GameState.EnemyTurn;


                        }
                        else if (action.ToUpper() == "FLEE")
                        {
                            State = GameState.Menu;
                        }
                        else
                        {
                            goto ACTION_SELECT;
                        }
                        break;
                    case GameState.EnemyTurn:
                        if(Enemy.MeleeAtk >= Enemy.RangedAtk && Enemy.MeleeAtk >= Enemy.MagicAtk)
                        {
                            Enemy.Attack(Player, AttackType.Melee);
                        }
                        else if(Enemy.RangedAtk >= Enemy.MeleeAtk && Enemy.RangedAtk >= Enemy.MagicAtk)
                        {
                            Enemy.Attack(Player, AttackType.Ranged);
                        }
                        else if(Enemy.MagicAtk >= Enemy.MeleeAtk && Enemy.MagicAtk >= Enemy.RangedAtk)
                        {
                            Enemy.Attack(Player, AttackType.Magic);
                        }
                        if (Player.CurrentHealh <= 0)
                        {
                            State = GameState.Lose;
                        }
                        else
                        {
                            State = GameState.PlayerTurn;
                        }
                        break;
                    case GameState.Win:
                        Console.WriteLine($"{Player.Name} Wins the battle gains {Enemy.Exp} EXP");
                        Player.GainExp(Enemy.Exp);

                        if(Enemy.Inventory.Count > 0)
                        {
                            var randLoot = new Random().Next(0, Enemy.Inventory.Count - 1);
                            Player.GainItem(Enemy.Inventory[randLoot].Name);
                            Console.WriteLine($"{Player.Name} loots {Enemy.Inventory[randLoot].Name}");
                        }
                        State = GameState.Menu;
                        return;
                        break;
                    case GameState.Lose:
                        Console.WriteLine($"{Player.Name} Loses the battle");
                        State = GameState.Menu;
                        return;
                        break;
                    case GameState.Menu:
                        Console.WriteLine("Return to menu");
                        return;
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
