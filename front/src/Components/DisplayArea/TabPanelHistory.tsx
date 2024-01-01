import { FC, ReactElement, useContext, useState } from 'react'
import { TabPanelHistoryProps } from './types'
import { Box, Grid } from '@mui/material'
import { TemperatureHistory } from '../TemperatureHistory'
import { GlobalData } from '../types'
import { maxYear, minYear } from '../constants'
import { sxTabPanelBox } from '../theme'
import { isNil } from 'lodash'
import { GlobalContext } from '../../App'

const TabPanelHistory: FC<TabPanelHistoryProps> = (props: TabPanelHistoryProps) => {
	const { locationId } = useContext<GlobalData>(GlobalContext)
	const firstYear = minYear
	const lastYear = maxYear

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
						<TemperatureHistory
							locationId={locationId}
							defaultYear={firstYear}></TemperatureHistory>
					</Grid>
				</Grid>
			)}
		</Box>
	)
}

export default TabPanelHistory
