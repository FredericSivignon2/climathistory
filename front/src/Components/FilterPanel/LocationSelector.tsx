import { FC, ReactElement, useContext, useState } from 'react'
import { LocationSelectorProps } from './types'
import { ThemeProvider } from '@emotion/react'
import { sxLocationSelectContainer, sxSelect, sxSelectContainer, theme } from '../theme'
import {
	CircularProgress,
	Container,
	FormControl,
	InputLabel,
	MenuItem,
	Select,
	SelectChangeEvent,
} from '@mui/material'
import { useQuery } from '@tanstack/react-query'
import { getAllCountries, getAllLocationsByCountry } from '../Api/api'
import { GlobalData, LocationModel } from '../types'
import { GlobalContext } from '../../App'
import { needSelection } from '../labels'
import { defaultFormControlVariant } from '../constants'
import { isNil } from 'lodash'

const LocationSelector: FC<LocationSelectorProps> = (props: LocationSelectorProps): ReactElement | null => {
	const [selectedLocationId, setSelectedLocationId] = useState<number | null>(props.locationId)

	const {
		isLoading,
		isError,
		data: allLocations,
		error,
	} = useQuery({
		queryKey: ['allLocations', props.countryId],
		queryFn: () => getAllLocationsByCountry(props.countryId),
	})

	const handleChange = (event: SelectChangeEvent) => {
		// setSelectedLocationId(event.target.value)
		props.onSelectedLocationChange(allLocations?.find((location) => location.name === event.target.value)?.locationId)
	}
	const location = allLocations?.find((location) => location.locationId === props.locationId) ?? null
	return (
		<ThemeProvider theme={theme}>
			<Container sx={sxLocationSelectContainer}>
				<FormControl variant={defaultFormControlVariant}>
					<InputLabel id='labelTown'>Ville</InputLabel>
					<Select
						id='selectTown'
						value={isNil(location) ? '' : location?.name}
						label='Ville'
						labelId='labelTown'
						sx={sxSelect}
						color='error'
						size='small'
						onChange={handleChange}>
						{isNil(allLocations) ? (
							<MenuItem
								key={needSelection}
								value={needSelection}>
								{needSelection}
							</MenuItem>
						) : (
							allLocations.map((town: LocationModel) => (
								<MenuItem
									key={town.name}
									value={town.name}>
									{town.name}
								</MenuItem>
							))
						)}
					</Select>
				</FormControl>
			</Container>
		</ThemeProvider>
	)
}

export default LocationSelector
