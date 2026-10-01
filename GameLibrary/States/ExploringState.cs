using GameLibrary.Events;

namespace GameLibrary.States
{
    public class ExploringState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly GameManager _gameManager;
        private readonly EventManager _eventManager;

        public ExploringState(StateMachine state_machine, GameManager game_manager, EventManager event_manager)
        {
            _stateMachine = state_machine;
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
                case GameActionType.NAVIGATE_UP:
                    _gameManager.NavigateSelection(-1);
                    break;

                case GameActionType.NAVIGATE_DOWN:
                    _gameManager.NavigateSelection(1);
                    break;

                case GameActionType.CONFIRM:
                    _gameManager.ConfirmSelection();
                    break;

                case GameActionType.CANCEL:
                    _gameManager.CancelSelection();
                    break;

                case GameActionType.QUIT:
                    _gameManager.RequestQuitFromExploration();
                    break;
            }
        }
    }
}
