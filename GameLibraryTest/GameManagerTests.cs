using GameLibrary;
using GameLibrary.Events;

namespace GameLibraryTest
{
    public class GameManagerTests
    {
        [Test]
        public void Constructor_NoDestinationSelectedAtStart()
        {
            EventManager event_manager = new EventManager();
            GameManager game_manager = CreateGameManager(event_manager);

            Assert.That(game_manager.GetSelectedDestinationIndex(), Is.EqualTo(-1));
        }

        [Test]
        public void ConfirmSelection_WithNoSelection_DoesNotChangeLocation()
        {
            EventManager event_manager = new EventManager();
            GameManager game_manager = CreateGameManager(event_manager);
            string location_name_before = game_manager.GetCurrentLocationName();

            event_manager.TriggerEvent(new GameActionGameEvent(GameActionType.CONFIRM));

            Assert.That(game_manager.GetCurrentLocationName(), Is.EqualTo(location_name_before));
        }

        [Test]
        public void NavigateDown_WithNoSelection_SelectsFirstDestination()
        {
            EventManager event_manager = new EventManager();
            GameManager game_manager = CreateGameManager(event_manager);

            event_manager.TriggerEvent(new GameActionGameEvent(GameActionType.NAVIGATE_DOWN));

            Assert.That(game_manager.GetSelectedDestinationIndex(), Is.EqualTo(0));
        }

        [Test]
        public void ConfirmSelection_WithSelection_ChangesLocationAndClearsSelection()
        {
            EventManager event_manager = new EventManager();
            GameManager game_manager = CreateGameManager(event_manager);
            string expected_destination_name = game_manager.GetDestinationName(0);

            event_manager.TriggerEvent(new GameActionGameEvent(GameActionType.NAVIGATE_DOWN));
            event_manager.TriggerEvent(new GameActionGameEvent(GameActionType.CONFIRM));

            game_manager.FixedUpdate(100.0f);

            Assert.That(game_manager.GetCurrentLocationName(), Is.EqualTo(expected_destination_name));

            Assert.That(game_manager.GetSelectedDestinationIndex(), Is.EqualTo(-1));
        }

        //Simule un GameManager
        private GameManager CreateGameManager(EventManager event_manager)
        {
            GameManager game_manager = new GameManager(event_manager);

            event_manager.TriggerEvent(new GameActionGameEvent(GameActionType.CONFIRM));
            event_manager.ProcessEvent();

            return game_manager;
        }
    }
}
