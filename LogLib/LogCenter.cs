using System;
using System.Collections.ObjectModel;

namespace LogCenterNameSpace
{
    public class LogCenter
    {
        public ObservableCollection<LogRecord> records;

        public event  Action<LogRecord>  NewMessage;
        public LogCenter()
        {
            records = new ObservableCollection<LogRecord>();
            records.Add(new LogRecord("Журнал создан"));
        }
        /// <summary>
        ///add message to log
        /// </summary>
        /// <param name="MSg"></param>
        public void AddMessage(string MSg)
        {
            LogRecord r = new LogRecord(MSg);
            records.Add(r);
            NewMessage?.Invoke(r);
        }
    }

    /// <summary>
    /// базовая запись лога
    /// </summary>
    public class LogRecord
    {
        public DateTime Time { get; }
        public string Message { get; }

        public LogRecord() { }

        public LogRecord(string Msg)
        {
            this.Time = DateTime.Now;
            this.Message = Msg;
        }

    }
}
