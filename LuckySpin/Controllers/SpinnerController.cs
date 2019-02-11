using System;
using System.Collections.Generic;
//TODO: add the Microsoft.EntityFrameworkCore
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
             * No longer needed - we will use the player id instead
             *             
             SpinViewModel spinVM = new SpinViewModel()
            {
                PlayerId = player.Id,
                FirstName = player.FirstName,
                Balance = player.Balance,
                Luck = player.Luck
            };
            */

            //TODO: pass the player.Id to the SpinIt action
            //      (remember, you have to pass it as an object property so use the 'new { }' syntax)
            return RedirectToAction("SpinIt");
        }

        /***
         * SpinIt Action
         **/  
               
         public IActionResult SpinIt() //TODO: add an int parameter to receive the id
        {
            //TODO: Use the _dbc and the given id to get the current player object 
            //       from Players, and Include her Spins (use Lamda expressions)
            var currentPlayer = _dbc.Players
                              ; //The above is incomplete

            //TODO: Add the properties to this SpinItViewModel object with data from the currentPlayer
            SpinViewModel spinVM = new SpinViewModel()
            {
                A = random.Next(1, 10),
                B = random.Next(1, 10),
                C = random.Next(1, 10),
                //Luck = currentPlayer.Luck,
                //Balance = currentPlayer.Balance,
                //FirstName = currentPlayer.FirstName,
            };

            spinVM.IsWinning = (spinVM.A == spinVM.Luck || spinVM.B == spinVM.Luck || spinVM.C == spinVM.Luck);

            //Use the ViewBag to handle items just needed for display
            if(spinVM.IsWinning)
                ViewBag.Display = "block";
            else
                ViewBag.Display = "none";
            //TODO Assign a ViewBag.PlayerId item used to assigns a link its route_id in SpinIt View
            //      (see the <a href> for "Current Balance" in the SpinIt.cshtml file)


            //TODO Compare DB records when adding a generic Spin, as shown below, 
            //     with adding a new Spin to the current player's Spins list
            _dbc.Spins.Add(new Spin { IsWinning = spinVM.IsWinning });
            _dbc.SaveChanges();

            return View("SpinIt", spinVM);
        }

        /***
         * ListSpins Action
         **/

         public IActionResult LuckList(int id)
        {
            //TODO: use the id to get the current player, including her Spins list


            //TODO: Send the player's Spins to the View
            return View();
        }

    }
}

