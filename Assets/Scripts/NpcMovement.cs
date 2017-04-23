using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class NpcMovement : MonoBehaviour {
	

	private GameManager gameManager;
    
	public MyPathNode nextNode;
    public MyPathNode[,] grid;

    public List<Action> myActions;

    public gridPosition currentGridPosition = new gridPosition();
	public gridPosition startGridPosition = new gridPosition();
	public gridPosition endGridPosition = new gridPosition();
	
	private Orientation gridOrientation = Orientation.Vertical;
	private bool allowDiagonals = false;
	private bool correctDiagonalSpeed = true;
	private Vector2 input;
	//private bool isMoving = false;
	private Vector3 startPosition;
	private Vector3 endPosition;
	private float t;

    public int currentAction = 0;
    public float currentStayTime = 0.0f;
    private bool configuredWalking = false;
    public bool isWaiting;

    IEnumerable<MyPathNode> currentPath;

    GameObject precious;
    GameObject player;

    public enum actionType
    {
        walk,
        stay
    };

    [Serializable]
    public class Action
    {
        [SerializeField]
        public Vector3 pos;
        [SerializeField]
        public float wait;
    }

	public class MySolver<TPathNode, TUserContext> : SettlersEngine.SpatialAStar<TPathNode, 
	TUserContext> where TPathNode : SettlersEngine.IPathNode<TUserContext>
	{
		protected override Double Heuristic(PathNode inStart, PathNode inEnd)
		{


			int formula = GameManager.distance;
			int dx = Math.Abs (inStart.X - inEnd.X);
			int dy = Math.Abs(inStart.Y - inEnd.Y);

			if(formula == 0)
				return Math.Sqrt(dx * dx + dy * dy); //Euclidean distance

			else if(formula == 1)
				return (dx * dx + dy * dy); //Euclidean distance squared

			else if(formula == 2)
				return Math.Min(dx, dy); //Diagonal distance

			else if(formula == 3)
				return (dx*dy)+(dx + dy); //Manhatten distance

		

			else 
				return Math.Abs (inStart.X - inEnd.X) + Math.Abs (inStart.Y - inEnd.Y);

		}
		
		protected override Double NeighborDistance(PathNode inStart, PathNode inEnd)
		{
			return Heuristic(inStart, inEnd);
		}

		public MySolver(TPathNode[,] inGrid)
			: base(inGrid)
		{
		}
	} 



	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        precious = GameObject.FindGameObjectWithTag("Precious");
        if (precious)
        {
            myActions[0].pos = precious.transform.position;
        }
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void findUpdatedPath(int currentX,int currentY)
	{
		MySolver<MyPathNode, System.Object> aStar = new MySolver<MyPathNode, System.Object>(gameManager.grid);
		currentPath = aStar.Search(new Vector2(currentX, currentY), new Vector2(endGridPosition.x, endGridPosition.y), null);


		int x = 0;

		if (currentPath != null) {
		
			foreach (MyPathNode node in currentPath)
			{
				if(x==1)
				{
					nextNode = node;
					break;
				}

				x++;

			}
		}
		
	}

    void Update()
    {
        if (precious == null)
        {
            precious = GameObject.FindGameObjectWithTag("Precious");
            myActions[0].pos = precious.transform.position;
        }
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (currentAction == myActions.Count)
        {
            currentAction = 0;
        }
        else
        {
            if (isWaiting == true)
            {
                currentStayTime += Time.deltaTime;
                if (currentStayTime >= myActions[currentAction].wait)
                {
                    //After player moves to endGridPosition he waits X seconds before starting next action
                    currentAction++;

                    //Debug.Log(currentAction);
                    currentStayTime = 0.0f;
                    isWaiting = false;
                }
            }
            else if (!configuredWalking && !isWaiting)
            {
                configuredWalking = true;

                startGridPosition = new gridPosition((int)this.transform.position.x, (int)this.transform.position.z);
                endGridPosition = new gridPosition((int)myActions[currentAction].pos.x, (int)myActions[currentAction].pos.z);
                initializePosition();
                updatePath();
                getNextMovement();
            }
            else if (this.transform.position.x == endGridPosition.x && this.transform.position.z == endGridPosition.y)
            {
                isWaiting = true;
                currentAction++;
                configuredWalking = false;
                StopAllCoroutines();
            }
        }
    }
    
	public float moveSpeed;
	
	public class gridPosition{
        public int x =0;
        public int y=0;

		public gridPosition()
		{
		}

		public gridPosition (int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	};
	
	
	private enum Orientation {
		Horizontal,
		Vertical
	};

	
	public IEnumerator move() {
		//isMoving = true;
		startPosition = transform.position;
		t = 0;

		if(gridOrientation == Orientation.Horizontal) {
			endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gameManager.gridSize, this.transform.position.y,
			                          startPosition.z);
			currentGridPosition.x += System.Math.Sign(input.x);
		} else {
			endPosition = new Vector3(startPosition.x + System.Math.Sign(input.x) * gameManager.gridSize, this.transform.position.y,
                                      startPosition.z + System.Math.Sign(input.y) * gameManager.gridSize);
			
			currentGridPosition.x += System.Math.Sign(input.x);
			currentGridPosition.y += System.Math.Sign(input.y);
		}
		
		while (t <= 1f) {
			t += Time.deltaTime * (moveSpeed/ gameManager.gridSize);
			transform.position = Vector3.Lerp(startPosition, endPosition, t);
            //Debug.Log("startPosition: " + startPosition + " endPosition: " + endPosition);
			yield return null;
		}

		//isMoving = false;
		getNextMovement ();
		
		yield return 0;
	}
	
	void updatePath()
	{
        findUpdatedPath (currentGridPosition.x, currentGridPosition.y);
	}
	
	void getNextMovement()
	{
        updatePath();

        if (nextNode != null)
        {
            input.x = 0;
            input.y = 0;
            if (nextNode.X > currentGridPosition.x)
            {
                input.x = 1;
            }
            if (nextNode.Y > currentGridPosition.y)
            {
                input.y = 1;
            }
            if (nextNode.Y < currentGridPosition.y)
            {
                input.y = -1;
            }
            if (nextNode.X < currentGridPosition.x)
            {
                input.x = -1;
            }
            StartCoroutine(move());
        }
	}

    public Vector3 getGridPosition(int x, int y)
	{
		float posX = gameManager.gridBox.transform.position.x + (gameManager.gridSize*x);
		float posY = gameManager.gridBox.transform.position.z + (gameManager.gridSize*y);
		return new Vector3(posX, this.transform.position.y, posY);	
		
	}


    public void initializePosition()
    {
        this.gameObject.transform.position = getGridPosition(startGridPosition.x, startGridPosition.y);
        currentGridPosition.x = startGridPosition.x;
        currentGridPosition.y = startGridPosition.y;
    }
}
