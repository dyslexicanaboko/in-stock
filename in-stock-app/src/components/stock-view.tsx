import { Stock } from "@/services/in-stock-api-models";
import { StockV1CreatedModel } from "@/services/in-stock-api-models";

//For now I am going to make this the default component for showing stock, I have no idea what I am doing
export default function StockView(
  stock: Stock | StockV1CreatedModel,
  isEditable?: boolean
) {
  const editable = isEditable ?? false;

  let extraProps;

  if ("createOnUtc" in stock) {
    extraProps = (
      <>
        <div className="grid">
          <label>Created On UTC</label>
          <span>{new Date(stock.createOnUtc).toISOString()}</span>
        </div>
      </>
    );
  } else {
    extraProps = <></>;
  }

  let notes;

  if (editable) {
    notes = <></>;
  } else {
    notes = (
      <div className="grid">
        <label>Notes</label>
        <span>{stock.notes}</span>
      </div>
    );
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
      {notes}
      {extraProps}
    </>
  );
}
