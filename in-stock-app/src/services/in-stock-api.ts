import {
  Stock,
  SymbolV1Model,
  StockV1CreatedModel,
  PortfolioV1GetModel,
} from "./in-stock-api-models";
import { BaseUrl } from "@/app/config";
import { EmptyToken, getToken } from "./user-info";

const getUrl = (path: string): string => BaseUrl + "/" + path;

const stockController = (path?: string): string => {
  if (!path) {
    path = "";
  }

  return getUrl("api/stock/" + path);
};

const portfolioController = (path?: string): string => {
  if (!path) {
    path = "";
  }

  return getUrl("api/portfolios/" + path);
};

const getHeaders = async (): Promise<Headers> => {
  const token = await getToken();

  if(token === EmptyToken) {
    redirectToLoginPage();
  }

  var headers = new Headers();
  headers.append("Content-Type", "application/json");
  headers.append("Authorization", "Bearer " + token);

  return headers;
};

export const getStockBySymbol = async (symbol: string): Promise<Stock> => {
  const headers = await getHeaders();

  var request: RequestInit = {
    method: "GET",
    headers: headers,
    redirect: "follow",
  };

  const response = await fetch(stockController(symbol + "/symbol"), request);

  return response.json();
};

export const getStockById = async (id: number): Promise<Stock> => {
  const headers = await getHeaders();

  var request: RequestInit = {
    method: "GET",
    headers: headers,
    redirect: "follow",
  };

  const response = await fetch(stockController(id.toString()), request);

  return response.json();
};

export const createStock = async (
  symbol: string
): Promise<StockV1CreatedModel> => {
  const headers = await getHeaders();

  const model: SymbolV1Model = { symbol: symbol };

  const raw = JSON.stringify(model);

  var request: RequestInit = {
    method: "POST",
    headers: headers,
    body: raw,
    redirect: "follow",
  };

  const response = await fetch(stockController(), request);

  return response.json();
};

export const updateStock = async (
  id: number,
  notes?: string
): Promise<void> => {
  const headers = await getHeaders();

  const raw = JSON.stringify([
    {
      op: "replace",
      path: "/notes",
      value: notes,
    },
  ]);

  var request: RequestInit = {
    method: "PATCH",
    headers: headers,
    body: raw,
    redirect: "follow",
  };

  await fetch(stockController(id.toString()), request);
};

export const getPortfolio = async (
  userId: number
): Promise<PortfolioV1GetModel[]> => {
  const headers = await getHeaders();

  var request: RequestInit = {
    method: "GET",
    headers: headers,
    redirect: "follow",
  };

  const response = await fetch(portfolioController(userId.toString()), request);

  return response.json();
};
function redirectToLoginPage() {
  throw new Error("Function not implemented.");
}

