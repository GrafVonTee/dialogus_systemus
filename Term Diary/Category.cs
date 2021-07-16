using System;
using System.Collections.Generic;

using CategoriesTitle = DialogusSystemus.Paragraph;
using Identifier = System.String;

namespace DialogusSystemus
{
    public class Category
    {
        private CategoriesTitle title;
        private readonly Dictionary<Identifier, Term> terms = new();
        private bool isOpen = false;

        public void SetCategory(string newTitle, (Identifier, Term)[] newTerms)
        {
            SetTitle(newTitle);
            AddTerm(newTerms);
        }
        public void SetTitle(string newTitle) { title = new CategoriesTitle(newTitle); }
        public void AddTerm((Identifier Identifier, Term Content)[] newTerms)
        {
            foreach (var n in newTerms)
                terms[n.Identifier] = n.Content;
        }

        public CategoriesTitle GetTitle() { return title; }
        public Term[] GetTerms()
        {
            var termsArray = new Term[terms.Values.Count];
            terms.Values.CopyTo(termsArray, 0);
            return termsArray;
        }
        public bool GetOpeningStatus() { return isOpen; }
        public void OpenTerm(Identifier identifier)
        {
            terms[identifier].OpenTerm();
            isOpen = true;
        }

        public bool ContentsTheTerm(Identifier identifier)
        {
            return terms.ContainsKey(identifier);
        }
    }
}