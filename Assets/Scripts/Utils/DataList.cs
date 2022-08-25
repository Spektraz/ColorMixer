using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public class DataList<Value, InternalValue, Id> where Value : InternalData<Id, InternalValue>
    {
        [SerializeField] protected List<Value> list;
        private readonly Dictionary<Id, InternalValue> dictionary = new Dictionary<Id, InternalValue>();

        protected Dictionary<Id, InternalValue> Dictionary
        {
            get
            {
                if (dictionary.IsNullOrEmpty())
                {
                    list.ForEach(x => dictionary.Add(x.ID, x.Value));
                }

                return dictionary;
            }
        }
        
        protected bool IsInvalidList()
        {
            if (list.IsNullOrEmpty())
            {
                return true;
            }

            return false;
        }
        

        public virtual void SortById()
        {
            if (IsInvalidList())
            {
                return;
            }

            list = list.OrderByDescending(x => x.ID).ToList();
        }

        public virtual void Validate()
        {
            if (IsInvalidList())
            {
                return;
            }
            var dict = new Dictionary<Id, Value>();
            bool hasSameValues = false;
            foreach (var value in list)
            {
                if (dict.ContainsKey(value.ID))
                {
                    hasSameValues = true;
                    continue;
                }
                dict.Add(value.ID, value);
            }

            if (hasSameValues)
            {
                list = new List<Value>(dict.Values);

            }
        }

        public bool HasKey(Id id) => Dictionary.ContainsKey(id);

        private bool HasValue(InternalValue value) => Dictionary.ContainsValue(value);

        public InternalValue GetById(Id id)
        {
            if (HasKey(id))
            {
                return Dictionary[id];
            }
            
            return Dictionary.FirstOrDefault().Value;
        }

        public Id GetByValue(InternalValue value)
        {
            if (HasValue(value))
            {
                foreach (var valuePair in dictionary)
                {
                    if (valuePair.Value.Equals(value))
                    {
                        return valuePair.Key;
                    }
                }
            }

            return Dictionary.FirstOrDefault().Key;
        }

        public Dictionary<Id, InternalValue>.KeyCollection Keys => Dictionary.Keys;
        public Dictionary<Id, InternalValue>.ValueCollection Values => Dictionary.Values;
    }
}