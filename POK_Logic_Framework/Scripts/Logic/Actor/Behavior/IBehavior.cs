/* 
 * Author: Gregory Maynard <CinderScript@gmail.com>
 * Copyright (C) - All Rights Reserved
 */

using UnityEngine;

using PawnOfKings.Logic.World;

namespace PawnOfKings.Logic.Actor {

    /// <summary>
    /// Interface for UnitBehavior classes.  Behavior classes are updated by a UnitController 
    /// and applied to a Unit each time Perform is called.  The out behavior is the next 
    /// behavior that will be updated on the Unit.  Set the out behavior to itself (this) 
    /// if the behavior is not done running.
    /// </summary>
    interface IUnitBehavior {

        /// <summary>
        /// Performs the action controlled by this Behavior class.  Null cannot be used
        /// to signify a return of True (behavior is finished), because the Controller
        /// that runs this behavior assignes the first behavior (behaviorChooser) when
        /// the out behavior is null (and the behavior 'chain' will never finish).
        /// </summary>
        /// <param name="behavior">The UnitBehavior to be performed on the next Perform update.</param>
        /// <returns>True when behavior is finished and is the last behavior.</returns>
        bool Perform(out IUnitBehavior behavior);
    }

    /// <summary>
    /// Unit bahavior that moves a unit to a new grid cell.
    /// </summary>
    class Move : IUnitBehavior {

        private readonly Cell to;
        private readonly Transform unit;

        private int count = 1;

        public Move(Cell destination, Transform unit)
        {
            this.to = destination;
            this.unit = unit;
        }

        /// <summary>
        /// Moves unit appropriate distance closer to target cell (based on unit's
        /// speed setting).
        /// </summary>
        /// <param name="behavior"></param>
        /// <returns></returns>
        public bool Perform(out IUnitBehavior behavior)
        {
            bool finished = false;
            behavior = this;

            // TEST THE BEHAVIOR OVER THREE FRAMES THEN END.
            count++;

            if ( count > 3 )
            {
                finished = true;
                behavior = null;
            }
            else
            {
                behavior = this;  // if not finished, return this behavior so it keeps working
            }

            return finished;
        }
    }
}
