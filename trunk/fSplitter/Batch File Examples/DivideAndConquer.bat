c:\\fsplit -split 512 mb %1
if errorlevel 1 (@Echo there was an error with the parameters provided to the program.) else (
        for /f "tokens=1,2 delims=." %%a in ('dir /b %1') do (for %%y in (%%a_*.%%b) do call Conquer.bat %%y)
)


