using GameLibrary.Events;

namespace GameLibrary.States
{
    public class MainMenuState : IState
    {
        private readonly GameManager _gameManager;
        private readonly EventManager _eventManager;

        public MainMenuState(GameManager game_manager, EventManager event_manager)
        {
            _gameManager = game_manager;
            _eventManager = event_manager;
        }

        //S'abonne aux inputs utilisateur
        public void Enter()
        {
            _eventManager.RegisterToEvent<GameActionGameEvent>(OnGameActionGameEvent);
        }

        //Se désabonne en quittant cet état
        public void Exit()
        {
            _eventManager.UnregisterFromEvent<GameActionGameEvent>(OnGameActionGameEvent);
        }

        public void Update(float elapsed_time)
        {

        }

        public void FixedUpdate(float fixed_elapsed_time)
        {

        }

        //Réagit aux commandes demandée
        private void OnGameActionGameEvent(IGameEvent game_event)
        {
            GameActionGameEvent game_action_game_event = game_event as GameActionGameEvent;

            switch (game_action_game_event._gameActionType)
            {
                case GameActionType.CONFIRM:
                    _gameManager.StartExploration();
                    break;

                case GameActionType.QUIT:
                    _gameManager.RequestQuit();
                    break;
            }
        }
    }
}
