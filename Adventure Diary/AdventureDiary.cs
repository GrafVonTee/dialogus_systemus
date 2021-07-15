using System;

using Identifier = System.String;

namespace DialogusSystemus
{
    public static class AdventureDiary
    {
        private static Chapter[] diary = Array.Empty<Chapter>();

        public static void InitializeDiary(params Chapter[] chapters)
        {
            var oldLength = diary.Length;
            Array.Resize(ref diary, diary.Length + chapters.Length);
            for (int i = 0; i < chapters.Length; i++)
                diary[oldLength + i] = chapters[i];
        }

        public static void PrintDiary()
        {
            if (IsDiaryEmpty()) return;

            Frame.PrintSelectionMenu(diary, isChapter: true); 
        }

        private static bool IsDiaryEmpty()
        {
            foreach (var d in diary)
                if (d.GetOpeningStatus())
                    return false;
            return true;
        }

        public static void OpenNote(params Identifier[] noteIdentifier)
        {
            foreach (var id in noteIdentifier)
                foreach (var ch in diary)
                {
                    if (ch.ContentsTheNote(id))
                    {
                        ch.OpenNote(id);
                        break;
                    }
                }
        }
    }
}
