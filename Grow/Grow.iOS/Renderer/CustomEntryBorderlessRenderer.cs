using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Grow.iOS.Renderer;
using Grow.BorderlessEntry;

[assembly: ExportRenderer(typeof(CustomEntryBorderless), typeof(CustomEntryBorderlessRenderer))]
namespace Grow.iOS.Renderer
{
    public class CustomEntryBorderlessRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.BorderStyle = UIKit.UITextBorderStyle.None;
            }
        }
    }
}