using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballerApp.Model.Interfaces;
using FootballerApp.Pages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace FootballerApp.Model.Footballers
{
    public class FootballersDataHandler: ISportsHandler
    {
        public static readonly List<string> COUNTRIES = new List<string>() { "Россия", "США", "Италия" };
        private SportsmanDbContext context;

        public FootballersDataHandler(SportsmanDbContext dbContext)
        {
            context = dbContext;
        }

        public IEnumerable<SportsmanItem> GetAllItems()
        {
            return context.Footballers;
        }

        public IEnumerable<string> GetTeams()
        {
            return context.Footballers.GroupBy(f => f.TeamName).Select(g => g.Key);
        }

        public IEnumerable<string> GetCountries()
        {
            return COUNTRIES;
        }


        public void AddItem(ICreateFormInputModel input)
        {
            var footballer = input as IndexModel.InputModel;
            context.Footballers.Add(new Footballer(footballer));
            context.SaveChangesAsync();
        }

        public void ChangeItem(ISportsmenInfoInputModel input)
        {
            var footballer_input = input as FootballersModel.InputModel;
            var footballer = context.Footballers.FirstOrDefault(f => f.Id == footballer_input.ID);
            if (footballer != null)
            {
                footballer.FirstName = footballer_input.FirstName;
                footballer.LastName = footballer_input.LastName;
                footballer.Gender = footballer_input.Gender == "male";
                footballer.DateOfBirth = DateTime.Parse(footballer_input.DateOfBirth.ToString());
                footballer.TeamName = footballer_input.TeamName;
                footballer.Country = footballer_input.Country;
                context.SaveChanges();
            }
            return;
        }

        public void DeleteItem(ISportsmenInfoInputModel input)
        {
            var footballer_input = input as FootballersModel.InputModel;
            var footballer = context.Footballers.FirstOrDefaultAsync(f => f.Id == footballer_input.ID);
            if (footballer != null)
            {
                context.Footballers.Remove(footballer.Result);
                context.SaveChanges();
            }
        }
    }
}
