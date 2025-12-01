using UnityEngine;

namespace Main.Shop
{
    public class Shop : MonoBehaviour
    {
        public static Shop Instance;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        private void UpdateData()
        {
            
        }

        public void Purchase(int itemId)
        {
            
        }
    }
}