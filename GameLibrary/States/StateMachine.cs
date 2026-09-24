namespace GameLibrary.States
{
    public class StateMachine
    {
        private IState _currentState;

        //Définir l'état de jeu initial
        public void SetInitialState(IState initial_state)
        {
            _currentState = initial_state;
            _currentState.Enter();
        }

        //Changer d'état de jeu
        public void ChangeState(IState new_state)
        {
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
    }
}