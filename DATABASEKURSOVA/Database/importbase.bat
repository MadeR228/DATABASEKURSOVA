@echo off
cd "C:\Program Files\MySQL\MySQL Server 8.0\bin"
set CURRENT_DIR=%~dp0
set SQL_FILE=%CURRENT_DIR%trolleydepot.sql
mysql -u root --password=%DB_PASSWORD% < "%SQL_FILE%"