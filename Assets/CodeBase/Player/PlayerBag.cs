using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerBag : MonoBehaviour
    {
        public int currentCapacity;
        public int maxCapacity;
        
        [HideInInspector] public Vector3 offsetBag;

        public bool HaveFreeSpace() => 
            currentCapacity < maxCapacity;

        public void IncreaseValue() => 
            currentCapacity++;
        
        public void DecreaseCapacity() => 
            currentCapacity--;
        
        public void IncreaseOffset() => 
            offsetBag.y += 0.3f;
        
        public void DecreaseOffset() => 
            offsetBag.y -= 0.3f;
    }
}