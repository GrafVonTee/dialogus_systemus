using NoteTitle = DialogusSystemus.Paragraph;
using Content = DialogusSystemus.Utterance;

namespace DialogusSystemus
{
    public class Note
    {
        private NoteTitle title;
        private Content content;
        private bool isOpen = false;

        public void SetTitle(string newTitle) { title = new Paragraph(newTitle); }
        public void SetContent(Content newContent) { content = newContent; }

        public void OpenNote() { isOpen = true; }
        public void CloseNote() { isOpen = false; }
        public bool GetOpeningStatus() { return isOpen; }
        public NoteTitle GetTitle() { return title; }
        public Content GetContent() { return content; }
    }
}
