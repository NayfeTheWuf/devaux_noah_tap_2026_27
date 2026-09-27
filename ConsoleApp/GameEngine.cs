using GameLibrary;
using GameLibrary.Events;
using GameLibrary.Logs;
using System;
using System.Diagnostics;

namespace ConsoleApp
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

        //Lance le program
        public void Run()
        {
            _stopwatch.Start();

            //Accumulateur de temps
            float lag = 0.0f;
            float last_time = GetCurrentTime();

            //Appel des Manager
            ILogWriter log_writer = new LogFileWriter();
            LogManager log_manager = new LogManager(_eventManager, log_writer);
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
                if (game_action_type != GameActionType.NULL)
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
                    return GameActionType.NAVIGATE_UP;

                case ConsoleKey.DownArrow:
                    return GameActionType.NAVIGATE_DOWN;

                case ConsoleKey.Enter:
                    return GameActionType.CONFIRM;

                case ConsoleKey.Backspace:
                    return GameActionType.CANCEL;

                case ConsoleKey.Escape:
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
            if (_gameManager.GetIsMenuStateActive())
            {
                RenderMainMenu();
            }
            else
            {
                RenderExploring();
            }

            _renderManager.Render();
        }

        //Écran titre
        private void RenderMainMenu()
        {
            _renderManager.Draw(0, 0, "Main Menu", ConsoleColor.Magenta);
            _renderManager.Draw(0, 1, "Press CONFIRM to start", ConsoleColor.White);
            _renderManager.Draw(0, 2, "Press QUIT to leave", ConsoleColor.White);
        }

        //Exploration
        private void RenderExploring()
        {
            //Apparition de la console
            _renderManager.Draw(0, 0, "Game in progress...", ConsoleColor.Magenta);

            //Récupérer la location actuelle
            string current_location_line = "Exploring " + _gameManager.GetCurrentLocationName();
            _renderManager.Draw(0, 1, current_location_line, ConsoleColor.Cyan);
            int selected_destination_index = _gameManager.GetSelectedDestinationIndex();

            //Récupérer les prochaines destinations 
            int destination_count = _gameManager.GetDestinationCount();

            for (int destination_index = 0; destination_index < destination_count; destination_index++)
            {
                string destination_name = _gameManager.GetDestinationName(destination_index);
                float destination_duration = _gameManager.GetDestinationDuration(destination_index);

                //Affichage
                string destination_line = (destination_index + 1) + ". " + destination_name + ": Distance: " + destination_duration;

                //Coloré l'élément sélectionné
                ConsoleColor background_color = ConsoleColor.Black;
                if (destination_index == selected_destination_index)
                {
                    background_color = ConsoleColor.DarkGreen;
                }

                _renderManager.Draw(0, 2 + destination_index, destination_line, ConsoleColor.Green, background_color);
            }
        }

        //Retourner le temps actuel
        private float GetCurrentTime()
        {
            return _stopwatch.ElapsedMilliseconds / 1000.0f;
        }
    }
}