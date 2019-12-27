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
    private ScoreCounter scorecount;
    //Static variables
    private static bool isReloaded = false;
    private static bool shouldShowHelp = false;


    private LevelNavControl levelNav;
    void Start()
    {
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
        levelNav = FindObjectOfType<LevelNavControl>();
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
        if (FlyFlag)
        {
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            this.gameObject.GetComponent<EdgeCollider2D>().enabled = false;
            PlayerSit(false);
            playerAnimation.SetBool("ShouldFly", true);
            playerAnimation.SetFloat("Speed", 0);
            audiomanager.PlaySound("flySound");
            hasFlown = true;
        }
        else
        {
            playerAnimation.SetBool("ShouldFly", false);
            audiomanager.StopSound("flySound");
        }

    }

    public void PlayerSit(bool shouldplayerSit)
    {
        if (shouldplayerSit)
        {
            this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            this.gameObject.GetComponent<EdgeCollider2D>().enabled = true;
            if(transform.localScale.x < 0)
            {
                transform.localScale = new Vector2(1.75f, 1.75f);
            }
            playerAnimation.SetBool("ShouldSit", shouldplayerSit);
        }
        else
        {
            playerAnimation.SetBool("ShouldSit", shouldplayerSit);
        }
    }

    public void PlayerWalk(bool playerCanJumpAndWalk)
    {

        if (Input.GetButton("Horizontal") && isTouchingGround)
        {
            audiomanager.PlaySound("walkSound");
        }
        if (playerCanJumpAndWalk)
        {
            playerAnimation.SetBool("ShouldFly", false);
            playerAnimation.SetBool("ShouldSit", false);
            if (movement > 0f)
            {
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
            if (Input.GetButtonDown("Jump") && isTouchingGround)
            {
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
        if (collision.collider.tag == "Pond" || collision.collider.tag == "tree" || collision.collider.tag == "KillerBIrd")
        {
            isReloaded = true;
            Time.timeScale = 0f;

            if (collision.collider.tag == "Pond")
            {
                shouldShowHelp = true;
                if (scorecounter.GetScore() > 0)
                    scorecounter.SetSCore(-5);
                messagecontrols.ShowMessage("PondMessage", true, "okay");
                playerAnimation.SetBool("ShouldSwim", true);
                transform.localScale = new Vector2(1f, 1f);
                playerAnimation.updateMode = AnimatorUpdateMode.UnscaledTime;
                audiomanager.PlaySound("dieSound");
            }
            else if (collision.collider.tag == "KillerBIrd")
            {
                if (scorecounter.GetScore() > 0)
                    scorecounter.SetSCore(-5);
                messagecontrols.ShowMessage("birdKillMessage", true, "okay");
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
                messagecontrols.ShowMessage("TreeMessage", true, "okay");
                playerAnimation.SetBool("IsStuck", true);
                playerAnimation.updateMode = AnimatorUpdateMode.UnscaledTime;
                transform.position = new Vector2(transform.position.x+2f, transform.position.y-2f);
                audiomanager.PlaySound("dieSound");
            }
        }
        else if(collision.collider.tag == "Finish")
        {
            navscripts.ShowHideNav(true);
            ShowOptionsPanel();
        }
    }

    public bool IsReloaded()
    {
        return isReloaded;
    }

    public void KillPlayer()
    {
        loadscene.changemenuscene("Lever");
    }

    void ShowOptionsPanel()
    {
        levelNav.ShowOptionsPanel("EndMsg", true, true, true);
    }
}



