//Should be StockV1GetModel; but API uses an Interface
export type ErrorModel = {
  Code: number;
  Message: string;
  Fields: InvalidArgumentModel[];
};

//https://stackoverflow.com/questions/41385059/possible-to-extend-types-in-typescript
export interface InvalidArgumentModel extends ErrorModel {
  Field: string;
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
  acquiredOnUtc: Date;
  shares: number;
  costBasis: number;
  lowestHeld: number;
  highestHeld: number;
  short: number;
  long: number;
  daysHeld: number;
  averagePrice: number;
  currentPrice: number;
  currentValue: number;
  totalGain: number;
  totalGainRate: number;
  gainPerDay: number;
};

export type PositionV1GetModel = {
  positionId: number;
  stockId: number;
  dateOpenedUtc: Date;
  dateClosedUtc?: Date;
  price: number;
  quantity: number;
};

export type PositionV1CreateModel = {
  stockId: number;
  dateOpenedUtc: Date;
  dateClosedUtc?: Date;
  price: number;
  quantity: number;
};

export type PositionV1PatchModel = {
  dateOpenedUtc: Date;
  dateClosedUtc?: Date;
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
  totalGainRate: number;
  gainPerDay: number;
  isLongPosition: boolean;
  rank: number;
  dateOpenedUtc: Date;
  dateClosedUtc?: Date;
  price: number;
};

export type CoverPositionLossV1Model = {
  desiredSalesPrice: number;
  totalShares: number;
  badShares: number;
  currentPrice: number;
  currentLoss: number;
  proposals: CoverPositionLossProposalV1Model[];
};

export type CoverPositionLossProposalV1Model = {
  proposal: number;
  sharesToBuy: number;
  cost: number;
  sale: number;
  gain: GainV1Model;
};

export type GainV1Model = {
  gain: number;
  gainRate: number;
};

export type ExitPositionWithYieldV1Model = {
  desiredYield: number;
  theoreticalValue: number;
  theoreticalPrice: number;
  theoreticalGain: GainV1Model;
  currentValue: number;
  currentPrice: number;
  currentGain: GainV1Model;
};
