# escape=`

# Setup arguments
ARG BuildConfiguration=Debug
ARG SYSTEM_ACCESSTOKEN=token
ARG ASSEMBLY_VERSION=0.1
ARG FILE_VERSION=0.1.0.0


FROM microsoft/dotnet:2.0-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.0-sdk AS build
ARG BuildConfiguration
ARG SYSTEM_ACCESSTOKEN
ARG ASSEMBLY_VERSION
ARG FILE_VERSION
ENV BuildConfiguration=$BuildConfiguration `
    ASSEMBLY_VERSION=$ASSEMBLY_VERSION `
    FILE_VERSION=$FILE_VERSION `
    SYSTEM_ACCESSTOKEN=$SYSTEM_ACCESSTOKEN
WORKDIR /build
COPY *.sln ./
COPY src/ImageProcessor.Business/ImageProcessor.Business.csproj src/ImageProcessor.Business/
COPY src/ImageProcessor.Service/ImageProcessor.Service.csproj src/ImageProcessor.Service/
COPY src/ImageProcessor.Service.Hosting/ImageProcessor.Service.Hosting.csproj src/ImageProcessor.Service.Hosting/
COPY src/ImageProcessor.Service.Hosting.Abstractions/ImageProcessor.Service.Hosting.Abstractions.csproj src/ImageProcessor.Service.Hosting.Abstractions/
RUN dotnet restore
COPY . .
WORKDIR /build/src/ImageProcessor.Service
RUN dotnet build --no-restore -c $BuildConfiguration -o /app /p:"Version=$ASSEMBLY_VERSION"

FROM build AS publish
RUN dotnet publish --no-restore -c $BuildConfiguration -o /app /p:"Version=$ASSEMBLY_VERSION"

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "lra.ImageProcessor.Service.dll"]
