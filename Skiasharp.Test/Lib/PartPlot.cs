using System.Drawing;
using SkiaSharp;

namespace Skiasharp.Test.Lib;

/// <summary>
/// 图片部分绘图的具体实现
/// </summary>
public class PartPlot
{
    /// <summary>
    /// 图片的起始坐标
    /// </summary>
    public (int X, int Y) Start { get; set; }

    /// <summary>
    /// 图片宽高
    /// </summary>
    public (int Width, int Height) Size { get; set; }

    /// <summary>
    /// 边框颜色
    /// </summary>
    public string BorderColor { get; set; } = "#bcbcbc";

    /// <summary>
    /// SkiaSharp图片对象
    /// </summary>
    public SKImage Plot { get; set; }

    /// <summary>
    /// 图例
    /// </summary>
    public PartLegend Legend { get; set; }

    /// <summary>
    /// 统计摘要
    /// </summary>
    public PartSummary Summary { get; set; }

    /// <summary>
    /// 子图集合
    /// </summary>
    public List<PartPlot> Plots { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="start">起始坐标</param>
    /// <param name="imgSize">图片宽高</param>
    /// <param name="img">图片对象</param>
    public PartPlot((int x, int y) start, (int width, int height) imgSize)
    {
        Start = start;
        Size = imgSize;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="start">起始坐标</param>
    /// <param name="imgSize">图片宽高</param>
    /// <param name="img">图片对象</param>
    public PartPlot((int x, int y) start, (int width, int height) imgSize, SKImage img)
    {
        Start = start;
        Size = imgSize;
        Plot = img;
    }
}