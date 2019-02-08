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

            /* **
             * No longer needed - use an int instead
             *             
             SpinViewModel spinVM = new SpinViewModel()
            {
                PlayerId = player.Id,
                FirstName = player.FirstName,
                Balance = player.Balance,
                Luck = player.Luck
            };
            */

            //TODO: only pass the player.Id to the SpinIt action
            return RedirectToAction("SpinIt");
        }

        /***
         * SpinIt Action
         **/  
               
         public IActionResult SpinIt() //TODO: change parameter to receive players Id
        {
            //TODO: Get the player with the given Id using the Players DbSet Find(Id) method


            //TODO: Build a new SpinItViewModel object with data from the Player and spin
            SpinViewModel spinVM = new SpinViewModel()
            {
                A = random.Next(1, 10),
                B = random.Next(1, 10),
                C = random.Next(1, 10)

            };

            spinVM.IsWinning = (spinVM.A == spinVM.Luck || spinVM.B == spinVM.Luck || spinVM.C == spinVM.Luck);

            //Prepare the ViewBag
            if(spinVM.IsWinning)
                ViewBag.Display = "block";
            else
                ViewBag.Display = "none";
            //TODO Add an item called ViewBag.PlayerId that assigns the LuckList link a route_id in the View
            //      (see the <a href> for "Current Balance" in the SpinIt.cshtml file)


            //TODO CHANGE THIS to add the new Spin to the __Players__ spin list
            _dbc.Spins.Add(new Spin() { IsWinning = spinVM.IsWinning });
            _dbc.SaveChanges();

            return View("SpinIt", spinVM);
        }

        /***
         * ListSpins Action
         **/

         public IActionResult LuckList(int id)
        {
            //TODO: get all the spins for the given Player from the Database


            //TODO: wrap each spin as an IEnumerable and send to the View
            return View();
        }

    }
}

