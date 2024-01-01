import { SelectChangeEvent } from '@mui/material'
import { ClockNumberProps } from '@mui/x-date-pickers'
import { ReactNode } from 'react'
import { LocationModel } from '../types'

export interface FilterPanelProps {
	children: any
	defaultCountryId: number
	defaultLocationId: number | null
}

export interface CountrySelectorProps {
	countryId: number
	onSelectedCountryChange: (newCountryId: number | undefined) => void
}

export interface LocationSelectorProps {
	countryId: number
	locationId: number | null
	onSelectedLocationChange: (newLocation: number | undefined) => void
}
