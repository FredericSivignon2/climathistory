/*
Each tempParisXXXX data has been generated with the following request (of course, need to adapt dates):
https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/paris/2021-01-01/2021-12-31?unitGroup=metric&elements=datetime%2Ctempmax%2Ctempmin%2Ctemp&include=days%2Cobs&key=3BXU58SLQQXU4CTK9YEEVCVFN&options=nonulls&contentType=json
*/
import { isNil } from '../../Components/utils'
import { TemperatureModel } from '../types'
import tempParis1985 from './json/temperatures_paris_1985.json'
import tempParis2000 from './json/temperatures_paris_2000.json'
import tempParis2019 from './json/temperatures_paris_2019.json'
import tempParis2020 from './json/temperatures_paris_2020.json'
import tempParis2021 from './json/temperatures_paris_2021.json'
import tempParis2022 from './json/temperatures_paris_2022.json'
import { WeatherData } from './types'

const data: WeatherData[] = [tempParis1985, tempParis2000, tempParis2019, tempParis2020, tempParis2021, tempParis2022]

export const getTemperatureHistoryPerYearFromVisualCrossing = (year: number): TemperatureModel[] | null => {
	const weatherData = data.find((wd) => wd.year === year)
	if (isNil(weatherData)) return null

	return getTemperatureModelFromWeatherData(weatherData)
}

const getTemperatureModelFromWeatherData = (data: WeatherData): TemperatureModel[] | null => {
	if (isNil(data.days) || data.days.length === 0) return null

	const result: TemperatureModel[] = Array(data.days.length)
	let index = 0
	data.days.forEach((day) => {
		const temperatureModel = {
			datetime: day.datetime,
			tempmax: day.tempmax,
			tempmin: day.tempmin,
			temp: day.temp,
		}
		result[index++] = temperatureModel
	})

	return result
}
