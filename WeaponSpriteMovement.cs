using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpriteMovement : MonoBehaviour
{
    [SerializeField] private MovimientoJugador movJugador;
    [SerializeField] private StatsJugador statsJugador;

    private WeaponScript weaponScript;
    public float height = 0.5f;
    public float upperLimit;
    private Vector3 startPos;

    private float time;
    public bool movActivo = true;

    void Start()
    {
        startPos = transform.localPosition;
        weaponScript = GetComponentInParent<WeaponScript>();
    }

    void Update()
    {

        if (movJugador.isMoving && movActivo)
        {
            float newY = Mathf.Sin(time) * height;
            transform.localPosition = new Vector3(startPos.x, Mathf.Min(startPos.y + newY, upperLimit), startPos.z);

            if(transform.localPosition.y == -0.5f)
            {
                time += Time.deltaTime * 25;
            }
            else
            {
                time += Time.deltaTime;
            }

            
        }else if (!movActivo)
        {
            transform.localPosition = startPos;
        }


    }


}
