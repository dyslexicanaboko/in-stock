import { Stock, SymbolV1Model, StockV1CreatedModel } from "./in-stock-types";

const baseUrl : string = "http://localhost:61478";
const getUrl = (path: string) : string => baseUrl + "/" + path;
const stockController = (path?: string) : string => {
  if(!path) {
    path = "";
  }

  return getUrl("api/stock/" + path);
}

export const getStock = async (symbol: string): Promise<Stock> => {
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

  return response.text();
};
