﻿using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Serializer;
using InitialProject.Validations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace InitialProject.Domain.Model
{
    public class TourRequest : ValidationBase, ISerializable
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public int IdLocation { get; set; }
        public RequestType Status { get; set; }
        public List<Image> Images { get; set; }

        private string _tourName;
        public string TourName
        {
            get => _tourName;
            set
            {
                if (value != _tourName)
                {
                    _tourName = value;
                    OnPropertyChanged(nameof(TourName));
                }
            }
        }


        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private string _language;
        public string TourLanguage
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged(nameof(TourLanguage));
                }
            }
        }

        private int _guestNum;
        public int GuestNum
        {
            get => _guestNum;
            set
            {
                if (value != _guestNum)
                {
                    _guestNum = value;
                    OnPropertyChanged(nameof(GuestNum));
                }
            }
        }

        private DateOnly startDate { get; set; }
        public DateOnly StartDate
        {
            get => startDate;
            set
            {
                if (value != startDate)
                {
                    startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }

        private DateOnly endDate { get; set; }
        public DateOnly EndDate
        {
            get => endDate;
            set
            {
                if (value != endDate)
                {
                    endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }

        
        public TourRequest ()
        {
            Images = new List<Image>();
        }

        public TourRequest(string name, Location location, string language, int guestNum, DateOnly startDate, DateOnly endDate, int idLocation, string description)
        {
            TourName = name;
            Location = location;
            TourLanguage = language;
            GuestNum = guestNum;
            StartDate = startDate;
            EndDate = endDate;
            IdLocation = idLocation;
            Description=description;
            Status = RequestType.OnHold;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourName,
                Location.City,
                Location.Country,
                TourLanguage,
                GuestNum.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                IdLocation.ToString(),
                Description,
                Status.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourName = values[1];
            Location = new Location(values[2], values[3]);
            TourLanguage = values[4];
            GuestNum = int.Parse(values[5]);
            StartDate = DateOnly.Parse(values[6]);
            EndDate = DateOnly.Parse(values[7]);
            IdLocation = int.Parse(values[8]);
            Description = values[9];
            Status = (RequestType)Enum.Parse(typeof(RequestType), values[10]);
        }

        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(this._tourName))
            {
                this.ValidationErrors["TourName"] = "TourName cannot be empty.";
            }
            if (string.IsNullOrWhiteSpace(this._language))
            {
                this.ValidationErrors["TourLanguage"] = "TourLanguage cannot be empty.";
            }
            if (this._guestNum == 0) 
            {
                this.ValidationErrors["GuestNum"] = "GuestNum cannot be empty.";
            }
            if (string.IsNullOrWhiteSpace(this._description))
            {
                this.ValidationErrors["Description"] = "Description cannot be empty.";
            }
            if (startDate == default(DateOnly))
            {
                this.ValidationErrors["StartDate"] = "StartDate is requied.";
            }
            if (endDate ==  default(DateOnly))
            {
                this.ValidationErrors["EndDate"] = "EndDate is requied.";
            }

            if (StartDate >= EndDate)
            {
                this.ValidationErrors["StartDate"] = "Start must be before end.";
                this.ValidationErrors["EndDate"] = "End must be after start.";
            }
        }
    }
}