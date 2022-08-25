using System;
using MVC.Model;
using UnityEngine;
using Utils;

namespace Core.Model
{
    [Serializable]
    [CreateAssetMenu(fileName = "FruitModel", menuName = "Model/Core/FruitModel")]
    public class FruitsModel : ScriptableObject, IModel
    {
  
        [SerializeField] private FruitsList fruitsList;
        public InternalFruitsPreset GetByName(FruitsName fruitsName) => fruitsList.GetById(fruitsName);
    }
    [Serializable]
    public class FruitsList : DataList<FruitsPreset, InternalFruitsPreset, FruitsName>
    {
    }
    [Serializable]
    public class FruitsPreset : InternalData<FruitsName, InternalFruitsPreset>
    {
    }
    [Serializable]
    public class InternalFruitsPreset
    {
        [SerializeField] private Color fruitColor;
        [SerializeField] private Sprite fruitSprite;
        public Color FruitColor => fruitColor;
        public Sprite FruitSprite => fruitSprite;
    }

    public enum FruitsName
    {
        Unset = 0,
        Apple = 1,
        Banana = 2,
        Orange = 3,
        Cherry = 4, 
        Tomato = 5,
        Broccoli = 6,
        Eggplant = 7,
    }
}