using RazorLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Email.Templates
{
    public class TemplateParser : ITemplateParser
    {
        /// <summary>
        /// Internal path to the templates
        /// </summary>
        private readonly string _templateFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates");

        internal readonly RazorLightEngine razorEngine;

        public TemplateParser()
        {
            razorEngine = new RazorLightEngineBuilder().UseEmbeddedResourcesProject(typeof(IAmAssembly).Assembly).UseMemoryCachingProvider().Build();
            CompileTemplates().GetAwaiter().GetResult();
        }

        public async Task<string> ParseTemplate<T>(T emailModel)
        {
            var template = await razorEngine.CompileTemplateAsync(emailModel.GetType().FullName);
            var content = await razorEngine.RenderTemplateAsync(template, emailModel);

            return content;
        }

        internal async Task CompileTemplates()
        {
            var templates = GetTemplates();
            foreach (var template in templates)
            {
                var typeName = string.Concat("Email.Templates.Models.", template.Name);
                var type = Type.GetType(typeName);
                var instance = Activator.CreateInstance(type);

                await razorEngine.CompileRenderStringAsync(typeName, template.UnparsedTemplate, instance);
            }
        }

        internal ICollection<ParseableEmailTemplate> GetTemplates()
        {
            List<ParseableEmailTemplate> templates = new List<ParseableEmailTemplate>();

            /* If no templates directory exist, return nothing */
            var templateDirectories = new DirectoryInfo(_templateFolderPath);
            if (!templateDirectories.Exists)
                return new List<ParseableEmailTemplate>();

            var files = templateDirectories.GetFiles();
            foreach (var file in files)
            {
                var template = new ParseableEmailTemplate(File.ReadAllText(file.FullName), file.Name.Split('.')[0]);
                templates.Add(template);
            }

            return templates;
        }
    }
}
