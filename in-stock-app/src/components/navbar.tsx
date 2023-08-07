"use client";

// https://reacthustle.com/blog/next-js-add-navbar-to-all-pages
// https://picocss.com/docs/navs.html
import React, { useEffect, useState, useRef, useCallback } from "react";
import Link from "next/link";
import { isLoggedIn, logout } from "@/services/user-info";
import { useRouter } from "next/navigation";

const Navbar = () => {
  const router = useRouter();
  const [view, setView] = useState<JSX.Element>();

  const getView = (button: JSX.Element) : JSX.Element => (
      <nav>
        <ul>
          <li>Stock</li>
          <li>
            <Link href="/stock" className="secondary">
              Search
            </Link>
          </li>
          <li>Portfolio</li>
          <li>
            <Link href="/portfolio" className="secondary">
              Positions
            </Link>
          </li>
        </ul>
        <ul>
          <li>
            <strong>In Stock</strong>
          </li>
        </ul>
        <ul>
          <li>{button}</li>
        </ul>
      </nav>
    );

  const logoutHandler = (): void => {
    logout();

    window.location.href = "/";

    //router.push("/"); //This is what I want to use, but I can't because the Nav itself never knows when someone logged in or not
  };

  isLoggedIn().then((loginStatus) => {
    let button;

    if (loginStatus) {
      button = <button onClick={() => logoutHandler()}>Logout</button>;
    } else {
      button = (
        <Link href="/" className="secondary">
          Login
        </Link>
      );
    }

    setView(getView(button));
  });

  return view ?? getView(<></>);
};

export default Navbar;
