using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballerApp.Model.Interfaces
{
    public interface ISportsHandler
    {
        public IEnumerable<SportsmanItem> GetAllItems();

        public IEnumerable<string> GetTeams();

        public IEnumerable<string> GetCountries();

        public void AddItem(ICreateFormInputModel input);

        public void ChangeItem(ISportsmenInfoInputModel input);

        public void DeleteItem(ISportsmenInfoInputModel input);
    }
}
