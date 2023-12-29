import { getAllCountries, getAllLocationsByCountry, getTemperatureHistory } from './api'
import fetchMock from 'jest-fetch-mock'
import {
	getAllCountriesMock,
	getAllTownMock,
	getEmptyAllTownMock,
	getEmptyTemperatureHistoryMock,
	getTemperatureHistoryMock,
} from './api.mock'

require('jest-fetch-mock').enableMocks()

beforeEach(() => {
	fetchMock.resetMocks()
})

describe('getTemperatureHistory should return', () => {
	it('valid data if parameters are corresponding to existing information.', async () => {
		fetchMock.mockResponseOnce(JSON.stringify(getTemperatureHistoryMock))

		const response = await getTemperatureHistory('France', 'Tournus', 1973)

		expect(response.year).toBe(1973)
		expect(response.days).not.toBeNull()
		expect(response.days.length).toBe(365)
		expect(fetchMock.mock.calls.length).toEqual(1)
		expect(fetchMock.mock.calls[0][0]).toEqual('https://localhost:4000/France/Tournus/temperatures/1973')
	})

	it('empty data if parameters are not corresponding to existing information.', async () => {
		fetchMock.mockResponseOnce(JSON.stringify(getEmptyTemperatureHistoryMock))

		const response = await getTemperatureHistory('France', 'Tournus', 1950)

		expect(response.year).toBe(0)
		expect(response.days).not.toBeNull()
		expect(response.days.length).toBe(0)
		expect(fetchMock.mock.calls.length).toEqual(1)
		expect(fetchMock.mock.calls[0][0]).toEqual('https://localhost:4000/France/Tournus/temperatures/1950')
	})
})

describe('getAllLocationsByCountry should return', () => {
	it('valid data if parameters are corresponding to existing information.', async () => {
		fetchMock.mockResponseOnce(JSON.stringify(getAllTownMock))

		const response = await getAllLocationsByCountry('Allemagne')

		expect(response).not.toBeNull()
		expect(response.length).toBe(getAllTownMock.length)
		expect(response[O].)
		expect(fetchMock.mock.calls.length).toEqual(1)
		expect(fetchMock.mock.calls[0][0]).toEqual('https://localhost:4000/Allemagne/alltowns')
	})

	it('empty data if parameters are not corresponding to existing information.', async () => {
		fetchMock.mockResponseOnce(JSON.stringify(getEmptyAllTownMock))

		const response = await getAllLocationsByCountry('Allemagne')

		expect(response).not.toBeNull()
		expect(response.length).toBe(0)
		expect(fetchMock.mock.calls.length).toEqual(1)
		expect(fetchMock.mock.calls[0][0]).toEqual('https://localhost:4000/Allemagne/alltowns')
	})
})

describe('getAllCountries should return', () => {
	it('valid data.', async () => {
		fetchMock.mockResponseOnce(JSON.stringify(getAllCountriesMock))

		const response = await getAllCountries()

		expect(response).not.toBeNull()
		expect(response.length).toBe(getAllCountriesMock.length)
		expect(response[0].name).toBeDefined()
		expect(response[0].countryId).toBeGreaterThan(0)
		expect(fetchMock.mock.calls.length).toEqual(1)
		expect(fetchMock.mock.calls[0][0]).toEqual('https://localhost:4000/api/all-countries')
	})
})

describe('getAverageTemperaturesPerYear should return', () => {
	it('valid data if parameters are corresponding to existing information.', async () => {
		fetchMock.mockResponseOnce(JSON.stringify(getAllCountriesMock))

		const response = await getAllCountries()

		expect(response).not.toBeNull()
		expect(response.length).toBe(getAllCountriesMock.length)
		expect(fetchMock.mock.calls.length).toEqual(1)
		expect(fetchMock.mock.calls[0][0]).toEqual('https://localhost:4000/allcountries')
	})
})
