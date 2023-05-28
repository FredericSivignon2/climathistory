import { FC, useState } from 'react'
import { FilterPanelProps } from './types'
import { ThemeProvider } from '@emotion/react'
import { theme } from '../theme'
import { CircularProgress, Container, MenuItem, Select, SelectChangeEvent } from '@mui/material'
import { useQuery } from '@tanstack/react-query'
import { getAllLocations } from '../Api/api'
import { isNil } from '../utils'

const FilterPanel: FC<FilterPanelProps> = (props: FilterPanelProps) => {
	const [selectedLocation, setSelectedLocation] = useState<string>(props.defaultTown)

	const {
		isLoading,
		isError,
		data: allLocations,
		error,
	} = useQuery({
		queryKey: ['allTowns'],
		queryFn: () => getAllLocations(),
	})

	const handleChange = (event: SelectChangeEvent) => {
		setSelectedLocation(event.target.value)
		console.log('[301] New location: ' + event.target.value)
		props.onLocationChange(event.target.value)
	}

	return (
		<ThemeProvider theme={theme}>
			<Container sx={{ bgcolor: 'background.paper', color: 'text.primary' }}>
				{isNil(allLocations) ? null : (
					<Select
						labelId='demo-simple-select-label'
						id='demo-simple-select'
						value={selectedLocation}
						label='Ville'
						sx={{
							bgcolor: 'background.paper',
							color: 'text.primary',
							'& .MuiOutlinedInput-notchedOutline': {
								borderColor: 'text.primary',
							},
						}}
						onChange={handleChange}>
						{allLocations.map((town: string) => (
							<MenuItem
								key={town}
								value={town}>
								{town}
							</MenuItem>
						))}
					</Select>
				)}
			</Container>
		</ThemeProvider>
	)
}

export default FilterPanel
