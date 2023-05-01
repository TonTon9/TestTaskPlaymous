using Road;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CompletedTileUIItem : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _tileName;
        
        [SerializeField]
        private TextMeshProUGUI _countOfCompletedTiles;

        public void Init(SimpleTile tile, int count)
        {
            _tileName.text = tile.TileName;
            _countOfCompletedTiles.text = count.ToString();
        }
    }
}
