# File-Splitter
-----------------------------------------------------------------------------------------

[![Download File Splitter](https://img.shields.io/sourceforge/dm/fsplit.svg)](https://sourceforge.net/projects/fsplit/files/latest/download)
![LGPLv3](https://img.shields.io/badge/Licence-LGPLv3-green.svg)
[![Build Status](https://travis-ci.org/dubasdey/File-Splitter.svg?branch=master)](https://travis-ci.org/dubasdey/File-Splitter)


## Information
-----------------------------------------------------------------------------------------

A Windows and Console based utility to split large files into smaller parts.

Both the Windows and console versions share the same core library of code.
Both versions are independent of each other (ie the Window version does not to gather all the
parameters and then pass them to the batch program).

The split files retain the original extension if not custom format is used.  
The current part number and the total partnumbers are inserted in the format "_XX(YY)", 
where XX is the current part number, and YY is the total number of parts.
This format can be changed by parameter.

example:
	splitting console.log into 3 parts generates

	console_1(3).log
	console_2(3).log
	console_3(3).log


## How To Use	
-----------------------------------------------------------------------------------------

### Split


Command line usage:
```
fsplit -split <size> <unit> <filePath> [-d] [-f <format>] [-df <folder>] [-lf <file>]"
```

Parameters:
	
* -split <size> <unit> <filePath>
	* Splits the File into parts with this options
		* size	
			* Size of parts in "unit"
		* unit	
			* unit used for the desired size. Units:
				* 'b'  bytes  
				* 'kb' Kilobytes 
				* 'mb' Megabytes 
				* 'gb' Gigabytes 
				* 'l' number of lines
		* filePath	
			* Path of file to be split ex: "C:\console\console.log"
	* -d 
		* Delete the original file after the split is done correctly.
	* -f <format>
		* Uses a custom format for the file names. Using a custom format is required to add an extension
		The text {0} is replaced with the current part number.  
		The text {1} is replaced with the total number of parts expected.  
		The format inputs accepts parameters to adapt it to the desired funcionality.  
		For you could specify a padding of **n** positions adding **,n**.  
		Example of 5 chars padding **{0,5}** with right align or **{0,-5}** to left align.    
		Also you could specify a numeric filling usin # and 0 after :  
		Examples for the file number 15:    
			Filling with five 0: **{0:00000}** this result in **'00015'**.  
			Filling with two and two 0 with - : **{0:00-00}** this result in **'00-15'**.  
			Filling with three 0 in six positions aligned left **{0,6:000}** this result in **'015   '**  
	* -df <folder>
		* Changes the result folder from current folder to the desired folder. The folder is created if not exists.
	* -lf <file>
		* Creates a file with the names of all the generated files 


Example: 

	fSplit -split 10230 kb c:\console\console.log   

WARNING: Cutting by file size currently cuts lines in half. To keep lines together use the option to split by line numbers, eg:

	fSplit -split 100000 l c:\console\console.log   


* Note 1: you can't use commas as delimiters
* Note 2: if you call the program from the console without parameters, it invokes the Windows version.

### Join

It's possible to merge again the files using the command line with the "copy" command  
For example if you have splitted a text file into two parts "p1.txt" and "p2.txt" with "copy /A p1.txt+p2.txt all.txt" it's possible to merge p1 and p2 in a new file called all.txt  
If the files are ASCII use /A flag and if the files are binary use the /B flag to ensure that the content is correctly joined.  
Take careful to put all files in the correct order.  

## Donate
-----------------------------------------------------------------------------------------
Buy me a coffe to help me continue supporting this project. 
<a href="https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=7J42FBHMT9VT4">Buy me a coffe</a>


