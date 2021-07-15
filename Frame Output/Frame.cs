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

        public static void PrintAsTitle(Paragraph title, bool isDetached = true, Options opt = Options.Sectioned)
        {
            PrintHorizontalFrameBorder();
            FrameColors.SetOption(opt);

            if (!isDetached)
                PrintLineInFrame();

            Print(title, isTitle: true);

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
                    Console.Write('\b');

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
                case "DialogusSystemus.Utterance":
                    Print((Utterance)item);
                    break;
                case "DialogusSystemus.Paragraph":
                    Print((Paragraph)item);
                    break;
                case "DialogusSystemus.Word[]":
                    Print((Word[])item);
                    break;
                default:
                    var err = new Paragraph("Unknown Type of Items!");
                    PrintError(err);
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
                    var err = new Paragraph("Unknown Type of Items!");
                    PrintError(err);
                    return;
            }
        }

        private static void PrintError(Paragraph err)
        {
            PrintHorizontalFrameBorder();
            FrameColors.SetOption(Options.Error);
            Print(err);
            FrameColors.UnsetOption(Options.Error);
            PrintHorizontalFrameBorder();
            Stop();
        }

        private static ConsoleKey ExitFromSubmenu()
        {
            while (Console.ReadKey().Key != ConsoleKey.Escape)
                Console.Write('\b');
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
            Print(note.GetContent());
            Console.WriteLine();
        }
        public static void Print(Term term)
        {
            if (!term.GetOpeningStatus()) return;

            PrintTitle(term, isDetached: false);
            foreach (var q in term.GetQuotes())
            {
                Print(q, onlyAuthor: false);
                Stop();
            }
        }
        public static void Print(Chapter chapter)
        {
            if (!chapter.GetOpeningStatus()) return;

            PrintSelectionMenu(chapter.GetNotes());
            // foreach notes
        }
        private static void Print(params Word[] words)
        {
            foreach (var w in words)
            {
                FrameColors.SetColor(w.Tag);
                Console.Write(w.Content);
            }
        }
        public static void Print(Quote quote, bool onlyAuthor = false)
        {
            if (!onlyAuthor)
                Print(quote.GetContent());
            else
                PrintHorizontalFrameBorder();

            Print(quote.GetAuthor(), endPar: true);

            PrintHorizontalFrameBorder();
            if (!onlyAuthor)
                Console.WriteLine();
        }
        public static void Print(Paragraph paragraph, bool isTitle = false, bool endPar = false)
        {
            var align = (isTitle) ? Alignment.Center : Alignment.Left;
            Word tempWord;
            Word[] tempLines = new[] { new Word() };

            foreach (var w in paragraph.Words)
            {
                tempWord = w;

                if ((Paragraph.Join(tempLines) + w.Content).Length + 1 > FrameWidth)
                {
                    PrintLineInFrame(align, tempLines);
                    tempLines = Array.Empty<Word>();
                }

                Array.Resize(ref tempLines, tempLines.Length + 1);
                tempLines[^1] = tempWord;
            }

            if (tempLines.Length > 0)
                PrintLineInFrame(align, tempLines);

            if (!isTitle && !endPar)
            {
                Stop();
                PrintLineInFrame();
            }
        }
        public static void Print(Utterance utt)
        {
            PrintHorizontalFrameBorder();

            foreach (var paragraph in utt.Paragraphs)
                Print(paragraph, endPar: (paragraph == utt.Paragraphs[^1]) ? true : false);

            PrintHorizontalFrameBorder();
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

        public static void PrintLineInFrame(Alignment align = Alignment.Left, params Word[] words)
        {
            Console.Write(LeftAngle);
            FrameColors.SetColor();
            Console.Write(FrameInnerSpaces);

            PrintTextWithAlign(align, words);

            FrameColors.UnsetColor();

            Console.WriteLine(RightAngle);
        }

        private static void PrintTextWithAlign(Alignment align = Alignment.Left, params Word[] words)
        {
            string str = Paragraph.Join(words);
            var spacesWidth = (str != "") ? (FrameWidth - str.Length - 1) : (FrameWidth - 1);

            switch (align)
            {
                case Alignment.Left:
                    Print(words);
                    PrintSpaces(spacesWidth);
                    break;
                case Alignment.Center:
                    PrintSpaces(spacesWidth / 2);
                    Print(words);
                    PrintSpaces(spacesWidth - spacesWidth / 2);
                    break;
                case Alignment.Right:
                    PrintSpaces(spacesWidth);
                    Print();
                    break;
                default:
                    break;
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