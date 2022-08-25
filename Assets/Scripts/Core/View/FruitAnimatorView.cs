using Core.Model;
using Core.ServiceLayer;
using DG.Tweening;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;

namespace Core.View
{
    public class FruitAnimatorView : MVC.View.View
    {
        [SerializeField] private FruitsName fruitsName;
        [SerializeField] private Transform posUpper;
        [SerializeField] private Transform posUpperBlender;
        [SerializeField] private Transform posInBlender;
        [SerializeField] private Transform otherPlace;
        public FruitsName FruitsName()
        {
            return fruitsName;
        }
        private void SetRigid()
        {
            if (gameObject.GetComponent<Rigidbody>())
            {
                return;
            }
            gameObject.AddComponent<Rigidbody>();
            Controller.Execute();
        }
        public void SetAnimation(int durationUpper, int durationBlender)
        {
            var Seq = DOTween.Sequence();
            Seq.Append(transform.DOMove(posUpper.position, durationUpper, false));
            Seq.Join(transform.DORotate(posUpper.rotation.eulerAngles, durationUpper, RotateMode.Fast));
            Seq.Append(transform.DOMove(posUpperBlender.position, durationBlender, false));
            Seq.Join(transform.DORotate(posUpperBlender.rotation.eulerAngles, durationBlender, RotateMode.Fast));
            Seq.Append(transform.DOMove(posInBlender.position, durationBlender, false));
            Seq.Join(transform.DORotate(posInBlender.rotation.eulerAngles, durationBlender, RotateMode.Fast));
            Seq.OnComplete(SetRigid);
            Seq.Append(transform.DOMove(otherPlace.position, 0, false));
        }
        protected override IController CreateController() => new FruitAnimatorController(this);
    }

    public class FruitAnimatorController : Controller<FruitAnimatorView, AddFruitServiceLayer>
    {
        private const int DurationUpper = 2;
        private const int DurationBlender = 1;
        public FruitAnimatorController(FruitAnimatorView view) : base(view)
        {
        }

        protected override void HandleServiceLayer()
        {
            if (View.FruitsName() == serviceLayer.GetContext())
            {
                View.SetAnimation(DurationUpper,DurationBlender);
            }
        }

        public override void Execute()
        {
            base.Execute();
            ServiceFactory.GetService<CreateWaterServiceLayer>().UpdateDto(View.FruitsName());
        }
    }
}