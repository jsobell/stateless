﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stateless
{
    public partial class StateMachine<TState, TTrigger, TTag>
    {
        internal class DynamicTriggerBehaviour : TriggerBehaviour
        {
            readonly Func<object[], TState> _destination;

            public DynamicTriggerBehaviour(TTrigger trigger, Func<object[], TState> destination, TransitionGuard transitionGuard)
                : base(trigger, transitionGuard)
            {
                _destination = Enforce.ArgumentNotNull(destination, nameof(destination));
            }

            public override bool ResultsInTransitionFrom(TState source, object[] args, out TState destination)
            {
                destination = _destination(args);
                return true;
            }
        }
    }
}
