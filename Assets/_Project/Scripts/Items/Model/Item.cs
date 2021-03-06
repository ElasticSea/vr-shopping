using System.Collections.Generic;

namespace Items.Model
{
    public class Item
    {
        public string Name { get; set; } 
        public string Category { get; set; } 
        public float Price { get; set; }
        public string Weight { get; set; }
        public Bounds Bounds { get; set; } 
        public List<Visual> Visuals { get; set; }
    }
}