using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stateless
{
    public partial class StateMachine<TState, TTrigger, TTag>
    {
        internal class StateReference
        {
            public TState State { get; set; }
        }
    }
}
