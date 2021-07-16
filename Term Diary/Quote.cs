using System;

using Author = System.String;
using Content = DialogusSystemus.Paragraph;

namespace DialogusSystemus
{
    public class Quote
    {
        private Content content;
        private Author author = "Dude";

        public void SetAuthor(Author newAuthor) { author = newAuthor; }
        public void SetContent(string newContent) { content = new Paragraph(newContent); }
        public void SetQuote(string utt, Author aut)
        {
            SetAuthor(aut);
            SetContent(utt);
        }

        public Content GetContent()
        {
            return new Content("#nam{@} " + author + ": " + content.GetText());
        }
        public Content GetAuthor() { return new Content("#nam{@} " + author); }
    }
}
