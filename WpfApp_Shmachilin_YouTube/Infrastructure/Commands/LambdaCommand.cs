﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp_Shmachilin_YouTube.Infrastructure.Commands.Base;

namespace WpfApp_Shmachilin_YouTube.Infrastructure.Commands
{
    internal class LambdaCommand : Command
    {

        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;

        public LambdaCommand(Action<Object> Execute, Func<object, bool> CanExecute = null) 
        { 
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }

        public override bool CanExecute(object? parameter) => _CanExecute?.Invoke(parameter) ?? true;

        public override void Execute(object? parameter) => _Execute(parameter);
    }
}
