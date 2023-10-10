using CardGame.View.Hand;
using UnityEngine;

namespace CardGame.View.Card
{
    public class CardViewBoxCasterModule : MonoBehaviour,ICardViewFinderModule
    {
        [Header("Variable References")] [SerializeField]
        private LayerMask layerMask;

        [SerializeField] Vector3 castSize = Vector3.one;

        private RaycastHit[] result = new RaycastHit[5];

        public Tile FindClosestTile()
        {
            var hitCount =
                Physics.BoxCastNonAlloc(transform.position, castSize * 0.5f, Vector3.forward, result,
                    transform.rotation, 1, layerMask);
            if (hitCount > 0)
            {
                float maxDistance = float.MaxValue;
                Tile closestTile = null;
                for (int i = 0; i < hitCount; i++)
                {
                    if (result[i].transform.TryGetComponent(out Tile tile))
                    {
                        var distance = Mathf.Abs((transform.position - result[i].point).sqrMagnitude);
                        if (distance < maxDistance)
                        {
                            maxDistance = distance;
                            closestTile = tile;
                        }
                    }
                }

                return closestTile;
            }

            return null;
        }
    }
}
