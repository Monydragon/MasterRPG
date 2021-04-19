using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace MasterRPG
{
    public class Equipment : Item
    {
        public EquipmentSlot Slot { get; set; }
        public int MeleeAtk { get; set; }
        public int RangedAtk { get; set; }
        public int MagicAtk { get; set; }
        public int MeleeDef { get; set; }
        public int RangedDef { get; set; }
        public int MagicDef { get; set; }

        public Equipment(string name, string descripton, string effect, int value, EquipmentSlot slot, int meleeAtk, int rangedAtk, int magicAtk, int meleeDef, int rangedDef, int magicDef)
        {
            Name = name;
            Description = descripton;
            Effect = effect;
            Value = value;
            Slot = slot;
            MeleeAtk = meleeAtk;
            RangedAtk = rangedAtk;
            MagicAtk = magicAtk;
            MeleeDef = meleeDef;
            RangedDef = rangedDef;
            MagicDef = magicDef;
        }

        public void Equip(Actor actor)
        {
            Console.WriteLine($"{actor.Name} Equips {Name}.");
            actor.Equipment[(int)Slot] = (Equipment)this;
            actor.MeleeAtk += MeleeAtk;
            actor.RangedAtk += RangedAtk;
            actor.MagicAtk += MagicAtk;
            actor.MeleeDef += MeleeDef;
            actor.RangedDef += RangedDef;
            actor.MagicDef += MagicDef;
        }

        public void UnEquip(Actor actor)
        {
            Console.WriteLine($"{actor.Name} UnEquips {Name}.");
            actor.Equipment[(int)Slot] = null;
            actor.MeleeAtk -= MeleeAtk;
            actor.RangedAtk -= RangedAtk;
            actor.MagicAtk -= MagicAtk;
            actor.MeleeDef -= MeleeDef;
            actor.RangedDef -= RangedDef;
            actor.MagicDef -= MagicDef;
        }

        public override void Use(Actor actor)
        {
            Console.WriteLine($"{actor.Name} Uses {Name}.");

            string[] words = Effect.Split(' ');

            foreach (var effect in words)
            {
                if (effect.ToLower().Contains("equip"))
                {
                    if (actor.Equipment.Contains(this))
                    {
                        UnEquip(actor);
                    }
                    else
                    {
                        Equip(actor);
                    }

                }
            }
        }
    }
}
