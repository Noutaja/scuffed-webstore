FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /

COPY *.sln .

COPY ScuffedWebstore.Framework/ScuffedWebstore.Framework.csproj ScuffedWebstore.Framework/
COPY ScuffedWebstore.Controller/ScuffedWebstore.Controller.csproj ScuffedWebstore.Controller/
COPY ScuffedWebstore.Service/ScuffedWebstore.Service.csproj ScuffedWebstore.Service/
COPY ScuffedWebstore.Core/ScuffedWebstore.Core.csproj ScuffedWebstore.Core/
COPY ScuffedWebstore.Test/ScuffedWebstore.Test.csproj ScuffedWebstore.Test/

RUN dotnet restore

COPY ScuffedWebstore.Framework/ ScuffedWebstore.Framework/
COPY ScuffedWebstore.Controller/ ScuffedWebstore.Controller/
COPY ScuffedWebstore.Service/ ScuffedWebstore.Service/
COPY ScuffedWebstore.Core/ ScuffedWebstore.Core/
COPY ScuffedWebstore.Test/ ScuffedWebstore.Test/

WORKDIR /ScuffedWebstore.Framework
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /
COPY --from=build /publish .
EXPOSE 8080
ENTRYPOINT [ "dotnet", "ScuffedWebstore.Framework.dll" ]