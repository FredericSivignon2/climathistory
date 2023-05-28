import React, { FC, useRef, useState } from 'react'
import {
	Chart as ChartJS,
	CategoryScale,
	LinearScale,
	PointElement,
	LineElement,
	Title,
	Tooltip,
	Legend,
} from 'chart.js'

import { QueryClient, useQuery, useQueryClient } from '@tanstack/react-query'
import { TemperatureHistoryDto, TemperatureHistoryProps } from './types'
import { DatePicker } from '@mui/x-date-pickers'
import { getTemperatureHistory } from '../Api/api'
import { CircularProgress, Container, MenuItem, Select, SelectChangeEvent } from '@mui/material'
import { getRandomInt, isNil } from '../utils'
import { Chart } from 'chart.js'
import { Line } from 'react-chartjs-2'
import { getChartData } from './data.mapper'
import { backgroundColor, textColor } from '../colors'

ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend)

export const options = {
	responsive: true,
	maintainAspectRatio: false,
	pointStyle: false,
	plugins: {
		legend: {
			position: 'top' as const,
			textColor: textColor,
		},
		title: {
			display: true,
			textColor: textColor,
			text: "Températures de l'année",
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
}

const TemperatureHistory: FC<TemperatureHistoryProps> = (props: TemperatureHistoryProps) => {
	const [selectedYear, setSelectedYear] = useState<number>(2022)
	let years = []
	for (let year = 1973; year <= 2022; year++) {
		years.push(year)
	}

	const {
		isLoading,
		isError,
		data: temperatureHistoryData,
		error,
	} = useQuery({
		queryKey: ['callTempHisto', selectedYear],
		queryFn: () => getTemperatureHistory(selectedYear),
	})

	console.log('Rendering...')

	const handleChange = (event: SelectChangeEvent) => {
		setSelectedYear(Number(event.target.value))
	}

	const errorMessage = error instanceof Error ? error.message : 'Unknown error'
	const data = temperatureHistoryData ? getChartData(temperatureHistoryData) : { labels: [], datasets: [] }
	// <DatePicker />
	return (
		<>
			<Container style={{ backgroundColor: backgroundColor, color: textColor }}>
				<Select
					labelId='demo-simple-select-label'
					id='demo-simple-select'
					value={selectedYear.toString()}
					label='Année'
					style={{ backgroundColor: backgroundColor, color: textColor }}
					onChange={handleChange}>
					{years.map((year) => (
						<MenuItem
							key={year}
							value={year}>
							{year}
						</MenuItem>
					))}
				</Select>
			</Container>
			<Container
				style={{
					minHeight: '600px',
					display: 'flex',
					backgroundColor: backgroundColor,
				}}>
				{isLoading ? (
					<CircularProgress />
				) : isError ? (
					<span>Error: errorMessage</span>
				) : (
					<Line
						options={options}
						data={data}
					/>
				)}
			</Container>
		</>
	)
}

export default TemperatureHistory
