import { FC, ReactElement, useContext, useState } from 'react'
import { TownSelectorProps } from './types'
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
import { getAllCountries, getAllTownsByCountry } from '../Api/api'
import { isNil } from '../utils'
import { GlobalData, LocationModel } from '../types'
import { GlobalContext } from '../../App'
import { needSelection } from '../labels'
import { defaultFormControlVariant } from '../constants'

const TownSelector: FC<TownSelectorProps> = (props: TownSelectorProps): ReactElement | null => {
	const [selectedTown, setSelectedTown] = useState<string | null>(props.defaultTown)

	const {
		isLoading,
		isError,
		data: allTowns,
		error,
	} = useQuery({
		queryKey: ['allTowns', props.country],
		queryFn: () => getAllTownsByCountry(props.country),
	})

	const handleChange = (event: SelectChangeEvent) => {
		setSelectedTown(event.target.value)
		props.onSelectedTownChange(event.target.value)
	}

	return (
		<ThemeProvider theme={theme}>
			<Container sx={sxLocationSelectContainer}>
				<FormControl variant={defaultFormControlVariant}>
					<InputLabel id='labelTown'>Ville</InputLabel>
					<Select
						id='selectTown'
						value={isNil(selectedTown) ? '' : selectedTown}
						label='Ville'
						labelId='labelTown'
						sx={sxSelect}
						color='error'
						size='small'
						onChange={handleChange}>
						{isNil(allTowns) ? (
							<MenuItem
								key={needSelection}
								value={needSelection}>
								{needSelection}
							</MenuItem>
						) : (
							allTowns.map((town: LocationModel) => (
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

export default TownSelector
