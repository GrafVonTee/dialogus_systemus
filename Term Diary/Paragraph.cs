using System;

namespace DialogusSystemus
{
    public class Paragraph
    {
        private readonly string text;
        public Paragraph(string newText) { text = newText; }
        private Word[] SplitTextToWordsInParagraph()
        {
            var inBlock = false;
            var tag = Tag.Default;
            var someWords = text.Split();
            var arrayOfWords = new Word[someWords.Length];
            for (int i = 0; i < someWords.Length; i++)
            {
                arrayOfWords[i] = new Word();
                (arrayOfWords[i], inBlock) = ExtractWordFromBlock(someWords[i], tag, inBlock);
            }
            return arrayOfWords;
        }

        private (Word word, bool InBlock) ExtractWordFromBlock(string str, Tag tag, bool inBlock)
        {
            if (str.Contains("{"))
            {
                tag = ExtractTag(str);
                str = str[5..];
                inBlock = true;
            }
            if (str.Contains("|"))
            {
                TermDiary.OpenTerm(str[0..str.IndexOf('|')]);
                str = str[(str.IndexOf('|') + 1)..];
            }
            if (str.Contains("}"))
            {
                str = str.Remove(str.IndexOf("}"), 1);
                inBlock = false;
            }
            str += " ";
            var w = new Word() { Content = str, Tag = tag };

            return (w, inBlock);
        }
        private static Tag ExtractTag(string w)
        {
            return w.Substring(0, 4) switch
            {
                "#rwd" => Tag.Reward,
                "#nam" => Tag.Name,
                "#plc" => Tag.Place,
                _ => Tag.Default,
            };
        }

        public static string Join(params Word[] words)
        {
            string str = "";
            foreach (var w in words)
                str += w.Content;
            return str;
        }

        public string AllString => Join(Words);

        public Word[] Words => SplitTextToWordsInParagraph();
    }

    public class Word
    {
        public string Content = "";
        public Tag Tag = Tag.Default;
    }

    public class Utterance
    {
        public Paragraph[] Paragraphs;
    }
}
