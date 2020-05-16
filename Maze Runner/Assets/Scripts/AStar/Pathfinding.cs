using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class Pathfinding : MonoBehaviour {

	Grid grid;
	static Pathfinding instance;
	
	void Awake() {
		grid = GetComponent<Grid>();
		instance = this;
	}

	public static Vector2[] RequestPath(Vector2 from, Vector2 to) {
		return instance.FindPath (from, to);
	}
	
	Vector2[] FindPath(Vector3 startPos, Vector3 targetPos){
        Vector2[] waypoints = new Vector2[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);
        startNode.parent = startNode;

        if(!startNode.walkable){
            startNode = grid.ClosestWalkableNode (startNode);
        }
        if(!targetNode.walkable){
            targetNode = grid.ClosestWalkableNode (targetNode);
        }
        
        if (startNode.walkable && targetNode.walkable){
            // openSet represents unexplored grids
            List<Node> openSet = new List<Node>();

            // closedSet represents visited grids
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            // looping the unexplored grids
            while (openSet.Count > 0){
                Node currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++){
                    if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost){
                        if(openSet[i].hCost < currentNode.hCost)
                            currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode){
                    pathSuccess = true;
                    break;
                }

                // foreach (Node neighbour in grid.Getneighbours(currentNode)){
				foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
                    // if the node is not walkable or has been visited
                    if (!neighbour.walkable || closedSet.Contains(neighbour)){
                        continue;
                    }

                    // get the new distance
                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    
                    // if the new distance is less than current distance                    
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)){
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        // if the neighbour has not yet been visited
                        if (!openSet.Contains(neighbour)){
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }
        if (pathSuccess){
            waypoints = RetracePath(startNode, targetNode);
        }
        return waypoints;
    }
	
	Vector2[] RetracePath(Node startNode, Node endNode) {
		List<Node> path = new List<Node>();
		Node currentNode = endNode;
		
		while (currentNode != startNode) {
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		Vector2[] waypoints = SimplifyPath(path);
		Array.Reverse(waypoints);
		return waypoints;
		
	}
	
	Vector2[] SimplifyPath(List<Node> path) {
		List<Vector2> waypoints = new List<Vector2>();
		Vector2 directionOld = Vector2.zero;
		
		for (int i = 1; i < path.Count; i ++) {
			Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX,path[i-1].gridY - path[i].gridY);
			if (directionNew != directionOld) {
				waypoints.Add(path[i].worldPosition);
			}
			directionOld = directionNew;
		}
		return waypoints.ToArray();
	}
	
	int GetDistance(Node nodeA, Node nodeB) {
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
		
		if (dstX > dstY)
			return 14*dstY + 10* (dstX-dstY);
		return 14*dstX + 10 * (dstY-dstX);
	}
	
	
}
