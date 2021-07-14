using System;
using System.Reflection;

namespace DialogusSystemus
{
    public static class Frame
    {
        public const string FrameOuterSpaces = " ";
        public const string FrameInnerSpaces = " ";
        public const string VerticalBorder = "|";
        public const string HorizontalBorder = "-";
        public static string LeftAngle => FrameOuterSpaces + VerticalBorder;
        public static string RightAngle => VerticalBorder;

        private const double ProportionsOfFrameHeight = 2 / 3.0;

        public static int NeededSpaces => (FrameOuterSpaces + VerticalBorder + FrameInnerSpaces).Length;
        public static int FrameWidth => Console.WindowWidth - 2 * NeededSpaces;
        public static int FrameHeight => (int)(Console.WindowHeight * ProportionsOfFrameHeight);
        public static void PrintHorizontalFrameBorder()
        {
            Console.Write(LeftAngle);
            for (int i = 0; i < FrameWidth; i++) Console.Write(HorizontalBorder);
            Console.WriteLine(RightAngle);
        }

        private static string GetAllString(params (string, Tag)[] input)
        {
            string str = "";
            foreach (var inp in input)
            {
                var (words, tag) = inp;
                str += words;
            }
            return str;
        }

        public static void PrintAsTitle(string title, bool isDetached = true, Options opt = Options.Sectioned)
        {
            PrintHorizontalFrameBorder();
            FrameColors.SetOption(opt);

            if (!isDetached)
                PrintLineInFrame();

            PrintLineInFrame(Alignment.Center, (title, Tag.Default));

            if (isDetached)
            {
                PrintHorizontalFrameBorder();
            }
            else
            {
                PrintLineInFrame();
                FrameColors.UnsetOption(opt);

            }
        }

        public static void PrintSelectionMenu(object[] items, bool isChapter = false)
        {
            var indexOfItem = -1;
            var indexOfLastItem = items.Length - 1;
            ConsoleKey key = ConsoleKey.Clear;

            while (key != ConsoleKey.Escape)
            {
                if ((key == ConsoleKey.Enter) && (indexOfItem >= 0))
                {
                    PrintWithType(items[indexOfItem]);
                    key = (isChapter) ? ConsoleKey.Clear : ExitFromSubmenu();
                }
                else
                {
                    for (var i = 0; i < items.Length; i++)
                        PrintElemOfMenu(indexOfItem == i, items[i]);
                    
                    if (isChapter)
                        PrintHorizontalFrameBorder();

                    key = Console.ReadKey().Key;
                    indexOfItem = MoveFocus(key, indexOfItem, indexOfLastItem);
                }

                FrameColors.ClearFrameColor();
            }
        }

        private static void PrintWithType(object item)
        {
            switch (item.GetType().ToString())
            {
                case "DialogusSystemus.Term":
                    Print((Term)item);
                    break;
                case "DialogusSystemus.Note":
                    Print((Note)item);
                    break;
                case "DialogusSystemus.Chapter":
                    Print((Chapter)item);
                    break;
                default:
                    PrintError("Unknown Type of Items!");
                    return;
            }
        }
        private static void PrintTitleWithType(object item)
        {
            switch (item.GetType().ToString())
            {
                case "DialogusSystemus.Term":
                    PrintTitle((Term)item);
                    break;
                case "DialogusSystemus.Note":
                    PrintTitle((Note)item);
                    break;
                case "DialogusSystemus.Chapter":
                    PrintTitle((Chapter)item);
                    break;
                default:
                    PrintError("Unknown Type of Items!");
                    return;
            }
        }

        private static void PrintError(string err)
        {
            PrintHorizontalFrameBorder();
            FrameColors.SetOption(Options.Error);
            PrintLineInFrame(Alignment.Left, (err, Tag.Default));
            FrameColors.UnsetOption(Options.Error);
            PrintHorizontalFrameBorder();
            Stop();
        }

        private static ConsoleKey ExitFromSubmenu()
        {
            while (Console.ReadKey().Key != ConsoleKey.Escape) ;
            return ConsoleKey.Clear;
        }

        private static int MoveFocus(ConsoleKey key, int indexOfItem, int indexOfLastItem)
        {
            return key switch
            {
                ConsoleKey.UpArrow or ConsoleKey.W => Math.Max(0, indexOfItem - 1),
                ConsoleKey.DownArrow or ConsoleKey.S => Math.Min(indexOfLastItem, indexOfItem + 1),
                ConsoleKey.Spacebar or ConsoleKey.Enter => indexOfItem,
                _ => -1,
            };
        }

        private static void PrintElemOfMenu(bool isFocused, object item)
        {
            if (isFocused)
            {
                FrameColors.SetOption(Options.Focused);
                PrintTitleWithType(item);
                FrameColors.UnsetOption(Options.Focused);
            }
            else
            {
                PrintTitleWithType(item);
            }
        }

        public static void Print(Note note)
        {
            if (!note.GetOpeningStatus()) return;

            PrintTitle(note, isDetached: false);
            note.GetContent().Print();
            Console.WriteLine();
        }
        public static void Print(Term term)
        {
            if (!term.GetOpeningStatus()) return;

            PrintTitle(term, isDetached: false);
            foreach (var q in term.GetQuotes())
            {
                q.PrintQuote(onlyAuthor: false);
                Stop();
            }
        }
        public static void Print(Chapter chapter)
        {
            if (!chapter.GetOpeningStatus()) return;

            PrintSelectionMenu(chapter.GetNotes());
            // foreach notes
        }

        public static void PrintTitle(Note note, bool isDetached = true)
        {
            if (!note.GetOpeningStatus()) return;

            PrintAsTitle(note.GetTitle(), isDetached);
        }
        public static void PrintTitle(Term term, bool isDetached = true)
        {
            if (!term.GetOpeningStatus()) return;

            PrintAsTitle(term.GetTermTitle(), isDetached);
        }
        public static void PrintTitle(Chapter chapter, bool isDetached = false)
        {
            if (!chapter.GetOpeningStatus()) return;

            PrintAsTitle(chapter.GetTitle(), isDetached, Options.Chaptered);
        }

        public static void PrintLineInFrame(
            Alignment align = Alignment.Left,
            params(string, Tag)[] input)
        {
            Console.Write(LeftAngle);
            FrameColors.SetColor();
            Console.Write(FrameInnerSpaces);

            PrintTextWithAlign(align, input);

            FrameColors.UnsetColor();

            Console.WriteLine(RightAngle);
        }

        private static void PrintTextWithAlign(
            Alignment align = Alignment.Left,
            params (string, Tag)[] input)
        {
            string str = GetAllString(input);
            var spacesWidth = (str != "") ? (FrameWidth - str.Length - 1) : (FrameWidth - 1);

            switch (align)
            {
                case Alignment.Left:
                    PrintInputWords(input);
                    PrintSpaces(spacesWidth);
                    break;
                case Alignment.Center:
                    PrintSpaces(spacesWidth / 2);
                    PrintInputWords(input);
                    PrintSpaces(spacesWidth - spacesWidth / 2);
                    break;
                case Alignment.Right:
                    PrintSpaces(spacesWidth);
                    PrintInputWords();
                    break;
                default:
                    break;
            }
        }

        private static void PrintInputWords(params (string, Tag)[] input)
        {
            foreach (var inp in input)
            {
                var (words, tag) = inp;
                FrameColors.SetColor(tag);
                Console.Write(words);
            }
        }

        private static void PrintSpaces(int spacesWidth)
        {
            for (int i = 0; i < spacesWidth; i++)
                Console.Write(" ");
        }

        public static void Stop() { Console.ReadKey(); }
    }
}