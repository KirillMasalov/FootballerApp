using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using FootballerApp.Model.Interfaces;
using FootballerApp.Pages;
using FootballerApp.Model;
using FootballerApp.Model.Footballers;

namespace FootballerApp.Pages
{
    public class FootballersModel : PageModel
    {
        public InputModel Input { get; set; }

        public IEnumerable<SelectListItem> Teams { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
        public IEnumerable<SportsmanItem> Footballers { get; set; }
        public Dictionary<int, bool> ValidIDForm { get; set; }

        private ISportsHandler dataHandler;

        public FootballersModel(ISportsHandler handler)
        {
            dataHandler = handler;

            Countries = dataHandler.GetCountries().Select(c => new SelectListItem(c, c)).ToList();
        }
        public void OnGet()
        {
            var rawFootballers = dataHandler.GetAllItems();
            ValidIDForm = new Dictionary<int, bool>();
            foreach (var footballer in rawFootballers)
                ValidIDForm[footballer.Id] = true;
            Footballers = rawFootballers;
            var teams = dataHandler.GetTeams().Select(t => new SelectListItem(t, t)).ToList();

            teams.Add(new SelectListItem("�������� �������", ""));
            Teams = teams;
        }

        public IActionResult OnPost(InputModel input)
        {
            var id = input.ID;
            if (!ModelState.IsValid && !input.Delete)
            {
                ValidIDForm = new Dictionary<int, bool>();
                var rawFootballers = dataHandler.GetAllItems();
                foreach (var input_footballer in rawFootballers)
                    ValidIDForm[input_footballer.Id] = true;
                ValidIDForm[input.ID] = false;
                Footballers = rawFootballers;
                var teams = dataHandler.GetTeams().Select(t => new SelectListItem(t, t)).ToList();
                teams.Add(new SelectListItem("�������� �������", ""));
                Teams = teams;
                return Page();
            }

            if (input.Delete)
                dataHandler.DeleteItem(input);
            else
                dataHandler.ChangeItem(input);
;           return RedirectToPage("footballers");
        }

        public class InputModel: ISportsmenInfoInputModel
        {
            public bool Delete { get; set; }
            public int ID { get; set; }
            [Required(ErrorMessage = "���������� ������� ���")]
            [StringLength(255, ErrorMessage = "������������ ����� ����� = {1}")]
            [Display(Name = "���")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "���������� ������� �������")]
            [StringLength(255, ErrorMessage = "������������ ����� ������� = {1}")]
            [Display(Name = "�������")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "���������� ������� ���")]
            [Display(Name = "���")]
            public string Gender { get; set; }


            [DataType(DataType.Date)]
            [Required(ErrorMessage = "���������� ������� ���� ��������")]
            [IndexModel.DateValidation]
            [Display(Name = "���� ��������")]
            public DateTime? DateOfBirth { get; set; }


            [Required(ErrorMessage = "���������� ������� �������� �������")]
            [Display(Name = "�������� �������")]
            public string TeamName { get; set; }

            [Display(Name = "������")]
            public string Country { get; set; }
        }
    }
}
