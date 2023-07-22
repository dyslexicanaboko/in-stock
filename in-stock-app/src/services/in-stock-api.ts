import { Stock } from "./in-stock-types";

export const getStock = async (): Promise<Stock> => {
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
    "http://localhost:61478/api/stock/cake/symbol",
    request
  );

  console.log(response);

  return response.json();
};

export const getToken = async (): Promise<string> => {
  var headers = new Headers();
  headers.append("Content-Type", "application/json");

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

  const response = await fetch("http://localhost:61478/token", request);

  console.log(response);

  return response.text();
};
