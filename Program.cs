using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using Path = System.IO.Path;

public static class Program
{
    public static void Main(params string[] args)
    {
        string imagePath = string.Join(' ', args);

        while (imagePath.Length == 0)
        {
            Console.WriteLine("Enter the path of the image that will be converted:");
            Console.Write("> ");
            
            imagePath = Console.ReadLine() ?? "";

            if (File.Exists(imagePath)) 
                continue;
            
            Console.WriteLine("File does not exist");
            imagePath = "";
        }

        Image<Argb32> image = Image.Load<Argb32>(imagePath);

        image.Mutate(x =>
        {
            DrawingOptions drawingOptions = new()
            {
                GraphicsOptions = new GraphicsOptions()
                {
                    AlphaCompositionMode = PixelAlphaCompositionMode.DestOut
                }
            };
    
            x.Fill(drawingOptions, Color.White, new EllipsePolygon(image.Width / 2f, 0, 11f * image.Width / 10f, image.Width / 4f).AsClosedPath());
            x.Fill(drawingOptions, Color.White, new Polygon(new LinearLineSegment(new PointF[]
            {
                new(image.Width / 6f, 0),
                new(2 * image.Width / 5f, image.Width / 4f),
                new(2.5f * image.Width / 6f, 0),
            })));
        });

        string savePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) +
                          $@"\hesays_{Path.GetFileNameWithoutExtension(imagePath)}.gif";

        image.SaveAsGif(savePath);
        Console.WriteLine($"Created gif: {savePath}");
    }
}