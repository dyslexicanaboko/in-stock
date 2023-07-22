"use client";

import StockView from "@/components/stock-view";
import { getStock, createStock } from "@/services/in-stock-api";
import { Stock } from "@/services/in-stock-types";
import { useCallback, useState } from "react";

export default function Page() {
  const [symbol, setSymbol] = useState("");
  const [view, setView] = useState<JSX.Element>();

  const handleCreate = useCallback((): void => {
    createStock(symbol).then((model) => {
      setView(StockView(model));
    });

    setView(<button aria-busy="true">Please wait…</button>);
  }, [symbol]);

  const emptyCreateForm = useCallback((): JSX.Element => {
    return (
      <>
        <article className="container">
          <h1>Enter symbol to add</h1>
          <label htmlFor="symbol">
            Symbol
            <input type="text" onChange={(e) => setSymbol(e.target.value)} />
          </label>
          <button
            onClick={() => {
              console.log(symbol);
              handleCreate();
            }}
          >
            Save
          </button>
        </article>
      </>
    );
  }, [handleCreate, symbol]);

  const showStock = async (symbol: string): Promise<JSX.Element> => {
    let stock: Stock;

    try {
      stock = await getStock(symbol);
    } catch (ex) {
      console.log(ex);

      throw ex;
    }

    return StockView(stock);
  };

  return view ? view : emptyCreateForm();
}
