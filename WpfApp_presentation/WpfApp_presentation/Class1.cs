using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_presentation
{
    class Person
    {
        public String Name
        {
            get;
            set;
        }

        public int Age
        {
            get;
            set;
        }

        public ObservableCollection<Person> Combokers
        {
            get;
            set;
        }
    }
}
