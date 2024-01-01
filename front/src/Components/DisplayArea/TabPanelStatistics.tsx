import { FC, ReactElement, useContext, useState } from 'react'
import { TabPanelStatisticsProps } from './types'
import { Box, Grid } from '@mui/material'
import { GlobalContext } from '../../App'
import { GlobalData } from '../types'
import { TemperatureAverageYear } from '../TemperatureAverageYear'
import { TemperatureMinMaxYear } from '../TemperatureMinMaxYear'
import { sxTabPanelBox } from '../theme'
import { isNil } from 'lodash'

const TabPanelStatistics: FC<TabPanelStatisticsProps> = (props: TabPanelStatisticsProps) => {
	const { locationId } = useContext<GlobalData>(GlobalContext)

	return (
		<Box sx={sxTabPanelBox}>
			{isNil(locationId) ? null : (
				<Grid
					container
					rowSpacing={1}
					spacing={0}>
					<Grid
						item
						sm={12}
						lg={12}
						xl={6}>
						<TemperatureAverageYear locationId={locationId}></TemperatureAverageYear>
					</Grid>
					<Grid
						item
						sm={12}
						lg={12}
						xl={6}>
						<TemperatureMinMaxYear locationId={locationId}></TemperatureMinMaxYear>
					</Grid>
				</Grid>
			)}
		</Box>
	)
}

export default TabPanelStatistics
