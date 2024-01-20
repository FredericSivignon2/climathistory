import { format } from 'date-fns'
import {
	CountryModel,
	LocationModel,
	TemperatureAveragePerYearModel,
	TemperatureMinMaxPerYearModel,
	TemperatureModel,
	YearInfoModel,
} from '../types'
import { apiVersion } from './constants'
import { isNil } from 'lodash'

const baseUrl = `https://localhost:4000/api/${apiVersion}`
const formatDateUrl = 'yyyy-MM-dd'

export const getTemperatureHistory = async (locationId: number, year: number): Promise<YearInfoModel> => {
	const url = `${baseUrl}/location/${locationId}/temperatures/${year}`
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
	const url = `${baseUrl}/country/${countryId}/all-locations`
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
	const url = `${baseUrl}/country/all`
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
	const url = `${baseUrl}/location/${locationId}/temperatures/average-per-year`
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
	const url = `${baseUrl}/location/${locationId}/temperatures/minmax-per-year`
	const response = await fetch(url, {
		method: 'GET',
	})

	if (response.ok) {
		return (await response.json()) as TemperatureMinMaxPerYearModel[]
	} else {
		throw new Error(response.statusText)
	}
}

export const getAverageTemperatureByDateRange = async (
	locationId: number | undefined | null,
	startDate: Date,
	endDate: Date
): Promise<TemperatureModel> => {
	if (isNil(locationId)) {
		return { value: NaN }
	}

	const url = `${baseUrl}/location/${locationId}/temperatures/average/${format(startDate, formatDateUrl)}/${format(
		endDate,
		formatDateUrl
	)}`
	const response = await fetch(url, {
		method: 'GET',
	})

	if (response.ok) {
		return (await response.json()) as TemperatureModel
	} else {
		throw new Error(response.statusText)
	}
}
