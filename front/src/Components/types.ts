export interface GlobalData {
	country: string
	setCountry: (arg: string) => void
	town: string | null
	setTown: (arg: string) => void
}

export interface CountryModel {
	countryId: number
	name: string
}

export interface LocationModel {
	locationId: number
	name: string
	countryId: number
}

export interface MeanPerYearModel {
	year: number
	mean: number
	meanMax: number
	meanMin: number
}

export interface DayInfoModel {
	date: Date
	tempMax: number
	tempMin: number
	tempMean: number
}

export interface YearInfoModel {
	year: number
	days: DayInfoModel[]
}

export interface LocationInfoModel {
	locationName: string
	yearsInfo: YearInfoModel[]
}

export interface TemperatureMinMaxPerYearModel {
	year: number
	min: number
	max: number
}

export interface TemperatureAveragePerYearModel {
	year: number
	mean: number
	meanMax: number
	meanMin: number
}
