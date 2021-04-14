$databaseZips =  'databases.zip'
if (-not(Test-Path -Path $databaseZips -PathType Leaf)){
    & .\googleDriveDownload.ps1 -GoogleFileId 1b3mYWIn2RU0phyjjkRBe68x7dFq3exn2 -FileDestination $databaseZips
}

Expand-Archive -Path $databaseZips -DestinationPath ./data/sql/
Remove-Item $databaseZips