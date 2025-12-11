using Godot;
using static Godot.GD;

public enum LogLevel
{
    TRACE,
    DEBUG,
    INFO,
    WARN,
    ERROR
}

public class Logger
{
    private static readonly char[] DELIMITERS = { '[', ']' };
    private readonly string klass;
    private readonly string name;
    private readonly string type;

    public Logger()
    {
        name = "";
        type = "";
        klass = "";
    }

    public Logger(string name)
    {
        this.name = name;
        type = "";
        klass = "";
    }

    public Logger(Node loggee)
    {
        name = loggee.Name;
        type = loggee.GetClass();
        klass = loggee.GetType().Name;
    }

    private string GetLogLevel(LogLevel level)
    {
        switch (level)
        {
            case LogLevel.TRACE:
                return "TRACE";
            case LogLevel.DEBUG:
                return "DEBUG";
            case LogLevel.INFO:
                return "INFO";
            case LogLevel.WARN:
                return "WARN";
            case LogLevel.ERROR:
                return "ERROR";
            default:
                return "UNKNOWN";
        }
    }

    private string GetHeader(LogLevel level)
    {
        var levelName = GetLogLevel(level);
        return $"{DELIMITERS[0]}{levelName}{DELIMITERS[1]} - {DELIMITERS[0]}{name}@{klass}: {type}{DELIMITERS[1]}";
    }

    private void log(LogLevel level, string message)
    {
        Print($"{GetHeader(level)} {message}");
    }

    public void trace(string message)
    {
        log(LogLevel.TRACE, message);
    }

    public void debug(string message)
    {
        log(LogLevel.DEBUG, message);
    }

    public void info(string message)
    {
        log(LogLevel.INFO, message);
    }

    public void warning(string message)
    {
        log(LogLevel.WARN, message);
    }

    public void error(string message)
    {
        log(LogLevel.ERROR, message);
    }
}