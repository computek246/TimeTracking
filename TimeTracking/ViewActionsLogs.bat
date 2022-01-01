
@echo off
sqlcmd -S . -E -d TimeTrack ^
-Q "SELECT ActionName,ActionDate,ProjectId FROM TimeTrack.dbo.ActionsLogs WHERE CAST(GETDATE() AS DATE)=CAST(ActionDate AS DATE)"

sqlcmd -S . -E -d TimeTrack ^
-Q "SELECT CONVERT(varchar, DATEADD(ms, DATEDIFF(SECOND,ActionDate,GETDATE()) * 1000, 0), 114) FROM dbo.ActionsLogs WHERE CAST(GETDATE() AS DATE)=CAST(ActionDate AS DATE)"

pause