import { Stock, StockV1CreatedModel } from "@/services/in-stock-types";

//For now I am going to make this the default component for showing stock, I have no idea what I am doing
export default function StockView(stock: Stock | StockV1CreatedModel) {
  let extraProps;

  if ("notes" in stock) {
    extraProps = (
      <>
        <div className="grid">
          <label>Notes</label>
          <span>{stock.notes}</span>
        </div>
        <div className="grid">
          <label>Created On UTC</label>
          <span>{new Date(stock.createOnUtc).toISOString()}</span>
        </div>
      </>
    );
  } else {
    extraProps = <></>;
  }

  //TODO: Need to format this crap properly, not sure how just yet
  return (
    <>
      <div className="grid">
        <label>StockId</label>
        <span>{stock.stockId}</span>
      </div>
      <div className="grid">
        <label>Symbol</label>
        <span>{stock.symbol}</span>
      </div>
      <div className="grid">
        <label>Name</label>
        <span>{stock.name}</span>
      </div>
      {extraProps}
    </>
  );
}
