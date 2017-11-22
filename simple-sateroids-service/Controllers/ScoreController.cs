using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using simple_sateroids_service.Db;
using System.Linq;

namespace simple_sateroids_service.Controllers
{
    public class ScoreController : Controller
    {        
        [HttpGet]
        [EnableCors("AllowAllOrigins")]
        public IActionResult GetLeaderboard()
        {            
            return Json(AsteroidsDb.GetLeaderboard().OrderByDescending(s => s.score));
        }

        [HttpPost]
        [EnableCors("AllowAllOrigins")]
        public IActionResult AddScore(string name, int score)
        {
            AsteroidsDb.AddScore(name, score);
            return Json(new { result = "ok" });
        }
    }
}
