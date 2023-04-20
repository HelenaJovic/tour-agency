﻿using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Repository;
using InitialProject.Serializer;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
    public class TourReservationService
    {
        private readonly ITourReservationRepository _tourReservationRepository;
        private readonly UserService _userService;

        public TourReservationService()
        {
            _tourReservationRepository = Inject.CreateInstance<ITourReservationRepository>();
            _userService = new UserService();

        }

        public List<TourReservation> GetByUser(User user)
        {
            return _tourReservationRepository.GetByUser(user);
        }

        public void Delete(TourReservation tourReservation)
        {
            _tourReservationRepository.Delete(tourReservation);
        }

        public void DeleteTour(Tour tour)
        {
            List<TourReservation> tourReservations = _tourReservationRepository.GetByTour(tour.Id);
            foreach(TourReservation tr in tourReservations)
            {
                _tourReservationRepository.Delete(tr);
            }
        }

        public List<User> GetUsersByTour(Tour tour)
        {
            List<User> users = new List<User>();
            User user = new User();
            foreach (TourReservation reservation in _tourReservationRepository.GetAll())
            {
                if (reservation.IdTour == tour.Id)
                {
                    user = _userService.GetById(reservation.IdUser);
                    users.Add(user);
                }
            }
            return users;
        }

        public List<TourReservation> GetByTour(int id)
        {
            return _tourReservationRepository.GetByTour(id);
        }
        public TourReservation Update(TourReservation tourReservation)
        {
            return _tourReservationRepository.Update(tourReservation);
        }
        public List<TourReservation> GetAll()
        {
            List<TourReservation> tourReservations = new List<TourReservation>();
            tourReservations = _tourReservationRepository.GetAll();
            return tourReservations;
        }

        public List<TourReservation> GetAllByUser(User user)
        {
            return _tourReservationRepository.GetByUser(user);
        }

        public TourReservation Save(TourReservation tourReservation)
        {
            return _tourReservationRepository.Save(tourReservation);
        }


    }
}
