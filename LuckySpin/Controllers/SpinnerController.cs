using System;
using System.Collections.Generic;
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
         *   Inject the LuckySpinDataContext        
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

            //Add the Player to the DB and save the changes
            _dbc.Players.Add(player);
            _dbc.SaveChanges();

            //Build a new SpinItViewModel object with data from the Player and pass it to the View
            SpinViewModel spinVM = new SpinViewModel()
            {
                PlayerId = player.Id,
                FirstName = player.FirstName,
                Balance = player.Balance,
                Luck = player.Luck
            };

            return RedirectToAction("SpinIt", spinVM);
        }

        /***
         * Spin Action
         **/  
               
         public IActionResult SpinIt(SpinViewModel spinVM)
        {
            spinVM.A = random.Next(1, 10);
            spinVM.B = random.Next(1, 10);
            spinVM.C = random.Next(1, 10);
            spinVM.IsWinning = (spinVM.A == spinVM.Luck || spinVM.B == spinVM.Luck || spinVM.C == spinVM.Luck);

            //Prepare the View
            if(spinVM.IsWinning)
                ViewBag.Display = "block";
            else
                ViewBag.Display = "none";

            ViewBag.PlayerId = spinVM.PlayerId;

            //TODO: Add a new Spin object to the Database
            _dbc.Spins.Add(new Spin() { IsWinning = spinVM.IsWinning });
            _dbc.SaveChanges();

            return View("SpinIt", spinVM);
        }

        /***
         * ListSpins Action
         **/

         public IActionResult LuckList()
        {
            //TODO: get all the spins from the Database and pass it as an IEnumerable<Spin> to the View
                return View();
        }

    }
}

