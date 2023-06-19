﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Index()
        {
            return View(_context.Genres.Include(x=>x.Books).ToList());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View();    
            }

         

            _context.Genres.Add(genre);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}


