namespace Core.Domain;

public class Image
{
    public int Id { get; set; }

    public byte[] ImageData { get; set; }

    public string ImageFormat { get; set; }
}