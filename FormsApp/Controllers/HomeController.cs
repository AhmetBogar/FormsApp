﻿using FormsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FormsApp.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index(string searchString, string category)
		{
			var products = Repository.Products;

			if (!string.IsNullOrEmpty(searchString))
			{
				ViewBag.SearchString = searchString;
				products = products.Where(p => p.Name!.ToLower().Contains(searchString)).ToList();
			}

			if (!string.IsNullOrEmpty(category))
			{
				products = products.Where(p => p.CategoryId == int.Parse(category)).ToList();
			}

			//ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name",category);

			var model = new ProductViewModel
			{
				Products = products,
				Categories = Repository.Categories,
				SelectedCategory = category
			};
			return View(model);
		}


		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Product model, IFormFile imageFile)
		{
			var allowedExtensions = new[] { ".jpg", "jpeg", ".png" };
			var extension = Path.GetExtension(imageFile.FileName);
			var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);

			if (imageFile != null)
			{
				if (!allowedExtensions.Contains(extension))
				{
					ModelState.AddModelError("", "Geçerli bir resim seçiniz.");
				}
			}

			if (ModelState.IsValid)
			{
				if (imageFile != null)
				{
					using (var stream = new FileStream(path, FileMode.Create))
					{
						await imageFile.CopyToAsync(stream);
					}
				}

				model.Image = randomFileName;
				model.ProductId = Repository.Products.Count + 1;
				Repository.CreateProduct(model);
				return RedirectToAction("Index");
			}
			ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
			return View(model);
		}
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var entity = Repository.Products.FirstOrDefault(p => p.ProductId == id);
			if (entity == null)
			{
				return NotFound();
			}
			ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "Name");
			return View(entity);
		}
	}
}