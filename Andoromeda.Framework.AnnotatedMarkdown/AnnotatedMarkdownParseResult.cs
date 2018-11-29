using System.Collections.Generic;

namespace Andoromeda.Framework.AnnotatedMarkdown
{
    public class AnnotatedMarkdownParseResult
    {
        public string PlainMarkdown { get; set; }

        public IDictionary<string, string> Annotations { get; set; }
    }
}
