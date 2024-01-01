import {
	getAllCountries,
	getAllLocationsByCountry,
	getAverageTemperaturesPerYear,
	getMinMaxTemperaturesPerYear,
	getTemperatureHistory,
} from './api'
import fetchMock from 'jest-fetch-mock'
import {
	getAllCountriesMock,
	getAllLocationsMock,
	getAverageTemperaturesMock,
	getEmptyAllLocationsMock,
	getEmptyAverageTemperaturesMock,
	getEmptyMinMaxTemperaturesMock,
	getEmptyTemperatureHistoryMock,
	getMinMaxTemperaturesMock,
	getTemperatureHistoryMock,
} from './api.mock'
import { LocationModel } from '../types'

require('jest-fetch-mock').enableMocks()

beforeEach(() => {
	fetchMock.resetMocks()
})

describe('getTemperatureHistory should return', () => {
	it('valid data if parameters are corresponding to existing information.', async () => {
		fetchMock.mockResponseOnce(JSON.stringify(getTemperatureHistoryMock))

		const response = await getTemperatureHistory(134, 1980)

		expect(response.year).toBe(1980)
		expect(response.days).not.toBeNull()
		expect(response.days.length).toBe(366)
		expect(fetchMock.mock.calls.length).toEqual(1)
		expect(fetchMock.mock.calls[0][0]).toEqual('https://localhost:4000/api/v1.0/location/134/temperatures/1980')
	})

	it('empty data if parameters are not corresponding to existing information.', async () => {
		fetchMock.mockResponseOnce(JSON.stringify(getEmptyTemperatureHistoryMock))

		const response = await getTemperatureHistory(158900, 1950)

		expect(response.year).toBe(0)
		expect(response.days).not.toBeNull()
		expect(response.days.length).toBe(0)
		expect(fetchMock.mock.calls.length).toEqual(1)
		expect(fetchMock.mock.calls[0][0]).toEqual('https://localhost:4000/api/v1.0/location/158900/temperatures/1950')
	})
})

describe('getAllLocationsByCountry should return', () => {
	it('valid data if parameters are corresponding to existing information.', async () => {
		fetchMock.mockResponseOnce(JSON.stringify(getAllLocationsMock))

		const response = await getAllLocationsByCountry(3)

		expect(response).not.toBeNull()
		expect(response.length).toBe(getAllLocationsMock.length)
		expect(response[0].name).toBe('Benoni')
		expect(response[0].locationId).toBe(134)
		expect(response[0].countryId).toBe(3)
		expect(fetchMock.mock.calls.length).toEqual(1)
		expect(fetchMock.mock.calls[0][0]).toEqual('https://localhost:4000/api/v1.0/country/3/all-locations')
	})

	it('empty data if parameters are not corresponding to existing information.', async () => {
		fetchMock.mockResponseOnce(JSON.stringify(getEmptyAllLocationsMock))

		const response = await getAllLocationsByCountry(16547)

		expect(response).not.toBeNull()
		expect(response.length).toBe(0)
		expect(fetchMock.mock.calls.length).toEqual(1)
		expect(fetchMock.mock.calls[0][0]).toEqual('https://localhost:4000/api/v1.0/country/16547/all-locations')
	})
})

describe('getAllCountries should return', () => {
	it('valid data.', async () => {
		fetchMock.mockResponseOnce(JSON.stringify(getAllCountriesMock))

		const response = await getAllCountries()

		expect(response).not.toBeNull()
		expect(response.length).toBe(getAllCountriesMock.length)
		expect(response[0].name).toBe('Allemagne')
		expect(response[0].countryId).toBe(1)
		expect(fetchMock.mock.calls.length).toEqual(1)
		expect(fetchMock.mock.calls[0][0]).toEqual('https://localhost:4000/api/v1.0/country/all')
	})
})

describe('getAverageTemperaturesPerYear should return', () => {
	it('valid data if parameters are corresponding to existing information.', async () => {
		fetchMock.mockResponseOnce(JSON.stringify(getAverageTemperaturesMock))

		const response = await getAverageTemperaturesPerYear(134)

		expect(response).not.toBeNull()
		expect(response.length).toBe(getAverageTemperaturesMock.length)
		expect(response[0].year).toBe(1973)
		expect(response[1].year).toBe(1974)
		expect(response[0].averageOfAverage).toBeGreaterThan(0)
		expect(response[0].averageOfMax).toBeGreaterThan(0)
		expect(response[0].averageOfMin).toBeGreaterThan(0)
		expect(fetchMock.mock.calls.length).toEqual(1)
		expect(fetchMock.mock.calls[0][0]).toEqual(
			'https://localhost:4000/api/v1.0/location/134/temperatures/average-per-year'
		)
	})

	it('empty data if the result is an empty array', async () => {
		fetchMock.mockResponseOnce(JSON.stringify(getEmptyAverageTemperaturesMock))

		const response = await getAverageTemperaturesPerYear(655890)
		expect(response).not.toBeNull()
		expect(response.length).toBe(0)
		expect(fetchMock.mock.calls.length).toEqual(1)
		expect(fetchMock.mock.calls[0][0]).toEqual(
			'https://localhost:4000/api/v1.0/location/655890/temperatures/average-per-year'
		)
	})
})

describe('getMinMaxTemperaturesPerYear should return', () => {
	it('valid data if parameters are corresponding to existing information.', async () => {
		fetchMock.mockResponseOnce(JSON.stringify(getMinMaxTemperaturesMock))

		const response = await getMinMaxTemperaturesPerYear(134)

		expect(response).not.toBeNull()
		expect(response.length).toBe(getAverageTemperaturesMock.length)
		expect(response[0].year).toBe(1973)
		expect(response[1].year).toBe(1974)
		expect(response[0].min).toBeDefined()
		expect(response[0].max).toBeDefined()
		expect(fetchMock.mock.calls.length).toEqual(1)
		expect(fetchMock.mock.calls[0][0]).toEqual(
			'https://localhost:4000/api/v1.0/location/134/temperatures/minmax-per-year'
		)
	})

	it('empty data if the result is an empty array', async () => {
		fetchMock.mockResponseOnce(JSON.stringify(getEmptyMinMaxTemperaturesMock))

		const response = await getMinMaxTemperaturesPerYear(655890)
		expect(response).not.toBeNull()
		expect(response.length).toBe(0)
		expect(fetchMock.mock.calls.length).toEqual(1)
		expect(fetchMock.mock.calls[0][0]).toEqual(
			'https://localhost:4000/api/v1.0/location/655890/temperatures/minmax-per-year'
		)
	})
})
