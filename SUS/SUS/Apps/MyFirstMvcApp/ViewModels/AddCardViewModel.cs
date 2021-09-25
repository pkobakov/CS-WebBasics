using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstMvcApp.ViewModels
{
   public class AddCardViewModel
    {
        public int Attack { get; set; }
        public int Health { get; set; }
        public int Damage =>Health-Attack;
    }
}
