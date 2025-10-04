param (
    [string]$ProjectDirectory = "Common/Helpers.Tests"
)

$BasePath = (Resolve-Path (Join-Path $PSScriptRoot "..\..")).Path

Push-Location
Set-Location (Join-Path $BasePath $ProjectDirectory)
try {
    allure generate allure-results --clean -o allure-report; allure open
}
finally {
    Pop-Location
}
