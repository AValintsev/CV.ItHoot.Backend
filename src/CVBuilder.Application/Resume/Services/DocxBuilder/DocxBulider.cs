using CVBuilder.Application.Resume.Responses.CvResponse;
using CVBuilder.Application.Resume.Services.Interfaces;
using CVBuilder.Application.Resume.Services.ResumeBuilder.ClassFiledParser;
using CVBuilder.Application.Resume.Services.ResumeBuilder.ResumeFiledParser.Interfaces;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Hosting;
using OpenXmlPowerTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TemplateEngine.Docx;

namespace CVBuilder.Application.Resume.Services.DocxBuilder
{
    public class DocxBuilder : IDocxBuilder
    {
        private readonly IClassFieldParser<ResumeResult> _classParser;
        private Stream _workDocument;
        private readonly string _tempWorkDocPath;
        IWebHostEnvironment _webHostEnvironment;

        private Content _valuesToFill;

        public DocxBuilder(IWebHostEnvironment webHostEnvironment)
        {
            _classParser = new ResumeFiledParser();
            _valuesToFill = new Content();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var rnd = new Random();
            var rndString = new string(Enumerable.Range(1, 10).Select(_ => chars[rnd.Next(chars.Length)]).ToArray());

            _webHostEnvironment = webHostEnvironment;
            _tempWorkDocPath = Path.Combine(_webHostEnvironment.ContentRootPath, $"Shared\\docx\\temp\\{rndString}.docx");
        }

        public async Task<Stream> BindTemplateAsync(ResumeResult resume, byte[] template, bool isShowLogoFooter = false)
        {
            File.WriteAllBytes(_tempWorkDocPath, template);

            await MapResumeAsync(resume, isShowLogoFooter);

            SaveChangesToFile();

            var dataBytes = File.ReadAllBytes(_tempWorkDocPath);
            _workDocument = new MemoryStream(dataBytes);

            File.Delete(_tempWorkDocPath);

            return _workDocument;
        }

        public async Task<Stream> BindTemplateAsync(ResumeResult resume, string templatePath, bool isShowLogoFooter = false)
        {
            byte[] byteArray = File.ReadAllBytes(templatePath);

            return await BindTemplateAsync(resume, byteArray, isShowLogoFooter);
        }

        private async Task MapResumeAsync(ResumeResult resume, bool isShowLogoFooter = false)
        {
            if (isShowLogoFooter)
            {
                AddFooterAndHeader();
            }
            MapResumeValues(resume);
            await MapPictureAsync(resume);
            MapListValues(resume);
            MapDiagrams(resume);

        }

        private void MapResumeValues(ResumeResult resume)
        {
            var dictionary = _classParser.GetFieldsWithValues(resume, "picture", "position");
            foreach (var value in dictionary)
            {
                var fillValue = new FieldContent(value.Key, value.Value ?? string.Empty);
                _valuesToFill.Fields.Add(fillValue);
            }
        }

