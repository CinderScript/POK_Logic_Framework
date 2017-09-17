using System.Collections.Generic;

using UnityEngine;

using PawnOfKings.Unity.Values;


namespace PawnOfKings.Logic.Actor {

    /// <summary>
    /// The unit loader provides a way to instance new Units using unit settings/props 
    /// established in the unity editor through the UnitProperties and the UnitBank.
    /// 
    /// DEPENDENCY:  ArmyManager has a dependancy on UnitLoader.
    /// UnitLoader has a dependancy on UnitProperties and UnitBank.
    /// </summary>
    class UnitLoader {

        private readonly List<UnitProperties> propertyList;

        public UnitLoader(UnitBank bank)
        {
            propertyList = bank.Units;
        }

        /// <summary>
        /// Returns a unit derived from the values set in the Unity editor.  Values are found 
        /// in the UnitProperties component.  If this unit type is not found in the UnitBank 
        /// set inside of the unity editor, null is returned (but consider using IsInUnitBank(type) 
        /// when needed).
        /// </summary>
        /// <param name="type">UnitType to check for in the UnitBank</param>
        /// <returns>new Unit with values set by matching unit in the UnitBank, or null</returns>
        public Unit LoadUnit(UnitType type)
        {
            bool isInUnitBank;
            UnitProperties p = getUnitMatch( type, out isInUnitBank );
            Unit unit = null;

            if ( isInUnitBank )
            {
                Transform prefab = p.Prefab;
                Condition cond = new Condition( p.MaxHealth, p.Armour );
                Movement move = new Movement( p.MoveType, p.MoveSpeed, p.MoveRange, p.MoveCost );
                Attack attack = new Attack( p.AttackType, p.AttackRange, p.AttackDamage, p.AttackCost );

                unit = new Unit( type, prefab, cond,  move, attack );
            }

            return unit;
        }

        /// <summary>
        /// Searches the UnitBank for a Unit containing matching UnitType.
        /// </summary>
        /// <param name="type">UnitType to check for in the UnitBank</param>
        /// <returns>True if a Unit's UnitType is found to match in the UnitBank</returns>
        public bool IsInUnitBank(UnitType type)
        {
            bool isInUnitBank = false;

            for ( int i = 0; i < propertyList.Count; i++ )
            {
                if ( type == propertyList[i].UnitType )
                {
                    isInUnitBank = true;
                    break;
                }
            }

            return isInUnitBank;
        }


        private UnitProperties getUnitMatch(UnitType type, out bool isInUnitBank)
        {
            UnitProperties props = null;
            isInUnitBank = false;

            for ( int i = 0; i < propertyList.Count; i++ )
            {
                if ( type == propertyList[i].UnitType )
                {
                    props = propertyList[i];
                    isInUnitBank = true;
                    break;
                }
            }

            return props;
        }
    }
}
