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
import { TemperatureHistoryProps } from './types'
import { DatePicker } from '@mui/x-date-pickers'
import { getTemperatureHistory } from '../Api/api'
import {
	CircularProgress,
	Container,
	FormControl,
	MenuItem,
	Select,
	SelectChangeEvent,
	ThemeProvider,
} from '@mui/material'
import { getRandomInt, isNil } from '../utils'
import { Chart } from 'chart.js'
import { Line } from 'react-chartjs-2'
import { getChartData } from './data.mapper'
import annotationPlugin from 'chartjs-plugin-annotation'
import { options } from './chart.options'
import { theme } from '../theme'

ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend)
ChartJS.register(annotationPlugin)

const TemperatureHistory: FC<TemperatureHistoryProps> = (props: TemperatureHistoryProps) => {
	const [selectedYear, setSelectedYear] = useState<number>(props.defaultYear)
	let years = []
	for (let year = 1973; year <= 2023; year++) {
		years.push(year)
	}

	const {
		isLoading,
		isError,
		data: temperatureHistoryData,
		error,
	} = useQuery({
		queryKey: ['callTempHisto', selectedYear, props.town],
		queryFn: () => getTemperatureHistory(props.country, props.town, selectedYear),
	})

	const handleChange = (event: SelectChangeEvent) => {
		setSelectedYear(Number(event.target.value))
	}

	const errorMessage = error instanceof Error ? error.message : 'Unknown error'
	const data = temperatureHistoryData ? getChartData(temperatureHistoryData) : { labels: [], datasets: [] }
	// <DatePicker />
	return (
		<ThemeProvider theme={theme}>
			<Container sx={{ bgcolor: 'background.default', color: 'text.primary' }}>
				<FormControl
					sx={{ m: 1, minWidth: 120 }}
					size='small'>
					<Select
						labelId='demo-simple-select-label'
						id='demo-simple-select'
						value={selectedYear.toString()}
						label='Année'
						sx={{
							bgcolor: 'background.default',
							color: 'text.primary',
							'& .MuiOutlinedInput-notchedOutline': {
								borderColor: 'text.primary',
							},
						}}
						onChange={handleChange}>
						{years.map((year) => (
							<MenuItem
								key={year}
								value={year}>
								{year}
							</MenuItem>
						))}
					</Select>
				</FormControl>
			</Container>
			<Container
				sx={{
					minHeight: '800px',
					display: 'flex',
					bgColor: 'background.default',
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
		</ThemeProvider>
	)
}

export default TemperatureHistory
