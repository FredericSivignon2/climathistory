// https://mui.com/system/getting-started/the-sx-prop/

import { Theme, createTheme } from '@mui/material'

export const backgroundColor = '#14213d'
export const chartBackgroundColor = '#ffffff'
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
			paper: chartBackgroundColor,
			default: backgroundColor,
		},
		text: {
			primary: textColor,
			secondary: secondaryColor,
		},
		action: {
			active: accentColor,
		},
	},
	components: {
		MuiCssBaseline: {
			styleOverrides: {
				body: {
					maxWidth: '2560px',
				},
			},
		},
		MuiMenu: {
			styleOverrides: {
				paper: {
					backgroundColor: backgroundColor, // Changer la couleur de fond ici
				},
			},
		},
	},
})

export const sxBody = {
	// bgcolor: 'background.default',
	backgroundColor: (theme: Theme) => theme.palette.background.paper,
	maxWidth: '2560px',
	color: 'text.primary',
	minHeight: '95vh',
	width: '100%',
	display: 'flex',
	flexDirection: 'column',
	alignItems: 'center',
	justifyContent: 'center',
	fontSize: '12px',
}

export const sxHeader = {
	flexGrow: 1,
	backgroundColor: (theme: Theme) => theme.palette.background.default,
}

export const sxFilterPanel = {
	display: 'flex',
	width: '100%',
	bgcolor: 'background.default',
	color: 'text.primary',
}

export const sxDisplayArea = {
	width: '100%',
}

export const sxDisplayTabs = {
	width: '100%',
}
