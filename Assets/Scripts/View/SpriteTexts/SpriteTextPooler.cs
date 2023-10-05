using System.Collections.Generic;
using UnityEngine;

public class SpriteTextPooler : Singleton<SpriteTextPooler>
{
    [Header("Projects References")] public List<Sprite> numberSprites;
    public SpriteRenderer emptySpriteRendererPrefab;

    [Header("Variable References")] [SerializeField]
    private int maxCount = 50;

    private Queue<SpriteRenderer> spriteRenderersQueue;

    private void Awake()
    {
        spriteRenderersQueue = new Queue<SpriteRenderer>(maxCount);

        for (int i = 0; i < maxCount; i++)
        {
            var sRend = Instantiate(emptySpriteRendererPrefab);
            sRend.gameObject.SetActive(false);
            spriteRenderersQueue.Enqueue(sRend);
        }
    }

    public SpriteRenderer GetSpriteRendererFromPool(Transform parent)
    {
        if (spriteRenderersQueue.Count <= 0)
        {
            Debug.LogError("Sprite text pooler Empty");
            return null;
        }

        var spriteRend = spriteRenderersQueue.Dequeue();
        spriteRend.transform.SetParent(parent);
        spriteRend.gameObject.SetActive(true);
        return spriteRend;
    }

    public void PutBack(SpriteRenderer spriteRend)
    {
        if (spriteRenderersQueue.Count >= maxCount)
        {
            Debug.LogError("Sprite text pooler Full");
        }

        spriteRend.gameObject.SetActive(false);
        spriteRend.transform.SetParent(null);
        spriteRenderersQueue.Enqueue(spriteRend);
    }
}