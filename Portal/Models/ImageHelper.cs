using Core.Domain;

namespace Portal.Models;

public static class ImageHelper
{
    public static string GetSrc(Image img)
    {
        var imgData = Convert.ToBase64String(img.ImageData);
        var imgFormat = img.ImageFormat;

        return $"data:{imgFormat};base64,{imgData}";
    }

    public static Image CreateImgFromFile(IFormFile file)
    {
        var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);

        return new Image
        {
            ImageData = memoryStream.ToArray(),
            ImageFormat = file.ContentType
        };
    }
}