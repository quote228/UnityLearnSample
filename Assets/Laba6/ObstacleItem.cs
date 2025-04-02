using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // Для использования UnityEvent

public class ObstacleItem : MonoBehaviour
{
    public float currentValue = 1f; // "Здоровье" препятствия (от 0 до 1)
    public UnityEvent onDestroyObstacle; // Событие для уничтожения объекта

    private Renderer objectRenderer; // Для изменения цвета объекта

    void Start()
    {
        objectRenderer = GetComponent<Renderer>(); // Получаем компонент Renderer для изменения цвета
    }

    // Метод, который будет вызываться для нанесения урона
    public void GetDamage(float value)
    {
        currentValue -= value; // Уменьшаем здоровье

        // Меняем цвет в зависимости от оставшегося здоровья
        float colorValue = Mathf.Clamp01(currentValue); // Ограничиваем значение от 0 до 1
        objectRenderer.material.color = Color.Lerp(Color.white, Color.red, 1 - colorValue); // Плавный переход от белого к красному

        if (currentValue <= 0)
        {
            // Когда здоровье достигает 0, запускаем событие
            onDestroyObstacle.Invoke();

            // Удаляем объект
            Destroy(gameObject);
        }
    }
}
