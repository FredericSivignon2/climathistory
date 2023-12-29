import {
	CountryModel,
	LocationModel,
	TemperatureAveragePerYearModel,
	TemperatureMinMaxPerYearModel,
	YearInfoModel,
} from '../types'

export const getTemperatureHistory = async (
	country: string,
	location: string,
	year: number
): Promise<YearInfoModel> => {
	const url = `https://localhost:4000/${country}/${location}/temperatures/${year}`
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
	const url = `https://localhost:4000/api§${countryId}/all-locations`
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
	const url = 'https://localhost:4000/api/all-countries'
	const response = await fetch(url, {
		method: 'GET',
	})

	if (response.ok) {
		return (await response.json()) as CountryModel[]
	} else {
		throw new Error(response.statusText)
	}
}

export const getAverageTemperaturesPerYear = async (
	country: string,
	town: string
): Promise<TemperatureAveragePerYearModel[]> => {
	const url = `https://localhost:4000/${country}/${town}/average-temperatures-per-year`
	const response = await fetch(url, {
		method: 'GET',
	})

	if (response.ok) {
		return (await response.json()) as TemperatureAveragePerYearModel[]
	} else {
		throw new Error(response.statusText)
	}
}

export const getMinMaxTemperaturesPerYear = async (
	country: string,
	town: string
): Promise<TemperatureMinMaxPerYearModel[]> => {
	const url = `https://localhost:4000/${country}/${town}/minmax-temperatures-per-year`
	const response = await fetch(url, {
		method: 'GET',
	})

	if (response.ok) {
		return (await response.json()) as TemperatureMinMaxPerYearModel[]
	} else {
		throw new Error(response.statusText)
	}
}
