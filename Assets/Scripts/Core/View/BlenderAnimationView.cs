using Core.ServiceLayer;
using DG.Tweening;
using MVC.Controller;
using UnityEngine;

namespace Core.View
{
    public class BlenderAnimationView: MVC.View.View
    {
        [SerializeField] private Transform lidPosition;
        [SerializeField] private Transform lidOtherPosition;
        [SerializeField] private Transform lidStartPosition;
        [SerializeField] private Transform jugPosition;
        

        public void SetAnimator(int posAdd, float time, float duration)
        {
             var seq = DOTween.Sequence();
             seq.Append(lidPosition.transform.DOMove(lidOtherPosition.position, posAdd, false));
             seq.Join(lidPosition.transform.DORotate(lidOtherPosition.rotation.eulerAngles, posAdd, RotateMode.Fast));
             seq.AppendInterval(time);
             var position = jugPosition.position;
             seq.Append(jugPosition.transform.DORotate(new Vector3(position.x + posAdd, position.y,
                 position.z + posAdd), duration, RotateMode.Fast));
             seq.Append(jugPosition.transform.DORotate(new Vector3(position.x - posAdd, position.y,
                 position.z + posAdd), duration, RotateMode.Fast));
             seq.Append(jugPosition.transform.DORotate(new Vector3(position.x - posAdd, position.y,
                 position.z - posAdd), duration, RotateMode.Fast));
             seq.Append(jugPosition.transform.DORotate(new Vector3(position.x, position.y,
                 position.z), duration, RotateMode.Fast));
             seq.AppendInterval(time);
             seq.Append(lidPosition.transform.DOMove(lidStartPosition.position, duration, false));
             seq.Join(lidPosition.transform.DORotate(lidStartPosition.rotation.eulerAngles, duration, RotateMode.Fast));
        }
        protected override IController CreateController() => new BlenderAnimationController(this);

    }

    public class BlenderAnimationController : Controller<BlenderAnimationView, AddFruitServiceLayer>
    {
        private const int PosAdd = 2;
        private const float Time = 1.5f;
        private const float Duration = 0.2f;
        public BlenderAnimationController(BlenderAnimationView view) : base(view)
        {
        }

        protected override void HandleServiceLayer()
        {
            View.SetAnimator(PosAdd, Time,Duration);
        }
        
    }
}