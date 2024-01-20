import { Theme } from '@mui/material'

export const sxTemperatureValue = {
	fontSize: '2.5rem',
	m: 2,
}

export const sxTitle = {
	fontSize: '1rem',
	m: 0.5,
}

export const sxBoxTemperatureCard = {
	border: 2,
	borderColor: (theme: Theme) => theme.palette.text.secondary,
	backgroundColor: (theme: Theme) => theme.palette.primary.main,
	borderRadius: 1,
	textAlign: 'center',
}
