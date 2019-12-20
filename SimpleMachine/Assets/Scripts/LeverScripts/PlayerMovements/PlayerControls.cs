using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class PlayerControls : MonoBehaviour
{
    public float speed = 20f;
    public float movement = 0f;
    public float jumpSpeed = 6f;
    private Rigidbody2D rigidBody;
    private Animator playerAnimation;
    public Transform groundCheckpoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;
    public bool playerCanJumpAndWalk = true;
    public AfterFlightCollisionController afterflightCC;
    private NavScripts navscripts;
    private AudioManager audiomanager;
    private bool hasFlown = false;
    private LoadScene loadscene;
    private ScoreCounter scorecounter;
    private HelpController helpcontrol;
    public RespawnPointControls respawnpoint;
    private MessageController messagecontrols;
    //added later for levelPanel
    private GameObject optnPanel;
    private Text endText;
    private LanguageController languagecontrols;
    private XmlDocument data = new XmlDocument();
    private ScoreCounter scorecount;
    //Static variables
    private static bool isReloaded = false;
    private static bool shouldShowHelp = false;


    void Start()
    {
        endText = GameObject.Find("TextCanvas/LevelPanelContainer/OptionsPanel/EndText")?.GetComponent<Text>();
        messagecontrols = FindObjectOfType<MessageController>();
        rigidBody = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        afterflightCC = FindObjectOfType<AfterFlightCollisionController>();
        navscripts = FindObjectOfType<NavScripts>();
        audiomanager = AudioManager.instance;
        scorecount = FindObjectOfType<ScoreCounter>();
        respawnpoint = FindObjectOfType<RespawnPointControls>();
        loadscene = FindObjectOfType<LoadScene>();
        scorecounter = FindObjectOfType<ScoreCounter>();
        helpcontrol = FindObjectOfType<HelpController>();
        //Debug.Log("playerControlStarted");
        if (isReloaded)
        {
            //Debug.Log(shouldShowHelp);
            //Debug.Log(respawnpoint.GetRespawnPoint());
            this.transform.position = respawnpoint.GetRespawnPoint();

            //if (shouldShowHelp)
            //{
               /// StartCoroutine(MessageShow());
               // shouldShowHelp = false;
            //}
            isReloaded = false;
        }
        languagecontrols = FindObjectOfType<LanguageController>();
        data = languagecontrols.DataFile();
        // audiomanager.PlaySound("gameMusic");
    }

    IEnumerator MessageShow()
    {
        //print(Time.time);
        yield return new WaitForSeconds(.2f);
        if(respawnpoint.GetRespawnPoint().x < 100)
        {
            helpcontrol.ShowHelp("lever_help_1");
        }
        else
        {
            helpcontrol.ShowHelp("lever_help_2");
        }
        //print(Time.time);
    }

    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        isTouchingGround = Physics2D.OverlapCircle(groundCheckpoint.position, groundCheckRadius, groundLayer);
        
    }
    public void playerFly(bool FlyFlag)
    {
        //Debug.Log("fly");
        //audiomanager.StopSound("sitSound");
        if (FlyFlag)
        {
            //Debug.Log("fly start");
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            this.gameObject.GetComponent<EdgeCollider2D>().enabled = false;
            //isPlayerSitting = false;
            PlayerSit(false);

            playerAnimation.SetBool("ShouldFly", true);
            playerAnimation.SetFloat("Speed", 0);
            audiomanager.PlaySound("flySound");
            hasFlown = true;
            //transform.localScale = new Vector2(1.8f, 1.8f);
        }
        else
        {
            playerAnimation.SetBool("ShouldFly", false);
            audiomanager.StopSound("flySound");
            //Debug.Log("fly end");
        }

    }
    public void PlayerSit(bool shouldplayerSit)
    {
        if (shouldplayerSit)
        {
            //Debug.Log("sit start");
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            this.gameObject.GetComponent<EdgeCollider2D>().enabled = true;
            //Debug.Log(transform.localScale.x);
            if(transform.localScale.x < 0)
            {
                transform.localScale = new Vector2(1.75f, 1.75f);
            }
            //transform.position = new Vector3(213.6625f, transform.position.y, -1);
            // transform.localScale = new Vector2(1.8f, 1.8f);
            playerAnimation.SetBool("ShouldSit", shouldplayerSit);
            //audiomanager.PlaySound("sitSound");
        }
        else
        {
            playerAnimation.SetBool("ShouldSit", shouldplayerSit);

            //Debug.Log("sit end");
        }
    }
    public void PlayerWalk(bool playerCanJumpAndWalk)
    {
        //Debug.Log(movement);

        if (Input.GetButton("Horizontal") && isTouchingGround)
        {
            audiomanager.PlaySound("walkSound");
        }
        if (playerCanJumpAndWalk)
        {
            //Debug.Log("walk start");
            playerAnimation.SetBool("ShouldFly", false);
            playerAnimation.SetBool("ShouldSit", false);
            //audiomanager.PlaySound("walkSound");
            if (movement > 0f)
            {
                //Debug.Log(movement);
                if (movement > 0.7)
                    speed = 9;
                rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
                transform.localScale = new Vector2(1.75f, 1.75f);
                playerAnimation.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
            }
            else if (movement < 0f)
            {
                rigidBody.velocity = new Vector2(movement * speed, rigidBody.velocity.y);
                transform.localScale = new Vector2(-1.75f, 1.75f);
                playerAnimation.SetFloat("Speed", Mathf.Abs(rigidBody.velocity.x));
            }else if(movement == 0f)
            {
                playerAnimation.SetFloat("Speed", 0);
            }

        }
    }

    public void PlayerJump(bool playerCanJumpAndWalk)
    {
        if (playerCanJumpAndWalk)
        {
            //Debug.Log("jump start");
            if (Input.GetButtonDown("Jump") && isTouchingGround)
            {
                //Debug.Log(jumpSpeed);
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpSpeed);
            }

        }
    }


    public bool groundTouchFlag()
    {
        return isTouchingGround;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.collider.tag);
        //if (collision.collider.tag == "tree")
        //{
            //Debug.Log("Ended!!!!!!!!!");
            //playerFly(false);
            /*PlayerSit(false);
            PlayerWalk(true);
            audiomanager.StopSound("flySound");*/
            //audiomanager.PlaySound("dieSound");
            //navscripts.ShowHideNav(true);
            //Time.timeScale = 0f;
        //}

        if (collision.collider.tag == "Pond" || collision.collider.tag == "tree" || collision.collider.tag == "KillerBIrd")
        {
           // Debug.Log("pond collision");
            isReloaded = true;
            Time.timeScale = 0f;

            if (collision.collider.tag == "Pond")
            {
                shouldShowHelp = true;
                if (scorecounter.GetScore() > 0)
                    scorecounter.SetSCore(-5);
                messagecontrols.ShowMessage("PondMessage", false, true, "okay");
                playerAnimation.SetBool("ShouldSwim", true);
                transform.localScale = new Vector2(1f, 1f);
                playerAnimation.updateMode = AnimatorUpdateMode.UnscaledTime;
                audiomanager.PlaySound("dieSound");
            }
            else if (collision.collider.tag == "KillerBIrd")
            {
                Debug.Log("KIller Bird");
                if (scorecounter.GetScore() > 0)
                    scorecounter.SetSCore(-5);
                messagecontrols.ShowMessage("birdKillMessage", false, true, "okay");
                playerAnimation.SetBool("IsDead", true);
                playerAnimation.updateMode = AnimatorUpdateMode.UnscaledTime;
                transform.localScale = new Vector2(3f, 3f);
                transform.position = new Vector2(transform.position.x, transform.position.y-2);
                audiomanager.PlaySound("dieSound");
            }
            else
            {
                shouldShowHelp = true;
                if (scorecounter.GetScore() > 0)
                    scorecounter.SetSCore(-5);
                messagecontrols.ShowMessage("TreeMessage", false, true, "okay");
                playerAnimation.SetBool("IsStuck", true);
                playerAnimation.updateMode = AnimatorUpdateMode.UnscaledTime;
                //transform.localScale = new Vector2(4.5f, 4.5f);
                transform.position = new Vector2(transform.position.x+2f, transform.position.y-2f);
                audiomanager.PlaySound("dieSound");
            }

            //audiomanager.PlaySound("dieSound");
            //navscripts.ShowHideNav(true);
            //StartCoroutine(KillPlayer());
        }
        else if(collision.collider.tag == "Finish")
        {
            navscripts.ShowHideNav(true);
            optnPanel = GameObject.Find("LevelPanelContainer");
            ShowOptionsPanel();
        }
    }

    public bool IsReloaded()
    {
        return isReloaded;
    }
    public void KillPlayer()
    {
        //Time.timeScale = 0f;
        //yield return new WaitForSeconds(2f);
        //Debug.Log("coroutine's sec part");
        loadscene.changemenuscene("Lever");
    }
    void ShowOptionsPanel()
    {
        Animator animator1 = GameObject.Find("TextCanvas/LevelPanelContainer/OptionsPanel/InclinedBtn").GetComponent<Animator>();
        Animator animator2 = GameObject.Find("TextCanvas/LevelPanelContainer/OptionsPanel/LeverBtn").GetComponent<Animator>();
        optnPanel.transform.GetChild(0).gameObject.SetActive(true);
        endText.text = data.SelectNodes("/strings/" + "string[@id='EndMsg']")[0].InnerText.ToString() + scorecount.GetScore();
        optnPanel.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        optnPanel.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Button>().interactable = true;
        optnPanel.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Button>().interactable = true;
        Time.timeScale = 0f;
        animator1.updateMode = AnimatorUpdateMode.UnscaledTime;
        animator2.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

}
