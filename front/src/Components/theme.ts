// https://mui.com/system/getting-started/the-sx-prop/

import { Theme, createTheme } from '@mui/material'

export const backgroundColor = '#1f253d'
export const primaryMain = '#394264'
export const primaryLight = '#50597b'
export const chartBackgroundColor = '#ffffff'
export const primaryColor = '#fca311'
export const secondaryColor = '#e5e5e5'
export const accentColor = '#5a189a'
export const textPrimaryColor = '#ffffff'
export const textSecondayColor = '#828ba6'
export const maxTempColor = '#5a189a'
export const minTempColor = '#fca311'
export const mediumTempColor = '#e5e5e5'

export const theme = createTheme({
	palette: {
		primary: {
			main: primaryMain,
			light: primaryLight,
		},
		background: {
			paper: chartBackgroundColor,
			default: backgroundColor,
		},
		text: {
			primary: textPrimaryColor,
			secondary: textSecondayColor,
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
	backgroundColor: (theme: Theme) => theme.palette.background.paper,
	maxWidth: '2560px',
	color: (theme: Theme) => theme.palette.text.primary,
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
	backgroundColor: (theme: Theme) => theme.palette.primary.main,
}

export const sxSelectContainer = {
	bgcolor: (theme: Theme) => theme.palette.primary.main,
	color: (theme: Theme) => theme.palette.text.primary,
	display: 'flex',
	flexDirection: 'row',
}

export const sxSelect = {
	backgroundColor: (theme: Theme) => theme.palette.primary.main,
	color: (theme: Theme) => theme.palette.text.primary,
	'& .MuiOutlinedInput-notchedOutline': {
		borderColor: (theme: Theme) => theme.palette.text.primary,
	},
}

export const sxFilterPanel = {
	display: 'flex',
	width: '100%',
	bgcolor: (theme: Theme) => theme.palette.primary.main,
	color: 'text.primary',
}

export const sxDisplayArea = {
	width: '100%',
}

export const sxDisplayTabs = {
	width: '100%',
	bgcolor: (theme: Theme) => theme.palette.primary.light,
	'& .MuiTab-textColorPrimary': {
		color: (theme: Theme) => theme.palette.text.primary,
		bgcolor: (theme: Theme) => theme.palette.primary.light,
	},
	'& .Mui-selected': {
		color: (theme: Theme) => theme.palette.text.primary,
	},
	'& .MuiTab-textSecondaryColor': {
		color: (theme: Theme) => theme.palette.text.primary,
	},
}
