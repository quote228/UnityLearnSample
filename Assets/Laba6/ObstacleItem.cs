using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleItem : MonoBehaviour
{
    public float currentValue = 1f; // Здоровье объекта
    public UnityEvent onDestroyObstacle; // Событие для уничтожения объекта
    public GameObject prefabToSpawn; // Префаб, который нужно создать

    private Renderer objectRenderer; // Для изменения цвета объекта

    void Start()
    {
        objectRenderer = GetComponent<Renderer>(); // Получаем компонент Renderer для изменения цвета
    }

    // Метод для нанесения урона объекту
    public void GetDamage(float value)
    {
        currentValue -= value; // Уменьшаем здоровье

        // Меняем цвет в зависимости от оставшегося здоровья
        float colorValue = Mathf.Clamp01(currentValue); // Ограничиваем значение от 0 до 1
        objectRenderer.material.color = Color.Lerp(Color.white, Color.red, 1 - colorValue); // Плавный переход от белого к красному

        if (currentValue <= 0)
        {
            // Когда здоровье объекта достигает нуля, вызываем событие
            onDestroyObstacle.Invoke();
            Use(); // Создаем новый префаб на месте уничтоженного объекта
            Destroy(gameObject); // Удаляем объект
        }
    }

    // Метод для использования объекта (создание префаба)
    private void Use()
    {
        if (prefabToSpawn != null)
        {
            // Создаем новый объект на месте текущего объекта
            Vector3 spawnPosition = transform.position;
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(prefabToSpawn, spawnPosition, spawnRotation);
        }
    }
}
