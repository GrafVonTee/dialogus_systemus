using System;
using System.Collections.Generic;
using Identifier = System.String;

namespace DialogusSystemus
{
    public static class TermDiary
    {
        private static Category[] termDiary = new Category[0];

        public static void InitializeDiary(params Category[] categories)
        {
            var oldLength = termDiary.Length;
            Array.Resize(ref termDiary, termDiary.Length + categories.Length);
            for (int i = 0; i < categories.Length; i++)
                termDiary[oldLength + i] = categories[i];
        }

        public static void PrintDiary()
        {
            if (IsDiaryEmpty()) return;

            Frame.PrintSelectionMenu(termDiary, new Paragraph("Termus Diary!"), isHead: true);
        }

        private static bool IsDiaryEmpty()
        {
            foreach (var cat in termDiary)
                if (cat.GetOpeningStatus())
                    return false;
            return true;
        }

        public static void OpenTerm(params Identifier[] termIdent)
        {
            foreach (var id in termIdent)
                foreach (var cat in termDiary)
                {
                    if (cat.ContentsTheTerm(id))
                    {
                        cat.OpenTerm(id);
                        break;
                    }
                }
        }

    }
}