using System;
using System.Collections.Generic;

namespace DialogusSystemus
{
    public static class FrameColors
    {
        private static readonly Dictionary<Options, bool> options = new()
        {
            { Options.Focused, false },
            { Options.Chaptered, false },
            { Options.Error, false },
            { Options.Sectioned, false },
        };

        public static void ClearFrameColor() { Console.Clear(); }

        public static void ResetOptions()
        {
            var keys = new Options[options.Count];
            options.Keys.CopyTo(keys, 0);

            foreach (var k in keys)
                options[k] = false;
        }
        public static void SetOption(params Options[] opt)
        {
            foreach (var o in opt)
                options[o] = true;
        }
        public static void UnsetOption(params Options[] opt)
        {
            foreach (var o in opt)
                options[o] = false;
        }

        public static void SetColor(Tag tag = Tag.Default)
        {
            if (options[Options.Focused])
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else if (options[Options.Chaptered])
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if (options[Options.Error])
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (options[Options.Sectioned])
            {
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
                SetColorWithTag(tag);
        }

        private static void SetColorWithTag(Tag tag)
        {
            switch (tag)
            {
                case Tag.Default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Tag.Name:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case Tag.Place:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Tag.Reward:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    break;
            }
        }

        public static void UnsetColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
