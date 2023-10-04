using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTextReferenceHolder : Singleton<SpriteTextReferenceHolder>
{
    [Header("Projects References")]
    
    public List<Sprite> numberSprites;
    
    public SpriteRenderer emptySpriteRendererPrefab;
}
