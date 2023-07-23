"use client";

import StockView from "@/components/stock-view";
import {
  getStockBySymbol,
  createStock,
  updateStock,
  getStockById,
} from "@/services/in-stock-api";
import {
  Stock,
  StockEdit,
  StockV1CreatedModel,
} from "@/services/in-stock-types";
import { ChangeEvent, useCallback, useState } from "react";

export default function Page() {
  const [symbol, setSymbol] = useState("");
  const [view, setView] = useState<JSX.Element>();
  const [_model, setModel] = useState<StockEdit>();

  const storeAsEditModel = useCallback((model: Stock | StockV1CreatedModel): void => {
    const edit: StockEdit = {
      stockId: model.stockId,
      notes: model.notes,
    };

    setModel(edit);

    console.log("State saved");
    console.log(edit);
  }, [setModel]);

  //TODO: I have no idea why, but the _model is undefined inside of this change event
  const onChangeNotes = useCallback(
    (e: ChangeEvent<HTMLInputElement>): void => {
      console.log(e.target.value);

      const edit: StockEdit = {
        stockId: _model.stockId,
        notes: e.target.value,
      };

      setModel(edit);
    },
    [_model]
  );

  const handleUpdate = useCallback(
    (edit: StockEdit): void => {
      console.log("handleUpdate");
      console.log(edit);

      updateStock(edit.stockId, edit.notes).then(() => {
        getStockById(edit.stockId).then((model) => {
          storeAsEditModel(model);

          const stock = StockView(model, true);

          setView(
            <>
              {stock}
              <input
                type="text"
                value={model.notes ?? ""}
                onChange={onChangeNotes}
              />
              <button onClick={() => handleUpdate(_model!)}>Save notes</button>
            </>
          );
        });
      });

      setView(<button aria-busy="true">Please wait…</button>);
    },
    [_model, onChangeNotes, storeAsEditModel]
  );

  const handleCreate = useCallback((): void => {
    createStock(symbol).then((model) => {
      storeAsEditModel(model);

      const stock = StockView(model, true);

      setView(
        <>
          {stock}
          <input
            type="text"
            value={model.notes ?? ""}
            onChange={onChangeNotes}
          />
          <button onClick={() => handleUpdate(_model!)}>Save notes</button>
        </>
      );
    });

    setView(<button aria-busy="true">Please wait…</button>);
  }, [_model, handleUpdate, onChangeNotes, symbol, storeAsEditModel]);

  const emptyCreateForm = useCallback((): JSX.Element => {
    return (
      <>
        <article>
          <label>
            Search for stock by symbol
            <input type="text" onChange={(e) => setSymbol(e.target.value)} />
          </label>
          <button onClick={() => handleCreate()}>Lookup</button>
        </article>
      </>
    );
  }, [handleCreate]);

  const showStock = async (symbol: string): Promise<JSX.Element> => {
    let stock: Stock;

    try {
      stock = await getStockBySymbol(symbol);
    } catch (ex) {
      console.log(ex);

      throw ex;
    }

    return StockView(stock);
  };

  return view ? view : emptyCreateForm();
}
