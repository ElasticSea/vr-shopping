using System.Collections.Generic;

namespace Items.Model
{
    public class Visual
    {
        public string Source { get; set; }
        public byte[] SourceBytes { get; set; }
        public List<Material> Materials { get; set; }
    }
}