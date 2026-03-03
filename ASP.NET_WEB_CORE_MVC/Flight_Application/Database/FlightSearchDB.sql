-- Flight Search Engine Database Setup Script

USE master;
GO

-- Create Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'FlightSearchDB')
BEGIN
    CREATE DATABASE FlightSearchDB;
END
GO

USE FlightSearchDB;
GO

-- Create Tables

-- Create Flights Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Flights')
BEGIN
    CREATE TABLE Flights (
        FlightId INT PRIMARY KEY IDENTITY(1,1),
        FlightName NVARCHAR(100) NOT NULL,
        FlightType NVARCHAR(50) NOT NULL,
        Source NVARCHAR(100) NOT NULL,
        Destination NVARCHAR(100) NOT NULL,
        PricePerSeat DECIMAL(18,2) NOT NULL
    );
END
GO

-- Create Hotels Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Hotels')
BEGIN
    CREATE TABLE Hotels (
        HotelId INT PRIMARY KEY IDENTITY(1,1),
        HotelName NVARCHAR(100) NOT NULL,
        HotelType NVARCHAR(50) NOT NULL,
        Location NVARCHAR(100) NOT NULL,
        PricePerDay DECIMAL(18,2) NOT NULL
    );
END
GO

-- Insert Sample Data

-- Insert Flights
IF NOT EXISTS (SELECT * FROM Flights)
BEGIN
    INSERT INTO Flights (FlightName, FlightType, Source, Destination, PricePerSeat) VALUES
    ('Air India AI-101', 'Domestic', 'Mumbai', 'Delhi', 5000.00),
    ('Air India AI-102', 'Domestic', 'Delhi', 'Mumbai', 5200.00),
    ('IndiGo 6E-201', 'Domestic', 'Mumbai', 'Bangalore', 4500.00),
    ('IndiGo 6E-202', 'Domestic', 'Bangalore', 'Mumbai', 4600.00),
    ('SpiceJet SG-301', 'Domestic', 'Delhi', 'Bangalore', 4800.00),
    ('SpiceJet SG-302', 'Domestic', 'Bangalore', 'Delhi', 4900.00),
    ('Vistara UK-401', 'Domestic', 'Mumbai', 'Chennai', 5500.00),
    ('Vistara UK-402', 'Domestic', 'Chennai', 'Mumbai', 5600.00),
    ('Air India AI-501', 'International', 'Mumbai', 'Dubai', 15000.00),
    ('Air India AI-502', 'International', 'Dubai', 'Mumbai', 15500.00),
    ('Emirates EK-601', 'International', 'Delhi', 'Dubai', 16000.00),
    ('Emirates EK-602', 'International', 'Dubai', 'Delhi', 16500.00),
    ('Singapore Airlines SQ-701', 'International', 'Mumbai', 'Singapore', 20000.00),
    ('Singapore Airlines SQ-702', 'International', 'Singapore', 'Mumbai', 20500.00),
    ('Qatar Airways QR-801', 'International', 'Delhi', 'Doha', 18000.00),
    ('Qatar Airways QR-802', 'International', 'Doha', 'Delhi', 18500.00);
END
GO

-- Insert Hotels
IF NOT EXISTS (SELECT * FROM Hotels)
BEGIN
    INSERT INTO Hotels (HotelName, HotelType, Location, PricePerDay) VALUES
    ('Taj Palace', '5-Star', 'Mumbai', 8000.00),
    ('The Leela', '5-Star', 'Delhi', 7500.00),
    ('ITC Grand', '5-Star', 'Bangalore', 7000.00),
    ('Taj Coromandel', '5-Star', 'Chennai', 6500.00),
    ('Burj Al Arab', '7-Star', 'Dubai', 25000.00),
    ('Marina Bay Sands', '5-Star', 'Singapore', 15000.00),
    ('The St. Regis', '5-Star', 'Doha', 12000.00);
END
GO

-- Create Stored Procedures

-- Get Sources
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetSources')
    DROP PROCEDURE sp_GetSources;
GO

CREATE PROCEDURE sp_GetSources
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT Source FROM Flights ORDER BY Source;
END
GO

-- Get Destinations
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetDestinations')
    DROP PROCEDURE sp_GetDestinations;
GO

CREATE PROCEDURE sp_GetDestinations
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT Destination FROM Flights ORDER BY Destination;
END
GO

-- Search Flights
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_SearchFlights')
    DROP PROCEDURE sp_SearchFlights;
GO

CREATE PROCEDURE sp_SearchFlights
    @Source NVARCHAR(100),
    @Destination NVARCHAR(100),
    @Persons INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        FlightId,
        FlightName,
        FlightType,
        Source,
        Destination,
        (PricePerSeat * @Persons) AS TotalCost
    FROM Flights
    WHERE Source = @Source AND Destination = @Destination
    ORDER BY TotalCost;
END
GO

-- Search Flights With Hotels
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_SearchFlightsWithHotels')
    DROP PROCEDURE sp_SearchFlightsWithHotels;
GO

CREATE PROCEDURE sp_SearchFlightsWithHotels
    @Source NVARCHAR(100),
    @Destination NVARCHAR(100),
    @Persons INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        F.FlightId,
        F.FlightName,
        F.Source,
        F.Destination,
        H.HotelName,
        ((F.PricePerSeat * @Persons) + H.PricePerDay) AS TotalCost
    FROM Flights F
    INNER JOIN Hotels H ON F.Destination = H.Location
    WHERE F.Source = @Source AND F.Destination = @Destination
    ORDER BY TotalCost;
END
GO

-- Verification
PRINT 'Database setup completed successfully!';
PRINT '';
PRINT 'Verification:';
SELECT 'Flights' AS TableName, COUNT(*) AS RecordCount FROM Flights
UNION ALL
SELECT 'Hotels', COUNT(*) FROM Hotels;
GO
