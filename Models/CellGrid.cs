namespace MazeSolvingDemonstration.Models {
    public class CellGrid {
        public string xs { get; set; }
        public string ys { get; set; }
        public string classes { get; set; }
        public string method { get; set; }
        public int stepstoResult { get; set; } = 0;
        public int vel { get; set; } = 0;

        public static Cell[] CreateGrid(CellGrid grid) {

            string[] x = new string[2294];
            x = grid.xs.Split(',');

            string[] y = new string[2294];
            y = grid.ys.Split(',');

            string[] classes = new string[2294];
            classes = grid.classes.Split(',');

            Cell[] cellsGrid = new Cell[2294];
            for(int v = 0; v < 2294; v++) {
                Cell cell = new Cell {
                    x = int.Parse(x[v]),
                    y = int.Parse(y[v]),
                    cellClass = classes[v]
                };
                cellsGrid[v] = cell;
            }

            return cellsGrid;
        } 
    }

}
