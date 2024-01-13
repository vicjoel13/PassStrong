using Microsoft.AspNetCore.Mvc;
using PassStrong.Models;
using PasswordGenerator;
using System.Diagnostics;

namespace PassStrong.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

		public IActionResult Results(PassModelCreation pass)
		{
			var watch = System.Diagnostics.Stopwatch.StartNew();
			var pwd = new Password(includeLowercase: pass.includeLowercase, includeUppercase: pass.includeUppercase, includeNumeric: pass.includeNumeric, includeSpecial: pass.includeSpeciaL, passwordLength: pass.passWordLenght);
			var password = pwd.Next();
			var result = Zxcvbn.Core.EvaluatePassword(password);
			
            var resultPassword = new PassResults();
            resultPassword.CrackResults = result;
            resultPassword.PassWord = password;
            resultPassword.Complexity = result.Score;
			watch.Stop();
			var elapsedMs = watch.ElapsedMilliseconds;
            resultPassword.Time = elapsedMs.ToString();
			return View(resultPassword);
		}



		public IActionResult Privacy()
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