using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MmaFIghter.MVVM.Models
{
    public class Favourite
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FighterFirstName { get; set; }
        public string FighterLastName { get; set; }
    }
}
