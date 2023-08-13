"use client";

import Waiting from "@/components/waiting";
import { getPortfolio } from "@/services/in-stock-api";
import { useState, useEffect } from "react";
import PortfolioView from "@/components/portfolio-view";
import { getUserId } from "@/services/user-info";

export default function Page() {
  const [view, setView] = useState<JSX.Element>();

  useEffect(() => {
    getPortfolio(getUserId()).then((models) => {
      setView(PortfolioView(models));
    });
  }, []);

  return view ? view : <Waiting />;
}
