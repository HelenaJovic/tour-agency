﻿using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Image : ISerializable
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int IdAccommodation { get; set; }
        public int IdTour { get; set; }

        public Image() { }
        public Image(string url, int idAccommodation, int idTour) { 
            Url= url;
            IdAccommodation = idAccommodation;
            IdTour= idTour;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Url,
                IdAccommodation.ToString(),
                IdTour.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Url = values[1];
            IdAccommodation =int.Parse(values[2]);
            IdTour= int.Parse(values[3]);
        }
    }
}
