Information
-----------------------------------------------------------------------------------------

A Windows and Console based utility to split large files into smaller parts.

Both the Windows and console versions share the same core library of code.
Both versions are independent of each other (ie the Window version does not to gather all the
parameters and then pass them to the batch program).

The split files retain the original extension.  The current partnumber
and the total partnumbers are inserted in the format "_XX(YY)", 
where XX is the current part number, and YY is the total number of parts.

example:
	splitting console.log into 3 parts generates

	console_1(3).log
	console_2(3).log
	console_3(3).log


How To Use	
-----------------------------------------------------------------------------------------

Command line usage:

fSplit -split <size> <unit> <filePath>

		size		Size of parts in "unit"
		unit		unit of size 'b' bytes  'kb' Kilobytes 'mb' Megabytes 'gb' Gigabytes
		filePath	Path of file to be split ex: "C:\console\console.log"

Example: fSplit -split 10230 kb c:\console\console.log   
Example: fSplit -split 512 mb "c:\console\a file name with spaces.log"	(note: quotes are only needed if the filepath contains spaces)

Note 1: you can't use commas as delimiters
Note 2: if you call the program from the console without parameters, it invokes the Windows version.




Thanks
-----------------------------------------------------------------------------------------
Thanks to James From Canberra, Australia. To help on some bugs, corrections, providing new features and filling this readme file and examples.