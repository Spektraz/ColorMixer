using System;
using MVC.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace MVC.View
{
    public abstract class View : MonoBehaviour
    {

        protected IController Controller
        {
            get
            {
                if (controller == null)
                {
                    controller = CreateController();
                    OnControllerCreate(controller);
                }

                return controller;
            }
        }
    
        private IController controller;

        protected virtual void Start()
        {
            Controller.AddListeners();
        }

        protected virtual void OnDestroy()
        {
            controller?.RemoveListeners();
        }

        protected abstract IController CreateController();
        
        protected virtual void OnControllerCreate(IController controller){}

      
    }
    public abstract class View<T> : View
    {
        public abstract void SetContext(T context);
    }
    public abstract class ButtonMonobehaviour : MonoBehaviour
    {
        [SerializeField] private Button button;

        public virtual void AddListener(Action action) => button.onClick.AddListener(action.Invoke);
        public virtual void RemoveListener(Action action) => button.onClick.RemoveListener(action.Invoke);
    
    }
}