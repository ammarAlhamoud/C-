using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.BoardCells
{
    class AuxCells:ABoardCell
    {
        private readonly AuxCellType r_CellType;

        public AuxCells(AuxCellType i_Type)
        {
            this.r_CellType = i_Type;
        }
        

        public enum AuxCellType
        {
            GO,
            GO_TO_JAIL,
            FREE_PARKING,
            JAIL
        }
    }
}
