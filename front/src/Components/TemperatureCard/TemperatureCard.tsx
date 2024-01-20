import { FC } from 'react'
import { TemperatureCardProps } from './types'
import { Box, Container, Typography } from '@mui/material'
import { useQuery } from '@tanstack/react-query'
import { isNil } from 'lodash'
import { sxBoxTemperatureCard, sxTemperatureValue, sxTitle } from './styles'

const TemperatureCard: FC<TemperatureCardProps> = ({ value, title }) => {
	return (
		<Container sx={sxBoxTemperatureCard}>
			<Typography sx={sxTemperatureValue}>{`${value.toFixed(2)} °C`}</Typography>
			<Typography sx={sxTitle}>{title}</Typography>
		</Container>
	)
}

export default TemperatureCard
