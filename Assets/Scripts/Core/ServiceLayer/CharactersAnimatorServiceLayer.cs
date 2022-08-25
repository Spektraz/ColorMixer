using BaseService.ModelEntity;
using Core.Model;
using MVC.Service;
using UnityEditor.Animations;

namespace Core.ServiceLayer
{
    public class CharactersAnimatorServiceLayer : ServiceLayer<CharactersAnimatorModel, int, CharactersAnimatorServiceLayer>
    {
        public AnimatorController GetCharacterAnimatorModel(CharactersAnimator charactersAnimator) =>
            ModelService.GetModel<CharactersAnimatorModel>().GetByName(charactersAnimator).CharactersAnimator;
        public override CharactersAnimatorServiceLayer GetContext()
        {
            return new CharactersAnimatorServiceLayer
            {
                
            };

        }
    }
}
