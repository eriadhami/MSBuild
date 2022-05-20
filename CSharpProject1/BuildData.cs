using System;

namespace CSharpProject1
{
    public class BuildData
    {
        public DateTimeOffset BuildDate { get; set; }
        public string GitHash { get; set; }
        public string GitBranch { get; set; }
    }
}