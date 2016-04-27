namespace ImageLoader
{
    public class Image
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public string Name { get; set; }
        public int[,] Data { get; set; }
    }
}
