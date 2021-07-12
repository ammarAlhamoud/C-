using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp_presentation
{
    class PersonViewModel : INotifyPropertyChanged
    {
        private Person _personmodel;

        public ICommand IncreaseAgeCommand { get; }

        public PersonViewModel(Person person)
        {
            _personmodel = person;

            IncreaseAgeCommand = new RelayCommand(

                p => CheckAge(),
            p => increaseAge());

            Combokers = new ObservableCollection<Person>();
            Combokers.Add(new Person() { Name = " Ammar ", Age = 23 });
            Combokers.Add(new Person() { Name = "Tobias" , Age = 20 });
            Combokers.Add(new Person() { Name = "Salem", Age = 26 });

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public String Name
        {
            get
            {
                return _personmodel.Name;

            }
            set
            {
                _personmodel.Name = value;
                OnPropertyChanged("Name");
            }
        }
        public int Age
        {
            get
            {
                return _personmodel.Age;

            }
            set
            {
                _personmodel.Age = value;
                OnPropertyChanged("Age");
            }
        }

        protected internal void OnPropertyChanged(String propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
        public void increaseAge()
        {
            Age++;
        }

        private bool CheckAge()
        {
            bool result = true;
            if (Age >= 40)
            {
                result = false;
            }
            return result;
        }

        

       

        public ObservableCollection<Person> Combokers
        {
            get {
                return _personmodel.Combokers;
            }
            private set
            {
                _personmodel.Combokers = value;
                OnPropertyChanged("Combokers");
            }
        }

    }
}
