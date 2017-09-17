
using PawnOfKings.Logic.World;
using UnityEngine;

namespace PawnOfKings.Logic.Actor {

    /// <summary>
    /// Implements UnitController and used by ArmyManager as a possible Generic type.
    /// This is a controller for Player units and its behaviors will be choosen through 
    /// the GUI by the player.
    /// </summary>
    class PlayerController : UnitController {

        /// <summary>
        /// Updates this controller's unit's behavior.  Returns true when 
        /// this controller is done running all behaviors for this unit's 
        /// turn and is ready to decide the next move. 
        /// 
        /// Called by ArmyManager when updating units.  
        /// </summary>
        /// <returns>True when finished with all behaviors this turn, else false.</returns>
        public override bool Update()
        {
            return updateUnit( getAction );
        }
        public void FixedUpdate() { }

        protected IUnitBehavior getAction(Unit unit, Grid grid)
        {
            PlayerBehaviorChooser action = new PlayerBehaviorChooser(unit, grid);

            return action;
        }
    }

    /// <summary>
    /// Behavior that listens for GUI input and runs the assosiated IBehavior.
    /// </summary>
    class PlayerBehaviorChooser : IUnitBehavior {

        private readonly Unit unit;
        private readonly Grid grid;

        public PlayerBehaviorChooser(Unit unit, Grid grid)
        {
            this.unit = unit;
            this.grid = grid;

            // get number of enemy units in range
            // get closest enemy unit
        }
        /// <summary>
        /// Performs this behavior.  Called by the UnitController's Update.
        /// </summary>
        /// <param name="nextAction"></param>
        /// <returns></returns>
        public bool Perform(out IUnitBehavior nextAction)
        {
            // if no units in range
            Cell cell = new Cell( true, 16, 6, new Vector3(), null, null );

            nextAction = new Move( cell, unit.Transform );

            return false;
        }
    }
}