using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace CCLForWPF
{
    public class BarChart : IChart
    {
        public CCLForWPFCanvas Canvas { get; set; }
        public RenderTargetBitmap RenderTargetBitmap { get; set; }
        public MemoryStream MemoryStream { get; set; }
        public byte[] RawImageDataBytes { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public Brush BackgroundColor { get; set; }

        public Pen Border { get; set; }

        public string Title { get; set; }
        public string TitleFontFamilyName { get; set; }
        public FontStyle TitleFontStyle { get; set; }
        public FontWeight TitleFontWeight { get; set; }
        public FontStretch TitleFontStretch { get; set; }
        public int TitleFontSize { get; set; }
        public Brush TitleFontBrush { get; set; }
        public int TitlePositionX { get; set; }
        public int TitlePositionY { get; set; }
        public int TitleMarginFromTop { get; set; }

        public bool ShowLegend { get; set; }
        public bool DynamicLegend { get; set; }
        public string LegendFontFamilyName { get; set; }
        public FontStyle LegendFontStyle { get; set; }
        public FontWeight LegendFontWeight { get; set; }
        public FontStretch LegendFontStretch { get; set; }
        public int LegendFontSize { get; set; }
        public Brush LegendFontBrush { get; set; }

        public Brush YAxisLineColor { get; set; }
        public double YAxisLineThickness { get; set; }
        public int NumberOfMarksY { get; set; }
        public bool FullMarksLines { get; set; }
        public Brush FullMarksLineColor { get; set; }
        public double FullMarksLineThickness { get; set; }
        public int WidthOfMarksY { get; set; }
        public Brush MarksYLineColor { get; set; }
        public double MarksYLineThickness { get; set; }
        public int LabelYToMarksYSpacing { get; set; }
        public bool DynamicallyCalculatedMaximumDataValue { get; set; }
        public double MaxDataValue { get; set; }
        public bool DynamicallyCalculatedMinimumDataValue { get; set; }
        public double MinDataValue { get; set; }
        public bool DynamicallyCalculatedYAxisLength { get; set; }
        public double YAxisLengthInPixels { get; set; }
        public double MarginSpaceYFromTitle { get; set; }
        public double MarginSpaceYFromBottom { get; set; }
        public double MarginSpaceFromLeft { get; set; }
        public double DynamicallyCalculateXAxisLength { get; set; }
        public double XAxisLengthInPixels { get; set; }
        public Brush XAxisLineColor { get; set; }
        public double XAxisLineThickness { get; set; }
        public string YMarksLabelsFontFamilyName { get; set; }
        public FontStyle YMarksLabelsFontStyle { get; set; }
        public FontWeight YMarksLabelsFontWeight { get; set; }
        public FontStretch YMarksLabelsFontStretch { get; set; }
        public int YMarksLabelsFontSize { get; set; }
        public Brush YMarksLabelsFontBrush { get; set; }
        public double YMarksLabelSpacing { get; set; }
        public double MarksXAxisSpacingFromXAxisLine { get; set; }
        public string XMarksLabelsFontFamilyName { get; set; }
        public FontStyle XMarksLabelsFontStyle { get; set; }
        public FontWeight XMarksLabelsFontWeight { get; set; }
        public FontStretch XMarksLabelsFontStretch { get; set; }
        public int XMarksLabelsFontSize { get; set; }
        public Brush XMarksLabelsFontColor { get; set; }

        public double BarWidth { get; set; }

        public List<object> Data { get; set; }
        public string YValuesPropertyName { get; set; }
        public string XLabelsPropertyName { get; set; }
        public string BarColorPropertyName { get; set; }
        public bool DynamicBarColors { get; set; }

        public object DrawBarChart(Globals.ChartType chartType = Globals.ChartType.Canvas)
        {
            switch(chartType)
            {
                case Globals.ChartType.RawImageDataBytes:
                    DrawingVisual dv = new DrawingVisual();
                    using (DrawingContext dc = dv.RenderOpen())
                    {
                        DrawChart(dc);
                    }
                    return null;
                case Globals.ChartType.BitmapImage:
                    return null;
                case Globals.ChartType.Canvas:
                    Canvas = new CCLForWPFCanvas();
                    Canvas.Width = Width > 0 ? Width : 500;
                    Canvas.Height = Height > 0 ? Height : 400;
                    Canvas.Chart = this;
                    return Canvas;
            }
            return null;
        }

        public void DrawChart(DrawingContext dc)
        {
            GenericChartDrawing.ClearChartBackground(dc, this);
            GenericChartDrawing.DrawTitle(dc, this);
            double maxDataValue = 0;
            if (DynamicallyCalculatedMaximumDataValue)
            {
                foreach(object o in Data)
                {
                    if((double)o.GetType().GetProperty(YValuesPropertyName).GetValue(o) > maxDataValue)
                    {
                        maxDataValue = (double)o.GetType().GetProperty(YValuesPropertyName).GetValue(o);
                    }
                } 
            } else
            {
                maxDataValue = MaxDataValue;
            }
            double minDataValue = maxDataValue;
            if(DynamicallyCalculatedMinimumDataValue)
            {
                foreach (object o in Data)
                {
                    if ((double)o.GetType().GetProperty(YValuesPropertyName).GetValue(o) < minDataValue)
                    {
                        minDataValue = (double)o.GetType().GetProperty(YValuesPropertyName).GetValue(o);
                    }
                }
            } else
            {
                minDataValue = MinDataValue;
            }
            double YAxisLength = 0;
            if(DynamicallyCalculatedYAxisLength)
            {
                YAxisLength = Height - ((TitleMarginFromTop > 0 ? TitleMarginFromTop : 10) + (TitleFontSize > 0 ? TitleFontSize : 16) +
                    (MarginSpaceYFromTitle > 0 ? MarginSpaceYFromTitle : 10) + (MarginSpaceYFromBottom > 0 ? MarginSpaceYFromBottom : 20));
            } else
            {
                YAxisLength = YAxisLengthInPixels;
            }
            dc.DrawLine(new Pen(YAxisLineColor == null ? Brushes.Black : YAxisLineColor, YAxisLineThickness > 0 ? YAxisLineThickness : 0.1),
                new Point(MarginSpaceFromLeft > 0 ? MarginSpaceFromLeft : 20, (TitleMarginFromTop > 0 ? TitleMarginFromTop : 10) +
                (TitleFontSize > 0 ? TitleFontSize : 16) + (MarginSpaceYFromTitle > 0 ? MarginSpaceYFromTitle : 10)), new Point(
                    MarginSpaceFromLeft > 0 ? MarginSpaceFromLeft : 20, (TitleMarginFromTop > 0 ? TitleMarginFromTop : 10) +
                (TitleFontSize > 0 ? TitleFontSize : 16) + (MarginSpaceYFromTitle > 0 ? MarginSpaceYFromTitle : 10) + YAxisLength));
            dc.DrawLine(new Pen(XAxisLineColor == null ? Brushes.Black : XAxisLineColor, XAxisLineThickness > 0 ? XAxisLineThickness : 0.1),
                new Point(MarginSpaceFromLeft > 0 ? MarginSpaceFromLeft : 20, MarginSpaceYFromBottom > 0 ? MarginSpaceYFromBottom :
                (TitleMarginFromTop > 0 ? TitleMarginFromTop : 10) + (TitleFontSize > 0 ? TitleFontSize : 16) + (MarginSpaceYFromTitle > 0 ?
                MarginSpaceYFromTitle : 10) + YAxisLength), new Point((MarginSpaceFromLeft > 0 ? MarginSpaceFromLeft : 20) +
                (XAxisLengthInPixels > 0 ? XAxisLengthInPixels : Width - 20), MarginSpaceYFromBottom > 0 ? MarginSpaceYFromBottom :
                (TitleMarginFromTop > 0 ? TitleMarginFromTop : 10) + (TitleFontSize > 0 ? TitleFontSize : 16) + (MarginSpaceYFromTitle > 0 ?
                MarginSpaceYFromTitle : 10) + YAxisLength));
            if(NumberOfMarksY > 0)
            {
                double valuePerMark = (maxDataValue - minDataValue) / NumberOfMarksY;
                int pixelsPerMark = (int)(YAxisLength / NumberOfMarksY);
                for(int i = 0; i <= NumberOfMarksY; i++)
                {
                    dc.DrawLine(new Pen(MarksYLineColor == null ? Brushes.Black : MarksYLineColor, MarksYLineThickness > 0 ? MarksYLineThickness :
                        0.1), new Point((MarginSpaceFromLeft > 0 ? MarginSpaceFromLeft : 20) - WidthOfMarksY, MarginSpaceYFromBottom > 0 ?
                        MarginSpaceYFromBottom : (TitleMarginFromTop > 0 ? TitleMarginFromTop : 10) + (TitleFontSize > 0 ? TitleFontSize : 16) + 
                        (MarginSpaceYFromTitle > 0 ? MarginSpaceYFromTitle : 10) + YAxisLength - (pixelsPerMark * i)), new Point(
                        (MarginSpaceFromLeft > 0 ? MarginSpaceFromLeft : 20) + (FullMarksLines ? (XAxisLengthInPixels > 0 ? XAxisLengthInPixels : 
                        Width - 20) : 0), MarginSpaceYFromBottom > 0 ? MarginSpaceYFromBottom : (TitleMarginFromTop > 0 ? TitleMarginFromTop : 
                        10) + (TitleFontSize > 0 ? TitleFontSize : 16) + (MarginSpaceYFromTitle > 0 ? MarginSpaceYFromTitle : 10) + YAxisLength -
                        (pixelsPerMark * i)));
                    double labelWidth = Globals.MeasureString(dc, (valuePerMark * i + minDataValue).ToString(), YMarksLabelsFontFamilyName
                        == null ? "Arial" : YMarksLabelsFontFamilyName, YMarksLabelsFontStyle == null ? FontStyles.Normal : YMarksLabelsFontStyle,
                        YMarksLabelsFontWeight == null ? YMarksLabelsFontWeight : FontWeights.Normal, YMarksLabelsFontStretch == null ?
                        FontStretches.Normal : YMarksLabelsFontStretch, YMarksLabelsFontSize > 0 ? YMarksLabelsFontSize : 10);
                    Globals.DrawString(dc, (valuePerMark * i + minDataValue).ToString(), YMarksLabelsFontFamilyName == null ? "Arial" : 
                        YMarksLabelsFontFamilyName, YMarksLabelsFontStyle == null ? FontStyles.Normal : YMarksLabelsFontStyle, 
                        YMarksLabelsFontWeight == null ? YMarksLabelsFontWeight : FontWeights.Normal, YMarksLabelsFontStretch == null ? 
                        FontStretches.Normal : YMarksLabelsFontStretch, YMarksLabelsFontSize > 0 ? YMarksLabelsFontSize : 10, 
                        YMarksLabelsFontBrush == null ? Brushes.Black : YMarksLabelsFontBrush, new Point((MarginSpaceFromLeft > 0 ? 
                        MarginSpaceFromLeft : 20) - WidthOfMarksY - labelWidth - (YMarksLabelSpacing > 0 ? YMarksLabelSpacing : 5), 
                        MarginSpaceYFromBottom > 0 ? MarginSpaceYFromBottom : (TitleMarginFromTop > 0 ? TitleMarginFromTop : 10) + 
                        (TitleFontSize > 0 ? TitleFontSize : 16) + (MarginSpaceYFromTitle > 0 ? MarginSpaceYFromTitle : 10) + YAxisLength - 
                        (pixelsPerMark * i) + ((YMarksLabelsFontSize > 0 ? YMarksLabelsFontSize : 10) / 2)));
                }
            }
            List<Brush> barColors = new List<Brush>();
            double xAxisLength = (XAxisLengthInPixels > 0 ? XAxisLengthInPixels : Width - 20);
            double pixelsPerXMark = xAxisLength / (Data.Count + 1);
            Random r = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < Data.Count; i++)
            {
                double barYValue = (double) Data[i].GetType().GetProperty(YValuesPropertyName).GetValue(Data[i]);
                string barXValue = (string) Data[i].GetType().GetProperty(XLabelsPropertyName).GetValue(Data[i]);
                Brush barBrush;
                if(DynamicBarColors)
                {
                    barBrush = new SolidColorBrush(Color.FromArgb(255, Convert.ToByte(r.Next(0, 255)), Convert.ToByte(r.Next(0, 255)),
                        Convert.ToByte(r.Next(0, 255))));
                    barColors.Add(barBrush);
                }
                else
                {
                    barBrush = (Brush)Data[0].GetType().GetProperty(BarColorPropertyName).GetValue(Data[0]);
                }
                double barHeight = (barYValue * YAxisLength) / maxDataValue;
                double labelWidth = Globals.MeasureString(dc, barXValue, XMarksLabelsFontFamilyName == null ? "Arial" : XMarksLabelsFontFamilyName,
                    XMarksLabelsFontStyle == null ? FontStyles.Normal : XMarksLabelsFontStyle, XMarksLabelsFontWeight == null ?
                    FontWeights.Normal : XMarksLabelsFontWeight, XMarksLabelsFontStretch == null ? FontStretches.Normal :
                    XMarksLabelsFontStretch, XMarksLabelsFontSize > 0 ? XMarksLabelsFontSize : 10);
                dc.DrawRectangle(barBrush, new Pen(barBrush, 0.1), new Rect(new Point((MarginSpaceFromLeft > 0 ? MarginSpaceFromLeft : 20) + 
                    (pixelsPerXMark * (i + 1)) + (BarWidth / 2), Height - (MarginSpaceYFromBottom > 0 ? MarginSpaceYFromBottom : 20)), 
                    new Point((MarginSpaceFromLeft > 0 ? MarginSpaceFromLeft : 20) + (pixelsPerXMark * (i + 1)) - (BarWidth / 2), 
                    Height - (MarginSpaceYFromBottom > 0 ? MarginSpaceYFromBottom : 20) - barHeight)));
                Globals.DrawString(dc, barXValue, XMarksLabelsFontFamilyName == null ? "Arial" : XMarksLabelsFontFamilyName,
                    XMarksLabelsFontStyle == null ? FontStyles.Normal : XMarksLabelsFontStyle, XMarksLabelsFontWeight == null ?
                    FontWeights.Normal : XMarksLabelsFontWeight, XMarksLabelsFontStretch == null ? FontStretches.Normal :
                    XMarksLabelsFontStretch, XMarksLabelsFontSize > 0 ? XMarksLabelsFontSize : 10, XMarksLabelsFontColor == null ?
                    Brushes.Black : XMarksLabelsFontColor, new Point((MarginSpaceFromLeft > 0 ? MarginSpaceFromLeft : 20) + (pixelsPerXMark * 
                    (i + 1)) - labelWidth / 2, Height - (MarginSpaceYFromBottom > 0 ? MarginSpaceYFromBottom : 20) +
                    (XMarksLabelsFontSize > 0 ? XMarksLabelsFontSize : 10) + MarksXAxisSpacingFromXAxisLine));
            }
        }
    }
}
