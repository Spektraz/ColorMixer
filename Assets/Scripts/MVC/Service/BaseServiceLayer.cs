using EventHandlerUtils;

namespace MVC.Service
{
    public abstract class BaseServiceLayer
    {
        public Handler DtoHandler { get; protected set; } = new Handler();

        public abstract bool IsInited { get; protected set; }
        public abstract void Reset();

    }
}