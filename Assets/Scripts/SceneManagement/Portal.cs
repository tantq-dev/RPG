using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;
    void OnTriggerEnter(Collider other)
    {
        print("Player enter trigger");
        if (other.tag == "Player")
        {
            StartCoroutine(Transition());
        }
    }
    private IEnumerator Transition()
    {

        DontDestroyOnLoad(this.gameObject);
        yield return SceneManager.LoadSceneAsync(sceneToLoad);
        print("scene loaded");

        Portal otherPortal = GetOtherPortal();
        UpdatePlayer(otherPortal);
        Destroy(this.gameObject);
    }

    private void UpdatePlayer(Portal otherPortal)
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
        player.transform.rotation = otherPortal.spawnPoint.rotation;
    }

    private Portal GetOtherPortal()
    {
        foreach (Portal portal in FindObjectsOfType<Portal>())
        {
            if (portal == this) continue;
            return portal;
        }
        return null;
    }
}
