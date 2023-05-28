import React from 'react'
import './App.css'
import { LocalizationProvider } from '@mui/x-date-pickers'
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'
import { TemperatureHistory } from './Components/TemperatureHistory'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { Grid } from '@mui/material'

const queryClient = new QueryClient()
/*
xs, extra-small: 0px
sm, small: 600px
md, medium: 900px
lg, large: 1200px
xl, extra-large: 1536px
*/

function App() {
	return (
		<QueryClientProvider client={queryClient}>
			<LocalizationProvider dateAdapter={AdapterDayjs}>
				<div className='App'>
					<header className='App-header'>Climat History</header>
					<body className='App-body'>
						<Grid
							container
							rowSpacing={1}
							spacing={0}>
							<Grid
								item
								xl={12}
								lg={12}
								sm={12}>
								<TemperatureHistory
									startDate={new Date()}
									endDate={new Date()}
									location={{ x: 0, y: 0 }}></TemperatureHistory>
							</Grid>
							<Grid
								item
								xl={4}
								lg={6}
								sm={12}>
								Other graph 1
								{/* <TemperatureHistory
									startDate={new Date()}
									endDate={new Date()}
									location={{ x: 0, y: 0 }}></TemperatureHistory> */}
							</Grid>
							<Grid
								item
								xl={4}
								lg={6}
								sm={12}>
								Other graph 2
								{/* <TemperatureHistory
									startDate={new Date()}
									endDate={new Date()}
									location={{ x: 0, y: 0 }}></TemperatureHistory> */}
							</Grid>
						</Grid>
					</body>
				</div>
			</LocalizationProvider>
		</QueryClientProvider>
	)
}

export default App
