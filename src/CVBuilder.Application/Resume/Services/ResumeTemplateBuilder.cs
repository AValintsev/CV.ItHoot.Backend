using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using CVBuilder.Application.Resume.Responses.CvResponse;

namespace CVBuilder.Application.Resume.Services;

public interface IConfigProperty
{
    public string PropertyName { get; }
    public string ConfigureValue(object property);
}

public interface ICustomRender
{
    string SectionId { get; }
    string TemplateId { get; }
    void Render(IElement ul, List<Dictionary<string, string>> list);
}

public class CustomRenderLevelsTwo : ICustomRender
{
    public string SectionId { get; }
    public string TemplateId => "template-two";
    private readonly IDocument _document;

    public CustomRenderLevelsTwo(string sectionId, IDocument document)
    {
        SectionId = sectionId;
        _document = document;
    }

    public void Render(IElement ul, List<Dictionary<string, string>> list)
    {
        var templateLi = ul?.Children.FirstOrDefault(x => x.TagName == "LI");

        if (templateLi == null)
        {
            return;
        }

        if (ul!.ClassList.Contains("template-two"))
        {
            foreach (var item in list)
            {
                var newLi = templateLi.Clone();
                foreach (var val in item)
                {
                    var liContent = newLi.ChildNodes.GetElementsByClassName(val.Key).FirstOrDefault();

                    if (val.Key == "level")
                    {
                        var listLevelElements = new List<INode>();
                        for (var i = 1; i < 6; i++)
                        {
                            var levelHtml = _document.CreateElement("div");
                            levelHtml.SetAttribute("style",
                                i <= int.Parse(val.Value) ? "background: #fe8503" : "background: #ddd");

                            listLevelElements.Add(levelHtml);
                        }

                        liContent?.AppendNodes(listLevelElements.ToArray());
                    }
                    else if (liContent != null)
                    {
                        liContent.TextContent = val.Value;
                    }
                }

                ul.AppendChild(newLi);
            }

            ul.RemoveChild(templateLi);
        }
    }
}

public class CustomRenderLevelsOne : ICustomRender
{
    public string SectionId { get; }
    public string TemplateId { get; } = "template-one";
    private readonly IDocument _document;

    public CustomRenderLevelsOne(string sectionId, IDocument document)
    {
        SectionId = sectionId;
        _document = document;
    }

    public void Render(IElement ul, List<Dictionary<string, string>> list)
    {
        var templateLi = ul?.Children.FirstOrDefault(x => x.TagName == "LI");

        if (templateLi == null)
            return;

        if (ul!.ClassList.Contains(TemplateId))
        {
            foreach (var item in list)
            {
                var newLi = templateLi.Clone();
                foreach (var val in item)
                {
                    var liContent = newLi.ChildNodes.GetElementsByClassName(val.Key).FirstOrDefault();

                    if (liContent == null)
                        continue;

                    if (val.Key == "level")
                    {
                        var path = liContent.Children.FirstOrDefault(x =>
                            x.TagName == "path" && x.ClassList!.Contains("circle"));

                        if (path == null)
                            continue;

                        path.Attributes.SetNamedItem(new Attr("stroke-dasharray", $"{int.Parse(val.Value) * 20},100"));
                    }
                    else
                    {
                        liContent.TextContent = val.Value;
                    }
                }

                ul.AppendChild(newLi);
            }

            ul.RemoveChild(templateLi);
        }
    }
}

public class BirthdateConfig : IConfigProperty
{
    public string PropertyName => "birthdate";

    public string ConfigureValue(object property)
    {
        var age = 0;
        if (property is DateTime birthdate)
        {
            var today = DateTime.Today;
            age = today.Year - birthdate.Year;
            if (birthdate.Date > today.AddYears(-age)) age--;
        }

        return $"{age} years";
    }
}

public class DateConfig : IConfigProperty
{
    public string PropertyName { get; }

    public DateConfig(string propertyName)
    {
        PropertyName = propertyName;
    }

    public string ConfigureValue(object property)
    {
        if (property is DateTime birthdate)
        {
            var date = birthdate.ToString("MM/yyyy");
            return date;
        }
        else
        {
            throw new ArgumentException("Invalid type");
        }
    }
}

public class ResumeTemplateBuilder
{
    private readonly string _html;
    private IDocument _resumeHtml;
    private IHtmlCollection<IElement> _body;
    private readonly IEnumerable<IConfigProperty> _configProperties;
    private IEnumerable<ICustomRender> _customRenders;

    public ResumeTemplateBuilder(string html)
    {
        _html = html;

        _configProperties = new List<IConfigProperty>()
        {
            new BirthdateConfig(),
            new DateConfig("startDate"),
            new DateConfig("endDate")
        };
    }

