using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pathfinding : MonoBehaviour
{
    PathRequestManager requestManager;
    Grid grid;

    void Awake(){
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<Grid>();
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos){
        StartCoroutine(FindPath(startPos, targetPos));
    }
    
    // pathfinding A* function
    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos){
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

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

                foreach (Node neighbor in grid.GetNeighbors(currentNode)){
                    // if the node is not walkable or has been visited
                    if (!neighbor.walkable || closedSet.Contains(neighbor)){
                        continue;
                    }

                    // get the new distance
                    float newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                    
                    // if the new distance is less than current distance                    
                    if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor)){
                        neighbor.gCost = newMovementCostToNeighbor;
                        neighbor.hCost = GetDistance(neighbor, targetNode);
                        neighbor.parent = currentNode;

                        // if the neighbor has not yet been visited
                        if (!openSet.Contains(neighbor)){
                            openSet.Add(neighbor);
                        }
                    }
                }
            }
        }
        yield return null;
        if (pathSuccess){
            waypoints = RetracePath(startNode, targetNode);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    // for path visualization
    Vector3[] RetracePath(Node startNode, Node endNode){
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode){
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        path.Reverse();

        grid.path = path;

        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path){
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++){
            Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX, path[i-1].gridY - path[i].gridY);
            if (directionNew != directionOld){
                waypoints.Add(path[i].worldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    int GetDistance(Node nodeA, Node nodeB){
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if(dstX > dstY){
            return 14*dstY + 10 * (dstX - dstY);
        }
        return 14*dstX + 10 * (dstY - dstX);
    }
}
