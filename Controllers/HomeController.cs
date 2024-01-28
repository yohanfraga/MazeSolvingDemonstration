using MazeSolvingDemonstration.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace MazeSolvingDemonstration.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CellGrid grid) {
            Cell[] cellsGrid = CellGrid.CreateGrid(grid);
            CellGrid showResult;
            switch (grid.method) {
                case "bfs":
                    showResult = Search.BFS(cellsGrid);
                    showResult.vel = grid.vel;
                    showResult.method = grid.method;
                    return View(showResult);
                case "dfs":
                    showResult = Search.DFS(cellsGrid);
                    showResult.vel = grid.vel;
                    showResult.method = grid.method;
                    return View(showResult);
                case "a**":
                    showResult = Search.AStar(cellsGrid);
                    showResult.vel = grid.vel;
                    showResult.method = grid.method;
                    return View(showResult);
                case "constructDFS":
                    showResult = Search.GenerateDFS(cellsGrid);
                    showResult.vel = grid.vel;
                    return View(showResult);
                case "fib":
                    showResult = Search.GenerateFIB(cellsGrid);
                    showResult.vel = grid.vel;
                    return View(showResult);
                case "rec":
                    showResult = Search.GenerateREC(cellsGrid);
                    showResult.vel = grid.vel;
                    return View(showResult);
                default:
                    return View();
            };
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
