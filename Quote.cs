using System;

namespace DialogusSystemus
{
    public class Quote
    {
        private Utterance utterance;
        private string author = "Unknown author";

        public void ChangeAuthor(string newAuthor) { author = newAuthor; }
        public void ChangeUtterance(Utterance newUtterance) { utterance = newUtterance; }
        public void SetQuote(Utterance utt, string aut)
        {
            ChangeAuthor(aut);
            ChangeUtterance(utt);
        }

        public void PrintQuote(bool onlyAuthor = false)
        {
            if (!onlyAuthor)
                utterance.Print();
            else
                Frame.PrintHorizontalFrameBorder();

            Frame.PrintLineInFrame(
                Alignment.Left,
                ("@ ", Tag.Default),
                (author, Tag.Name)
                );

            Frame.PrintHorizontalFrameBorder();
            if (!onlyAuthor)
                Console.WriteLine();
        }
    }
}
