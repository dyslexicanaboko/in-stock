"use client"

// https://reacthustle.com/blog/next-js-add-navbar-to-all-pages
// https://picocss.com/docs/navs.html
import React, { useEffect, useState, useRef, useCallback } from "react";
import Link from "next/link";
import { isLoggedIn, logout } from "@/services/user-info";
import { useRouter } from 'next/navigation'

const Navbar = () => {
  const router = useRouter();
  const jsx = useRef<JSX.Element>(<></>);

  const logoutHandler = () : void => {
    logout();

    window.location.href = "/";

    //router.push("/"); //This is what I want to use, but I can't because the Nav itself never knows when someone logged in or not
  };

  if(isLoggedIn()) {
    jsx.current = <button onClick={() => logoutHandler()}>Logout</button>;
  } else {
    jsx.current = <Link href="/" className="secondary">Login</Link>;
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
        <li>{jsx.current}</li>
      </ul>
    </nav>
  );
};

export default Navbar;
