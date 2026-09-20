using System;

namespace activity_00_tap_26_27
{
    public class ConsoleRenderManager
    {
        private struct Pixel
        {
            public char Character;
            public ConsoleColor Color;
            public ConsoleColor ForegroundColor;
            public ConsoleColor BackgroundColor;
        }

        private Pixel[,] _currentBuffer;
        private Pixel[,] _previousBuffer;
        private int _width;
        private int _height;

        //Mise en graphique de la console
        public ConsoleRenderManager()
        {
            _width = Console.WindowWidth;
            _height = Console.WindowHeight;
            _currentBuffer = new Pixel[_width, _height];
            _previousBuffer = new Pixel[_width, _height];
            
            Console.CursorVisible = false;
            ClearBuffer(_currentBuffer);
            ClearBuffer(_previousBuffer);
        }

        // (idk)
        private void ClearBuffer(Pixel[,] buffer)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    buffer[x, y] = new Pixel 
                    { 
                        Character = ' ',
                        ForegroundColor = ConsoleColor.Gray,
                        BackgroundColor = ConsoleColor.Black
                    };
                }
            }
        }

        //Dessine la structure graphique
        public void Draw(int x, int y, string text, ConsoleColor color, ConsoleColor background_color = ConsoleColor.Black)
        {
            if (x < 0 || x >= _width || y < 0 || y >= _height)
            {
                return;
            }

            for (int character_index = 0; character_index < text.Length; character_index++)
            {
                if (x + character_index < _width)
                {
                    _currentBuffer[x + character_index, y] = new Pixel
                    {
                        Character = text[character_index],
                        ForegroundColor = color,
                        BackgroundColor = background_color
                    };
                }
            }
        }
        
        //Affiche le visuel définit
        public void Render()
        {
            // The actual drawing to the console happens here by comparing buffers
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    Pixel current = _currentBuffer[x, y];
                    Pixel previous = _previousBuffer[x, y];

                    if (
                            current.Character != previous.Character
                            || current.ForegroundColor != previous.ForegroundColor
                            || current.BackgroundColor != previous.BackgroundColor
                        )
                    {
                        Console.SetCursorPosition(x, y);
                        Console.ForegroundColor = current.ForegroundColor;
                        Console.BackgroundColor = current.BackgroundColor;
                        Console.Write(current.Character);
                        _previousBuffer[x, y] = current;
                    }
                }
            }

            //Reset current buffer for next frame
            ClearBuffer(_currentBuffer);
            
            Console.ResetColor();
        }
    }
}