import { FC, useContext, useState } from 'react'
import { FilterPanelProps } from './types'
import { ThemeProvider } from '@emotion/react'
import { sxFilterPanel, theme } from '../theme'
import { Box, CircularProgress, Container, FormControl, MenuItem, Select, SelectChangeEvent } from '@mui/material'
import { useMutation, useQuery } from '@tanstack/react-query'
import { getAllCountries, getAllTownsByCountry as getAllTownsByCountry } from '../Api/api'
import { isNil } from '../utils'
import CountrySelector from './CountrySelector'
import { GlobalData } from '../types'
import { GlobalContext } from '../../App'
import TownSelector from './TownSelector'
// const [selectedCountry, setSelectedCountry] = useState<string>(props.defaultCountry)
// const [selectedLocation, setSelectedLocation] = useState<string>(props.defaultTown)

const FilterPanel: FC<FilterPanelProps> = (props: FilterPanelProps) => {
	const { country, setCountry, town, setTown } = useContext<GlobalData>(GlobalContext)

	const handleCountryChange = (newCountry: string) => {
		setCountry(newCountry)
		setTown('')
	}

	const handleTownChange = (newTown: string) => {
		setTown(newTown)
	}

	return (
		<ThemeProvider theme={theme}>
			<Box sx={sxFilterPanel}>
				<FormControl
					sx={{ m: 1, minWidth: 120 }}
					size='small'>
					<CountrySelector
						defaultCountry={country}
						onSelectedCountryChange={handleCountryChange}
					/>
				</FormControl>
				<FormControl
					sx={{ m: 1, minWidth: 120 }}
					size='small'>
					<TownSelector
						country={country}
						defaultTown={town}
						onSelectedTownChange={handleTownChange}
					/>
				</FormControl>
			</Box>
		</ThemeProvider>
	)
}

export default FilterPanel
