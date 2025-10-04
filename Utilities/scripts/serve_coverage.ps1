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
    reportgenerator -reports:"$($info[3])" -targetdir:"coverage-report" -reporttypes:"Html_Dark"
    Invoke-Item "coverage-report/index.html"
}
finally {
    Pop-Location
}
