import { getDefaultYearToCompare } from './utils'

test('getDefaultYearToCompare', () => {
	expect(getDefaultYearToCompare(1973)).toBe(1974)
	expect(getDefaultYearToCompare(2023)).toBe(2022)
})
