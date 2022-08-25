using BaseService.Initializator;
using BaseService.RegistryEntity.Service;
using BaseService.Thread;
using UnityEngine;

#pragma warning disable 0649

namespace BaseService.ApplicationService.Service
{
    public class ApplicationService : MonoBehaviour
    {
        public static ApplicationService Instance { get; set; }

        /// <summary>
        /// Component that allow invoke code with unity api from background thread  -> alternative is concurent queue
        /// </summary>
        private LazyInitializator<UnityMainThreadDispatcher> unityMainThreadDispatcher =
            new LazyInitializator<UnityMainThreadDispatcher>();
        

        private void Awake()
        {
            if (Instance == null)
            {
                DeployMainSystems();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void DeployMainSystems()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            RegistryService.Init();
        }

        private void Update()
        {
            unityMainThreadDispatcher.Value.Update();
        }
    }
}