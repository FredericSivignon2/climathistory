// https://mui.com/system/getting-started/the-sx-prop/

import { createTheme } from '@mui/material'

export const backgroundColor = '#14213d'
export const primaryColor = '#fca311'
export const secondaryColor = '#e5e5e5'
export const accentColor = '#5a189a'
export const textColor = '#ffffff'
export const maxTempColor = '#5a189a'
export const minTempColor = '#fca311'
export const mediumTempColor = '#e5e5e5'

export const theme = createTheme({
	palette: {
		background: {
			paper: backgroundColor,
		},
		text: {
			primary: textColor,
			secondary: secondaryColor,
		},
		action: {
			active: accentColor,
		},
	},
})
