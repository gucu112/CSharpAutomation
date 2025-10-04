param (
    [string]$ProjectDirectory = "Common/Helpers.Tests"
)

Push-Location
Set-Location (Resolve-Path (Join-Path $PSScriptRoot "..\..")).Path
try {
    Set-Location $ProjectDirectory
    allure generate allure-results --clean -o allure-report; allure open
}
finally {
    Pop-Location
}
