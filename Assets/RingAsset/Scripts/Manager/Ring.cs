using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Ring
{
    #region Component

    [Serializable]
    public class GameController
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Game Controller")]
        [ChangeColorLabel(0.2f, 1, 1)] public GameObject _player;
    }

    [Serializable]
    public class PlayerController
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Player Controller")]
        [ChangeColorLabel(0.2f, 1, 1)] public Rigidbody _rigidbodyWeapon;
    }

    [Serializable]
    public class BotController
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Bot Controller")]
        [ChangeColorLabel(0.2f, 1, 1)] public Rigidbody _rigidbodyBot;
    }

    [Serializable]
    public class MusicController
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Audio Clip")]
        [ChangeColorLabel(0.9f, .55f, .95f)] public AudioClip audioClip_;
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Audio Source")]
        [ChangeColorLabel(0.2f, 1, 1)] public AudioSource audioSource_;
    }

    [Serializable]
    public class UiController
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Ui Controller")]
        [ChangeColorLabel(0.2f, 1, 1)] public GameObject _winGameObject;
    }

    [Serializable]
    public class CheckScene
    {
        [HeaderTextColor(0.2f, .7f, .8f, headerText = "Check Scene")]
        [ChangeColorLabel(.7f, 1f, 1f)] public bool _isGetCurrentNameScene;

        [ChangeColorLabel(.7f, 1f, 1f)] public string _nameSceneChange;
    }

    #endregion Component

    #region Text Color

#if UNITY_EDITOR

    [CustomPropertyDrawer(typeof(HeaderTextColorAttribute))]
    public class HeaderTextColorDecorator : DecoratorDrawer
    {
        private GUIStyle headerStyle;
        private bool initialized;

        private void InitGUIStyle()
        {
            headerStyle = new GUIStyle(GUI.skin.label);
            headerStyle.fontStyle = FontStyle.Bold;
            headerStyle.normal.textColor = ((HeaderTextColorAttribute)attribute).color;
            initialized = true;
        }

        public override float GetHeight()
        {
            /*if (!initialized)
            {
                InitGUIStyle();
            }*/

            return EditorGUIUtility.singleLineHeight * 2;
        }

        public override void OnGUI(Rect position)
        {
            if (!initialized)
            {
                InitGUIStyle();
            }

            HeaderTextColorAttribute attribute = (HeaderTextColorAttribute)this.attribute;
            string headerText = attribute.headerText;

            Rect labelRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth + 100, 50);
            EditorGUI.LabelField(labelRect, headerText, headerStyle);
        }
    }

    [CustomPropertyDrawer(typeof(ChangeColorLabelAttribute))]
    public class RedLabelDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Get the color from the attribute
            ChangeColorLabelAttribute changeColorLabelAttribute = (ChangeColorLabelAttribute)attribute;
            Color labelColor = changeColorLabelAttribute.color;

            // Set the color
            GUIStyle coloredLabelStyle = new GUIStyle(EditorStyles.label) { normal = { textColor = labelColor } };
            float originalLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = EditorStyles.label.CalcSize(label).x;

            // Draw the colored label
            EditorGUI.LabelField(position, label, coloredLabelStyle);

            // Draw the property without the label
            EditorGUIUtility.labelWidth = originalLabelWidth;
            position.x += EditorGUIUtility.labelWidth;
            position.width -= EditorGUIUtility.labelWidth;
            EditorGUI.PropertyField(position, property, GUIContent.none, true);

            EditorGUI.EndProperty();
        }
    }

#endif

    public class HeaderTextColorAttribute : PropertyAttribute
    {
        public Color color;
        public string headerText;

        public HeaderTextColorAttribute(float r, float g, float b, float a = 1.0f, string headerText = "")
        {
            color = new Color(r, g, b, a);
            this.headerText = headerText;
        }
    }

    public class ChangeColorLabelAttribute : PropertyAttribute
    {
        public Color color;

        public ChangeColorLabelAttribute(float r, float g, float b, float a = 1.0f)
        {
            color = new Color(r, g, b, a);
        }
    }

    #endregion Text Color

    #region Editor Window

