import { getTemperatureHistory } from './api'
import fetchMock from 'jest-fetch-mock'
import { getTemperatureHistoryMock } from './api.mock'

require('jest-fetch-mock').enableMocks()

beforeEach(() => {
	fetchMock.resetMocks()
})

describe('getTemperatureHistory should return', () => {
	it('valid data', async () => {
		fetchMock.mockResponseOnce(JSON.stringify(getTemperatureHistoryMock))

		const response = await getTemperatureHistory('France', 'Tournus', 1973)

		expect(response.year).toBe(1973)
		expect(response.days).not.toBeNull()
		expect(response.days.length).toBe(365)
		expect(fetchMock.mock.calls.length).toEqual(1)
		expect(fetchMock.mock.calls[0][0]).toEqual('https://localhost:4000/France/Tournus/temperatures/1973')
	})
})
