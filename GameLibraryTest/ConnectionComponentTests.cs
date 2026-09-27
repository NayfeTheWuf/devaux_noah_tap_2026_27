using GameLibrary;
using GameLibrary.Components;

namespace GameLibraryTest
{
    public class ConnectionComponentTests
    {
        [Test]
        public void GetDestination_ReturnsConstructionDestination()
        {
            LocationComponent destination = CreateLocation("Daisy Town");
            ConnectionComponent connection = new ConnectionComponent(destination, 3.0f);

            Assert.That(connection.GetDestination(), Is.EqualTo(destination));

            Assert.That(connection.GetDuration(), Is.EqualTo(3.0f).Within(0.001f));
        }

        //Méthode de test qui crée un GameObject avec une location comme le WorlBuilder
        private LocationComponent CreateLocation(string location_name)
        {
            GameObject location_object = new GameObject(location_name);
            LocationComponent location_component = new LocationComponent(location_name);

            location_object.AddComponent(location_component);
            location_object.SetIsActive(true);

            return location_component;
        }
    }
}