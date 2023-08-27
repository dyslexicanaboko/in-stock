import { createServer as createHttpsServer } from 'https';
import next from 'next';
import { existsSync, readFileSync } from 'fs';

const dev = process.env.NODE_ENV !== 'production';
const app = next({ dev });
const handle = app.getRequestHandler();
const PORT = process.env.PORT || 3000;

if (!existsSync('./certs/.capath')) {
  console.error(red('\nError: Missing SSL certificates\n'));

  console.error(`To fix this error, run the command below:`);
  console.error("→ npm run ssl:setup");

  process.exit();
}

app
  .prepare()
  .then(() => {
    const server = createHttpsServer(
      {
        key: readFileSync('./certs/devcert.key'),
        cert: readFileSync('./certs/devcert.cert'),
      },
      (req, res) => handle(req, res)
    );

    return server.listen(PORT, (err) => {
      if (err) throw err;

      console.log('> Ready on https://certs.ArchimedesJr.local:3000')
    });
  })
  .catch((err) => {
    console.error(err);
  });
