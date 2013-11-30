namespace TableOfContents.Model
{
    public class TocSource
    {
        public TocSource()
        {
            MinIndentLevel = 1;
        }

        public string FilePath { get; set; }

        public string Url { get; set; }

        public int MinIndentLevel { get; set; }
    }
}
