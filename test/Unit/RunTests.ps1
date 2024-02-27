param ( [string]$outputRoot )
if( "" -eq $outputRoot ) { $output = "$PSScriptRoot" } else { $output = "$outputRoot" }

function DotNet_Test {
  param($testProject)

Write-Host Project "$PSScriptRoot/$testProject" -ForegroundColor Yellow

  $results = "$output/TestResults/" + $testProject.split('.')[0]

Write-Host Results "$results" -ForegroundColor Yellow

  Set-Location "$PSScriptRoot/$testProject"

  if( "$PSScriptRoot" -eq "$output" ) { dotnet clean -c Testing | Out-Null }
  dotnet build -c Testing  --no-restore
  dotnet test -c Testing --collect:"XPlat Code Coverage" --no-build --results-directory "$results"
  Set-Location $PSScriptRoot
}

DotNet_Test 'Core.Tests'
#DotNet_Test 'Helper.Tests'
