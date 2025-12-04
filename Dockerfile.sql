FROM mcr.microsoft.com/mssql/server:2022-latest
ENV SA_PASSWORD=Your_password123
ENV ACCEPT_EULA=Y
ENV MSSQL_PID=Developer
EXPOSE 1433
