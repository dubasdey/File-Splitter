md result
copy test_256.txt toDelete.txt
..\bin\Debug\fSplit.exe -split 1 kb toDelete.txt -d -f sp_{0:0000}_of_{1:0000}.txt -df result -lf files.txt
