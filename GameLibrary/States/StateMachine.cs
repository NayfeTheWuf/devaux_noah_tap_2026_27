using GameLibrary.Events;

namespace GameLibrary.States
{
    public class StateMachine
    {
        private readonly EventManager _eventManager;
        private IState _currentState;

        public StateMachine(EventManager event_manager)
        {
            _eventManager = event_manager;
        }

        //Définir l'état de jeu initial
        public void SetInitialState(IState initial_state)
        {
            LogTransition("None", initial_state);

            _currentState = initial_state;
            _currentState.Enter();
        }

        //Changer d'état de jeu
        public void ChangeState(IState new_state)
        {
            LogTransition(_currentState.GetType().Name, new_state);

            _currentState.Exit();
            _currentState = new_state;
            _currentState.Enter();
        }

        //Mise à jour par frame de l'état du jeu
        public void Update(float elapsed_time)
        {
            _currentState.Update(elapsed_time);
        }

        //Mise à jour à intervalle spécifique de l'état du jeu
        public void FixedUpdate(float fixed_elapsed_time)
        {
            _currentState.FixedUpdate(fixed_elapsed_time);
        }

        //Écrit dans les logs lors de transition d'état
        private void LogTransition(string previous_state_name, IState new_state)
        {
            string log_message = previous_state_name + " to " + new_state.GetType().Name;

            _eventManager.TriggerEvent(new LogMessageGameEvent(log_message));
        }
    }
}