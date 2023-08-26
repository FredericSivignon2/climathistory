import { SelectChangeEvent } from '@mui/material'
import { ReactNode } from 'react'

export interface FilterPanelProps {
	children: any
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
