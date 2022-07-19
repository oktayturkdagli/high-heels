#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SOLevel))]
public class LevelEditor : Editor
{
    private SOLevel level;
    private Vector3 mousePosition;
    private int width = 0, height = 0;
    private bool onMouseDown = false;
    private string[] itemTypesText;
    private int selectedItem = 0;
    private const string helpBoxText =
        "\nUsing this custom editor you can design your grid based level.\n" +
        "\nNull option deletes the objects you specify with the pointer\n";

    private void OnEnable()
    {
        level = target as SOLevel;
        width = level.Width;
        height = level.Height;
        int itemTypesLength = Enum.GetValues(typeof(ItemTypes)).Length;
        itemTypesText = new string[itemTypesLength];
        for (int i = 0; i < itemTypesLength; i++)
        {
            itemTypesText[i] = ((ItemTypes)i).ToString();
        }
        SceneView.duringSceneGui += OnScene;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnScene;
    }
    
    private void OnScene(SceneView sceneView)
    {
        CheckInput();
        HandleInput();
        Draw3DObjectOnScene();
    }
    
    public override void OnInspectorGUI()
    {
        DrawMyInspector();
    }

    private void DrawMyInspector()
    {
        EditorGUILayout.Space(); GUILayout.Label("Create Your Level", EditorStyles.boldLabel); EditorGUILayout.Space();
        EditorGUILayout.Space(); GUILayout.Label("Draw", EditorStyles.boldLabel); EditorGUILayout.Space();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Width", EditorStyles.boldLabel);
        width = EditorGUILayout.IntField(width);
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Height", EditorStyles.boldLabel);
        height = EditorGUILayout.IntField(height);
        GUILayout.EndHorizontal();
        level.Width = width;
        level.Height = height;

        DrawMyButtons();
        
        EditorGUILayout.Space(20);
        EditorGUILayout.HelpBox(helpBoxText, MessageType.None);
        EditorUtility.SetDirty(level);
    }

    private void DrawMyButtons()
    {
        if (GUILayout.Button("Draw Width x Height Road"))
            CreateRoad();
        
        selectedItem = GUILayout.Toolbar(selectedItem, itemTypesText);
        
        EditorGUILayout.Space(20); GUILayout.Label("Clear", EditorStyles.boldLabel); EditorGUILayout.Space();
        if (GUILayout.Button("Remove Last"))
            RemoveAt(mousePosition);
        if (GUILayout.Button("Clear All Of Selected"))
            Clear();
    }
    
    private void Draw3DObjectOnScene()
    {
        Handles.color = Color.magenta;
        Handles.DrawWireDisc(Vector3.zero, Vector3.up, 0.5f);
        for (var i = 0; i < level.LevelGrid.Count; i++)
        {
            switch (level.LevelGrid[i].type)
            {
                case ItemTypes.Null:
                    break;
                case ItemTypes.Road:
                    Handles.color = Color.white;
                    Handles.DrawWireCube(new Vector3(level.LevelGrid[i].position.x, level.LevelGrid[i].position.y, level.LevelGrid[i].position.z + height/2), new Vector3(width,0f,height));
                    break;
                case ItemTypes.Cube:
                    Handles.color = Color.red;
                    Handles.DrawWireCube(level.LevelGrid[i].position, Vector3.one);
                    break;
                case ItemTypes.Shoe:
                    Handles.color = Color.magenta;
                    Handles.DrawWireCube(level.LevelGrid[i].position, Vector3.one);
                    break;
                case ItemTypes.Diamond:
                    Handles.color = Color.cyan;
                    Handles.DrawWireCube(level.LevelGrid[i].position, Vector3.one);
                    break;
                case ItemTypes.Finish:
                    Handles.color = Color.green;
                    Handles.DrawWireDisc(level.LevelGrid[i].position, Vector3.up, width);
                    break;
            }
        }
    }
    
    private void CheckInput()
    {
        Event guiEvent = Event.current;
        if (guiEvent.type == EventType.Layout)
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive)); // When the new object is created, Don't assign the inspector to that object 
        else if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0) //When Down the left mouse button
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition);
            float drawPlaneHeight = 0;
            float distanceToDrawPlane = (drawPlaneHeight - ray.origin.y) / ray.direction.y;
            mousePosition = ray.GetPoint(distanceToDrawPlane);
            mousePosition.x = Mathf.Round(mousePosition.x);
            mousePosition.z = Mathf.Round(mousePosition.z);
            onMouseDown = true;
        }
        else
        {
            onMouseDown = false;
        }
    }
    
    private void HandleInput()
    {
        if (onMouseDown)
        {
            onMouseDown = false;
            RemoveAt(mousePosition);
            CreateObject(mousePosition);
        }
    }
    
    private void CreateObject(Vector3 position)
    {
        ItemTypes tempItemType = (ItemTypes)selectedItem;
        if (tempItemType == ItemTypes.Null)
            return;
        
        var levelItem = new LevelItem(tempItemType, position);
        level.LevelGrid.Add(levelItem);
    }
    
    private void CreateRoad()
    {
        var position = new Vector3(0, -1, 0);
        selectedItem = 1;
        mousePosition = position;
        RemoveAt(position);
        CreateObject(position);
    }
    
    private void RemoveAt(Vector3 position)
    {
        for (int i = 0; i < level.LevelGrid.Count; i++)
        {
            if (level.LevelGrid[i].position == position)
            {
                level.LevelGrid.RemoveAt(i);
                break;
            }
        }
    }

    private void Clear()
    {
        ItemTypes tempItemType = (ItemTypes)selectedItem;
        
        if (tempItemType == ItemTypes.Null)
            return;
        
        mousePosition = Vector3.zero;
        for (int i = 0; i < level.LevelGrid.Count; i++)
        {
            if (level.LevelGrid[i].type == tempItemType)
            {
                level.LevelGrid.RemoveAt(i);
                i--;
            }
        }
    }
    
}
#endif