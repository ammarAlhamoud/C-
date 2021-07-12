using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Balls.Common.Interfaces;
using Balls.Common.Infrastructure.UI.Base;
using Balls.Common.Models;

namespace Balls.UI.ViewModel
{
    public class HelpViewModel : ViewModelBase, IHelpViewModel
    {
        public Common.Interfaces.Base.IBaseView View
        {
            get;
            set;
        }

        public List<HelpModel> ListOfHelpModels
        {
            get
            {
                return new List<HelpModel>{
                    new HelpModel{Recno =1, Name="Help/1"},
                    new HelpModel{Recno =2, Name="Help/2"},
                    new HelpModel{Recno =3, Name="Help/3"},
                    new HelpModel{Recno =4, Name="Help/4"},
                    new HelpModel{Recno =5, Name="Help/5"},
                    new HelpModel{Recno =6, Name="Help/6"},
                    new HelpModel{Recno =7, Name="Help/7"},
                };
            }
        }

        public HelpViewModel(IHelpView view)
        {
            this.View = view;
            this.View.SetViewModel(this);
        }

        public void LoadData(string uri)
        {

        }
    }
}
