using System;
using System.Collections.Generic;
using System.Text;

namespace Email.Templates
{
    internal class ParseableEmailTemplate
    {
        public string UnparsedTemplate { get; private set; }
        public string Name { get; private set; }
        public ParseableEmailTemplate(string unparsedTemplate, string name)
        {
            UnparsedTemplate = unparsedTemplate;
            Name = name;
        }
    }
}
