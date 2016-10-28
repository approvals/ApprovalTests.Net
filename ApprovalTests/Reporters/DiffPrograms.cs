using System;
using System.Collections.Generic;

namespace ApprovalTests.Reporters
{
    public class DiffPrograms
    {
        private static Func<IEnumerable<string>> TEXT           = GenericDiffReporter.GetTextFileTypes;
        private static Func<IEnumerable<string>> IMAGE          = GenericDiffReporter.GetImageFileTypes;
        private static Func<IEnumerable<string>> TEXT_AND_IMAGE = null;
        public static class Mac
        {
            public static DiffInfo DIFF_MERGE = new DiffInfo("/Applications/DiffMerge.app/Contents/MacOS/DiffMerge",
                "{0} {1}--nosplash", TEXT);
            public static DiffInfo BEYOND_COMPARE = new DiffInfo("/Applications/Beyond Compare.app/Contents/MacOS/bcomp",
                TEXT);
            public static DiffInfo KALEIDOSCOPE = new DiffInfo("/Applications/Kaleidoscope.app/Contents/MacOS/ksdiff",
                TEXT_AND_IMAGE);
            public static DiffInfo KDIFF3 = new DiffInfo("/Applications/kdiff3.app/Contents/MacOS/kdiff3",
                "{0} {1}-m", TEXT);
            public static DiffInfo P4MERGE = new DiffInfo("/Applications/p4merge.app/Contents/MacOS/p4merge",
                TEXT_AND_IMAGE);
            public static DiffInfo TK_DIFF = new DiffInfo("/Applications/TkDiff.app/Contents/MacOS/tkdiff", TEXT);
        }
        public static class Windows
        {
            public static DiffInfo BEYOND_COMPARE_3 = new DiffInfo("{ProgramFiles}Beyond Compare 3\\BCompare.exe",
                TEXT_AND_IMAGE);
            public static DiffInfo BEYOND_COMPARE_4 = new DiffInfo("{ProgramFiles}Beyond Compare 4\\BCompare.exe",
             TEXT_AND_IMAGE);
            public static DiffInfo TORTOISE_IMAGE_DIFF = new DiffInfo(
                "{ProgramFiles}TortoiseSVN\\bin\\TortoiseIDiff.exe", "/left:{0} /right:{1}", IMAGE);
            public static DiffInfo TORTOISE_TEXT_DIFF = new DiffInfo(
                "{ProgramFiles}TortoiseSVN\\bin\\TortoiseMerge.exe", TEXT);

            public static DiffInfo TORTOISEGIT_TEXT_DIFF = new DiffInfo(
                "{ProgramFiles}TortoiseGit\\bin\\TortoiseGitMerge.exe", TEXT);

            public static DiffInfo WIN_MERGE = new DiffInfo("{ProgramFiles}WinMerge\\WinMergeU.exe", TEXT);
            public static DiffInfo ARAXIS_MERGE = new DiffInfo("{ProgramFiles}Araxis\\Araxis Merge\\Compare.exe", TEXT);
            public static DiffInfo CODE_COMPARE = new DiffInfo("{ProgramFiles}Devart\\Code Compare\\CodeCompare.exe",
                "/ENVIRONMENT=visualstudio {0} {1}", TEXT);
            public static DiffInfo KDIFF3 = new DiffInfo("{ProgramFiles}KDiff3\\kdiff3.exe",
                "{0} {1} -m -o {1}", TEXT);
            public static DiffInfo P4MERGE_TEXT = new DiffInfo("{ProgramFiles}Perforce\\p4merge.exe",
                "{1} {0} {1} {1}", TEXT);
            public static DiffInfo P4MERGE_IMAGE = new DiffInfo("{ProgramFiles}Perforce\\p4merge.exe",
                "{0} {1}", IMAGE);
        }
    }
}