using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.IO;
using System.Reflection;

namespace Test_Task.Common
{
    public class ReporterHelper
    {
        public AventStack.ExtentReports.ExtentReports extent { get; set; }
        public ExtentV3HtmlReporter reporter { get; set; }
        public ExtentTest test { get; set; }

        public ReporterHelper()
        {
            extent = new AventStack.ExtentReports.ExtentReports();
            reporter = new ExtentV3HtmlReporter(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ExtentReports.html"));
            reporter.Config.DocumentTitle = "This suppose to be fun report";
            reporter.Config.ReportName = "Test Task Testing (TTT)";
            reporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            extent.AttachReporter(reporter);
            extent.AddSystemInfo("Application Under Test", "{JSON} Placeholder");
            extent.AddSystemInfo("Environment", "https://jsonplaceholder.typicode.com/");
            extent.AddSystemInfo("Machine", Environment.MachineName);
            extent.AddSystemInfo("OS", Environment.OSVersion.VersionString);
        }

        public void CreateTest(string testName)
        {
            test = extent.CreateTest(testName);
        }

        public void SetStepStatusPass(string stepDescription)
        {
            test.Log(Status.Pass, stepDescription);
        }

        public void SetStepStatusWarning(string stepDescription)
        {
            test.Log(Status.Warning, stepDescription);
        }

        public void SetTestStatusPass()
        {
            test.Pass("Test Executed Sucessfully!");
        }

        public void SetTestStatusFail(string message = null)
        {
            var printMessage = "<p><b>Test FAILED!</b></p>";
            if (!string.IsNullOrEmpty(message))
            {
                printMessage += $"Message: <br>{message}<br>";
            }
            test.Fail(printMessage);
        }

        public void SetTestStatusSkipped()
        {
            test.Skip("Test skipped!");
        }

        public void Close()
        {
            extent.Flush();
        }

        //var htmlReporter = new ExtentHtmlReporter(@"/");
        //htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
        //extent = new AventStack.ExtentReports.ExtentReports();
        //klov = new ExtentKlovReporter();
        //extent.AttachReporter(htmlReporter);
    }
}
