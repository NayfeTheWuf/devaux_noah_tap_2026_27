using System.Collections.Generic;
using System.Diagnostics;
using System;
using activity_00_tap_26_27.Components;
using activity_00_tap_26_27.Events;

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
            while (!_shouldQuit)
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

        //Confirme les actions claviers de l'utilisateur
        private void ProcessInput()
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo player_command = Console.ReadKey(true);

                if (player_command.Key == ConsoleKey.Escape)
                {
                    _shouldQuit = true;
                }
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