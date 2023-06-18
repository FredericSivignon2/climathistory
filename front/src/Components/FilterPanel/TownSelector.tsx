import { FC, ReactElement, useContext, useState } from 'react'
import { TownSelectorProps } from './types'
import { ThemeProvider } from '@emotion/react'
import { theme } from '../theme'
import { CircularProgress, Container, MenuItem, Select, SelectChangeEvent } from '@mui/material'
import { useQuery } from '@tanstack/react-query'
import { getAllCountries, getAllTownsByCountry } from '../Api/api'
import { isNil } from '../utils'
import { GlobalData } from '../types'
import { GlobalContext } from '../../App'
import { needSelection } from '../labels'

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
			<Container sx={{ bgcolor: 'background.default', color: 'text.primary' }}>
				<Select
					labelId='demo-simple-select-label'
					id='demo-simple-select'
					value={isNil(selectedTown) ? '' : selectedTown}
					label='Ville'
					sx={{
						backgroundColor: 'background.default',
						color: 'text.primary',
						'& .MuiOutlinedInput-notchedOutline': {
							borderColor: 'text.primary',
						},
					}}
					onChange={handleChange}>
					{isNil(allTowns) ? (
						<MenuItem
							key={needSelection}
							value={needSelection}>
							{needSelection}
						</MenuItem>
					) : (
						allTowns.map((town: string) => (
							<MenuItem
								key={town}
								value={town}>
								{town}
							</MenuItem>
						))
					)}
				</Select>
			</Container>
		</ThemeProvider>
	)
}

export default TownSelector
