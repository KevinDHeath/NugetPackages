param ( [string]$outputRoot )
if( "" -eq $outputRoot ) { $output = "$PSScriptRoot" } else { $output = "$outputRoot" }

function DotNet_Test {
  param($testProject)

  $results = "$output\TestResults\" + $testProject.split('.')[0].ToLower()

#Write-Host Project "$PSScriptRoot\$testProject" -ForegroundColor Yellow
#Write-Host Results "$results" -ForegroundColor Yellow

  Set-Location "$PSScriptRoot\$testProject"
  if( "$PSScriptRoot" -eq "$output" ) { dotnet clean -c Testing | Out-Null }
  dotnet build -c Testing  --no-restore
  dotnet test -c Testing --collect:"XPlat Code Coverage" --no-build --results-directory "$results"
  Set-Location $PSScriptRoot

  if( "$PSScriptRoot" -eq "$output" ) {
    Write-Host Generating reports... -ForegroundColor Yellow
    $reports = "$results\*\coverage.cobertura.xml"
    reportgenerator -reports:"$reports" -targetdir:"$results\reports\html" -verbosity:Warning
    reportgenerator -reports:"$reports" -targetdir:"$results\reports" -reporttypes:MarkdownSummaryGithub -verbosity:Warning
    reportgenerator -reports:"$reports" -targetdir:"$results\reports\badges" -reporttypes:Badges -verbosity:Warning
    Remove-Item "$results\reports\badges\badge_shieldsio_*.svg" | Out-Null
  }
}

DotNet_Test 'Core.Tests'
#DotNet_Test 'Helper.Tests'
