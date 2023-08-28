//Should be StockV1GetModel; but API uses an Interface
export type ErrorModel = {
	Code: number;
	Message: string;
	Fields: InvalidArgumentModel[]
}

//https://stackoverflow.com/questions/41385059/possible-to-extend-types-in-typescript
export interface InvalidArgumentModel extends ErrorModel {
	Field: string
}

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
	stockId: number;
	symbol: string;
	acquiredOn: Date;
	shares: number;
	costBasis: number;
	lowestHeld: number;
	highestHeld: number;
	short: number;
	long: number;
	daysHeld: number;
	currentPrice: number;
	currentValue: number;
	totalGain: number;
	totalGainPercentage: number;
	gainRate: number
};

export type PositionV1GetModel = {
	positionId: number;
	stockId: number;
	dateOpened: Date;
	dateClosed?: Date;
	price: number;
	quantity: number;
};

export type PositionV1CreateModel = {
	stockId: number;
	dateOpened: Date;
	dateClosed?: Date;
	price: number;
	quantity: number;
};

export type PositionV1PatchModel = {
	dateOpened: Date;
	dateClosed?: Date;
	price: number;
	quantity: number;
};

export type PositionV1GetCalculatedModel = {
	positionId: number;
	shares: number;
	costBasis: number;
	daysHeld: number;
	currentPrice: number;
	currentValue: number;
	totalGain: number;
	totalGainPercentage: number;
	gainRate: number;
	isLongPosition: boolean;
	rank: number;
	dateOpened: Date;
	dateClosed?: Date;
	price: number;
};
