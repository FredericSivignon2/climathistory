export interface TemperatureHistoryProps {
	country: string
	town: string
	defaultYear: number
}

// Generated by https://quicktype.io

// export interface TemperatureHistoryDto {
// 	data: Data
// }

// export interface Data {
// 	timelines: Timeline[]
// }

// export interface Timeline {
// 	timestep: string
// 	endTime: string
// 	startTime: string
// 	intervals: Interval[]
// }

export interface Interval {
	startTime: string
	values: Values
}

export interface Values {
	temperature: number
	cloudCover: number
}

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
