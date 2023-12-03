import { CoverPositionLossV1Model } from "@/services/in-stock-api-models";
import {
  formatNumber as fn,
  formatCurrency as fc,
  formatPercent as fp,
} from "@/services/string-formats";
import { getPositionCoverLosses } from "@/services/in-stock-api";
import { useState } from "react";
import Waiting from "./waiting";

interface IProps {
  symbol: string;
}

type FormData = {
  desiredPrice: number;
  proposals: number;
};

const PositionCoverLosses: React.FC<IProps> = ({ symbol }) => {
  const [view, setView] = useState<JSX.Element>();
  const [data, setData] = useState<FormData>({
    desiredPrice: 1.0,
    proposals: 1,
  });

  const calculate = (): void => {
    getPositionCoverLosses(symbol, data.desiredPrice, data.proposals).then(
      (result) => {
        setView(renderCalculation(result));
      }
    );

    setView(<Waiting />);
  };

  const renderCalculation = (result: CoverPositionLossV1Model): JSX.Element => {
    return (
      <>
        <article>
          <div className="grid">
            <label>Desired Sales Price</label>
            <label>{fc(result.desiredSalesPrice)}</label>
          </div>
          <div className="grid">
            <label>Total Shares</label>
            <label>{fn(result.totalShares, 2)}</label>
          </div>
          <div className="grid">
            <label>Bad Shares</label>
            <label>{fn(result.badShares, 2)}</label>
          </div>
          <div className="grid">
            <label>Current Price</label>
            <label>{fc(result.currentPrice)}</label>
          </div>
          <div className="grid">
            <label>Current Loss</label>
            <label>{fc(result.currentLoss)}</label>
          </div>
        </article>
        <hr />
        <article>
          <div className="grid">
            <div className="grid">
              <div>
                <label>Proposal</label>
                <label>Shares to Buy</label>
                <label>Cost</label>
                <label>sale</label>
                <label>Gain</label>
                <label>Gain %</label>
              </div>
            </div>
            {result.proposals.map((proposal, key) => {
              //const positiveGain = result.totalGain < 0;

              return (
                <div className="grid" key={key}>
                  <div>
                    <label>{proposal.proposal}</label>
                    <label>{fn(proposal.sharesToBuy, 2)}</label>
                    <label>{fc(proposal.cost)}</label>
                    <label>{fc(proposal.sale)}</label>
                    <label>{fc(proposal.gain.gain)}</label>
                    <label>{fp(proposal.gain.gainRate)}</label>
                  </div>
                </div>
              );
            })}
          </div>
        </article>
      </>
    );
  };

  return (
    <>
      <div className="grid">
        <label>Desired Sales Price</label>
        <input
          type="number"
          value={data.desiredPrice}
          onChange={(e) =>
            setData({ ...data, desiredPrice: e.target.valueAsNumber })
          }
        />
      </div>
      <div className="grid">
        <label>Proposals</label>
        <input
          type="number"
          value={data.proposals}
          onChange={(e) =>
            setData({ ...data, proposals: e.target.valueAsNumber })
          }
        />
      </div>
      <div className="grid">
        <button onClick={() => calculate()}>Calculate</button>
      </div>
      <hr />
      {view ?? "Fill out the form and click Calculate."}
    </>
  );
};

export default PositionCoverLosses;
