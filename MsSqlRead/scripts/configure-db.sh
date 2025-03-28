#!/bin/bash

# Wait 60 seconds for SQL Server to start up by ensuring that 
# calling SQLCMD does not return an error code, which will ensure that sqlcmd is accessible
# and that system and user databases return "0" which means all databases are in an "online" state
# https://docs.microsoft.com/en-us/sql/relational-databases/system-catalog-views/sys-databases-transact-sql?view=sql-server-2017 

echo "Starting init db script waiting 45 secs..."
sleep 45

DBSTATUS=1
ERRCODE=1
i=0

echo "Starting init db script"
while [[ $DBSTATUS -ne 0 ]] && [[ $i -lt 60 ]] && [[ $ERRCODE -ne 0 ]]; do
	i=$i+1
	echo "Try to connect..."
	DBSTATUS=$(/opt/mssql-tools18/bin/sqlcmd -C -N true -h -1 -t 1 -U sa -P Test1234= -Q "SET NOCOUNT ON; Select SUM(state) from sys.databases")
	ERRCODE=$?
	sleep 1
done

if [ $DBSTATUS -ne 0 ] OR [ $ERRCODE -ne 0 ]; then 
	echo "SQL Server took more than 60 seconds to start up or one or more databases are not in an ONLINE state"
	exit 1
fi

echo "Executing init script"
# Run the setup script to create the DB and the schema in the DB
/opt/mssql-tools18/bin/sqlcmd -C -N true -S localhost,1433 -U sa -P Test1234= -d master -i ./script.sql