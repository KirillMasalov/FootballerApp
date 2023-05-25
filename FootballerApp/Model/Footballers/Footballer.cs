using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballerApp.Model.Interfaces;
using Microsoft.Data.Sqlite;
using FootballerApp.Pages;

namespace FootballerApp.Model.Footballers
{
    public class Footballer : SportsmanItem
    {
        public Footballer() : base()
        { }
        public Footballer(IndexModel.InputModel input)
        {
            FirstName = input.FirstName;
            LastName = input.LastName;
            Gender = input.Gender == "male";
            DateOfBirth = DateTime.Parse(input.DateOfBirth.ToString());
            TeamName = input.TeamName;
            Country = input.Country;
        }
    }
}
