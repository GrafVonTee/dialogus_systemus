using System;

namespace DialogusSystemus
{
    public class Utterance
    {
        private readonly string[] text;

        public Utterance(string[] newText) { text = newText; }

        private string[][] SplitTextToWords()
        {
            var arrayOfWords = new string[text.Length][];
            for (int i = 0; i < text.Length; i++)
                arrayOfWords[i] = text[i].Split();
            return arrayOfWords;
        }

        private string[][] Words => SplitTextToWords();

        private string GetLastWordsString()
        {
            var lastWords = "";
            foreach (var w in Words[^1])
                lastWords += w + " ";
            return lastWords;
        }

        private static Tag GetTag(string w)
        {
            return w.Substring(0, 4) switch
            {
                "#rwd" => Tag.Reward,
                "#nam" => Tag.Name,
                "#plc" => Tag.Place,
                _ => Tag.Default,
            };
        }

        // ОТРЕФАКТОРИ БУДЬ ДОБР ПОЖАЛУЙСТА ЭТО КОШМАР
        private void PrintWords()
        {
            foreach (string[] paragraph in Words)
            {
                var wordsInLine = "";
                string wrd;
                Tag tag = Tag.Default;
                (string, Tag)[] wordsInParagraphWithTag = { ("", Tag.Default) };
                var par = "";
                bool inBlock = false;
                
                foreach (var w in paragraph)
                {
                    if ((wordsInLine + w + " ").Length + 1 > Frame.FrameWidth)
                    {
                        Frame.PrintLineInFrame(Alignment.Left, wordsInParagraphWithTag);
                        wordsInLine = "";
                        wordsInParagraphWithTag = Array.Empty<(string, Tag)>();
                    }

                    (wrd, tag, inBlock) = WorkWithBlock(w, tag, inBlock);

                    Array.Resize(ref wordsInParagraphWithTag, wordsInParagraphWithTag.Length + 1);
                    wordsInParagraphWithTag[^1] = (wrd, tag);
                    wordsInLine += wrd;

                    tag = (inBlock) ? tag : Tag.Default;
                    par += w + " ";
                }

                if (wordsInLine != "")
                    Frame.PrintLineInFrame(Alignment.Left, wordsInParagraphWithTag);

                if (par == GetLastWordsString())
                    continue;

                Frame.Stop();
                Frame.PrintLineInFrame();
            }
        }

        public static (string Word, Tag Tag, bool InBlock) WorkWithBlock(string w, Tag tag, bool inBlock)
        {
            if (w.Contains("{"))
            {
                tag = GetTag(w);
                w = w[5..];
                inBlock = true;
            }
            if (w.Contains("|"))
            {
                TermDiary.OpenTerm(w[0..w.IndexOf('|')]);
                w = w[(w.IndexOf('|') + 1)..];
            }
            if (w.Contains("}"))
            {
                w = w.Remove(w.IndexOf("}"), 1);
                inBlock = false;
            }
            w += " ";

            return (w, tag, inBlock);
        }

        public void Print()
        {
            Frame.PrintHorizontalFrameBorder();
            PrintWords();
            Frame.PrintHorizontalFrameBorder();
        }
    }
}
