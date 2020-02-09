using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using ApprovalTests.Core;
using ApprovalUtilities.Utilities;

namespace ApprovalTests.Reporters
{
    public class GenericDiffReporter : IEnvironmentAwareReporter
    {
        public const string DEFAULT_ARGUMENT_FORMAT = "{0} {1}";

        public static IEnumerable<string> GetTextAndImageFileTypes()
        {
            var all = new HashSet<string>();
            all.AddAll(GetTextFileTypes());
            all.AddAll(GetImageFileTypes());
            return all;
        }

        private static readonly HashSet<string> TEXT_FILE_TYPES = new HashSet<string>
        {
            // begin-snippet: text_file_types
            "ada",
            "adb",
            "ads",
            "applescript",
            "au3",
            "as",
            "asc",
            "ascx",
            "ascii",
            "asm",
            "asmx",
            "asp",
            "aspx",
            "atom",
            "awk",
            "bas",
            "bash",
            "bashrc",
            "bat",
            "bbcolors",
            "bcp",
            "bdsgroup",
            "bdsproj",
            "bib",
            "bowerrc",
            "c",
            "cbl",
            "cc",
            "cfc",
            "cfg",
            "cfm",
            "cfml",
            "cgi",
            "clj",
            "cljs",
            "cls",
            "cmake",
            "cmd",
            "cnf",
            "cob",
            "code-snippets",
            "coffee",
            "coffeekup",
            "conf",
            "cp",
            "cpp",
            "cpt",
            "cpy",
            "crt",
            "cs",
            "csh",
            "cson",
            "csproj",
            "csr",
            "css",
            "csslintrc",
            "csv",
            "ctl",
            "curlrc",
            "cxx",
            "d",
            "dart",
            "dfm",
            "diff",
            "dof",
            "dpk",
            "dpr",
            "dproj",
            "dtd",
            "eco",
            "editorconfig",
            "ejs",
            "el",
            "elm",
            "emacs",
            "eml",
            "ent",
            "erb",
            "erl",
            "eslintignore",
            "eslintrc",
            "ex",
            "exs",
            "f",
            "f03",
            "f77",
            "f90",
            "f95",
            "fish",
            "for",
            "fpp",
            "frm",
            "fs",
            "fsproj",
            "fsx",
            "ftn",
            "gemrc",
            "gemspec",
            "gitattributes",
            "gitconfig",
            "gitignore",
            "gitkeep",
            "gitmodules",
            "go",
            "gpp",
            "gradle",
            "groovy",
            "groupproj",
            "grunit",
            "gtmpl",
            "gvimrc",
            "h",
            "haml",
            "hbs",
            "hgignore",
            "hh",
            "hrl",
            "hpp",
            "hs",
            "hta",
            "htaccess",
            "htc",
            "htm",
            "html",
            "htpasswd",
            "hxx",
            "iced",
            "iml",
            "inc",
            "ini",
            "ino",
            "int",
            "irbrc",
            "itcl",
            "itermcolors",
            "itk",
            "jade",
            "java",
            "jhtm",
            "jhtml",
            "js",
            "jscsrc",
            "jshintignore",
            "jshintrc",
            "json",
            "json5",
            "jsonld",
            "jsp",
            "jspx",
            "jsx",
            "ksh",
            "less",
            "lhs",
            "lisp",
            "log",
            "ls",
            "lsp",
            "lua",
            "m",
            "m4",
            "mak",
            "map",
            "markdown",
            "master",
            "md",
            "mdown",
            "mdwn",
            "mdx",
            "metadata",
            "mht",
            "mhtml",
            "mjs",
            "mk",
            "mkd",
            "mkdn",
            "mkdown",
            "ml",
            "mli",
            "mm",
            "mxml",
            "nfm",
            "nfo",
            "noon",
            "npmignore",
            "npmrc",
            "nuspec",
            "nvmrc",
            "ops",
            "pas",
            "pasm",
            "patch",
            "pbxproj",
            "pch",
            "pem",
            "pg",
            "php",
            "php3",
            "php4",
            "php5",
            "phpt",
            "phtml",
            "pir",
            "pl",
            "pm",
            "pmc",
            "pod",
            "pot",
            "prettierrc",
            "properties",
            "props",
            "pt",
            "pug",
            "purs",
            "py",
            "pyx",
            "r",
            "rake",
            "rb",
            "rbw",
            "rc",
            "rdoc",
            "rdoc_options",
            "resx",
            "rexx",
            "rhtml",
            "rjs",
            "rlib",
            "ron",
            "rs",
            "rss",
            "rst",
            "rtf",
            "rvmrc",
            "rxml",
            "s",
            "sass",
            "scala",
            "scm",
            "scss",
            "seestyle",
            "sh",
            "shtml",
            "sln",
            "sls",
            "spec",
            "sql",
            "sqlite",
            "sqlproj",
            "ss",
            "sss",
            "st",
            "strings",
            "sty",
            "styl",
            "stylus",
            "sub",
            "sublime-build",
            "sublime-commands",
            "sublime-completions",
            "sublime-keymap",
            "sublime-macro",
            "sublime-menu",
            "sublime-project",
            "sublime-settings",
            "sublime-workspace",
            "sv",
            "svc",
            "svg",
            "swift",
            "t",
            "tcl",
            "tcsh",
            "terminal",
            "tex",
            "text",
            "textile",
            "tg",
            "tk",
            "tmLanguage",
            "tmTheme",
            "tmpl",
            "tpl",
            "ts",
            "tsv",
            "tsx",
            "tt",
            "tt2",
            "ttml",
            "twig",
            "txt",
            "text",
            "v",
            "vb",
            "vbproj",
            "vbs",
            "vcproj",
            "vcxproj",
            "vh",
            "vhd",
            "vhdl",
            "vim",
            "viminfo",
            "vimrc",
            "vm",
            "vue",
            "webapp",
            "x-php",
            "wsc",
            "xaml",
            "xht",
            "xhtml",
            "xml",
            "xs",
            "xsd",
            "xsl",
            "xslt",
            "y",
            "yaml",
            "yml",
            "zsh",
            "zshrc"
            // end-snippet
        };

