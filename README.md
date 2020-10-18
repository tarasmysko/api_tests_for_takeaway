# Takeaway Test task

This test project is a test task for Takeaway created by Taras Mysko. 
It is build with use of .net Core 3.1 (it may run on linux and mac, but I havent tried), Nunit, Restsharp and Fluent Assertions. 
Reporting is done via Nunit standard reporting -> output XML report file to the TestResults directory e.g. Test_task_takeaway\Test Task\TestResults\results.xml
Execution time is logged to the report as writeline text. 
 

## Requirements

- Windows 10 PC with the latest Windows 10 version (it's .net core app so it could run on Linux or Mac, but i havent tried)
- dotnet core 3.1 to run dotnet CLI

## How to use
1. To run all tests from project file -> dotnet test "Test Task.csproj" --logger trx;LogFileName=results.xml --verbosity q
2. To run all tests from built dll file -> dotnet test "Test Task.dll" --logger trx;LogFileName=results.xml --verbosity q
3. To run tests for specific Endpoint (Posts, Albums, Users) -> dotnet test "Test Task.dll" --logger trx;LogFileName=results.xml --verbosity q --filter TestCategory=Posts
4. Response time is output to consile and saved to Test_task_takeaway\Test Task\TestResults\results.xml
5. Tests are run in paralel so there should not be any issues trigger them on CI for Load testing purpose. Unfortunatelly Nunit does not allow repeat tests so i had to use for loop.
6. For load testing in appsetting.json change "Repeat": 1 to any number and all tests will execute defined number of times and log execution time to results file.
7.If you want to know more about running dotnet tests -> https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-test

## Issues
1. Some exceptions are not handled as they should be. 
2. Lack of negative testing due to limitation with Post request.

## Discussion
1. I could use OneTimeSetup to execute request but i decided to run a new request for every test. This way every test feels more idempoten but i have doubts.
2. Lack of negative testing for Post Request. Was it a strict requirement? I feel terrible because there is ponly one test for Post ;( 
