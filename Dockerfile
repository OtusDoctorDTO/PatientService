FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /PatientService

COPY ./PatientService.API ./PatientService.API
COPY ./PatientService.Data ./PatientService.Data
COPY ./PatientService.Domain ./PatientService.Domain

RUN dotnet restore ./PatientService.API
RUN dotnet publish ./PatientService.API -c Release -o publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /PatientService
COPY --from=base PatientService/publish ./
EXPOSE 8080
ENTRYPOINT [ "dotnet", "PatientService.API.dll", "--environment=Test" ]