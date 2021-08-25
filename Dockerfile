FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
RUN mkdir /app
COPY . ./app
WORKDIR /app/
RUN dotnet build -c Release -o output
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
# Create app dir and app user
RUN mkdir /app &&\
    groupadd app -g 10010 &&\
    useradd app -u 10010 -g 10010 -m -s /bin/sh -c "App user"
# Set default workdir
WORKDIR /app
COPY --from=build /app/output .
# Give perms to app user
RUN chown -R app:app /app
# Set default user
USER app
ENTRYPOINT ["dotnet", "FizzBuzz.Api.dll"]