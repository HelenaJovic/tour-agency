﻿using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class TheMostVisitedTourViewModel : ViewModelBase
    {
        public Tour TopTour { get; set; }
        public Tour TopYearTour { get; set; }

        private readonly TourAttendanceRepository _tourAttendanceRepository;
        private readonly TourService _tourService;
        public List<TourAttendance> ToursAttendances { get; set; }
        public List<Tour> Tours { get; set; }
        public static ObservableCollection<int> Years { get; set; }
        public TheMostVisitedTourViewModel(User user)
        {
            _tourAttendanceRepository = new TourAttendanceRepository();
            _tourService = new TourService();
            ToursAttendances = new List<TourAttendance>(_tourAttendanceRepository.GetAllByGuide(user));
            Tours = new List<Tour>(_tourService.GetAllByUser(user));
            TopTour = FindTopTour(Tours, ToursAttendances);
            Years = new ObservableCollection<int>(_tourService.GetAllYears(user));
            TopYearTour = TopTour; 
        }


        private String _selectedYear;
        public String SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    TopYearTour = FindTopYearTour(Tours, ToursAttendances, int.Parse(SelectedYear));
                    OnPropertyChanged(nameof(TopYearTour)); 
                }
            }
        }

        public Tour FindTopTour(List<Tour> Tours, List<TourAttendance> ToursAttendances)
        {
            int max = 0;
            int idTour = 0;
            int j = 0;

            foreach(Tour t in Tours)
            {
                foreach (TourAttendance ta in ToursAttendances)
                {
                    if (t.Id == ta.IdTour)
                    {
                        j++;
                    }
                }
                if(j>max) 
                {
                    max = j;
                    idTour = t.Id;
                    j = 0;
                }
            }

            return _tourService.GetById(idTour);
        }

        private Tour FindTopYearTour(List<Tour> tours, List<TourAttendance> toursAttendances, int year)
        {
            int max = 0;
            int idTour = 0;
            int j = 0;

            foreach (Tour t in Tours)
            {
                if(t.Date.Year== year)
                {
                    foreach (TourAttendance ta in ToursAttendances)
                    {
                        if (t.Id == ta.IdTour)
                        {
                            j++;
                        }
                    }
                    if (j > max)
                    {
                        max = j;
                        idTour = t.Id;
                        j = 0;
                    }
                }
            }

            return _tourService.GetById(idTour);
        }

    }
}
