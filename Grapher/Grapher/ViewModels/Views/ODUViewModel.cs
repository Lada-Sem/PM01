using Grapher.Common;
using Grapher.Utils;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore.Defaults;
using static MathNet.Symbolics.Linq;
using Grapher.Models;
using Grapher.Properties;

namespace Grapher.ViewModels.Views
{
    public class ODUViewModel : ViewModelBase
    {
        private OperationModel _operation;
        private string _theory;

        public string ExpressionString { get; set; }
        public double X0 { get; set; }
        public double Y0 { get; set; }
        public double X1 { get; set; }
        public int Steps { get; set; }
        public ODUSqulptor Squlptor { get; set; }
        public string Theory
        {
            get => _theory;
            set => Set(value, ref _theory);
        }
        public ObservableCollection<ISeries> Series { get; set; }
        public Axis[] XAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Name = "X",
                    NamePaint = new SolidColorPaint(SKColors.White),

                    LabelsPaint = new SolidColorPaint(SKColors.White),
                    TextSize = 10,

                    SeparatorsPaint = new SolidColorPaint(SKColors.White)
                    {
                        StrokeThickness = 2,
                        PathEffect = new DashEffect(new float[] { 3, 3 })
                    }
                }
            };

        public Axis[] YAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Name = "Y",
                    NamePaint = new SolidColorPaint(SKColors.White),

                    LabelsPaint = new SolidColorPaint(SKColors.White),
                    TextSize = 20,

                    SeparatorsPaint = new SolidColorPaint(SKColors.White)
                    {
                        StrokeThickness = 2,
                        PathEffect = new DashEffect(new float[] { 3, 3 })
                    }
                }
            };
        public OperationModel Operation
        {
            get => _operation;
            set => Set(value, ref _operation);
        }

        public ODUViewModel()
        {
            Series = new ObservableCollection<ISeries>();
            Squlptor = new();
        }
        public void SelectMethod(string executeName)
        {
            try
            {
                Squlptor.CreateFunction(ExpressionString);
            }
            catch
            {
                Operation = new OperationModel
                {
                    Description = "Опишите функцию dx/dy, а также x0, y0, x1 и количество шагов"
                };
                return;
            }
            switch (executeName)
            {
                case "Метод Эйлера":
                    {
                        Squlptor.Operation = new();
                        var values = Squlptor.ExplicitEuler(X0, Y0, X1, Steps);
                        if(values is not null) InitSeries("Явный метод Эйлера", values);
                        values = Squlptor.ImplicitEuler(X0, Y0, X1, Steps);
                        if(values is not null) InitSeries("Неявный метод Эйлера", values);
                        values = Squlptor.ModifiedEuler(X0, Y0, X1, Steps);
                        if(values is not null) InitSeries("Модифицированный метод Эйлера", values);
                        Theory = Resources.ODU_AILER;
                        break;
                    }
                case "Методы Рунге-Кутты":
                    {
                        var values = Squlptor.RungeKutta4(X0, Y0, X1, Steps);
                        if(values is not null) InitSeries("Метод Рунге-Кутты", values);
                        Theory = Resources.ODU_RK;
                        break;
                    }
                case "Метод Адамса":
                    {
                        var values = Squlptor.AdamsBashforthMoulton4(X0, Y0, X1, Steps);
                        if (values is not null) InitSeries("Метод Адамса (Предиктор-Корректор)", values);
                        Theory = Resources.ODU_ADAMS;
                    }
                    break;
                default:
                    break;
            }
            Operation = Squlptor.Operation;
        }

        public void InitSeries(string Name, List<(double, double)> values)
        {
            var existSeries = Series?.FirstOrDefault(s => s.Name == Name);
            if(existSeries is not null)
                Series?.Remove(existSeries);
            Series?.Add(new LineSeries<ObservablePoint>
            {
                Values = values.Select(S => new ObservablePoint(S.Item1, S.Item2)).ToList(),
                Fill = new SolidColorPaint(SKColors.Blue),
                GeometryFill = new SolidColorPaint(SKColors.Blue),
                Stroke = new SolidColorPaint(SKColors.Blue),
                Tag = "X",
                Name = Name,
            });
        }
    }
}
