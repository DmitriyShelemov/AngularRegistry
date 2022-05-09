# Registry WebSite
### _Prerequisites_

- Node.js v18.1.0
- VSCode
- Visual Studio 2022
- Docker desktop
- MSSQL(optional)

### Startup steps

- Open backend/RegisterApi.sln in Visual Studio
- Ensure port 888 is free
- In case if you interested to user MsSql database then open appsettings.json and check the following:
  - change 'UseInMemoryDb' to false
  - ensure that connection string is suitable
- Run solution by F5
- Open frontend folder in VSCode
- Ensure port 8881 is free
- Run npm install in terminal
- Run npm start in terminal
- Open http://localhost:8881 in browser