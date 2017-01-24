REM @echo off

echo Building files to test

REM Recreate test files

IF NOT EXIST 10K.txt (
	echo 10 Kb File.
	copy /Y /B 1024.txt+1024.txt+1024.txt+1024.txt+1024.txt+1024.txt+1024.txt+1024.txt+1024.txt+1024.txt /B 10K.txt 
)

IF NOT EXIST 100K.txt (
	echo 100 Kb File 
	copy /Y /B 10K.txt+10K.txt+10K.txt+10K.txt+10K.txt+10K.txt+10K.txt+10K.txt+10K.txt+10K.txt /B 100K.txt
)

IF NOT EXIST 1M.txt (	
	echo 1 Mb File
	copy /Y /B 100K.txt+100K.txt+100K.txt+100K.txt+100K.txt+100K.txt+100K.txt+100K.txt+100K.txt+100K.txt+10K.txt+10K.txt+1024.txt+1024.txt+1024.txt+1024.txt /B 1M.txt
)

IF NOT EXIST 10M.txt (	
	echo 10 Mb File
	copy /Y /B 1M.txt+1M.txt+1M.txt+1M.txt+1M.txt+1M.txt+1M.txt+1M.txt+1M.txt+1M.txt /B 10M.txt
)

IF NOT EXIST 100M.txt (	
	echo 100Mb File
	copy /Y /B 10M.txt+10M.txt+10M.txt+10M.txt+10M.txt+10M.txt+10M.txt+10M.txt+10M.txt+10M.txt /B 100M.txt
)

IF NOT EXIST 1G.txt (	
	echo 1GB File
	copy /Y /B 100M.txt+100M.txt+100M.txt+100M.txt+100M.txt+100M.txt+100M.txt+100M.txt+100M.txt+100M.txt+10M.txt+10M.txt+1M.txt+1M.txt+1M.txt+1M.txt /B 1G.txt
)

IF NOT EXIST 10G.txt (	
	echo 10 GB File (Non FAT32 working)
	copy /Y /B 1G.txt+1G.txt+1G.txt+1G.txt+1G.txt+1G.txt+1G.txt+1G.txt+1G.txt+1G.txt /B 10G.txt
)

REM Create directories
for /l %x in (1, 1, 10) do (
IF NOT EXIST result%%x (	
	echo Creating directory result%%x
	md result%%x
)


SET FS =..\bin\Release\fSplit.exe

echo 100 Kb to 1 Kb files
%FS% -split 1 kb 100K.txt -df result1

echo 1 Mb to 100 Kb files
%FS% -split 100 kb 1M.txt -df result2

echo 10 Mb to 100 Kb files
%FS% -split 100 kb 10M.txt -df result3

echo 100 MB to 1 Mb files
%FS% -split 1 Mb 100M.txt -df result4

echo 1 GB to 10 Mb files
%FS% -split 10 Mb 1G.txt -df result5

echo 10 GB to 100 Mb files
%FS% -split 100 Mb 10G.txt -df result6


REM copy 256.txt toDelete_256.txt
REM ..\bin\Debug\fSplit.exe -split 1 kb toDelete_256.txt -d -f sp_{0:0000}_of_{1:0000}.txt -df result -lf files.txt


REM echo TEST2
REM copy 28.txt toDelete28.txt 
REM ..\bin\Debug\fSplit.exe -split 4 kb toDelete28.txt -df result
