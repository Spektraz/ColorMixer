using System;
using MVC.Model;
using UnityEditor.Animations;
using UnityEngine;
using Utils;

namespace Core.Model
{
    [Serializable]
    [CreateAssetMenu(fileName = "CharactersAnimatorModel", menuName = "Model/Core/CharactersAnimatorModel")]
    public class CharactersAnimatorModel : ScriptableObject, IModel
    {
  
        [SerializeField] private CharactersAnimatorList charactersAnimatorList;
        public InternalCharactersAnimatorPreset GetByName(CharactersAnimator charactersAnimator) => charactersAnimatorList.GetById(charactersAnimator);
    }
    [Serializable]
    public class CharactersAnimatorList : DataList<CharactersAnimatorPreset, InternalCharactersAnimatorPreset, CharactersAnimator>
    {
    }
    [Serializable]
    public class CharactersAnimatorPreset : InternalData<CharactersAnimator, InternalCharactersAnimatorPreset>
    {
    }
    [Serializable]
    public class InternalCharactersAnimatorPreset
    {
        [SerializeField] private AnimatorController charactersAnimator;
        public AnimatorController CharactersAnimator => charactersAnimator;
    }

    public enum CharactersAnimator
    {
        Unset = 0,
        Idle = 1,
        Run = 2,
        Walk = 3,
        Jump = 4,
        BackWalk = 5,
    }
}