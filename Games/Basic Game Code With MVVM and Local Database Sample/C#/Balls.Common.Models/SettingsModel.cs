using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Balls.Common.Models
{
    public class SettingsModel
    {
        public Int32 Recno { get; set; }
        public Boolean IsAudioEnabled { get; set; }
        public double Volume { get; set; }
        public bool IsFromDatabase { get; set; }
    }
}
