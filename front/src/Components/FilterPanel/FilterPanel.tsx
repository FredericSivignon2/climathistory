import { FC, useContext, useState } from 'react'
import { FilterPanelProps } from './types'
import { isNil } from 'lodash'
import { ThemeProvider } from '@emotion/react'
import { sxFilterAndChartContainer, sxFilterPanel, theme } from '../theme'
import { Box, CircularProgress, Container, FormControl, MenuItem, Select, SelectChangeEvent } from '@mui/material'
import CountrySelector from './CountrySelector'
import { GlobalData } from '../types'
import { GlobalContext } from '../../App'
import LocationSelector from './LocationSelector'
import { defaultCountryId, defaultLocationId } from '../../constants'

const FilterPanel: FC<FilterPanelProps> = (props: FilterPanelProps) => {
	const { countryId, setCountryId, locationId, setLocationId } = useContext<GlobalData>(GlobalContext)

	const handleCountryChange = (newCountry: number | undefined) => {
		if (!isNil(newCountry)) {
			setCountryId(newCountry)
		}
		setLocationId(null)
	}

	const handleTownChange = (newLocationId: number | undefined) => {
		isNil(newLocationId) ? setLocationId(null) : setLocationId(newLocationId)
	}

	return (
		<ThemeProvider theme={theme}>
			<Container sx={sxFilterAndChartContainer}>
				<Box sx={sxFilterPanel}>
					<FormControl
						sx={{ m: 1, minWidth: 120 }}
						size='small'>
						<CountrySelector
							countryId={countryId}
							onSelectedCountryChange={handleCountryChange}
						/>
					</FormControl>
					<FormControl
						sx={{ m: 1, minWidth: 120 }}
						size='small'>
						<LocationSelector
							countryId={countryId}
							locationId={locationId}
							onSelectedLocationChange={handleTownChange}
						/>
					</FormControl>
				</Box>
				{props.children}
			</Container>
		</ThemeProvider>
	)
}

export default FilterPanel
