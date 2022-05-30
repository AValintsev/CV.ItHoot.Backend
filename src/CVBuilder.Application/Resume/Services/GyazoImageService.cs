using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CVBuilder.Application.Resume.Services.Interfaces;

namespace CVBuilder.Application.Resume.Services;

public class GyazoResponse
{
    public string Type { get; set; }
    [JsonPropertyName("thumb_url")] public string Thumb { get; set; }
    [JsonPropertyName("createad_at")] public DateTime CreatedAt { get; set; }
    [JsonPropertyName("image_id")] public string ImageId { get; set; }
    [JsonPropertyName("permalink_url")] public string PermaLinkUrl { get; set; }
    public string Url { get; set; }
}

public class GyazoImageService : IImageService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GyazoImageService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> UploadImage(string fileType, byte[] image)
    {
        var url = " https://upload.gyazo.com/api/upload?access_token=M8RXuXZDyPgOk-hl358SBo82Io_q1hJ_kdor87xyZLw";
        var client = _httpClientFactory.CreateClient();
        var requestContent = new MultipartFormDataContent();
        var imageContent = new ByteArrayContent(image);
        imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse(fileType);
        requestContent.Add(imageContent, "imagedata", "image.jpg");
        var response = await client.PostAsync(url, requestContent);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Something went wrong");
        
        var model = await response.Content.ReadFromJsonAsync<GyazoResponse>();
        return model!.Url;
    }
}