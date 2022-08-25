using System.Collections.Generic;
using Core.Model;
using Core.ServiceLayer;
using DG.Tweening;
using MVC.Controller;
using MVC.Factory;
using UnityEditor.Animations;
using UnityEngine;

namespace Core.View
{
    public class CharacterAnimatorView : MVC.View.View
    {
        [SerializeField] private Animator animatorCharacter;
        private const int Duration = 4;
        private List<Vector3> posAnimator = new List<Vector3>();
        private Transform posDoor;
        private Transform middleRoom;
        private Transform posChair;
        protected override void Start()
        {
            base.Start();
            var seq = DOTween.Sequence();
            posDoor = GameObject.FindGameObjectWithTag("CharacterPointAnimator").transform; // refactor
            middleRoom = GameObject.FindGameObjectWithTag("CharacterPointMiddleAnimator").transform; //refactor
            posChair = GameObject.FindGameObjectWithTag("CharacterPointChairAnimator").transform; //refactor
            posAnimator.Add(posDoor.position);
            posAnimator.Add(middleRoom.position);
            posAnimator.Add(posChair.position);
            seq.Append(transform.DORotate(middleRoom.rotation.eulerAngles, Duration, RotateMode.Fast));
            seq.Join(transform.DOPath(posAnimator.ToArray(), Duration));
            seq.Join(transform.DORotate(posChair.rotation.eulerAngles, Duration, RotateMode.Fast));
            seq.OnComplete(Controller.Execute);
        }
        public void SetAnimator(AnimatorController animator)
        {
            animatorCharacter.runtimeAnimatorController = animator;
        }
        protected override IController CreateController() => new CharacterAnimatorController(this);
    }

    public class CharacterAnimatorController : Controller<CharacterAnimatorView, StartGameServiceLayer>
    {
        private readonly CharactersAnimatorServiceLayer charactersAnimatorServiceLayer;
        private readonly WinServiceLayer winServiceLayer;
        public CharacterAnimatorController(CharacterAnimatorView view) : base(view)
        {
            charactersAnimatorServiceLayer = ServiceFactory.GetService<CharactersAnimatorServiceLayer>();
            winServiceLayer = ServiceFactory.GetService<WinServiceLayer>();
        }

        public override void AddListeners()
        {
            base.AddListeners();
            winServiceLayer.DtoHandler.AddListener(HandleWinServiceLayer);
        }

        public override void RemoveListeners()
        {
            base.RemoveListeners();
            winServiceLayer.DtoHandler.RemoveListener(HandleWinServiceLayer);
        }

        protected override void HandleServiceLayer()
        {
            View.SetAnimator(charactersAnimatorServiceLayer.GetCharacterAnimatorModel(CharactersAnimator.Walk));
        }

        private void HandleWinServiceLayer()
        {
            View.SetAnimator(winServiceLayer.GetContext()
                ? charactersAnimatorServiceLayer.GetCharacterAnimatorModel(CharactersAnimator.Jump)
                : charactersAnimatorServiceLayer.GetCharacterAnimatorModel(CharactersAnimator.BackWalk));
        }
        public override void Execute()
        {
            base.Execute();
            View.SetAnimator(charactersAnimatorServiceLayer.GetCharacterAnimatorModel(CharactersAnimator.Idle));
            ServiceFactory.GetService<FinishAnimatorServiceLayer>().UpdateDto(true);
        }
    }
}