// To make this function a type guard, we add this special return type
export const isNil = (value: any): value is null | undefined => {
	return value == null || value === null || value === undefined || typeof value === 'undefined'
}

export const getRandomInt = (min: number, max: number): number => {
	return Math.floor(Math.random() * (max - min + 1)) + min
}

export const hexToRGBA = (hex: string, alpha: number = 1): string => {
	let r: number, g: number, b: number

	if (hex.length === 7) {
		// if #RRGGBB
		r = parseInt(hex.slice(1, 3), 16)
		g = parseInt(hex.slice(3, 5), 16)
		b = parseInt(hex.slice(5, 7), 16)
	} else if (hex.length === 4) {
		// if #RGB
		r = parseInt(hex[1] + hex[1], 16)
		g = parseInt(hex[2] + hex[2], 16)
		b = parseInt(hex[3] + hex[3], 16)
	} else {
		throw new Error('Invalid color hex string')
	}

	return `rgba(${r}, ${g}, ${b}, ${alpha})`
}
