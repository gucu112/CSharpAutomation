param (
    [string]$ProjectDirectory = "Common/Helpers.Tests"
)

Push-Location
Set-Location (Resolve-Path (Join-Path $PSScriptRoot "..\..")).Path
try {
    $cmdOutput = @(dotnet test "$ProjectDirectory" --collect:"XPlat Code Coverage")
    $info = $cmdOutput | Select-Object -Last 4
    Write-Host $info[0]
    Set-Location $ProjectDirectory
    Write-Host $info[3]
    reportgenerator -reports:"TestResults/*/coverage.cobertura.xml" `
        -targetdir:"coverage-report" `
        -reporttypes:"Html_Dark" `
        -filefilters:-*.g.cs
    Invoke-Item "coverage-report/index.html"
}
finally {
    Pop-Location
}
