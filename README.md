# C# Automation

> This is repository which contains various projects that show how you can approach building your own framework using some set of technologies and tools.

## Table of Contents
* [Getting started](#getting-started)
  * [Prerequisites](#prerequisites)
  * [Building](#building)
  * [Running](#running)
* [Deployment](#deployment)
* [Built with](#build-with)
  * [PlaywrightXunit](#playwrightxunit)
  * [PlaywrightXunitParallel](#playwrightxunitparallel)
  * [PlaywrightReqnrollMsTest](#playwrightreqnrollmstest)
  * [SeleniumReqnrollNunit](#seleniumreqnrollnunit)
  * [Common/Helpers](#common-helpers)
  * [Common/Helpers.Tests](#common-helpers-tests)
* [Contributing](#contributing)
* [Versioning](#versioning)
* [Authors](#authors)
* [License](#license)

## Getting started

Simply open the solution file (`CSharpAutomation.sln`) from main project directory. Any commands should be run towards chosen project, so remember to `cd` into chosen project directory.

### Prerequisites

#### Playwright

Run this command from selected project directory (e.g. `PlaywrightXunitParallel`), replacing `{{Configuration}}` with current configuration (e.g. `ChromiumDebug`).

```
pwsh bin/{{Configuration}}/net8.0/playwright.ps1 install
```

If `pwsh` is not available, you have to [install PowerShell](https://docs.microsoft.com/powershell/scripting/install/installing-powershell).

### Building

Use Visual Studio or execute following command to build the tests:

```
dotnet build
```

### Running

Use Visual Studio Test Explorer or execute following command to run the tests:

```
dotnet test
```

## Deployment

There are no deployment procedure established yet.

## Build with

### PlaywrightXunit

* [Playwright](https://playwright.dev/dotnet/)
* [Xunit](https://xunit.net/)
* [Xunit.SkippableFact](https://github.com/AArnott/Xunit.SkippableFact)

### PlaywrightXunitParallel

* [Playwright](https://playwright.dev/dotnet/)
* [Xunit](https://xunit.net/)
* [Xunit.SkippableFact](https://github.com/AArnott/Xunit.SkippableFact)
* [FluentAssertions](https://fluentassertions.com/)

### PlaywrightReqnrollMSTest

### SeleniumReqnrollNunit

### Common Helpers

* [Json.NET](https://www.newtonsoft.com/json)

#### Common Helpers Tests

* [NUnit](https://nunit.org/)
* [Moq](https://github.com/devlooped/moq)

## Contributing

There are no contribution rules established yet.

## Versioning

Project versioning pattern is defined as follows:

```
v{yy}.{M}.{d}.{r}
```

**Legend:**

- `{yy}` - 2-digits year, range from 00 to 99
- `{M}` - month, range from 1 to 12
- `{d}` - day of the month, range from 1 to 31
- `{r}` - revison number, incrementing for each daily version, starting from 0

## Authors

- **Bartlomiej Roszczypala** - [Gucu112](https://github.com/gucu112)

See also the list of [contributors](https://github.com/gucu112/CSharpAutomation/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE.txt) file for details.
