using GameLibrary.Events;
using GameLibrary.Logs;

namespace GameLibraryTest
{
    public class LogManagerTests
    {
        [Test]
        public void LogMessage_WriteTheCorrectSentence()
        {
            EventManager event_manager = new EventManager();
            LogFakeWriter log_writer = new LogFakeWriter();
            LogManager log_manager = new LogManager(event_manager, log_writer);

            event_manager.TriggerEvent(new LogMessageGameEvent("Hello"));

            Assert.That(log_writer.GetWrittenLineCount(), Is.EqualTo(1));

            Assert.That(log_writer.GetWrittenLine(0), Does.EndWith("Hello"));
        }

        [Test]
        public void LogMessage_DoesNotReorderSentences()
        {
            EventManager event_manager = new EventManager();
            LogFakeWriter log_writer = new LogFakeWriter();
            LogManager log_manager = new LogManager(event_manager, log_writer);

            event_manager.TriggerEvent(new LogMessageGameEvent("First"));
            event_manager.TriggerEvent(new LogMessageGameEvent("Second"));

            Assert.That(log_writer.GetWrittenLine(0), Does.EndWith("First"));

            Assert.That(log_writer.GetWrittenLine(1), Does.EndWith("Second"));
        }
    }
}
