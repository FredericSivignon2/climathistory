import { FC, ReactElement, useContext, useState } from 'react'
import { CountrySelectorProps, FilterPanelProps } from './types'
import { ThemeProvider } from '@emotion/react'
import { sxSelect, sxSelectContainer, theme } from '../theme'
import {
	CircularProgress,
	Container,
	FormControl,
	InputLabel,
	MenuItem,
	Select,
	SelectChangeEvent,
	Typography,
} from '@mui/material'
import { useQuery } from '@tanstack/react-query'
import { getAllCountries, getAllTownsByCountry } from '../Api/api'
import { isNil } from '../utils'
import { GlobalData } from '../types'
import { GlobalContext } from '../../App'
import { defaultFormControlVariant } from '../constants'

const CountrySelector: FC<CountrySelectorProps> = (props: CountrySelectorProps): ReactElement | null => {
	const [selectedCountry, setSelectedCountry] = useState<string>(props.defaultCountry)

	const {
		isLoading,
		isError,
		data: allCountries,
		error,
	} = useQuery({
		queryKey: ['allCountries'],
		queryFn: () => getAllCountries(),
	})

	const handleChange = (event: SelectChangeEvent) => {
		setSelectedCountry(event.target.value)
		props.onSelectedCountryChange(event.target.value)
	}

	return (
		<ThemeProvider theme={theme}>
			<Container sx={sxSelectContainer}>
				{isNil(allCountries) ? null : (
					<FormControl variant={defaultFormControlVariant}>
						<InputLabel id='labelCountry'>Pays</InputLabel>
						<Select
							labelId='labelCountry'
							id='selectCountry'
							value={selectedCountry}
							label='Pays'
							sx={sxSelect}
							onChange={handleChange}>
							{allCountries.map((country: string) => (
								<MenuItem
									key={country}
									value={country}>
									{country}
								</MenuItem>
							))}
						</Select>
					</FormControl>
				)}
			</Container>
		</ThemeProvider>
	)
}

export default CountrySelector
