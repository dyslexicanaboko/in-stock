//Should be StockV1GetModel, but API uses an Interface
export type Stock = {
  stockId: number;
  symbol: string;
  name: string;
  createOnUtc: Date;
  notes?: string;
};

export type SymbolV1Model = {
  symbol: string;
};

export type StockV1CreatedModel = {
  stockId: number;
  symbol: string;
  name: string;
  notes?: string;
};

export type PortfolioV1GetModel = {
	stockId: number,
	symbol: string,
	acquiredOn: Date,
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
