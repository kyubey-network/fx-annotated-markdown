using System.Collections.Generic;
using System.Linq;

namespace Andoromeda.Framework.AnnotatedMarkdown
{
    public static class AnnotationParser
    {
        public static AnnotatedMarkdownParseResult Parse(string src)
        {
            var splited = src.Split('\n')
                             .Select(x => x.TrimEnd('\r'));
            var annotationLines = splited.Where(x => x.StartsWith("@ ") && x.Contains(":"));
            var dic = new Dictionary<string, string>();
            foreach(var x in annotationLines)
            {
                try
                {
                    var colonPosition = x.IndexOf(':');
                    var key = x.Substring(0, colonPosition);
                    key = key.Substring(2);
                    var value = colonPosition + 1 < x.Length ? x.Substring(colonPosition + 1) : null;
                    if (dic.ContainsKey(key)) 
                    {
                        dic.Remove(key);
                    }
                    dic.Add(key, value);
                }
                catch { }
            }

            var plain = splited.Where(x => !x.StartsWith("@ ")).ToList();
            var emptyLineCount = 0;
            for (var i = 0; i < plain.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(plain[i]))
                {
                    ++emptyLineCount;
                }
                else
                {
                    break;
                }
            }
            var markdown = string.Join("\r\n", plain.Skip(emptyLineCount).ToArray());
            return new AnnotatedMarkdownParseResult
            {
                Annotations = dic,
                PlainMarkdown = markdown
            };
        }
    }
}
