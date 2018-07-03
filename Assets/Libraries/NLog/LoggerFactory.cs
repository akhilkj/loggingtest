using System.Collections.Generic;

namespace NLog {
    public static class LoggerFactory {
        public static LogLevelSet globalLogLevel {
            get { return _globalLogLevel; }
            set {
                _globalLogLevel = value;
                foreach (var logger in _loggers.Values) {
                    logger.logLevel = value;
                }
            }
        }

        static LogLevelSet _globalLogLevel;
        static LoggerSet.LogDelegate _appenders;
        readonly static Dictionary<string, LoggerSet> _loggers = new Dictionary<string, LoggerSet>();

        public static void AddAppender(LoggerSet.LogDelegate appender) {
            _appenders += appender;
            foreach (var logger in _loggers.Values) {
                logger.OnLog += appender;
            }
        }

        public static void RemoveAppender(LoggerSet.LogDelegate appender) {
            _appenders -= appender;
            foreach (var logger in _loggers.Values) {
                logger.OnLog -= appender;
            }
        }

        public static LoggerSet GetLogger(string name) {
            LoggerSet logger;
            if (!_loggers.TryGetValue(name, out logger)) {
                logger = createLogger(name);
                _loggers.Add(name, logger);
            }

            return logger;
        }

        public static void Reset() {
            _loggers.Clear();
            _appenders = null;
        }

        static LoggerSet createLogger(string name) {
            var logger = new LoggerSet(name);
            logger.logLevel = globalLogLevel;
            logger.OnLog += _appenders;
            return logger;
        }
    }
}

