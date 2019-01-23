@echo off
set "message=%1"
if [%1]==[] goto INPUT
if NOT [%1]==[] goto COMMIT
:INPUT
set /p MESSAGE=Enter commit message: 
:COMMIT
@echo on
git add -A
git commit -m %message%
git push
@echo off
set /p=Press any key to exit...
