using System;

namespace NLog {
    public class LoggerSet {
        public event LogDelegate OnLog;

        public delegate void LogDelegate(LoggerSet logger, LogLevelSet logLevel, string message);

        public LogLevelSet logLevel { get; set; }

        public string name { get; private set; }

        public LoggerSet(string name) {
            this.name = name;
        }

        public void Trace(string message) {
            log(LogLevelSet.Trace, message);
        }

        public void Debug(string message) {
            log(LogLevelSet.Debug, message);
        }

        public void Info(string message) {
            log(LogLevelSet.Info, message);
        }

        public void Warn(string message) {
            log(LogLevelSet.Warn, message);
        }

        public void Error(string message) {
            log(LogLevelSet.Error, message);
        }

        public void Fatal(string message) {
            log(LogLevelSet.Fatal, message);
        }

        public void Assert(bool condition, string message) {
            if (!condition) {
                throw new NLogAssertException(message);
            }
        }

        void log(LogLevelSet logLvl, string message) {
            if (OnLog != null && logLevel <= logLvl) {
                OnLog(this, logLvl, message);
            }
        }
    }

    public class NLogAssertException : Exception {
        public NLogAssertException(string message) : base(message) {
        }
    }
}

