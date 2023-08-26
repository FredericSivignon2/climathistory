import { FC, ReactElement, useContext, useState } from 'react'
import { TabPanelHistoryProps } from './types'
import { Box, Grid } from '@mui/material'
import { TemperatureHistory } from '../TemperatureHistory'
import { GlobalContext } from 'src/App'
import { GlobalData } from '../types'
import { isNil } from '../utils'
import { maxYear, minYear } from '../constants'
import { sxTabPanelBox } from '../theme'

const TabPanelHistory: FC<TabPanelHistoryProps> = (props: TabPanelHistoryProps) => {
	const { country, town } = useContext<GlobalData>(GlobalContext)
	const firstYear = minYear
	const lastYear = maxYear

	return (
		<Box sx={sxTabPanelBox}>
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
						<TemperatureHistory
							country={country}
							town={town}
							defaultYear={firstYear}></TemperatureHistory>
					</Grid>
				</Grid>
			)}
		</Box>
	)
}

export default TabPanelHistory
