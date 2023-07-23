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

export type StockEdit = {
  stockId: number;
  notes?: string;
};
