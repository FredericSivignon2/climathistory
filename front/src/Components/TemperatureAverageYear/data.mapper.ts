import { ChartData } from 'chart.js'
import { hexToRGBA } from '../utils'
import { copyFile } from 'fs'
import { maxTempColor, mediumTempColor, minTempColor } from '../theme'
import { TemperatureAveragePerYearModel } from '../types'
import { isNil } from 'lodash'

export const getChartData = (source: TemperatureAveragePerYearModel[]): ChartData<'line'> => {
	if (isNil(source) || source.length === 0)
		return {
			labels: [],
			datasets: [],
		}

	const labels: string[] = source.slice(0, source.length - 1).map((x) => x.year.toString())
	const dataMin = source.map((x) => x.averageOfMin)
	const dataMax = source.map((x) => x.averageOfMax)
	const data = source.map((x) => x.averageOfAverage)

	const datasets = [
		{
			label: 'Moyenne des températures minimums',
			data: dataMin,
			borderColor: minTempColor,
			backgroundColor: hexToRGBA(minTempColor, 0.5),
			borderWidth: 2,
		},
		{
			label: 'Moyenne des températures maximums',
			data: dataMax,
			borderColor: maxTempColor,
			backgroundColor: hexToRGBA(maxTempColor, 0.5),
			borderWidth: 2,
		},
		{
			label: 'Moyenne des températures moyennes',
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
