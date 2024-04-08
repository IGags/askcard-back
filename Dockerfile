FROM mcr.microsoft.com/dotnet/sdk:7.0.404-1-alpine3.18-amd64 as base
WORKDIR /app
EXPOSE 80/tcp
COPY . .
RUN dotnet publish "./Api/Api.csproj" -c Release -o ./publish
WORKDIR /app/publish
ENTRYPOINT ["dotnet", "Api.dll"]