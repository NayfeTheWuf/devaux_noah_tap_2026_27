namespace GameLibrary.Events
{
    public class RegisterGameObjectGameEvent : IGameEvent
    {
        public readonly GameObject _gameObject;

        public RegisterGameObjectGameEvent(GameObject game_object)
        {
            _gameObject = game_object;
        }
    }
}