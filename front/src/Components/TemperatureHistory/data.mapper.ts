import { ChartData } from 'chart.js'
import { hexToRGBA, isNil, zeroPad } from '../utils'
import { copyFile } from 'fs'
import {
	maxTempColor,
	maxTempColorSecondary,
	mediumTempColor,
	mediumTempColorSecondary,
	minTempColor,
	minTempColorSecondary,
} from '../theme'
import { TownTemperaturesPerYearModel } from './types'

export const getChartData = (
	source: TownTemperaturesPerYearModel,
	sourceToCompare: TownTemperaturesPerYearModel | undefined
): ChartData<'line'> => {
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
		let dateToDisplay = temperatureModel.datetime
		if (!isNil(sourceToCompare)) {
			// When comparing dates, remove the year in the
			// horizontal axis ticks labels
			dateToDisplay = dateToDisplay.substring(5)
		}

		labels[index] = dateToDisplay
		dataMin[index] = temperatureModel.tempmin
		dataMax[index] = temperatureModel.tempmax
		data[index] = temperatureModel.temp
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
			data: data,
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
		sourceToCompare.days.forEach((temperatureModel) => {
			dataToCompareMin[index] = temperatureModel.tempmin
			dataToCompareMax[index] = temperatureModel.tempmax
			dataToCompare[index] = temperatureModel.temp
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
