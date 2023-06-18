import { ChartData } from 'chart.js'
import { hexToRGBA, isNil } from '../utils'
import { copyFile } from 'fs'
import { maxTempColor, mediumTempColor, minTempColor } from '../theme'
import { TemperatureMinMaxPerYearModel } from './types'

export const getChartData = (source: TemperatureMinMaxPerYearModel[]): ChartData<'line'> => {
	if (isNil(source) || source.length === 0)
		return {
			labels: [],
			datasets: [],
		}

	const labels: string[] = source.slice(0, source.length - 1).map((x) => x.year.toString())
	const dataMin = source.map((x) => x.min)
	const dataMax = source.map((x) => x.max)

	const datasets = [
		{
			label: "Température minimum de l'année",
			data: dataMin,
			borderColor: minTempColor,
			backgroundColor: hexToRGBA(minTempColor, 0.5),
			borderWidth: 2,
			// Ne fonctionne pas, à voir (calculer soi même ?)
			trendlineLinear: {
				style: 'rgba(255,105,180, .8)',
				lineStyle: 'dotted|solid',
				width: 2,
			},
		},
		{
			label: "Température maximum de l'année",
			data: dataMax,
			borderColor: maxTempColor,
			backgroundColor: hexToRGBA(maxTempColor, 0.5),
			borderWidth: 2,
			trendlineLinear: {
				style: 'rgba(255,105,180, .8)',
				lineStyle: 'dotted|solid',
				width: 2,
			},
		},
	]

	return {
		labels: labels,
		datasets: datasets,
	}
}
