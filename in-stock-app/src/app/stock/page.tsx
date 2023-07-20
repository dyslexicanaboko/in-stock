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
    <label>StockId</label>&nbsp;<span>{stock.stockId}</span><br/>
    <label>Symbol</label>&nbsp;<span>{stock.symbol}</span><br/>
    <label>Name</label>&nbsp;<span>{stock.name}</span><br/>
    <label>Notes</label>&nbsp;<span>{stock.notes}</span><br/>
    <label>Created On UTC</label>&nbsp;<span>{new Date(stock.createOnUtc).toISOString()}</span>
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

  const response = await fetch("http://localhost:61478/api/stock/cake/symbol", request);
  
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

  const response = await fetch("http://localhost:61478/token", request);
  
  console.log(response);

  return response.text();
} 
