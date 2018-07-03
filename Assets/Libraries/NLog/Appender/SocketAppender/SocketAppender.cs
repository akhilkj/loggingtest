using System.Text;

namespace NLog {
    public class SocketAppender : SocketAppenderBase {
        protected override byte[] serializeMessage(LogLevelSet logLevel, string message) {
            return Encoding.UTF8.GetBytes(message + "\n");
        }
    }
}