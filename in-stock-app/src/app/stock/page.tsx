import StockView from "@/components/stock-view";
import { getStock } from "@/services/in-stock-api";
import { Stock } from "@/services/in-stock-types";

export default async function Page() {
  let stock: Stock;

  try {
    stock = await getStock();
  } catch (ex) {
    console.log(ex);

    throw ex;
  }

  return StockView(stock);
}
