using System;
using System.Collections.Generic;

namespace DialogusSystemus
{
    class Answer
    {
        private Paragraph text;
        private Message message;

        public Answer(Paragraph newText, Message newMessage)
        {
            text = newText;
            message = newMessage;
        }
    }

    public class Message
    {
        private Paragraph Text;
        private Answer[] questions;
        private Term[] terms;
    }
}
