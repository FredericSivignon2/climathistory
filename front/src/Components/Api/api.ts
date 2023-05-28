import { TownTemperaturesPerYearModel } from '../TemperatureHistory'

export const getTemperatureHistory = async (location: string, year: number): Promise<TownTemperaturesPerYearModel> => {
	const url = `https://localhost:4000/${location}/temperatures/${year}`
	const response = await fetch(url, {
		method: 'GET',
	})

	if (response.ok) {
		return (await response.json()) as TownTemperaturesPerYearModel
	} else {
		throw new Error(response.statusText)
	}
}

export const getAllLocations = async (): Promise<string[]> => {
	const url = 'https://localhost:4000/alllocations'
	const response = await fetch(url, {
		method: 'GET',
	})

	if (response.ok) {
		return (await response.json()) as string[]
	} else {
		throw new Error(response.statusText)
	}
}
