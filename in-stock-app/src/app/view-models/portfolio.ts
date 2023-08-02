//Not sure if I am using this yet
export type Portfolio = {
	stockId: number,
	symbol: string,
	ownedAsOf: Date,
	shares: number,
	costBasis: number,
	lowestHeld: number,
	highestHeld: number,
	short: number,
	long: number,
	daysHeld: number,
	currentPrice: number,
	currentValue: number,
	totalGain: number,
	totalGainPercentage: number,
	gainRate: number
};
