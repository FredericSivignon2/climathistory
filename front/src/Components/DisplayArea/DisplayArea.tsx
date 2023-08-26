import { Box, Grid, Tab, Tabs } from '@mui/material'
import { DisplayAreaProps } from './types'
import { FC, useState } from 'react'
import { TemperatureHistory } from '../TemperatureHistory'
import { isNil } from '../utils'
import TabPanelHistory from './TabPanelHistory'
import TabPanelStatistics from './TabPanelStatistics'
import { sxBody, sxBoxDisplayArea, sxDisplayTabs } from '../theme'

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

	const handleTabChange = (event: React.SyntheticEvent, newValue: number) => {
		setSelectedTab(newValue)
	}

	return (
		<Box sx={sxBoxDisplayArea}>
			<Tabs
				sx={sxDisplayTabs}
				textColor='secondary'
				value={selectedTab}
				onChange={handleTabChange}
				aria-label='chart type tabs'>
				<Tab label={tabHistorique} />
				<Tab label={tabStatistics} />
			</Tabs>
			{selectedTab === 0 && <TabPanelHistory />}
			{selectedTab === 1 && <TabPanelStatistics />}
		</Box>
	)
}

export default DisplayArea
