using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TableOfContents.Model;

namespace TableOfContents
{
    public static class TocGenerator
    {
        private static readonly Regex headingRegex = new Regex(@"^(#+)\s*(.+?)\s*#*$");
        private static readonly Regex nonWordCharRegex = new Regex(@"[_\W-[\s]]");
        private static readonly Regex tocTagRegex = new Regex(@"(<toc[^>]*>).*?(</toc>)",
                                                              RegexOptions.Singleline | RegexOptions.IgnoreCase);

        public static void Generate(IEnumerable<TocInject> tocInjects)
        {
            try
            {
                foreach (TocInject tocInject in tocInjects)
                {
                    List<TocHeading> headings = new List<TocHeading>();

                    foreach (TocSource source in tocInject.Sources)
                    {
                        foreach (string line in readLines(source.FilePath))
                        {
                            Match match = headingRegex.Match(line);

                            if (match.Success)
                            {
                                TocHeading heading = new TocHeading
                                {
                                    IndentLevel = match.Groups[1].Length,
                                    Text = match.Groups[2].Value,
                                    Url = source.Url,
                                    HashTag = generateHashTag(match.Groups[2].Value),
                                };

                                headings.Add(heading);
                            }
                        }
                    }

                    StringBuilder sb = new StringBuilder();

                    foreach (TocHeading heading in headings)
                    {
                        sb.AppendFormat("{0}- [{1}]({2}#{3})\n",
                                        new string(' ', 4 * (heading.IndentLevel - 1)),
                                        heading.Text,
                                        heading.Url,
                                        heading.HashTag);
                    }

                    injectIntoFile(tocInject.Destination, sb.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: {0}", ex.Message);
            }
        }

        private static IEnumerable<string> readLines(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);

            using (StreamReader sr = fi.OpenText())
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        private static void injectIntoFile(string filePath, string toc)
        {
            FileInfo fi = new FileInfo(filePath);

            string fileContent = "";
            using (StreamReader sr = fi.OpenText())
            {
                fileContent = sr.ReadToEnd();
            }

            fileContent = tocTagRegex.Replace(fileContent,
                                              string.Format("$1\n\n{0}\n$2", toc));

            using (StreamWriter sw = fi.CreateText())
            {
                sw.Write(fileContent);
            }
        }

        private static string generateHashTag(string text)
        {
            return nonWordCharRegex.Replace(text, "")
                                   .Replace(' ', '-')
                                   .ToLower();
        }
    }
}
