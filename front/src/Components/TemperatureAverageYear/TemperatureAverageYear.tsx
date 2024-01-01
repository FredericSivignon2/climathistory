import React, { FC, useRef, useState } from 'react'
import {
	Chart as ChartJS,
	CategoryScale,
	LinearScale,
	PointElement,
	LineElement,
	BarElement,
	Title,
	Tooltip,
	Legend,
} from 'chart.js'

import { QueryClient, useQuery, useQueryClient } from '@tanstack/react-query'
import { TemperatureAverageYearProps } from './types'
import { DatePicker } from '@mui/x-date-pickers'
import { getAverageTemperaturesPerYear, getTemperatureHistory } from '../Api/api'
import {
	CircularProgress,
	Container,
	FormControl,
	MenuItem,
	Select,
	SelectChangeEvent,
	ThemeProvider,
} from '@mui/material'
import { Chart } from 'chart.js'
import { Bar, Line } from 'react-chartjs-2'
import { getChartData } from './data.mapper'
import annotationPlugin from 'chartjs-plugin-annotation'
import { options } from './chart.options'
import { sxChartContainer, theme } from '../theme'

ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend)
ChartJS.register(annotationPlugin)

const TemperatureAverageYear: FC<TemperatureAverageYearProps> = (props: TemperatureAverageYearProps) => {
	const {
		isLoading,
		isError,
		data: temperatureAverageYearData,
		error,
	} = useQuery({
		queryKey: ['callTempAverateYear', props.locationId],
		queryFn: () => getAverageTemperaturesPerYear(props.locationId),
	})

	const errorMessage = error instanceof Error ? error.message : 'Unknown error'
	const data = temperatureAverageYearData ? getChartData(temperatureAverageYearData) : { labels: [], datasets: [] }
	// <DatePicker />
	return (
		<ThemeProvider theme={theme}>
			<Container sx={sxChartContainer}>
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

export default TemperatureAverageYear
