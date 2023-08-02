import { PortfolioV1GetModel } from "@/services/in-stock-api-models";
import { formatDate } from "@/services/utils";

//TODO: All of the data fields need to be formatted nicely
export default function PortfolioView(portfolio: PortfolioV1GetModel[]) {
  return (
    <>
      <table role="grid">
        <thead>
          <tr>
            <th scope="col">Id</th>
            <th scope="col">Symbol</th>
            <th scope="col">ownedAsOf</th>
            <th scope="col">Shares</th>
            <th scope="col">Cost Basis</th>
            <th scope="col">Low</th>
            <th scope="col">High</th>
            <th scope="col">Short</th>
            <th scope="col">Long</th>
            <th scope="col">Days Held</th>
            <th scope="col">Current Price</th>
            <th scope="col">Current Value</th>
            <th scope="col">Total Gain</th>
            <th scope="col">Total Gain %</th>
            <th scope="col">Gain Rate</th>
          </tr>
        </thead>
        <tbody>
          {portfolio.map((position, key) => {
            return (
              <tr key={key}>
                <td>{position.stockId}</td>
                <td>{position.symbol}</td>
                <td>{formatDate(position.ownedAsOf)}</td>
                <td>{position.shares}</td>
                <td>{position.costBasis}</td>
                <td>{position.lowestHeld}</td>
                <td>{position.highestHeld}</td>
                <td>{position.short}</td>
                <td>{position.long}</td>
                <td>{position.daysHeld}</td>
                <td>{position.currentPrice}</td>
                <td>{position.currentValue}</td>
                <td>{position.totalGain}</td>
                <td>{position.totalGainPercentage}</td>
                <td>{position.gainRate}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </>
  );
}
