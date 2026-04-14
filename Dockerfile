FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000
ENV DOTNET_USE_POLLING_FILE_WATCHER=true
ENV ASPNETCORE_hostBuilder__reloadConfigOnChange=false

EXPOSE 10000
ENTRYPOINT ["dotnet", "QuantityMeasurementApp.Api.dll"]