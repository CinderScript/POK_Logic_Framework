using UnityEngine;

using PawnOfKings.Logic.World;

namespace PawnOfKings.Logic.Actor {

    /// <summary>
    /// A gameplay unit used in Pawn of Kings.
    /// </summary>
    class Unit {

        /* * * Force Assignment on instance (readonly) so controller functions cannot 
         * access these fields without them being initialized and set first.  * * */
        private readonly UnitType type;
        private readonly Transform prefab;
        private readonly Movement movement;
        private readonly Attack attack;
        private readonly Condition condition;

        // provide encapsulation while retaining readonly.  (no auto readonly in c# 4)
        public UnitType Type
        {
            get {
                return type;
            }
        }
        public Transform Prefab
        {
            get {
                return prefab;
            }
        }
        public Movement Movement
        {
            get {
                return movement;
            }
        }
        public Attack Attack
        {
            get {
                return attack;
            }
        }
        public Condition Condition
        {
            get {
                return condition;
            }
        }

        /* * * Assigned by ArmyManager * * */
        public int Id { get; set; }
        public Transform Transform { get; set; }
        public Unit Target { get; set; }
        private Cell cell;
        /// <summary>
        /// Setting this Unit's Cell also sets Cell's Unit reference to this unit.
        /// </summary>
        public Cell Cell
        {
            get {
                return cell;
            }

            set {
                cell = value;
                value.Unit = this;
            }
        }

        /// <summary>
        /// Creates a new Unit and initializes the Type, Prefab, Condition, Movement, and Attack.
        /// These are readonly properties that cannot be changed when the Unit is instanced.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="prefab"></param>
        /// <param name="condition"></param>
        /// <param name="movement"></param>
        /// <param name="attack"></param>
        public Unit(UnitType type, Transform prefab, 
            Condition condition, Movement movement, Attack attack)
        {
            this.type = type;
            this.prefab = prefab;
            this.condition = condition;
            this.movement = movement;
            this.attack = attack;
        }
    }

    public struct Attack {

        public AttackType Type { get; private set; }
        public float Range { get; private set; }
        public float Damage { get; private set; }
        public float Cost { get; private set; }

        public Attack(AttackType type, float range, float damage, float cost) : this()
        {
            Type = type;
            Range = range;
            Damage = damage;
            Cost = cost;
        }
    }

    public struct Movement {

        public MoveType Type { get; private set; }
        public float Speed { get; private set; }
        public float Range { get; private set; }
        public float Cost { get; private set; }

        public Movement(MoveType type, float speed, float range, float cost) : this()
        {
            Type = type;
            Speed = speed;
            Range = range;
            Cost = cost;
        }
    }

    public struct Condition {

        public float MaxHealth { get; private set; }
        public float DamageReduction { get; private set; }

        public bool IsAlive { get; set; }
        public float Health { get; set; }

        public Condition(float maxHealth, float damageReduction) : this()
        {
            IsAlive = true;
            MaxHealth = maxHealth;
            Health = maxHealth;
            DamageReduction = damageReduction;
        }
    }

    public enum AttackType {
        Melee, Ranged
    }

    public enum MoveType {
        Ground, Air, Water
    }

    public enum UnitType {
        Not_Found, TestUnit, SpaceFighter
    }
}