using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseService.Thread
{
    public class UnityMainThreadDispatcher
    {

        private readonly Queue<Action> executionQueue = new Queue<Action>();

       
       
        public void Update()
        {
            lock (executionQueue)
            {
                while (executionQueue.Count > 0)
                {
                    executionQueue.Dequeue().Invoke();
                }
            }
        }

        private void Enqueue(IEnumerator action)
        {
            lock (executionQueue)
            {
                executionQueue.Enqueue(() => { ApplicationService.Service.ApplicationService.Instance.StartCoroutine(action); });
            }
        }

        public void Enqueue(Action action)
        {
            Enqueue(ActionWrapper(action));
        }
        
        private Task EnqueueAsync(Action action)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            void WrappedAction()
            {
                try
                {
                    action();
                    taskCompletionSource.TrySetResult(true);
                }
                catch (Exception ex)
                {
                    taskCompletionSource.TrySetException(ex);
                }
            }

            Enqueue(ActionWrapper(WrappedAction));
            return taskCompletionSource.Task;
        }


        private IEnumerator ActionWrapper(Action action)
        {
            action();
            yield return null;
        }
    }
}