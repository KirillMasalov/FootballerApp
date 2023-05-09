using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballerApp.Model.Interfaces
{
    public abstract class SportsmanItem
    {
        public int ID { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public bool Gender { get; protected set; }
        public string DateOfBirth { get; protected set; }
        public string TeamName { get; protected set; }
        public string Country { get; protected set; }
    }
}
