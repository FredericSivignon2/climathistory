export interface TemperatureAveragePerYearModel {
	year: number
	mean: number
	meanMax: number
	meanMin: number
}

export interface TemperatureAverageYearProps {
	country: string
	town: string
}
