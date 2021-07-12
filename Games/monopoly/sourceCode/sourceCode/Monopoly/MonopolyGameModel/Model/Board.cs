using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.BoardCells;

namespace MonopolyGameModel.Model
{
    public class Board
    {
        private List<ABoardCell> r_Cells = new List<ABoardCell>();

        /// <summary>
        /// creates the board for the game.
        /// </summary>
        public Board()
        {
            createBoardCells();
        }

        /// <summary>
        /// This function creates the cell of the board game.
        /// Nothing fancy here.
        /// </summary>
        private void createBoardCells()
        {
            
            r_Cells.Add(new AuxCells(AuxCells.AuxCellType.GO));
            r_Cells.Add(new CityCell(60));
            r_Cells.Add(new ChanceCell());
            r_Cells.Add(new CityCell(60));
            r_Cells.Add(new TaxCell());
            r_Cells.Add(new RailroadCell());
            r_Cells.Add(new CityCell(100));
            r_Cells.Add(new ChanceCell());
            r_Cells.Add(new CityCell(100));
            r_Cells.Add(new CityCell(120));

            #region more of the same
            r_Cells.Add(new AuxCells(AuxCells.AuxCellType.JAIL));
            r_Cells.Add(new CityCell(140));
            r_Cells.Add(new UtilityCell(150));
            r_Cells.Add(new CityCell(140));
            r_Cells.Add(new CityCell(160));
            r_Cells.Add(new RailroadCell());
            r_Cells.Add(new CityCell(180));
            r_Cells.Add(new ChanceCell());
            r_Cells.Add(new CityCell(180));
            r_Cells.Add(new CityCell(200));

            r_Cells.Add(new AuxCells(AuxCells.AuxCellType.FREE_PARKING));
            r_Cells.Add(new CityCell(220));
            r_Cells.Add(new ChanceCell());
            r_Cells.Add(new CityCell(220));
            r_Cells.Add(new CityCell(240));
            r_Cells.Add(new RailroadCell());
            r_Cells.Add(new CityCell(260));
            r_Cells.Add(new CityCell(260));
            r_Cells.Add(new UtilityCell(150));
            r_Cells.Add(new CityCell(280));

            r_Cells.Add(new AuxCells(AuxCells.AuxCellType.GO_TO_JAIL));
            r_Cells.Add(new CityCell(300));
            r_Cells.Add(new CityCell(300));
            r_Cells.Add(new ChanceCell());
            r_Cells.Add(new CityCell(320));
            r_Cells.Add(new RailroadCell());
            r_Cells.Add(new ChanceCell());
            r_Cells.Add(new CityCell(350));
            r_Cells.Add(new TaxCell());
            r_Cells.Add(new CityCell(400));
            #endregion 
        }
    }
}
