using Castle.DynamicProxy;

namespace Example.Common.Intercepters
{
    public abstract class MethodInterception : BaseInterception
    {
        protected virtual void OnBefore(IInvocation invocation)
        {
        }

        protected virtual void OnAfter(IInvocation invocation)
        {
        }

        protected virtual void OnException(IInvocation invocation)
        {
        }

        protected virtual void OnSuccess(IInvocation invocation)
        {
        }

        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch
            {
                isSuccess = false;
                OnException(invocation);
                throw;
            }
            finally
            {
                if (isSuccess)
                    OnSuccess(invocation);
                OnAfter(invocation);
            }
        }
    }
}