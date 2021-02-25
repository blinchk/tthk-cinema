using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using tthk_kinoteater.Enums;
using tthk_kinoteater.Models;

namespace tthk_kinoteater
{
    class DataHandler
    {
        private readonly SqlConnection connection = new(
            @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =|DataDirectory|\AppData\cinema.mdf; Integrated Security = True; MultipleActiveResultSets=true;");
        public DataHandler()
        {
            connection.Open();
        }

        private void TryToCloseConnection()
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
        }

        private HallSize ConvertPlacesNumber(int size)
        {
            switch(size)
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
                halls.Add(hall);
            }
            return halls;
        }

        public List<Place> GetPlaces(Hall hall)
        {
            List<Place> places = new List<Place>();
            int numberOfPlaces = Convert.ToInt32(hall.NumberOfPlaces);
            for (int row = 1; row <= numberOfPlaces/10; row++)
            {
                for (int column = 1; column <= 10; column++)
                {
                    places.Add(new Place() { 
                        Hall = hall,
                        Number = column,
                        Row = row
                    });
                }
            }
            return places;
        }

        public List<Session> GetSessions()
        {
            var dataHandler = new DataHandler();
            var movies = dataHandler.GetMovies();
            var halls = dataHandler.GetHalls();
            List<Session> sessions = new List<Session>();
            var command = new SqlCommand("SELECT * FROM Sessions", connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var movie = Convert.ToInt32(reader["Movie"].ToString());
                var hall = Convert.ToInt32(reader["Hall"].ToString());
                Session session = new Session
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Movie = movies.First(m => m.Id == movie),
                    Hall = halls.First(h => h.Id == hall)
                };
                sessions.Add(session);
            }
            return sessions;
        }

        public List<Movie> GetMovies()
        {
            List<Movie> movies = new List<Movie>();
            var command = new SqlCommand("SELECT * FROM Movies;", connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Movie movie = new Movie
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
    }
}
