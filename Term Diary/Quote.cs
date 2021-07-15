using System;

using Author = DialogusSystemus.Paragraph;
using Content = DialogusSystemus.Utterance;

namespace DialogusSystemus
{
    public class Quote
    {
        private Content content;
        private Author author = new("#nam{@} author");

        public void ChangeAuthor(Author newAuthor) { author = newAuthor; }
        public void SetContent(Content newContent) { content = newContent; }
        public void SetQuote(Content utt, Author aut)
        {
            ChangeAuthor(aut);
            SetContent(utt);
        }

        public Content GetContent() { return content; }
        public Author GetAuthor() { return new Author("#nam{@} " + author.AllString); }
    }
}
