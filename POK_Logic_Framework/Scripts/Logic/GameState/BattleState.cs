using PawnOfKings.Logic.Actor;
using PawnOfKings.Logic.World;

namespace PawnOfKings.Logic.GameState {

    /// <summary>
    /// Inherits IGameState.  The state that controls the gameplay during a battle.
    /// 
    /// The BattleState contains two ArmyManagers, one for the AI's team, and one for the Player's team.
    /// This state keeps track of the current turn and gives control the the correct Army's turn update.
    /// 
    /// DEPENDENCY:  GameStateManager has a dependancy for BattleState.
    /// BattleState has a dependancy for ArmyManager, AIController, and PlayerController.
    /// 
    /// Temp dependncy on Grid for character spawning.  Will be handled by spawner or event system 
    /// in future.
    /// </summary>
    class BattleState : IGameState {

        private readonly ArmyManager<AIController> aiArmy;
        private readonly ArmyManager<PlayerController> playerArmy;
        private readonly Grid grid;

        private const int PLAYER = 0;
        private const int AI = 1;

        private int turn = PLAYER;
        private bool turnStarted = false;

        private delegate void armyStartTurn();

        public BattleState(ArmyManager<AIController> aiArmy, ArmyManager<PlayerController> playerArmy, Grid grid)
        {
            this.aiArmy = aiArmy;
            this.playerArmy = playerArmy;
            this.grid = grid;

            addTestUnits(); // Temporary

            aiArmy.InstanceUnits();
            playerArmy.InstanceUnits();

            playerArmy.StartTurn();
        }

        /// <summary>
        /// Runs the correct Army's update loop and keeps track of turns.
        /// </summary>
        public void Update()
        {
            if ( turn == PLAYER )
            {
                bool endTurn = playerArmy.Update();  // update army, check for turn end

                // if latest update player's turn is ended, settup AI for new turn
                if ( endTurn )
                {
                    turn = AI; // next update AI will update
                    aiArmy.StartTurn(); // controller[0] = active controller
                }
            }
            else // turn == AI
            {
                bool endTurn = aiArmy.Update();

                if ( endTurn )
                {
                    turn = PLAYER;
                    playerArmy.StartTurn();
                }
            }
        }

        public void FixedUpdate()
        {
            // NOT IMPLEMENTED
        }

        /// <summary>
        /// Possible multi army turn updator.  Not in current Use.
        /// </summary>
        /// <param name="nextArmy"></param>
        /// <param name="nextArmySetup"></param>
        private void performTurn(int nextArmy, armyStartTurn nextArmySetup)
        {
            bool endTurn = aiArmy.Update();

            if ( endTurn )
            {
                turn = nextArmy;
                nextArmySetup();
            }
        }

        /// <summary>
        /// Temporary method for adding Units for testing.  In future, Units will be 
        /// added based on GUI selection.
        /// </summary>
        private void addTestUnits()
        {
            aiArmy.AddUnit( UnitType.TestUnit, grid.AIStart );
            playerArmy.AddUnit( UnitType.TestUnit, grid.PlayerStart );
        }
    }
}