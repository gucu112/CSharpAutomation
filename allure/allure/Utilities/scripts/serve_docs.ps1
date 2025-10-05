param ()

Push-Location
Set-Location (Resolve-Path (Join-Path $PSScriptRoot "..\..")).Path
try {
    docfx docfx.json --serve
}
finally {
    Pop-Location
}
