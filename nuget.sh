#/bin/sh
find . -type f -name packages.config | while read FILE
do
	mono --runtime=v4.0.30319 .nuget/NuGet.exe install ${FILE} -o packages
done
