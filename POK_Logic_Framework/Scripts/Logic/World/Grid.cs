using UnityEngine;

using PawnOfKings.Logic.Actor;

using PawnOfKings.Unity.Values;

namespace PawnOfKings.Logic.World {

    /// <summary>
    /// The Grid is the gameplay grid used during the BattleState.  It defines each cell a unit 
    /// may occupy or travel to.
    /// 
    /// DEPENDENCY: GameStateManager.LoadBattleState, ArmyManager, UnitController, IBehavior.
    /// </summary>
    class Grid {

        public Cell[][] Cells { get; set; }

        public int CellSize { get; private set; }
        public int TerrainMargins { get; private set; }
        public float CellHeightAboveTerrain { get; private set; }

        private Vector2 playerStart { get; set; }
        private Vector2 aiStart { get; set; }

        public Cell PlayerStart
        {
            get {
                float x = playerStart.x;
                float y = playerStart.y;

                return Cells[Mathf.FloorToInt(x)][Mathf.FloorToInt( y )];
            }
        }
        public Cell AIStart
        {
            get {
                float x = aiStart.x;
                float y = aiStart.y;

                return Cells[Mathf.FloorToInt( x )][Mathf.FloorToInt( y )];
            }
        }

        /// <summary>
        /// Creates a new Grid object.  This Grid's Cells are populated and set to enabled or disabled.
        /// </summary>
        /// <param name="gridProperties">Properties script component set in unity editor</param>
        public Grid(GridProperties gridProperties) : this(gridProperties.GridTerrain,
            gridProperties.CellIcon, gridProperties.MaxGridHeight, gridProperties.GridCellSize,
            gridProperties.TerrainMargins, gridProperties.CellHeightAboveTerrain,
            gridProperties.PlayerStart, gridProperties.AI) { }

        /// <summary>
        /// Creates a new Grid object.  This Grid's Cells are populated and set to enabled or disabled.
        /// </summary>
        /// <param name="terrain">Terrain this Grid will be drawn over the top of</param>
        /// <param name="cellIcon">Icon that will be positioned over the cell's position</param>
        /// <param name="heightCeiling">Maximum height a cell will be enabled at</param>
        public Grid(Terrain terrain, Transform cellIcon, Transform heightCeiling, int cellSize, 
                    int terrainMargins, float cellHeight, Vector2 playerStart, Vector2 aiStart)
        {
            this.playerStart = playerStart;
            this.aiStart = aiStart;

            CellSize = cellSize;
            TerrainMargins = terrainMargins;
            CellHeightAboveTerrain = cellHeight;


            /* * * MAKE EMPTY 2D GRID * * */
            float width = terrain.terrainData.size.x;
            int totalRows = calculateNumberOfGridRows(width, TerrainMargins, CellSize);

            // create grid with references to correct number of rows (arrays).
            Cells = new Cell[totalRows][];
            for ( int i = 0; i < totalRows; i++ )
            {
                Cells[i] = new Cell[totalRows];
            }

            /* * * POPULATE GRID AND SET CELLS * * */

            // Spawn GridCell at each point in grid (x = row, z = column)
            for ( int row = 0; row < Cells.Length; row++ )
            {
                for ( int col = 0; col < Cells[row].Length; col++ )
                {
                    // create position for this cell
                    float x = row * CellSize + TerrainMargins;
                    float z = col * CellSize + TerrainMargins;
                    float height = terrain.SampleHeight(new Vector3(x, 0, z));

                    // flip y and z to account for world canvas orientation
                    Vector3 pos = new Vector3(x, height + CellHeightAboveTerrain, z);

                    // instance and enable each cell that is below the maximum grid height.
                    if ( height < heightCeiling.position.y )
                    {
                        Cells[row][col] = new Cell(true, row, col, pos, cellIcon, null);
                    }
                    else
                    {
                        Cells[row][col] = new Cell(false, row, col, pos, null, null);
                    }
                }
            }
        }

        /// <summary>
        /// Spawns an icon for each cell in the grid that is enabled and stores it in the Cell's icon property.
        /// </summary>
        /// <param name="cellIcons"></param>
        public void InstantiateCellIcons()
        {
            GameObject parent = new GameObject("Grid");

            for ( int row = 0; row < Cells.Length; row++ )
            {
                for ( int col = 0; col < Cells[row].Length; col++ )
                {
                    if ( Cells[row][col].Enabled )
                    {
                        Transform icon = (Transform)GameObject.Instantiate(Cells[row][col].CellIcon,
                                                                           Cells[row][col].Position,
                                                                           Cells[row][col].CellIcon.rotation,
                                                                           parent.transform);
                        icon.name = "Cell " + row + ", " + col;

                        Cells[row][col].CellIcon = icon;
                    }
                }
            }
        }

        /// <summary>
        /// Calculate number of rows needed to fill the terrain up to the empty margins.  Used in 
        /// constructor during grid creation.
        /// </summary>
        /// <returns>number of rows needed to fill terrain</returns>
        private int calculateNumberOfGridRows(float terrainSize, float marginSize, float cellSize)
        {
            // length of grid area / size
            float rows = (terrainSize - (marginSize * 2)) / cellSize;

            return Mathf.FloorToInt(rows);
        }
    }

    /// <summary>
    /// Contains all values needed to define a single Grid's cell.
    /// </summary>
    struct Cell {

        public bool Enabled { get; private set; }

        public int X { get; private set; }
        public int Y { get; private set; }
        public Vector2 Coordinate { get { return new Vector2( X, Y ); } }

        public Vector3 Position { get; private set; }

        public Transform CellIcon { get; set; }
        public Unit Unit { get; set; }

        public Cell (bool isEnabled, int x, int y, Vector3 position, Transform cellIcon, Unit unit)
        {
            Enabled = isEnabled;
            X = x;
            Y = y;
            Position = position;
            CellIcon = cellIcon;
            Unit = unit;
        }
    }
}