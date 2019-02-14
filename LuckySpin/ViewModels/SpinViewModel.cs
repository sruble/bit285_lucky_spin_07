using System;
namespace LuckySpin.ViewModels
{
    public class SpinViewModel
    {
        //will get from the Player entity
        public string FirstName { get; set; }
        public decimal Balance { get; set; }
        public int Luck { get; set; }

        //will get from the spin process
        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public Boolean IsWinning { get; set; }


    }
}
