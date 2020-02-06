@echo off

:START
cls

echo ==================
echo $      Menu      $
echo ==================
echo.
echo [1] Start
echo [2] Information
echo [3] Backup
echo [4] Close
echo.
set /p choice="Enter choice(number): "

if %choice%==1 (
    echo.
    echo Running program...
    cd program
    dotnet run  "..\In" "..\Out"
    cd ..
    echo Running python script..
    py website.py
    pause
    goto START

) else if %choice%==2 (
    echo.
    echo General purpose is create chessboard with possible moves of pawn on the basis of "In" folder and save results into "Out" folder.
    echo Script website.py is generating and opening website in browser.
    echo Created by Dominik Tyc
    echo.
    pause
    goto START

) else if %choice%==3 (
    echo.
    echo Creating backup of data...
    echo.
    set hh=%time:~0,2%
    set mm=%time:~3,2%
    set ss=%time:~6,2%
    set folder="backup\%date%-%hh%_%mm%_%ss%"

    mkdir "%folder%\In"
    mkdir "%folder%\Out"
    
    ROBOCOPY .\In %folder%\In
    ROBOCOPY .\Out %folder%\Out

    echo.
    pause
    goto START

) else if %choice%==4 (
    echo.
    echo Shutting the program...
    echo.
    pause
    exit /B 

) else (
    echo.
    echo Enter number from the list
    echo.
    pause
    goto START
)
