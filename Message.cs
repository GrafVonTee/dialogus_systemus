using System;
using System.Collections.Generic;

namespace DialogusSystemus
{
    class Answer
    {
        private Utterance text;
        private Message message;

        public Answer(Utterance newText, Message newMessage) {
            text = newText;
            message = newMessage;
        }
    }

    public class Message
    {
        private Utterance Text;
        private Answer[] questions;
        private Term[] terms;
    }
}
