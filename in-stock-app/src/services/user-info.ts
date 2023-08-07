import jwt_decode from "jwt-decode"; //https://stackoverflow.com/questions/53835816/decode-jwt-token-react, https://github.com/auth0/jwt-decode
import {
  UserV1PostModel,
  RefreshTokenV1PostModel,
  LoginResult,
} from "@/app/view-models/login";
import { BaseUrl } from "@/app/config";
import { ErrorModel } from "./in-stock-api-models";

const KeyToken = "token";
const KeyTokenExpiration = "token-expiration";
const KeyRefreshToken = "refresh-token";
const KeyUserId = "user-id";
export const EmptyToken = "";

export const redirectToLoginPage = (): void => {
  window.location.href = "/";
};

export const login = async (login: UserV1PostModel): Promise<LoginResult> =>
  await fetchLogin("/token", login);

const attemptReLogin = async (
  refreshToken: RefreshTokenV1PostModel
): Promise<LoginResult> => await fetchLogin("/token/refresh", refreshToken);

const fetchLogin = async (
  path: string,
  postBody: any
): Promise<LoginResult> => {
  var headers = new Headers();
  headers.append("Content-Type", "application/json");

  var raw = JSON.stringify(postBody);

  var request: RequestInit = {
    method: "POST",
    headers: headers,
    body: raw,
    redirect: "follow",
  };

  const response = await fetch(BaseUrl + path, request);

  if (response.ok) {
    const jwt = await response.text();

    localStorage.setItem(KeyToken, jwt);

    var decoded: any = jwt_decode(jwt);

    localStorage.setItem(KeyUserId, decoded.UserId);
    localStorage.setItem(KeyRefreshToken, decoded.RefreshToken);

    //TODO: Should shave off a minute (or more), so that there is room for refresh
    const expiration = new Date(parseInt(decoded.exp) * 1000).toISOString();

    localStorage.setItem(KeyTokenExpiration, expiration);

    return { isSuccess: true };
  }

  var errorModel: ErrorModel = await response.json();

  return { isSuccess: false, error: errorModel.Message };
};

export const getToken = async (): Promise<string> => {
  const token = localStorage.getItem(KeyToken);

  if (!token) {
    console.warn("logged out - token was null");

    return EmptyToken;
  }

  const isExpired = isLoginExpired();

  if (isExpired) {
    const refreshToken = localStorage.getItem(KeyRefreshToken);

    if (!refreshToken) {
      console.warn("logged out - refresh token was null");

      return EmptyToken;
    }

    const model: RefreshTokenV1PostModel = { refreshToken: refreshToken };

    const result = await attemptReLogin(model);

    if (!result.isSuccess) {
      console.warn("logged out - refresh attempt failed");

      return EmptyToken;
    }

    return await getToken();
  }

  return token;
};

export const getUserId = (): number => {
  const userId = localStorage.getItem(KeyUserId);

  if (!userId) {
    return 0;
  }

  return parseInt(userId);
};

const isLoginExpired = (): boolean => {
  const expiration = localStorage.getItem(KeyTokenExpiration);

  if (!expiration) return true;

  const dtm = new Date(expiration);

  if (dtm <= new Date()) {
    console.warn("logged out - token expired");

    return true;
  }

  return false;
};

export const isLoggedIn = async (): Promise<boolean> => {
  const token = await getToken();
  const userId = getUserId();

  //If token and userId are valid, then user is logged in
  return token !== EmptyToken && userId > 0;
};

export const logout = (): void => {
  localStorage.removeItem(KeyToken);
  localStorage.removeItem(KeyUserId);
  localStorage.removeItem(KeyTokenExpiration);
  localStorage.removeItem(KeyRefreshToken);
};
