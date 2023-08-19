import { DaysInOneYear } from "@/services/common";
import { PositionV1GetCalculatedModel, PositionV1GetModel } from "@/services/in-stock-api-models";
import {
  formatDate as fd,
  formatNumber as fn,
  formatCurrency as fc,
  formatPercent as fp,
} from "@/services/string-formats";

export default function PositionsView(positions: PositionV1GetCalculatedModel[]) {
  positions.sort((a, b) => parseInt(a.dateOpened.toString()) - parseInt(b.dateOpened.toString()));
  
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
            <th scope="col">Rank</th>
          </tr>
        </thead>
        <tbody>
          {positions.map((position, key) => {
            let dateClosed: string;
            
            if(position.dateClosed) {
              dateClosed = fd(position.dateClosed);
            } else {
              dateClosed = "--";
            }

            return (
              <tr key={key}>
                <td>{position.positionId}</td>
                <td>{fn(position.shares, 2)}</td>
                <td>{fc(position.price)}</td>
                <td>{fc(position.costBasis)}</td>
                <td>{fd(position.dateOpened)}</td>
                <td>{dateClosed}</td>
                <td>{fn(position.daysHeld, 2)}</td>
                <td>{fn(position.daysHeld/DaysInOneYear, 2)}</td>
                <td>{fc(position.currentPrice)}</td>
                <td>{fc(position.currentValue)}</td>
                <td>{fc(position.totalGain)}</td>
                <td>{fp(position.totalGainPercentage)}</td>
                <td>{fc(position.gainRate)}</td>
                <td>{position.isLongPosition ? "long" : "short"}</td>
                <td>{position.rank}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </>
  );
}
