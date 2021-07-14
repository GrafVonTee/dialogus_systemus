using System;

namespace DialogusSystemus
{
    class Program
    {
        static void Main()
        {
            /*
            var term = new Term("Pen", Tag.Place);

            Quote jobs = new();
            jobs.SetQuote( new Utterance(new string[]{
                "My pen is #nam{Jobs}.",
                "My Pen can #rwd{Masturbate}" }
            ), "Evgeniy Jobs");

            Quote dude = new();
            dude.SetQuote(new Utterance(new string[]{
                "#nam{My dad} love #plc{basketball}" }
            ), "Dude Dude");

            term.AddQuote(jobs, dude);

            Frame.Print(term);
            term.OpenTerm();
            Frame.Print(term);
            */

            var startOfAdventure = new Chapter();
            startOfAdventure.SetTitle("Intro");

            (string Identifier, Note Content) letsStart = ("lets_start", new Note());
            letsStart.Content.SetTitle("Beginning!");
            letsStart.Content.SetContent(
                new Utterance
                ( 
                    new string[]
                    {
                        "It is #plc{a time} to #rwd{begin}!"
                    }
                )
            );

            (string Identifier, Note Content) firstFail = ("first_fail", new Note());
            firstFail.Content.SetTitle("My first Fail!");
            firstFail.Content.SetContent(
                new Utterance(
                    new string[]
                    {
                        "It is #plc{a time} to #rwd{suck}..."
                    }));

            startOfAdventure.AddNotes(
                new []
                {
                    letsStart,
                    firstFail
                });

            var endOfAdventure = new Chapter();
            endOfAdventure.SetTitle("Outro");

            (string Identifier, Note Content) endGame = ("end_game", new Note());
            endGame.Content.SetTitle("U just end The Game");
            endGame.Content.SetContent(
                new Utterance(
                    new string[]
                    {
                        "#nam{I} end the #rwd{Game}..."
                    }));

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
