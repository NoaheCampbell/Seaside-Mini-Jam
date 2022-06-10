using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public int maxEnemies = 10;
    public int enemyCount = 0;

    public List<GameObject> enemies = new List<GameObject>();
}
