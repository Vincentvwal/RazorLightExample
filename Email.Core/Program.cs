using Email.Templates;
using Email.Templates.Models;
using System;

namespace Email.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            ITemplateParser _templateParser = new TemplateParser();

            var model = new HelloWorld();
            model.Name = "Vincent";
            model.Date = DateTime.Now;

            var htmlContent = _templateParser.ParseTemplate(model).GetAwaiter().GetResult();

            Console.WriteLine(htmlContent);
            Console.ReadKey();
        }
    }
}
