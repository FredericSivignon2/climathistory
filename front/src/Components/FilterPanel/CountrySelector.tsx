import { FC, ReactElement, useContext, useState } from 'react'
import { CountrySelectorProps, FilterPanelProps } from './types'
import { ThemeProvider } from '@emotion/react'
import { theme } from '../theme'
import { CircularProgress, Container, MenuItem, Select, SelectChangeEvent, Typography } from '@mui/material'
import { useQuery } from '@tanstack/react-query'
import { getAllCountries, getAllTownsByCountry } from '../Api/api'
import { isNil } from '../utils'
import { GlobalData } from '../types'
import { GlobalContext } from '../../App'

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
			<Container sx={{ bgcolor: 'background.default', color: 'text.primary' }}>
				{isNil(allCountries) ? null : (
					<Select
						labelId='demo-simple-select-label'
						id='demo-simple-select'
						value={selectedCountry}
						label='Pays'
						sx={{
							backgroundColor: 'background.default',
							color: 'text.primary',
							'& .MuiOutlinedInput-notchedOutline': {
								borderColor: 'text.primary',
							},
						}}
						onChange={handleChange}>
						{allCountries.map((country: string) => (
							<MenuItem
								key={country}
								value={country}>
								{country}
							</MenuItem>
						))}
					</Select>
				)}
			</Container>
		</ThemeProvider>
	)
}

export default CountrySelector
