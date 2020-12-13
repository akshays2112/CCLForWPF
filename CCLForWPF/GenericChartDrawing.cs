using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CCLForWPF
{
    public class GenericChartDrawing
    {
        public static void ClearChartBackground(DrawingContext dc, IChart Chart)
        {
            dc.DrawRectangle(Chart.BackgroundColor == null ? Brushes.White : Chart.BackgroundColor, Chart.Border == null ? 
                new Pen(Brushes.White, 0.1) : Chart.Border, new Rect(new Point(0, 0), new Point(Chart.Width, Chart.Height)));
        }

        public static void DrawTitle(DrawingContext dc, IChart Chart)
        {
            if (Chart.Title != null && Chart.Title.Length > 0)
            {
                double titleWidth = Globals.MeasureString(dc, Chart.Title, Chart.TitleFontFamilyName == null || 
                    Chart.TitleFontFamilyName.Length == 0 ? "Arial" : Chart.TitleFontFamilyName, Chart.TitleFontStyle == null ? 
                    FontStyles.Normal : Chart.TitleFontStyle, Chart.TitleFontWeight == null ? FontWeights.Bold : Chart.TitleFontWeight,
                    Chart.TitleFontStretch == null ? FontStretches.Normal : Chart.TitleFontStretch, Chart.TitleFontSize > 0 ? 
                    Chart.TitleFontSize : 16);
                Globals.DrawString(dc, Chart.Title, Chart.TitleFontFamilyName == null || Chart.TitleFontFamilyName.Length == 0 ? "Arial" :
                    Chart.TitleFontFamilyName, Chart.TitleFontStyle == null ? FontStyles.Normal : Chart.TitleFontStyle, 
                    Chart.TitleFontWeight == null ? FontWeights.Bold : Chart.TitleFontWeight, Chart.TitleFontStretch == null ? 
                    FontStretches.Normal : Chart.TitleFontStretch, Chart.TitleFontSize > 0 ? Chart.TitleFontSize : 16,
                    Chart.TitleFontBrush == null ? Brushes.Black : Chart.TitleFontBrush, new Point(Chart.TitlePositionX > 0 ? 
                    Chart.TitlePositionX : (Chart.Width - titleWidth) / 2, Chart.TitlePositionY > 0 ? Chart.TitlePositionY : 
                    (Chart.TitleFontSize > 0 ? Chart.TitleFontSize : 16) + (Chart.TitleMarginFromTop > 0 ? Chart.TitleMarginFromTop : 10)));
            }
        }
    }
}
