"use client";

import StockView from "@/components/stock-view";
import Waiting from "@/components/waiting";
import {
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

  const viewPleaseWait = (): void =>
    setView(<Waiting />);

  setView((
    <>
      
    </>
  ));

  return view;
}


/*
 DateOpened
,DateClosed
,Price
,Quantity
,CreateOnUtc
,UpdatedOnUtc
*/