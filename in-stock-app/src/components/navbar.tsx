// https://reacthustle.com/blog/next-js-add-navbar-to-all-pages
// https://picocss.com/docs/navs.html
import React from "react";
import Link from "next/link";

const Navbar = () => {
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
        <li><Link href="/" className="secondary">Home</Link></li>
      </ul>
    </nav>
  );
};

export default Navbar;
