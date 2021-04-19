using System;
using System.Collections.Generic;
using System.Text;

namespace MasterRPG
{
    public enum GameState
    {
         Menu,
         PlayerTurn,
         EnemyTurn,
         Win,
         Lose
    }

    public enum AttackType
    {
        Melee,
        Ranged,
        Magic
    }

    public enum EquipmentSlot
    {
        Head,
        Torso,
        Legs,
        Weapon,
        Shield
    }
}
