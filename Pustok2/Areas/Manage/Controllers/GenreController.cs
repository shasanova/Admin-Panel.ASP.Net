﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok2.Areas.Manage.ViewModels;
using Pustok2.DAL;
using Pustok2.Entities;

namespace Pustok2.Areas.Manage.Controllers
{
    [Area("manage")]

    public class GenreController : Controller
    {
        private readonly Pustok2DbContext _context;

        public GenreController(Pustok2DbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page=1)
        {

            var vm = new PaginatedList<Genre> {

                Items = _context.Genres.Include(x=>x.Books).Skip((page - 1) * 2).Take(2).ToList(),

                PageIndex = page,

                TotalPages = (int)Math.Ceiling(_context.Genres.Count()/2d)


            };


            return View();
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if (!ModelState.IsValid)
                return View();

            if (_context.Genres.Any(x => x.GenreName == genre.GenreName))
            {
                ModelState.AddModelError("GenreName", "Name is already taken");
                return View();
            }

            _context.Genres.Add(genre);

            _context.SaveChanges();

            return RedirectToAction("index");
        }




        public IActionResult Edit(int id)
        {
            Genre genre = _context.Genres.FirstOrDefault(x => x.Id == id);

            if (genre == null) return View("error");

            return View(genre);    
        }


        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Genre existGenre = _context.Genres.FirstOrDefault(x => x.Id == genre.Id);

            if (existGenre == null) return View("error");


            if (genre.GenreName != existGenre.GenreName && _context.Genres.Any(x => x.GenreName == genre.GenreName))
            {
                ModelState.AddModelError("GenreName", "Name is already taken");
                return View();
            }


            existGenre.GenreName = genre.GenreName;

            _context.SaveChanges();

            return RedirectToAction("index");
        }

    }
}


