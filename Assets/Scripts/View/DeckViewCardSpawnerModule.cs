using System.Collections;
using System.Collections.Generic;
using CardGame.Core;
using CardGame.View;
using UnityEngine;
using Color = UnityEngine.Color;

namespace CardGame.View
{
    public class DeckViewCardSpawnerModule : MonoBehaviour
    {
        [SerializeField] private CardViewBase cardPrefab;
        [SerializeField] private Transform cardSpawnTransform;

        private Quaternion cardSpawnRotation = Quaternion.AngleAxis(180, Vector3.up);

        public void Init()
        {
        }

        public CardViewBase SpawnCard(NumericColoredCard card,Color deckColor)
        {
           var c= Instantiate(cardPrefab, cardSpawnTransform.position,cardSpawnRotation);
           c.Init(card,deckColor);
           return c;
        }
    }
}