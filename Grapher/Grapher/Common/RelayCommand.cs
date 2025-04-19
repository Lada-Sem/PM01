using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher.Common
{
    public class RelayCommand<T> : CommandBase
    {
        private readonly Action<T> _action;
        public RelayCommand(Action<T> action)
        {
            _action = action;
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            _action((T)parameter);
        }
    }

    public class RelayCommand : CommandBase
    {
        private readonly Action _action;
        public RelayCommand(Action action)
        {
            _action = action;
        }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            _action();
        }
    }

}
