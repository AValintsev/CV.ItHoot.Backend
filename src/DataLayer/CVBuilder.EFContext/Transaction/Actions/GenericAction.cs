using System;

namespace CVBuilder.EFContext.Transaction.Actions
{
    public class GenericAction : ActionBase
    {
        private Action Action { get; set; }

        public GenericAction(Action action)
        {
            Action = action
                     ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Execute()
        {
            Action();
        }
    }
}
