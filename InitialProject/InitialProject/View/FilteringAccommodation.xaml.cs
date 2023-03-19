﻿using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for FilteringAccommodation.xaml
    /// </summary>
    public partial class FilteringAccommodation : Window
    {
       
        public Accommodation SelectedAccommodation { get; set; }
        public User LoggedInUser { get; set; }
        public static ObservableCollection<String> Countries { get; set; }
        public static ObservableCollection<String> Cities { get; set; }
      //  public static ObservableCollection<AccommodationType> Types { get; set; }

        public static String SelectedCountry { get; set; }
        public static String SelectedCity { get; set; }

        public static AccommodationType SelectedType { get; set; }

        // private readonly AccommodationRepository _accommodationRepository;
        private readonly LocationRepository _locationRepository;

        private string _accommodationType;
        public string AccommType
        {
            get => _accommodationType;
            set
            {
                if (value != _accommodationType)
                {
                    _accommodationType = value;
                    OnPropertyChanged();
                }
            }
        }


        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }



        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public FilteringAccommodation()
        {
            InitializeComponent();
            //accommodations = new List<Accommodation>();
            _locationRepository = new LocationRepository();
            DataContext = this;
            Countries = new ObservableCollection<String>(_locationRepository.GetAllCountries());
         /*   Types = new ObservableCollection<AccommodationType>()
            {   
                AccommodationType.Apartment,
                AccommodationType.House,
                AccommodationType.Cottage
            };
            ComboboxType.SelectedIndex = -1;*/
        }
        

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            Guest1MainWindow.AccommodationsMainList.Clear();
            /* foreach (Accommodation a in Guest1MainWindow.AccommodationsCopyList)
             {
                 Guest1MainWindow.AccommodationsMainList.Add(a);
             }*/

            /* String Name = txtName.Text
             String[] splittedLocation= txtLocation.Text.Split(",");
             String[] splittedType = txtType.Text.Split(",");
             String[] splittedGuestNum = txtGuestNum.Text.Split(",");
             String[] splittedReservationNum = txtReservationNum.Text.Split(",");*/

            /*List<int> indexesToDrop = new List<int>();
            List<Accommodation> filteredAccommodations = new List<Accommodation>();*/

            
            int max=0;
            int min=0;
            if (!(int.TryParse(txtGuestNum.Text, out max) || (txtGuestNum.Text.Equals(""))) || !(int.TryParse(txtReservationNum.Text, out min) || (txtReservationNum.Text.Equals(""))))
            {
                return;
            }
            foreach (Accommodation a in Guest1MainWindow.AccommodationsCopyList)
            {

                Location location = _locationRepository.GetById(a.IdLocation);
                //AccommodationType selectedType = (AccommodationType)ComboboxType.SelectedItem;
               // AccommodationType type=(AccommodationType)Enum.Parse(typeof(AccommType));
                if (a.Name.ToLower().Contains(txtName.Text.ToLower()) && (location.Country == SelectedCountry ||  SelectedCountry == null) && (location.City==SelectedCity || SelectedCity == null) && (a.Type.ToString().Equals(((ComboBoxItem)ComboboxType.SelectedItem).Content.ToString()) || ComboboxType.SelectedItem == null)

&&
(a.MaxGuestNum - max >= 0 || txtGuestNum.Text.Equals("")) && (a.MinReservationDays -min <=0 || txtReservationNum.Text.Equals("")))
                {
                    a.Location = _locationRepository.GetById(a.IdLocation);
                    Guest1MainWindow.AccommodationsMainList.Add(a);
                }

            }


        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            Country = ComboBoxCountry.SelectedItem.ToString();
            Cities = new ObservableCollection<String>(_locationRepository.GetCities(Country));

            ComboboxCity.ItemsSource = Cities;
            ComboboxCity.SelectedIndex = 0;
            ComboboxCity.IsEnabled = true;
        }

        private void ComboboxCity_DropDownClosed(object sender, EventArgs e)
        {
            City = ComboboxCity.SelectedItem.ToString();
        }

        private void ComboboxType_DropDownClosed(object sender, EventArgs e)
        {

        }
    }
}
