using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveBox : MonoBehaviour
{
    public InteractiveBox next; // Ссылка на следующий объект
    public float health = 100f; // Здоровье объекта, которое будет уменьшаться

    // Метод для добавления ссылки на следующий объект
    public void AddNext(InteractiveBox box)
    {
        if (next != null)
        {
            Debug.LogWarning("Next объект уже установлен!");
        }
        else
        {
            next = box;
            Debug.Log("Next объект добавлен: " + box.gameObject.name);
        }
    }

    void Update()
    {
        if (next != null)
        {
            // Стреляем лучом от текущего объекта к объекту next
            Vector3 direction = next.transform.position - transform.position;
            Debug.DrawRay(transform.position, direction, Color.red); // Рисуем луч в редакторе

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                // Если луч попал в объект с компонентом ObstacleItem
                ObstacleItem obstacle = hit.collider.GetComponent<ObstacleItem>();
                if (obstacle != null)
                {
                    // Вызываем метод GetDamage у объекта
                    obstacle.GetDamage(Time.deltaTime);
                }
            }
        }
    }
}