        private async Task MapPictureAsync(ResumeResult resume)
        {
            if (resume.Picture != null)
            {
                var myClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true });
                var response = await myClient.GetAsync(resume.Picture);
                var pictureBytes = await response.Content.ReadAsByteArrayAsync();
                var image = new ImageContent("photo", pictureBytes);
                _valuesToFill.Images.Add(image);
            }
        }

        private void MapListValues(ResumeResult resume)
        {
            var lists = _classParser.GetListFieldsWithValues(resume);

            foreach (var item in lists)
            {
                if (item.ListValues.Count > 0)
                {
                    var block = new RepeatContent(item.ListName);
                    foreach (var blockItem in item.ListValues)
                    {
                        var blockItems = new List<IContentItem>();
                        foreach (var val in blockItem)
                        {
                            if (val.Key == "id") { continue; }
                            blockItems.Add(new FieldContent(val.Key, val.Value ?? string.Empty));
                        }
                        block.AddItem(blockItems.ToArray());
                    }
                    _valuesToFill.Repeats.Add(block);
                }
            }
        }

        private void MapDiagrams(ResumeResult resume)
        {
            using (var wDoc = WordprocessingDocument.Open(_tempWorkDocPath, true))
            {
                var documentDiagramNameList = wDoc.MainDocumentPart.Document.Body
                    .Descendants<SdtElement>()
                    .Where(x => x.SdtProperties.GetFirstChild<Tag>() != null && x.SdtProperties.GetFirstChild<Tag>().Val.InnerText.Contains("Diagram"))
                    .Select(x => x.SdtProperties.GetFirstChild<Tag>().Val.InnerText)
                    .ToList();

                var modelLists = _classParser.GetListFieldsWithValues(resume);

                foreach (var diagramName in documentDiagramNameList)
                {
                    var nameOnModelList = diagramName.Replace("Diagram", string.Empty);

                    var modelData = modelLists
                        .Where(x => x.ListName == nameOnModelList)
                        .Select(x => x.ListValues)
                        .FirstOrDefault();

                    if (modelData != null)
                    {
                        var categoryNames = new List<string>() { "Max level" };
                        var values = new List<double>() { 5 };
                        foreach (var dataItem in modelData)
                        {
                            var lable = dataItem
                                .Where(x => x.Key.Contains("Name"))
                                .FirstOrDefault()
                                .Value;

                            if (!string.IsNullOrEmpty(lable))
                            {
                                var value = dataItem
                                .Where(x => x.Key.Contains("level"))
                                .FirstOrDefault()
                                .Value;

                                categoryNames.Add(lable);
                                values.Add(Convert.ToDouble(value));
                            }
                        }

                        var chartData = new ChartData
                        {
                            SeriesNames = new[] {
                                    "Values",
                                },
                            CategoryDataType = ChartDataType.String,
                            CategoryNames = categoryNames.ToArray(),
                            Values = new double[][] { values.ToArray() },
                        };
                        var isUpdate = ChartUpdater.UpdateChart(wDoc, diagramName, chartData);
                    }
                }
            }
        }

        private void AddFooterAndHeader()
        {
            var footerFile = Path.Combine(_webHostEnvironment.ContentRootPath, "Shared\\docx\\LayoutFooterHeader.docx");

            FileInfo footerHeaderSourceDocx = new FileInfo(footerFile);
            FileInfo mainDocx = new FileInfo(_tempWorkDocPath);

            var sources = new List<Source>()
            {
                new Source(new WmlDocument(mainDocx.FullName)) { KeepSections = false, DiscardHeadersAndFootersInKeptSections = true },
                new Source(new WmlDocument(footerHeaderSourceDocx.FullName)) { KeepSections = true },
            };
            DocumentBuilder.BuildDocument(sources, _tempWorkDocPath);
        }

        public void AddHeaderFromTo(string filepathFrom, string filepathTo)
        {
            // Replace header in target document with header of source document.
            using (WordprocessingDocument
                wdDoc = WordprocessingDocument.Open(filepathTo, true))
            {
                MainDocumentPart mainPart = wdDoc.MainDocumentPart;

                // Delete the existing header part.
                mainPart.DeleteParts(mainPart.HeaderParts);

                // Create a new header part.
                DocumentFormat.OpenXml.Packaging.HeaderPart headerPart =
            mainPart.AddNewPart<HeaderPart>();

                // Get Id of the headerPart.
                string rId = mainPart.GetIdOfPart(headerPart);

                // Feed target headerPart with source headerPart.
                using (WordprocessingDocument wdDocSource =
                    WordprocessingDocument.Open(filepathFrom, true))
                {
                    DocumentFormat.OpenXml.Packaging.HeaderPart firstHeader =
            wdDocSource.MainDocumentPart.HeaderParts.FirstOrDefault();

                    wdDocSource.MainDocumentPart.HeaderParts.FirstOrDefault();

                    if (firstHeader != null)
                    {
                        headerPart.FeedData(firstHeader.GetStream());
                    }
                }

                // Get SectionProperties and Replace HeaderReference with new Id.
                IEnumerable<DocumentFormat.OpenXml.Wordprocessing.SectionProperties> sectPrs =
            mainPart.Document.Body.Elements<SectionProperties>();
                foreach (var sectPr in sectPrs)
                {
                    // Delete existing references to headers.
                    //sectPr.RemoveAllChildren<HeaderReference>();

                    // Create the new header reference node.
                    sectPr.PrependChild<HeaderReference>(new HeaderReference() { Id = rId });
                }
            }
        }

        public void SaveChangesToFile()
        {
            using (var outputDocument = new TemplateProcessor(_tempWorkDocPath)
                .SetRemoveContentControls(true).SetNoticeAboutErrors(false))
            {
                outputDocument.FillContent(_valuesToFill);
                outputDocument.SaveChanges();
            }
        }
    }
}
