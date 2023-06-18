import { SelectChangeEvent } from '@mui/material'

export interface FilterPanelProps {
	defaultCountry: string
	defaultTown: string | null
}

export interface CountrySelectorProps {
	defaultCountry: string
	onSelectedCountryChange: (newTown: string) => void
}

export interface TownSelectorProps {
	country: string
	defaultTown: string | null
	onSelectedTownChange: (newTown: string) => void
}
