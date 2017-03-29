using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using PersonExample.Models;
using System.ComponentModel;
using System.Collections;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace PersonExample.ViewModels
{
    class PersonViewModel : BindableBase
    {
        readonly Person Person;
        public ICommand IncrementAge { get; private set; }
        public ICommand DecrementAge { get; private set; }
        string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                if (SetProperty(ref firstName, value))
                {
                    Person.FirstName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }
        public string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                if (SetProperty(ref lastName, value))
                {
                    Person.LastName = value;
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }
        int age;
        public int Age
        {
            get => age;
            set
            {
                if (SetProperty(ref age, value))
                    Person.Age = value;
            }
        }
        public string FullName
        {
            get => FirstName + " " + LastName;
            set
            {
                var splitName = (value ?? "").Split(' ');
                if (splitName.Length < 2)
                    throw new ArgumentException("フルネームはスペース区切りが必要です。");
                if (splitName.Length > 2)
                    throw new ArgumentException("フルネームはスペース区切り一つまで対応しています。");
                FirstName = splitName.First();
                LastName = splitName.Last();
            }
        }
        public PersonViewModel() : this(new Person()) { }
        public PersonViewModel(Person Person) : base()
        {
            (FirstName, LastName, Age) = (Person.FirstName, Person.LastName, Person.Age);
            this.Person = Person;
            this.Person.PropertyChanged += ModelPropertyChange;
            IncrementAge = new DelegateCommand(() => Age++, () => Age < 100);
            DecrementAge = new DelegateCommand(() => Age--, () => 0 < Age);
        }
        void ModelPropertyChange(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Person.FirstName))
                FirstName = Person.FirstName;
            else if (e.PropertyName == nameof(LastName))
                LastName = Person.LastName;
            else if (e.PropertyName == nameof(Age))
                Age = Person.Age;
        }
    }
}
