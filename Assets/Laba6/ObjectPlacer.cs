using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public GameObject prefab; // Префаб, который будет создаваться
    public string targetTag = "InteractivePlane"; // Тег поверхности для создания объекта

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Проверяем клик левой кнопкой мыши
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Создаем луч из позиции мыши
            RaycastHit hit;

            // Проверяем, не попал ли луч в объект с нужным тегом
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag(targetTag)) // Если объект с тегом "InteractivePlane"
                {
                    // Создаем объект в точке попадания луча
                    Vector3 spawnPosition = hit.point;

                    // Учитываем нормаль поверхности для корректного размещения
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                    // Создаем объект и устанавливаем его позицию и ориентацию
                    GameObject newObject = Instantiate(prefab, spawnPosition, rotation);

                    // Учитываем размер создаваемого объекта (если требуется)
                    Vector3 scale = newObject.transform.localScale;
                    newObject.transform.localScale = new Vector3(scale.x, scale.y, scale.z); // При необходимости можно изменить размер
                }
            }
        }
    }
}
