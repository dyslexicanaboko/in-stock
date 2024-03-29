import {
  Stock,
  SymbolV1Model,
  StockV1CreatedModel,
  PortfolioV1GetModel,
  PositionV1GetModel,
  PositionV1GetCalculatedModel,
  PositionV1CreateModel,
  PositionV1PatchModel,
} from "./in-stock-api-models";
import { BaseUrl } from "@/app/config";
import { EmptyToken, getToken } from "./user-info";
import { EmptyString, redirectToLoginPage } from "./common";

const getUrl = (path: string): string => BaseUrl + "/" + path;

const stockController = (path?: string): string => {
  if (!path) {
    path = EmptyString;
  }

  return getUrl("api/stock/" + path);
};

const portfolioController = (path?: string): string => {
  if (!path) {
    path = EmptyString;
  }

  return getUrl("api/portfolios/" + path);
};

const positionsController = (path?: string): string => {
  if (!path) {
    path = EmptyString;
  } else {
    path = "/" + path;
  }

  return getUrl("api/positions" + path);
};

//Because JS is lame, there is no `nameof()` equivalent like in C#
const getPatchOperation = (path: string, value: any) : any => {
  return {
    op: "replace",
    path: "/" + path,
    value: value,
  }
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

export const getPosition = async (
  id: number
): Promise<PositionV1GetModel> => {
  const headers = await getHeaders();

  var request: RequestInit = {
    method: "GET",
    headers: headers,
    redirect: "follow",
  };

  const response = await fetch(positionsController(id.toString()), request);

  return response.json();
};

export const getPositions = async (
  symbol: string
): Promise<PositionV1GetModel[]> => {
  const headers = await getHeaders();

  var request: RequestInit = {
    method: "GET",
    headers: headers,
    redirect: "follow",
  };

  const response = await fetch(positionsController(symbol + "/symbol"), request);

  return response.json();
};

export const getCalculatedPositions = async (
  symbol: string
): Promise<PositionV1GetCalculatedModel[]> => {
  const headers = await getHeaders();

  var request: RequestInit = {
    method: "GET",
    headers: headers,
    redirect: "follow",
  };

  const response = await fetch(positionsController(symbol + "/symbolCalculated"), request);

  return response.json();
};

export const createPosition = async (
  position: PositionV1CreateModel
): Promise<PositionV1GetModel> => {
  const headers = await getHeaders();

  const raw = JSON.stringify(position);

  var request: RequestInit = {
    method: "POST",
    headers: headers,
    body: raw,
    redirect: "follow",
  };

  const response = await fetch(positionsController(), request);

  return response.json();
};

export const updatePosition = async (
  id: number,
  model: PositionV1PatchModel
): Promise<void> => {
  const headers = await getHeaders();

  //TODO: Automatically convert object to JsonPatchDocument
  const raw = JSON.stringify([
    getPatchOperation("dateOpened", model.dateOpened),
    getPatchOperation("dateClosed", model.dateClosed),
    getPatchOperation("price", model.price),
    getPatchOperation("quantity", model.quantity),
  ]);

  var request: RequestInit = {
    method: "PATCH",
    headers: headers,
    body: raw,
    redirect: "follow",
  };

  await fetch(positionsController(id.toString()), request);
};
