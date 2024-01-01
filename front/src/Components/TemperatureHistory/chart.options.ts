import { Theme } from '@mui/material'
import { maxTempColor, maxTempColorSecondary, minTempColor, minTempColorSecondary } from '../theme'
import { ChartData, ChartEvent, LegendElement, LegendItem } from 'chart.js'
import { getChartData } from '../TemperatureAverageYear/data.mapper'
import { isNil } from 'lodash'

const newLegendClickHandler = function (e: ChartEvent, legendItem: LegendItem, legend: LegendElement<'line'>) {
	const index = legendItem.datasetIndex
	const ci = legend.chart
	if (isNil(index)) return

	// If clicked on a maximum/minimum/medium, show values for both years
	// (e.g. if clicked on maximum on first year, also show maximum for second year and hide all others)
	if (!isNil(ci.legend) && !isNil(ci.legend.legendItems)) {
		const clickedLegendText = ci.legend.legendItems[index].text.split(' ')[0]
		for (let i = 0; i < ci.legend.legendItems.length; i++) {
			const curLegendText = ci.legend.legendItems[i].text.split(' ')[0]
			if (clickedLegendText === curLegendText) {
				ci.show(i)
			} else {
				ci.hide(i)
			}
		}
	}

	// DEFAULT BEHAVIOR
	// if (ci.isDatasetVisible(index)) {
	// 	ci.hide(index)
	// 	legendItem.hidden = true
	// } else {
	// 	ci.show(index)
	// 	legendItem.hidden = false
	// }
}

export const options = {
	responsive: true,
	maintainAspectRatio: false,
	pointStyle: false,
	plugins: {
		legend: {
			position: 'top' as const,
			textColor: (theme: Theme) => theme.palette.text.primary,

			onClick: newLegendClickHandler,
			labels: {
				// Hide specific legends by default
				// filter: (legendItem: LegendItem, chartData: ChartData) => {
				// 	// Change 'Dataset 1' to the label of the dataset you want to hide by default
				// 	if (!legendItem.text.startsWith('Maximum')) {
				// 		legendItem.hidden = true
				// 	}
				// 	return true
				// },
			},
		},
		title: {
			display: true,
			textColor: (theme: Theme) => theme.palette.text.primary,
			text: "Températures de l'année",
		},
		annotation: {
			annotations: {
				lowTemperatureLine: {
					type: 'line' as 'line',
					yMin: 0,
					yMax: 0,
					borderColor: minTempColorSecondary,
					borderWidth: 1,
				},
				highTemperatureLine: {
					type: 'line' as 'line',
					yMin: 30,
					yMax: 30,
					borderColor: maxTempColorSecondary,
					borderWidth: 1,
				},
				// lowTemperatureBox: {
				// 	type: 'box' as 'box',
				// 	adjustScaleRange: false,
				// 	borderWidth: 0,
				// 	xMin: 1,
				// 	xMax: 365,
				// 	yMin: -50,
				// 	yMax: 0,
				// 	backgroundColor: 'rgba(150, 200, 255, 0.1)',
				// },
				// highTemperatureBox: {
				// 	type: 'box' as 'box',
				// 	adjustScaleRange: false,
				// 	borderWidth: 0,
				// 	xMin: 1,
				// 	xMax: 365,
				// 	yMin: 30,
				// 	yMax: 60,
				// 	backgroundColor: 'rgba(255, 10, 80, 0.1)',
				// },
			},
		},
	},
	xAxes: [
		{
			type: 'string',
			ticks: {
				autoSkip: true,
				maxTicksLimit: 10,
			},
		},
	],
	scales: {
		y: {
			min: -20,
			max: 50,
		},
	},
}
