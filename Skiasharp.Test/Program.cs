// See https://aka.ms/new-console-template for more information
using Skiasharp.Test;
using Skiasharp.Test.Lib;
using SkiaSharp;
using System.Drawing;
using System.Runtime.InteropServices;
 
var plot1 = CreatePlotDemo();
var plot2 = CreatePlotDemo();
var plot3 = CreatePlotDemo();
var plot4 = CreatePlotDemo();
plot1.Start = (0, 0);
plot2.Start = (1490, 0);
plot3.Start = (0, 689);
plot4.Start = (1490, 689);

var setting = new PartSetting() { Size=(3000,1380), File= $"output_{DateTime.Now:yyyyMMddHHmmss}.png" };
setting.Plots.Add(plot1);
setting.Plots.Add(plot2);
setting.Plots.Add(plot3);
setting.Plots.Add(plot4);

PlotImageUtils.SkDrawImage(setting);

//// 调用SkDrawImage方法绘制图像
//var fullimage = PlotImageUtils.SkDrawPlot(plot2);
//// 将 SKImage 转换为 PNG 格式的字节
//SKData encoded = fullimage.Encode(SKEncodedImageFormat.Png, 100);

//// 获取字节数据
//byte[] imageBytes = encoded.ToArray();

//// 定义文件路径
//string savePath = $"output_{DateTime.Now:yyyyMMddHHmmss}.png";

//// 将字节写入文件
//File.WriteAllBytes(savePath, imageBytes);


static PartPlot CreatePlotDemo()
{
    var path = "ScottPlot.png"; // 请确保此路径正确
    var skimg = PlotImageUtils.LoadImageFromPng(path);

    // 创建图例部分
    var legends = new List<PartLegendItem>();
    var random = new Random();
    for (int i = 0; i < 20; i++)
    {
        Shape shape;
        switch (i % 3)
        {
            case 0:
                shape = Shape.Triangle; // 三角形
                break;
            case 1:
                shape = Shape.Square; // 矩形
                break;
            default:
                shape = Shape.Circle; // 圆形
                break;
        }

        var value = $"Value {i:D2}"; // 文字长度约为10个字符
        var color = SKColor.FromHsl(random.Next(360), 100, 50);
        var colorTxt = $"#{color.Red:X2}{color.Green:X2}{color.Blue:X2}";
        legends.Add(new PartLegendItem(colorTxt, shape, value));
    }


    var legend = new PartLegend() { Items = legends, LegendSize = (150, 300) };

    // 创建摘要部分
    var summaries = new List<PartSummaryItem>();
    for (int i = 0; i < 20; i++)
    {
        string title = $"Title"; // 文字长度为5个字符
        string value = $"Value {i:D2}"; // 文字长度约为10个字符

        summaries.Add(new PartSummaryItem(title, value));
    }

    var summary = new PartSummary() { Items = summaries, Size = (150, 300) };

    // 创建设置对象
    var setting = new PartPlot((0, 0), (1339, 688))
    {
        Plot = skimg,
        BorderColor = "#bcbcbc",
        Legend = legend,
        Summary = summary
    };

    return setting;
}

