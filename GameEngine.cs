using System.Collections.Generic;
using System.Diagnostics;
using System;
using activity_00_tap_26_27.Components;
using activity_00_tap_26_27.Events;

namespace activity_00_tap_26_27
{
    public class GameEngine
    {
        private const float FIXED_FRAME_TIME = 20 / 1000.0f;

        private readonly Stopwatch _stopwatch = new Stopwatch();

        private readonly List<GameObject> _gameObjectTable = new List<GameObject>();

        private readonly ConsoleRenderManager _renderManager = new ConsoleRenderManager();

        private readonly EventManager _eventManager;
        
        private bool _shouldQuit = false;

        //Lance le program
        public void Run()
        {
            _stopwatch.Start();

            float last_time = GetCurrentTime();

            //Game loop
            while (!_shouldQuit)
            {
                float loop_start_time = GetCurrentTime();
                float elapsed_time = loop_start_time - last_time;
                
                ProcessInput();

                while (elapsed_time == 50)
                {
                    FixedUpdate(FIXED_FRAME_TIME);    
                }

                Update(elapsed_time);

                _eventManager.ProcessEvent();

                Render();

                last_time = loop_start_time;
            }

            Console.WriteLine("Goodbye!");
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

        //Mise à jour à intervalles de temps fixes
        private void FixedUpdate(float fixed_elapsed_time)
        {
            for (int object_index = 0; object_index < _gameObjectTable.Count; object_index++)
            {
                GameObject game_object = _gameObjectTable[object_index];

                if (game_object.GetIsActive())
                {
                    game_object.FixedUpdate(fixed_elapsed_time);
                }
            }
        }

        //Mise à jour à chaque frame
        private void Update(float elapsed_time)
        {
            for (int object_index = 0; object_index < _gameObjectTable.Count; object_index++)
            {
                GameObject game_object = _gameObjectTable[object_index];

                if (game_object.GetIsActive())
                {
                    game_object.Update(elapsed_time);
                }
            }
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