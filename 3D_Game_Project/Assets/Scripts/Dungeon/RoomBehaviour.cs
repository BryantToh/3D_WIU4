using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject[] m_walls;
    [SerializeField] private GameObject[] m_doors;

    [Header("Test")]
    [SerializeField] private bool[] m_isOpen;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < m_isOpen.Length; i++)
        {
            m_walls[i].SetActive(!m_isOpen[i]);
            m_doors[i].SetActive(m_isOpen[i]);
        }
    }
}
