# Unit Tests
A _unit test_ is a test that exercises individual software components or methods, also known as a "unit of work." Unit tests should only test code within the developer's control. They do not test infrastructure concerns such as interacting with databases, file systems, and network resources.

> Unit tests help to ensure functionality and provide a means of verification for refactoring efforts.

Dependencies:
- [FluentAssertions](https://www.nuget.org/packages/FluentAssertions)
- [Xunit.SkippableFact](https://www.nuget.org/packages/Xunit.SkippableFact)

References:
- [Unit testing best practices](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
- [About xUnit.net](https://xunit.net/)
- [Fluent assertions](https://fluentassertions.com/tips/)


## Generating a Code Coverage report

Code coverage is a measurement of the amount of code that is run by unit tests - either lines, branches, or methods.

- [Use code coverage for unit testing](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage)
- [Code coverage report generator](https://reportgenerator.io/getstarted)
- [Code Coverage Metrics](https://dunnhq.com/posts/2023/code-coverage-metrics/)

> _The code coverage feature in Visual Studio is only available with the Enterprise edition._

Install or update the ReportGenerator package as a global .NET tool:
```shell
dotnet tool install --global dotnet-reportgenerator-globaltool --version 5.2.0
dotnet tool update --global dotnet-reportgenerator-globaltool
reportgenerator -help
```
\
Run the tests with no build and generate the HTML coverage report:
```shell
cd [project folder]
dotnet test --collect:"XPlat Code Coverage" --no-build
reportgenerator -reports:TestResults\*\coverage.cobertura.xml -targetdir:TestResults\reports\html

# Optional reports
reportgenerator -reports:TestResults\*\coverage.cobertura.xml -targetdir:TestResults\reports -reporttypes:MarkdownSummaryGithub
reportgenerator -reports:TestResults\*\coverage.cobertura.xml -targetdir:TestResults\reports\badges -reporttypes:Badges
```
\
Run the tests with build and generate the HTML coverage report with history:\
_(__Important:__ These create a new *CoverageHistory.xml file)_
```shell
cd [project folder]
dotnet test --collect:"XPlat Code Coverage" --configuration "Testing"
reportgenerator -reports:TestResults\*\coverage.cobertura.xml -targetdir:TestResults\reports\html -historydir:Testdata\history

# Optional reports
reportgenerator -reports:TestResults\*\coverage.cobertura.xml -targetdir:TestResults\reports -reporttypes:SvgChart -historydir:Testdata\history
```
\
Run the tests and generate the coverage report with increased cyclomatic complexity threshold _(the default is 30)_:
```shell
cd [project folder]
dotnet test --collect:"XPlat Code Coverage" --settings unittest.runsettings --no-build
reportgenerator -reports:TestResults\*\coverage.cobertura.xml -targetdir:TestResults\reports\html --riskHotspotsAnalysisThresholds:metricThresholdForCyclomaticComplexity=36
```

