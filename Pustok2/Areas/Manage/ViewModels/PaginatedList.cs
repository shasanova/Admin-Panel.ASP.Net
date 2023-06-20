﻿using System;
namespace Pustok2.Areas.Manage.ViewModels
{
	public class PaginatedList<T>
	{
		public List<T> Items { get; set; }

        public int PageIndex { get; set; }

        public int TotalPages { get; set; }


    }
}

