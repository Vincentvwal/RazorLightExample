using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Email.Templates
{
    public interface ITemplateParser
    {
        Task<string> ParseTemplate<T>(T emailModel);
    }
}
