using Core.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;

namespace Core.View
{
    public class WaterBlenderColorView : MVC.View.View
    {
        [SerializeField] private Renderer renderer;

        public void SetColor(Color color)
        {
            renderer.enabled = true;
            renderer.material.color = color;
        }
        protected override IController CreateController() => new WaterBlenderColorController(this);
    }
    public class WaterBlenderColorController : Controller<WaterBlenderColorView, CreateWaterServiceLayer>
    {
        private readonly FruitsServiceLayer fruitsServiceLayer;
        private Color mainColor;
        public WaterBlenderColorController(WaterBlenderColorView view) : base(view)
        {
            fruitsServiceLayer = ServiceFactory.GetService<FruitsServiceLayer>();
        }

        protected override void HandleServiceLayer()
        {
            mainColor += fruitsServiceLayer.GetFruitsColorModel(serviceLayer.GetContext());
            View.SetColor(mainColor);
            ServiceFactory.GetService<ColorMixerServiceLayer>().UpdateDto(mainColor);
        }
    }
}