@echo off

for /F "delims=" %%a in ('dotnet ef dbcontext list') do (
	echo.%%~a | findstr /irc:"DbContext" >nul 2>nul && (
		echo.%%~a | findstr /irc:"ApplicationDbContext" >nul 2>nul && (
			set appContext=%%~a
		) || (
			set dataContext=%%~a
		)
	)
)

REM if defined appContext echo.appContext :%appContext%
REM if defined dataContext echo.dataContext :%dataContext%
REM pause


for /F "tokens=1* delims=<" %%A in (Entities.txt) do (
	for /F "tokens=1* delims=>" %%A in ("%%B") do (
		for /F "tokens=1 delims= " %%R in ("%%B") do (
			dotnet aspnet-codegenerator controller --model %%~A --dataContext %dataContext% --useDefaultLayout --referenceScriptLibraries --relativeFolderPath Controllers --useAsyncActions --controllerName %%~RController --force
		)
	)
)

pause