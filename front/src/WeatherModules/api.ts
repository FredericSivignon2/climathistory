import { TownTemperaturesPerYearModel } from './types'

export const getTemperatureHistoryPerYear = async (year: number): Promise<TownTemperaturesPerYearModel> => {
	const url = 'https://localhost:4000/Paris/temperatures/' + year
	const response = await fetch(url, {
		method: 'GET',
	})

	if (response.ok) {
		return (await response.json()) as TownTemperaturesPerYearModel
	} else {
		throw new Error(response.statusText)
	}
}
