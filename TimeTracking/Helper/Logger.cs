using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace TimeTracking.Console.Helper
{
    /// <summary>
    ///     Logs in a log file the given information.
    /// </summary>
    public sealed class Logger : IDisposable
    {
        private static readonly Logger Instance = new(deleteOldFiles: new TimeSpan(5, 0, 0));

        private readonly FolderCleaner _cleaner;

        private readonly string _dateTimeFormat;

        private readonly string[] _enabledLabels;

        private readonly object _lock;

        private readonly OpenStreams _openStreams;
        private readonly bool _useUtcTime;

        private bool _disposed;

        private int _longestLabel;


        /// <summary>
        ///     Constructs the logger using the given configuration.
        /// </summary>
        /// <param name="useUtcTime">True to use UTC time rather than local time</param>
        /// <param name="deleteOldFiles">
        ///     If other than null it sets to delete any file in the log folder that is older than the specified time
        /// </param>
        /// <param name="dateTimeFormat">Format string to use when calling DateTime.Format</param>
        /// <param name="directory">
        ///     Directory where to create the log files, null to use a local "logs" directory
        /// </param>
        /// <param name="enabledLabels">
        ///     Labels enabled to be logged by the library, an attempt to log with a label that is not enabled is ignored
        ///     (no error is raised), null or empty enables all labels
        /// </param>
        public Logger(
            bool useUtcTime = false,
            TimeSpan? deleteOldFiles = null,
            string dateTimeFormat = "yyyy-MM-dd HH:mm:ss",
            string directory = null,
            params string[] enabledLabels
        )
        {
            _useUtcTime = useUtcTime;
            var deleteOldFiles1 = deleteOldFiles;
            _dateTimeFormat = dateTimeFormat;
            var directory1 = directory ?? Path.Combine(AppContext.BaseDirectory, "logs");
            _enabledLabels = (enabledLabels ?? Array.Empty<string>()).Select(Normalize).ToArray();
            _lock = new object();
            _openStreams = new OpenStreams(directory1);

            if (deleteOldFiles1.HasValue)
            {
                var min = TimeSpan.FromSeconds(5);
                var max = TimeSpan.FromHours(8);

                var cleanUpTime = new TimeSpan(deleteOldFiles1.Value.Ticks / 5);

                if (cleanUpTime < min)
                    cleanUpTime = min;

                if (cleanUpTime > max)
                    cleanUpTime = max;

                _cleaner = new FolderCleaner(directory1, _openStreams, deleteOldFiles1.Value, cleanUpTime);
            }

            _longestLabel = _enabledLabels.Any() ? _enabledLabels.Select(l => l.Length).Max() : 15;
            _disposed = false;
        }

        private DateTime Now => _useUtcTime ? DateTime.UtcNow : DateTime.Now;

        /// <summary>
        ///     Disposes the file writer and the directory cleaner used by this instance.
        /// </summary>
        public void Dispose()
        {
            lock (_lock)
            {
                if (_disposed)
                    return;

                _openStreams?.Dispose();

                _cleaner?.Dispose();

                _disposed = true;
            }
        }

        public static Logger GetInstance()
        {
            return Instance;
        }

        /// <summary>
        ///     Logs the given information.
        /// </summary>
        /// <param name="label">Label to use when logging</param>
        /// <param name="content">A string with a message or an object to call ToString() on it</param>
        public void Log(Enum label, object content)
        {
            Log(label.ToString(), content);
        }


        //public void Log(object content) => Log(string.Empty, content);


        /// <summary>
        ///     Formats the given information and logs it.
        /// </summary>
        /// <param name="label">Label to use when logging</param>
        /// <param name="content">A string with a message or an object to call ToString() on it</param>
        public void Log(string label, object content)
        {
            if (label == null)
                throw new ArgumentNullException(nameof(label));

            if (content == null)
                throw new ArgumentNullException(nameof(content));

            //label = Normalize(label);

            if (_enabledLabels.Any() && !_enabledLabels.Contains(label))
                return;

            _longestLabel = Math.Max(_longestLabel, label.Length);

            var date = Now;
            var formattedDate = date.ToString(_dateTimeFormat, CultureInfo.InvariantCulture);
            var padding = new string(' ', _longestLabel - label.Length);

            var output = Regex.Replace($"{content}", @"\s+", " ").Replace(Environment.NewLine, "").Trim();
            var line = $"{formattedDate} {label} {padding}{output}";

            lock (_lock)
            {
                if (_disposed)
                    throw new ObjectDisposedException("Cannot access a disposed object.");

                if (output.Contains("Opened connection"))
                    _openStreams.Append(date, label, new string('-', 110));

                _openStreams.Append(date, label, line);

                if (output.Contains("Closed connection"))
                    _openStreams.Append(date, label, new string('-', 110));
            }
        }

        /// <summary>
        ///     Logs the given information with DEBUG label.
        /// </summary>
        /// <param name="content">A string with a message or an object to call ToString() on it</param>
        public void Debug(object content)
        {
            Log(LogLevel.Debug, content);
        }

        /// <summary>
        ///     Logs the given information with INFO label.
        /// </summary>
        /// <param name="content">A string with a message or an object to call ToString() on it</param>
        public void Info(object content)
        {
            Log(LogLevel.Information, content);
        }

        /// <summary>
        ///     Logs the given information with WARN label.
        /// </summary>
        /// <param name="content">A string with a message or an object to call ToString() on it</param>
        public void Warning(object content)
        {
            Log(LogLevel.Warning, content);
        }

        /// <summary>
        ///     Logs the given information with ERROR label.
        /// </summary>
        /// <param name="content">A string with a message or an object to call ToString() on it</param>
        public void Error(object content)
        {
            Log(LogLevel.Error, content);
        }

        /// <summary>
        ///     Logs the given information with FATAL label.
        /// </summary>
        /// <param name="content">A string with a message or an object to call ToString() on it</param>
        public void Critical(object content)
        {
            Log(LogLevel.Critical, content);
        }

        private static string Normalize(string label)
        {
            return label.Trim().ToUpperInvariant();
        }
    }
}