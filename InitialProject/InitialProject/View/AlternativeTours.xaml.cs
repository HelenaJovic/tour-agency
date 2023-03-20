﻿using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AlternativeTours.xaml
    /// </summary>
    public partial class AlternativeTours : Window
    {
        public static ObservableCollection<Tour> Tours { get; set; }
        public static ObservableCollection<Tour> AlternativeToursMainList { get; set; }
        public static ObservableCollection<Tour> AlternativeToursCopyList { get; set; }
        public User LoggedInUser { get; set; }
        public Tour SelectedTour { get; set; }
        public TourReservation SelectedTourReservation { get; set; }
        public Tour SelectedAlternativeTour { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        private readonly TourRepository _tourRepository;
        private readonly TourReservationRepository _tourReservationRepository;
        private string AgainGuestNum { get; set; }
        public AlternativeTours(User user, Tour tour, TourReservation tourReservation, string againGuestNum, Tour alternativeTour)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            SelectedTour = tour;
            SelectedTourReservation = tourReservation;
            AgainGuestNum = againGuestNum;
            SelectedAlternativeTour = alternativeTour;
            _tourRepository = new TourRepository();
            _tourReservationRepository = new TourReservationRepository();
            Tours = new ObservableCollection<Tour>(_tourRepository.GetByUser(user));
            AlternativeToursMainList = new ObservableCollection<Tour>();
            AlternativeToursCopyList = new ObservableCollection<Tour>(_tourRepository.GetAll());
            Locations = new ObservableCollection<Location>();

            foreach (Tour tours in AlternativeToursCopyList)
            {
                if (SelectedTourReservation != null)
                {
                    Location location = _tourRepository.GetLocationById(SelectedTourReservation.Id);
                    if (location.Country == tours.Location.Country && location.City == tours.Location.City && int.Parse(AgainGuestNum) <= tours.MaxGuestNum)
                    {
                        AlternativeToursMainList.Add(tours);
                    }
                }
                else
                {
                    if (SelectedTour.Location.Country == tours.Location.Country && SelectedTour.Location.City == tours.Location.City && int.Parse(AgainGuestNum) <= tours.MaxGuestNum)
                    {
                        AlternativeToursMainList.Add(tours);
                    }
                }

            }

            AlternativeToursCopyList.Clear();

            foreach (Tour t in AlternativeToursMainList)
            {
                AlternativeToursCopyList.Add(t);
            }
        }
        private void Button_Click_ResrveAlternative(object sender, RoutedEventArgs e)
        {
            if (Tab.SelectedIndex == 0)
            {
                if (SelectedAlternativeTour != null)
                {
                    ReserveAlternativeTour();
                }
                else
                {
                    MessageBox.Show("Choose a tour which you can reserve");
                }
            }
            Close();
        }

        private void ReserveAlternativeTour()
        {
            if (SelectedAlternativeTour.FreeSetsNum - int.Parse(AgainGuestNum) >= 0 || AgainGuestNum.Equals(""))
            {
                SelectedAlternativeTour.FreeSetsNum -= int.Parse(AgainGuestNum);
                string TourName = _tourRepository.GetTourNameById(SelectedAlternativeTour.Id);
                TourReservation newAlternativeTour = new TourReservation(SelectedAlternativeTour.Id, TourName, LoggedInUser.Id, int.Parse(AgainGuestNum), SelectedAlternativeTour.FreeSetsNum, -1, LoggedInUser.Username);
                TourReservation savedAlternativeTour = _tourReservationRepository.Save(newAlternativeTour);
                Guest2MainWindow.ReservedTours.Add(savedAlternativeTour);
            }
        }

        private void Button_Click_FiltersAlternative(object sender, RoutedEventArgs e)
        {
            AlternativeTourFiltering filterAlternativeTour = new AlternativeTourFiltering();
            filterAlternativeTour.Show();
        }

        private void Button_Click_RestartAlternative(object sender, RoutedEventArgs e)
        {
            AlternativeToursMainList.Clear();
            foreach (Tour t in AlternativeToursCopyList)
            {
                AlternativeToursMainList.Add(t);
            }
        }

        private void Button_Click_ViewTourGallery(object sender, RoutedEventArgs e)
        {
            ViewTourGallery viewTourGallery = new ViewTourGallery(SelectedTour);
            viewTourGallery.Show();
        }
    }
}