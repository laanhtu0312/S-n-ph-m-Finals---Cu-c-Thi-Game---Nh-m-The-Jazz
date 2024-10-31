using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab;               // Prefab của quái vật
    public Tilemap groundTilemap;                  // Tilemap của vùng đất
    public int monstersPerSpawn = 10;              // Số quái vật tối đa
    public float spawnIntervalMinutes = 1f;        // Thời gian chờ giữa mỗi lần spawn (phút)
    public List<Vector3Int> spawnPositions;        // Danh sách các vị trí đã chọn trên tilemap để spawn quái

    private List<GameObject> spawnedMonsters = new List<GameObject>();  // Danh sách quái vật đã spawn

    private void Start()
    {
        // Bắt đầu Coroutine để spawn quái vật sau mỗi khoảng thời gian
        StartCoroutine(SpawnMonstersEveryInterval());
    }

    private IEnumerator SpawnMonstersEveryInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnIntervalMinutes * 60); // Chờ 1 phút
            SpawnMonsters();
        }
    }

    private void SpawnMonsters()
    {
        // Xóa quái vật đã bị tiêu diệt khỏi danh sách
        spawnedMonsters.RemoveAll(monster => monster == null);

        // Chỉ spawn thêm nếu số lượng quái vật hiện tại nhỏ hơn giới hạn
        int monstersToSpawn = monstersPerSpawn - spawnedMonsters.Count;

        if (monstersToSpawn <= 0) return; // Nếu đã đủ số lượng, không spawn thêm

        for (int i = 0; i < monstersToSpawn; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            if (spawnPosition != Vector3.zero)
            {
                GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
                spawnedMonsters.Add(monster);
            }
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        if (spawnPositions.Count == 0)
        {
            Debug.LogWarning("Danh sách vị trí spawn trống. Vui lòng chỉ định các vị trí spawn.");
            return Vector3.zero;
        }

        // Chọn ngẫu nhiên một vị trí từ danh sách đã chỉ định
        Vector3Int randomCell = spawnPositions[Random.Range(0, spawnPositions.Count)];

        // Kiểm tra xem vị trí có tile trong groundTilemap không
        if (groundTilemap.HasTile(randomCell))
        {
            // Chuyển đổi vị trí tile sang vị trí trong thế giới
            return groundTilemap.CellToWorld(randomCell) + new Vector3(0.5f, 0.5f, 0);
        }

        return Vector3.zero;
    }
}