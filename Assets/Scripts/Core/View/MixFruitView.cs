using System;
using Core.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Core.View
{
    public class MixFruitView : MVC.View.View
    {
        [SerializeField] private Button mixButton;
        [SerializeField] private Image backImage;
        [SerializeField] private TextMeshProUGUI textNumber;
        [SerializeField] private TextMeshProUGUI textMesh;
        [SerializeField] private TextMeshProUGUI textResult;
        private float time = 5;
        private bool state;

        public void Update()
        {
            if (!state) return;
            time -= Time.deltaTime;
            if (!(time <= 0)) return;
            Controller.Execute();
            state = false;
        }
        public void AddListener(Action action)
        {
            mixButton.onClick.AddListener(action.Invoke);
        }

        public void RemoveListener()
        {
            mixButton.onClick.RemoveAllListeners();
        }

        public void SetText(int number, string text)
        {
            backImage.enabled = true;
            textMesh.enabled = true;
            textNumber.enabled = true;
            textResult.enabled = true;
            textNumber.text = number.ToString();
            textResult.text = text;
        }

        public void SetButton()
        {
            mixButton.enabled = false;
        }

        public void SetState()
        {
            state = true;
        }
        protected override IController CreateController() => new MixFruitController(this);
    }

    public class MixFruitController : Controller<MixFruitView,ColorDrinkServiceLayer>
    {
        private const int AllCount = 300;
        private const int HowFruit = 3;
        private const int MaxWin = 45;
        private readonly ColorMixerServiceLayer colorMixerServiceLayer;
        private bool state;
        public MixFruitController(MixFruitView view) : base(view)
        {
            colorMixerServiceLayer = ServiceFactory.GetService<ColorMixerServiceLayer>();
        }

        public override void AddListeners()
        {
            base.AddListeners();
           View.AddListener(SetButton);
        }

        public override void RemoveListeners()
        {
            base.RemoveListeners();
           View.RemoveListener();
        }

        protected override void HandleServiceLayer()
        {
        }
        private void SetButton()
        {
            View.SetButton(); 
            var result = ColorUtils.ResultColor(colorMixerServiceLayer.GetContext(), serviceLayer.GetContext());
            if (result < MaxWin)
            {
                var resultText = (int)(AllCount - result) / HowFruit;
                View.SetText(resultText, "Win");
                View.SetState();
                state = true;
            }
            else
            {
                var resultText = (int)(AllCount - result) / HowFruit;
                View.SetText(resultText, "Loose");
                View.SetState();
                state = false;
            }
        }
        public override void Execute()
        {
            base.Execute();
            ServiceFactory.GetService<WinServiceLayer>().UpdateDto(state);
        }
        
    }
}