using Grapher.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher.Models
{
    public class MatrixModel : ModelBase
    {
        #region params
        private double _x11;
        private double _x21;
        private double _x31;
        private double _x41;

        private double _x12;
        private double _x22;
        private double _x32;
        private double _x42;

        private double _x13;
        private double _x23;
        private double _x33;
        private double _x43;


        public double X11
        {
            get => _x11;
            set => Set(value, ref _x11);
        }

        public double X21
        {
            get => _x21;
            set => Set(value, ref _x21);
        }

        public double X31
        {
            get => _x31;
            set => Set(value, ref _x31);
        }
        public double X41
        {
            get => _x41;
            set => Set(value, ref _x41);
        }

        public double X12
        {
            get => _x12;
            set => Set(value, ref _x12);
        }

        public double X22
        {
            get => _x22;
            set => Set(value, ref _x22);
        }

        public double X32
        {
            get => _x32;
            set => Set(value, ref _x32);
        }
        public double X42
        {
            get => _x42;
            set => Set(value, ref _x42);
        }


        public double X13
        {
            get => _x13;
            set => Set(value, ref _x13);
        }

        public double X23
        {
            get => _x23;
            set => Set(value, ref _x23);
        }

        public double X33
        {
            get => _x33;
            set => Set(value, ref _x33);
        }
        public double X43
        {
            get => _x43;
            set => Set(value, ref _x43);
        }


        #endregion


        public double[,] GetArray()
        {
            double[,] array =
            {
                { X11, X21, X31, X41 },
                { X12, X22, X32, X42 },
                { X13, X23, X33, X43 },
            };
            return array;
        }

        public double[,] GetChunkArray()
        {
            double[,] array =
            {
                { X11, X21, X31 },
                { X12, X22, X32 },
                { X13, X23, X33 },
            };
            return array;
        }

        public double[] GetVector()
        {
            double[] vector = { X41, X42, X43 };
            return vector;
        }
    }
}
