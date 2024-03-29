﻿using Weather.Application.Model;
using Weather.Application.Models;
using Weather.Services.VisualCrossing;

namespace Weather.Application.Query
{
    public interface IWeatherReader
    {
        Task<bool> Health();

        Task<IEnumerable<CountryModel>> GetAllCountries();
        Task<IEnumerable<LocationModel>> GetAllLocationsByCountry(long countryId);

        Task<TemperaturesLocationInfoModel> GetLocationInfoFrom(long locationId);
        Task<TemperaturesYearInfoModel> GetTemperaturesInfoFrom(long locationId, int year);
        
        Task<IEnumerable<AveragePerYearModel>> GetAvgTemperaturesForAllYears(long locationId);
        Task<IEnumerable<MinMaxPerYearModel>> GetMinMaxTemperaturesDataFrom(long locationId);

        Task<TemperatureModel?> GetAverageTemperatureByDateRange(long locationId, DateTime startDate, DateTime endDate);
    }
}
