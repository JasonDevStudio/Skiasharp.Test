using SkiaSharp;

namespace Skiasharp.Test.Lib;

/// <summary>
/// 绘制图片部分
/// </summary>
public static class PlotImageUtils
{
    /// <summary>
    /// 行高
    /// </summary>
    private const int RowHeight = 20; // 每行的高度  

    /// <summary>
    /// 边框颜色
    /// </summary>
    private const string BorderColor = "#bcbcbc";

    /// <summary>
    /// 在画布上绘制并保存图像
    /// </summary>
    /// <param name="setting">绘图设置</param>
    public static void SkDrawImage(PartSetting setting)
    {
        // 创建一个与 PartSetting.Size 匹配的 SKSurface
        var info = new SKImageInfo(setting.Size.Width, setting.Size.Height);
        var surface = SKSurface.Create(info);

        // 从 SKSurface 获取 SKCanvas
        var canvas = surface.Canvas;

        // 遍历 Plots
        foreach (var plot in setting.Plots)
        {
            // 获取 SkImage
            var image = SkDrawPlot(plot); // 假设 PartPlot 类有一个返回 SkImage 的 Plot 属性

            // 根据 PartPlot 的 Start 位置将 SkImage 填充到对应位置
            canvas.DrawImage(image, plot.Start.X, plot.Start.Y);
        }

        // 最终保存图片到 File 路径
        using (var image = surface.Snapshot())
            SaveImageAsPng(image, setting.File);
    }

    /// <summary>
    /// 在画布上绘制 Plot
    /// </summary>
    /// <param name="canvas">画布</param>
    /// <param name="plot">绘图信息</param>
    /// <returns>更新后的图片</returns>
    public static SKImage SkDrawPlot(PartPlot plot)
    {
        float x = 0; // 横坐标起始位置
        float y = 0; // 纵坐标起始位置 
        var width = plot.Size.Width + plot.Legend.LegendSize.Width;
        var info = new SKImageInfo(width, plot.Size.Height);
        var surface = SKSurface.Create(info);
        var canvas = surface.Canvas;
        canvas.DrawColor(SKColors.White);

        // 如果 Plots 不为空，则遍历 Plot,将 Plots 拼接成一个大图片
        if (plot.Plots != null && plot.Plots.Count > 0)
        {
            foreach (var subPlot in plot.Plots)
            {
                // 绘制每个子 Plot
                // 此处可以添加逻辑来绘制具体的 Plot 图片
                canvas.DrawImage(plot.Plot, x, y);
                x += subPlot.Size.Width;
                y += subPlot.Size.Height;
            }
        }
        // 如果 Plots 为空，Img 不为空，则直接用 Img 拼接
        else if (plot.Plot != null)
        {
            canvas.DrawImage(plot.Plot, x, y);
        }

        // 调用 SkDrawLegend 和 SkDrawSummary 绘制 图表右侧图例和统计摘要
        var setting = plot;
        SkDrawLegend(canvas, setting);
        SkDrawSummary(canvas, setting);

        // 使用 BorderColor 对图片增加1像素的边框
        SkDrawBorder(canvas, x, y, setting.Size.Width, setting.Size.Height);

        // 从 SKSurface 获取 SKImage
        return surface.Snapshot();
    }

    /// <summary>
    /// 在画布上绘制 Legend
    /// </summary>
    /// <param name="canvas">画布</param>
    /// <param name="plot">图表设置信息</param>
    /// <returns>更新后的画布</returns>
    private static SKCanvas SkDrawLegend(SKCanvas canvas, PartPlot plot)
    {
        var textPaint = new SKPaint
        {
            TextSize = 12,
            Typeface = SKTypeface.FromFamilyName("Microsoft YaHei"), // 使用支持中文的字体
            Color = SKColors.Black
        };

        // 获取 Legend 信息
        var legend = plot.Legend;
        var items = legend.Items;
        var legendSize = legend.LegendSize;

        float x = plot.Size.Width + 10; // 横坐标起始位置
        float y = 10; // 纵坐标起始位置

        foreach (var item in items)
        {
            // 计算文字宽度 
            float textWidth = textPaint.MeasureText(item.Value);

            // 如果文字宽度超过 Legend 的宽度，省略显示
            if (textWidth > legendSize.Width)
            {
                item.Value = item.Value.Substring(0, 10) + "..."; // 例如截取前10个字符并添加省略号
                textWidth = textPaint.MeasureText(item.Value); // 重新计算宽度
            }

            // 如果绘制高度超过 Legend 的高度，省略显示
            if (y > legendSize.Height)
            {
                canvas.DrawText("...", x, y, textPaint); // 在最后一行绘制省略号
                break;
            }
            else
            {
                // 绘制形状
                var shapePaint = new SKPaint { Color = SKColor.Parse(item.ColorTxt) };
                canvas.DrawShape(item.Shape, x, y, shapePaint);

                // 绘制文字
                canvas.DrawText(item.Value, x + 15, y + 10, textPaint); // 在形状右侧20个单位的位置绘制文字 
            }

            // 更新纵坐标
            y += RowHeight; // 每行间隔20个单位
        }

        return canvas;
    }

