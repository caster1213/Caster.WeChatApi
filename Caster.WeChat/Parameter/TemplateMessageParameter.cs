using System.Collections.Generic;

namespace Caster.WeChat.Parameter
{
    public class TemplateMessageParameter
    {
        public TemplateMessageParameter()
        {
            Content = new List<FieldConfig>();
        }

        public FieldConfig First { get; set; }
        public List<FieldConfig> Content { get; }
        public FieldConfig Remark { get; set; }
    }
    
    public class FieldConfig
    {
        public FieldConfig(string color,string value)
        {
            Value = value;
            Color = color;
        }
        public string Value { get; set; }
        public string Color { get; set; }
    }
}