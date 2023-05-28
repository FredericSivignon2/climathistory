export interface TownTemperaturesPerYearModel {
	year: number
	resolvedAdress: string
	address: string
	days: TemperatureModel[]
}

export interface TemperatureModel {
	datetime: string
	tempmax: number
	tempmin: number
	temp: number
}
