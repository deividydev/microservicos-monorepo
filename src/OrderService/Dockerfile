# Step build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy soluction
COPY src/OrderService/OrderService.sln .

# Copy projects
COPY src/OrderService/Presentation/Presentation.csproj ./Presentation/
COPY src/OrderService/Application/Application.csproj ./Application/
COPY src/MessageBus/RabbitMQ/RabbitMQ.csproj ./MessageBus/RabbitMQ/

# Restore
RUN dotnet restore OrderService.sln

# Copy files
COPY src/OrderService ./OrderService
COPY src/MessageBus ./MessageBus

# Publish api 
RUN dotnet publish /src/OrderService/Presentation/Presentation.csproj -c Release -o /app/publish

# Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Presentation.dll"]
