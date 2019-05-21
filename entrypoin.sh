export CONNECTION_STRING="Server=localhost;Database=planty_compare_db;User Id=sa;Password=${SA_PASSWORD}"

#export ASPNETCORE_AppSettings="ConnectionStrings:PlantyCompare=${CONNECTION_STRING}"
export ASPNETCORE_ConnectionStrings__PlantyCompare="${CONNECTION_STRING}"

dotnet planty-compare-portal.dll

