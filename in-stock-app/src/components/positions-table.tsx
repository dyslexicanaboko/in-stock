import { DaysInOneYear } from "@/services/common";
import {
  PositionV1GetCalculatedModel,
  PositionV1GetModel,
} from "@/services/in-stock-api-models";
import {
  formatDate as fd,
  formatNumber as fn,
  formatCurrency as fc,
  formatPercent as fp,
} from "@/services/string-formats";

interface IProps {
  positions: PositionV1GetCalculatedModel[];
  editAction: (positionId: number) => void;
  deleteAction: (positionId: number) => void;
}

const PositionsTable: React.FC<IProps> = ({
  positions,
  editAction,
  deleteAction,
}) => {
  positions.sort(
    (a, b) =>
      parseInt(a.dateOpenedUtc.toString()) -
      parseInt(b.dateOpenedUtc.toString())
  );
  const ranks = Array.from(positions, (x) => x.rank);
  const min = Math.min(...ranks);
  const max = Math.max(...ranks);

  return (
    <>
      <table role="grid">
        <thead>
          <tr>
            <th scope="col">Id</th>
            <th scope="col">Shares</th>
            <th scope="col">Price</th>
            <th scope="col">Cost Basis</th>
            <th scope="col">Opened</th>
            <th scope="col">Closed</th>
            <th scope="col">Days</th>
            <th scope="col">Years</th>
            <th scope="col">Current Price</th>
            <th scope="col">Current Value</th>
            <th scope="col">Gain</th>
            <th scope="col">Gain %</th>
            <th scope="col">$/day</th>
            <th scope="col">Capital Gains</th>
            <th scope="col">&nbsp;</th>
            <th scope="col">&nbsp;</th>
          </tr>
        </thead>
        <tbody>
          {positions.map((position, key) => {
            let dateClosed: string;

            if (position.dateClosedUtc) {
              dateClosed = fd(position.dateClosedUtc);
            } else {
              dateClosed = "--";
            }

            let s;

            //Make CSS class for this style
            if (position.rank === min) {
              s = { color: "green" };
            } else if (position.rank === max) {
              s = { color: "red" };
            }

            return (
              <tr key={key}>
                <td>{position.positionId}</td>
                <td>{fn(position.shares, 2)}</td>
                <td style={s}>{fc(position.price)}</td>
                <td>{fc(position.costBasis)}</td>
                <td>{fd(position.dateOpenedUtc)}</td>
                <td>{dateClosed}</td>
                <td>{fn(position.daysHeld, 2)}</td>
                <td>{fn(position.daysHeld / DaysInOneYear, 2)}</td>
                <td>{fc(position.currentPrice)}</td>
                <td>{fc(position.currentValue)}</td>
                <td>{fc(position.totalGain)}</td>
                <td>{fp(position.totalGainPercentage)}</td>
                <td>{fc(position.gainRate)}</td>
                <td>{position.isLongPosition ? "long" : "short"}</td>
                <td>
                  <button onClick={() => editAction(position.positionId)}>
                    📝
                  </button>
                </td>
                <td>
                  <button onClick={() => deleteAction(position.positionId)}>
                    ❌
                  </button>
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </>
  );
};

export default PositionsTable;
