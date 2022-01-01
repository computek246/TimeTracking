using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace TimeTracking.Helper
{
    internal sealed class OpenStreams : IDisposable
    {
        private readonly string _directory;

        private readonly object _lock;

        private readonly Dictionary<DateTime, StreamWriter> _streams;

        private readonly Timer _timer;

        internal OpenStreams(string directory)
        {
            _directory = directory;
            _streams = new Dictionary<DateTime, StreamWriter>();
            _lock = new object();
            _timer = new Timer(ClosePastStreams, null, 0, (int)TimeSpan.FromHours(2).TotalMilliseconds);
        }

        public void Dispose()
        {
            _timer.Dispose();
            CloseAllStreams();
        }

        internal void Append(DateTime date, string label, string content)
        {
            lock (_lock)
            {
                GetStream(date.Date, label).WriteLine(content);
            }
        }

        internal string[] Filepaths()
        {
            return Directory.EnumerateFiles(_directory, "*.log").ToArray();
        }

        private void ClosePastStreams(object ignored)
        {
            lock (_lock)
            {
                var today = DateTime.Today;
                var past = _streams.Where(kvp => kvp.Key < today).ToList();

                foreach (var kvp in past)
                {
                    kvp.Value.Dispose();
                    _streams.Remove(kvp.Key);
                }
            }
        }

        private void CloseAllStreams()
        {
            lock (_lock)
            {
                foreach (var stream in _streams.Values)
                    stream.Dispose();

                _streams.Clear();
            }
        }


        private StreamWriter GetStream(DateTime date, string label)
        {
            // Opening the stream if needed
            if (!_streams.ContainsKey(date))
            {
                // Building stream's filepath
                var filename =
                    $"{(Assembly.GetExecutingAssembly().GetName().Name ?? "").Split(',').First()}.{label}.{date:yy.MM.dd.hh}.log";
                var filepath = Path.Combine(_directory, filename);

                // Making sure the directory exists
                Directory.CreateDirectory(_directory);

                // Opening the stream
                var stream = new StreamWriter(
                    File.Open(filepath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite)
                );
                stream.AutoFlush = true;

                // Storing the created stream
                _streams[date] = stream;
            }

            return _streams[date];
        }
    }
}