using System;
using System.Collections.Generic;
using System.Text;

namespace MasterRPG
{
   public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Effect { get; set; }
        public int Amount { get; set; } = 1;    
        public int Value { get; set; }

        public Item()
        {

        }

        public Item(string name = "", string description = "", string effect = "", int value = 0)
        {
            Name = name;
            Description = description;
            Effect = effect;
            Value = value;
        }

        public virtual void Use(Actor actor)
        {
            Console.WriteLine($"{actor.Name} Uses {Name}.");
            string[] words = Effect.Split(' ');

            foreach (var effect in words)
            {
                if (effect.ToLower().Contains("heal"))
                {
                    var healAmt = int.Parse(effect.Split(":")[1]);
                    actor.CurrentHealh += healAmt;
                }

                if (effect.ToLower().Contains("consumable"))
                {
                    Amount -= 1;
                }
            }
        }
    }
}
