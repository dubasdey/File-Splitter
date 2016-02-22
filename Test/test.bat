md result
echo TEST1
REM copy test_256.txt toDelete_256.txt
REM ..\bin\Debug\fSplit.exe -split 1 kb toDelete_256.txt -d -f sp_{0:0000}_of_{1:0000}.txt -df result -lf files.txt

echo TEST2
copy test_28.txt toDelete28.txt 
..\bin\Debug\fSplit.exe -split 4 kb toDelete28.txt -df result
