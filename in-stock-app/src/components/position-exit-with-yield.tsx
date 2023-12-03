import { ExitPositionWithYieldV1Model } from "@/services/in-stock-api-models";
import {
  formatNumber as fn,
  formatCurrency as fc,
  formatPercent as fp,
} from "@/services/string-formats";
import { getPositionExitWithYield } from "@/services/in-stock-api";
import { useState } from "react";
import Waiting from "./waiting";

interface IProps {
  symbol: string;
}

type FormData = {
  desiredYield: number;
};

const PositionExitWithYield: React.FC<IProps> = ({ symbol }) => {
  const [view, setView] = useState<JSX.Element>();
  const [data, setData] = useState<FormData>({
    desiredYield: 1.0,
  });

  const calculate = (): void => {
    const asDecimal = data.desiredYield / 100;

    getPositionExitWithYield(symbol, asDecimal).then((result) => {
      setView(renderCalculation(result));
    });

    setView(<Waiting />);
  };

  const renderCalculation = (
    result: ExitPositionWithYieldV1Model
  ): JSX.Element => {
    return (
      <article>
        <div className="grid">
          <div>
            <div className="grid">
              <label>Desired Yield</label>
              <label>{fp(result.desiredYield)}</label>
            </div>
            <div className="grid">
              <label>Theoretical Value</label>
              <label>{fc(result.theoreticalValue)}</label>
            </div>
            <div className="grid">
              <label>Theoretical Price</label>
              <label>{fc(result.theoreticalPrice)}</label>
            </div>
            <div className="grid">
              <label>Theoretical Gain</label>
              <label>{fc(result.theoreticalGain.gain)}</label>
            </div>
            <div className="grid">
              <label>Theoretical Gain</label>
              <label>{fp(result.theoreticalGain.gainRate)}</label>
            </div>
          </div>
          <div>
            <div className="grid">
              <label>Current Value</label>
              <label>{fc(result.currentValue)}</label>
            </div>
            <div className="grid">
              <label>Current Price</label>
              <label>{fc(result.currentPrice)}</label>
            </div>
            <div className="grid">
              <label>Current Gain</label>
              <label>{fc(result.currentGain.gain)}</label>
            </div>
            <div className="grid">
              <label>Current Gain</label>
              <label>{fp(result.currentGain.gainRate)}</label>
            </div>
          </div>
        </div>
      </article>
    );
  };

  return (
    <>
      <div className="grid">
        <label>Desired Yield</label>
        <input
          type="number"
          value={data.desiredYield}
          onChange={(e) =>
            setData({ ...data, desiredYield: e.target.valueAsNumber })
          }
        />
        %
      </div>
      <div className="grid">
        <button onClick={() => calculate()}>Calculate</button>
      </div>
      <hr />
      {view ?? "Fill out the form and click Calculate."}
    </>
  );
};

export default PositionExitWithYield;
