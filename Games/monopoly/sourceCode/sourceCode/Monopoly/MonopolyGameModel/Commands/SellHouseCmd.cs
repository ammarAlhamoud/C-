﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonopolyGameModel.Model;

namespace MonopolyGameModel.Commands
{
    public class SellHouseCmd : ACommand
    {
        public SellHouseCmd(APlayer player)
            : base(player)
        {

        }
    }
}