        private static readonly HashSet<string> IMAGE_FILE_TYPES = new HashSet<string>
        {
            // begin-snippet: image_file_types
            ".png",
            ".gif",
            ".jpg",
            ".jpeg",
            ".bmp",
            ".tif",
            ".tiff"
            // end-snippet
        };

        protected string arguments;
        protected string originalDiffProgram;
        protected string actualDiffProgram;
        protected string diffProgramNotFoundMessage;
        protected Func<IEnumerable<string>> fileTypes = GetTextFileTypes;

        public static HashSet<string> GetTextFileTypes()
        {
            return TEXT_FILE_TYPES;
        }


        public static HashSet<string> GetImageFileTypes()
        {
            return IMAGE_FILE_TYPES;
        }

        public static void RegisterTextFileTypes(params string[] extensionsWithDots)
        {
            AssertDots(extensionsWithDots);
            TEXT_FILE_TYPES.AddAll(extensionsWithDots);
        }

        public static void RegisterImageFileTypes(params string[] extensionsWithDots)
        {
            AssertDots(extensionsWithDots);
            IMAGE_FILE_TYPES.AddAll(extensionsWithDots);
        }

        private static void AssertDots(string[] extensionsWithDots)
        {
            var wrong = extensionsWithDots.Where(s => !s.StartsWith(".")).ToList();
            if (wrong.Any())
            {
                throw new ArgumentException($"The following extensions don't start with dots: {wrong.ToReadableString()}");
            }
        }

        public GenericDiffReporter(string diffProgram)
            : this(diffProgram, DEFAULT_ARGUMENT_FORMAT, $"Couldn't find: {diffProgram}")
        {
        }

        public GenericDiffReporter(string diffProgram, string diffProgramNotFoundMessage)
            : this(diffProgram, DEFAULT_ARGUMENT_FORMAT, diffProgramNotFoundMessage)
        {
        }

