using System;
using System.Collections.Generic;

using Identifier = System.String;

namespace DialogusSystemus
{
    public class Chapter
    {
        private string title;
        private Dictionary<Identifier, Note> notes = new();
        private bool isOpen = false;

        public void SetChapter(string newTitle, (Identifier, Note)[] newNotes)
        {
            SetTitle(newTitle);
            AddNotes(newNotes);
        }
        public void SetTitle(string newTitle) { title = newTitle; }
        public void AddNotes((Identifier Identifier, Note Content)[] newNotes)
        {
            foreach (var n in newNotes)
                notes[n.Identifier] = n.Content;
        }

        public string GetTitle() { return title; }
        public Note[] GetNotes()
        {
            var notesArray = new Note[notes.Values.Count];
            notes.Values.CopyTo(notesArray, 0);
            return notesArray;
        }
        public bool GetOpeningStatus() { return isOpen; }
        public void OpenNote(Identifier identifier) {
            notes[identifier].OpenNote();
            isOpen = true;
        }

        public bool ContentsTheNote(Identifier identifier)
        {
            return notes.ContainsKey(identifier);
        }
    }
}
