param ( [string]$outputRoot )
if( "" -eq $outputRoot ) { $output = "$PSScriptRoot" } else { $output = "$outputRoot" }

function DotNet_Test {
  param($testProject)
  $results = "$output\TestResults\" + $testProject.split('.')[0].ToLower()
  $history = "$PSScriptRoot\$testProject\Testdata\history"

  # Remove existing results
  if( Test-Path "$results" ) { Remove-Item -Path "$results\*" -Recurse | Out-Null }

  # Build project and run unit tests
  Set-Location "$PSScriptRoot\$testProject"
  if( "$PSScriptRoot" -eq "$output" ) { dotnet clean -c Testing | Out-Null }
  dotnet build -c Testing  --no-restore
  dotnet test -c Testing --collect:"XPlat Code Coverage" --no-build --results-directory "$results"
  Set-Location $PSScriptRoot

  # If running locally generate reports
  if( "$PSScriptRoot" -eq "$output" ) {
    $reports = "$results\*\coverage.cobertura.xml"
    $rpTypes = "-reporttypes:Html;Badges;MarkdownSummaryGithub"
    $setting = "--settings:createSubdirectoryForAllReportTypes=true"
    $title = "-title:$testProject"

    Write-Host "Generating coverage reports for $testProject" -ForegroundColor Yellow
    reportgenerator -reports:"$reports" -targetdir:"$results\reports" $title -verbosity:Warning $rpTypes $setting

     # Tidy up output
    $wrk = "$results\reports\Html"
    if( Test-Path "$wrk" ) { Rename-Item -path "$wrk" -NewName "html" }

    $wrk = "$results\reports\Badges" # Move required badges
    if( Test-Path "$wrk" ) {
      Remove-Item "$wrk\badge_shieldsio_*.svg" | Out-Null
      Move-Item -Path "$wrk\*.svg" -Destination "$results\reports"
      Remove-Item "$wrk"
    }

    $wrk = "$results\reports\MarkdownSummaryGithub" # Move required markdown
    if( Test-Path "$wrk" ) {
      Move-Item -Path "$wrk\*.md" -Destination "$results\reports"
      Remove-Item "$wrk"
    }
  }
}

# Perform tasks  
DotNet_Test 'Core.Tests'
DotNet_Test 'Helper.Tests'
