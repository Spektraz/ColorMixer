using System;
using System.Linq;
using UnityEngine;

namespace BaseService.RegistryEntity.Service
{
    public static class RegistryService
    {
        public static void Init()
        {
            // ServiceFactory.GetService<BestScoreSaveServiceLayer>().UpdateDto(GetStoreMax());
            // ServiceFactory.GetService<DiamondAllScoreServiceLayer>().UpdateDto(GetDiamondMax());
            // ServiceFactory.GetService<SaveBallsServiceLayer>().UpdateDto(GetBallsMax());
        }
        
        public static int GetStoreMax()
        {
            if (RegistryUtils.HasKeys(RegistryType.ScoreMax))
            {
                return RegistryUtils.GetIntKey(RegistryType.ScoreMax);
            }

            return 0;
        }
        public static int GetDiamondMax()
        {
            if (RegistryUtils.HasKeys(RegistryType.ScoreDiamond))
            {
                return RegistryUtils.GetIntKey(RegistryType.ScoreDiamond);
            }

            return 0;
        }

        // public static BallsName GetBallsMax()
        // {
        //     if (RegistryUtils.HasKeys(RegistryType.BuyBalls))
        //     {
        //     }
        //
        //     return BallsName.Hobbies;
        // }
        public static void SaveDiamond(int registryType)
        {
            RegistryUtils.UpdateKey(RegistryType.ScoreDiamond, registryType);
            RegistryUtils.Save();
        }
        // public static void Save(int registryType)
        // {
        //     ServiceFactory.GetService<BestScoreSaveServiceLayer>().UpdateDto(registryType);
        //     RegistryUtils.UpdateKey(RegistryType.ScoreMax, registryType);
        //     RegistryUtils.Save();
        // }
    }
    public static class RegistryUtils
    {
        
        public static bool HasKeys(params RegistryType[] registryTypeArray)
        {
            return registryTypeArray.ToList().TrueForAll(HasKey);
        }

        private static bool HasKey(RegistryType registryType)
        {
            var result = PlayerPrefs.HasKey(GenerateKey(registryType));
            return result;
        }

        public static void UpdateKey(RegistryType key, bool value)
        {
            InternalUpdateKey(key, (int) Convert.ToUInt32(value));
        }

        public static void UpdateKey(RegistryType key, int value)
        {
            InternalUpdateKey(key, value);
        }
        public static void Save() => PlayerPrefs.Save();

        public static string GetStringKey(RegistryType key)
        {
            return PlayerPrefs.GetString(GenerateKey(key));
        }

        public static int GetIntKey(RegistryType key)
        {
            return PlayerPrefs.GetInt(GenerateKey(key));
        }

        public static bool GetBoolKey(RegistryType key)
        {
            var value = PlayerPrefs.GetInt(GenerateKey(key));
            return value != 0;
        }
        
        private static void InternalUpdateKey(RegistryType key, int value)
        {
            PlayerPrefs.SetInt(GenerateKey(key), value);
        }
        private static string GenerateKey(RegistryType key) => key.ToString();
        public static void DeleteAllKeys()
        {
            PlayerPrefs.DeleteAll();
        }
    }
    public enum RegistryType
    {
        Unset = 0,
        AudioActiveState = 1,
        ScoreMax = 2,
        BuyBalls = 3,
        ScoreDiamond = 4,
    }
}