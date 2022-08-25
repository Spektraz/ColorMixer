using MVC.Service;
using UnityEngine;

namespace Core.ServiceLayer
{
    public class ColorDrinkServiceLayer : ServiceLayer<Color,Color>
    {
        public override Color GetContext()
        {
            return dto;
        }
    }
}




