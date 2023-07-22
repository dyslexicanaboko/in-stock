export type Stock = {
  stockId: number;
  symbol: string;
  name: string;
  createOnUtc: Date;
  notes?: string;
};
