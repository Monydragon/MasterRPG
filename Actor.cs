using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace MasterRPG
{
    public class Actor
    {
        private int currentHealh = 100;
        private int maxHealth = 100;

        public string Name { get; set; }
        public string Descripton { get; set; }
        public int Age { get; set; }
        public int Initative { get; set; }
        public bool isAlive { get; set; } = true;
        public int CurrentHealh
        {
            get
            {
                if (currentHealh >= maxHealth)
                {
                    currentHealh = maxHealth;
                    isAlive = true;
                    return maxHealth;
                }
                else if (currentHealh <= 0)
                {
                    currentHealh = 0;
                    isAlive = false;
                    Die();
                    return 0;
                }
                else
                {
                    isAlive = true;
                    return currentHealh;
                }
            }
            set
            {
                if(value >= maxHealth)
                {
                    currentHealh = maxHealth;
                }
                else if(value <= 0)
                {
                    currentHealh = 0;
                }
                else
                {
                    currentHealh = value;
                }
            }
        }
        public int MaxHealth 
        { 
            get => maxHealth;
            set => maxHealth = value; 
        }

        public int MeleeAtk { get; set; } = 1;
        public int RangedAtk { get; set; } = 1;
        public int MagicAtk { get; set; } = 1;
        public int MeleeDef { get; set; } = 1;
        public int RangedDef { get; set; } = 1;
        public int MagicDef { get; set; } = 1;
        public int Exp { get; set; } = 0;
        public int Level { get; set; } = 1;
        public List<Item> Inventory { get; set; } = new List<Item>();
        public Equipment[] Equipment { get; set; } = new Equipment[Enum.GetNames(typeof(EquipmentSlot)).Length];

        public Actor()
        {

        }

        public void Die()
        {
            Console.WriteLine($"{Name} has died");
        }

        public void GainExp(int amt)
        {
            Exp += amt;

            while (Exp >= ExpChart.ExpLevel[Level + 1])
            {
                Level++;
                maxHealth += Level * 10;
                currentHealh = maxHealth;
                Console.WriteLine($"{Name} levels up to level: {Level}");
            }
        }

        public void UseItem(string name)
        {
            var item = Inventory.Find(x => x.Name.ToUpper() == name.ToUpper());
            if (item == null)
            {
                foreach (var equip in Equipment)
                {
                    if(equip?.Name.ToUpper() == name.ToUpper())
                    {
                        item = equip;
                    }
                }
            }
            if(item != null)
            {
                item.Use(this);
                if (item.Amount <= 0)
                {
                    Inventory.Remove(item);
                }
            }
            
        }

        public void GainItem(string name)
        {
            Console.WriteLine($"{Name} obtained {name}.");
            var item = Inventory.Find(x => x.Name.ToUpper() == name.ToUpper());
            if(item != null)
            {
                item.Amount += 1;
            }
            else
            {
                item = ItemDatabase.GetItem(name);
                Inventory.Add(item);
            }
        }

        public void Attack(Actor targetActor, AttackType type)
        {
            var rand = new Random();
            switch (type)
            {
                case AttackType.Melee:
                    var meleeatk = rand.Next(0, (MeleeAtk * 5));
                    var meleecalc = meleeatk - targetActor.MeleeDef;
                    Console.WriteLine($"{Name} attacks using {type} for {meleecalc} damage. ");
                    if(meleecalc <= 0)
                    {
                        Console.WriteLine("Melee Attack Misses");
                    }
                    else
                    {
                        targetActor.CurrentHealh -= meleecalc;
                    }
                    break;
                case AttackType.Ranged:
                    var rangedatk = rand.Next(0, (MeleeAtk * 5));
                    var rangedcalc = rangedatk - targetActor.MeleeDef;
                    Console.WriteLine($"{Name} attacks using {type} for {rangedcalc} damage. ");
                    if (rangedcalc <= 0)
                    {
                        Console.WriteLine("Ranged Attack Misses");
                    }
                    else
                    {
                        targetActor.CurrentHealh -= rangedcalc;
                    }
                    break;
                case AttackType.Magic:
                    var magicatk = rand.Next(0, (MeleeAtk * 5));
                    var magiccalc = magicatk - targetActor.MeleeDef;
                    Console.WriteLine($"{Name} attacks using {type} for {magiccalc} damage. ");
                    if (magicatk <= 0)
                    {
                        Console.WriteLine("Magic Attack Misses");
                    }
                    else
                    {
                        targetActor.CurrentHealh -= magiccalc;
                    }
                    break;
                default:
                    break;
            }

            Console.WriteLine($"{Name} has {currentHealh} HP. {targetActor.Name} has {targetActor.CurrentHealh} HP.");

        }
    }
}
