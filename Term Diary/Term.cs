using System;

using TermName = DialogusSystemus.Paragraph;

namespace DialogusSystemus
{
    public class Term
    {
        private TermName termName;
        private Tag tag;
        private Quote[] quotes = Array.Empty<Quote>();
        private bool isOpen = false;

        public void SetTerm(string newTermName, Tag newTag)
        {
            SetTitle(newTermName);
            SetTag(newTag);
        }

        public void SetTitle(string newTermTitle) { termName = new TermName(newTermTitle); }
        public void SetTag(Tag newTag) { tag = newTag; }

        public void AddQuote(params Quote[] newQuotes)
        {
            foreach (var q in newQuotes)
            {
                Array.Resize(ref quotes, quotes.Length + 1);
                quotes[^1] = q;
            }
        }
        
        public TermName GetTermTitle() { return termName; }
        public Quote[] GetQuotes() { return quotes; }

        public void OpenTerm() { isOpen = true; }
        public bool GetOpeningStatus() { return isOpen; }
    }
}