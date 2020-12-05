using System.Collections.Generic;
using UnityEngine;

namespace Items.Model
{
    public class Material
    {
        public Dictionary<string, int> IntProperties = new Dictionary<string, int>();
        public Dictionary<string, float> FloatProperties = new Dictionary<string, float>();
        public Dictionary<string, bool> BoolProperties = new Dictionary<string, bool>();
        public Dictionary<string, Color> ColorProperties = new Dictionary<string, Color>();
        public Dictionary<string, MaterialTexture> TextureProperties = new Dictionary<string, MaterialTexture>();
        public int RenderQueue = -1;
    }
}