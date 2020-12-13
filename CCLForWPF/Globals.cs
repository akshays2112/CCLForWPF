using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CCLForWPF
{
    public class Globals
    {
        public enum ChartType
        {
            RawImageDataBytes,
            Canvas,
            BitmapImage
        }

        public static double DrawString(DrawingContext dc, string text, string fontFamilyName, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch, int fontSize, Brush colorBrush, Point location)
        {
            Typeface typeface = new Typeface(new FontFamily(fontFamilyName), fontStyle, fontWeight, fontStretch);

            GlyphTypeface glyphTypeface;
            if (!typeface.TryGetGlyphTypeface(out glyphTypeface))
                return 0;

            ushort[] glyphIndexes = new ushort[text.Length];
            double[] advanceWidths = new double[text.Length];

            double totalWidth = 0;

            for (int n = 0; n < text.Length; n++)
            {
                ushort glyphIndex = glyphTypeface.CharacterToGlyphMap[text[n]];
                glyphIndexes[n] = glyphIndex;

                double width = glyphTypeface.AdvanceWidths[glyphIndex] * fontSize;
                advanceWidths[n] = width;

                totalWidth += width;
            }

            GlyphRun glyphRun = new GlyphRun(glyphTypeface, 0, false, fontSize, glyphIndexes, location, advanceWidths, null, null, null, null, null, null);

            dc.DrawGlyphRun(colorBrush, glyphRun);

            return totalWidth;
        }

        public static double MeasureString(DrawingContext dc, string text, string fontFamilyName, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch, int fontSize)
        {
            Typeface typeface = new Typeface(new FontFamily(fontFamilyName), fontStyle, fontWeight, fontStretch);

            GlyphTypeface glyphTypeface;
            if (!typeface.TryGetGlyphTypeface(out glyphTypeface))
                return 0;

            ushort[] glyphIndexes = new ushort[text.Length];
            double[] advanceWidths = new double[text.Length];

            double totalWidth = 0;

            for (int n = 0; n < text.Length; n++)
            {
                ushort glyphIndex = glyphTypeface.CharacterToGlyphMap[text[n]];
                glyphIndexes[n] = glyphIndex;

                double width = glyphTypeface.AdvanceWidths[glyphIndex] * fontSize;
                advanceWidths[n] = width;

                totalWidth += width;
            }

            return totalWidth;
        }
    }
}
