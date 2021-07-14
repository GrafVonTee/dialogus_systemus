using System;
using System.Collections.Generic;
using System.Text;

namespace DialogusSystemus
{
    public class Note
    {
        private string title;
        private Utterance content;
        private bool isOpen = false;

        public void SetTitle(string newTitle) { title = newTitle; }
        public void SetContent(Utterance newContent) { content = newContent; }

        public void OpenNote() { isOpen = true; }
        public void CloseNote() { isOpen = false; }
        public bool GetOpeningStatus() { return isOpen; }
        public string GetTitle() { return title; }
        public Utterance GetContent() { return content; }
    }
}
