﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using tthk_kinoteater.Enums;
using tthk_kinoteater.Models;

namespace tthk_kinoteater
{
    internal class DataHandler
    {
        private readonly SqlConnection connection = new(
            @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =|DataDirectory|\AppData\cinema.mdf; Integrated Security = True; MultipleActiveResultSets=true;");

        public DataHandler()
        {
            connection.Open();
        }

        private HallSize ConvertPlacesNumber(int size)
        {
            switch (size)
            {
                case 1:
                    return HallSize.Small;
                case 2:
                    return HallSize.Medium;
                case 3:
                    return HallSize.Big;
                default:
                    return HallSize.Small;
            }
        }

        public List<Hall> GetHalls()
        {
            var command = new SqlCommand("SELECT * FROM Halls", connection);
            var halls = new List<Hall>();
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var hall = new Hall
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Title = reader["Title"].ToString(),
                    NumberOfPlaces = ConvertPlacesNumber(Convert.ToInt32(reader["HallSize"].ToString()))
                };
                hall.Places = GetPlaces(hall);
                halls.Add(hall);
            }

            return halls;
        }

        public List<Place> GetPlaces(Hall hall)
        {
            const int placesInRow = 12;
            var places = new List<Place>();
            var numberOfPlaces = Convert.ToInt32(hall.NumberOfPlaces);
            for (var row = 1; row <= numberOfPlaces / placesInRow; row++)
            for (var column = 1; column <= placesInRow; column++)
                places.Add(new Place
                {
                    Hall = hall,
                    Number = column,
                    Row = row
                });
            return places;
        }

        public List<Session> GetSessions()
        {
            var dataHandler = new DataHandler();
            var movies = dataHandler.GetMovies();
            var halls = dataHandler.GetHalls();
            var sessions = new List<Session>();
            var command = new SqlCommand("SELECT * FROM Sessions", connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var movie = Convert.ToInt32(reader["Movie"].ToString());
                var hall = Convert.ToInt32(reader["Hall"].ToString());
                var session = new Session
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Movie = movies.First(m => m.Id == movie),
                    Hall = halls.First(h => h.Id == hall),
                    StartTime = Convert.ToDateTime(reader["StartTime"].ToString())
                };
                sessions.Add(session);
            }

            return sessions;
        }

        public List<Movie> GetMovies()
        {
            var movies = new List<Movie>();
            var command = new SqlCommand("SELECT * FROM Movies;", connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var movie = new Movie
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Title = reader["Title"].ToString(),
                    Year = Convert.ToInt32(reader["Year"].ToString()),
                    Director = reader["Director"].ToString(),
                    Duration = TimeSpan.FromMinutes(Convert.ToInt32(reader["Duration"].ToString()))
                };
                movies.Add(movie);
            }

            return movies;
        }

        public List<Ticket> GetTickets(Session session)
        {
            var tickets = new List<Ticket>();
            var command = new SqlCommand("SELECT * FROM Tickets WHERE Session = @session;", connection);
            command.Parameters.AddWithValue("@session", session.Id);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var ticket = new Ticket
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Number = Convert.ToInt32(reader["Number"].ToString()),
                    Row = Convert.ToInt32(reader["Row"].ToString())
                };
                tickets.Add(ticket);
            }

            return tickets;
        }

        public void AddTicket(Ticket ticket, Session session)
        {
            var command = new SqlCommand("INSERT INTO Tickets(Number, Row, Session) VALUES (@number, @row, @session);",
                connection);
            command.Parameters.AddWithValue("@number", ticket.Number);
            command.Parameters.AddWithValue("@row", ticket.Row);
            command.Parameters.AddWithValue("@session", session.Id);
            command.ExecuteNonQuery();
        }
    }
}