@echo off

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

IF NOT EXIST 900M.txt (	
	echo 900Mb File
	copy /Y /B 100M.txt+100M.txt+100M.txt+100M.txt+100M.txt+100M.txt+100M.txt+100M.txt+100M.txt /B 900M.txt
)


IF NOT EXIST 1G.txt (	
	echo 1GB File
	copy /Y /B 100M.txt+100M.txt+100M.txt+100M.txt+100M.txt+100M.txt+100M.txt+100M.txt+100M.txt+100M.txt+10M.txt+10M.txt+1M.txt+1M.txt+1M.txt+1M.txt /B 1G.txt
)

IF NOT EXIST 10G.txt (	
	echo 10 GB File
	copy /Y /B 1G.txt+1G.txt+1G.txt+1G.txt+1G.txt+1G.txt+1G.txt+1G.txt+1G.txt+1G.txt /B 10G.txt
)

SET FS=..\bin\Release\fSplit.exe

IF NOT EXIST result1 (	
	echo 100 Kb to 1 Kb files
	md result1
	%FS% -split 1 kb 100K.txt -df result1
)
IF NOT EXIST result2 (	
	echo 1 Mb to 100 Kb files
	md result2
	%FS% -split 100 kb 1M.txt -df result2
)

IF NOT EXIST result3 (	
	echo 10 Mb to 100 Kb files
	md result3
	%FS% -split 100 kb 10M.txt -df result3
)

IF NOT EXIST result4 (	
	echo 100 MB to 1 Mb files
	md result4
	%FS% -split 1 Mb 100M.txt -df result4
)

IF NOT EXIST result5 (	
	echo 1 GB to 10 Mb files
	md result5
	%FS% -split 10 Mb 1G.txt -df result5
)

IF NOT EXIST result6 (	
	echo 10 GB to 100 Mb files
	md result6
	%FS% -split 100 Mb 10G.txt -df result6
)

IF NOT EXIST result7 (	
	echo 900 Mb to 10 Mb files
	md result7
	%FS% -split 10 Mb 900M.txt -df result7
)

