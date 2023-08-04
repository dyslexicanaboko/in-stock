import jwt_decode from "jwt-decode"; //https://stackoverflow.com/questions/53835816/decode-jwt-token-react, https://github.com/auth0/jwt-decode
import { Login, LoginResult } from "@/app/view-models/login";
import { BaseUrl } from "@/app/config";
import { ErrorModel } from "./in-stock-api-models";

const KeyToken = "token";
const KeyTokenExpiration = "token-expiration";
const KeyUserId = "user-id";
const EmptyToken = "";

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

  const response = await fetch(BaseUrl + "/token", request);

  if (response.ok) {
    const jwt = await response.text();

    localStorage.setItem(KeyToken, jwt);

    var decoded: any = jwt_decode(jwt);

    localStorage.setItem(KeyUserId, decoded.UserId);

    //TODO: Should shave off a minute (or more), so that there is room for refresh
    const expiration = new Date(parseInt(decoded.exp) * 1000).toISOString();

    localStorage.setItem(KeyTokenExpiration, expiration);

    return { isSuccess: true };
  }

  var errorModel: ErrorModel = await response.json();

  return { isSuccess: false, error: errorModel.Message };
};

export const getToken = (): string => {
  const token = localStorage.getItem(KeyToken);

  if (!token) {
    //How to log people out?
    console.log("logged out - token was null");

    return EmptyToken;
  }

  const isExpired = isLoginExpired();

  if (isExpired) return EmptyToken;

  return token;
};

export const getUserId = (): number => {
  const userId = localStorage.getItem(KeyUserId);

  if (!userId) {
    //Log out?
    return 0;
  }

  return parseInt(userId);
};

const isLoginExpired = (): boolean => {
  const expiration = localStorage.getItem(KeyTokenExpiration);

  if (!expiration) return true;

  const dtm = new Date(expiration);

  if (dtm <= new Date()) {
    //How to refresh tokens?
    console.log("logged out - token expired");

    return true;
  }

  return false;
};

export const isLoggedIn = () : boolean => {
  const token = getToken();
  const userId = getUserId();

  //If token and userId are valid, then user is logged in
  return token !== EmptyToken && userId > 0;
}

export const logout = (): void => {
  localStorage.removeItem(KeyToken);
  localStorage.removeItem(KeyUserId);
  localStorage.removeItem(KeyTokenExpiration);
};
