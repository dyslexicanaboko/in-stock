"use client";

import StockView from "@/components/stock-view";
import Waiting from "@/components/waiting";
import {
  createStock,
  updateStock,
  getStockById,
} from "@/services/in-stock-api";
import { StockEdit } from "@/app/view-models/stock";
import { Stock, StockV1CreatedModel } from "@/services/in-stock-api-models";
import { ChangeEvent, useCallback, useState, useRef, useEffect } from "react";
import Container from "@/components/container";
import Link from "next/link";
import { useSearchParams } from 'next/navigation'

export default function Page() {
  const [symbol, setSymbol] = useState("");
  //const symbol = useRef("");
  const [notes, setNotes] = useState("");
  const [view, setView] = useState<JSX.Element>();
  const _model = useRef<StockEdit>();
  const _qsp = useSearchParams();
  const _qspRef = useRef("");

  const viewPleaseWait = (): void => setView(<Waiting />);

  const storeAsEditModel = useCallback(
    (model: Stock | StockV1CreatedModel): void => {
      const edit: StockEdit = {
        stockId: model.stockId,
        notes: model.notes,
      };

      _model.current = edit;

      console.log("State saved");
      console.log(edit);
    },
    []
  );

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

  const getPositionsLink = (symbol: string): JSX.Element => (
    <Link href={"/positions?symbol=" + symbol}>Edit position</Link>
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
            <Container>
              <article>
                {stock}
                <div className="grid">
                  <label>Notes</label>
                  <input
                    type="text"
                    defaultValue={model.notes}
                    onChange={onChangeNotes}
                  />
                </div>
                <button onClick={() => handleUpdate(_model.current!)}>
                  Save notes
                </button>
                {getPositionsLink(model.symbol)}
              </article>
            </Container>
          );
        });
      });

      viewPleaseWait();
    },
    [onChangeNotes, storeAsEditModel]
  );

  const handleCreate = useCallback((): void => {
    console.log(`symbol: ${symbol}`);

    createStock(symbol).then((model) => {
      storeAsEditModel(model);

      const stock = StockView(model, true);

      setView(
        <Container>
          <article>
            {stock}
            <div className="grid">
              <label>Notes</label>
              <input
                type="text"
                defaultValue={model.notes}
                onChange={onChangeNotes}
              />
            </div>
            <button onClick={() => handleUpdate(_model.current!)}>
              Save notes
            </button>
            {getPositionsLink(symbol)}
          </article>
        </Container>
      );
    });

    viewPleaseWait();
  }, [symbol, storeAsEditModel, onChangeNotes, handleUpdate]);

  const onKeyDown = useCallback(
    (event: React.KeyboardEvent<HTMLDivElement>): void => {
      // 'keypress' event misbehaves on mobile so we track 'Enter' key via 'keydown' event
      if (event.key === "Enter") {
        event.preventDefault();
        event.stopPropagation();
        handleCreate();
      }
    },
    [handleCreate]
  );

  const emptyCreateForm = useCallback((): JSX.Element => {
    return (
      <Container>
        <article>
          <label>
            Search for stock by symbol
            <input
              type="text"
              onChange={(e) => setSymbol(e.target.value)}
              onKeyDown={onKeyDown}
            />
          </label>
          <button onClick={() => handleCreate()}>Lookup</button>
        </article>
      </Container>
    );
  }, [handleCreate, onKeyDown]);

  useEffect(() => {  
    if(_qsp) {
      const qspSymbol = _qsp.get("symbol");
  
      if(qspSymbol && _qspRef.current !== qspSymbol) {
        _qspRef.current = qspSymbol;
        
        setSymbol(qspSymbol);
  
        handleCreate();
      }
    }
  }, [_qsp, handleCreate]);
  
  return view ? view : emptyCreateForm();
}
