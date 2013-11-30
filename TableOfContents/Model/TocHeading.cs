namespace TableOfContents.Model
{
    public class TocHeading
    {
        public int IndentLevel { get; set; }

        public string Text { get; set; }

        public string Url { get; set; }
        public string HashTag { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: [{1}](#{2})", IndentLevel, Text, HashTag);
        }
    }
}
