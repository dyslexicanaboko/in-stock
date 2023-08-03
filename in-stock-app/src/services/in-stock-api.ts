import { Stock, SymbolV1Model, StockV1CreatedModel, PortfolioV1GetModel } from "./in-stock-api-models";

const baseUrl : string = "http://localhost:61478";

const getUrl = (path: string) : string => baseUrl + "/" + path;

const stockController = (path?: string) : string => {
  if(!path) {
    path = "";
  }

  return getUrl("api/stock/" + path);
}

const portfolioController = (path?: string) : string => {
  if(!path) {
    path = "";
  }

  return getUrl("api/portfolios/" + path);
}

export const getToken = async (): Promise<string> => {
  var headers = new Headers();
  headers.append("Content-Type", "application/json");

  //TODO: This is temporary until I figure out the right way to store and recall this crap
  //TODO: Need to work on expiration too
  var raw = JSON.stringify({
    username: "User1",
    password: "emmC2YNvh%9LtNMHWo#T",
  });

  var request: RequestInit = {
    method: "POST",
    headers: headers,
    body: raw,
    redirect: "follow",
  };

  const response = await fetch(getUrl("token"), request);

  //TODO: Need to decode the JWT to get the UserId out of it
  return response.text();
};

export const getStockBySymbol = async (symbol: string): Promise<Stock> => {
  const token = await getToken();

  var headers = new Headers();
  headers.append("Content-Type", "application/json");
  headers.append("Authorization", "Bearer " + token);

  var request: RequestInit = {
    method: "GET",
    headers: headers,
    redirect: "follow",
  };

  const response = await fetch(
    stockController(symbol + "/symbol"),
    request
  );

  return response.json();
};

export const getStockById = async (id: number): Promise<Stock> => {
  const token = await getToken();

  var headers = new Headers();
  headers.append("Content-Type", "application/json");
  headers.append("Authorization", "Bearer " + token);

  var request: RequestInit = {
    method: "GET",
    headers: headers,
    redirect: "follow",
  };

  const response = await fetch(
    stockController(id.toString()),
    request
  );

  return response.json();
};

export const createStock = async (symbol: string): Promise<StockV1CreatedModel> => {
  const token = await getToken();

  const model : SymbolV1Model = { symbol: symbol }; 

  const raw = JSON.stringify(model);

  var headers = new Headers();
  headers.append("Content-Type", "application/json");
  headers.append("Authorization", "Bearer " + token);

  var request: RequestInit = {
    method: "POST",
    headers: headers,
    body: raw,
    redirect: "follow",
  };

  const response = await fetch(
    stockController(),
    request
  );

  return response.json();
};

export const updateStock = async (id: number, notes?: string): Promise<void> => {
  const token = await getToken();

  const raw = JSON.stringify([{
    "op": "replace",
    "path": "/notes",
    "value": notes
  }]);

  var headers = new Headers();
  headers.append("Content-Type", "application/json");
  headers.append("Authorization", "Bearer " + token);

  var request: RequestInit = {
    method: "PATCH",
    headers: headers,
    body: raw,
    redirect: "follow",
  };

  await fetch(
    stockController(id.toString()),
    request
  );
};

export const getPortfolio = async (userId: number): Promise<PortfolioV1GetModel[]> => {
  const token = await getToken();

  var headers = new Headers();
  headers.append("Content-Type", "application/json");
  headers.append("Authorization", "Bearer " + token);

  var request: RequestInit = {
    method: "GET",
    headers: headers,
    redirect: "follow",
  };

  const response = await fetch(
    portfolioController(userId.toString()),
    request
  );

  return response.json();
};