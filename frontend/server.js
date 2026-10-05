// Servidor estático y proxy local sin dependencias externas.
// El navegador llama a /api; este proceso reenvía la solicitud a ASP.NET Core.
const http = require('http');
const fs = require('fs');
const path = require('path');
const { URL } = require('url');

const port = Number(process.env.FRONTEND_PORT || 5173);
const apiBase = process.env.API_BASE_URL || 'http://localhost:5154';
const root = __dirname;
const mime = { '.html':'text/html; charset=utf-8', '.js':'application/javascript; charset=utf-8', '.css':'text/css; charset=utf-8' };

http.createServer((request, response) => {
  const requestUrl = new URL(request.url, `http://${request.headers.host}`);
  if (requestUrl.pathname === '/api' || requestUrl.pathname.startsWith('/api/')) {
    const target = new URL(requestUrl.pathname + requestUrl.search, apiBase);
    const proxy = http.request(target, { method: request.method, headers: { ...request.headers, host: target.host } }, upstream => {
      response.writeHead(upstream.statusCode || 502, upstream.headers);
      upstream.pipe(response);
    });
    proxy.on('error', error => { response.writeHead(502, { 'content-type':'application/json' }); response.end(JSON.stringify({ message:`No se pudo conectar con la API: ${error.message}` })); });
    request.pipe(proxy); return;
  }
  const relative = requestUrl.pathname === '/' ? '/index.html' : requestUrl.pathname;
  const file = path.resolve(root, '.' + relative);
  if (!file.startsWith(root + path.sep) || !fs.existsSync(file) || fs.statSync(file).isDirectory()) { response.writeHead(404); response.end('No encontrado'); return; }
  response.writeHead(200, { 'content-type': mime[path.extname(file)] || 'application/octet-stream' });
  fs.createReadStream(file).pipe(response);
}).listen(port, () => console.log(`Dulce Pro frontend: http://localhost:${port}  →  API: ${apiBase}`));
