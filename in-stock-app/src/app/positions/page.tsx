"use client";

import { getPositions } from "@/services/in-stock-api";
import { useSearchParams } from "next/navigation";
import { EmptyString } from "@/services/common";
import Waiting from "@/components/waiting";
import { useRef, useState, useEffect } from "react";
import PositionsView from "@/components/positions-view";

export default function Page() {
  //const symbol = useRef(EmptyString);
  const _qsp = useSearchParams();
  const _qspRef = useRef(EmptyString);
  const [view, setView] = useState<JSX.Element>();

  useEffect(() => {
    if (!_qsp) return;
    
    const qspSymbol = _qsp.get("symbol");

    if (!qspSymbol) {
      _qspRef.current = EmptyString;

      setView(undefined);

      return;
    }

    getPositions(qspSymbol).then((models) => {
      setView(PositionsView(models));
    });
  }, [_qsp]);

  return view ? view : <Waiting />;
}
