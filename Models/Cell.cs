namespace MazeSolvingDemonstration.Models {
    public class Cell {
        public int x { get; set; }
        public int y { get; set; }
        public string cellClass { get; set; }
        public Cell parent { get; set; }
    }
}
