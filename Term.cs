using System;
using System.Collections.Generic;

using TermName = System.String;

namespace DialogusSystemus
{
    public class Term
    {
        private TermName termName;
        private Tag tag;
        private Quote[] quotes = new Quote[0];
        private bool isOpen = false;

        public Term(string newTermName, Tag newTag) { termName = newTermName; tag = newTag; }

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

    public static class TermDiary
    {
        private static Dictionary<string, Term> termDiary = new();

        public static void OpenTerm(string nameOfTerm) { termDiary[nameOfTerm].OpenTerm(); }
    }
}