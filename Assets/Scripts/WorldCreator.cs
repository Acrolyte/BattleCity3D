using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorldCreator : MonoBehaviour
{
    public string fileName;
    public Dictionary<Coordinate, Block> world;
    public List<GameObject> enemies;

    private void Start()
    {
        world = new();
    }


    public void RenderImage(string levelName)
    {

        Texture2D image = (Texture2D)Resources.Load("Images/" + levelName);

        for(int i = 0; i < image.height; i++)
        {
            for(int j = 0; j < image.width; j++)
            {
                Coordinate coordinate = new(i, j);
                Color32 pixel = image.GetPixel(i, j);
                string hex = ColorUtility.ToHtmlStringRGB(pixel);
                
                AddBlock(coordinate, hex);
                //Debug.Log($"x={i}, y={j}, val={block.value}");
                //Debug.Log($"x={i}, y={j}, r={pixel.r}, g={pixel.g}, b={pixel.b}, hex={hex}");
            }
        }

        
    }
    
    public void AddBlock(Coordinate coordinate, string hex)
    {
        if (hex == "FFFFFF") return;

        Vector3 position = new Vector3(coordinate.X, 0.5f, coordinate.Y);

        GameObject cube = Instantiate(Resources.Load("Cube"), position, Quaternion.identity, transform) as GameObject;
        Block block = cube.AddComponent<Block>();

        
        if (hex == "ED1C24")
        {
            block.boxType = BoxType.Brick; 
            cube.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/brick");
        }
        else if (hex == "000000")
        {
            block.boxType = BoxType.Metal;
            cube.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/metal");
        }
        else if (hex == "22B14C")
        {
            block.boxType = BoxType.Grass;
            cube.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/grass");
            cube.GetComponent<NavMeshObstacle>().enabled = false;
        }
        else if (hex == "00A2E8")
        {
            block.boxType = BoxType.Water;
            cube.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/water");
            cube.GetComponent<NavMeshObstacle>().enabled = false;
        }
        else if(hex == "A349A4")
        {
            Destroy(cube);
            GameObject enemy = (GameObject)Resources.Load("Enemy");
            enemy.transform.position = new Vector3(position.x, 0f, position.z);
            enemy.transform.rotation = Quaternion.Euler(-90, -90, 0);
            enemies.Add(enemy);
            return;
        }
        world.Add(coordinate, block);
       

        cube.GetComponent<Block>().boxType = block.boxType;

        cube.name = "Cube " + coordinate.X + " " + coordinate.Y;


        //if (block.value == 1) cube.GetComponent<MeshRenderer>().material.color = Color.red;
        //else if (block.value == 2) cube.GetComponent<MeshRenderer>().material.color = Color.black;


    }

    public void SpawnEnemies()
    {
        foreach(var enemy in enemies){
            Instantiate(enemy);
        }
    }

}
