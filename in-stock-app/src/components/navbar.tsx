"use client"

// https://reacthustle.com/blog/next-js-add-navbar-to-all-pages
// https://picocss.com/docs/navs.html
import React, { useState } from "react";
import Link from "next/link";
import { isLoggedIn, logout } from "@/services/user-info";
import { useRouter } from 'next/navigation'

const Navbar = () => {
  const router = useRouter();
  const [loggedIn, setLoggedIn] = useState(isLoggedIn());
  let jsx: JSX.Element;

  const logoutHandler = () : void => {
    setLoggedIn(false);

    logout();

    router.push("/");
  }

  //TODO: Need to figure out how to refresh the navbar jsx when a user logs in and out.
  if(loggedIn) {
    jsx = <button onClick={() => logoutHandler()}>Logout</button>;
  } else {
    jsx = <Link href="/" className="secondary">Login</Link>;
  }

  return (
    <nav>
      <ul>
        <li>Stock</li>
        <li>
            <Link href="/stock" className="secondary">Search</Link>
        </li>
        <li>Portfolio</li>
        <li>
            <Link href="/portfolio" className="secondary">Positions</Link>
        </li>
      </ul>
      <ul>
        <li><strong>In Stock</strong></li>
      </ul>
      <ul>
        <li>{jsx}</li>
      </ul>
    </nav>
  );
};

export default Navbar;
