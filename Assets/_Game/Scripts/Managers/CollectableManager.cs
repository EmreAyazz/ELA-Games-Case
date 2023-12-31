using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collectable
{
    public class CollectableManager
    {
        public static int coin;
        public static int shield;

        public delegate void CoinChanged();
        public static event CoinChanged OnCoinChanged;

        public delegate void ShieldChanged();
        public static event ShieldChanged OnShieldChanged;

        public static void AddCoin(int count) => Coin(count);
        public static void RemoveCoin(int count) => Coin(-count);

        public static void AddShield(int count) => Shield(count);
        public static void RemoveShield(int count) => Shield(-count);

        public static void Coin(int count)
        {
            coin += count;

            GameActor.Instance.moneyText.text = $"${coin}";

            PlayerPrefs.SetInt("Coin", coin);

            //OnCoinChanged.Invoke();
        }
        public static void Shield(int count)
        {
            shield += count;

            GameActor.Instance.shieldText.text = $"Shield : {shield}";

            PlayerPrefs.SetInt("Shield", shield);

            //OnShieldChanged.Invoke();
        }


        public static bool CoinOkay(int count)
        {
            if (coin >= count) return true;
            else return false;
        }

        public static void Load()
        {
            if (PlayerPrefs.HasKey("Coin")) coin = PlayerPrefs.GetInt("Coin");
            else coin = 100;

            GameActor.Instance.moneyText.text = $"${coin}";
        }
    }
}
