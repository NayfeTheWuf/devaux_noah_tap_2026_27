namespace GameLibrary.Components
{
    public abstract class Component 
    {
        //Var
        private bool _isActive = true;
        private GameObject _gameObject;

        //Constructeur 
        public Component(GameObject game_object)
        {
            _gameObject = game_object;
        }

        public virtual void Update(float elapsed_time)
        {

        }

        public virtual void FixedUpdate(float elapsed_fixed_time)
        {

        }

        //Retourne le statut de "_isActive"
        public bool GetIsActive()
        {
            return _isActive;
        }

        //Définir un statut à "_isActive"
        public void SetActive(bool is_active)
        {
            if (_isActive != is_active)
            {
                if (is_active)
                {
                    OnEnable();
                }
                else
                {
                    OnDisable();
                }

                _isActive = is_active;
            }
        }

        public virtual void OnEnable()
        {

        }

        public virtual void OnDisable()
        {

        }
    }
}