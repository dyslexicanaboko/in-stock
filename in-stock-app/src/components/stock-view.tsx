import { Stock } from "@/services/in-stock-types";

//For now I am going to make this the default component for showing stock, I have no idea what I am doing
export default function StockView(stock: Stock) {
  return (
    <>
      <h1>Stock</h1>
      <label>StockId</label>&nbsp;<span>{stock.stockId}</span>
      <br />
      <label>Symbol</label>&nbsp;<span>{stock.symbol}</span>
      <br />
      <label>Name</label>&nbsp;<span>{stock.name}</span>
      <br />
      <label>Notes</label>&nbsp;<span>{stock.notes}</span>
      <br />
      <label>Created On UTC</label>&nbsp;
      <span>{new Date(stock.createOnUtc).toISOString()}</span>
    </>
  );
}
