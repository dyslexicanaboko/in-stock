//https://nextjs.org/docs/pages/building-your-application/routing/middleware
//https://stackoverflow.com/questions/74573751/how-do-i-check-if-a-user-is-logged-in-on-every-request-in-next-js
import { isLoggedIn } from '@/services/user-info'
import { NextResponse } from 'next/server'
import type { NextRequest } from 'next/server'
 
// This function can be marked `async` if using `await` inside
export function middleware(request: NextRequest) {
  const loggedIn = isLoggedIn();

  if(!loggedIn) {
    //If not logged in, go home
    return NextResponse.redirect(new URL('/', request.url));
  }
  
  return NextResponse.next();
}
 
// Anything that isn't the home page which is root "/"
export const config = {
  matcher: '/:path*',
}
