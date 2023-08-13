import { PositionV1GetModel } from "@/services/in-stock-api-models";
import {
  formatDate as fd,
  formatNumber as fn,
  formatCurrency as fc,
  //formatPercent as fp
} from "@/services/string-formats";

//TODO: All of the data fields need to be formatted nicely
export default function PositionsView(positions: PositionV1GetModel[]) {
  positions.sort((a, b) => parseInt(a.dateOpened.toString()) - parseInt(b.dateOpened.toString()));
  
  return (
    <>
      <table role="grid">
        <thead>
          <tr>
            <th scope="col">Id</th>
            <th scope="col">Opened</th>
            <th scope="col">Closed</th>
            <th scope="col">Price</th>
            <th scope="col">Quantity</th>
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
                <td>{fd(position.dateOpened)}</td>
                <td>{dateClosed}</td>
                <td>{fc(position.price)}</td>
                <td>{fn(position.quantity, 2)}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </>
  );
}
