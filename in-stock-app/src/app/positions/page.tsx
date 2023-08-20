"use client";

import { getCalculatedPositions } from "@/services/in-stock-api";
import { useSearchParams } from "next/navigation";
import { EmptyString } from "@/services/common";
import Waiting from "@/components/waiting";
import { useRef, useState, useEffect, useCallback } from "react";
import PositionsView from "@/components/positions-view";
import Container from "@/components/container";
import { PositionV1GetCalculatedModel } from "@/services/in-stock-api-models";

export default function Page() {
  const _symbol = useRef(EmptyString);
  const _qsp = useSearchParams();
  const _qspRef = useRef(EmptyString);
  const [view, setView] = useState<JSX.Element>();
  const _positions = useRef<PositionV1GetCalculatedModel[]>([]);
  const _modal = useRef<HTMLDialogElement>(null);
  const [modalVisibility, setModalVisibility] = useState<boolean>(false);

  const openPositionModal = useCallback(() : void => {
    const m = _modal.current;

    if(!m) return;

    if (m.open && !modalVisibility) {
      m.close()

      setModalVisibility(false);

      console.log("Close modal");
    } else if (!m.open && modalVisibility) {
      m.showModal()
     
      console.log("Open modal");
    }
  }, [modalVisibility]);

  const renderPositions = useCallback((models: PositionV1GetCalculatedModel[]) : void => {
    const positions = PositionsView(models);

    setView(<>
      <dialog id="positionModal" ref={_modal}>
        <article>
          <header>
            <a href="#" aria-label="Close" className="close" onClick={() => setModalVisibility(false)}></a>
            New position
          </header>
          <div className="grid">
            <label>Shares</label>
            <input type="number" />
          </div>
          <div className="grid">
            <label>Price</label>
            <input type="number" />
          </div>
          <div className="grid">
            <label>Opened</label>
            <input type="date" />
          </div>
          <div className="grid">
            <label>Closed</label>
            <input type="date" />
          </div>
          <footer>
            <button>Save</button>  
          </footer>
        </article>
      </dialog>
      <Container><button onClick={() => setModalVisibility(true)}>Add</button></Container>
      {positions}</>);
  }, []);

  useEffect(() => {
    if (!_qsp) return;
    
    const qspSymbol = _qsp.get("symbol");

    if (!qspSymbol) {
      _qspRef.current = EmptyString;

      setView(undefined);

      return;
    }

    openPositionModal();

    _symbol.current = qspSymbol;

    getCalculatedPositions(qspSymbol)
      .then((models) => {
        _positions.current = models;
        
        renderPositions(models);    
      });
  }, [_qsp, openPositionModal, renderPositions]);

  return view ? view : <Waiting />;
}
