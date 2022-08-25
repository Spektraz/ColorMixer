using Core.Model;
using Core.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;

namespace Core.View
{
    public class CharactersRandomView : MVC.View.View
    {
        [SerializeField] private Transform posCreateCharacter;
        public void CreateCharacter(GameObject characterObject)
        {
            var position = posCreateCharacter.position;
            Instantiate(characterObject, new Vector3(position.x, position.y, position.z), Quaternion.identity);
        }

        protected override IController CreateController() => new CharacterRandomController(this);
    }

    public class CharacterRandomController : Controller<CharactersRandomView, StartGameServiceLayer>
    {
        private const int RandomR = 8;
        private readonly CharactersServiceLayer charactersServiceLayer;
        public CharacterRandomController(CharactersRandomView view) : base(view)
        {
            charactersServiceLayer = ServiceFactory.GetService<CharactersServiceLayer>();
        }
        protected override void HandleServiceLayer()
        {
            View.CreateCharacter(RandomCharacter());
            ServiceFactory.GetService<CreateCharacterServiceLayer>().UpdateDto(true);
        }

        private GameObject RandomCharacter()
        {
            var random = Random.Range(0, RandomR);
            var randomCharacter = charactersServiceLayer.GetFruitsSpriteModel((CharactersName) random);
            return randomCharacter;
        }
    }
}