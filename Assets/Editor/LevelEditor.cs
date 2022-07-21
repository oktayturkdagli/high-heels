#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(LevelManager), true)]
    public class LevelEditor : UnityEditor.Editor
    {
        private LevelManager levelManager;
        private Level currentLevel ;
        private Vector3 mousePosition;
        private int levelIndex = 1;
        private bool onMouseDown = false;
        private string[] itemTypesText;
        private int selectedItem = 0;
        private const string HelpBoxText =
            "\nUsing this custom editor you can design your grid based level.\n" +
            "\nNull option deletes the objects you specify with the pointer\n";

        private void OnEnable()
        {
            ShowItemsInTabMenu();
            SceneView.duringSceneGui += OnScene;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnScene;
        }

        private void OnScene(SceneView sceneView)
        {
            PullLevelData();
            
            if (levelManager == null || currentLevel == null)
                return;
            
            CheckInput();
            HandleInput();
            Draw3DObjectOnScene();
        }
        
        public override void OnInspectorGUI()
        {
            if (currentLevel == null)
                return;
            
            DrawMyInspector();
        }

        private void DrawMyInspector()
        {
            EditorGUILayout.Space(); GUILayout.Label("Create Your Level", EditorStyles.boldLabel); EditorGUILayout.Space();
            EditorGUILayout.Space(); GUILayout.Label("Draw", EditorStyles.boldLabel); EditorGUILayout.Space();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Current Level", EditorStyles.boldLabel);
            levelIndex = EditorGUILayout.IntField(levelIndex);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("width", EditorStyles.boldLabel);
            currentLevel.width = EditorGUILayout.IntField(currentLevel.width);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Label("height", EditorStyles.boldLabel);
            currentLevel.height = EditorGUILayout.IntField(currentLevel.height);
            GUILayout.EndHorizontal();

            DrawMyButtons();
            
            EditorGUILayout.Space(20);
            GUILayout.Label("Levels", EditorStyles.boldLabel);
            serializedObject.Update();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("levelContainer"), true);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.Space(20);
            EditorGUILayout.HelpBox(HelpBoxText, MessageType.None);
            EditorUtility.SetDirty(levelManager);
        }

        private void DrawMyButtons()
        {
            if (GUILayout.Button("Draw width x height Road"))
                CreateRoad();
        
            selectedItem = GUILayout.Toolbar(selectedItem, itemTypesText);
        
            EditorGUILayout.Space(20); GUILayout.Label("Clear or Save", EditorStyles.boldLabel); EditorGUILayout.Space();
            if (GUILayout.Button("Remove Last"))
                RemoveAt(mousePosition);
            if (GUILayout.Button("Clear All Of Selected"))
                Clear();
            if (GUILayout.Button("Save"))
                levelManager.Save();
        }
    
        private void Draw3DObjectOnScene()
        {
            if (currentLevel == null)
                return;

            Handles.color = Color.magenta;
            Handles.DrawWireDisc(Vector3.zero, Vector3.up, 0.5f);
            if (currentLevel.levelGrid.Count < 1)
                return;

            for (var i = 0; i < currentLevel.levelGrid.Count; i++)
            {
                switch (currentLevel.levelGrid[i].type)
                {
                    case ItemTypes.Null:
                        break;
                    case ItemTypes.Road:
                        Handles.color = Color.white;
                        Handles.DrawWireCube(new Vector3(currentLevel.levelGrid[i].position.x, currentLevel.levelGrid[i].position.y, 
                            currentLevel.levelGrid[i].position.z + currentLevel.height/2), new Vector3(currentLevel.width,0f,currentLevel.height));
                        break;
                    case ItemTypes.Cube:
                        Handles.color = Color.red;
                        Handles.DrawWireCube(currentLevel.levelGrid[i].position, Vector3.one);
                        break;
                    case ItemTypes.Shoe:
                        Handles.color = Color.magenta;
                        Handles.DrawWireCube(currentLevel.levelGrid[i].position, Vector3.one);
                        break;
                    case ItemTypes.Diamond:
                        Handles.color = Color.cyan;
                        Handles.DrawWireCube(currentLevel.levelGrid[i].position, Vector3.one);
                        break;
                    case ItemTypes.Finish:
                        Handles.color = Color.green;
                        Handles.DrawWireDisc(currentLevel.levelGrid[i].position, Vector3.up, currentLevel.width);
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
                float drawPlaneheight = 0;
                float distanceToDrawPlane = (drawPlaneheight - ray.origin.y) / ray.direction.y;
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
        
            var levelItem = new LevelItem
            {
                type = tempItemType,
                position = position
            };
            currentLevel.levelGrid.Add(levelItem);
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
            for (int i = 0; i < currentLevel.levelGrid.Count; i++)
            {
                if (currentLevel.levelGrid[i].position == position)
                {
                    currentLevel.levelGrid.RemoveAt(i);
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
            for (int i = 0; i < currentLevel.levelGrid.Count; i++)
            {
                if (currentLevel.levelGrid[i].type == tempItemType)
                {
                    currentLevel.levelGrid.RemoveAt(i);
                    i--;
                }
            }
        }
    
        private void ShowItemsInTabMenu()
        {
            int itemTypesLength = Enum.GetValues(typeof(ItemTypes)).Length;
            itemTypesText = new string[itemTypesLength];
            for (int i = 0; i < itemTypesLength; i++)
            {
                itemTypesText[i] = ((ItemTypes)i).ToString();
            }
        }

        private void PullLevelData()
        {
            levelManager = target as LevelManager;
            if (levelManager != null)
            {
                currentLevel = levelManager.BringLevel(levelIndex);
            }
        }
    }
}
#endif