@echo off
echo ===================================
echo   Push to GitHub - Linebot
echo ===================================
echo.

git add .
git commit -m "first commit"
git branch -M main
git remote add origin https://github.com/chinjungnattapong123/Linebot.git 2>nul
git push -u origin main

echo.
echo ===================================
echo   Done!
echo ===================================
pause
