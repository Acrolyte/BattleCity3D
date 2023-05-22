using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public WorldCreator worldCreator;

    public static GameManager instance { get; private set; }

    public ParticleSystem particles;

    bool levelLoaded = false;
    

    private void Awake()
    {
        Debug.Log("Awake called in gm");
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);

        particles = Resources.Load<ParticleSystem>("Explosion");
    }

    
    private void Update()
    {

        if (levelLoaded)
        {
            // supply names
            worldCreator.RenderImage("level2");
            worldCreator.SpawnEnemies();
            levelLoaded = false;
        }

        //End level and Change Scene else levelLoaded = true
       
           
    }

    private void Start()
    {
        Debug.Log("Start called in gm");
    }

    public void ExplodeAtPos(Vector3 position)
    {
        Instantiate(particles, position, Quaternion.identity);
        particles.Play();
    }

    public void LoadScene(string name)
    {
        levelLoaded = true;
        SceneManager.LoadScene(name);
    }
}
