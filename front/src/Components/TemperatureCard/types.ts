import { TemperatureModel } from '../types'

export interface TemperatureCardProps {
	title: string
	getTemperatureValueCallback: () => Promise<TemperatureModel>
}
