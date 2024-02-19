# SheetIncision.Backend
### Solution based on SheetIncision.Backend.Web-API

*Description:* web API for calculating amount of made pieces on Sheet N x M cells after marking arbitrary amount of cells.

*Stack:* ASP.NET Core (.Net 8), Serilog, NUnit.

*Hosting:* web API is hosted on Azure with domen: sheet-incision-web-api.azurewebsites.net

*Endpoints:*
| Method | URL | Description |
| ------- | --------------------------------------------------------- | ----------- |
| Get | /api/SheetIncision/GetNumberOfPieces?data=*object with matrix and allowDiagonals fields*  | Retrieve amount of pieces sheet was cut |

*Frontend:* https://github.com/SheetIncision/SheetIncision.Frontend.git
