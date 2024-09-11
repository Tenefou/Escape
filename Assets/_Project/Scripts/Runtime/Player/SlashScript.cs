using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlashScript : MonoBehaviour
{
    [SerializeField] private List<Slash> slashes;
    private Coroutine currentSlashCoroutine;

    private void Start()
    {
        DisableSlashes();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(currentSlashCoroutine != null)
                StopCoroutine(currentSlashCoroutine);

            currentSlashCoroutine = StartCoroutine(Slash());
        }
    }

    private void DisableSlashes()
    {
        for (int i = 0; i < slashes.Count; ++i) 
        {
            slashes[i].slashObj.SetActive(false);
        }
    }

    private IEnumerator Slash()
    {
        DisableSlashes(); // Désactive les slashes avant de commencer

        for ( int i = 0; i < slashes.Count; i++)
        {
            yield return new WaitForSeconds(slashes[i].delay);
            slashes[i].slashObj.SetActive(true);
        }
    }

}

[System.Serializable]
public class Slash
{
    public GameObject slashObj;
    public float delay;
}
