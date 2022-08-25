using Core.ServiceLayer;
using DG.Tweening;
using MVC.Controller;
using UnityEngine;

namespace Core.View
{
    public class CharactersAnimatorCameraView : MVC.View.View
    {
        [SerializeField] private GameObject characterAnimator;
        [SerializeField] private Camera cameraMain;
        [SerializeField] private Transform posCameraLeft;
        [SerializeField] private Transform posCameraRight;
        [SerializeField] private Transform posCameraStandard;
        private int time;
        private bool state;
        public void Update()
        {
            if (state)
            {
                cameraMain.transform.LookAt(characterAnimator.transform);
            }
        }

        public void SetLook(int time)
        {
            characterAnimator = GameObject.FindGameObjectWithTag("Characters"); //refactor
            state = true;
            this.time = time;
            SetAnimator();
        }
        private void SetAnimator()
        {
            var Seq = DOTween.Sequence();
             Seq.AppendInterval(time);
             Seq.Append(cameraMain.transform.DOMove(posCameraLeft.position, 0, false));
             Seq.Join(cameraMain.transform.DORotate(posCameraLeft.rotation.eulerAngles, 0, RotateMode.Fast));
             Seq.AppendInterval(time);
             Seq.Append(cameraMain.transform.DOMove(posCameraRight.position, 0, false));
             Seq.Join(cameraMain.transform.DORotate(posCameraRight.rotation.eulerAngles, 0, RotateMode.Fast));
             Seq.AppendInterval(time);
             Seq.Append(cameraMain.transform.DOMove(posCameraStandard.position, 0, false));
             Seq.Join(cameraMain.transform.DORotate(posCameraStandard.rotation.eulerAngles, 0, RotateMode.Fast));
             Seq.OnComplete(SetAnimator);
        }
        protected override IController CreateController() => new CharactersAnimatorCameraController(this);

    }

    public class CharactersAnimatorCameraController : Controller<CharactersAnimatorCameraView, CreateCharacterServiceLayer>
    {
        private const int Time = 10;
        public CharactersAnimatorCameraController(CharactersAnimatorCameraView view) : base(view)
        {
        }

        protected override void HandleServiceLayer()
        {
          View.SetLook(Time);
        }
        
    }
}