using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CCLForWPF
{
    public interface IChart
    {
        CCLForWPFCanvas Canvas { get; set; }
        RenderTargetBitmap RenderTargetBitmap { get; set; }
        MemoryStream MemoryStream { get; set; }
        byte[] RawImageDataBytes { get; set; }

        int Width { get; set; }
        int Height { get; set; }

        Brush BackgroundColor { get; set; }

        Pen Border { get; set; }

        string Title { get; set; }
        string TitleFontFamilyName { get; set; }
        FontStyle TitleFontStyle { get; set; }
        FontWeight TitleFontWeight { get; set; }
        FontStretch TitleFontStretch { get; set; }
        int TitleFontSize { get; set; }
        Brush TitleFontBrush { get; set; }
        int TitlePositionX { get; set; }
        int TitlePositionY { get; set; }
        int TitleMarginFromTop { get; set; }

        bool ShowLegend { get; set; }
        bool DynamicLegend { get; set; }
        string LegendFontFamilyName { get; set; }
        FontStyle LegendFontStyle { get; set; }
        FontWeight LegendFontWeight { get; set; }
        FontStretch LegendFontStretch { get; set; }
        int LegendFontSize { get; set; }
        Brush LegendFontBrush { get; set; }
    }
}
