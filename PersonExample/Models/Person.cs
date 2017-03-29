using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using System.ComponentModel;

namespace PersonExample.Models
{
    class Person : BindableBase
    {
        public Person() : base()
        {
            PropertyChanged += Logger;
        }
        ~Person()
        {
            PropertyChanged -= Logger;
        }
        string firstName;
        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }
        string lastName;
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }
        int age;
        public int Age
        {
            get => age;
            set => SetProperty(ref age, value);
        }
        void Logger(object sender, PropertyChangedEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine($"{args.PropertyName} : {this}");
        }
        public override string ToString()
        {
            return $"{nameof(Person)}{{{nameof(FirstName)}:{FirstName}, {nameof(LastName)}:{LastName}, {nameof(Age)}:{Age}}}";
        }
    }
}
