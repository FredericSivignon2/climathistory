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
	InputLabel,
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
import { sxSelect, sxSelectContainer, theme } from '../theme'
import { defaultFormControlVariant } from '../constants'

ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend)
ChartJS.register(annotationPlugin)

const TemperatureHistory: FC<TemperatureHistoryProps> = (props: TemperatureHistoryProps) => {
	const [selectedYear, setSelectedYear] = useState<number>(props.defaultYear)
	const [selectedYear2, setSelectedYear2] = useState<number>(1973)
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
	const handleChange2 = (event: SelectChangeEvent) => {
		setSelectedYear2(Number(event.target.value))
	}

	const errorMessage = error instanceof Error ? error.message : 'Unknown error'
	const data = temperatureHistoryData ? getChartData(temperatureHistoryData) : { labels: [], datasets: [] }
	// <DatePicker />
	return (
		<ThemeProvider theme={theme}>
			<FormControl variant={defaultFormControlVariant}>
				<Container sx={sxSelectContainer}>
					<InputLabel id='labelYearOne'>Année 1</InputLabel>
					<Select
						labelId='labelYearOne'
						id='selectYearOne'
						value={selectedYear.toString()}
						label='Année 1'
						sx={sxSelect}
						onChange={handleChange}>
						{years.map((year) => (
							<MenuItem
								key={year}
								value={year}>
								{year}
							</MenuItem>
						))}
					</Select>
					<InputLabel id='labelYearTwo'>Année 2</InputLabel>
					<Select
						labelId='labelYearTwo'
						id='selectYearTwo'
						value={selectedYear2.toString()}
						label='Année 2'
						sx={sxSelect}
						onChange={handleChange2}>
						{years.map((year) => (
							<MenuItem
								key={year}
								value={year}>
								{year}
							</MenuItem>
						))}
					</Select>
				</Container>
			</FormControl>
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
