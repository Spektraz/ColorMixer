using BaseService.ApplicationService.Model;
using MVC.Model;
using UnityEngine;

namespace BaseService.ModelEntity
{
    public static class ModelService
    {
        private static GameModel gameModel;

        private static GameModel Model
        {
            get
            {
                if (gameModel == null)
                {
                    gameModel = Resources.Load<GameModel>($"{ApplicationConfiguration.GameModelPath}");
                }
                return gameModel;
            }
        }

        public static T GetModel<T>() where T : IModel
        {
            return Model.GetModel<T>();
        }
    }
}