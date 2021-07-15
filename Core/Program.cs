using System;

namespace DialogusSystemus
{
    class Program
    {
        static void Main()
        {
            var term = new Term();
            term.SetTitle("Painis");
            term.SetTag(Tag.Place);

            Quote jobs = new();
            jobs.SetQuote(
                new Utterance()
                {
                    Paragraphs = new[]
                    {
                        new Paragraph("My pen is #nam{Jobs}.") ,
                        new Paragraph("My Pen can #rwd{Masturbate}!") ,
                    }
                },
                new Paragraph("Evgeniy Jobs")
                );

            Quote dude = new();
            dude.SetQuote(
                new Utterance()
                {
                    Paragraphs = new[]
                    {
                        new Paragraph("#nam{My dad} love #plc{basketball}") ,
                    }
                },
                new Paragraph("Dude Dudda")
                );

            term.AddQuote(jobs, dude);

            Frame.Print(term);
            term.OpenTerm();
            Frame.Print(term);

            Console.Clear();

            var startOfAdventure = new Chapter();
            startOfAdventure.SetTitle("Intro");

            (string Identifier, Note Content) letsStart = ("lets_start", new Note());
            letsStart.Content.SetTitle("Beginning!");
            letsStart.Content.SetContent(
                new Utterance()
                {
                    Paragraphs = new[]
                    {
                        new Paragraph("I need a #rwd{dventure}!"),
                    }
                }
                );

            (string Identifier, Note Content) firstFail = ("first_fail", new Note());
            firstFail.Content.SetTitle("My first Fail!");
            firstFail.Content.SetContent(
                new Utterance()
                {
                    Paragraphs = new[]
                    {
                        new Paragraph("I was sucked some #plc{Cock}!"),
                    }
                }
                );

            startOfAdventure.AddNotes(
                new[]
                {
                    letsStart,
                    firstFail
                });

            var endOfAdventure = new Chapter();
            endOfAdventure.SetTitle("Outro");

            (string Identifier, Note Content) endGame = ("end_game", new Note());
            endGame.Content.SetTitle("U just end The Game");
            endGame.Content.SetContent(
                new Utterance()
                {
                    Paragraphs = new[]
                    {
                        new Paragraph("#nam{I} end the #rwd{Game}..."),
                    }
                }
                );

            endOfAdventure.AddNotes(
                new[]
                {
                    endGame
                });

            AdventureDiary.InitializeDiary(startOfAdventure, endOfAdventure);
            AdventureDiary.OpenNote("lets_start", "end_game", "first_fail");
            AdventureDiary.PrintDiary();
        }
    }
}