        public GenericDiffReporter(string diffProgram, string argumentsFormat, string diffProgramNotFoundMessage)
            : this(diffProgram, argumentsFormat, diffProgramNotFoundMessage, GetTextFileTypes)
        {
        }

        public GenericDiffReporter(string diffProgram, string argumentsFormat, string diffProgramNotFoundMessage,
            Func<IEnumerable<string>> allowedFileTypes)
        {
            if (diffProgram == null)
            {
                throw new NullReferenceException(
                    string.Format(@"Illegal arguments for {0} (diffProgram, argumentsFormat, diffProgramNotFoundMessage)
Received {0} ({1}, {2}, {3})", GetType().Name, diffProgram, argumentsFormat, diffProgramNotFoundMessage));
            }

            originalDiffProgram = diffProgram;
            arguments = argumentsFormat;
            this.diffProgramNotFoundMessage = diffProgramNotFoundMessage;
            fileTypes = allowedFileTypes;
        }

        protected GenericDiffReporter(DiffInfo info) : this(info.DiffProgram, info.Parameters, $"Unable to find program at {info.DiffProgram}", info.FileTypes)
        {
        }

        public static string GetActualProgramFile(string fullPath)
        {
            if (File.Exists(fullPath))
            {
                return fullPath;
            }

            var toFind = Path.GetFileName(fullPath);
            var output = PathUtilities.LocateFileFromEnvironmentPath(toFind).FirstOrDefault();
            return string.IsNullOrEmpty(output) ? fullPath : output;
        }

        public string GetDiffProgram()
        {
            return actualDiffProgram ??= GetActualProgramFile(originalDiffProgram);
        }

        public virtual void Report(string approved, string received)
        {
            if (!File.Exists(GetDiffProgram()))
            {
                throw new Exception(diffProgramNotFoundMessage);
            }

            EnsureFileExists(approved);
            Launch(GetLaunchArguments(approved, received));
        }

        public static void EnsureFileExists(string approved)
        {
            if (!File.Exists(approved))
            {
                var fileType = new FileInfo(approved).Extension;
                if (IMAGE_FILE_TYPES.Contains(fileType))
                {
                    using var bitmap = new Bitmap(1, 1);
                    bitmap.SetResolution(96, 96);
                    bitmap.Save(approved);
                }
                else
                {
                    File.WriteAllText(approved, " ", Encoding.UTF8);
                }

                ReporterEvents.CreatedApprovedFile(approved);
            }
        }

        public virtual bool IsWorkingInThisEnvironment(string forFile)
        {
            return File.Exists(GetDiffProgram()) && IsValidFileType(forFile);
        }

        public bool IsValidFileType(string forFile)
        {
            return IsFileOneOf(forFile, fileTypes());
        }

        public LaunchArgs GetLaunchArguments(string approved, string received)
        {
            return new LaunchArgs(GetDiffProgram(), string.Format(arguments, new[] {WrapPath(received), WrapPath(approved)}));
        }

        private string WrapPath(string path)
        {
            return '"' + path + '"';
        }

        public static bool IsTextFile(string forFile)
        {
            return IsFileOneOf(forFile, GetTextFileTypes());
        }

        public static bool IsFileOneOf(string forFile, IEnumerable<string> fileTypes)
        {
            return fileTypes.Any(forFile.EndsWith);
        }

        [ObsoleteEx(
            RemoveInVersion = "5.0",
            ReplacementTypeOrMember = nameof(Launch))]
        public static void LaunchAsync(LaunchArgs launchArgs)
        {
             Launch(launchArgs);
        }

        public static void Launch(LaunchArgs launchArgs)
        {
            try
            {
                Process.Start(launchArgs.Program, launchArgs.Arguments);
            }
            catch (Win32Exception e)
            {
                throw new Exception($"Unable to launch: {launchArgs.Program} with arguments {launchArgs.Arguments}\nError Message: {e.Message}", e);
            }
        }
    }
}