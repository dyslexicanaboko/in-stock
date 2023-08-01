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
import { ChangeEvent, useCallback, useState, useRef } from "react";

export default function Page() {
  const [symbol, setSymbol] = useState("");
  const [notes, setNotes] = useState("");
  const [view, setView] = useState<JSX.Element>();
  const _model = useRef<StockEdit>();

  const viewPleaseWait = () : void => setView(<button aria-busy="true">Please wait…</button>);

  const storeAsEditModel = useCallback((model: Stock | StockV1CreatedModel): void => {
    const edit: StockEdit = {
      stockId: model.stockId,
      notes: model.notes,
    };

    _model.current = edit;

    console.log("State saved");
    console.log(edit);
  }, []);

  const onChangeNotes = useCallback(
    (e: ChangeEvent<HTMLInputElement>): void => {
      setNotes(e.target.value);
      
      console.log(e.target.value);

      const edit: StockEdit = {
        stockId: _model.current!.stockId,
        notes: e.target.value,
      };

      _model.current = edit;
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
              <div className="grid">
                <label>Notes</label>
                <input
                  type="text"
                  defaultValue={model.notes}
                  onChange={onChangeNotes}
                />
              </div>
              <button onClick={() => handleUpdate(_model.current!)}>Save notes</button>
            </>
          );
        });
      });

      viewPleaseWait();
    },
    [onChangeNotes, storeAsEditModel]
  );

  const handleCreate = useCallback((): void => {
    createStock(symbol).then((model) => {
      storeAsEditModel(model);

      const stock = StockView(model, true);

      setView(
        <>
          {stock}
          <div className="grid">
            <label>Notes</label>
            <input
              type="text"
              defaultValue={model.notes}
              onChange={onChangeNotes}
            />
          </div>
          <button onClick={() => handleUpdate(_model.current!)}>Save notes</button>
        </>
      );
    });

    viewPleaseWait();
  }, [symbol, storeAsEditModel, onChangeNotes, handleUpdate]);

  const onKeyDown = useCallback((event: React.KeyboardEvent<HTMLDivElement>): void => {
    // 'keypress' event misbehaves on mobile so we track 'Enter' key via 'keydown' event
    if (event.key === 'Enter') {
      event.preventDefault();
      event.stopPropagation();
      handleCreate();
    }
  }, [handleCreate]);

  const emptyCreateForm = useCallback((): JSX.Element => {
    return (
      <>
        <article>
          <label>
            Search for stock by symbol
            <input 
              type="text" 
              onChange={(e) => setSymbol(e.target.value)}
              onKeyDown={onKeyDown} />
          </label>
          <button onClick={() => handleCreate()}>Lookup</button>
        </article>
      </>
    );
  }, [handleCreate, onKeyDown]);

  return view ? view : emptyCreateForm();
}
