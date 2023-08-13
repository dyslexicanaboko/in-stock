import { PortfolioV1GetModel } from "@/services/in-stock-api-models";
import {
  formatDate as fd,
  formatNumber as fn,
  formatCurrency as fc,
  formatPercent as fp
} from "@/services/string-formats";
import Link from "next/link";

//TODO: All of the data fields need to be formatted nicely
export default function PortfolioView(portfolio: PortfolioV1GetModel[]) {
  return (
    <>
      <table role="grid">
        <thead>
          <tr>
            <th scope="col">Id</th>
            <th scope="col">Symbol</th>
            <th scope="col">Acquired On</th>
            <th scope="col">Shares</th>
            <th scope="col">Cost Basis</th>
            <th scope="col">Low</th>
            <th scope="col">High</th>
            <th scope="col">Short</th>
            <th scope="col">Long</th>
            <th scope="col">Days</th>
            <th scope="col">Years</th>
            <th scope="col">Current Price</th>
            <th scope="col">Current Value</th>
            <th scope="col">Total Gain</th>
            <th scope="col">Total Gain %</th>
            <th scope="col">Gain Rate ($/day)</th>
          </tr>
        </thead>
        <tbody>
          {portfolio.map((position, key) => {
            const symbol = position.symbol.toUpperCase();
            
            return (
              <tr key={key}>
                <td>{position.stockId}</td>
                <td><Link href={"/stock?symbol=" + symbol}>{symbol}</Link></td>
                <td>{fd(position.acquiredOn)}</td>
                <td>{fn(position.shares, 2)}</td>
                <td>{fc(position.costBasis)}</td>
                <td>{fc(position.lowestHeld)}</td>
                <td>{fc(position.highestHeld)}</td>
                <td>{fn(position.short)}</td>
                <td>{fn(position.long)}</td>
                <td>{fn(position.daysHeld, 2)}</td>
                <td>{fn(position.daysHeld/365.0, 2)}</td>
                <td>{fc(position.currentPrice)}</td>
                <td>{fc(position.currentValue)}</td>
                <td>{fc(position.totalGain)}</td>
                <td>{fp(position.totalGainPercentage)}</td>
                <td>{fc(position.gainRate)}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </>
  );
}
