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
        [Header("Project References")]
        [SerializeField] private CardViewBase cardPrefab;
        [Header("Child References")]
        [SerializeField] private Transform cardSpawnTransform;

        // Can be transfer another script for memory allocate
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