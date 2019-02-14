using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LuckySpin.Models;
using LuckySpin.ViewModels;

namespace LuckySpin.Controllers
{
    public class SpinnerController : Controller
    {
        private LuckySpinDataContext _dbc;
        Random random;

        /***
         * Controller Constructor
         */
        public SpinnerController(LuckySpinDataContext dbc)
        {
            random = new Random();
            _dbc = dbc;
        }

        /***
         * Entry Page Action
         **/
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Player player)
        {
            if (!ModelState.IsValid) { return View(); }

            _dbc.Players.Add(player);
            _dbc.SaveChanges();
            
            return RedirectToAction("SpinIt", new { idNum = player.Id });
        }

        /***
         * SpinIt Action
         **/
         public IActionResult SpinIt(int idNum)
        {
            var currentPlayer = _dbc.Players.Include(x => x.Spins)
                .Single(x => x.Id == idNum);

            SpinViewModel spinVM = new SpinViewModel()
            {
                A = random.Next(1, 10),
                B = random.Next(1, 10),
                C = random.Next(1, 10),
                Luck = currentPlayer.Luck,
                Balance = currentPlayer.Balance,
                FirstName = currentPlayer.FirstName,
            };

            spinVM.IsWinning = (spinVM.A == spinVM.Luck || spinVM.B == spinVM.Luck || spinVM.C == spinVM.Luck);

            //Use the ViewBag to handle items just needed for display
            if(spinVM.IsWinning)
                ViewBag.Display = "block";
            else
                ViewBag.Display = "none";

            ViewBag.PlayerId = idNum;

            _dbc.Spins.Add(new Spin { IsWinning = spinVM.IsWinning });
            _dbc.SaveChanges();

            return View("SpinIt", spinVM);
        }

        /***
         * ListSpins Action
         **/
         public IActionResult LuckList(int id)
        {
            var Player = _dbc.Players.Include(x => x.Spins).Single(x => x.Id == id);

            return View(Player.Spins);
        }
    }
}

