using System.Runtime.CompilerServices;

namespace MazeSolvingDemonstration.Models {
    public class PriorityQueue {
        List<Cell> Obj { get; set; }
        List<int> f {  get; set; }
        List<int> h { get; set; }
        List<int> g { get; set; }

        public PriorityQueue(List<Cell> Obj, List<int> f, List<int> h, List<int> g) {
            this.Obj = Obj;
            this.f = f;
            this.h = h;
            this.g = g;
        }

        public void Enqueue(Cell cell, int fcell, int hcell, int gcell) {
            int index = 0, indexend = 0;
            bool fequal = false, hequal = false, gequal = false;
            foreach (int i in f) {
                if (fcell == i) {
                    fequal = true;
                    index = f.IndexOf(i);
                    indexend = f.LastIndexOf(i);
                }
            }
            if (fequal) {
                for (int i = index; i <= indexend; i++) {
                    if (hcell == h[i]) {
                        hequal = true;
                        index = h.IndexOf(h[i], index);
                        indexend = h.LastIndexOf(h[i], indexend, indexend - index + 1);
                    }
                }
                for (int i = index; i <= indexend; i++) {
                    if (hcell > h[i]) {
                        index++;
                    }
                }
            } else {
                f.Insert(0, fcell);
                f.Sort();
                index = f.IndexOf(fcell);
                f.RemoveAt(index);
            }
            f.Insert(index, fcell);
            Obj.Insert(index, cell);
            h.Insert(index, hcell);
            g.Insert(index, gcell);
        }

        public (Cell, int) Dequeue() {
            Cell cell = Obj[0];
            int gval = g[0];
            Obj.RemoveAt(0);
            f.RemoveAt(0);
            g.RemoveAt(0);
            h.RemoveAt(0);
            return (cell, gval);
        }

        public bool Contains(Cell cell) { 
            foreach (Cell c in Obj) {
                if (cell == c) {
                    return true;
                }
            }
            return false;
        }

        public int Count() {
            return Obj.Count;
        }

    }
}
