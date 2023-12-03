"use client";

import {
  createPosition,
  deletePosition,
  getCalculatedPositions,
  getPosition,
  getStockBySymbol,
  updatePosition,
} from "@/services/in-stock-api";
import { useSearchParams } from "next/navigation";
import { EmptyString, toFloat, getDateWithOffset } from "@/services/common";
import Waiting from "@/components/waiting";
import { useRef, useState, useEffect, useCallback } from "react";
import PositionsTable from "@/components/positions-table";
import Container from "@/components/container";
import {
  PositionV1CreateModel,
  PositionV1GetCalculatedModel,
  PositionV1PatchModel,
} from "@/services/in-stock-api-models";
import { formatIsoDate } from "@/services/string-formats";
import {
  Mode,
  PositionDeleteModalModel,
  PositionAddEditModalModel,
  PositionModel,
} from "../view-models/positions";
import DeleteModal, {
  IActions as IDeleteModalActions,
} from "@/components/delete-modal";
import AddEditModal, {
  IActions as IAddEditModalActions,
} from "@/components/add-edit-modal";
import PositionOverview from "@/components/position-overview";
import TabContainer from "@/components/tab-container";
import PositionCoverLosses from "@/components/position-cover-losses";

export default function Page() {
  const _symbol = useRef(EmptyString);
  const _stockId = useRef(0);
  const _qsp = useSearchParams();
  const _qspRef = useRef(EmptyString);
  const _positions = useRef<PositionV1GetCalculatedModel[]>([]);
  const _positionChanged = useRef(0);
  const [view, setView] = useState<JSX.Element>();
  const _modalAddEditActions = useRef<IAddEditModalActions>(null);
  const _modalAddEditActionData = useRef<PositionAddEditModalModel>({
    title: EmptyString,
    buttonText: EmptyString,
    action: () => {},
  });
  const _modalDeleteActions = useRef<IDeleteModalActions>(null);
  const _modalDeleteActionData = useRef<PositionDeleteModalModel>({
    positionId: 0,
    action: () => {},
  });
  const [positionState, setPositionState] = useState<PositionModel>({
    stockId: 0,
    dateOpenedUtc: new Date(),
    price: 0,
    quantity: 0,
    dateClosedUtc: undefined,
  });

  const renderPositions = useCallback(
    (models: PositionV1GetCalculatedModel[]): void => {
      const reloadPositions = (symbol: string): void => {
        _positionChanged.current++;

        getCalculatedPositions(symbol).then((models) => {
          _positions.current = models;

          renderPositions(models);
        });
      };

      const launchAddEditModal = (mode: Mode, positionId: number): void => {
        const props = _modalAddEditActionData.current;

        if (mode === Mode.Add) {
          props.title = "Add";
          props.buttonText = "Save";
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

          _modalAddEditActions.current?.show();
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
            props.buttonText = "Update";
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

            _modalAddEditActions.current?.show();
          });
        }
      };

      const launchModalDelete = (positionId: number): void => {
        _modalDeleteActionData.current.positionId = positionId;
        _modalDeleteActionData.current.action = (positionId: number) => {
          deletePosition(positionId).then(() => {
            reloadPositions(_symbol.current);
          });
        };

        _modalDeleteActions.current?.show();
      };

      const props = _modalAddEditActionData.current;

      setView(
        <>
          <AddEditModal
            ref={_modalAddEditActions}
            title={props.title}
            buttonText={props.buttonText}
            onClickAction={() => {
              var p = { ...positionState, stockId: _stockId.current };

              props.action(p);
              setPositionState(p);
              setView(undefined);
            }}
          >
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
          </AddEditModal>
          <DeleteModal
            ref={_modalDeleteActions}
            title="Delete positions"
            onClickAction={() => {
              _modalDeleteActionData.current.action(
                _modalDeleteActionData.current.positionId
              );
            }}
          >
            <p>Are you sure you want to delete this position?</p>
          </DeleteModal>
          <PositionOverview
            stockId={_stockId.current}
            change={_positionChanged.current}
          />
          <Container>
            <button onClick={() => launchAddEditModal(Mode.Add, 0)}>Add</button>
          </Container>
          <PositionsTable
            positions={models}
            editAction={(positionId: number) => {
              launchAddEditModal(Mode.Edit, positionId);
            }}
            deleteAction={(positionId: number) => {
              launchModalDelete(positionId);
            }}
          />
          <TabContainer
            tabs={[
              {
                title: "Cover Losses",
                selected: true,
                content: <PositionCoverLosses symbol={_symbol.current} />,
              },
              {
                title: "Exit with Yield",
                selected: false,
                content: <div>Exit with yield control goes here</div>,
              },
            ]}
          />
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
  }, [_qsp, renderPositions, _modalDeleteActions]);

  return view ? view : <Waiting />;
}
