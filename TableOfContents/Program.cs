using TableOfContents.Model;

namespace TableOfContents
{
    class Program
    {
        private static TocInject[] tocConversions = new[] {
            new TocInject
            {
                Sources = new[]
                {
                    new TocSource
                    {
                        FilePath = @"C:\Users\Adrian\Documents\My Dropbox\Job\Job Leads\4_Silk\RegexParser\ImplementedRegexFeatures.md",
                        Url = @"/Doc/ImplementedRegexFeatures.md",
                    },
                    new TocSource
                    {
                        FilePath = @"C:\Users\Adrian\Documents\My Dropbox\Job\Job Leads\4_Silk\RegexParser\HowRegexParserWorks.md",
                        Url = @"/Doc/HowRegexParserWorks.md",
                    },
                },
                Destination = @"C:\Users\Adrian\Documents\My Dropbox\Job\Job Leads\4_Silk\RegexParser\README.md",
            },

            new TocInject
            {
                Sources = new[]
                {
                    new TocSource
                    {
                        FilePath = @"C:\Users\Adrian\Documents\My Dropbox\Job\Job Leads\4_Silk\RegexParser\HowRegexParserWorks.md",
                        Url = @"",
                        MinIndentLevel = 2,
                    },
                },
                Destination = @"C:\Users\Adrian\Documents\My Dropbox\Job\Job Leads\4_Silk\RegexParser\HowRegexParserWorks.md",
            },
        };

        static void Main(string[] args)
        {
            TocGenerator.Generate(tocConversions);
        }
    }
}
