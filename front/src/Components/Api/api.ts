import { getTemperatureHistoryPerYear } from 'src/WeatherModules/api'
import { TemperatureModel, TownTemperaturesPerYearModel } from 'src/WeatherModules/types'

export const getTemperatureHistory = async (year: number): Promise<TownTemperaturesPerYearModel> => {
	console.log('Retreiving temperature history for year: ' + year)
	return await getTemperatureHistoryPerYear(year)
}

/*
Version to directly call api from tomorrow.io

import moment from 'moment'
import { secretKey } from '../TemperatureHistory/constants'
import queryString from 'query-string'
import { TemperatureHistoryDto } from '../TemperatureHistory'

export const getTemperatureHistory =
	async (): Promise<TemperatureHistoryDto | null> => {
		// set the Timelines GET endpoint as the target URL
		const getTimelineURL = 'https://api.tomorrow.io/v4/timelines'

		// pick the location, as a latlong pair
		let location = [40.758, -73.9855]

		// list the fields
		const fields = [
			'precipitationIntensity',
			'precipitationType',
			'windSpeed',
			'windGust',
			'windDirection',
			'temperature',
			'temperatureApparent',
			'cloudCover',
			'cloudBase',
			'cloudCeiling',
			'weatherCode',
		]
		const apikey = secretKey

		// choose the unit system, either metric or imperial
		const units = 'imperial'

		// set the timesteps, like "current", "1h" and "1d"
		const timesteps = ['current', '1h', '1d']

		// configure the time frame up to 6 hours back and 15 days out
		const now = moment.utc()
		const startTime = moment.utc(now).add(0, 'minutes').toISOString()
		const endTime = moment.utc(now).add(1, 'days').toISOString()

		// specify the timezone, using standard IANA timezone format
		const timezone = 'America/New_York'

		// request the timelines with all the query string parameters as options
		const getTimelineParameters = queryString.stringify(
			{
				apikey,
				location,
				fields,
				units,
				timesteps,
				startTime,
				endTime,
				timezone,
			},
			{ arrayFormat: 'comma' }
		)

		let data = null

		console.log('URL = ' + getTimelineURL + '?' + getTimelineParameters)
		const response = await fetch(getTimelineURL + '?' + getTimelineParameters, {
			method: 'GET',
			// compress: true,
		})

		if (response.ok) {
			return (await response.json()) as TemperatureHistoryDto
		} else {
			// console.error('Error:', response.status, response.statusText)
			throw new Error(response.statusText)
		}
	}
*/
