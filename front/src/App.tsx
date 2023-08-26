import React, { createContext, useContext, useState } from 'react'
import './App.css'
import { LocalizationProvider } from '@mui/x-date-pickers'
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'
import { TemperatureHistory } from './Components/TemperatureHistory'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { Box, Container, CssBaseline, Grid, SelectChangeEvent, Tab, Tabs, ThemeProvider } from '@mui/material'
import FilterPanel from './Components/FilterPanel/FilterPanel'
import { sxBody, theme } from './Components/theme'
import Header from './Components/Header/Header'
import { GlobalData } from './Components/types'
import { isNil } from './Components'
import DisplayArea from './Components/DisplayArea/DisplayArea'

const queryClient = new QueryClient()
/*
xs, extra-small: 0px
sm, small: 600px
md, medium: 900px
lg, large: 1200px
xl, extra-large: 1536px
*/

const defaultValue = { country: 'France', setCountry: () => {}, town: 'Paris', setTown: () => {} }
export const GlobalContext = createContext<GlobalData>(defaultValue)

function App() {
	const [selectedCountry, setSelectedCountry] = useState<string>(defaultValue.country)
	const [selectedTown, setSelectedTown] = useState<string | null>(defaultValue.town)

	return (
		<GlobalContext.Provider
			value={{
				country: selectedCountry,
				setCountry: setSelectedCountry,
				town: selectedTown,
				setTown: setSelectedTown,
			}}>
			<ThemeProvider theme={theme}>
				<CssBaseline />
				<QueryClientProvider client={queryClient}>
					<LocalizationProvider dateAdapter={AdapterDayjs}>
						<Header />
						<Box sx={sxBody}>
							<FilterPanel
								defaultTown={selectedTown}
								defaultCountry={selectedCountry}>
								<DisplayArea
									country={selectedCountry}
									town={selectedTown}
								/>
							</FilterPanel>
						</Box>
					</LocalizationProvider>
				</QueryClientProvider>
			</ThemeProvider>
		</GlobalContext.Provider>
	)
}

export default App
