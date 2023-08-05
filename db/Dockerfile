FROM postgres

# FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
# WORKDIR /app
# RUN dotnet tool install --global dotnet-ef
ENV POSTGRES_DB taskify
# ENV POSTGRES_USER taskify_auth
# ENV POSTGRES_PASSWORD Admin@123
COPY /migrations/. /docker-entrypoint-initdb.d
# RUN postgres psql

# dotnet tool install --global dotnet-ef