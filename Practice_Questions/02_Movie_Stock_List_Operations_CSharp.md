# Movie Stock – Academic Note

## Problem Statement

### 2. Movie Stock

### Functionalities

In the `Movie` class, implement the below-given properties.

---

## Movie Class – Properties

| Datatype | Property |
| -------- | -------- |
| string   | Title    |
| string   | Artist   |
| string   | Genre    |
| int      | Ratings  |

---

## Program Class – Requirements

In the `Program` class, a static list is already provided:

```csharp
public static List<Movie> MovieList;
```

Implement the following methods using `MovieList`.

---

## Method Specifications

### 1. AddMovie

```csharp
public void AddMovie(string MovieDetails)
```

**Description:**

* Adds movie details to `MovieList`.
* Accepts movie details as a **comma-separated string**.
* Converts the string into a `Movie` object.
* Stores the `Movie` object in `MovieList`.

---

### 2. ViewMoviesByGenre

```csharp
public List<Movie> ViewMoviesByGenre(string genre)
```

**Description:**

* Finds movies based on the given genre.
* If movies are available for the given genre:

  * Return the list of matching movies.
* If movies are not available:

  * Return an empty list.
  * Print **"No Movies found in genre '{genre}'"** in the `Main` method.

---

### 3. ViewMoviesByRatings

```csharp
public List<Movie> ViewMoviesByRatings()
```

**Description:**

* Sorts the movies based on ratings in **ascending order**.
* Returns the sorted list.

---

## Main Method Instructions

In the `Main` method:

1. Get the movie details from the user.
2. Call all the methods accordingly.
3. Display the output.
4. In the sample input/output, the **highlighted text in bold** represents user input.

---

## C# Implementation (As Written)

> ⚠️ **Note:**
> The following code is included **exactly as written by the student**.
> No logic, formatting, or comments have been changed or removed.

```csharp
using System;
using System.Linq;
using System.Net;
namespace Learning_Thread
{
    public class Program
    {
        public static List<Movie> MovieList = new List<Movie>();

        public static void Main(string[] args)
        {
            // Add movies
            AddMovie("Aam,Asad,Comedy,4");
            AddMovie("Bahubali,Rajamouli,Action,5");
            AddMovie("3 Idiots,Hirani,Comedy,5");
            AddMovie("Interstellar,Nolan,SciFi,5");

            // Display all movies
            Console.WriteLine("---- All Movies ----");
            foreach (var movie in MovieList)
            {
                Console.WriteLine($"{movie.Title} | {movie.Artist} | {movie.Genre} | {movie.Ratings}");
            }

            // Filter movies by genre
            Console.WriteLine("\n---- Filter movies by genre ----");
            string genre = "Comedy";
            var comedyMovies = ViewMoviesByGenre(genre);

            if (comedyMovies.Count == 0)
            {
                Console.WriteLine($"No Movies found in genre '{genre}'");
            }
            else
            {
                foreach (var movie in comedyMovies)
                {
                    Console.WriteLine($"{movie.Title} | {movie.Ratings}");
                }
            }

            // Sort movies by ratings
            Console.WriteLine("\n---- Movies Sorted By Ratings ----");
            var sortedMovies = ViewMovieByRatings();
            foreach (var movie in sortedMovies)
            {
                Console.WriteLine($"{movie.Title} | {movie.Ratings}");
            }
        }

        /// <summary>
        /// Adds a movie to the MovieList from comma-separated string.
        /// </summary>
        public static void AddMovie(string movieDetails)
        {
            string[] movies = movieDetails.Split(',');

            if (movies.Length != 4)
                throw new ArgumentException("Invalid movie format");

            MovieList.Add(new Movie
            {
                Title = movies[0].Trim(),
                Artist = movies[1].Trim(),
                Genre = movies[2].Trim(),
                Ratings = int.Parse(movies[3].Trim())
            });
        }

        /// <summary>
        /// Filters and returns movies by the specified genre.
        /// </summary>
        public static List<Movie> ViewMoviesByGenre(string genre)
        {
            List<Movie> res = new List<Movie>();

            foreach (var i in MovieList)
            {
                if (i.Genre == genre)
                {
                    res.Add(i);
                }
            }

            return res;

            // Second Method:
        //     return MovieList
        //    .Where(m => m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
        //    .ToList();
        }

        /// <summary>
        /// Sorts and returns movies by ratings in ascending order.
        /// </summary>
        public static List<Movie> ViewMovieByRatings()
        {
            var res = from i in MovieList orderby (i.Ratings) select i;
            return res.ToList();
        }
    }

    public class Movie
    {
        public string? Title { get; set; }
        public string? Artist { get; set; }
        public string? Genre { get; set; }
        public int Ratings { get; set; }
    }
}
```

---

## Conclusion

This program demonstrates:

* Object-oriented design using a `Movie` class
* Storing objects in a `List`
* Parsing comma-separated input strings
* Filtering collections by condition
* Sorting data using LINQ
* Clean separation of logic using methods
