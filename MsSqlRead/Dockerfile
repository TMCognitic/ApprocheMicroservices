FROM mcr.microsoft.com/mssql/server:2022-latest

WORKDIR /app

COPY ./scripts .

RUN chmod +x ./entrypoint.sh
RUN chmod +x ./configure-db.sh

ENTRYPOINT [ "./entrypoint.sh" ]