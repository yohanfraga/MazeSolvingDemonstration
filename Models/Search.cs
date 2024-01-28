using MazeSolvingDemonstration.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MazeSolvingDemonstration.Models {
    public class Search {
        public static CellGrid CreatePath(Cell EndCell, Cell[] cellsGrid) {
            CellGrid path = new CellGrid() {
                stepstoResult = 0,
                xs = "",
                classes = ""
            };
            List<string> grid = new List<string>();
            List<string> changes = new List<string>();
            Stack<Cell> cells = new Stack<Cell>();
            Cell currentCell = EndCell;
            while (currentCell != null) {
                path.stepstoResult++;
                cells.Push(currentCell);
                currentCell = currentCell.parent;
            }
            for (int i = 0; i < path.stepstoResult; i++) {
                currentCell = cells.Pop();
                int j = 0;
                foreach (Cell c in cellsGrid) {
                    if (currentCell == c) {
                        if (c.cellClass != "fim" && c.cellClass != "inicio") {
                            c.cellClass = "path";
                            grid.Add(c.cellClass);
                            changes.Add(j.ToString());
                        }
                    }
                    j++;
                }
                path.classes = path.classes + string.Join(",", grid) + ",";
                path.xs = path.xs + string.Join(",", changes) + ",";
                grid.Clear();
                changes.Clear();
            }
            path.classes.Remove(path.classes.Length);
            path.xs.Remove(path.xs.Length);
            return path;
        }

        public static CellGrid BFS(Cell[] cellsGrid) {
            CellGrid resultado = new CellGrid();
            int IdStart = 0;
            foreach (Cell c in cellsGrid) {
                if (c.cellClass == "inicio") {
                    break;
                }
                IdStart++;
            }
            int NumSteps = 1, index = 0;
            Cell CurrentCell = new Cell();
            List<string> grid = new List<string>();
            List<string> changes = new List<string>();
            foreach (Cell c in cellsGrid) {
                grid.Add(c.cellClass);
            }
            resultado.ys = string.Join(",", grid);
            grid.Clear();

            Queue<int> cellsIndex = new Queue<int>();
            cellsIndex.Enqueue(IdStart);

            Queue<Cell> cellsNeightboors = new Queue<Cell>(); //cria fila
            cellsNeightboors.Enqueue(cellsGrid[IdStart]);

            while (cellsNeightboors.Count > 0) {
                CurrentCell = cellsNeightboors.Dequeue(); // vai para o próximo vizinho
                index = cellsIndex.Dequeue();

                if (CurrentCell.cellClass != "fim") { //verifica se é o final
                    NumSteps++;
                } else {
                    break;
                }

                if (CurrentCell.cellClass != "inicio") {
                    cellsGrid[index].cellClass = "visitada";
                    grid.Add(cellsGrid[index].cellClass);
                    changes.Add(index.ToString());
                }

                if (CurrentCell.x != 0) { //verifica se é vizinho da celula atual
                    if (cellsGrid[index - 74].cellClass != "parede" && cellsGrid[index - 74].cellClass != "inicio" && cellsGrid[index - 74].cellClass != "visitada") {
                        if (cellsNeightboors.Contains(cellsGrid[index - 74])) {//verifica se ja está na fila
                        } else {
                            cellsGrid[index - 74].parent = CurrentCell;
                            cellsNeightboors.Enqueue(cellsGrid[index - 74]); //adiciona os vizinhos da celula atual na fila
                            cellsIndex.Enqueue(index - 74);
                        }
                    }
                }
                if (CurrentCell.y != 73) { //verifica se é vizinho da celula atual
                    if (cellsGrid[index + 1].cellClass != "parede" && cellsGrid[index + 1].cellClass != "inicio" && cellsGrid[index + 1].cellClass != "visitada") {
                        if (cellsNeightboors.Contains(cellsGrid[index + 1])) {//verifica se ja está na fila
                        } else {
                            cellsGrid[index + 1].parent = CurrentCell;
                            cellsNeightboors.Enqueue(cellsGrid[index + 1]); //adiciona os vizinhos da celula atual na fila
                            cellsIndex.Enqueue(index + 1);
                        }
                    }
                }
                if (CurrentCell.x != 30) { //verifica se é vizinho da celula atual
                    if (cellsGrid[index + 74].cellClass != "parede" && cellsGrid[index + 74].cellClass != "inicio" && cellsGrid[index + 74].cellClass != "visitada") {
                        if (cellsNeightboors.Contains(cellsGrid[index + 74])) {//verifica se ja está na fila
                        } else {
                            cellsGrid[index + 74].parent = CurrentCell;
                            cellsNeightboors.Enqueue(cellsGrid[index + 74]); //adiciona os vizinhos da celula atual na fila
                            cellsIndex.Enqueue(index + 74);
                        }
                    }
                }
                if (CurrentCell.y != 0) { //verifica se é vizinho da celula atual
                    if (cellsGrid[index - 1].cellClass != "parede" && cellsGrid[index - 1].cellClass != "inicio" && cellsGrid[index - 1].cellClass != "visitada") {
                        if (cellsNeightboors.Contains(cellsGrid[index - 1])) {//verifica se ja está na fila
                        } else {
                            cellsGrid[index - 1].parent = CurrentCell;
                            cellsNeightboors.Enqueue(cellsGrid[index - 1]); //adiciona os vizinhos da celula atual na fila
                            cellsIndex.Enqueue(index - 1);
                        }
                    }
                }
            }

            CellGrid solvePath = CreatePath(CurrentCell, cellsGrid);
            resultado.classes = string.Join(",", grid) + solvePath.classes;
            resultado.xs = string.Join(",", changes) + solvePath.xs;
            resultado.stepstoResult = NumSteps + solvePath.stepstoResult;
            return resultado;
        }

        public static CellGrid DFS(Cell[] cellsGrid) {
            CellGrid resultado = new CellGrid();
            int IdStart = 0;
            foreach (Cell c in cellsGrid) {
                if (c.cellClass == "inicio") {
                    break;
                }
                IdStart++;
            }
            int NumSteps = 1, index = -1;
            Cell CurrentCell = new Cell();
            List<string> grid = new List<string>();
            List<string> changes = new List<string>();
            foreach (Cell c in cellsGrid) {
                grid.Add(c.cellClass);
            }
            resultado.ys = string.Join(",", grid);
            grid.Clear();

            List<int> neighborsIndex = new List<int>();

            Stack<int> cellsIndex = new Stack<int>();
            cellsIndex.Push(IdStart);
            int indexsave;

            Stack<Cell> cellsPath = new Stack<Cell>(); //cria fila
            cellsPath.Push(cellsGrid[IdStart]);

            while (cellsPath.Count > 0) {
                CurrentCell = cellsPath.Peek();
                indexsave = index;
                index = cellsIndex.Peek();

                if (CurrentCell.cellClass != "fim") { //verifica se é o final
                    NumSteps++;
                } else {
                    if (cellsGrid[indexsave].cellClass != "inicio") {
                        NumSteps++;
                        cellsGrid[indexsave].cellClass = "visitada";
                        grid.Add(cellsGrid[indexsave].cellClass);
                        changes.Add(indexsave.ToString());
                    }
                    break;
                }

                if (CurrentCell.cellClass != "inicio") {
                    cellsGrid[index].cellClass = "atual";
                    grid.Add(cellsGrid[index].cellClass);
                    changes.Add(index.ToString());
                }
                if (indexsave > -1 && cellsGrid[indexsave].cellClass != "inicio") {
                    NumSteps++;
                    cellsGrid[indexsave].cellClass = "visitada";
                    grid.Add(cellsGrid[indexsave].cellClass);
                    changes.Add(indexsave.ToString());
                }

                if (CurrentCell.x != 0) { //verifica se é vizinho da celula atual
                    if (cellsGrid[index - 74].cellClass != "parede" && cellsGrid[index - 74].cellClass != "inicio" && cellsGrid[index - 74].cellClass != "visitada") {
                        if (cellsPath.Contains(cellsGrid[index - 74])) {//verifica se ja está na fila
                        } else { //adiciona os vizinhos da celula atual na fila
                            neighborsIndex.Add(index - 74);
                        }
                    }
                }
                if (CurrentCell.y != 73) { //verifica se é vizinho da celula atual
                    if (cellsGrid[index + 1].cellClass != "parede" && cellsGrid[index + 1].cellClass != "inicio" && cellsGrid[index + 1].cellClass != "visitada") {
                        if (cellsPath.Contains(cellsGrid[index + 1])) {//verifica se ja está na fila
                        } else {
                            neighborsIndex.Add(index + 1);
                        }
                    }
                }
                if (CurrentCell.x != 30) { //verifica se é vizinho da celula atual
                    if (cellsGrid[index + 74].cellClass != "parede" && cellsGrid[index + 74].cellClass != "inicio" && cellsGrid[index + 74].cellClass != "visitada") {
                        if (cellsPath.Contains(cellsGrid[index + 74])) {//verifica se ja está na fila
                        } else {
                            neighborsIndex.Add(index + 74);
                        }
                    }
                }
                if (CurrentCell.y != 0) { //verifica se é vizinho da celula atual
                    if (cellsGrid[index - 1].cellClass != "parede" && cellsGrid[index - 1].cellClass != "inicio" && cellsGrid[index - 1].cellClass != "visitada") {
                        if (cellsPath.Contains(cellsGrid[index - 1])) {//verifica se ja está na fila
                        } else {
                            neighborsIndex.Add(index - 1);
                        }
                    }
                }
                if (neighborsIndex.Count > 0) {
                    cellsGrid[neighborsIndex[0]].parent = CurrentCell;
                    cellsPath.Push(cellsGrid[neighborsIndex[0]]); //adiciona os vizinhos da celula atual na fila
                    cellsIndex.Push(neighborsIndex[0]);
                    neighborsIndex.Clear();
                } else {
                    cellsPath.Pop();
                    cellsIndex.Pop();
                }
            }

            CellGrid solvePath = CreatePath(CurrentCell, cellsGrid);
            resultado.classes = string.Join(",", grid) + solvePath.classes;
            resultado.xs = string.Join(",", changes) + solvePath.xs;
            resultado.stepstoResult = NumSteps + solvePath.stepstoResult;
            return resultado;
        }

        public static CellGrid AStar(Cell[] cellsGrid) {
            CellGrid resultado = new CellGrid();
            int IdStart = 0, IdEnd = 0;
            foreach (Cell c in cellsGrid) {
                if (c.cellClass == "inicio") {
                    break;
                }
                IdStart++;
            }

            foreach (Cell c in cellsGrid) {
                if (c.cellClass == "fim") {
                    break;
                }
                IdEnd++;
            }

            int g = 0, h = Math.Abs(cellsGrid[IdEnd].x - cellsGrid[IdStart].x) + Math.Abs(cellsGrid[IdEnd].y - cellsGrid[IdStart].y), f = g + h;

            int NumSteps = 1, index = 0;
            Cell CurrentCell = new Cell();
            List<string> grid = new List<string>();
            List<string> changes = new List<string>();
            foreach (Cell c in cellsGrid) {
                grid.Add(c.cellClass);
            }
            resultado.ys = string.Join(",", grid);
            grid.Clear();

            List<Cell> Obj = new List<Cell>();
            List<int> Objf = new List<int>();
            List<int> Objh = new List<int>();
            List<int> Objg = new List<int>();
            PriorityQueue cellsNeightboors = new PriorityQueue(Obj, Objf, Objh, Objg); //cria fila
            cellsNeightboors.Enqueue(cellsGrid[IdStart], f, h, g);

            while (cellsNeightboors.Count() > 0) {
                (CurrentCell, g) = cellsNeightboors.Dequeue(); // vai para o próximo vizinho
                g = g + 1;
                int i = 0;
                foreach (Cell c in cellsGrid) {
                    if (cellsGrid[i] == CurrentCell) {
                        index = i;
                    }
                    i++;
                }

                if (CurrentCell.cellClass != "fim") { //verifica se é o final
                    NumSteps++;
                } else {
                    break;
                }

                if (CurrentCell.cellClass != "inicio") {
                    cellsGrid[index].cellClass = "visitada";
                    grid.Add(cellsGrid[index].cellClass);
                    changes.Add(index.ToString());
                }

                if (CurrentCell.x != 30) { //verifica se é vizinho da celula atual
                    if (cellsGrid[index + 74].cellClass != "parede" && cellsGrid[index + 74].cellClass != "inicio" && cellsGrid[index + 74].cellClass != "visitada") {
                        if (!cellsNeightboors.Contains(cellsGrid[index + 74])) {
                            h = Math.Abs(cellsGrid[IdEnd].x - cellsGrid[index + 74].x) + Math.Abs(cellsGrid[IdEnd].y - cellsGrid[index + 74].y);
                            f = g + h;
                            cellsGrid[index + 74].parent = CurrentCell;
                            cellsNeightboors.Enqueue(cellsGrid[index + 74], f, h, g); //adiciona os vizinhos da celula atual na fila
                        }
                    }
                }
                if (CurrentCell.y != 0) { //verifica se é vizinho da celula atual
                    if (cellsGrid[index - 1].cellClass != "parede" && cellsGrid[index - 1].cellClass != "inicio" && cellsGrid[index - 1].cellClass != "visitada") {
                        if (!cellsNeightboors.Contains(cellsGrid[index - 1])) {
                            h = Math.Abs(cellsGrid[IdEnd].x - cellsGrid[index - 1].x) + Math.Abs(cellsGrid[IdEnd].y - cellsGrid[index - 1].y);
                            f = g + h;
                            cellsGrid[index - 1].parent = CurrentCell;
                            cellsNeightboors.Enqueue(cellsGrid[index - 1], f, h, g); //adiciona os vizinhos da celula atual na fila
                        }
                    }
                }
                if (CurrentCell.x != 0) { //verifica se é vizinho da celula atual
                    if (cellsGrid[index - 74].cellClass != "parede" && cellsGrid[index - 74].cellClass != "inicio" && cellsGrid[index - 74].cellClass != "visitada") {
                        if (!cellsNeightboors.Contains(cellsGrid[index - 74])) {
                            h = Math.Abs(cellsGrid[IdEnd].x - cellsGrid[index - 74].x) + Math.Abs(cellsGrid[IdEnd].y - cellsGrid[index - 74].y);
                            f = g + h;
                            cellsGrid[index - 74].parent = CurrentCell;
                            cellsNeightboors.Enqueue(cellsGrid[index - 74], f, h, g); //adiciona os vizinhos da celula atual na fila
                        }
                    }
                }
                if (CurrentCell.y != 73) { //verifica se é vizinho da celula atual
                    if (cellsGrid[index + 1].cellClass != "parede" && cellsGrid[index + 1].cellClass != "inicio" && cellsGrid[index + 1].cellClass != "visitada") {
                        if (!cellsNeightboors.Contains(cellsGrid[index + 1])) {
                            h = Math.Abs(cellsGrid[IdEnd].x - cellsGrid[index + 1].x) + Math.Abs(cellsGrid[IdEnd].y - cellsGrid[index + 1].y);
                            f = g + h;
                            cellsGrid[index + 1].parent = CurrentCell;
                            cellsNeightboors.Enqueue(cellsGrid[index + 1], f, h, g); //adiciona os vizinhos da celula atual na fila
                        }
                    }
                }
            }

            CellGrid solvePath = CreatePath(CurrentCell, cellsGrid);
            resultado.classes = string.Join(",", grid) + solvePath.classes;
            resultado.xs = string.Join(",", changes) + solvePath.xs;
            resultado.stepstoResult = NumSteps + solvePath.stepstoResult;
            return resultado;
        }

        public static CellGrid GenerateDFS(Cell[] cellsGrid) {
            CellGrid resultado = new CellGrid();
            int IdStart = 0;
            int NumSteps = 1, index = 0;

            Cell CurrentCell = new Cell();
            List<string> grid = new List<string>();
            List<string> changes = new List<string>();
            foreach (Cell c in cellsGrid) {
                c.cellClass = "parede";
                grid.Add(c.cellClass);
            }
            resultado.ys = string.Join(",", grid);
            grid.Clear();

            List<int> neighborsIndex = new List<int>();

            Stack<int> cellsIndex = new Stack<int>();
            cellsIndex.Push(IdStart);
            int indexmiddle = 0;

            Stack<Cell> cellsPath = new Stack<Cell>(); //cria fila
            cellsPath.Push(cellsGrid[IdStart]);

            while (cellsPath.Count > 0) {
                CurrentCell = cellsPath.Peek();
                index = cellsIndex.Peek();
                if (CurrentCell.cellClass != "caminho") {
                    NumSteps++;
                    cellsGrid[indexmiddle].cellClass = "caminho";
                    grid.Add(cellsGrid[indexmiddle].cellClass);
                    changes.Add(indexmiddle.ToString());

                    NumSteps++;
                    cellsGrid[index].cellClass = "caminho";
                    grid.Add(cellsGrid[index].cellClass);
                    changes.Add(index.ToString());
                }

                if (CurrentCell.x > 1) { //verifica se é vizinho da celula atual
                    if (cellsGrid[index - 148].cellClass != "caminho") {
                        if (cellsPath.Contains(cellsGrid[index - 148])) {//verifica se ja está na fila
                        } else { //adiciona os vizinhos da celula atual na fila
                            neighborsIndex.Add(-74);
                        }
                    }
                }
                if (CurrentCell.y < 72) { //verifica se é vizinho da celula atual
                    if (cellsGrid[index + 2].cellClass != "caminho") {
                        if (cellsPath.Contains(cellsGrid[index + 2])) {//verifica se ja está na fila
                        } else {
                            neighborsIndex.Add(1);
                        }
                    }
                }
                if (CurrentCell.x < 29) { //verifica se é vizinho da celula atual
                    if (cellsGrid[index + 148].cellClass != "caminho") {
                        if (cellsPath.Contains(cellsGrid[index + 148])) {//verifica se ja está na fila
                        } else {
                            neighborsIndex.Add(74);
                        }
                    }
                }
                if (CurrentCell.y > 1) { //verifica se é vizinho da celula atual
                    if (cellsGrid[index - 2].cellClass != "caminho") {
                        if (cellsPath.Contains(cellsGrid[index - 2])) {//verifica se ja está na fila
                        } else {
                            neighborsIndex.Add(-1);
                        }
                    }
                }
                if (neighborsIndex.Count > 0) {
                    int randomPicker = new Random().Next(0, neighborsIndex.Count);
                    cellsGrid[index + neighborsIndex[randomPicker] * 2].parent = CurrentCell;
                    cellsPath.Push(cellsGrid[index + neighborsIndex[randomPicker] * 2]); //adiciona os vizinhos da celula atual na fila
                    cellsIndex.Push(index + neighborsIndex[randomPicker] * 2);
                    indexmiddle = index + neighborsIndex[randomPicker];
                    neighborsIndex.Clear();
                } else {
                    cellsPath.Pop();
                    cellsIndex.Pop();
                }
            }

            NumSteps++;
            cellsGrid[900].cellClass = "inicio";
            grid.Add(cellsGrid[900].cellClass);
            changes.Add(900.ToString());
            NumSteps++;
            cellsGrid[950].cellClass = "fim";
            grid.Add(cellsGrid[950].cellClass);
            changes.Add(950.ToString());
            NumSteps++;
            cellsGrid[index].cellClass = "caminho";
            grid.Add(cellsGrid[index].cellClass);
            changes.Add(index.ToString());

            resultado.classes = string.Join(",", grid);
            resultado.xs = string.Join(",", changes);
            resultado.stepstoResult = NumSteps;
            return resultado;

        }
        public static CellGrid GenerateFIB(Cell[] cellsGrid) {
            CellGrid resultado = new CellGrid();
            Cell currentCell;
            int IdStart;
            int NumSteps = 1, index, indexend = 0, fib1 = 1, fib2 = 0, fibsum = 1;

            List<string> grid = new List<string>();
            List<string> changes = new List<string>();
            List<int> indexErase = new List<int>();

            foreach (Cell c in cellsGrid) {
                c.cellClass = "caminho";
                grid.Add(c.cellClass);
            }
            resultado.ys = string.Join(",", grid);
            grid.Clear();

            IdStart = 0;
            index = IdStart;

            int j = 0, z;
            for (int i = 0; i < 8; i++) {

                while (cellsGrid[index].y < (73 - i * 2)) {
                    NumSteps++;
                    cellsGrid[index].cellClass = "parede";
                    grid.Add(cellsGrid[index].cellClass);
                    changes.Add(index.ToString());
                    indexErase.Add(index);
                    index = index + 1;
                }
                while (cellsGrid[index].x < (30 - i * 2)) {
                    NumSteps++;
                    cellsGrid[index].cellClass = "parede";
                    grid.Add(cellsGrid[index].cellClass);
                    changes.Add(index.ToString());
                    indexErase.Add(index);
                    index = index + 74;
                }
                while (cellsGrid[index].y > (0 + i * 2)) {
                    NumSteps++;
                    cellsGrid[index].cellClass = "parede";
                    grid.Add(cellsGrid[index].cellClass);
                    changes.Add(index.ToString());
                    indexErase.Add(index);
                    index = index - 1;
                }
                while (cellsGrid[index].x > (0 + i * 2)) {
                    NumSteps++;
                    cellsGrid[index].cellClass = "parede";
                    grid.Add(cellsGrid[index].cellClass);
                    changes.Add(index.ToString());
                    indexErase.Add(index);
                    index = index - 74;
                }
                int erase = new Random().Next(0, indexErase.Count);
                erase = indexErase[erase];
                while (cellsGrid[erase].x == (0 + i * 2) && cellsGrid[erase].y == (0 + i * 2) || cellsGrid[erase].x == (30 - i * 2) && cellsGrid[erase].y == (0 + i * 2) || cellsGrid[erase].x == (30 - i * 2) && cellsGrid[erase].y == (73 - i * 2) || cellsGrid[erase].x == (0 + i * 2) && cellsGrid[erase].y == (73 - i * 2)) 
                {
                    erase = new Random().Next(0, indexErase.Count);
                    erase = indexErase[erase];
                }

                NumSteps++;
                cellsGrid[erase].cellClass = "caminho";
                grid.Add(cellsGrid[erase].cellClass);
                changes.Add(erase.ToString());

                indexErase.Clear();

                index = index + 148 + 2;
            }
            index = index - 74 - 1;
            indexend = IdStart + 74 + 1;
            NumSteps++;
            cellsGrid[index].cellClass = "inicio";
            grid.Add(cellsGrid[index].cellClass);
            changes.Add(index.ToString());
            NumSteps++;
            cellsGrid[indexend].cellClass = "fim";
            grid.Add(cellsGrid[indexend].cellClass);
            changes.Add(indexend.ToString());

            resultado.classes = string.Join(",", grid);
            resultado.xs = string.Join(",", changes);
            resultado.stepstoResult = NumSteps;
            return resultado;
        }

        public static CellGrid GenerateREC(Cell[] cellsGrid) {
            CellGrid resultado = new CellGrid();
            Cell currentCell;
            int IdStart;
            int NumSteps = 1, index, indexend = 0, fib1 = 1, fib2 = 0, fibsum = 1;

            List<string> grid = new List<string>();
            List<string> changes = new List<string>();
            List<int> indexErase = new List<int>();

            foreach (Cell c in cellsGrid) {
                c.cellClass = "caminho";
                grid.Add(c.cellClass);
            }
            resultado.ys = string.Join(",", grid);
            grid.Clear();

            IdStart = 0;
            index = IdStart;

            while (cellsGrid[index].y < 73) {
                NumSteps++;
                cellsGrid[index].cellClass = "parede";
                grid.Add(cellsGrid[index].cellClass);
                changes.Add(index.ToString());
                index = index + 1;
            }
            while (cellsGrid[index].x < 30) {
                NumSteps++;
                cellsGrid[index].cellClass = "parede";
                grid.Add(cellsGrid[index].cellClass);
                changes.Add(index.ToString());
                index = index + 74;
            }
            while (cellsGrid[index].y > 0) {
                NumSteps++;
                cellsGrid[index].cellClass = "parede";
                grid.Add(cellsGrid[index].cellClass);
                changes.Add(index.ToString());
                index = index - 1;
            }
            while (cellsGrid[index].x > 0) {
                NumSteps++;
                cellsGrid[index].cellClass = "parede";
                grid.Add(cellsGrid[index].cellClass);
                changes.Add(index.ToString());
                index = index - 74;
            }

            for (int i = 1; i < 4; i++) {
                int erase = new Random().Next(0, 1);
                switch (erase) {
                    case 0:
                        erase = new Random().Next(1, 14);
                        index = index + 1 + 148 * erase;
                        while (cellsGrid[index].y < (73)) {
                            NumSteps++;
                            cellsGrid[index].cellClass = "parede";
                            grid.Add(cellsGrid[index].cellClass);
                            changes.Add(index.ToString());
                            indexErase.Add(index);
                            index = index + 1;
                        }
                        index = index - 1;
                        break;
                    case 1:
                        erase = new Random().Next(1, 14);
                        index = index + 74 + 2 * erase;
                        while (cellsGrid[index].x < (30)) {
                            NumSteps++;
                            cellsGrid[index].cellClass = "parede";
                            grid.Add(cellsGrid[index].cellClass);
                            changes.Add(index.ToString());
                            indexErase.Add(index);
                            index = index + 74;
                        }
                        index = index - 74;
                        break;
                }
                erase = new Random().Next(0, indexErase.Count);
                erase = indexErase[erase];
                while (cellsGrid[erase].x == (0 + i * 2) && cellsGrid[erase].y == (0 + i * 2) || cellsGrid[erase].x == (30 - i * 2) && cellsGrid[erase].y == (0 + i * 2) || cellsGrid[erase].x == (30 - i * 2) && cellsGrid[erase].y == (73 - i * 2) || cellsGrid[erase].x == (0 + i * 2) && cellsGrid[erase].y == (73 - i * 2)) {
                    erase = new Random().Next(0, indexErase.Count);
                    erase = indexErase[erase];
                }

                NumSteps++;
                cellsGrid[erase].cellClass = "caminho";
                grid.Add(cellsGrid[erase].cellClass);
                changes.Add(erase.ToString());

                indexErase.Clear();
            }

            resultado.classes = string.Join(",", grid);
            resultado.xs = string.Join(",", changes);
            resultado.stepstoResult = NumSteps;
            return resultado;
        }
    }
}