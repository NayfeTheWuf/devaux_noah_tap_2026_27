using activity_00_tap_26_27.CORE.Components;
using activity_00_tap_26_27.CORE.Events;
using activity_00_tap_26_27.Events;
using System;
using System.Diagnostics;

namespace activity_00_tap_26_27
{
    public class GameEngine
    {
        //Var de temps
        private const float FIXED_FRAME_TIME = 20 / 1000.0f;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        //Var de Manager
        private readonly ConsoleRenderManager _renderManager = new ConsoleRenderManager();
        private readonly EventManager _eventManager = new EventManager();
        private GameManager _gameManager;
        
        //Var indé
        private bool _shouldQuit = false;

        //Lance le program
        public void Run()
        {
            _stopwatch.Start();

            //Accumulateur de temps
            float lag = 0.0f;
            float last_time = GetCurrentTime();

            //Appel des Manager
            LogManager log_manager = new LogManager(_eventManager);
            _gameManager = new GameManager(_eventManager);

            //Game loop
            while (!_gameManager.GetShouldQuit())
            {
                //Valeur de temps
                float loop_start_time = GetCurrentTime();
                float elapsed_time = loop_start_time - last_time;
                lag += elapsed_time;

                ProcessInput();

                //Toujours à la même fréquence
                while (lag >= FIXED_FRAME_TIME)
                {
                    FixedUpdate(FIXED_FRAME_TIME);
                    _eventManager.ProcessEvent();
                    lag -= FIXED_FRAME_TIME;
                }

                Update(elapsed_time);
                Render();

                last_time = loop_start_time;
            }
        }

        //Lit les input du joueur pour les traduire et déclancher l'évènement correspondant
        private void ProcessInput()
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo player_command = Console.ReadKey(true);
                //Traduit l'input du joueur
                GameActionType game_action_type = TranslateKey(player_command.Key);

                //Si l'input est correcte, trigger l'event lié
                if (game_action_type != null)
                {
                    _eventManager.TriggerEvent(new GameActionGameEvent(game_action_type));
                }
            }
        }

        //Traduit l'input en commande de jeu
        private GameActionType TranslateKey(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    Console.WriteLine("Up");
                    return GameActionType.NAVIGATE_UP;

                case ConsoleKey.DownArrow:
                    Console.WriteLine("Down");
                    return GameActionType.NAVIGATE_DOWN;

                case ConsoleKey.Enter:
                    Console.WriteLine("Confirm");
                    return GameActionType.CONFIRM;

                case ConsoleKey.Backspace:
                    Console.WriteLine("Cancel");
                    return GameActionType.CANCEL;

                case ConsoleKey.Escape:
                    Console.WriteLine("Quit");
                    return GameActionType.QUIT;
                
                default:
                    return GameActionType.NULL;
            }
        }

        //Appel le FixedUpdate du GameManager
        private void FixedUpdate(float fixed_elapsed_time)
        {
            _gameManager.FixedUpdate(fixed_elapsed_time);
        }

        //Appel l'Update du GameManager
        private void Update(float elapsed_time)
        {
            _gameManager.Update(elapsed_time);
        }

        //Méthode de rendu graphique 
        private void Render()
        {
            _renderManager.Draw(0,0, "Game in progress...\n", ConsoleColor.Magenta);
            _renderManager.Render();
        }

        //Retourner le temps actuel
        private float GetCurrentTime()
        {
            return _stopwatch.ElapsedMilliseconds / 1000.0f;
        }
    }
}