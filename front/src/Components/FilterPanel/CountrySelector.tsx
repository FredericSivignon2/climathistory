import { FC, ReactElement, useContext, useState } from 'react'
import { CountrySelectorProps, FilterPanelProps } from './types'
import { ThemeProvider } from '@emotion/react'
import { AccessAlarm, ThreeDRotation } from '@mui/icons-material'
import { sxLocationSelectContainer, sxSelect, sxSelectContainer, theme } from '../theme'
import {
	Box,
	CircularProgress,
	Container,
	FormControl,
	InputLabel,
	MenuItem,
	Select,
	SelectChangeEvent,
	SvgIcon,
	SvgIconProps,
	Typography,
} from '@mui/material'
import DeleteTwoToneIcon from '@mui/icons-material/DeleteTwoTone'
import { useQuery } from '@tanstack/react-query'
import { getAllCountries, getAllTownsByCountry } from '../Api/api'
import { isNil } from '../utils'
import { CountryModel, GlobalData } from '../types'
import { GlobalContext } from '../../App'
import { defaultFormControlVariant } from '../constants'
// import flagFrance from '@Assets/flag_france.png'

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
			{/* <img src={flagFrance} /> */}
			<Container sx={sxLocationSelectContainer}>
				{isNil(allCountries) ? null : (
					<FormControl variant={defaultFormControlVariant}>
						<InputLabel id='labelCountry'>Pays</InputLabel>
						<Select
							labelId='labelCountry'
							id='selectCountry'
							value={selectedCountry}
							label='Pays'
							sx={sxSelect}
							size='small'
							onChange={handleChange}>
							{allCountries.map((country: CountryModel) => (
								<MenuItem
									key={country.name}
									value={country.name}>
									{country.name}
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
