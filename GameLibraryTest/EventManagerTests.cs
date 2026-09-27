using GameLibrary.Events;

namespace GameLibraryTest
{
    public class EventManagerTests
    {
        //Var
        private bool _wasCalled;

        [Test]
        public void DelayedTriggerEvent_DoesNotRunBeforeProcessEvent()
        {
            EventManager event_manager = new EventManager();
            _wasCalled = false;
            event_manager.RegisterToEvent<LogMessageGameEvent>(OnTestEvent);

            event_manager.DelayedTriggerEvent(new LogMessageGameEvent("Test"));

            Assert.That(_wasCalled, Is.False);
        }

        [Test]
        public void DelayedTriggerEvent_RunsAfterProcessEvent()
        {
            EventManager event_manager = new EventManager();
            _wasCalled = false;
            event_manager.RegisterToEvent<LogMessageGameEvent>(OnTestEvent);
            event_manager.DelayedTriggerEvent(new LogMessageGameEvent("Test"));

            event_manager.ProcessEvent();

            Assert.That(_wasCalled, Is.True);
        }

        private void OnTestEvent(IGameEvent game_event)
        {
            _wasCalled = true;
        }
    }
}
