using FormsApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FormsApp.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index(string searchString)
		{
			var products = Repository.Products;

			if (!string.IsNullOrEmpty(searchString))
			{
				ViewBag.SearchString=searchString;
				products=products.Where(p=>p.Name.ToLower().Contains(searchString)).ToList();
			}
			return View(products);
		}

		public IActionResult Privacy()
		{
			return View();
		}
	}
}