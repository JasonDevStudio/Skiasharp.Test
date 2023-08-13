using System.Drawing;

namespace Skiasharp.Test.Lib;

/// <summary>
/// Legend的信息的具体实现
/// </summary>
public class PartLegend
{
    /// <summary>
    /// Legend大小
    /// </summary>
    public (int Width,int Height) LegendSize { get; set; }
     
    /// <summary>
    /// Legend Item 集合
    /// </summary>
    public List<PartLegendItem> Items { get; set; }
}