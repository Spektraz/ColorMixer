using System;
using System.Collections.Generic;
using Core.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Core.View
{
    public class GameCanvasView : MVC.View.View
    {
        [SerializeField] private List<GameCanvasButtons> gameCanvasButtonsList;
        private readonly Dictionary<CanvasType, GameCanvasButtons> gameCanvasButtonsDictionary =
            new Dictionary<CanvasType, GameCanvasButtons>();

        private bool state;
        private Dictionary<CanvasType, GameCanvasButtons> GameCanvasButtonsDictionary
        {
            get
            {
                if(gameCanvasButtonsDictionary.Count == 0)
                {
                    gameCanvasButtonsList.ForEach(x => gameCanvasButtonsDictionary.Add(x.Id, x));
                }
                return gameCanvasButtonsDictionary;
            }
        }
        public void AddListener(CanvasType role, Action action)
        {
            if (!GameCanvasButtonsDictionary.ContainsKey(role))
            {
                Debug.LogError($"{role} is not found");
                return;
            }
            GameCanvasButtonsDictionary[role].AddListener(action);
        }
        public void RemoveAllListeners()
        {
            gameCanvasButtonsList.ForEach(x => x.RemoveListeners());
        }

        public void SetCanvas(CanvasType canvasType)
        {
            foreach (var var in gameCanvasButtonsList)
            {
                var.SetCanvas(false);
            }
            if (canvasType == CanvasType.StartCanvas)
                return;
            gameCanvasButtonsDictionary[canvasType].SetCanvas(true);
        }
        protected override IController CreateController() => new GameCanvasController(this);
    }
    [Serializable]
    public class GameCanvasButtons : ButtonsInputView<CanvasType>
    {
        [SerializeField] private Canvas canvas;
        public void SetCanvas(bool state)
        {
            canvas.enabled = state;
        }
    }
    public class GameCanvasController : Controller<GameCanvasView, FinishAnimatorServiceLayer>
    {
        private readonly WinServiceLayer winServiceLayer;
        public GameCanvasController(GameCanvasView view) : base(view)
        {
            winServiceLayer = ServiceFactory.GetService<WinServiceLayer>();
        }

        public override void AddListeners()
        {
            base.AddListeners();
            winServiceLayer.DtoHandler.AddListener(HandleWinServiceLayer);
            View.AddListener(CanvasType.StartCanvas, StartGame);
            View.AddListener(CanvasType.FinishNextCanvas, FinishNextCanvas);
            View.AddListener(CanvasType.FinishResetCanvas, FinishResetCanvas);
            View.AddListener(CanvasType.FruitCanvas, FruitGame);
        }
        public override void RemoveListeners()
        {
            base.RemoveListeners();
            winServiceLayer.DtoHandler.RemoveListener(HandleWinServiceLayer);
            View.RemoveAllListeners();
        }
        
        protected override void HandleServiceLayer()
        {
            View.SetCanvas(CanvasType.FruitCanvas);
        }

        private void HandleWinServiceLayer()
        {
            if (winServiceLayer.GetContext())
            {
                NextGame();
            }
            else
            {
                ResetGame();
            }
        }
        private void NextGame()
        {
            View.SetCanvas(CanvasType.NextCanvas);
        }
        private void ResetGame()
        {
            View.SetCanvas(CanvasType.ResetCanvas);
        }
        private void FruitGame()
        {
            View.SetCanvas(CanvasType.FruitCanvas);
        }
        private void StartGame()
        {
            View.SetCanvas(CanvasType.StartCanvas);
            ServiceFactory.GetService<StartGameServiceLayer>().UpdateDto(true);
        }

        private void FinishResetCanvas()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            ServiceFactory.ResetServiceLayer();
        }
        
        private void FinishNextCanvas()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            ServiceFactory.ResetServiceLayer();
        }
    }

    public enum CanvasType
    {
        NextCanvas = 0,
        StartCanvas = 1,
        ResetCanvas = 2,
        FruitCanvas = 3,
        FinishResetCanvas = 4,
        FinishNextCanvas = 5,
    }
}
