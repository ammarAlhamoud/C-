using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.BoardCells
{
    public class CityCell:ABoardCell
    {
        private readonly int r_Price;

        public CityCell(int i_Price)
        {
            this.r_Price = i_Price;
        }
    }
}
