using System;
using System.Collections;
using System.Collections.Generic;
using MVC.Model;
using UnityEngine;
using Utils;

namespace Core.Model
{
    [Serializable]
    [CreateAssetMenu(fileName = "CharacterModel", menuName = "Model/Core/CharacterModel")]
    public class CharactersModel : ScriptableObject, IModel
    {
  
        [SerializeField] private CharactersList charactersList;
        public InternalCharactersPreset GetByName(CharactersName charactersName) => charactersList.GetById(charactersName);
    }
    [Serializable]
    public class CharactersList : DataList<CharactersPreset, InternalCharactersPreset, CharactersName>
    {
    }
    [Serializable]
    public class CharactersPreset : InternalData<CharactersName, InternalCharactersPreset>
    {
    }
    [Serializable]
    public class InternalCharactersPreset
    {
        [SerializeField] private GameObject characterObject;
        public GameObject CharacterObject => characterObject;
    }

    public enum CharactersName
    {
        Unset = 0,
        BusinessWoman = 1,
        BusinessMan = 2,
        CleanerMan = 3,
        CleanerWoman = 4, 
        DeveloperMan = 5,
        DeveloperWoman = 6,
        SecurityMan = 7,
    }
    
}