    /// <summary>
    /// 在指定的位置上绘制指定的形状
    /// </summary>
    /// <param name="canvas">画布</param>
    /// <param name="shape">要绘制的形状类型</param>
    /// <param name="x">形状的横坐标</param>
    /// <param name="y">形状的纵坐标</param>
    /// <param name="paint">用于绘制的画笔</param>
    private static void DrawShape(this SKCanvas canvas, Shape shape, float x, float y, SKPaint paint)
    {
        // 形状大小
        float size = 10;

        // 根据形状类型绘制
        switch (shape)
        {
            case Shape.Circle:
                canvas.DrawCircle(x + size / 2, y + size / 2, size / 2, paint); // 绘制圆形
                break;
            case Shape.Square:
                canvas.DrawRect(x, y, size, size, paint); // 绘制正方形
                break;
            case Shape.Triangle:
                // 绘制三角形
                SKPath path = new SKPath();
                path.MoveTo(x, y);
                path.LineTo(x + size, y);
                path.LineTo(x + size / 2, y + size);
                path.Close();
                canvas.DrawPath(path, paint);
                break;
                // 添加其他形状的绘制逻辑
        }
    }

    /// <summary>
    /// 在画布上绘制 Summary
    /// </summary>
    /// <param name="canvas">画布</param>
    /// <param name="plot">图表设置信息</param>
    /// <returns>更新后的画布</returns>
    private static SKCanvas SkDrawSummary(SKCanvas canvas, PartPlot plot)
    {
        // 获取 Summary 信息
        var summary = plot.Summary;
        var items = summary.Items;
        var summarySize = summary.Size;

        float x = plot.Size.Width + 10; // 横坐标起始位置 
        float y = plot.Size.Height / 2; // 纵坐标起始位置  

        foreach (var item in items)
        {
            // 拼接 title 和空格
            string text = item.Title + " " + item.Value;

            // 计算文字宽度
            var textPaint = new SKPaint { TextSize = 12 };
            float textWidth = textPaint.MeasureText(text);

            // 如果文字宽度超过 Summary 的宽度，省略显示
            if (textWidth > summarySize.Width)
            {
                text = text.Substring(0, 10) + "..."; // 例如截取前10个字符并添加省略号
                textWidth = textPaint.MeasureText(text); // 重新计算宽度
            }

            // 如果绘制高度超过 Summary 的高度，省略显示
            if (y > plot.Size.Height - RowHeight)
            {
                canvas.DrawText("...", x, y, textPaint); // 在最后一行绘制省略号
                break;
            }
            else
            {
                // 绘制文字
                canvas.DrawText(text, x, y + 10, textPaint);
            }

            // 更新纵坐标
            y += RowHeight; // 每行间隔20个单位
        }

        return canvas;
    }

    /// <summary>
    /// 在画布上绘制1像素的边框
    /// </summary>
    /// <param name="canvas">用于绘制的画布</param>
    /// <param name="x">边框的左上角 x 坐标</param>
    /// <param name="y">边框的左上角 y 坐标</param>
    /// <param name="width">边框的宽度</param>
    /// <param name="height">边框的高度</param>
    private static SKCanvas SkDrawBorder(SKCanvas canvas, float x, float y, int width, int height)
    {
        // 解析边框颜色
        var borderColor = SKColor.Parse(BorderColor);

        // 创建画笔，设置颜色和笔触宽度
        var borderPaint = new SKPaint { Color = borderColor, StrokeWidth = 1, IsStroke = true };

        // 绘制矩形作为边框，减1确保1像素的笔触完全可见
        canvas.DrawRect(x + 1, y + 1, width - 2, height - 3, borderPaint);

        // 返回画布以进行链式调用（如果需要）
        return canvas;
    }

    /// <summary>
    /// 将图片文件加载到SKImage
    /// </summary>
    /// <param name="path">图片文件地址</param>
    /// <returns>SKImage</returns>
    public static SKImage LoadImageFromPng(string path)
    {
        using (var stream = File.OpenRead(path))
        {
            // 从流解码 PNG 到 SKBitmap
            SKBitmap bitmap = SKBitmap.Decode(stream);

            // 从 SKBitmap 创建 SKImage
            SKImage image = SKImage.FromBitmap(bitmap);

            return image;
        }
    }

    /// <summary>
    /// 将SKImage保存为PNG图片
    /// </summary>
    /// <param name="image">SKimg</param>
    /// <param name="path">保存文件路径</param>
    public static void SaveImageAsPng(SKImage image, string path)
    {
        using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
        using (var stream = System.IO.File.Create(path))
        {
            data.SaveTo(stream);
        }
    }
}
