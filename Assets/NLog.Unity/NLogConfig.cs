using NLog;
using UnityEngine;

namespace NLog.Unity {

    [DisallowMultipleComponent]
    public class NLogConfig : MonoBehaviour {
        public LogLevelSet logLevel;
        public bool catchUnityLogs = true;
        LoggerSet _unityLog;

        void Awake() {
            DontDestroyOnLoad(gameObject);
            LoggerFactory.globalLogLevel = LogLevelSet.Off;
        }

        void OnEnable() {
            LoggerFactory.globalLogLevel = logLevel;
        }

        void OnDisable() {
            LoggerFactory.globalLogLevel = LogLevelSet.Off;
        }

        void Start() {
            if (catchUnityLogs) {
                _unityLog = LoggerFactory.GetLogger("Unity");
                Application.logMessageReceived += onLog;
              //  Application.logMessageReceivedThreaded += onLog;
            }
        }

        void OnDestroy() {
            Application.logMessageReceived -= onLog;
          //  Application.logMessageReceivedThreaded -= onLog;
        }

        void onLog(string condition, string stackTrace, LogType type) {
            if (type == LogType.Log) {
                _unityLog.Debug(condition);
            } else if (type == LogType.Warning) {
                _unityLog.Warn(condition);
            } else {
                _unityLog.Error(condition + "\n" + stackTrace);
            }
        }
    }
}