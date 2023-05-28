import { ChartData } from 'chart.js'
import { TemperatureModel, TownTemperaturesPerYearModel } from 'src/WeatherModules/types'
import { hexToRGBA, isNil } from '../utils'
import { copyFile } from 'fs'
import { maxTempColor, mediumTempColor, minTempColor } from '..'

export const getChartData = (source: TownTemperaturesPerYearModel): ChartData<'line'> => {
	if (isNil(source) || isNil(source.days) || source.days.length === 0)
		return {
			labels: [],
			datasets: [],
		}

	const labels: string[] = Array(source.days.length)
	const dataMin = Array(source.days.length)
	const dataMax = Array(source.days.length)
	const data = Array(source.days.length)
	let index = 0
	source.days.forEach((temperatureModel) => {
		labels[index] = temperatureModel.datetime
		dataMin[index] = temperatureModel.tempmin
		dataMax[index] = temperatureModel.tempmax
		data[index] = temperatureModel.temp
		index++
	})

	const datasets = [
		{
			label: 'Minimum',
			data: dataMin,
			borderColor: minTempColor,
			backgroundColor: hexToRGBA(minTempColor, 0.5),
			borderWidth: 2,
		},
		{
			label: 'Maximum',
			data: dataMax,
			borderColor: maxTempColor,
			backgroundColor: hexToRGBA(maxTempColor, 0.5),
			borderWidth: 2,
		},
		{
			label: 'Moyenne',
			data: data,
			borderColor: mediumTempColor,
			backgroundColor: hexToRGBA(mediumTempColor, 0.5),
			borderWidth: 2,
		},
	]

	return {
		labels: labels,
		datasets: datasets,
	}
}
