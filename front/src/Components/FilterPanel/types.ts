import { SelectChangeEvent } from '@mui/material'

export interface FilterPanelProps {
	defaultTown: string
	onLocationChange: (newLocation: string) => void
}
