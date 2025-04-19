using Grapher.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher.Models
{
    public class OperationModel : ModelBase
    {
        private string _name;
        private string _resolve;
        private string _description;

        public string Name
        {
            get => _name;
            set => Set(value, ref _name);
        }

        public string Resolve
        {
            get => _resolve;
            set => Set(value, ref _resolve);
        }

        public string Description
        {
            get => _description;
            set => Set(value, ref _description);
        }

    }
}
