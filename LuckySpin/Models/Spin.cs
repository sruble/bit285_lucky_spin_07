using System;
using System.ComponentModel.DataAnnotations;

namespace LuckySpin.Models
{
    public class Spin
    {
        public long Id { get; set; }
        [Required]
        public Boolean IsWinning { get; set; }
    }
}
