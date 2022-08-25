using System;
using System.Collections.Generic;
using MVC.Model;
using UnityEngine;
using Object = UnityEngine.Object;

#pragma warning disable 0649

namespace BaseService.ApplicationService.Model
{
    [Serializable]
    [CreateAssetMenu(fileName = "GameModel", menuName = "Model/GameModel")]
    public class GameModel : ScriptableObject
    {
        [SerializeField] protected List<ModelPreset> modelList;

        protected Dictionary<Type, IModel> modelDictionary = new Dictionary<Type, IModel>();

        protected Dictionary<Type, IModel> ModelDictionary
        {
            get
            {
                if (modelDictionary == null || modelDictionary.Count == 0)
                {
                    modelList.ForEach(x => modelDictionary.Add(x.GetType(), (IModel) x.Instance));
                }

                return modelDictionary;
            }
        }

        internal T GetModel<T>() where T : IModel
        {
            return (T) ModelDictionary[typeof(T)];
        }
        
    }

    [Serializable]
    public class ModelPreset : ModelPreset<ScriptableObject> 
    {
        public new Type GetType() => prefab.GetType();
    }

    [Serializable]
    public class ModelPreset<T> where T : ScriptableObject
    {
        [SerializeField] protected T prefab;
        private T instance;

        public T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Object.Instantiate(prefab);
                }

                return instance;
            }
        }
    }
  
}