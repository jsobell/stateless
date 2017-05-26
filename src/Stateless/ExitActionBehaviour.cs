﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stateless
{
    public partial class StateMachine<TState, TTrigger, TTag>
    {
        internal abstract class ExitActionBehavior
        {
            readonly Reflection.InvocationInfo _actionDescription;

            public abstract void Execute(Transition transition);
            public abstract Task ExecuteAsync(Transition transition);

            protected ExitActionBehavior(Reflection.InvocationInfo actionDescription)
            {
                _actionDescription = Enforce.ArgumentNotNull(actionDescription, nameof(actionDescription));
            }

            internal Reflection.InvocationInfo Description => _actionDescription;

            public class Sync : ExitActionBehavior
            {
                readonly Action<Transition> _action;

                public Sync(Action<Transition> action, Reflection.InvocationInfo actionDescription) : base(actionDescription)
                {
                    _action = action;
                }

                public override void Execute(Transition transition)
                {
                    _action(transition);
                }

                public override Task ExecuteAsync(Transition transition)
                {
                    Execute(transition);
                    return TaskResult.Done;
                }
            }

            public class Async : ExitActionBehavior
            {
                readonly Func<Transition, Task> _action;

                public Async(Func<Transition, Task> action, Reflection.InvocationInfo actionDescription) : base(actionDescription)
                {
                    _action = action;
                }

                public override void Execute(Transition transition)
                {
                    throw new InvalidOperationException(
                        $"Cannot execute asynchronous action specified in OnExit event for '{transition.Source}' state. " +
                         "Use asynchronous version of Fire [FireAsync]");
                }

                public override Task ExecuteAsync(Transition transition)
                {
                    return _action(transition);
                }
            }
        }
    }
}
