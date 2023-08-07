"use client";

import { useState } from "react";
import { Login } from "./view-models/login";
import { login as apiLogin } from "@/services/user-info"
import Waiting from "@/components/waiting";
import Container from "@/components/container";
import { useRouter } from 'next/navigation'

export default function Home() {
  const router = useRouter();
  const [view, setView] = useState<JSX.Element>();
  const [loginInfo, setLoginInfo] = useState<Login>({
    username: "User1",
    password: "emmC2YNvh%9LtNMHWo#T",
  });

/*

{
    username: "",
    password: "",
  }

{
    username: "User1",
    password: "emmC2YNvh%9LtNMHWo#T",
  }
*/

  const pause = async (delayInMs: number) : Promise<void> => {
    return new Promise(res => setTimeout(res, delayInMs));
  }

  const login = (): void => {
    apiLogin(loginInfo).then((result) => {
      setView(
        <Container>
          <textarea 
            readOnly={true}
            rows={result.isSuccess ? 1 : 2}
            wrap="soft"
            value={result.isSuccess ? "Success" : result.error} aria-invalid={result.isSuccess ? "false" : "true"} />
        </Container>
      );

      pause(1000).then(() => {
        window.location.href = "/portfolio";
        //router.push("/portfolio"); //No way to notify Navbar that the user logged in, full refresh required
      });
    });

    setView(<Waiting />);
  };

  const getDefaultLogin = (): void => {
    setView(
      <main className="container">
        <div className="grid">
          <div />
          <label htmlFor="username">
            Username
            <input
              type="text"
              name="username"
              placeholder="Username"
              required
              onChange={(e) =>
                setLoginInfo({ ...loginInfo, username: e.target.value })
              }
            />
          </label>
          <div />
        </div>
        <div className="grid">
          <div />
          <label htmlFor="password">
            Password
            <input
              type="password"
              name="password"
              placeholder="Password"
              required
              onChange={(e) =>
                setLoginInfo({ ...loginInfo, password: e.target.value })
              }
            />
          </label>
          <div />
        </div>
        <div className="grid">
          <div />
          <button onClick={() => login()}>Login</button>
          <div />
        </div>
      </main>
    );
  };

  return view ?? getDefaultLogin();
}
