using _11112023ClassWork.Models;
using _11112023ClassWork.Models.Home;
using _11112023ClassWork.Services.Hash;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace _11112023ClassWork.Controllers
{
    public class HomeController : Controller
    {
        //залежність (від служби) заявляється як private readonly поле
        private readonly IHashService _hashService;   //DIP - тип залежності - це інтерфейс
        private readonly IValidationService _validationService;
        private readonly ILogger<HomeController> _logger;

        //конструктор зазначає необхідні залежності, їх передає Resolver (Injector)
        public HomeController(ILogger<HomeController> logger, IHashService hashService, IValidationService validationService)
        {
            _logger = logger;
            _hashService = hashService;
            _validationService = validationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Razor()
        {
            ViewData["formController"] = "Hello from Controler";
            return View();
        }

        public IActionResult Transfer()
        {
            // модель у параметрах автоматично збирається з даних, що
            // передаються у запиті.
            TransferFormModel? formModel;
            if (HttpContext.Session.Keys.Contains("formModel"))
            {
                // є збережені дані - відновлюємо їх та видаляємо з сесії
                formModel = JsonSerializer.Deserialize<TransferFormModel>(
                    HttpContext.Session.GetString("formModel")!
                );
                HttpContext.Session.Remove("formModel");
            }
            else formModel = null;
            TransferViewModel model = new()
            {
                Date = DateOnly.FromDateTime(DateTime.Today),
                Time = TimeOnly.FromDateTime(DateTime.Now),
                ControllerName = this.GetType().Name,
                FormModel = formModel
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult ProcessTransferForm(TransferFormModel? formModel) 
        {
            if(formModel != null)
            {//зберігаємо у сесії серіалізований об'єкт formModel
                HttpContext.Session.SetString(
                    "formModel", JsonSerializer.Serialize(formModel)
                    );
            }

            return RedirectToAction(nameof(Transfer));
        }
        public IActionResult Homework18112023()
        {
            UserViewModel viewModel;

            if (HttpContext.Session.Keys.Contains("formModel"))
            {
                var formModel = JsonSerializer.Deserialize<UserModel>(
                    HttpContext.Session.GetString("formModel")!
                );
                HttpContext.Session.Remove("formModel");

                viewModel = new UserViewModel
                {
                    FormModel = formModel,
                    ValidationResult = formModel.ValidationResult ?? new ValidationResultModel()
                };
            }
            else
            {
                viewModel = new UserViewModel
                {
                    FormModel = new UserModel(),
                    ValidationResult = new ValidationResultModel()
                };
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ProcessHomework18112023Form(UserModel? formModel)
        {
            if (formModel != null)
            {
                var validationResult = new ValidationResultModel
                {
                    IsNameValid = _validationService.IsNameValid(formModel.Name),
                    IsPhoneValid = _validationService.IsPhoneValid(formModel.Phone),
                    IsEmailValid = _validationService.IsEmailValid(formModel.Email)
                };

                formModel.ValidationResult = validationResult;

                // Вывод отладочной информации
                Console.WriteLine($"IsNameValid: {validationResult.IsNameValid}");
                Console.WriteLine($"IsPhoneValid: {validationResult.IsPhoneValid}");
                Console.WriteLine($"IsEmailValid: {validationResult.IsEmailValid}");

                HttpContext.Session.SetString("formModel", JsonSerializer.Serialize(formModel));

                return RedirectToAction(nameof(Homework18112023), new UserViewModel
                {
                    FormModel = formModel,
                    ValidationResult = validationResult
                });
            }

            return View(nameof(Homework18112023), new UserViewModel
            {
                FormModel = new UserModel(),
                ValidationResult = new ValidationResultModel()
            });
        }

        public IActionResult Homework11112023()
        {
            return View();
        }
        public IActionResult Task1()
        {
            Task1ViewModel model = new()
            {
                DayOfYear = DateTime.Today.DayOfYear,
            };
            return View(model);
        }
        public IActionResult Task2()
        {
            //Generation of random letter
            char randomLetter = (char)('A' + new Random().Next(26));
            Task2ViewModel model = new()
            {
                RandomLetter = randomLetter,
            };
            return View(model);
        }
        public IActionResult Task3()
        {
            Task3ViewModel model = new()
            {
                RestaurantName = "Nikos Greek Bistro",
                Address = "Grecka Street, 32",
                PhoneNumber = "067 302-32-32",
                Schedule = "11:00 - 22:00",
            };
            return View(model);
        }
        public IActionResult Task4()
        {
            List<Task3ViewModel> restaurantList = new List<Task3ViewModel>
            {
                new Task3ViewModel 
                {
                    RestaurantName = "Nikos Greek Bistro",
                    Address = "Grecka Street, 32",
                    PhoneNumber = "067 302-32-32",
                    Schedule = "11:00 - 22:00",
                },
                new Task3ViewModel
                {
                    RestaurantName = "Monica Pinza Pasta Bar",
                    Address = "Ekaterininskaya Street, 23",
                    PhoneNumber = "097 985-20-50",
                    Schedule = "11:00 - 22:00",
                },
                new Task3ViewModel
                {
                    RestaurantName = "Vegano Hooligano",
                    Address = "Malaya Arnautskaya Street, 59",
                    PhoneNumber = "096 000-77-68",
                    Schedule = "10:00 - 20:00",
                },
                new Task3ViewModel
                {
                    RestaurantName = "Alternative",
                    Address = "Filatova Street, 24",
                    PhoneNumber = "063 114-33-28",
                    Schedule = "08:00 - 22:00",
                }
            };
            Task4ViewModel model = new()
            {
                Restaurants = restaurantList,
            };
            return View(model);
        }
        public IActionResult Task5()
        {
            List<CountryModel> countries = new List<CountryModel>
            {
                new CountryModel
                {
                    Name = "Ukraine",
                    Capital = "Kiev",
                    Population = 44000000, 
                },
                new CountryModel
                {
                    Name = "Czech Republic",
                    Capital = "Prague",
                    Population = 10800000,
                },
                new CountryModel
                {
                    Name = "Germany",
                    Capital = "Berlin",
                    Population = 84400000,
                },
                new CountryModel
                {
                    Name = "Switzerland",
                    Capital = "Bern",
                    Population = 8900000,
                }
            };
            Task5ViewModel model = new()
            {
                Countries = countries,
            };
            return View(model);
        }

        public ViewResult Ioc()
        {
            //використовуємо сервіс
            ViewData["hash"] = _hashService.HexString("123");
            ViewData["objHash"] = _hashService.GetHashCode();
            return View();
        }

        public ViewResult Db()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}