using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using FootballerApp.Model.Interfaces;
using FootballerApp.Model;


namespace FootballerApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public IEnumerable<SelectListItem> Teams { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }

        private ISportsHandler dataHandler;

        public IndexModel(ISportsHandler handler)
        {
            dataHandler = handler;
            Countries = dataHandler.GetCountries()
                .Select(g => new SelectListItem(g, g));
        }

        public IActionResult OnGet()
        {
            var teams = dataHandler.GetTeams()
                .Select(t => new SelectListItem(t, t)).ToList();
            teams.Add(new SelectListItem("Добавить команду", ""));
            Teams = teams;
            return Page();
        }


        public IActionResult OnPost(InputModel input)
        {
            if (!ModelState.IsValid)
            {
                var teams = dataHandler.GetTeams()
                    .Select(t => new SelectListItem(t, t)).ToList();
                teams.Add(new SelectListItem("Добавить команду", ""));
                Teams = teams;
                return Page();
            }

            dataHandler.AddItem(input);
            return RedirectToPage("footballers");
        }

        public class DateValidation : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is null)
                    return new ValidationResult(ErrorMessage = "Необходимо указать дату рождения");
                if (DateTime.TryParse(value.ToString(), out var date))
                {
                    if (date > DateTime.Now)
                        return new ValidationResult(ErrorMessage = "Дата рождения не может быть позже текущей");
                    else if (date < DateTime.Parse("1900-01-01 00:00:00"))
                        return new ValidationResult(ErrorMessage = "Дата рождения должна быть позже 1850г."); 
                    else
                        return ValidationResult.Success;
                }
                return new ValidationResult(ErrorMessage="Неверный формат даты");
            }
        }
        public class InputModel: ICreateFormInputModel
        {
            [Required(ErrorMessage = "Необходимо указать имя")]
            [StringLength(255, ErrorMessage = "Максимальная длина имени = {1}")]
            [Display(Name = "Имя")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "Необходимо указать фамилию")]
            [StringLength(255, ErrorMessage = "Максимальная длина фамилии = {1}")]
            [Display(Name = "Фамилия")]
            public string LastName { get; set; }

            [Required(ErrorMessage = "Необходисо указать пол")]
            [Display(Name = "Пол")]
            public string Gender { get; set; }


            [DataType(DataType.Date)]
            [Required(ErrorMessage = "Необходимо указать дату рождения")]
            [DateValidation]
            [Display(Name = "Дата рождения")]
            public DateTime? DateOfBirth { get; set; }


            [Required(ErrorMessage = "Необходимо указать название команды")]
            [Display(Name = "Название команды")]
            public string TeamName { get; set; }

            [Display(Name = "Страна")]
            public string Country { get; set; }
        }
    }
}
