using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Ссылка на объект персонажа
    public Vector3 offset; // Смещение камеры относительно персонажа
    public float smoothSpeed = 0.125f; // Скорость сглаживания

    private void LateUpdate()
    {
        Vector3 desiredPosition = player.position + offset; // Желаемая позиция камеры
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Интерполяция позиции
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z); // Обновление позиции камеры
    }
}
