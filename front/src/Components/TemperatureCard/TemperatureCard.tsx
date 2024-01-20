import { FC } from 'react'
import { TemperatureCardProps } from './types'
import { Box, Container, Typography } from '@mui/material'
import { useQuery } from '@tanstack/react-query'
import { isNil } from 'lodash'
import { sxBoxTemperatureCard, sxTemperatureValue, sxTitle } from './styles'

const TemperatureCard: FC<TemperatureCardProps> = ({ getTemperatureValueCallback, title }) => {
	const {
		isLoading,
		isError,
		data: temperatureValue,
		error,
	} = useQuery({
		queryKey: ['callBack' + title],
		queryFn: () => getTemperatureValueCallback(),
	})

	return (
		<Container sx={sxBoxTemperatureCard}>
			<Typography sx={sxTemperatureValue}>{`${temperatureValue?.value.toFixed(2)} °C`}</Typography>
			<Typography sx={sxTitle}>{title}</Typography>
		</Container>
	)
}

export default TemperatureCard
