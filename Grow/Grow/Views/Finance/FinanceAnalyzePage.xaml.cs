using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinanceAnalyzePage : ContentPage
    {
        public FinanceAnalyzePage()
        {
            InitializeComponent();

            /*var orangeColors = new SKColor[]
            {
                new SKColor(255,153,0,1),
                new SKColor(160, 96, 0, 1)
            };

            var orangeShader = SKShader.CreateLinearGradient(
                new SKPoint(0, 0),
                new SKPoint(255, 255),
                orangeColors,
                null,
                SKShaderTileMode.Clamp);

            var blueColors = new SKColor[]
{
                new SKColor(0,134,255,1), //LightBlue
                new SKColor(160, 96, 0, 1) //DarkBlue
};

            var blueShader = SKShader.CreateLinearGradient(
                new SKPoint(0, 0),
                new SKPoint(255, 255),
                blueColors,
                null,
                SKShaderTileMode.Clamp);

            var aquaColors = new SKColor[]
{
                new SKColor(255,153,0,1),
                new SKColor(160, 96, 0, 1)
};

            var aquaShader = SKShader.CreateLinearGradient(
                new SKPoint(0, 0),
                new SKPoint(255, 255),
                aquaColors,
                null,
                SKShaderTileMode.Clamp);*/

            ChartEntry[] testEntries = new[]
            {
                new ChartEntry(4800)
                {
                    Label = "Income",
                    ValueLabel = "4800",
                    Color = SKColor.Parse("#FF9900")
                },

                new ChartEntry(3600)
                {
                    Label = "Outcome",
                    ValueLabel = "3600",
                    Color = SKColor.Parse("#0086ff")
                },

                new ChartEntry(1200)
                {
                    Label = "Balance",
                    ValueLabel = "1200",
                    Color = SKColor.Parse("#26d0d4")
                }
            };

            IncomeOutcomeChart.Chart = new PieChart 
            {
                Entries = testEntries,
                HoleRadius = 0.4f,
                LabelMode = LabelMode.LeftAndRight,
                IsAnimated = true
            };

            CategoricalSpendingChart.Chart = new BarChart();
        }
    }
}