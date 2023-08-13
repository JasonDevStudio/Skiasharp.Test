using System.Drawing;

namespace Skiasharp.Test.Lib;

/// <summary>
/// Summary的信息的具体实现
/// </summary>
public class PartSummary
{
    /// <summary>
    /// 大小
    /// </summary>
    public (int Width,int Height) Size { get; set; } 

    /// <summary>
    /// Summary Item 集合
    /// </summary>
    public List<PartSummaryItem> Items { get; set; }
}