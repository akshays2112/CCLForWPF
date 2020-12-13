using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace CCLForWPF
{
    public class CCLForWPFCanvas : Canvas
    {
        public object Chart { get; set; }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if(Chart != null)
            {
                Chart.GetType().GetMethod("DrawChart").Invoke(Chart, new object[] { dc });
            }
        }
    }
}
