Next JS does not support proper local HTTPS development.
This is the GitHub issue about this: https://github.com/vercel/next.js/discussions/33768
> No matter what I tried I kept getting an error like this one:
```
TypeError: fetch failed
    at Object.fetch (node:internal/deps/undici/undici:11576:11)
    at process.processTicksAndRejections (node:internal/process/task_queues:95:5) {
  cause: Error: self-signed certificate
      at TLSSocket.onConnectSecure (node:_tls_wrap:1627:34)
      at TLSSocket.emit (node:events:512:28)
      at TLSSocket._finishInit (node:_tls_wrap:1038:8)
      at ssl.onhandshakedone (node:_tls_wrap:824:12)
      at TLSWrap.callbackTrampoline (node:internal/async_hooks:130:17) {
    code: 'DEPTH_ZERO_SELF_SIGNED_CERT'
```

None of these solutions worked:
- https://stackoverflow.com/questions/70440486/locally-developing-nextjs-and-fetch-getting-self-signed-cert-error/71558621
- https://code-specialist.com/cloud/nextjs-self-signed

When looking in the package.json you will see three dev scripts:
- dev: normal NextJs runtime via `npm run dev`
- dev1: `set NODE_EXTRA_CA_CERTS=.\\certs\\.capath && node src/scripts/create-local-server.mjs` this solution was adapted from the 1st URL above
  - I had to adapt it to work for Windows
  - The server script needed to have the `chalk` module removed because it was causing unecessary problems and is optional
  - Had to make this script into a module
- dev2: `set NODE_TLS_REJECT_UNAUTHORIZED=0 && node src/scripts/create-local-server.mjs` this solution was created from the 2nd URL above
  - Reused the server module script
    