"use client";

import {
  createPosition,
  getCalculatedPositions,
  getPosition,
  getStockBySymbol,
  updatePosition,
} from "@/services/in-stock-api";
import { useSearchParams } from "next/navigation";
import { EmptyString, toFloat, getDateWithOffset } from "@/services/common";
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
  const _positions = useRef<PositionV1GetCalculatedModel[]>([]);
  const [view, setView] = useState<JSX.Element>();
  const _modal = useRef<HTMLDialogElement>(null);
  const _modalProps = useRef<PositionModal>({
    title: EmptyString,
    action: () => {},
  });
  const [modalVisibility, setModalVisibility] = useState<boolean>(false);
  const [positionState, setPositionState] = useState<PositionModel>({
    stockId: 0,
    dateOpenedUtc: new Date(),
    price: 0,
    quantity: 0,
    dateClosedUtc: undefined,
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
      const reloadPositions = (symbol: string): void => {
        getCalculatedPositions(symbol).then((models) => {
          _positions.current = models;

          renderPositions(models);
        });
      };

      const launchModal = (mode: Mode, positionId: number): void => {
        const props = _modalProps.current;

        if (mode === Mode.Add) {
          props.title = "Add";
          props.action = (position: PositionModel) => {
            const model: PositionV1CreateModel = {
              dateOpenedUtc: position.dateOpenedUtc,
              dateClosedUtc: position.dateClosedUtc,
              price: position.price,
              quantity: position.quantity,
              stockId: position.stockId,
            };

            createPosition(model).then(() => {
              reloadPositions(_symbol.current);
            });
          };

          setModalVisibility(true);
        } else {
          getPosition(positionId).then((model) => {
            console.log("Position model");
            console.log(model);

            setPositionState({
              stockId: _stockId.current,
              dateOpenedUtc: model.dateOpenedUtc,
              dateClosedUtc: model.dateClosedUtc,
              price: model.price,
              quantity: model.quantity,
            });

            props.title = "Edit";
            props.action = (position: PositionModel) => {
              const model: PositionV1PatchModel = {
                dateOpenedUtc: position.dateOpenedUtc,
                dateClosedUtc: position.dateClosedUtc,
                price: position.price,
                quantity: position.quantity,
              };

              updatePosition(positionId, model).then(() => {
                reloadPositions(_symbol.current);
              });
            };

            setModalVisibility(true);
          });
        }
      };

      const positions = PositionsView(
        models,
        (positionId: number) => {
          launchModal(Mode.Edit, positionId);
        },
        (positionId: number) => {
          console.log(`Delete ${positionId}`);
        }
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
                    setPositionState({
                      ...positionState,
                      price: toFloat(e.target.value),
                    })
                  }
                />
              </div>
              <div>
                <label>Opened</label>
                <input
                  type="date"
                  value={formatIsoDate(positionState.dateOpenedUtc)}
                  onChange={(e) => {
                    if (e.target.valueAsDate === null) return;

                    console.log("onChange set");
                    console.log(e.target.valueAsNumber);
                    console.log(new Date(e.target.valueAsNumber));

                    setPositionState({
                      ...positionState,
                      dateOpenedUtc: getDateWithOffset(e.target.valueAsDate),
                    });
                  }}
                />
              </div>
              <div>
                <label>Closed</label>
                <input
                  type="date"
                  value={
                    positionState.dateClosedUtc
                      ? formatIsoDate(positionState.dateClosedUtc)
                      : EmptyString
                  }
                  onChange={(e) =>
                    setPositionState({
                      ...positionState,
                      dateClosedUtc:
                        e.target.valueAsDate === null
                          ? undefined
                          : getDateWithOffset(e.target.valueAsDate),
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

    if (_symbol.current === qspSymbol) {
      renderPositions(_positions.current);

      return;
    }

    _symbol.current = qspSymbol;

    getStockBySymbol(qspSymbol).then((model) => {
      _stockId.current = model.stockId;
    });

    getCalculatedPositions(qspSymbol).then((models) => {
      _positions.current = models;

      renderPositions(models);
    });
  }, [_qsp, handleModalVisibility, renderPositions]);

  return view ? view : <Waiting />;
}
