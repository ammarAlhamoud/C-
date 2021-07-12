using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.BoardCells
{
    public class UtilityCell:ABoardCell
    {
        private readonly int r_Price;

        public UtilityCell(int i_Price)
        {
            this.r_Price = i_Price;
        }
    }
}
