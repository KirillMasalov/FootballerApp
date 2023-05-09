using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballerApp.Model.Interfaces;
using FootballerApp.Pages;
using Microsoft.Data.Sqlite;

namespace FootballerApp.Model.Footballers
{
    public class FootballersDataHandler: ISportsHandler
    {
        public static readonly List<string> COUNTRIES = new List<string>() { "Россия", "США", "Италия" };

        private readonly string TableName = "Footballers";
        private readonly string FootballerID = "Footballer_ID";
        private readonly string FirstName = "First_Name";
        private readonly string LastName = "Last_Name";
        private readonly string Gender = "Gender";
        private readonly string DateOfBirth = "Date_Of_Birth";
        private readonly string TeamName = "Team_Name";
        private readonly string Country = "Country";

        public IEnumerable<SportsmanItem> GetAllItems()
        {
            var allFootballers = new List<Footballer>();
            using (var connection = new SqliteConnection("Data Source=db.db"))
            {
                connection.Open();

                TryCreateTable(connection);

                var queryCommand = connection.CreateCommand();
                queryCommand.CommandText =
                @$"
                    SELECT {FootballerID}, {FirstName}, {LastName}, {Gender}, {DateOfBirth}, {TeamName}, {Country}
                    FROM {TableName}
                ";

                using (var reader = queryCommand.ExecuteReader())
                {
                    while (reader.Read())
                        allFootballers.Add(new Footballer(reader));
                }
            }

            return allFootballers;
        }

        public IEnumerable<string> GetTeams()
        {
            var allTeams = new List<string>();
            using (var connection = new SqliteConnection("Data Source=db.db"))
            {
                connection.Open();

                TryCreateTable(connection);

                var queryCommand = connection.CreateCommand();
                queryCommand.CommandText =
                @$"
                    SELECT {TeamName}
                    FROM {TableName}
                    GROUP BY {TeamName}
                ";

                using (var reader = queryCommand.ExecuteReader())
                {
                    while (reader.Read())
                        allTeams.Add(reader.GetString(0));
                }
            }

            return allTeams;
        }

        public IEnumerable<string> GetCountries()
        {
            return COUNTRIES;
        }

        private void TryCreateTable(SqliteConnection con)
        { 
            var createCommand = con.CreateCommand();
            createCommand.CommandText =
                @$"
                    CREATE TABLE IF NOT EXISTS {TableName}(
                    {FootballerID} INTEGER PRIMARY KEY AUTOINCREMENT,
                    {FirstName} varchar(255),
                    {LastName} varchar(255),
                    {Gender} boolean,
                    {DateOfBirth} Date,
                    {TeamName} varchar(255),
                    {Country} varchar(255))
                ";

            createCommand.ExecuteReader();
        }

        public void AddItem(ICreateFormInputModel input)
        {
            var footballer= input as IndexModel.InputModel;
            using (var connection = new SqliteConnection("Data Source=db.db"))
            {
                connection.Open();
                TryCreateTable(connection);
                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText =
                    $@"
                        INSERT INTO {TableName} ({FirstName}, {LastName}, {Gender}, {DateOfBirth}, {TeamName}, {Country})
                        VALUES ('{footballer.FirstName}', '{footballer.LastName}',
                                '{((footballer.Gender == "male") ? 1 : 0)}',
                                '{footballer.DateOfBirth?.ToString("dd-MM-yyyy")}',
                                '{footballer.TeamName}', '{footballer.Country}') 
                     ";
                insertCommand.ExecuteNonQuery();
            }
        }

        public void ChangeItem(ISportsmenInfoInputModel input)
        {
            var footballer = input as FootballersModel.InputModel;
            using (var connection = new SqliteConnection("Data Source=db.db"))
            {
                connection.Open();
                TryCreateTable(connection);
                var changeCommand = connection.CreateCommand();
                changeCommand.CommandText =
                    $@"
                        UPDATE {TableName}
                        SET {FirstName} = '{footballer.FirstName}',
                            {LastName} = '{footballer.LastName}',
                            {Gender} = '{((footballer.Gender == "male") ? 1 : 0)}',
                            {DateOfBirth} = '{footballer.DateOfBirth?.ToString("dd-MM-yyyy")}',
                            {TeamName} = '{footballer.TeamName}',
                            {Country} = '{footballer.Country}'
                        WHERE {FootballerID} = {footballer.ID}
                     ";
                changeCommand.ExecuteNonQuery();
            }
        }
    }
}
