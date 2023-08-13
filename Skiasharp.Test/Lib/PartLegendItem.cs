namespace Skiasharp.Test.Lib;

/// <summary>
/// PartLegendItem的信息的具体实现
/// </summary>
public class PartLegendItem
{ 
    /// <summary>
    /// 颜色文本
    /// </summary>
    public string ColorTxt { get; set; }

    /// <summary>
    /// 形状
    /// </summary>
    public Shape Shape { get; set; }

    /// <summary>
    /// 值
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary> 
    /// <param name="colorTxt">颜色文本</param>
    /// <param name="shape">形状</param>
    /// <param name="value">值</param>
    public PartLegendItem(string colorTxt, Shape shape, string value)
    { 
        ColorTxt = colorTxt;
        Shape = shape;
        Value = value;
    }
}