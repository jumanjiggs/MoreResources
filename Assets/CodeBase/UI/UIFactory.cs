using System.Collections;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class UIFactory : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI reachMaxText;
        [SerializeField] private TextMeshProUGUI finishNecessaryResources;
        
        public void MaximumCapacityReachedUI(GameObject obj)
        {
            reachMaxText.text = obj.name + " IS FULL !!!";
            StartCoroutine(SpawnHintUI());
        }

        public void ShowOutNecessaryResources()
        {
            StartCoroutine(SpawnHintOutResources());
        }

        private IEnumerator SpawnHintUI()
        {
            ActivateHint(reachMaxText,true);
            yield return new WaitForSeconds(2f);
            ActivateHint(reachMaxText,false);
        }
        
        private IEnumerator SpawnHintOutResources()
        {
            ActivateHint(finishNecessaryResources,true);
            yield return new WaitForSeconds(2f);
            ActivateHint(finishNecessaryResources,false);
        }

        private void ActivateHint(TextMeshProUGUI text, bool value) => 
            text.gameObject.SetActive(value);
    }
}