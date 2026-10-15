using GameLibrary;
using GameLibrary.Components;

namespace GameLibraryTest
{
    public class LocationComponentTests
    {
        [Test]
        //A à B
        public void LinkTo_AddsDestinationToSourceLocation()
        {
            string location_name = null;
            GameObject location_game_object = new GameObject(location_name);
            LocationComponent location_a = new LocationComponent("A", location_game_object);
            LocationComponent location_b = new LocationComponent("B", location_game_object);

            location_a.LinkTo(location_b, 5.0f);

            Assert.That(location_a.GetLocationTableCount(), Is.EqualTo(1));

            Assert.That(location_a.GetLiaisonDestination(0), Is.EqualTo(location_b));

            Assert.That(location_a.GetLiaisonDuration(0), Is.EqualTo(5.0f).Within(0.001f));
        }

        [Test]
        //B à A
        public void LinkTo_AddsSourceLocationToDestination()
        {
            string location_name = null;
            GameObject location_game_object = new GameObject(location_name);
            LocationComponent location_a = new LocationComponent("A", location_game_object);
            LocationComponent location_b = new LocationComponent("B", location_game_object);

            location_a.LinkTo(location_b, 5.0f);

            Assert.That(location_b.GetLocationTableCount(), Is.EqualTo(1));

            Assert.That(location_b.GetLiaisonDestination(0), Is.EqualTo(location_a));

            Assert.That(location_b.GetLiaisonDuration(0), Is.EqualTo(5.0f).Within(0.001f));
        }
    }
}
