using System;
using System.Collections.Generic;
using System.Text;

namespace MasterRPG
{
    public class Stat
    {
        public string Name { get; set; }
        public float CurVal { get; set; }
        public float MaxVal { get; set; }

        public Stat(string name, float curval, float maxval)
        {
            Name = name;
            CurVal = curval;
            MaxVal = maxval;
        }

        public void IncreaseVal(float amt)
        {
            CurVal += amt;

            if(CurVal >-MaxVal)
            {
                CurVal = MaxVal;
            }
        }

        public void DecreaseVal(float amt)
        {
            CurVal -= amt;

            if(CurVal <= 0)
            {
                CurVal = 0;
            }
        }

    }
}
