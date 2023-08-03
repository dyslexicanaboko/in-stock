"use client";

import Waiting from "@/components/waiting";
import { getPortfolio } from "@/services/in-stock-api";
//import { Stock, StockEdit } from "@/app/view-models/portfolio";
//import { PortfolioV1GetModel } from "@/services/in-stock-api-models";
import { useState, useEffect } from "react";
import PortfolioView from "@/components/position-view";
import { getUserId } from "@/services/user-info";

export default function Page() {
  const [view, setView] = useState<JSX.Element>();

  //const viewPleaseWait = (): void => setView(<Waiting />);

  useEffect(() => {
    getPortfolio(getUserId()).then((models) => {
      console.log(models);

      setView(PortfolioView(models));
    });
  }, []);

  console.log("render?");

  return view ? view : <Waiting />;
}
