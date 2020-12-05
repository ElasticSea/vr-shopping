namespace Items.Model
{
    public class MaterialProperty
    {
        public string Key { get; set; }
        public MaterialPropertyType Type { get; set; }
        public object Value { get; set; }

        public MaterialProperty(string key, MaterialPropertyType type, object value)
        {
            Key = key;
            Type = type;
            Value = value;
        }
    }
}