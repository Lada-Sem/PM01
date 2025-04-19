using Grapher.Common;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore;
using System.Collections.ObjectModel;
using Grapher.Models.Enums;
using Grapher.Models;
using Grapher.Utils;
using LiveChartsCore.Kernel;
using LiveChartsCore.Defaults;

namespace Grapher.ViewModels.Views
{
    public class CalculateViewModel : ViewModelBase
    {
        private string _header;
        private OperationModel _operation;
        private double _a;
        private double _b;

        public string Header
        {
            get => _header;
            set => Set(value, ref _header);
        }

        public double A
        {
            get => _a;
            set => Set(value, ref _a);
        }
        public double B
        {
            get => _b;
            set => Set(value, ref _b);
        }
        public FunctionSqulptor Squlptor { get; set; }
        public OperationModel Operation
        {
            get => _operation;
            set => Set(value, ref _operation);
        }
        public RelayCommand<MethodType> SetMethod { get; set; }
        public ObservableCollection<ISeries> Series { get; set; }
        public Axis[] XAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Name = "X Axis",
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
                    Name = "Y Axis",
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
        public CalculateViewModel()
        {
        }
        public void AddGraph(GraphViewModel viewModel)
        {
            Series = viewModel.Series;
            Squlptor = viewModel.Squlptor;
        }

        public void Execute(string executeName)
        {
            switch (executeName)
            {
                case "Метод половинного деления":
                    Header = executeName;
                    if (A == 0 && B == 0)
                    {
                        Squlptor.Bisection(-10, 10, 1e-6, 1000);
                    }
                    else
                    {
                        Squlptor.Bisection(A, B, 1e-6, 1000);
                    }
                    break;
                case "Метод Ньютона":
                    Header = executeName;
                    Squlptor.Newton(10, 1e-6, 1000);
                    break;
                case "Метод секущих":
                    Header = executeName;
                    Squlptor.Secant(A, B, 1e-6, 1000);
                    break;
                case "Метод простой итерации":
                    Header = executeName;
                    Squlptor.FixedPointIteration(1.0, 1e-6, 1000);
                    break;
                case "Нахождение корней полиномов":
                    Header = executeName;
                    Squlptor.Lagger();
                    break;
                case "Интерполяционный полином Лагранжа":
                    {
                        Header = executeName;
                        var points = Series.First(f => f.Name == Squlptor.ExpressionString).Values?.Cast<ObservablePoint>();
                        if (points is null) return; 
                        Squlptor.LagrangeInterpolate(
                            points.Select(s => s.Coordinate.SecondaryValue).ToArray(),
                            points.Select(s => s.Coordinate.PrimaryValue).ToArray(),
                            A);
                        break;
                    }
                case "Кубический сплайны":
                    {
                        Header = executeName;
                        var points = Series.First(f => f.Name == Squlptor.ExpressionString).Values?.Cast<ObservablePoint>();
                        if (points is null) return;
                        Squlptor.CubicSplineInterpolate(
                            points.Select(s => s.Coordinate.SecondaryValue).ToArray(),
                            points.Select(s => s.Coordinate.PrimaryValue).ToArray(),
                            A);
                    }
                    break;
                case "Метод наименьших квадратов":
                    {
                        Header = executeName;
                        var points = Series.First(f => f.Name == Squlptor.ExpressionString).Values?.Cast<ObservablePoint>();
                        if (points is null) return;
                        Squlptor.MinimalRectangle(
                            points.Select(s => s.Coordinate.SecondaryValue).ToArray(),
                            points.Select(s => s.Coordinate.PrimaryValue).ToArray(),
                            A);
                        break;
                    }
                case "Формулы прямоугольников":
                    Header = executeName;
                    Squlptor.Operation = new();
                    Squlptor.RectangleLeft(A, B, 2);
                    Squlptor.RectangleRight(A, B, 2);
                    Squlptor.RectangleMidpoint(A, B, 2);
                    break;
                case "Формула трапеций":
                    Header = executeName;
                    Squlptor.TrapezoidalRule(A, B, 2);
                    break;
                case "Формула Симпсона":
                    Header = executeName;
                    Squlptor.SimpsonsRuleSingleLoop(A, B, 2);
                    break;
                case "Формулы Гаусса":
                    Header = executeName;
                    Squlptor.Gauss(A, B, 2);
                    break;
                default:
                    break;
            }
            Operation = Squlptor.Operation;
        }

    }
}
