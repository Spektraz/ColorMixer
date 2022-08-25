using MVC.Factory;
using MVC.Service;

namespace MVC.Controller
{
    public abstract class Controller<T> : IController where T : View.View
    {

        protected T View { get; }

        public Controller(T view)
        {
            this.View = view;
        }

        public abstract void AddListeners();
        public abstract void RemoveListeners();

        public virtual void Execute()
        {
        }
    }

    public abstract class Controller<T, Layer> : Controller<T> where T : View.View where Layer : BaseServiceLayer
    {
        protected readonly Layer serviceLayer;

        protected Controller(T view) : base(view)
        {
            serviceLayer = ServiceFactory.GetService<Layer>();
        }

        public override void AddListeners()
        {
            serviceLayer.DtoHandler.AddListener(HandleServiceLayer);
        }

        public override void RemoveListeners()
        {
            RemoveServiceLayerListener();
        }

        protected abstract void HandleServiceLayer();

        protected void RemoveServiceLayerListener() => serviceLayer.DtoHandler.RemoveListener(HandleServiceLayer);
    }
    public interface IController
    {
        void AddListeners();
        void RemoveListeners();
        void Execute(); 
    }
}