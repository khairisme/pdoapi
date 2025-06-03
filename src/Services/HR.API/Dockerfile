FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
ENV GRPC_TOOLS_CPU_ARCH=x64
COPY ["src/Services/HR.API/HR.API.csproj", "src/Services/HR.API/"]
COPY ["src/Services/HR.Application/HR.Application.csproj", "src/Services/HR.Application/"]
COPY ["src/Services/HR.Core/HR.Core.csproj", "src/Services/HR.Core/"]
COPY ["src/Services/HR.Infrastructure/HR.Infrastructure.csproj", "src/Services/HR.Infrastructure/"]
COPY ["src/Shared/Shared.Common/Shared.Common.csproj", "src/Shared/Shared.Common/"]
COPY ["src/Shared/Shared.Contracts/Shared.Contracts.csproj", "src/Shared/Shared.Contracts/"]
COPY ["src/Shared/Shared.Messaging/Shared.Messaging.csproj", "src/Shared/Shared.Messaging/"]
RUN dotnet restore "src/Services/HR.API/HR.API.csproj"
COPY . .
WORKDIR "/src/src/Services/HR.API"
RUN dotnet build "HR.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HR.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HR.API.dll"]
