using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace XvoleR.Unity.EventBusModule
{
    public class EventBusEditorWindow : EditorWindow
    {
        [MenuItem("Window/XvoleR/EventBusEditorWindow")]
        public static void ShowExample()
        {
            EventBusEditorWindow wnd = GetWindow<EventBusEditorWindow>();
            wnd.titleContent = new GUIContent("EventBusEditorWindow");
        }
        IEnumerable<System.Type> eventBaseTypes;
        protected ScrollView scrollView { get; private set; }

        private void OnEnable()
        {

            var button = new Button(Refresh) { text = "Refresh" };
            rootVisualElement.Add(button);

            scrollView = new ScrollView();
            scrollView.style.flexGrow = 1f;
            scrollView.viewDataKey = "main-scroll-bar";
            scrollView.verticalScroller.slider.pageSize = 100;
            rootVisualElement.Add(scrollView);
            Refresh();
        }
        protected virtual void Refresh()
        {
            scrollView.Clear();
            RefreshKnownEventTypes();
        }

        private void RefreshKnownEventTypes()
        {
            eventBaseTypes = EventBusFactory.GetAllEventsTypes().OrderBy(t => t.FullName);
            foreach (var item in eventBaseTypes)
            {
                VisualElement visualElement = new Label(string.Format("[{0}, {1}]: {2}", item.FullName, item.Assembly.GetName().Name, item.Assembly.CodeBase));
                scrollView.Add(visualElement);
            }
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            //VisualElement root = rootVisualElement;

            //// VisualElements objects can contain other VisualElement following a tree hierarchy.
            //VisualElement label = new Label("Hello World! From C#");
            //root.Add(label);

            //// Import UXML
            //var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Modules/XvoleR.EventBusModule/Editor/EventBusEditorWindow.uxml");
            //VisualElement labelFromUXML = visualTree.Instantiate();
            //root.Add(labelFromUXML);

            //// A stylesheet can be added to a VisualElement.
            //// The style will be applied to the VisualElement and all of its children.
            //var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Modules/XvoleR.EventBusModule/Editor/EventBusEditorWindow.uss");
            //VisualElement labelWithStyle = new Label("Hello World! With Style");
            //labelWithStyle.styleSheets.Add(styleSheet);
            //root.Add(labelWithStyle);
            //for (int i = 0; i < 20; i++)
            //{

            //    foreach (var item in eventBaseTypes)
            //    {
            //        VisualElement visualElement = new Label(item.FullName);
            //        root.Add(visualElement);
            //    }
            //}

        }
    }
}