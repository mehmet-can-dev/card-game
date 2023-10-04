using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

public class SpriteText : MonoBehaviour
{
    const float MAX_SIZE = 0.5f;

    List<Vector3> positionList = new List<Vector3>();
    List<SpriteRenderer> spawnedRenderer = new List<SpriteRenderer>();

    public void SetNumber(int number)
    {
        for (int i = 0; i < spawnedRenderer.Count; i++)
        {
            Destroy(spawnedRenderer[i].gameObject);
        }

        spawnedRenderer.Clear();
        positionList.Clear();

        SpriteRenderer rendererPrefab = SpriteTextReferenceHolder.Instance.emptySpriteRendererPrefab;
        var numberList = SpriteTextReferenceHolder.Instance.numberSprites;
        var array = IntUtilities.GetIntArray(number);


        float size = 1 / (float)array.Length;
        size = Mathf.Min(size, MAX_SIZE);

        bool isEvenLenght = array.Length % 2 == 0;

        if (!isEvenLenght)
        {
            positionList.Add(Vector3.zero);
        }

        int tempSing = 1;
        int tempIndex = isEvenLenght ? 0 : 1;
        float offset = size * .5f;
        for (int i = isEvenLenght ? 0 : 1; i < array.Length; i++)
        {
            var pos = Vector3.zero;

            if (isEvenLenght)
            {
                pos.x = offset + tempIndex * size * Mathf.Sign(tempSing);
            }
            else
            {
                pos.x = tempIndex * size * Mathf.Sign(tempSing);
            }

            bool isOdd = i % 2 == 1;
            if (!isOdd)
            {
                tempIndex++;
            }

            tempSing *= -1;

            positionList.Add(pos);
        }

        positionList = positionList.OrderBy(p => p.x).ToList();

        for (int i = 0; i < array.Length; i++)
        {
            var sRend = Instantiate(rendererPrefab, transform);
            sRend.sprite = numberList[array[i]];
            sRend.transform.localScale = Vector3.one * size;
            sRend.transform.localPosition = positionList[i];
            spawnedRenderer.Add(sRend);
        }
    }
}