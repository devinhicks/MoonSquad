using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int stage; // 0 - enemy move, 1 - player move, 2 - execute cards
    public Team team;

    [Header("Team")]
    public Captain _captain;
    public Pilot _pilot;
    public Mechanic _mechanic;
    public Gunner _gunner;

    [Header("Canvases")]
    public Transform board;
    public Canvas loadingScreen;

    [Header("Slots")]
    public DropSlot playerDiscardPile;
    public Transform enemyDiscardPile;
    public DropSlot enemyCardSlot;
    public DropSlot playerCardSlot;

    [Header("Buttons")]
    public Button[] powerUpButtons;
    public Button playersMoveButton;
    public Button makePlayButton;

    public bool playerCardSelected = false;
    public bool hasDiscarded = false;
    public bool hasUsedPowerup = false;

    private bool updatingBoard = false;
    private bool boardUp = true;
    private Vector2 boardUpPos;
    private Vector2 boardDownPos;

    // instance
    public static GameManager instance;
    void Awake() { instance = this; }

    public void Start()
    {
        boardUpPos = new Vector2(board.position.x, board.position.y);
        boardDownPos = new Vector2(board.position.x, board.position.y * -1);
    }

    public void InitializeGame()
    {
        _captain.SetCharacter(Team.instance.captain);
        _pilot.SetCharacter(Team.instance.pilot);
        _mechanic.SetCharacter(Team.instance.mechanic);
        _gunner.SetCharacter(Team.instance.gunner);

        EnemyDeck.instance.InitializeDeck();
        PlayerDeck.instance.InitializeDeck();

        EnemyDeck.instance.UpdateHand();
        PlayerDeck.instance.UpdateHand();
    }

    // Update is called once per frame
    void Update()
    {
        // update board position
        if (updatingBoard)
        {
            if (boardUp)
            {
                board.position = Vector2.Lerp(board.position, boardDownPos, .1f);

                if (Vector2.Distance(new Vector2(board.position.x,
                    board.position.y), boardDownPos) < .05f)
                {
                    boardUp = false;
                    updatingBoard = false;
                }
            }
            else
            {
                board.position = Vector2.Lerp(board.position, boardUpPos, .1f);

                if (Vector2.Distance(new Vector2(board.position.x,
                    board.position.y), boardUpPos) < .05f)
                {
                    boardUp = true;
                    updatingBoard = false;
                }
            }
        }

        // de/activate discard pile
        if (!hasDiscarded)
        {
            playerDiscardPile.gameObject.SetActive(true);
        }
        else
        {
            playerDiscardPile.gameObject.SetActive(false);
        }

        // de/activate powerup buttons
        if (!hasUsedPowerup)
        {
            foreach (Button b in powerUpButtons)
            {
                b.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Button b in powerUpButtons)
            {
                b.enabled = false;
            }
        }
    }

    public void UpdateStage()
    {
        // update stage index
        if (stage == 0)
            stage = 1;
        else if (stage == 1)
            stage = 2;
        else if (stage == 2)
            stage = 0;
    }

    public void UpdateBoardPosition()
    {
        updatingBoard = true;
    }

    public void EnemysMove()
    {
        // update cards

        UpdateStage();
    }

    public void OnEnemyCardSelected()
    {
        // set other cards to be unclickable
        foreach(GameObject card in EnemyDeck.instance.currentHand)
        {
            card.GetComponent<EnemyCard>().canClick = false;
        }

        // character says something
        Debug.Log("ENEMY CARD SELECTED");

        playersMoveButton.gameObject.SetActive(true);
    }

    public void OnPlayersMoveButton()
    {
        foreach (GameObject card in EnemyDeck.instance.currentHand)
        {
            card.GetComponent<EnemyCard>().RemoveCardsFromBoard();
        }

        UpdateBoardPosition();

        playersMoveButton.enabled = false;
    }

    public void OnPlayerCardSelected()
    {
        // set other cards to be unclickable
        foreach (GameObject card in EnemyDeck.instance.currentHand)
        {
            card.GetComponent<EnemyCard>().canClick = false;
        }

        // character says something
        Debug.Log("PLAYER CARD SELECTED");

        playerCardSelected = true;

        makePlayButton.gameObject.SetActive(true);
    }

    public void OnMakePlayButton()
    {
        enemyCardSlot.GetDroppedItem();
        playerCardSlot.GetDroppedItem();
    }
}
