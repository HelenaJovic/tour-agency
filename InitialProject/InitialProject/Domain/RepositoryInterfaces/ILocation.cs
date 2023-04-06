﻿using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
	internal interface ILocation : IRepository<Location>
	{
		bool IsSaved(Location location);

		Location GetByCity(string city);

		Location FindLocation(String Country, String City);

		List<String> GetAllCountries();

		List<String> GetCities(String Country);
	}
}
