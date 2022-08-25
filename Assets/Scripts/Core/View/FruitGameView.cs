using System;
using System.Collections.Generic;
using Core.Model;
using Core.ServiceLayer;
using MVC.Controller;
using MVC.Factory;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Core.View
{
    public class FruitGameView : MVC.View.View
    {
        [SerializeField] private List<FruitButtons> fruitButtonList;
        public readonly Dictionary<FruitsName, FruitButtons> fruitButtonsDictionary =
            new Dictionary<FruitsName, FruitButtons>();

        private bool state;
        private Dictionary<FruitsName, FruitButtons> FruitButtonsDictionary
        {
            get
            {
                if(fruitButtonsDictionary.Count == 0)
                {
                    fruitButtonList.ForEach(x => fruitButtonsDictionary.Add(x.Id, x));
                }
                return fruitButtonsDictionary;
            }
        }
        public void AddListener(FruitsName role, Action action)
        {
            if (!FruitButtonsDictionary.ContainsKey(role))
            {
                Debug.LogError($"{role} is not found");
                return;
            }
            FruitButtonsDictionary[role].AddListener(action);
        }
        public void RemoveAllListeners()
        {
            fruitButtonList.ForEach(x => x.RemoveListeners());
        }

        public void MovePos(FruitsName fruitsName)
        {
            fruitButtonsDictionary[fruitsName].MoveToPlace();
        }
        protected override IController CreateController() => new FruitGameController(this);
    }
    [Serializable]
    public class FruitButtons : ButtonsInputView<FruitsName>
    {
        [SerializeField] private List<GameObject> fruitObject;
        [SerializeField] private Image fruitImage;
        [SerializeField] private Transform posTakeFruit;
        public void MoveToPlace()
        {
            fruitObject[0].transform.localPosition = posTakeFruit.localPosition;
        }

        public void SetImage(Sprite sprite)
        {
            fruitImage.sprite = sprite;
        }
    }
    public class FruitGameController : Controller<FruitGameView, StartGameServiceLayer>
    {
        private readonly FruitsServiceLayer fruitsServiceLayer;
        public FruitGameController(FruitGameView view) : base(view)
        {
            fruitsServiceLayer = ServiceFactory.GetService<FruitsServiceLayer>();
        }

        public override void AddListeners()
        {
            base.AddListeners();
            View.AddListener(FruitsName.Apple,SetApple);
            View.AddListener(FruitsName.Banana,SetBanana);
            View.AddListener(FruitsName.Broccoli,SetBroccoli);
            View.AddListener(FruitsName.Cherry,SetCherry);
            View.AddListener(FruitsName.Eggplant,SetEggplant);
            View.AddListener(FruitsName.Orange,SetOrange);
            View.AddListener(FruitsName.Tomato,SetTomato);
            
        }
        
        public override void RemoveListeners()
        {
            base.RemoveListeners();
            View.RemoveAllListeners();
        }

        private void SetApple()
        {
            ServiceFactory.GetService<AddFruitServiceLayer>().UpdateDto(FruitsName.Apple);
            View.MovePos(FruitsName.Apple);
        }

        private void SetBanana()
        {
            ServiceFactory.GetService<AddFruitServiceLayer>().UpdateDto(FruitsName.Banana);
            View.MovePos(FruitsName.Banana);
        }
        private void SetOrange()
        {
            ServiceFactory.GetService<AddFruitServiceLayer>().UpdateDto(FruitsName.Orange);
            View.MovePos(FruitsName.Orange);
        }
        private void SetCherry()
        {
            ServiceFactory.GetService<AddFruitServiceLayer>().UpdateDto(FruitsName.Cherry);
            View.MovePos(FruitsName.Cherry);
        }
        private void SetTomato()
        {
            ServiceFactory.GetService<AddFruitServiceLayer>().UpdateDto(FruitsName.Tomato);
            View.MovePos(FruitsName.Tomato);
        }
        private void SetBroccoli()
        {
            ServiceFactory.GetService<AddFruitServiceLayer>().UpdateDto(FruitsName.Broccoli);
            View.MovePos(FruitsName.Broccoli);
        }
        private void SetEggplant()
        {
            ServiceFactory.GetService<AddFruitServiceLayer>().UpdateDto(FruitsName.Eggplant);
            View.MovePos(FruitsName.Eggplant);
        }
        protected override void HandleServiceLayer()
        {
            foreach (var var in View.fruitButtonsDictionary)
            {
                var.Value.SetImage(fruitsServiceLayer.GetFruitsSpriteModel(var.Key));
            }
        }
    }
}