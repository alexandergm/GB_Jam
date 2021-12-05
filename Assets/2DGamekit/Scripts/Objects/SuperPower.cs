using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Gamekit2D
{
    public class SuperPower : MonoBehaviour, IDataPersister
    {
        public string[] requiredInventoryItemKeys;  
        
        public InventoryController characterInventory;        
        public UnityEvent superPower;
        public DataSettings dataSettings;

        public bool ready;

        [ContextMenu("Update State")]       

        void CheckInventory()
        {
            var stateIndex = -1;
            foreach (var i in requiredInventoryItemKeys)
            {
                if (characterInventory.HasItem(i))
                {
                    stateIndex++;
                }
            }
            if (stateIndex >= 0 || ready)
            {
                ready = true;
                if (stateIndex == requiredInventoryItemKeys.Length - 1)
                {
                    superPower.Invoke();                    
                }
            }
        }

        void OnEnable()
        {           
            characterInventory.OnInventoryLoaded += CheckInventory;
        }

        void Update()
        {
            CheckInventory();  
        }

        public DataSettings GetDataSettings()
        {
            return dataSettings;
        }

        public void LoadData(Data data)
        {
            var d = data as Data<bool>;
            ready = d.value;
        }

        public Data SaveData()
        {
            return new Data<bool>(ready);
        }

        public void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType)
        {
            dataSettings.dataTag = dataTag;
            dataSettings.persistenceType = persistenceType;
        }


    }
}