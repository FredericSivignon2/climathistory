import { FC, ReactElement, useContext, useState } from 'react'
import { TabPanelStatisticsProps } from './types'
import { Box, Grid } from '@mui/material'
import { TemperatureHistory } from '../TemperatureHistory'
import { GlobalContext } from 'src/App'
import { GlobalData } from '../types'
import { isNil } from '../utils'
import { TemperatureAverageYear } from '../TemperatureAverageYear'
import { TemperatureMinMaxYear } from '../TemperatureMinMaxYear'

const TabPanelStatistics: FC<TabPanelStatisticsProps> = (props: TabPanelStatisticsProps) => {
	const { country, town } = useContext<GlobalData>(GlobalContext)

	return (
		<Box>
			{isNil(town) ? null : (
				<Grid
					container
					rowSpacing={1}
					spacing={0}>
					<Grid
						item
						sm={12}
						lg={12}
						xl={6}>
						<TemperatureAverageYear
							country={country}
							town={town}></TemperatureAverageYear>
					</Grid>
					<Grid
						item
						sm={12}
						lg={12}
						xl={6}>
						<TemperatureMinMaxYear
							country={country}
							town={town}></TemperatureMinMaxYear>
					</Grid>
				</Grid>
			)}
		</Box>
	)
}

export default TabPanelStatistics
