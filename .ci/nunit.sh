#!/bin/sh -x

mono --runtime=v4.0 .nuget/NuGet.exe install NUnit.Runners -Version 2.6.1 -o packages

for arg in $@; do
	find . -name ${arg} | while read file; do
	    mono --runtime=v4.0 packages/NUnit.Runners.2.6.1/tools/nunit-console.exe -noxml -nodots -labels -stoponerror "${file}"
	    if [ $? -ne 0 ]; then
	        exit 1
	    fi
	done
done

exit $?
