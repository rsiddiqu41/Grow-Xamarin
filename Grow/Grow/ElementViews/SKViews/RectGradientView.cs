using System;
using System.Collections.Generic;
using System.Text;
using SkiaSharp;

using Grow.ElementViews.SKViews.Base;

namespace Grow.ElementViews.SKViews
{
    public class RectGradientView : GradientViewBase
    {
        public RectGradientView() { }
        protected override void DrawGradient(SKImageInfo info, SKCanvas canvas, SKPaint paint)
        {
            canvas.DrawRect(0, 0, info.Width, info.Height, paint);
        }
    }
}
