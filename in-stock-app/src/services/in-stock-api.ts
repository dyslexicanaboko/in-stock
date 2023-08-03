import { Login, LoginResult } from "@/app/view-models/login";
import {
  Stock,
  SymbolV1Model,
  StockV1CreatedModel,
  PortfolioV1GetModel,
  ErrorModel,
} from "./in-stock-api-models";
import jwt_decode from "jwt-decode"; //https://stackoverflow.com/questions/53835816/decode-jwt-token-react, https://github.com/auth0/jwt-decode

const baseUrl: string = "http://localhost:61478";

const getUrl = (path: string): string => baseUrl + "/" + path;

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

const getHeaders = (): Headers => {
  const token = getToken();

  var headers = new Headers();
  headers.append("Content-Type", "application/json");
  headers.append("Authorization", "Bearer " + token);

  return headers;
};

export const login = async (login: Login): Promise<LoginResult> => {
  var headers = new Headers();
  headers.append("Content-Type", "application/json");

  var raw = JSON.stringify(login);

  var request: RequestInit = {
    method: "POST",
    headers: headers,
    body: raw,
    redirect: "follow",
  };

  const response = await fetch(getUrl("token"), request);

  if (response.ok) {
    const jwt = await response.text();

    localStorage.setItem("token", jwt);

    var decoded:any = jwt_decode(jwt);

    console.log(decoded);

    localStorage.setItem("user-id", decoded.UserId);

    //TODO: Should shave off a minute (or more), so that there is room for refresh
    const expiration = new Date(parseInt(decoded.exp)*1000).toISOString();

    localStorage.setItem("token-expiration", expiration);

    return { isSuccess: true };
  }

  var errorModel : ErrorModel = await response.json();

  return { isSuccess: false, error: errorModel.Message };
};

const getToken = (): string => {
  const token = localStorage.getItem("token");

  if (!token) {
    //How to log people out?
    console.log("logged out - token was null");

    return "";
  }

  const expiration = localStorage.getItem("token-expiration");

  if(!expiration) {
    //How to log people out?
    console.log("logged out - expiration was null");

    return "";
  }

  const dtm = new Date(expiration);

  if(dtm <= new Date()) {
    //How to refresh tokens?
    console.log("logged out - token expired");

    return "";
  }

  return token;
};

export const getStockBySymbol = async (symbol: string): Promise<Stock> => {
  const headers = getHeaders();

  var request: RequestInit = {
    method: "GET",
    headers: headers,
    redirect: "follow",
  };

  const response = await fetch(stockController(symbol + "/symbol"), request);

  return response.json();
};

export const getStockById = async (id: number): Promise<Stock> => {
  const headers = getHeaders();

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
  const headers = getHeaders();

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
  const headers = getHeaders();

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
  const headers = getHeaders();

  var request: RequestInit = {
    method: "GET",
    headers: headers,
    redirect: "follow",
  };

  const response = await fetch(portfolioController(userId.toString()), request);

  return response.json();
};
