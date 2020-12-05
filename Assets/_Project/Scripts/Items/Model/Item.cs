namespace Items.Model
{
    public class Item
    {
        public string Name { get; set; } 
        public string Category { get; set; } 
        public Bounds Bounds { get; set; } 
        public Visual Visual { get; set; }
    }
}