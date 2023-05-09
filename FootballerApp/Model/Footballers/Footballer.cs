using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballerApp.Model.Interfaces;
using Microsoft.Data.Sqlite;

namespace FootballerApp.Model.Footballers
{
    public class Footballer: SportsmanItem
    {
        public Footballer(SqliteDataReader reader)
        {
            ID = reader.GetInt32(0);
            FirstName = reader.GetString(1);
            LastName = reader.GetString(2);
            Gender = reader.GetBoolean(3);
            DateOfBirth = reader.GetString(4);
            TeamName = reader.GetString(5);
            Country = reader.GetString(6);
        }
    }
}
