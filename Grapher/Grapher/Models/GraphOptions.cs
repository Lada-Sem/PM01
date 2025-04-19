using Grapher.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher.Models
{
    public class GraphOptions : ModelBase
    {
        private int _minX;
        private int _maxX;
        private int _minY;
        private int _maxY;
        private float _points;

        public int MinX
        {
            get => _minX;
            set => Set(value, ref _minX);
        }

        public int MaxX
        {
            get => _maxX;
            set => Set(value, ref _maxX);
        }

        public int MinY
        {
            get => _minY;
            set => Set(value, ref _minY);
        }

        public int MaxY
        {
            get => _maxY;
            set => Set(value, ref _maxY);
        }

        public float Points
        {
            get => _points;
            set => Set(value, ref _points);
        }

        public GraphOptions()
        {
            MinY = -20;
            MinX = -20;
            MaxY = 20;
            MaxX = 20;
            Points = 0.1f;
        }
    }
}
