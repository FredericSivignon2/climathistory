import { Box, Container, Grid, Tab, Tabs } from '@mui/material'
import { DisplayAreaProps } from './types'
import { FC, useContext, useState } from 'react'
import TabPanelHistory from './TabPanelHistory'
import TabPanelStatistics from './TabPanelStatistics'
import { sxBoxDisplayArea, sxDisplayTabs, sxHorizontalFlex } from '../theme'
import { TemperatureCard } from '../TemperatureCard'
import { getAverageTemperatureByDateRange } from '../Api/api'
import { GlobalContext } from '../../App'
import { GlobalData } from '../types'
import { isNil } from 'lodash'
import { useQueries } from '@tanstack/react-query'

const tabHistorique = 'Historique'
const tabStatistics = 'Statistiques'

function a11yProps(index: number) {
	return {
		id: `simple-tab-${index}`,
		'aria-controls': `simple-tabpanel-${index}`,
	}
}

const DisplayArea: FC<DisplayAreaProps> = (props: DisplayAreaProps) => {
	const [selectedTab, setSelectedTab] = useState<number>(0)
	const { locationId } = useContext<GlobalData>(GlobalContext)

	const handleTabChange = (event: React.SyntheticEvent, newValue: number) => {
		setSelectedTab(newValue)
	}

	const results = useQueries({
		queries: [
			{
				queryKey: ['getAverageTemperatureByDateRange1', locationId],
				queryFn: () => getAverageTemperatureByDateRange(locationId, new Date(1973, 0, 1), new Date(1983, 11, 31)),
			},
			{
				queryKey: ['getAverageTemperatureByDateRange2', locationId],
				queryFn: () => getAverageTemperatureByDateRange(locationId, new Date(2013, 0, 1), new Date(2023, 11, 31)),
			},
		],
	})

	const tmpApi = (id: number) => 23.2
	return (
		<Box sx={sxBoxDisplayArea}>
			<Container sx={sxHorizontalFlex}>
				<TemperatureCard
					value={results[0].data?.value ?? NaN}
					title='Moyenne des températures sur la période 1973-1983'></TemperatureCard>
				<TemperatureCard
					value={results[1].data?.value ?? NaN}
					title='Moyenne des températures sur la période 2013-2023'></TemperatureCard>
			</Container>
			<TabPanelStatistics />
			<TabPanelHistory />
		</Box>
	)
}

export default DisplayArea
