using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;

public class MyEditorWindow : EditorWindow
{
    private Transform locationsRoot;
    private Transform pathsRoot;
    private string connectionsMade = "-";
    private GameObject linePrefab;

    [MenuItem("Window/Connect Locations Editor")]
    public static void ShowWindow()
    {
        GetWindow<MyEditorWindow>("Connect Locations Editor");
    }

    private void OnGUI()
    {
        if(Selection.count < 2)
            GUI.enabled = false;
        if(GUILayout.Button("Connect Locations"))
        {
            connectionsMade = "";
            var locations = Selection.gameObjects
                .Select(x => x.GetComponent<Location>())
                .ToList();
            
            foreach(var left in locations)
            {
                foreach(var right in locations)
                {
                    if(left == right)
                        continue;
                    if(!left.ConnectedLocations.Contains(right))
                    {
                        left.ConnectedLocations.Add(right);
                        if(pathsRoot != null && linePrefab != null)
                            CreatePath(left, right);
                        connectionsMade += "+ Connected " + left.name + " to " + right.name + "\n";
                        EditorUtility.SetDirty(left);
                    }
                }
            }
        }
        if(GUILayout.Button("Disconnect Locations"))
        {
            connectionsMade = "";
            var locations = Selection.gameObjects
                .Select(x => x.GetComponent<Location>())
                .ToList();
            
            foreach(var left in locations)
            {
                foreach(var right in locations)
                {
                    if(left == right)
                        continue;
                    if(left.ConnectedLocations.Contains(right))
                    {
                        left.ConnectedLocations.Remove(right);
                        connectionsMade += "- Disconnected " + left.name + " to " + right.name + "\n";
                        EditorUtility.SetDirty(left);
                    }
                }
            }
        }
        GUI.enabled = true;
        GUILayout.Label(connectionsMade);
        
        GUILayout.Space(10);
        
        locationsRoot = (Transform)EditorGUILayout.ObjectField("Locations Root", locationsRoot, typeof(Transform), true);
        pathsRoot = (Transform)EditorGUILayout.ObjectField("Paths Root", pathsRoot, typeof(Transform), true);
        linePrefab = (GameObject)EditorGUILayout.ObjectField("Line Prefab", linePrefab, typeof(GameObject), false);
        if(GUILayout.Button("Generate Paths"))
        {
            if(locationsRoot != null && pathsRoot != null)
                GeneratePaths();
        }
    }
    
    
    private class Connection
    {
        public Location From;
        public Location To;
        
        protected bool Equals(Connection other)
        {
            return Equals(From, other.From) && Equals(To, other.To);
        }

        public override bool Equals(object obj)
        {
            if(ReferenceEquals(null, obj)) return false;
            if(ReferenceEquals(this, obj)) return true;
            if(obj.GetType() != this.GetType()) return false;
            return Equals((Connection)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(From, To);
        }
    }
    
    void GeneratePaths()
    {
        var createdPaths = new HashSet<Connection>();
        var locations = locationsRoot.GetComponentsInChildren<Location>();
        int tries = 0;

        var currentPaths = pathsRoot.Cast<Transform>().ToList();
        for(int i = 0; i < currentPaths.Count; i++)
        {
            DestroyImmediate(currentPaths[i].gameObject);
        }
        
        foreach(var location in locations)
        {
            foreach(var connectedLocation in location.ConnectedLocations)
            {
                if(tries > 10000)
                    return;
                tries += 1;
                var connection = new Connection {From = location, To = connectedLocation};
                if(createdPaths.Contains(connection))
                    continue;
                createdPaths.Add(connection);
                CreatePath(location, connectedLocation);
            }
        }
    }

    private void CreatePath(Location location, Location connectedLocation)
    {
        var path = Instantiate(linePrefab, pathsRoot);
        path.name = "Path";
        var lineRenderer = path.GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, location.transform.position);
        lineRenderer.SetPosition(1, connectedLocation.transform.position);
    }
}
#endif