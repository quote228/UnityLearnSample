using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveRaycast : MonoBehaviour
{
    public GameObject prefab; // Префаб для создания объекта

    private InteractiveBox currentInteractiveBox; // Текущий объект, на который был кликнут

    public Color rayColor = Color.red; // Цвет луча

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Левый клик мыши
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Создаем луч из позиции мыши
            RaycastHit hit;

            // Рисуем луч, чтобы он был видим в игровом окне
            Debug.DrawRay(ray.origin, ray.direction * 100, rayColor, 1f); // Длительность луча в 1 секунду

            if (Physics.Raycast(ray, out hit))
            {
                // Левый клик по объекту с тегом "InteractivePlane"
                if (hit.collider.CompareTag("InteractivePlane"))
                {
                    // Создаем объект в точке попадания луча
                    Vector3 spawnPosition = hit.point;

                    // Учитываем нормаль поверхности для корректного размещения
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                    // Создаем объект и устанавливаем его позицию и ориентацию
                    GameObject newObject = Instantiate(prefab, spawnPosition, rotation);

                    // Учитываем размер создаваемого объекта
                    Vector3 scale = newObject.transform.localScale;
                    newObject.transform.localScale = new Vector3(scale.x, scale.y, scale.z); // При необходимости можно изменить размер
                }
                // Левый клик по объекту с компонентом InteractiveBox
                else if (hit.collider.GetComponent<InteractiveBox>() != null)
                {
                    InteractiveBox interactiveBox = hit.collider.GetComponent<InteractiveBox>();

                    // Если нет текущего объекта, запоминаем его как next
                    if (currentInteractiveBox == null)
                    {
                        currentInteractiveBox = interactiveBox;
                        Debug.Log("Запомнил объект как next: " + interactiveBox.gameObject.name);
                    }
                    else
                    {
                        // Если объект уже запомнен, добавляем новый объект как next
                        currentInteractiveBox.AddNext(interactiveBox);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) // Правый клик мыши
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Создаем луч из позиции мыши
            RaycastHit hit;

            // Рисуем луч для правого клика
            Debug.DrawRay(ray.origin, ray.direction * 100, rayColor, 1f); // Длительность луча в 1 секунду

            if (Physics.Raycast(ray, out hit))
            {
                // Правый клик по объекту с компонентом InteractiveBox
                InteractiveBox interactiveBox = hit.collider.GetComponent<InteractiveBox>();
                if (interactiveBox != null)
                {
                    // Удаляем объект
                    Destroy(interactiveBox.gameObject);
                    Debug.Log("Объект с компонентом InteractiveBox был удалён");
                }
            }
        }
    }
}