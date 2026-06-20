using UnityEngine;

public class Disparo : MonoBehaviour
{
    [Header("Disparo")]
    [SerializeField] private float alcance = 100f;
    [SerializeField] private float cadencia = 0.2f;
    [SerializeField] private Transform puntoDisparo;
    [SerializeField] private ParticleSystem efectoDisparo;

    private float tiempoUltimoDisparo;
    private Camera camaraPrincipal;

    private void Awake()
    {
        camaraPrincipal = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && PuedoDisparar())
        {
            Disparar();
            tiempoUltimoDisparo = Time.time;
        }
    }

    private bool PuedoDisparar()
    {
        return Time.time >= tiempoUltimoDisparo + cadencia;
    }

    private void Disparar()
    {
        if (efectoDisparo != null)
            efectoDisparo.Play();

        Ray rayo = camaraPrincipal.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(rayo, out RaycastHit impacto, alcance))
        {
            Debug.DrawLine(rayo.origin, impacto.point, Color.red, 0.5f);
            Debug.Log($"Impacto en: {impacto.collider.gameObject.name}");
        }
        else
        {
            Debug.DrawRay(rayo.origin, rayo.direction * alcance, Color.yellow, 0.5f);
        }
    }
}