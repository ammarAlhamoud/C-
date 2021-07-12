using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.ViewModels;
using Monopoly;
using System.Collections.ObjectModel;

namespace Monopoly.Models
{
    public class Player_Manager
    {

        public int CurrentPlayerID { get; set; }
       // public string StrCurrentPlayerID { get; set; }
        

        public ObservableCollection<PlayerBase> Players
        {
            get;
            set;
        }
    }
}
