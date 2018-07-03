using NLog;
using UnityEngine;

namespace NLog.Unity {
    public class DebugLogAppender : MonoBehaviour {

        DefaultLogMessageFormatter _defaultFormatter;
        void OnEnable() {
            LoggerFactory.AddAppender(log);
          //  _defaultFormatter = new DefaultLogMessageFormatter();
        }

        void OnDisable() {
            LoggerFactory.RemoveAppender(log);
        }

        void log(LoggerSet logger, LogLevelSet logLevel, string message) {
            //message = _defaultFormatter.FormatMessage(logger, logLevel, message);
            Debug.Log(message);
            if (logLevel <= LogLevelSet.Info)
            {
                Debug.Log(message);
            }
            else if (logLevel == LogLevelSet.Warn)
            {
                Debug.LogWarning(message);
            }
            else
            {
                Debug.LogError(message);
            }
        }
    }
}
