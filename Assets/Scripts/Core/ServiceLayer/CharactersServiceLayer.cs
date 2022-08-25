using BaseService.ModelEntity;
using Core.Model;
using MVC.Service;
using UnityEngine;

namespace Core.ServiceLayer
{
    public class CharactersServiceLayer : ServiceLayer<CharactersModel, int, CharactersServiceLayer>
    {
        public GameObject GetFruitsSpriteModel(CharactersName charactersName) =>
            ModelService.GetModel<CharactersModel>().GetByName(charactersName).CharacterObject;
        public override CharactersServiceLayer GetContext()
        {
            return new CharactersServiceLayer
            {
                
            };

        }
    }
}
