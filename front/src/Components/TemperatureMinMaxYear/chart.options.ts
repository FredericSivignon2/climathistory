import { Theme } from '@mui/material'
import { maxTempColor, minTempColor } from '../theme'

export const options = {
	responsive: true,
	maintainAspectRatio: false,
	pointStyle: false,
	plugins: {
		legend: {
			position: 'top' as const,
			textColor: (theme: Theme) => theme.palette.text.primary,
		},
		title: {
			display: true,
			textColor: (theme: Theme) => theme.palette.text.primary,
			text: 'Températures minimum et maxium par année',
		},
		annotation: {
			annotations: {
				lowTemperatureLine: {
					type: 'line' as 'line',
					yMin: 0,
					yMax: 0,
					borderColor: 'rgba(150, 200, 255, 0.8)',
					borderWidth: 1,
				},
				highTemperatureLine: {
					type: 'line' as 'line',
					yMin: 30,
					yMax: 30,
					borderColor: 'rgba(255, 10, 80, 0.9)',
					borderWidth: 1,
				},
				lowTemperatureBox: {
					type: 'box' as 'box',
					adjustScaleRange: false,
					borderWidth: 0,
					xMin: 1,
					xMax: 365,
					yMin: -50,
					yMax: 0,
					backgroundColor: 'rgba(150, 200, 255, 0.1)',
				},
				highTemperatureBox: {
					type: 'box' as 'box',
					adjustScaleRange: false,
					borderWidth: 0,
					xMin: 1,
					xMax: 365,
					yMin: 30,
					yMax: 60,
					backgroundColor: 'rgba(255, 10, 80, 0.1)',
				},
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
	// scales: {
	// 	y: {
	// 		min: -20,
	// 		max: 50,
	// 	},
	// },
}
