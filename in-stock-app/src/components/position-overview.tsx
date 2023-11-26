import { DaysInOneYear } from "@/services/common";
import { PortfolioV1GetModel } from "@/services/in-stock-api-models";
import {
  formatDate as fd,
  formatNumber as fn,
  formatCurrency as fc,
  formatPercent as fp,
} from "@/services/string-formats";
import { getStockPosition } from "@/services/in-stock-api";
import { getUserId } from "@/services/user-info";
import { useEffect, useState } from "react";
import Waiting from "./waiting";

interface IProps {
  stockId: number;
  change: number;
}

const PositionOverview: React.FC<IProps> = ({ stockId, change }) => {
  const [view, setView] = useState<JSX.Element>();
  //const [update, setUpdate] = useState<number>();

  useEffect(() => {
    getStockPosition(getUserId(), stockId).then((position) => {
      setView(renderPosition(position));
    });
  }, [stockId, change]);

  return view ?? <Waiting />;
};

const renderPosition = (position: PortfolioV1GetModel): JSX.Element => {
  const symbol = position.symbol.toUpperCase();

  const positiveGain = position.totalGain < 0;

  return (
    <>
      <div className="grid">
        <div></div>
        <article>
          <div className="grid">
            <label>Symbol</label>
            <input type="text" value={symbol} readOnly />
          </div>
          <div className="grid">
            <label>Shares</label>
            <label>{fn(position.shares, 2)}</label>
          </div>
          <div className="grid">
            <label>Acquired On</label>
            <label>{fd(position.acquiredOnUtc)}</label>
          </div>
          <div className="grid">
            <label>Short</label>
            <label>{fn(position.short)}</label>
          </div>
          <div className="grid">
            <label>Long</label>
            <label>{fn(position.long)}</label>
          </div>
          <div className="grid">
            <label>Days held</label>
            <label>{fn(position.daysHeld, 2)}</label>
          </div>
          <div className="grid">
            <label>Years held</label>
            <label>{fn(position.daysHeld / DaysInOneYear, 2)}</label>
          </div>
          <div className="grid">
            <label>Low</label>
            <label>{fc(position.lowestHeld)}</label>
          </div>
          <div className="grid">
            <label>High</label>
            <label>{fc(position.highestHeld)}</label>
          </div>
        </article>
        <article>
          <div className="grid">
            <label>Cost Basis</label>
            <label>{fc(position.costBasis)}</label>
          </div>
          <div className="grid">
            <label>Current Value</label>
            <label>{fc(position.currentValue)}</label>
          </div>
          <div className="grid">
            <label>Current Price</label>
            <label>{fc(position.currentPrice)}</label>
          </div>
          <div className="grid">
            <label>Average Price</label>
            <label>{fc(position.averagePrice)}</label>
          </div>
          <div className="grid">
            <label>Gain</label>
            <input
              type="text"
              value={fc(position.totalGain)}
              aria-invalid={positiveGain}
            />
          </div>
          <div className="grid">
            <label>Gain %</label>
            <input
              type="text"
              value={fp(position.totalGainRate)}
              aria-invalid={positiveGain}
            />
          </div>
          <div className="grid">
            <label>$/day</label>
            <input
              type="text"
              value={fc(position.gainPerDay)}
              aria-invalid={positiveGain}
            />
          </div>
        </article>
        <div></div>
      </div>
    </>
  );
};

export default PositionOverview;
