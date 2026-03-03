-- Flight Search Engine - Setup Verification Script

USE FlightSearchDB;
GO

PRINT 'FLIGHT SEARCH ENGINE - SETUP VERIFICATION';
PRINT '';

-- Check Tables
PRINT '1. CHECKING TABLES...';
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Flights')
    PRINT 'Flights table exists'
ELSE
    PRINT 'Flights table NOT found';

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Hotels')
    PRINT 'Hotels table exists'
ELSE
    PRINT 'Hotels table NOT found';
PRINT '';

-- Check Data
PRINT '2. CHECKING DATA...';
DECLARE @FlightCount INT, @HotelCount INT;
SELECT @FlightCount = COUNT(*) FROM Flights;
SELECT @HotelCount = COUNT(*) FROM Hotels;

PRINT 'Flights: ' + CAST(@FlightCount AS VARCHAR) + ' records (Expected: 16)';
PRINT 'Hotels: ' + CAST(@HotelCount AS VARCHAR) + ' records (Expected: 7)';
PRINT '';

-- Check Stored Procedures
PRINT '3. CHECKING STORED PROCEDURES...';
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetSources')
    PRINT 'sp_GetSources exists'
ELSE
    PRINT 'sp_GetSources NOT found';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetDestinations')
    PRINT 'sp_GetDestinations exists'
ELSE
    PRINT 'sp_GetDestinations NOT found';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_SearchFlights')
    PRINT 'sp_SearchFlights exists'
ELSE
    PRINT 'sp_SearchFlights NOT found';

IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_SearchFlightsWithHotels')
    PRINT 'sp_SearchFlightsWithHotels exists'
ELSE
    PRINT 'sp_SearchFlightsWithHotels NOT found';
PRINT '';

-- Test Stored Procedures
PRINT '4. TESTING STORED PROCEDURES...';

PRINT 'Testing sp_GetSources...';
EXEC sp_GetSources;
PRINT '';

PRINT 'Testing sp_GetDestinations...';
EXEC sp_GetDestinations;
PRINT '';

PRINT 'Testing sp_SearchFlights (Mumbai to Delhi, 2 persons)...';
EXEC sp_SearchFlights @Source = 'Mumbai', @Destination = 'Delhi', @Persons = 2;
PRINT '';

PRINT 'Testing sp_SearchFlightsWithHotels (Mumbai to Dubai, 1 person)...';
EXEC sp_SearchFlightsWithHotels @Source = 'Mumbai', @Destination = 'Dubai', @Persons = 1;
PRINT '';

PRINT 'VERIFICATION COMPLETE';
PRINT 'If all checks passed, your database is ready!';
PRINT '';
