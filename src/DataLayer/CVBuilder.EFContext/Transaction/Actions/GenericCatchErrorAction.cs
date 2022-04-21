using System;

namespace CVBuilder.EFContext.Transaction.Actions
{
    public class GenericCatchErrorAction : ActionBase
    {
        private readonly string _catchErrorMessage;
        private Action Action { get; }

        public GenericCatchErrorAction(Action action, string catchErrorMessage)
        {
            if (string.IsNullOrWhiteSpace(catchErrorMessage))
            {
                throw new ArgumentNullException(nameof(catchErrorMessage));
            }

            _catchErrorMessage = catchErrorMessage;

            Action = action
                     ?? throw new ArgumentNullException(nameof(action));
        }

        public override void Execute()
        {
            try
            {
                Action();
            }
            catch (Exception ex)
            {
                //LogHolder.MainLog.Error(ex, CatchErrorMessage);
            }
        }
    }
}
