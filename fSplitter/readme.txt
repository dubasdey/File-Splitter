Small utility to split files into smaller parts.

The utility generates files from ".0" to number of parts -1 with the
specified size

ex:
	for console.log generates

	console.log.0
	console.log.1
	console.log.2
	...

Command line usage:

fSplit -split <size> <unit> <filePath>

		size		Size of parts in "unit"
		unit		unit of size 'b' bytes  'kb' Kilobytes 'mb' Megabytes
		filePath	Path of file to be splitted ex: "C:\console\console.log"
