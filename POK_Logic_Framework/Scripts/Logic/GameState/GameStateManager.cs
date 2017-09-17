using PawnOfKings.Logic.Actor;
using PawnOfKings.Logic.World;

using PawnOfKings.Unity.Values;

using Unity.Custom.EventSystem;

namespace PawnOfKings.Logic.GameState {

    /// <summary>
    /// GameStateManager is based on a state machine pattern that manages the current state of the game.  
    /// GameStateManager is also a singleton - therefore it's instance will persist even when the 
    /// GameObjects holding a reference to this GameStateManager are destroyed and reloaded.  The 
    /// GameStateManager will not loose track of the current GameState or player data when loading levels.
    /// 
    /// Each IGameState (interface) inheriting class contains a method to be run in an Update and 
    /// FixedUpdate loop, and the manager runs the current states inherited methods.
    /// 
    /// DEPENDENCY: The GameStateManager will only be seen by the entry object into the game (Driver classes) 
    /// and no other classes have a GameStateManager dependancy.
    /// </summary>
    class GameStateManager {

        private readonly EventManager events = new EventManager();
        private IGameState currentState = null;

        // Singleton pattern
        private static GameStateManager instance = null;
        private GameStateManager() { }
        public static GameStateManager Instance()
        {
            if ( instance == null )
            {
                instance = new GameStateManager();
            }

            return instance;
        }

        /// <summary>
        /// Calls the update method for the current state in the state machine.
        /// UpdateGame() is called in Unity's Update method via a Driver class (called 
        /// once every frame update).
        /// </summary>
        public void UpdateGame()
        {
            currentState.Update();
        }
        /// <summary>
        /// Calls the FixedUpdate method for the current state in the state machine.
        /// FixedUpdateGame() is called in Unity's FixedUpdate method via a Driver 
        /// class (called exactly 60 times a second).
        /// </summary>
        public void FixedUpdateGame()
        {
            //currentState.FixedUpdate();
        }

        /// <summary>
        /// Instances a new BattleState and sets the instance as the current state.
        /// </summary>
        /// <param name="gridProps">Grid settings to be used for the current scene's world grid.</param>
        /// <param name="unitBank">Contains list of all units available for this battle.</param>
        public void LoadBattleState(GridProperties gridProps, UnitBank unitBank)
        {
            Grid grid = new Grid( gridProps );
            grid.InstantiateCellIcons();

            ArmyManager<AIController> aiArmy = new ArmyManager<AIController>(unitBank, grid, "AI: ");
            ArmyManager<PlayerController> playerArmy = 
                                     new ArmyManager<PlayerController>( unitBank, grid, "Player: " );

            BattleState battleState = new BattleState(aiArmy, playerArmy, grid);
            currentState = battleState;
        }
    }
}
