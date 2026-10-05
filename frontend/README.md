# Frontend de Dulce Pro

Interfaz web independiente, escrita con HTML, CSS y JavaScript sin paquetes ni framework. El servidor local también funciona como proxy HTTP hacia la API ASP.NET Core; no hay acceso directo a SQL Server ni cambios al backend.

## Ejecución

En una terminal, iniciar la API desde la raíz del proyecto:

```powershell
dotnet run --launch-profile http
```

En otra terminal, iniciar el frontend:

```powershell
cd frontend
node server.js
```

Abrir `http://localhost:5173`. Si la API se ejecuta en otra URL, definir `API_BASE_URL`, por ejemplo:

```powershell
$env:API_BASE_URL = 'https://localhost:7211'; node server.js
```
