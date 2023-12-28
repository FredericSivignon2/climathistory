using System.Data;
using Npgsql;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.Database.Model;
using System.Data.Common;
using System.Xml.Linq;

namespace Weather.Database.Postgres
{
    // docker run --name mypostgresdb -e POSTGRES_PASSWORD=weaTHERapp_2024 -d -p 5432:5432 -v postgres_data:/var/lib/postgresql/data postgres

    public class WeatherRepository : IWeatherRepository
    {
        private bool _disposed = false;
        private IDbConnection _connection;

        public WeatherRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        private void EnsureConnectionOpen()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        // Méthodes pour Country
        public async Task<bool> IsExistingCountry(string name)
        {
            EnsureConnectionOpen();
            var parameters = new { Name = name };
            var sql = "SELECT COUNT(1) FROM Country WHERE Name=@Name";

            var numberOfElement = await _connection.ExecuteScalarAsync<int>(sql, parameters);
            return numberOfElement > 0;
        }

        public async Task<CountryData> GetCountryByNameAsync(string name)
        {
            EnsureConnectionOpen();
            var parameters = new { Name = name };
            var sql = "SELECT * FROM Country WHERE Name=@Name";

            return await _connection.QuerySingleAsync<CountryData>(sql, parameters);

        }

        public async Task<bool> IsExistingLocation(long countryId, string name)
        {
            EnsureConnectionOpen();

            var parameters = new { CountryId = countryId, Name = name };
            var sql = "SELECT COUNT(1) FROM Location WHERE CountryId=@CountryId AND Name=@Name";

            var numberOfElement = await _connection.ExecuteScalarAsync<int>(sql, parameters);
            return numberOfElement > 0;
        }

        public async Task<IEnumerable<CountryData>> GetAllCountriesAsync()
        {
            EnsureConnectionOpen();

            return await _connection.QueryAsync<CountryData>("SELECT * FROM Country");
        }

        public async Task AddCountryAsync(CountryData country)
        {
            EnsureConnectionOpen();

            var parameters = new { Name = country.Name };
            var sql = "INSERT INTO Country (Name) VALUES (@Name)";
            await _connection.ExecuteAsync(sql, parameters);
        }

        public async Task UpdateCountryAsync(CountryData country)
        {
            EnsureConnectionOpen();

            await _connection.ExecuteAsync("UPDATE Country SET Name = @Name WHERE CountryId = @CountryId", country);
        }

        // Méthodes pour Location
        public async Task<IEnumerable<LocationData>> GetAllLocationsAsync()
        {
            EnsureConnectionOpen();
            return await _connection.QueryAsync<LocationData>("SELECT * FROM Location");
        }

        public async Task<IEnumerable<LocationData>> GetAllLocationsAsync(long countryId)
        {
            EnsureConnectionOpen();

            var parameters = new { CountryId = countryId };
            var sql = "SELECT * FROM Location WHERE CountryId = @CountryId";
            return await _connection.QueryAsync<LocationData>(sql, parameters);
        }

        public async Task AddLocationAsync(LocationData location)
        {
            EnsureConnectionOpen();
            var parameters = new { CountryId = location.CountryId, Name = location.Name };
            var sql = "INSERT INTO Location (Name, CountryId) VALUES (@Name, @CountryId)";
            await _connection.ExecuteAsync(sql, parameters);
        }

        public async Task UpdateLocationAsync(LocationData location)
        {
            EnsureConnectionOpen();
            await _connection.ExecuteAsync("UPDATE Location SET Name = @Name, CountryId = @CountryId WHERE LocationId = @LocationId", location);
        }

        // Méthodes pour Temperatures
        public async Task<IEnumerable<TemperaturesData>> GetTemperaturesAsync(long locationId)
        {
            EnsureConnectionOpen();
            return await _connection.QueryAsync<TemperaturesData>("SELECT * FROM Temperatures WHERE LocationId = @LocationId", new { LocationId = locationId });
        }

        public async Task<TemperaturesData?> GetTemperaturesAsync(long locationId, DateTime date)
        {
            EnsureConnectionOpen();

            var parameters = new { LocationId = locationId, Date = date };
            var sql = "SELECT * FROM Temperatures WHERE LocationId = @LocationId AND Date = @Date";
            return await _connection.QueryFirstOrDefaultAsync<TemperaturesData>(sql, parameters);
        }

        public async Task AddTemperatureAsync(TemperaturesData temperature)
        {
            EnsureConnectionOpen();
            await _connection.ExecuteAsync("INSERT INTO Temperatures (LocationId, Date, MinTemperature, MaxTemperature, AvgTemperature) VALUES (@LocationId, @Date, @MinTemperature, @MaxTemperature, @AvgTemperature)", temperature);
        }

        public async Task AddTemperaturesBulkAsync(IEnumerable<TemperaturesData> temperatures)
        {
            EnsureConnectionOpen();

            var sql = "INSERT INTO Temperatures (LocationId, Date, MinTemperature, MaxTemperature, AvgTemperature) VALUES (@LocationId, @Date, @MinTemperature, @MaxTemperature, @AvgTemperature)";
            await _connection.ExecuteAsync(sql, temperatures);
        }

        public async Task UpdateTemperatureAsync(TemperaturesData temperature)
        {
            EnsureConnectionOpen();
            await _connection.ExecuteAsync("UPDATE Temperatures SET MinTemperature = @MinTemperature, MaxTemperature = @MaxTemperature, AvgTemperature = @AvgTemperature WHERE LocationId = @LocationId AND Date = @Date", temperature);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Méthode protégée Dispose(bool) qui effectue le travail de libération de ressources
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Libérez ici les ressources gérées
                    _connection.Close();
                    _connection.Dispose();
                }

                // Libérez ici les ressources non gérées

                _disposed = true;
            }
        }
    }

}
