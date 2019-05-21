#!/bin/sh

if [ -z "${CONNECTION_STRING}" ]; then 
  export CONNECTION_STRING="Server=planty-compare-db;Database=planty_compare_db;User Id=sa;Password=${SA_PASSWORD}";
fi

#export ASPNETCORE_AppSettings="ConnectionStrings:PlantyCompare=${CONNECTION_STRING}"
export ASPNETCORE_ConnectionStrings__PlantyCompare="${CONNECTION_STRING}"

dotnet ef database update -c MyIdentityDbContext -v || true
dotnet ef database update -c MyDbContext -v || true
exec dotnet out/planty-compare-portal.dll

