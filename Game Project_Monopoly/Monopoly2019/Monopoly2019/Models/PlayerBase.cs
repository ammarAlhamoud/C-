using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly2019.Models
{
    class PlayerBase
    {
        public string Name;
        public int ID;
        public int Credit;
        public int CurrentPosition;
        public List<int> OwnedProperty;

    }
}
