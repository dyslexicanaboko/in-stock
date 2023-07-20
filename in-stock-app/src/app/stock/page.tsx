export default async function StockPage() {
  //https://waltergalvao.dev/how-to-set-up-local-ssl-with-nextjs
  let stock: Stock;

  try {
    stock = await getStock();
  }
  catch(ex)
  {
    console.log(ex);

    throw ex;
  }

  return (<div><h1>Stock</h1>
    <label>StockId</label><span>{stock.stockId}</span>
    <label>Symbol</label><span>{stock.symbol}</span>
    <label>Name</label><span>{stock.name}</span>
    <label>Notes</label><span>{stock.notes}</span>
    <label>Created On UTC</label><span>{stock.createOnUtc.toDateString()}</span>
    </div>
  );
}

type Stock = {
  stockId: number,
  symbol: string,
  name: string,
  createOnUtc: Date,
  notes?: string
}

const getStock = async () : Promise<Stock> => {
  const token = await getToken();

  var headers = new Headers();
  headers.append("Content-Type", "application/json");
  headers.append("Authorization", "Bearer " + token);

  var request : RequestInit = {
    method: 'GET',
    headers: headers,
    redirect: 'follow'
  };

  const response = await fetch("https://localhost:44304/api/stock/cake/symbol", request);
  
  console.log(response);
  
  return response.json();
} 

const getToken = async () : Promise<string> => {  
  var headers = new Headers();
  headers.append("Content-Type", "application/json");

  var raw = JSON.stringify({
    "username": "User1",
    "password": "emmC2YNvh%9LtNMHWo#T"
  });

  var request : RequestInit = {
    method: 'POST',
    headers: headers,
    body: raw,
    redirect: 'follow'
  };

  const response = await fetch("https://localhost:44304/api/token", request);
  
  console.log(response);

  return response.text();
} 
