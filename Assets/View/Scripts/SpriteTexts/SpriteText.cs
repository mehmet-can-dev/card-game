using System.Collections.Generic;
using System.Linq;
using CardGame.View.Utilities;
using UnityEngine;

namespace CardGame.View.SpriteTexts
{
    public class SpriteText : MonoBehaviour
    {
        const float MAX_SIZE = 0.5f;

        List<Vector3> positionList = new List<Vector3>();
        List<SpriteRenderer> spawnedRenderer = new List<SpriteRenderer>();

        public void SetNumber(int number)
        {
            ClearOldStates();

            var numberList = SpriteTextPooler.Instance.numberSprites;
            var array = IntUtilities.GetIntArray(number);

            var size = CalculateSize(array);

            CalculateSpritePosition(array, size);

            positionList = positionList.OrderBy(p => p.x).ToList();

            CreateSpriteRenderers(array, numberList, size);
        }

        private void CreateSpriteRenderers(int[] array, List<Sprite> numberList, float size)
        {
            for (int i = 0; i < array.Length; i++)
            {
                var sRend = SpriteTextPooler.Instance.GetSpriteRendererFromPool(transform);
                sRend.sprite = numberList[array[i]];
                sRend.transform.localScale = Vector3.one * size;
                sRend.transform.localPosition = positionList[i];
                sRend.transform.localRotation = Quaternion.identity;
                spawnedRenderer.Add(sRend);
            }
        }

        private void CalculateSpritePosition(int[] array, float size)
        {
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
        }

        private static float CalculateSize(int[] array)
        {
            float size = 1 / (float)array.Length;
            size = Mathf.Min(size, MAX_SIZE);
            return size;
        }

        private void OnDisable()
        {
            ClearOldStates();
        }

        private void ClearOldStates()
        {
            if (SpriteTextPooler.Instance == null)
                return;

            for (int i = 0; i < spawnedRenderer.Count; i++)
            {
                SpriteTextPooler.Instance.PutBack(spawnedRenderer[i]);
            }

            spawnedRenderer.Clear();
            positionList.Clear();
        }
    }
}