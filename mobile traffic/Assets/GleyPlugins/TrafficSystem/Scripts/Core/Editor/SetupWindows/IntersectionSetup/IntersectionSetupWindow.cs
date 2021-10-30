﻿using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GleyTrafficSystem
{
    public class IntersectionSetupWindow : SetupWindowBase
    {
        const string intersectionHolderName = "IntersectionsHolder";
        const string intersectionPrefix = "Intersection_";

        private List<PriorityIntersectionSettings> allPriorityIntersections;
        private List<TrafficLightsIntersectionSettings> allTrafficLightsIntersections;
        private Transform intersectionHolder;
        private IntersectionSave save;
        private RoadColors roadColors;
        private float scrollAdjustment = 196;


        public override ISetupWindow Initialize(WindowProperties windowProperties)
        {
            IntersectionDrawer.onIntersectionClicked += IntersectionClicked;
            save = SettingsLoader.LoadIntersectionsSettings();
            roadColors = SettingsLoader.LoadRoadColors();
            LoadIntersections();
            return base.Initialize(windowProperties);
        }


        public override void DrawInScene()
        {
            if (GleyUtilities.SceneCameraMoved())
            {
                SettingsWindow.Refresh();
            }
            for (int i = 0; i < allPriorityIntersections.Count; i++)
            {
                IntersectionDrawer.DrawIntersection(allPriorityIntersections[i], save.priorityColor, allPriorityIntersections[i].enterWaypoints, save.stopWaypointsColor, roadColors.textColor, allPriorityIntersections[i].exitWaypoints, save.exitWaypointsColor);
            }
            for (int i = 0; i < allTrafficLightsIntersections.Count; i++)
            {
                IntersectionDrawer.DrawIntersection(allTrafficLightsIntersections[i], save.lightsColor, allTrafficLightsIntersections[i].stopWaypoints, save.stopWaypointsColor, roadColors.textColor);
            }

            base.DrawInScene();
        }


        protected override void TopPart()
        {
            base.TopPart();
            if (GUILayout.Button("Create Priority Intersection"))
            {
                Transform intersection = CreateIntersectionObject();
                IntersectionClicked(intersection.gameObject.AddComponent<PriorityIntersectionSettings>());
            }
            EditorGUILayout.Space();

            if (GUILayout.Button("Create Traffic Lights Intersection"))
            {
                Transform intersection = CreateIntersectionObject();
                IntersectionClicked(intersection.gameObject.AddComponent<TrafficLightsIntersectionSettings>());
            }
            EditorGUILayout.Space();

            save.showAll = EditorGUILayout.Toggle("Show All Intersections", save.showAll);
        }


        protected override void ScrollPart(float width, float height)
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, false, GUILayout.Width(width - SCROLL_SPACE), GUILayout.Height(height - scrollAdjustment));

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Priority Intersections");
            for (int i = 0; i < allPriorityIntersections.Count; i++)
            {
                if (!save.showAll)
                {
                    if (!GleyUtilities.IsPointInsideView(allPriorityIntersections[i].transform.position))
                    {
                        continue;
                    }
                }
                DrawIntersectionButton(allPriorityIntersections[i]);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Traffic Light Intersections");
            for (int i = 0; i < allTrafficLightsIntersections.Count; i++)
            {
                if (!save.showAll)
                {
                    if (!GleyUtilities.IsPointInsideView(allTrafficLightsIntersections[i].transform.position))
                    {
                        continue;
                    }
                }
                DrawIntersectionButton(allTrafficLightsIntersections[i]);
            }
            EditorGUILayout.EndVertical();

            GUILayout.EndScrollView();
        }


        public override void DestroyWindow()
        {
            IntersectionDrawer.onIntersectionClicked -= IntersectionClicked;
            SettingsLoader.SaveIntersectionsSettings(save);
            base.DestroyWindow();
        }


        private void IntersectionClicked(GenericIntersectionSettings clickedIntersection)
        {
            NavigationRuntimeData.SetSelectedIntersection(clickedIntersection);
            if (clickedIntersection.GetType().Equals(typeof(TrafficLightsIntersectionSettings)))
            {
                SettingsWindow.SetActiveWindow(WindowType.TrafficLightsIntersection, true);
            }
            if (clickedIntersection.GetType().Equals(typeof(PriorityIntersectionSettings)))
            {
                SettingsWindow.SetActiveWindow(WindowType.PriorityIntersection, true);
            }
        }


        private void LoadIntersections()
        {
            allPriorityIntersections = new List<PriorityIntersectionSettings>();
            allTrafficLightsIntersections = new List<TrafficLightsIntersectionSettings>();
            Transform intersectionHolder = GetIntersectionHolder();
            for (int i = 0; i < intersectionHolder.childCount; i++)
            {
                GenericIntersectionSettings intersection = intersectionHolder.GetChild(i).GetComponent<GenericIntersectionSettings>();
                if (intersection != null)
                {
                    if (intersection.GetType().Equals(typeof(PriorityIntersectionSettings)))
                    {
                        allPriorityIntersections.Add(intersection as PriorityIntersectionSettings);
                    }

                    if (intersection.GetType().Equals(typeof(TrafficLightsIntersectionSettings)))
                    {
                        allTrafficLightsIntersections.Add(intersection as TrafficLightsIntersectionSettings);
                    }
                }
            }
        }


        private Transform CreateIntersectionObject()
        {
            GameObject intersection = new GameObject(intersectionPrefix + GetFreeRoadNumber());
            intersection.transform.SetParent(GetIntersectionHolder());
            intersection.gameObject.tag = Constants.editorTag;
            Vector3 poz = SceneView.lastActiveSceneView.camera.transform.position;
            poz.y = 0;
            intersection.transform.position = poz;
            return intersection.transform;
        }


        private int GetFreeRoadNumber()
        {
            return GetIntersectionHolder().childCount;
        }


        private Transform GetIntersectionHolder()
        {
            bool editingInsidePrefab = GleyPrefabUtilities.EditingInsidePrefab();
            if (intersectionHolder == null)
            {
                GameObject holder = null;
                if (editingInsidePrefab)
                {
                    GameObject prefabRoot = GleyPrefabUtilities.GetScenePrefabRoot();
                    Transform waypointsHolder = prefabRoot.transform.Find(intersectionHolderName);
                    if (waypointsHolder == null)
                    {
                        waypointsHolder = new GameObject(intersectionHolderName).transform;
                        waypointsHolder.SetParent(prefabRoot.transform);
                    }
                    holder = waypointsHolder.gameObject;
                }
                else
                {

                    GameObject[] allObjects = Object.FindObjectsOfType<GameObject>().Where(obj => obj.name == intersectionHolderName).ToArray();
                    if (allObjects.Length > 0)
                    {
                        for (int i = 0; i < allObjects.Length; i++)
                        {
                            if (!GleyPrefabUtilities.IsInsidePrefab(allObjects[i]))
                            {
                                holder = allObjects[i];
                                break;
                            }
                        }
                    }
                    if (holder == null)
                    {
                        holder = new GameObject(intersectionHolderName);
                    }
                }
                intersectionHolder = holder.transform;
            }
            return intersectionHolder;
        }


        private void DrawIntersectionButton(GenericIntersectionSettings intersection)
        {
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            EditorGUILayout.LabelField(intersection.name);
            if (GUILayout.Button("View", GUILayout.Width(BUTTON_DIMENSION)))
            {
                GleyUtilities.TeleportSceneCamera(intersection.transform.position, 10);
            }
            if (GUILayout.Button("Edit", GUILayout.Width(BUTTON_DIMENSION)))
            {
                IntersectionClicked(intersection);
            }
            if (GUILayout.Button("Delete", GUILayout.Width(BUTTON_DIMENSION)))
            {
                DestroyImmediate(intersection.gameObject);
                LoadIntersections();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
