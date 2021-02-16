using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using tthk_kinoteater.Enums;
using tthk_kinoteater.Models;

namespace tthk_kinoteater
{
    class DataHandler
    {
        private readonly SqlConnection connection = new SqlConnection(
            @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =|DataDirectory|\AppData\cinema.mdf; Integrated Security = True");
        private SqlCommand command;

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

        public Movie GetMovie(int id)
        {
            command = new SqlCommand("SELECT * FROM Movies WHERE Id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            Movie movie = new Movie();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    movie.Id = id;
                    movie.Title = reader["Title"].ToString();
                    movie.Year = Convert.ToInt32(reader["Email"].ToString());
                    movie.Director = reader["Director"].ToString();
                    movie.Duration = TimeSpan.FromMinutes(Convert.ToInt32(reader["Duration"].ToString()));
                }
            }
            TryToCloseConnection();
            return movie;
        }

        public Hall GetHall(int id)
        {
            command = new SqlCommand("SELECT * FROM Halls", connection);
            var hall = new Hall();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    hall.Id = id;
                    hall.Title = reader["Title"].ToString();
                    hall.NumberOfPlaces = ConvertPlacesNumber(Convert.ToInt32(reader["HallSize"].ToString()));
                }
            }
            TryToCloseConnection();
            return hall;
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
            List<Session> sessions = new List<Session>();
            command = new SqlCommand("SELECT * FROM Sessions;", connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Session session = new Session
                    {
                        Id = Convert.ToInt32(reader["Id"].ToString()),
                        Movie = GetMovie(Convert.ToInt32(reader["Id"].ToString())),
                        StartTime = Convert.ToDateTime(reader["StartTime"].ToString())
                    };
                    sessions.Add(session);
                }
            }
            TryToCloseConnection();
            return sessions;
        }

        public List<Movie> GetMovies()
        {
            List<Movie> movies = new List<Movie>();
            command = new SqlCommand("SELECT * FROM Movies;", connection);
            using (var reader = command.ExecuteReader())
            {
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
            }
            TryToCloseConnection();
            return movies;
        }
    }
}
