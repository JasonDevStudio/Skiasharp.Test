using SkiaSharp;

namespace Skiasharp.Test.Lib;

/// <summary>
/// Part Setting
/// </summary>
public class PartSetting
{
    /// <summary>
    /// 文件保存路径
    /// </summary>
    public string File { get; set; }

    /// <summary>
    /// 画板大小
    /// </summary>
    public (int Width, int Height) Size { get; set; }

    /// <summary>
    /// 边框颜色
    /// </summary>
    public string BorderColor { get; set; } = "#bcbcbc";

    /// <summary>
    /// 包含图片部分的绘图信息的列表
    /// </summary>
    public List<PartPlot> Plots { get; set; } = new();
}