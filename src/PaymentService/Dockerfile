# Step build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy soluction
COPY src/PaymentService/PaymentService.sln .

# Copy projects
COPY src/PaymentService/Worker/Worker.csproj ./Worker/
COPY src/MessageBus/RabbitMQ/RabbitMQ.csproj ./MessageBus/RabbitMQ/

# Restore
RUN dotnet restore PaymentService.sln

# Copy files
COPY src/PaymentService ./PaymentService
COPY src/MessageBus ./MessageBus

# Publish api 
RUN dotnet publish /src/PaymentService/Worker/Worker.csproj -c Release -o /app/publish

# Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Worker.dll"]
