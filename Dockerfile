#FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
FROM mcr.microsoft.com/dotnet/core/sdk:2.2

RUN curl -sL https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install -y nodejs
RUN echo "node -v: $(node -v)"
RUN echo "npm -v: $(npm -v)"

WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
#COPY aspnetapp/*.csproj ./aspnetapp/
COPY ./*.csproj ./
RUN dotnet restore

# copy everything else and build app
#COPY aspnetapp/. ./aspnetapp/
COPY . ./

##WORKDIR /app/aspnetapp
RUN dotnet publish -c Release -o out

#FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
#WORKDIR /app
#COPY --from=build /app/aspnetapp/out ./

RUN chmod +x ./entrypoint.sh
ENTRYPOINT ["sh", "./entrypoint.sh"]

