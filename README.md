# Takeaway Test task

This test project is a test task for Takeaway created by Taras Mysko. 
It is build with use of .net Core 3.1 (it may run on linux and mac, but I havent tried), Nunit, Restsharp and Fluent Assertions. 
Reporting is done in 2 ways:
1. Nunit standard reporting, I like it more -> output XML report file to the TestResults directory e.g. Test_task_takeaway\Test Task\TestResults\results.xml
2. Extent Report -> experiemntal html report, output to build directory as ExtentReports.html. e.g. Test_task_takeaway\Test Task\bin\Debug\netcoreapp3.1\ExtentReports.html

## Requirements

- Windows 10 PC with the latest Windows 10 version
- dotnet core 3.1 to run dotnet CLI

## How to use
1. To run all tests -> dotnet test "Test Task.csproj" --logger trx;LogFileName=results.xml --verbosity q
2. To run tests for specific Endpoint (Posts, Albums, Users) -> dotnet test "Test Task.csproj" --logger trx;LogFileName=results.xml --verbosity q --filter TestCategory=Posts
3. Response time is output to consile and saved to Test_task_takeaway\Test Task\TestResults\results.xml
4. Tests are run in paralel so there should not be any issues trigger them on CI for Load testing purpose. Unfortunatelly Nunit does not allow repeat tests.

## Issues
1. Some exceptions are not handled as they should be. 
2. Lack of negative testing due to limitation with Post request. 
