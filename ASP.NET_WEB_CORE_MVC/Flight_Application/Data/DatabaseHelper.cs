using System.Data;
using Microsoft.Data.SqlClient;
using Flight_Application.Models;

namespace Flight_Application.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<List<string>> GetSourcesAsync()
        {
            var sources = new List<string>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_GetSources", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var source = reader["Source"]?.ToString();
                            if (!string.IsNullOrEmpty(source))
                            {
                                sources.Add(source);
                            }
                        }
                    }
                }
            }

            return sources;
        }

        public async Task<List<string>> GetDestinationsAsync()
        {
            var destinations = new List<string>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_GetDestinations", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var destination = reader["Destination"]?.ToString();
                            if (!string.IsNullOrEmpty(destination))
                            {
                                destinations.Add(destination);
                            }
                        }
                    }
                }
            }

            return destinations;
        }

        public async Task<List<FlightResult>> SearchFlightsAsync(string source, string destination, int persons)
        {
            var flights = new List<FlightResult>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_SearchFlights", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Source", source);
                    command.Parameters.AddWithValue("@Destination", destination);
                    command.Parameters.AddWithValue("@Persons", persons);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            flights.Add(new FlightResult
                            {
                                FlightId = Convert.ToInt32(reader["FlightId"]),
                                FlightName = reader["FlightName"]?.ToString() ?? string.Empty,
                                FlightType = reader["FlightType"]?.ToString() ?? string.Empty,
                                Source = reader["Source"]?.ToString() ?? string.Empty,
                                Destination = reader["Destination"]?.ToString() ?? string.Empty,
                                TotalCost = Convert.ToDecimal(reader["TotalCost"])
                            });
                        }
                    }
                }
            }

            return flights;
        }

        public async Task<List<FlightHotelResult>> SearchFlightsWithHotelsAsync(string source, string destination, int persons)
        {
            var packages = new List<FlightHotelResult>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("sp_SearchFlightsWithHotels", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Source", source);
                    command.Parameters.AddWithValue("@Destination", destination);
                    command.Parameters.AddWithValue("@Persons", persons);

                    await connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            packages.Add(new FlightHotelResult
                            {
                                FlightId = Convert.ToInt32(reader["FlightId"]),
                                FlightName = reader["FlightName"].ToString(),
                                Source = reader["Source"].ToString(),
                                Destination = reader["Destination"].ToString(),
                                HotelName = reader["HotelName"].ToString(),
                                TotalCost = Convert.ToDecimal(reader["TotalCost"])
                            });
                        }
                    }
                }
            }

            return packages;
        }
    }
}
