namespace BaseService.Initializator
{
    public class LazyInitializator<T> where T : new()
    {
        private T value;
        public T Value
        {
            get
            {
                if (value == null)
                {
                    value = new T();
                }

                return value;
            }
        }
    }
}