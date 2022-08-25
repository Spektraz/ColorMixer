using BaseService.ModelEntity;
using Core.Model;
using MVC.Service;
using UnityEngine;

namespace Core.ServiceLayer
{
    public class FruitsServiceLayer : ServiceLayer<FruitsModel, int, FruitsServiceLayer>
    {
        public Sprite GetFruitsSpriteModel(FruitsName fruitsName) =>
            ModelService.GetModel<FruitsModel>().GetByName(fruitsName).FruitSprite;
        public Color GetFruitsColorModel(FruitsName fruitsName) =>
            ModelService.GetModel<FruitsModel>().GetByName(fruitsName).FruitColor;
        public override FruitsServiceLayer GetContext()
        {
            return new FruitsServiceLayer
            {
                
            };

        }
    }
}
