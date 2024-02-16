# Unit Testing
A _unit test_ helps write simpler, readable code and provides a means of verification for refactoring efforts. Test Driven Development _(TDD)_ is when you write a _unit test_ before the code it's meant to check. 
- [Test Driven Development](https://deviq.com/practices/test-driven-development)
- [About xUnit.net](https://xunit.net/)
- [Fluent assertions](https://fluentassertions.com/tips/)

Dependencies:\
&nbsp;[FluentAssertions](https://www.nuget.org/packages/FluentAssertions)\
&nbsp;[Xunit.SkippableFact](https://www.nuget.org/packages/Xunit.SkippableFact)

## Generating a Code Coverage report
Code coverage is a measurement of the amount of code that is run by unit tests - either lines, branches, or methods.

- [Use code coverage for unit testing](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage)
- [Code coverage report generator](https://reportgenerator.io/getstarted)
- [Code Coverage Metrics](https://dunnhq.com/posts/2023/code-coverage-metrics/)

> _The code coverage feature in Visual Studio is only available with the Enterprise edition._

Dependencies:\
&nbsp;[ReportGenerator](https://www.nuget.org/packages/ReportGenerator)
- [ReportGenerator Settings](https://github.com/danielpalme/ReportGenerator/wiki/Settings)
- [ReportGenerator Usage](https://reportgenerator.io/usage)

To install or update the ReportGenerator package as a global .NET tool use  the following `powershell` commands:
```shell
dotnet tool install --global dotnet-reportgenerator-globaltool

dotnet tool update --global dotnet-reportgenerator-globaltool
reportgenerator -help
```
&nbsp;
> Run the following commands from the Unit Test project directory.

To run the tests with no build and generate a HTML coverage report:
```shell
dotnet test --collect:"XPlat Code Coverage" --no-build
reportgenerator -reports:TestResults\*\coverage.cobertura.xml -targetdir:TestResults\reports\html
```
\
See [Report Generator Code Coverage - Build process](https://github.com/KevinDHeath/KevinDHeath.github.io/blob/main/README.md) for instructions on generating the coverage reports with **history**.\
\
To generate a coverage report with increased crap score threshold:\
_(v5.2.1: the default is 15)_
```shell
reportgenerator -reports:TestResults\*\coverage.cobertura.xml -targetdir:TestResults\reports\html --riskHotspotsAnalysisThresholds:metricThresholdForCrapScore=26
```
\
To generate a coverage report with increased cyclomatic complexity threshold:\
_(v5.2.0: the default is 30)_
```shell
reportgenerator -reports:TestResults\*\coverage.cobertura.xml -targetdir:TestResults\reports\html --riskHotspotsAnalysisThresholds:metricThresholdForCyclomaticComplexity=36
```