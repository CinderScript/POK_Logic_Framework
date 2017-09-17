
using PawnOfKings.Logic.World;
using UnityEngine;

namespace PawnOfKings.Logic.Actor {

    /// <summary>
    /// Implements UnitController and used by ArmyManager as a possible Generic type.
    /// This is a controller for AI units and its behaviors will not be choosen 
    /// through the GUI, but through AI logic.
    /// </summary>
    class AIController : UnitController {

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
            return updateUnit( aiBehaviorChooser );
        }
        public void FixedUpdate() { }

        protected IUnitBehavior aiBehaviorChooser(Unit unit, Grid grid)
        {
            AIBehaviorChooser action = new AIBehaviorChooser(unit, grid);

            return action;
        }

    }

    /// <summary>
    /// Behavior that calculates the AI's behavior for this unit's turn.
    /// </summary>
    class AIBehaviorChooser : IUnitBehavior {

        private readonly Unit unit;
        private readonly Grid grid;

        public AIBehaviorChooser(Unit unit, Grid grid)
        {
            this.unit = unit;
            this.grid = grid;

            // get number of enemy units in range
            // get closest enemy unit
        }
        public bool Perform(out IUnitBehavior nextAction)
        {
            // if no units in range
            Cell cell = new Cell( true, 16, 6, new UnityEngine.Vector3(), null, null );
            nextAction = new Move( cell, unit.Transform );

            return false ;
        }
    }
}