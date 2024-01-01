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
import DisplayArea from './Components/DisplayArea/DisplayArea'
import { defaultCountryId, defaultLocationId } from './constants'

const queryClient = new QueryClient()
/*
xs, extra-small: 0px
sm, small: 600px
md, medium: 900px
lg, large: 1200px
xl, extra-large: 1536px
*/

const defaultValue = {
	countryId: defaultCountryId,
	setCountryId: () => {},
	locationId: defaultLocationId,
	setLocationId: () => {},
}
export const GlobalContext = createContext<GlobalData>(defaultValue)

function App() {
	const [selectedCountryId, setSelectedCountryId] = useState<number>(defaultValue.countryId)
	const [selectedLocationId, setSelectedLocationId] = useState<number | null>(defaultValue.locationId)

	return (
		<GlobalContext.Provider
			value={{
				countryId: selectedCountryId,
				setCountryId: setSelectedCountryId,
				locationId: selectedLocationId,
				setLocationId: setSelectedLocationId,
			}}>
			<ThemeProvider theme={theme}>
				<CssBaseline />
				<QueryClientProvider client={queryClient}>
					<LocalizationProvider dateAdapter={AdapterDayjs}>
						<Header />
						<Box sx={sxBody}>
							<FilterPanel
								defaultLocationId={selectedLocationId}
								defaultCountryId={selectedCountryId}>
								<DisplayArea
									countryId={selectedCountryId}
									locationId={selectedLocationId}
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
