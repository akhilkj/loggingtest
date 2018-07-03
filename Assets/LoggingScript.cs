using System;
using NLog;
using NLog.Config;
using NLog.Targets;
using UnityEngine;

public class LoggingScript : MonoBehaviour {
    public static readonly NLog.LoggerSet _log = LoggerFactory.GetLogger(typeof(LoggingScript).Name);
    public UnityEngine.UI.InputField num1;
    public UnityEngine.UI.InputField num2;
    Calculate obj = new Calculate();
    private void Start()
    {
        obj.Logged += LogEntry;
    }
    public void Divide()
    {
       
        int a = int.Parse(num1.text);
        int b = int.Parse(num2.text);
        int x = obj.Div(a, b);
        var config = new LoggingConfiguration();
        var fileTarget = new FileTarget("targetFile")
        {
            FileName = "C:/Users/Akhilkj/Documents/CalculatorTest/Logs/unityTest.log",
            Layout = "${longdate} ${level} ${message}  ${exception}"
        };
        config.AddTarget(fileTarget);
        config.AddRule(LogLevel.Debug, LogLevel.Fatal, fileTarget, "*");
        LogManager.Configuration = config;
        NLog.Logger logger = LogManager.GetCurrentClassLogger();
        //custom nlog made to store into file
        logger.Debug("Testing NLog");

        //Log using nlog object of Logging Script which logs to Papertrail
        _log.Debug("test test" + x);

        //Unity log goes to unity console as well as Papertrail
        Debug.Log("xxxxxxxx");
        
    }

    private void LogEntry(string foo)
    {
        var config = new LoggingConfiguration();
       var fileTarget = new FileTarget("targetFile")
        {
            FileName = "C:/Users/Akhilkj/Documents/CalculatorTest/Logs/dllLog.log",
            Layout = "${longdate} ${level} ${message}  ${exception}"
        };
        config.AddTarget(fileTarget);
        config.AddRule(LogLevel.Trace, LogLevel.Fatal, fileTarget, "*");
        LogManager.Configuration = config;
        NLog.Logger dllLog = LogManager.GetCurrentClassLogger();
        //Log goes to custom file
        dllLog.Debug(foo);
        //Logs to PaperTrail
        _log.Debug(foo);
        //Log goes to Unity console as well as Papertrail
        Debug.Log("unitydlllog: " + foo);
    }
    private void OnDestroy()
    {
        obj.Logged -= LogEntry;
    }
}
