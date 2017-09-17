using UnityEngine;

using PawnOfKings.Logic.Actor;

namespace PawnOfKings.Unity.Values {

    /// <summary>
    /// Contains all the properties needed to define a Unit in the Unity Editor.  Place this script 
    /// on a prefab and set the values.  The created prefab can then be added to the UnitBank MonoBehaviour.
    /// </summary>
    class UnitProperties : MonoBehaviour {

        [Header( "Game Object Resource" )]
        public Transform Prefab = null;

        [Header( "Unit Values" )]
        public UnitType UnitType = UnitType.Not_Found;

        [Header( "Condition" )]
        public float MaxHealth = 1;
        public float Armour = 1;

        [Header( "Attack" )]
        public AttackType AttackType = AttackType.Melee;
        public float AttackRange = 1;
        public float AttackDamage = 1;
        public float AttackCost = 1;

        [Header( "Movement" )]
        public MoveType MoveType = MoveType.Ground;
        public float MoveSpeed = 1;
        public float MoveRange = 1;
        public float MoveCost = 1;
    }
}