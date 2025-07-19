# Use the .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
WORKDIR /app/EmployeeManagmentSystem
RUN dotnet restore EmployeeManagmentSystem.csproj
RUN dotnet publish EmployeeManagmentSystem.csproj -c Release -o out

# Use the .NET ASP.NET runtime image for running
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/EmployeeManagmentSystem/out .
ENTRYPOINT ["dotnet", "EmployeeManagmentSystem.dll"]