    public async Task<string> BindTemplateAsync(ResumeResult resume)
    {
        var parser = new HtmlParser();
        _resumeHtml = await parser.ParseDocumentAsync(_html);
        _body = _resumeHtml.All;
        _customRenders = new List<ICustomRender>
        {
            new CustomRenderLevelsOne("languages", _resumeHtml),
            new CustomRenderLevelsOne("skills", _resumeHtml),
            new CustomRenderLevelsTwo("languages", _resumeHtml),
            new CustomRenderLevelsTwo("skills", _resumeHtml)
        };

        MapResume(resume);

        return _resumeHtml.ToHtml();
    }

    private void MapResume(ResumeResult resume)
    {
        var dictionary = GetFieldsWithValues(resume, "picture", "position");
        foreach (var value in dictionary)
        {
            MapResumeValue(value.Key, value.Value);
        }

        MapPicture(resume);
        MapResumeLists(resume);
    }


    private void MapPicture(ResumeResult resume)
    {
        var picture = _body.FirstOrDefault(x => x.ClassName == "picture");
        if (resume.Picture != null)
        {
            picture?.Attributes.SetNamedItem(new Attr("src", resume.Picture));
        }
    }

    private void MapResumeLists(ResumeResult resume)
    {
        var fields = typeof(ResumeResult).GetProperties();
        foreach (var field in fields)
        {
            var fieldType = field.PropertyType;
            if (fieldType.IsGenericType && typeof(List<>) == fieldType.GetGenericTypeDefinition())
            {
                var listName = ToCamelCase(field.Name);
                var list = field.GetValue(resume) as IEnumerable;
                var values = new List<Dictionary<string, string>>();
                foreach (var val in list!)
                {
                    var nameAndValues = new Dictionary<string, string>();
                    foreach (var property in val.GetType().GetProperties())
                    {
                        var propertyName = ToCamelCase(property.Name);
                        var propertyValue = property.GetValue(val, null);

                        var config = _configProperties.FirstOrDefault(x => x.PropertyName == propertyName);

                        if (config != null)
                        {
                            propertyValue = config.ConfigureValue(propertyValue);
                        }

                        nameAndValues.Add(propertyName, propertyValue?.ToString());
                    }

                    values.Add(nameAndValues);
                }

                BindList(listName, values);
            }
        }
    }

    private void BindList(string sectionId, List<Dictionary<string, string>> list)
    {
        var ul = _body.FirstOrDefault(x => x.Id == sectionId);
        var templateLi = ul?.Children.FirstOrDefault(x => x.TagName == "LI");

        if (ul == null || templateLi == null)
            return;


        if (ul.ClassList.Contains("default"))
        {
            var renders = _customRenders.Where(x => x.SectionId == sectionId);
            foreach (var render in renders)
            {
                if (ul.ClassList.Contains(render.TemplateId))
                {
                    render.Render(ul, list);
                }
            }

            return;
        }

        foreach (var item in list)
        {
            var newLi = templateLi!.Clone();
            foreach (var val in item)
            {
                var liContent = newLi.ChildNodes.GetElementsByClassName(val.Key).FirstOrDefault();
                if (liContent != null)
                {
                    liContent.TextContent = val.Value;
                }
            }

            ul.AppendChild(newLi);
        }

        ul.RemoveChild(templateLi);
    }

    private void MapResumeValue(string selector, string value)
    {
        if (value == null)
            return;
        var values = _body.Where(x => x.ClassList.Contains(selector));
        foreach (var val in values)
        {
            val.TextContent = value;
        }
    }

    private Dictionary<string, string> GetFieldsWithValues(ResumeResult resume,
        params string[] excludeProperties)
    {
        var fields = typeof(ResumeResult).GetProperties();
        var dictionary = new Dictionary<string, string>();
        foreach (var field in fields)
        {
            var fieldType = field.PropertyType;

            if (fieldType.IsGenericType && typeof(List<>) == fieldType.GetGenericTypeDefinition())
                continue;

            if (excludeProperties != null && excludeProperties.Contains(field.Name.ToLowerInvariant()))
                continue;

            var propertyName = field.Name;
            propertyName = char.ToLowerInvariant(propertyName[0]) + propertyName[1..];

            var propertyValue = field.GetValue(resume);

            var config = _configProperties.FirstOrDefault(x => x.PropertyName == propertyName);

            if (config != null)
            {
                propertyValue = config.ConfigureValue(propertyValue);
            }

            dictionary.Add(propertyName, propertyValue?.ToString());
        }

        return dictionary;
    }

    private static string ToCamelCase(string str)
    {
        return char.ToLowerInvariant(str[0]) + str[1..];
    }
}