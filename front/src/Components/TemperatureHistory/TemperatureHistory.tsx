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

import { QueryClient, useQueries, useQuery, useQueryClient } from '@tanstack/react-query'
import { TemperatureHistoryProps } from './types'
import { DatePicker } from '@mui/x-date-pickers'
import { getTemperatureHistory } from '../Api/api'
import {
	Checkbox,
	CircularProgress,
	Container,
	FormControl,
	FormControlLabel,
	InputLabel,
	MenuItem,
	Select,
	SelectChangeEvent,
	TextField,
	ThemeProvider,
} from '@mui/material'
import { getDefaultYearToCompare, getRandomInt } from '../utils'
import { Chart } from 'chart.js'
import { Line } from 'react-chartjs-2'
import { getChartData } from './data.mapper'
import annotationPlugin from 'chartjs-plugin-annotation'
import { options } from './chart.options'
import { sxChartContainer, sxCompareCheckBox, sxSelect, sxSelectContainer, sxTextField, theme } from '../theme'
import { defaultFormControlVariant, maxYear, minYear } from '../constants'
import { isNil } from 'lodash'

ChartJS.register(CategoryScale, LinearScale, PointElement, LineElement, Title, Tooltip, Legend)
ChartJS.register(annotationPlugin)

const TemperatureHistory: FC<TemperatureHistoryProps> = (props: TemperatureHistoryProps) => {
	const [selectedYear, setSelectedYear] = useState<number>(props.defaultYear)
	const [selectedYearToCompare, setSelectedYearToCompare] = useState<number>(getDefaultYearToCompare(props.defaultYear))
	const [compareChecked, setCompareChecked] = useState<boolean>(false)
	let years = []
	for (let year = minYear; year <= maxYear; year++) {
		years.push(year)
	}

	const selectedYears = [selectedYear]
	if (compareChecked) {
		selectedYears.push(selectedYearToCompare)
	}

	// const {
	// 	isLoading,
	// 	isError,
	// 	data: temperatureHistoryData,
	// 	error,
	// } = useQuery({
	// 	queryKey: ['callTempHisto', props.locationId, selectedYear],
	// 	queryFn: () => getTemperatureHistory(props.locationId, selectedYear),
	// })

	const results = useQueries({
		queries: selectedYears.map((year) => ({
			queryKey: ['callTempHisto', props.locationId, year],
			queryFn: () => getTemperatureHistory(props.locationId, year),
		})),
	})

	const handleChange = (event: React.ChangeEvent<HTMLInputElement>) => {
		setSelectedYear(Number(event.target.value))
	}
	const handleChange2 = (event: React.ChangeEvent<HTMLInputElement>) => {
		setSelectedYearToCompare(Number(event.target.value))
	}

	const handleCompareChange = (event: React.ChangeEvent<HTMLInputElement>) => {
		setCompareChecked(event.target.checked)
	}

	let isLoading = false,
		isError = false,
		error: unknown
	results.forEach((result) => {
		isLoading = isLoading || result.isLoading
		isError = isError || result.isError
		if (isNil(error)) {
			error = result.error
		}
	})

	const errorMessage = error instanceof Error ? error.message : 'Unknown error'
	const data = results[0].data
		? getChartData(results[0].data, results.length > 1 ? results[1].data : undefined)
		: { labels: [], datasets: [] }
	// <DatePicker />
	return (
		<ThemeProvider theme={theme}>
			<FormControl variant={defaultFormControlVariant}>
				<Container sx={sxSelectContainer}>
					<TextField
						id='standard-select-currency'
						select
						label='Année'
						size='small'
						defaultValue={selectedYear.toString()}
						onChange={handleChange}
						variant={defaultFormControlVariant}
						sx={sxTextField}
						// helperText="Sélectionnez l'année "
					>
						{years.map((year) => (
							<MenuItem
								key={year}
								value={year}>
								{year}
							</MenuItem>
						))}
					</TextField>
					<FormControlLabel
						control={
							<Checkbox
								defaultChecked
								color='secondary'
								size='small'
								checked={compareChecked}
								sx={sxCompareCheckBox}
								onChange={handleCompareChange}
							/>
						}
						label="comparer avec l'année "
					/>
					<TextField
						id='standard-select-currency'
						select
						label='Année comparée'
						size='small'
						defaultValue={selectedYearToCompare.toString()}
						onChange={handleChange2}
						variant={defaultFormControlVariant}
						disabled={!compareChecked}
						sx={sxTextField}
						// helperText="Sélectionnez l'année "
					>
						{years.map((year) => (
							<MenuItem
								key={year}
								value={year}>
								{year}
							</MenuItem>
						))}
					</TextField>
				</Container>
			</FormControl>
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

export default TemperatureHistory
