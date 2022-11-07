FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /source
COPY ./Library_API .
RUN dotnet restore "Library_API.csproj"
RUN dotnet publish "Library_API.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app ./
EXPOSE 443
EXPOSE 80
ENTRYPOINT ["dotnet", "Library_API.dll"]