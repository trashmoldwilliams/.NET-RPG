using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using TimeAdventure.Models;
using Microsoft.Data.Entity;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeAdventure.Controllers
{
    [Authorize]
    public class GameController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public GameController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext db
        )
        {
            _userManager = userManager;
            _db = db;
        }

        private ApplicationDbContext db = new ApplicationDbContext();
        public IActionResult Index()
        {
            var player = _db.Players.FirstOrDefault(p => p.PlayerId == 1);
            ViewData["PlayerId"] = player.PlayerId;
            //var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
            return View();
        }

        public IActionResult LoadLevel(int levelid, int playerId)
        {
            var thisLevel = _db.Levels.FirstOrDefault(x => x.LevelId == levelid);
            var levelPlayer = new LevelPlayer();
            levelPlayer.LevelId = levelid;
            levelPlayer.PlayerId = playerId;

            _db.LevelPlayers.Add(levelPlayer);
            _db.SaveChanges();
            return View(thisLevel);
        }
        public IActionResult Fight(int id)
        {
            Level level = _db.Levels.FirstOrDefault(m => m.LevelId == id);
            level.Players = _db.Players.Join(_db.LevelPlayers.Where(m => m.LevelId == id).ToList(),
                m => m.PlayerId,
                m => m.PlayerId,
                (o, i) => o).ToList();
            //var questionList = _db.Questions.ToList();
            return View(level);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Player player)
        {
            var currentUser = await _userManager.FindByIdAsync(User.GetUserId());
            player.User = currentUser;
            _db.Players.Add(player);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}