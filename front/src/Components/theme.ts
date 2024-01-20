// https://mui.com/system/getting-started/the-sx-prop/

import { Theme, createTheme } from '@mui/material'

export const secondaryColor = '#e5e5e5'
export const maxTempColor = '#b2474a'
export const maxTempColorSecondary = '#fe6569'
export const minTempColor = '#649ac2'
export const minTempColorSecondary = '#83caff'
export const mediumTempColor = '#e8af57'
export const mediumTempColorSecondary = '#ffd55f'

export const theme = createTheme({
	palette: {
		primary: {
			main: '#394264', // Default background for all controls
			light: '#50597b',
		},
		secondary: {
			main: '#ffffff',
		},
		background: {
			paper: '#1f253d', // Page default background
			default: '#1f253d',
		},
		text: {
			primary: '#ffffff',
			secondary: '#828ba6',
		},
		action: {
			active: '#ffffff',
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
					backgroundColor: '#1f253d', // Changer la couleur de fond ici
				},
			},
		},
	},
})

export const sxFilterPanel = {
	display: 'flex',
	width: '100%',
	backgroundColor: (theme: Theme) => theme.palette.primary.light,
	color: (theme: Theme) => theme.palette.text.primary,
}

export const sxBoxDisplayArea = {
	width: '100%',
	margin: '4px',
}

export const sxHorizontalFlex = {
	display: 'flex',
}

export const sxDisplayTabs = {
	width: '100%',
	backgroundColor: (theme: Theme) => theme.palette.primary.light,
	'& .MuiTab-textColorPrimary': {
		color: (theme: Theme) => theme.palette.text.primary,
		backgroundColor: (theme: Theme) => theme.palette.primary.main,
		// backgroundColor: '#ff0000',
	},
	'& .Mui-selected': {
		color: (theme: Theme) => theme.palette.text.primary,
	},
	'& .MuiTab-textSecondaryColor': {
		color: (theme: Theme) => theme.palette.text.primary,
	},
}

export const sxTabPanelBox = {
	backgroundColor: (theme: Theme) => theme.palette.primary.main,
}

export const sxHeaderBox = {
	backgroundColor: (theme: Theme) => theme.palette.primary.main,
}

/* ------------------ HEADER & BODY ------------------ */
export const sxBody = {
	backgroundColor: (theme: Theme) => theme.palette.background.paper,
	maxWidth: '2560px',
	color: (theme: Theme) => theme.palette.text.primary,
	minHeight: '95vh',
	width: '100%',
	display: 'flex',
	flexDirection: 'column',
	alignItems: 'center',
	justifyContent: 'top',
	fontSize: '12px',
}

export const sxHeader = {
	flexGrow: 1,
	backgroundColor: (theme: Theme) => theme.palette.primary.main,
}

/* ------------------ CONTAINERS ------------------ */
export const sxFilterAndChartContainer = {
	border: 2,
	borderColor: (theme: Theme) => theme.palette.text.secondary,
	backgroundColor: (theme: Theme) => theme.palette.primary.main,
	borderRadius: 1,
}

export const sxChartContainer = {
	minHeight: '400px',
	display: 'flex',
	backgroundColor: '#ffffff',
}

/* --------------------- TEXT --------------------- */

export const sxTextField = {
	width: '120px',
}

/* ------------------- CONTROLS ------------------- */

export const sxCompareCheckBox = {
	marginLeft: '24px',
}

export const sxLocationSelectContainer = {
	backgroundColor: (theme: Theme) => theme.palette.primary.light,
	color: (theme: Theme) => theme.palette.text.primary,
	display: 'flex',
	flexDirection: 'row',
	padding: '4px',
	justifyContent: 'center',
}

export const sxSelectContainer = {
	backgroundColor: (theme: Theme) => theme.palette.primary.light,
	color: (theme: Theme) => theme.palette.text.primary,
	display: 'flex',
	flexDirection: 'row',
	padding: '12px',
}

export const sxSelect = {
	backgroundColor: (theme: Theme) => theme.palette.primary.main,
	color: (theme: Theme) => theme.palette.text.primary,
	'& .MuiOutlinedInput-notchedOutline': {
		borderColor: (theme: Theme) => theme.palette.text.primary,
	},
	minWidth: '280px',
}
