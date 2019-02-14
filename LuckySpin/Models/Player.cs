using System.Collections.Generic;//add this line to link to the library
using System.ComponentModel.DataAnnotations;
namespace LuckySpin.Models
{
    public class Player
    {
        public long Id { get; set; }

        [Required(ErrorMessage ="Please enter your Name")]
        public string FirstName { get; set; }

        [Range(1,9, ErrorMessage = "Choose a number")]
        public int Luck { get; set; }

        [Range(1,100, ErrorMessage ="Enter a balance between 1 and 100")]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }

        //TODO: Each Player has a set of Spins, add these as an ICollection<Spin>
        //(Refresh your DB afterwards by deleting the LuckySpin database and re-run)
        public ICollection<Spin> Spins { get; set; }

 
    }
}