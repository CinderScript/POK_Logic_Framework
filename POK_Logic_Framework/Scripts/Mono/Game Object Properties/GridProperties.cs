using UnityEngine;

namespace PawnOfKings.Unity.Values {

    /// <summary>
    /// Contains all the properties needed to define the world's play grid during the BattleState 
    /// stage of the game.  This script is added to a GameObject in a scene.  The BattleStateDriver needs 
    /// a reference to this object so these values can be passed to the game logic.
    /// </summary>
    class GridProperties : MonoBehaviour {

        [Header( "Parent Objects" )]
        public Terrain GridTerrain;

        [Header( "Grid Object References" )]
        public Transform CellIcon;
        public Transform MaxGridHeight = null;

        [Header( "Grid Properties" )]
        public int GridCellSize = 20;
        public int TerrainMargins = 100;
        public float CellHeightAboveTerrain = 10;

        [Header( "Player Start Position" )]
        public Vector2 PlayerStart;

        [Header( "AI Start Position" )]
        public Vector2 AI;
    }
}