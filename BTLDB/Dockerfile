FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY bin/Release/net8.0/* ./
RUN ls /app
RUN ls ./
ENTRYPOINT ["dotnet", "BTLDB.dll"]
