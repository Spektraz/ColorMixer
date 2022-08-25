using Core.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;
using UnityEngine.UI;

namespace Core.View
{
    public class CharacterDrinkCreateView : MVC.View.View
    {
        [SerializeField] private Canvas canvasDrink;
        [SerializeField] private Image imageDrink;
        public void SetCanvas(bool state)
        {
            canvasDrink.enabled = state;
        }
        public void SetDrink(Color color)
        {
            imageDrink.color = color;
        }
        protected override IController CreateController() => new CharacterDrinkCreateController(this);
    }

    public class CharacterDrinkCreateController : Controller<CharacterDrinkCreateView, FinishAnimatorServiceLayer>
    {
        private const float RandomR = 2.0F;
        public CharacterDrinkCreateController(CharacterDrinkCreateView view) : base(view)
        {
        }

        protected override void HandleServiceLayer()
        {
            View.SetCanvas(true);
            SetColor();
        }

        private void SetColor()
        {
           var green = Random.Range(0,RandomR);
           var blue = Random.Range(0, RandomR);
           var red = Random.Range(0, RandomR);
           var color = new Color(red,green,blue);
           View.SetDrink(color);
           ServiceFactory.GetService<ColorDrinkServiceLayer>().UpdateDto(color);
        }
    }
}