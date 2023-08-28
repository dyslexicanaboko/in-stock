"use client";

import {
  createPosition,
  getCalculatedPositions,
  getPosition,
  getStockBySymbol,
  updatePosition,
} from "@/services/in-stock-api";
import { useSearchParams } from "next/navigation";
import { EmptyString, toFloat } from "@/services/common";
import Waiting from "@/components/waiting";
import { useRef, useState, useEffect, useCallback } from "react";
import PositionsView from "@/components/positions-view";
import Container from "@/components/container";
import {
  PositionV1CreateModel,
  PositionV1GetCalculatedModel,
  PositionV1PatchModel,
} from "@/services/in-stock-api-models";
import { formatIsoDate } from "@/services/string-formats";
import { Mode, PositionModal, PositionModel } from "../view-models/positions";

export default function Page() {
  const _symbol = useRef(EmptyString);
  const _stockId = useRef(0);
  const _qsp = useSearchParams();
  const _qspRef = useRef(EmptyString);
  const [view, setView] = useState<JSX.Element>();
  const _modal = useRef<HTMLDialogElement>(null);
  const _modalProps = useRef<PositionModal>({
    title: EmptyString,
    action: () => {},
  });
  const [modalVisibility, setModalVisibility] = useState<boolean>(false);
  const [positionState, setPositionState] = useState<PositionModel>({
    stockId: 0,
    dateOpened: new Date(),
    price: 0,
    quantity: 0,
    dateClosed: undefined,
  });

  const handleModalVisibility = useCallback((): void => {
    const m = _modal.current;

    if (!m) return;

    if (m.open && !modalVisibility) {
      m.close();

      setModalVisibility(false);

      console.log("Close modal");
    } else if (!m.open && modalVisibility) {
      m.showModal();

      console.log("Open modal");
    }
  }, [modalVisibility]);

  const renderPositions = useCallback(
    (models: PositionV1GetCalculatedModel[]): void => {
      const launchModal = (mode: Mode, positionId: number): void => {    
        const props = _modalProps.current;
    
        if (mode === Mode.Add) {
          props.title = "Add";
          props.action = (position: PositionModel) => {
            const model: PositionV1CreateModel = {
              dateOpened: position.dateOpened,
              dateClosed: position.dateClosed,
              price: position.price,
              quantity: position.quantity,
              stockId: position.stockId,
            };
    
            createPosition(model).then(() => {
              getCalculatedPositions(_symbol.current).then((models) => {
                renderPositions(models);
              });
            });
          };

          setModalVisibility(true);
        } else {
          getPosition(positionId).then((model) => {
            console.log("Position model");
            console.log(model);
            
            setPositionState({
              stockId: _stockId.current,
              dateOpened: new Date(model.dateOpened),
              dateClosed: model.dateClosed,
              price: model.price,
              quantity: model.quantity,
            });

            props.title = "Edit";
            props.action = (position: PositionModel) => {
              const model: PositionV1PatchModel = {
                dateOpened: position.dateOpened,
                dateClosed: position.dateClosed,
                price: position.price,
                quantity: position.quantity,
              };
      
              updatePosition(positionId, model).then(() => {
                getCalculatedPositions(_symbol.current).then((models) => {
                  renderPositions(models);
                });
              });
            };

            setModalVisibility(true);
          });
        }
      };

      const positions = PositionsView(
        models, 
        (positionId: number) => { launchModal(Mode.Edit, positionId);},
        (positionId: number) => { console.log(`Delete ${positionId}`);},
      );

      const props = _modalProps.current;

      setView(
        <>
          <dialog id="positionModal" ref={_modal}>
            <article>
              <header>
                <a
                  href="#"
                  aria-label="Close"
                  className="close"
                  onClick={() => setModalVisibility(false)}
                ></a>
                {props.title} position
              </header>
              <div>
                <label>Shares</label>
                <input
                  type="number"
                  className="number"
                  value={positionState.quantity}
                  onChange={(e) =>
                    setPositionState({
                      ...positionState,
                      quantity: toFloat(e.target.value),
                    })
                  }
                />
              </div>
              <div>
                <label>Price</label>
                <input
                  type="number"
                  className="number"
                  value={positionState.price}
                  onChange={(e) =>
                    setPositionState({ ...positionState, price: toFloat(e.target.value) })
                  }
                />
              </div>
              <div>
                <label>Opened</label>
                <input
                  type="date"
                  value={formatIsoDate(positionState.dateOpened)}
                  onChange={(e) => {
                    if (e.target.valueAsDate === null) return;
                    setPositionState({
                      ...positionState,
                      dateOpened: e.target.valueAsDate,
                    });
                  }}
                />
              </div>
              <div>
                <label>Closed</label>
                <input
                  type="date"
                  value={
                    positionState.dateClosed
                      ? formatIsoDate(positionState.dateClosed)
                      : EmptyString
                  }
                  onChange={(e) =>
                    setPositionState({
                      ...positionState,
                      dateClosed:
                        e.target.valueAsDate === null
                          ? undefined
                          : e.target.valueAsDate,
                    })
                  }
                />
              </div>
              <footer>
                <button
                  onClick={() => {
                    var p = { ...positionState, stockId: _stockId.current };

                    props.action(p);
                    setPositionState(p);
                    setModalVisibility(false);
                    setView(undefined);
                  }}
                >
                  Save
                </button>
              </footer>
            </article>
          </dialog>
          <Container>
            <button onClick={() => launchModal(Mode.Add, 0)}>Add</button>
          </Container>
          {positions}
        </>
      );
    },
    [positionState]
  );

  useEffect(() => {
    if (!_qsp) return;

    const qspSymbol = _qsp.get("symbol");

    if (!qspSymbol) {
      _qspRef.current = EmptyString;

      setView(undefined);

      return;
    }

    handleModalVisibility();

    _symbol.current = qspSymbol;

    getStockBySymbol(qspSymbol).then((model) => {
      _stockId.current = model.stockId;
    });

    getCalculatedPositions(qspSymbol).then((models) => {
      renderPositions(models);
    });
  }, [_qsp, handleModalVisibility, renderPositions]);

  return view ? view : <Waiting />;
}
