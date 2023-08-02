"use client";

import Waiting from "@/components/waiting";
import {
  getPortfolio,
} from "@/services/in-stock-api";
//import { Stock, StockEdit } from "@/app/view-models/portfolio";
import { PortfolioV1GetModel } from "@/services/in-stock-api-models";
import { ChangeEvent, useCallback, useState, useRef, useEffect } from "react";
import PortfolioView from "@/components/position-view";

export default function Page() {
  const [view, setView] = useState<JSX.Element>();

  const viewPleaseWait = (): void => setView(<Waiting />);

  useEffect(() => {
    getPortfolio(1).then((models) => {
      console.log(models);
          
      setView(PortfolioView(models));
    });
  }, []);

  console.log("render?");

  return view ? view : <Waiting />;
}
