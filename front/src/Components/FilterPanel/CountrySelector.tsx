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
import { getAllCountries } from '../Api/api'
import { CountryModel, GlobalData } from '../types'
import { GlobalContext } from '../../App'
import { defaultFormControlVariant } from '../constants'
import { isNil } from 'lodash'
// import flagFrance from '@Assets/flag_france.png'

const CountrySelector: FC<CountrySelectorProps> = (props: CountrySelectorProps): ReactElement | null => {
	const [selectedCountryId, setSelectedCountryId] = useState<number>(props.countryId)

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
		const newCountryId = allCountries?.find((country) => country.name === event.target.value)?.countryId
		setSelectedCountryId(newCountryId ?? 0)
		props.onSelectedCountryChange(newCountryId)
	}

	const country = allCountries?.find((country) => country.countryId === props.countryId) ?? null
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
							value={country?.name ?? ''}
							label='Pays'
							sx={sxSelect}
							size='small'
							onChange={handleChange}>
							{allCountries.map((country: CountryModel) => (
								<MenuItem
									key={country.countryId}
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
