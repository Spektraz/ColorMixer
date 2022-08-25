using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    [Serializable]
    public class ButtonInputView<ID> :BaseInputView<ID, Button> where ID : struct
    {
        public virtual void AddListener(Action action)
        {
            button.onClick.AddListener(action.Invoke);
        }

        public override void RemoveListeners()
        {
            button.onClick.RemoveAllListeners();
        }
        
    }
    [Serializable]
    public class ButtonsInputView<ID> :BaseInputView<ID, List<Button>> where ID : struct
    {
        public virtual void AddListener(Action action)
        {
            foreach (var var in button)
            {
                var.onClick.AddListener(action.Invoke);   
            }
        }

        public override void RemoveListeners()
        {
            foreach (var var in button)
            {
                var.onClick.RemoveAllListeners();   
            }
        }
        
    }
    [Serializable]
    public abstract class BaseInputView<ID,INPUT> where ID : struct
    {
        [SerializeField] protected ID id;
        [SerializeField] protected INPUT button;

        public ID Id => id;

        public abstract void RemoveListeners();
        

    }
}