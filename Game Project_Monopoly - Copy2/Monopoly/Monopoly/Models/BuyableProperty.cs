using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models
{
    class BuyableProperty : PropertyBase
    {
        public int RentPrice, BuyPrice;
        public bool IsRentable, IsBought;
    }
}
