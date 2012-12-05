set MyDir=%~p0
for /D %%d in (*) do (
    if exist %%d\packages.config (
        "%MyDir%.nuget\NuGet.exe" install "%%d\packages.config" -o "%MyDir%packages"
    )
)