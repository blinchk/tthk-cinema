using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public Movie GetMovie(int id)
        {
            command = new SqlCommand("SELECT * FROM Movies WHERE Id = @id");
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
            command = new SqlCommand("SELECT * FROM Halls");
            Hall hall = new Hall();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    hall.Id = id;
                    hall.Title = reader["Title"].ToString();
                }
            }
            TryToCloseConnection();
            return hall;
        }

        public List<Place> GetPlaces(Hall hall)
        {
            List<Place> places = new List<Place>();
            command = new SqlCommand("SELECT * FROM Places WHERE Hall = @id");
            command.Parameters.AddWithValue("@id", hall.Id);
            Place place = new Place();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    place.Id = Convert.ToInt32(reader["Id"].ToString());
                    place.Number = Convert.ToInt32(reader["Number"].ToString());
                    place.Row = Convert.ToInt32(reader["Row"].ToString());
                    places.Add(place);
                }
            }
            TryToCloseConnection();
            return places;
        }

        public List<Session> GetSessions()
        {
            List<Session> sessions = new List<Session>();
            command = new SqlCommand("SELECT * FROM Sessions;");
            Session session = new Session();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    session.Id = Convert.ToInt32(reader["Id"].ToString());
                    session.Movie = GetMovie(Convert.ToInt32(reader["Id"].ToString()));
                    session.StartTime = Convert.ToDateTime(reader["StartTime"].ToString());
                    sessions.Add(session);
                }
            }
            TryToCloseConnection();
            return sessions;
        }

        public List<Movie> GetMovies()
        {
            List<Movie> movies = new List<Movie>();
            command = new SqlCommand("SELECT * FROM Movies;");
            Movie movie = new Movie();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    movie.Id = Convert.ToInt32(reader["Id"].ToString());
                    movie.Title = reader["Title"].ToString();
                    movie.Year = Convert.ToInt32(reader["Email"].ToString());
                    movie.Director = reader["Director"].ToString();
                    movie.Duration = TimeSpan.FromMinutes(Convert.ToInt32(reader["Duration"].ToString()));
                    movies.Add(movie);
                }
            }
            TryToCloseConnection();
            return movies;
        }
    }
}
