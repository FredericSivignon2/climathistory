import {
	CountryModel,
	LocationModel,
	TemperatureAveragePerYearModel,
	TemperatureMinMaxPerYearModel,
	YearInfoModel,
} from '../types'
import { apiVersion } from './constants'

export const getTemperatureHistory = async (locationId: number, year: number): Promise<YearInfoModel> => {
	const url = `https://localhost:4000/api/${apiVersion}/location/${locationId}/temperatures/${year}`
	const response = await fetch(url, {
		method: 'GET',
	})

	if (response.ok) {
		return (await response.json()) as YearInfoModel
	} else {
		throw new Error(response.statusText)
	}
}

export const getAllLocationsByCountry = async (countryId: number): Promise<LocationModel[]> => {
	const url = `https://localhost:4000/api/${apiVersion}/country/${countryId}/all-locations`
	const response = await fetch(url, {
		method: 'GET',
	})

	if (response.ok) {
		return (await response.json()) as LocationModel[]
	} else {
		throw new Error(response.statusText)
	}
}

export const getAllCountries = async (): Promise<CountryModel[]> => {
	const url = `https://localhost:4000/api/${apiVersion}/country/all`
	const response = await fetch(url, {
		method: 'GET',
	})

	if (response.ok) {
		return (await response.json()) as CountryModel[]
	} else {
		throw new Error(response.statusText)
	}
}

export const getAverageTemperaturesPerYear = async (locationId: number): Promise<TemperatureAveragePerYearModel[]> => {
	const url = `https://localhost:4000/api/${apiVersion}/location/${locationId}/temperatures/average-per-year`
	const response = await fetch(url, {
		method: 'GET',
	})

	if (response.ok) {
		return (await response.json()) as TemperatureAveragePerYearModel[]
	} else {
		throw new Error(response.statusText)
	}
}

export const getMinMaxTemperaturesPerYear = async (locationId: number): Promise<TemperatureMinMaxPerYearModel[]> => {
	const url = `https://localhost:4000/api/${apiVersion}/location/${locationId}/temperatures/minmax-per-year`
	const response = await fetch(url, {
		method: 'GET',
	})

	if (response.ok) {
		return (await response.json()) as TemperatureMinMaxPerYearModel[]
	} else {
		throw new Error(response.statusText)
	}
}
