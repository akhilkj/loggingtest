using NLog;
using NLog.Config;
using NLog.Targets;
using UnityEngine;

public class LoggingScript : MonoBehaviour
{
    public static readonly NLog.LoggerSet _log = LoggerFactory.GetLogger(typeof(LoggingScript).Name);
    public UnityEngine.UI.InputField num1;
    public UnityEngine.UI.InputField num2;
    private Calculate obj = new Calculate();
    public NLog.Logger logger = LogManager.GetCurrentClassLogger();


    private void Start()
    {
    }

    public void Divide()
    {
        var fileTarget = new FileTarget("targetFile")
        {
            FileName = "C:/Users/Akhilkj/Documents/CalculatorTest/Logs/unityTest.log",
            Layout = "${longdate}\t${logger}  [${level}]  ${message}  ${exception}"
        };


        //logs given to NLog logger in unity and dlls will be saced in unityTest.log file
        //logs given to _log is send to Papertrail
        //DEbug.log logs to Console and Papertrail 


        int a = int.Parse(num1.text);
        int b = int.Parse(num2.text);
        var config = new LoggingConfiguration();
        config.AddTarget(fileTarget);
        config.AddRule(LogLevel.Debug, LogLevel.Fatal, fileTarget, "*");
        LogManager.Configuration = config;
        int x = obj.Div(a, b);

        //custom nlog made to store into file
        logger.Debug("Testing NLog");

        //Log using nlog object of Logging Script which logs to Papertrail
        _log.Debug("test test" + x);

        //Unity log goes to unity console as well as Papertrail
        Debug.Log("xxxxxxxx");
    }

    private void OnDestroy()
    {
    }
}