#if UNITY_EDITOR

    #region Save Position Object

    public class SavingPositionObject : EditorWindow
    {
        private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

        #region Saving Position Object
        //Load luôn position sau khi dừng play
        [MenuItem("Ring/Save Position/Saving Position - After Play Load Position Now")]
        public static void ShowWindow()
        {
            GetWindow<SavingPositionObject>("Saving Position Object");
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += HandlePlayModeChange;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= HandlePlayModeChange;
        }

        private void HandlePlayModeChange(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode)
            {
                LoadPositions();
                Debug.Log(1);
            }
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Save Positions"))
            {
                SavePositions();
            }
        }

        private void SavePositions()
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                originalPositions[obj] = obj.transform.position;
            }
        }

        private void LoadPositions()
        {
            foreach (KeyValuePair<GameObject, Vector3> entry in originalPositions)
            {
                if (entry.Key != null)
                {
                    entry.Key.transform.position = entry.Value;
                }
            }
        }

        #endregion Saving Position Object
    }

    public class ObjectPositionSaverEditor : EditorWindow
    {
        private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();

        #region Object Position Saver
        //Cho phép chọn load hay không sau khi play
        [MenuItem("Ring/Save Position/Saving Position - After Play, Change Button Load Position")]
        public static void ShowWindow()
        {
            GetWindow<ObjectPositionSaverEditor>("Object Position Saver");
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Save Positions"))
            {
                SavePositions();
            }

            if (GUILayout.Button("Load Positions"))
            {
                LoadPositions();
            }
        }

        private void SavePositions()
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                originalPositions[obj] = obj.transform.position;
            }
        }

        private void LoadPositions()
        {
            foreach (KeyValuePair<GameObject, Vector3> entry in originalPositions)
            {
                entry.Key.transform.position = entry.Value;
            }
        }

        #endregion Object Position Saver
    }

    public class TransformListEditorWindow : EditorWindow
    {
        [SerializeField] private Transform[] objectList;
        //Lưu transform cho list object
        [MenuItem("Ring/Save Position/Save Transform List - VIP(Save Even While Playing)")]
        public static void ShowWindow()
        {
            GetWindow<TransformListEditorWindow>("Transform List Editor");
        }

        private void OnGUI()
        {
            GUILayout.Label("Transform List Editor", EditorStyles.boldLabel);

            // Hiển thị danh sách Transform
            EditorGUILayout.LabelField("Object List:");
            EditorGUI.indentLevel++;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty objectListProperty = serializedObject.FindProperty("objectList");
            EditorGUILayout.PropertyField(objectListProperty, true);
            serializedObject.ApplyModifiedProperties();

            EditorGUI.indentLevel--;

            // Button lưu transform
            if (GUILayout.Button("Save Transform"))
            {
                SaveTransformList();
            }

            // Button load transform khi tắt chế độ chơi
            if (GUILayout.Button("Load Transform"))
            {
                LoadTransformList();
            }
        }

        private void SaveTransformList()
        {
            if (objectList != null && objectList.Length > 0)
            {
                EditorPrefs.SetInt("TransformListCount", objectList.Length);

                for (int i = 0; i < objectList.Length; i++)
                {
                    if (objectList[i] != null)
                    {
                        EditorPrefs.SetString($"Transform_{i}_Position", objectList[i].position.ToString());
                        EditorPrefs.SetString($"Transform_{i}_Rotation", objectList[i].rotation.ToString());
                    }
                }

                Debug.Log("Transform List Saved!");
            }
            else
            {
                Debug.LogWarning("No objects selected to save!");
            }
        }

        private void LoadTransformList()
        {
            int transformListCount = EditorPrefs.GetInt("TransformListCount", 0);

            if (transformListCount > 0)
            {
                for (int i = 0; i < transformListCount; i++)
                {
                    string positionString = EditorPrefs.GetString($"Transform_{i}_Position", "");
                    string rotationString = EditorPrefs.GetString($"Transform_{i}_Rotation", "");

                    if (!string.IsNullOrEmpty(positionString) && !string.IsNullOrEmpty(rotationString))
                    {
                        Vector3 position = ParseVector3(positionString);
                        Quaternion rotation = ParseQuaternion(rotationString);

                        if (objectList != null && i < objectList.Length && objectList[i] != null)
                        {
                            objectList[i].position = position;
                            objectList[i].rotation = rotation;
                        }
                    }
                }

                Debug.Log("Transform List Loaded!");
            }
            else
            {
                Debug.LogWarning("No saved transform list to load!");
            }
        }

        private Vector3 ParseVector3(string vectorString)
        {
            vectorString = vectorString.Replace("(", "").Replace(")", "");
            string[] values = vectorString.Split(',');

            if (values.Length == 3)
            {
                float x = float.Parse(values[0]);
                float y = float.Parse(values[1]);
                float z = float.Parse(values[2]);

                return new Vector3(x, y, z);
            }

            return Vector3.zero;
        }

        private Quaternion ParseQuaternion(string quaternionString)
        {
            quaternionString = quaternionString.Replace("(", "").Replace(")", "");
            string[] values = quaternionString.Split(',');

            if (values.Length == 4)
            {
                float x = float.Parse(values[0]);
                float y = float.Parse(values[1]);
                float z = float.Parse(values[2]);
                float w = float.Parse(values[3]);

                return new Quaternion(x, y, z, w);
            }

            return Quaternion.identity;
        }
    }
    #endregion Save Position Object

    #region Rename

    #region Rename Scene, Asset

    public class RenamingScenes : EditorWindow
    {
        private SerializedObject so;
        private SerializedProperty scenesProp;
        private int startingNumber;
        //Thay tên scenes
        [MenuItem("Ring/Rename/Scenes")]
        public static void ShowWindow()
        {
            RenamingScenes window = (RenamingScenes)GetWindow(typeof(RenamingScenes));
            window.so = new SerializedObject(window);
            window.scenesProp = window.so.FindProperty("scenes");
            window.Show();
        }

        private void OnGUI()
        {
            // Input field for starting number
            startingNumber = EditorGUILayout.IntField("Starting Number:", startingNumber);

            // Display each scene field
            EditorGUILayout.PropertyField(scenesProp, true);
            so.ApplyModifiedProperties();

            // Button to rename scenes
            if (GUILayout.Button("Rename Scenes"))
            {
                RenameScenes();
            }
        }

        public List<SceneAsset> scenes;

        private void RenameScenes()
        {
            for (int i = 0; i < scenes.Count; i++)
            {
                if (scenes[i] != null)
                {
                    string newPath = AssetDatabase.GetAssetPath(scenes[i]).Replace(scenes[i].name,
                    startingNumber.ToString());
                    AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(scenes[i]), startingNumber.ToString());
                    AssetDatabase.Refresh();
                    startingNumber++;
                }
            }
        }
    }

    public class RenamingAssets : EditorWindow
    {
        private SerializedObject so;
        private SerializedProperty assetsProp;
        private string startingNumber;
        private bool isAddNumber;
        //Thay tên object trên Prefabs
        [MenuItem("Ring/Rename/Assets")]
        public static void ShowWindow()
        {
            RenamingAssets window = (RenamingAssets)GetWindow(typeof(RenamingAssets));
            window.so = new SerializedObject(window);
            window.assetsProp = window.so.FindProperty("assets");
            window.Show();
        }

        private void OnGUI()
        {
            // Input field for starting number
            startingNumber = EditorGUILayout.TextField("Starting Number:", startingNumber);
            isAddNumber = EditorGUILayout.Toggle("Add Number Behind Name ?:", isAddNumber);
            // Display each asset field
            EditorGUILayout.PropertyField(assetsProp, true);
            so.ApplyModifiedProperties();

            // Button to rename assets
            if (GUILayout.Button("Rename Assets"))
            {
                RenameAssets();
            }
        }

        public List<UnityEngine.Object> assets;

        private void RenameAssets()
        {
            for (int i = 0; i < assets.Count; i++)
            {
                if (assets[i] != null)
                {
                    if (Int32.TryParse(startingNumber, out int result))
                    {
                        AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(assets[i]), startingNumber.ToString());
                        AssetDatabase.Refresh();
                        startingNumber = (result + 1).ToString();
                    }
                    else
                    {
                        if (isAddNumber)
                        {
                            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(assets[i]), startingNumber.ToString() + " " + (i + 1));
                            AssetDatabase.Refresh();
                        }
                        else
                        {
                            Debug.LogError("Must number behind");
                        }
                    }
                }
            }
        }
    }

    #endregion Rename Scene, Asset

    #region Rename Object in Scene

    #region Rename => String

    public class RenamingObjectsInSceneToString : EditorWindow
    {
        private string startingName;
        private SerializedObject so;
        private SerializedProperty objectsProp;
        private bool isAddNumber;
        public List<GameObject> objectsToRename;
        //Thay tên object trên scene
        [MenuItem("Ring/Rename/Objects In Scene")]
        public static void ShowWindow()
        {
            RenamingObjectsInSceneToString window = (RenamingObjectsInSceneToString)GetWindow(typeof(RenamingObjectsInSceneToString));
            window.so = new SerializedObject(window);
            window.objectsProp = window.so.FindProperty("objectsToRename");
            window.Show();
        }

        private void OnGUI()
        {
            // Input field for starting number
            startingName = EditorGUILayout.TextField("Starting Name:", startingName);
            isAddNumber = EditorGUILayout.Toggle("Add Number Behind Name ?:", isAddNumber);

            // Display each object field
            EditorGUILayout.PropertyField(objectsProp, true);
            so.ApplyModifiedProperties();

            // Button to rename objects in scene
            if (GUILayout.Button("Rename Objects in Scene"))
            {
                RenameObjectsInScene();
            }
        }

        private void RenameObjectsInScene()
        {
            if (startingName != null)
            {
                for (int i = 0; i < objectsToRename.Count; i++)
                {
                    if (objectsToRename[i] != null)
                    {
                        if (!Int32.TryParse(startingName, out int result))
                        {
                            if (isAddNumber)
                            {
                                objectsToRename[i].name = startingName + " " + i;
                            }
                            else
                            {
                                objectsToRename[i].name = startingName;
                            }
                        }
                        else
                        {
                            objectsToRename[i].name = result.ToString();
                            startingName = (Int32.Parse(startingName) + 1).ToString();
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Null String Name!");
            }
        }
    }

    #endregion Rename => String

    #endregion Rename Object in Scene

    #region Rename Tag Object Scene

    public class RenamingTag : EditorWindow
    {
        private SerializedObject so;
        private SerializedProperty assetsProp;
        //Thay tên hoặc các thuộc tính cho object trên scene
        [MenuItem("Ring/Rename/Rename Prefabs Tag,Name,Layer,Position...")]
        public static void ShowWindow()
        {
            RenamingTag window = (RenamingTag)GetWindow(typeof(RenamingTag));
            window.so = new SerializedObject(window);
            window.assetsProp = window.so.FindProperty("assets");
            window.Show();
        }

        private void OnGUI()
        {
            // Display each asset field
            EditorGUILayout.PropertyField(assetsProp, true);
            so.ApplyModifiedProperties();
            // Button to rename assets
            if (GUILayout.Button("Change Options"))
            {
                ChangeOptions();
            }
        }

        public List<UnityEngine.Object> assets;

        private void ChangeOptions()
        {
            for (int i = 0; i < assets.Count; i++)
            {
                GameObject gameObject = assets[i] as GameObject;
                if (gameObject != null)
                {
                    Transform child = gameObject.transform;

                    // Kiểm tra child tồn tại và có thể đổi tên
                    if (child != null)
                    {
                        // Ghi lại thay đổi
                        Undo.RecordObject(child.gameObject, "Rename Tags");

                        // Thay đổi tên
                        child.gameObject.name = "Mesh";
                        //child.gameObject.tag = "ObjectTouch";
                        //child.gameObject.layer = LayerMask.NameToLayer("ObjectTouch");
                        //child.transform.rotation = Quaternion.Euler(19, 180, 0);

                        // Lưu lại thay đổi
                        EditorUtility.SetDirty(child.gameObject);
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
                }
            }
        }
    }

    #endregion

    #endregion Rename
    #region Component

    #region CopyComponent
    //copy component prefabs
    public class CopyComponentEditorWindow : EditorWindow
    {
        // Chỉ sử dụng nhấn từng prefabs 1 để xác định object và click vào Gui sau đó nhấn phím K sẽ tự động lấy asset
        [MenuItem("Ring/Component/Copy Components In One Prefabs (With Separate Assets - Prefabs)")]
        private static void ShowWindow()
        {
            GetWindow<CopyComponentEditorWindow>().Show();
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Copy Components with Separate Assets - Prefabs"))
            {
                CopyComponentsWithSeparateAssets();
            }
            Event e = Event.current;
            if (e.keyCode == KeyCode.K && e.type == EventType.KeyDown)
            {
                CopyComponentsWithSeparateAssets();
            }
        }

        private void CopyComponentsWithSeparateAssets()
        {
            // Get selected object A
            GameObject objA = Selection.activeGameObject;
            if (objA == null)
            {
                Debug.LogWarning("Please select object A to copy components from.");
                return;
            }
            GameObject objB = objA.transform.GetChild(0).gameObject;
            // Get components to copy
            MeshCollider meshFilterA = objA.GetComponent<MeshCollider>();
            // Handle existing components on object B
            MeshFilter meshFilterB = objB.GetComponent<MeshFilter>();

            meshFilterA.sharedMesh = meshFilterB.sharedMesh;
        }
    }

    #endregion CopyComponent

    #region GetComponent
    public class ObjectTouchColliderEditorWindow : EditorWindow
    {
        //Get component in editor
        [MenuItem("Ring/Component/Auto Get Components for Selected Objects (Allows Multiple Objects To Be Selected )")]
        public static void ShowWindow()
        {
            GetWindow<ObjectTouchColliderEditorWindow>("Auto Assign Components");
        }

        private void OnGUI()
        {
            GUILayout.Label("Auto Assign Components for Selected Objects", EditorStyles.boldLabel);

            if (GUILayout.Button("Auto Assign Components"))
            {
                AutoAssignComponentsForSelectedObjects();
            }
        }

        private void AutoAssignComponentsForSelectedObjects()
        {
            //tạo class ObjectTouch và tạo method getcomponent như bình thường
            /*ObjectTouch[] selectedObjects = Selection.GetFiltered<ObjectTouch>(SelectionMode.Editable | SelectionMode.Deep);
            foreach (ObjectTouch obj in selectedObjects)
            {
                Undo.RecordObject(obj, "Auto Assign Components");
                obj.SetupComponents();
                EditorUtility.SetDirty(obj);
            }*/
        }
    }

    #endregion GetComponent
   
    #endregion
#endif

    #endregion Editor Window

    #region Base Method

    public abstract class RingSingleton<T> : MonoBehaviour where T : RingSingleton<T>
    {
        private static T _instance;

        public enum ChangeDestroy
        {
            DontDestroy,
            Destroy
        }

        public ChangeDestroy _changDestroy = ChangeDestroy.Destroy;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        Debug.LogError("An instance of " + typeof(T) +
                                       " is needed in the scene, but there is none.");
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            _instance = (T)this;
            if (_changDestroy == ChangeDestroy.DontDestroy)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    #endregion Base Method
}