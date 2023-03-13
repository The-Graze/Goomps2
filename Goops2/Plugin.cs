using BepInEx;
using Photon.Pun;
using System;
using System.Linq;
using UnityEngine;
using Utilla;

namespace Goops2
{
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class Plugin : BaseUnityPlugin
    {
        GOOMPS2 g2;
        void Start()
        {Utilla.Events.GameInitialized += OnGameInitialized;}

        void OnEnable()
        {
            g2.enabled = true;
        }

        void OnDisable()
        {
            g2.enabled = false;
        }

        void OnGameInitialized(object sender, EventArgs e)
        {
            g2 = GameObject.Find("Local Gorilla Player").AddComponent<GOOMPS2>();
            GorillaParent.instance.vrrigParent.AddComponent<Adder>();
        }
    }
    public class GOOMPS2 : MonoBehaviour
    {
        void Awake()
        {

        }
        void Update()
        {
            GameObject[] objs = GameObject.FindObjectsOfType(typeof(GameObject)).Select(g => g as GameObject).Where(g => g.GetComponent<Gplayer>() != null).ToArray();
            foreach (GameObject g in objs)
            {
                if(Vector3.Distance(transform.position, g.transform.position) < 0.52f)
                {
                    g.transform.GetChild(3).GetComponent<Renderer>().enabled = false;
                }
                else
                {
                    g.transform.GetChild(3).GetComponent<Renderer>().enabled = true;
                }
            }
        }
    }
    class Gplayer : MonoBehaviour
    {

    }


    public class Adder : MonoBehaviour
    {
        void LateUpdate()
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.GetComponent<Gplayer>() == null && child.GetComponent<PhotonView>().Owner != PhotonNetwork.LocalPlayer)
                {
                    child.gameObject.AddComponent<Gplayer>();
                }
            }
        }
    }
}
