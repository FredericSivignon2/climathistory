import { ChartData } from 'chart.js'
import { hexToRGBA, zeroPad } from '../utils'
import { copyFile } from 'fs'
import {
	maxTempColor,
	maxTempColorSecondary,
	mediumTempColor,
	mediumTempColorSecondary,
	minTempColor,
	minTempColorSecondary,
} from '../theme'
import { YearInfoModel } from '../types'
import { isNil } from 'lodash'
import { format } from 'date-fns'

export const getChartData = (source: YearInfoModel, sourceToCompare: YearInfoModel | undefined): ChartData<'line'> => {
	if (isNil(source) || isNil(source.days) || source.days.length === 0)
		return {
			labels: [],
			datasets: [],
		}

	const labels: string[] = Array(source.days.length)
	const dataMin = Array(source.days.length)
	const dataMax = Array(source.days.length)
	const dataAvg = Array(source.days.length)
	let index = 0
	source.days.forEach((temperatureModel) => {
		// When comparing dates, remove the year in the
		// horizontal axis ticks labels
		const dateToDisplay = isNil(sourceToCompare)
			? format(new Date(temperatureModel.date), 'dd-MM-yyyy')
			: format(new Date(temperatureModel.date), 'dd-MM')

		labels[index] = dateToDisplay
		dataMin[index] = temperatureModel.tempMin
		dataMax[index] = temperatureModel.tempMax
		dataAvg[index] = temperatureModel.tempAvg
		index++
	})

	const datasets = [
		{
			label: 'Minimum ' + source.year,
			data: dataMin,
			borderColor: minTempColor,
			backgroundColor: hexToRGBA(minTempColor, 0.5),
			borderWidth: 1,
		},
		{
			label: 'Maximum ' + source.year,
			data: dataMax,
			borderColor: maxTempColor,
			backgroundColor: hexToRGBA(maxTempColor, 0.5),
			borderWidth: 1,
		},
		{
			label: 'Moyenne ' + source.year,
			data: dataAvg,
			borderColor: mediumTempColor,
			backgroundColor: hexToRGBA(mediumTempColor, 0.5),
			borderWidth: 1,
		},
	]

	if (!isNil(sourceToCompare)) {
		const dataToCompareMin = Array(sourceToCompare.days.length)
		const dataToCompareMax = Array(sourceToCompare.days.length)
		const dataToCompare = Array(sourceToCompare.days.length)
		let index = 0
		sourceToCompare?.days.forEach((temperatureModel) => {
			dataToCompareMin[index] = temperatureModel.tempMin
			dataToCompareMax[index] = temperatureModel.tempMax
			dataToCompare[index] = temperatureModel.tempAvg
			index++
		})

		datasets.push({
			label: 'Minimum ' + sourceToCompare.year,
			data: dataToCompareMin,
			borderColor: minTempColorSecondary,
			backgroundColor: hexToRGBA(minTempColorSecondary, 0.5),
			borderWidth: 1,
		})
		datasets.push({
			label: 'Maximum ' + sourceToCompare.year,
			data: dataToCompareMax,
			borderColor: maxTempColorSecondary,
			backgroundColor: hexToRGBA(maxTempColorSecondary, 0.5),
			borderWidth: 1,
		})
		datasets.push({
			label: 'Moyenne ' + sourceToCompare.year,
			data: dataToCompare,
			borderColor: mediumTempColorSecondary,
			backgroundColor: hexToRGBA(mediumTempColorSecondary, 0.5),
			borderWidth: 1,
		})
	}

	return {
		labels: labels,
		datasets: datasets,
	}
}
