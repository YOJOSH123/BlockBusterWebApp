﻿using BlockBuster.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlockBuster
{
    public class BlockBusterBasicFunctions
    {
        public static Movie GetMovieById(int id)
        {
            using(var db = new SE407_BlockBusterContext())
            {
                return db.Movies.Find(id);
            }
        }

        public static List<Movie> GetAllMovies()
        {
            using(var db = new SE407_BlockBusterContext())
            {
                return db.Movies.ToList();
            }
        }

        public static List<Movie> GetAllCheckedOutMovies()
        {
            using(var db = new SE407_BlockBusterContext())
            {
                return db.Movies
                    .Join(db.Transactions,
                    m => m.MovieId,
                    t => t.Movie.MovieId,
                    (m, t) => new
                    {
                        MovieId = m.MovieId,
                        Title = m.Title,
                        ReleaseYear = m.ReleaseYear,
                        GenreId = m.GenreId,
                        DirectorId = m.DirectorId,
                        CheckedIn = t.CheckedIn
                    }).Where(w => w.CheckedIn == "N")
                    .Select(m => new Movie
                    {
                        MovieId = m.MovieId,
                        Title = m.Title,
                        ReleaseYear = m.ReleaseYear,
                        GenreId = m.GenreId,
                        DirectorId = m.DirectorId
                    }).ToList();
            }
        }

        public static List<Movie> GetMoviesByGenreDescription(int genreId)
        {
            using (var db = new SE407_BlockBusterContext())
            {
                return db.Movies
                    .Join(db.Genres,
                    m => m.Genre.GenreId,
                    t => t.GenreId,
                    (m, t) => new
                    {
                        MovieId = m.MovieId,
                        Title = m.Title,
                        ReleaseYear = m.ReleaseYear,
                        GenreId = m.GenreId,
                        DirectorId = m.DirectorId,
                    }).Where(w => w.GenreId == genreId)
                    .Select(m => new Movie
                    {
                        MovieId = m.MovieId,
                        Title = m.Title,
                        ReleaseYear = m.ReleaseYear,
                        GenreId = m.GenreId,
                        DirectorId = m.DirectorId
                    }).ToList();
            }
        }

        public static List<Movie> GetMoviesByDirectorLastName(String director)
        {
            using (var db = new SE407_BlockBusterContext())
            {
                return db.Movies
                    .Join(db.Directors,
                    m => m.Director.DirectorId,
                    t => t.DirectorId,
                    (m, t) => new
                    {
                        MovieId = m.MovieId,
                        Title = m.Title,
                        ReleaseYear = m.ReleaseYear,
                        GenreId = m.GenreId,
                        DirectorId = m.DirectorId,
                        DirectorLast = m.Director.LastName,
                    }).Where(w => w.DirectorLast == director)
                    .Select(m => new Movie
                    {
                        MovieId = m.MovieId,
                        Title = m.Title,
                        ReleaseYear = m.ReleaseYear,
                        GenreId = m.GenreId,
                        DirectorId = m.DirectorId
                    }).ToList();
            }
        }

        public static List<Movie> GetAllMoviesFull()
        {
            using (var db = new SE407_BlockBusterContext())
            {
                var movies = db.Movies
                    .Include(movies => movies.Director)
                    .Include(movies => movies.Genre)
                    .ToList();
                return movies;
            }
        }

        public static Movie GetFullMovieById(int id)
        {
            using (var db = new SE407_BlockBusterContext())
            {
                var movie = db.Movies
                    .Include(m => m.Director)
                    .Include(m => m.Genre)
                    .Where(m => m.MovieId == id)
                    .FirstOrDefault();
                return movie;
            }
        }

        public static List<Genre> GetAllGenres()
        {
            using (var db = new SE407_BlockBusterContext())
            {
                return db.Genres.ToList();
            }
        }

        public static List<Director> GetAllDirectors()
        {
            using (var db = new SE407_BlockBusterContext())
            {
                return db.Directors.ToList();
            }
        }
    }
}
