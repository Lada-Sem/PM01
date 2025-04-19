using Grapher.Common;
using Grapher.Models;
using Grapher.Properties;
using Grapher.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Grapher.ViewModels.Views
{
    public class SLAUViewModel : ViewModelBase
    {
        public SLAUSqulptor Squlptor { get; set; }
        public MatrixModel Matrix { get; set; }
        private OperationModel _operation;
        private string _theory;

        public OperationModel Operation
        {
            get => _operation;
            set => Set(value, ref _operation);
        }
        public string Theory
        {
            get => _theory;
            set => Set(value, ref _theory);
        }
        public SLAUViewModel()
        {
            Squlptor = new();
            Matrix = new();
        }
        public void SelectMethod(string executeName)
        {
            switch (executeName)
            {
                case "Метод Гаусса":
                    Theory = Resources.SLAU_GAUSS;
                    Squlptor.SolveGauss(Matrix.GetArray());
                    break;
                case "Метод Жордана-Гаусса":
                    Theory = Resources.SLAU_JORDAN_GAUSS;
                    Squlptor.SolveJordanGauss(Matrix.GetArray());
                    break;
                case "Метод простой итерации":
                    Theory = Resources.SLAU_ITERATION;
                    Squlptor.SolveIteration(Matrix.GetChunkArray(), Matrix.GetVector());
                    break;
                case "Метод Зейделя":
                    Theory = Resources.SLAU_ZAIDEL;
                    Squlptor.SolveZaidel(Matrix.GetChunkArray(), Matrix.GetVector());
                    break;
                default:
                    break;
            }
            Operation = Squlptor.Operation;
        }
    }
}
