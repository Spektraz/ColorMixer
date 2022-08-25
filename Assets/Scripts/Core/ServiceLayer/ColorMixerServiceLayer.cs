using MVC.Service;
using UnityEngine;

namespace Core.ServiceLayer
{
    public class ColorMixerServiceLayer : ServiceLayer<Color,Color>
    {
        public override Color GetContext()
        {
            return dto;
        }
    }
}



