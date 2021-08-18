using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Creos.Cameras
{
    public class CameraMenu : MonoBehaviour
    {
        #region Create Menu
        [MenuItem("Creos/Camera/Create Camera")]
        public static void CreateCamera()
        {
            GameObject[] selectedGO = Selection.gameObjects;
            if (selectedGO.Length > 0)
            {
                if (selectedGO.Length < 2)
                {
                    AttachCameraScript(selectedGO[0].gameObject, null);
                    EditorUtility.DisplayDialog("Camera Tools", "You need to select Camera and GameObject in the scene " +
                        "for this to work and the first selection needs to be a camera!", "OK");
                }
                else if (selectedGO.Length == 2)
                {
                    AttachCameraScript(selectedGO[0].gameObject, selectedGO[1].transform);
                    EditorUtility.DisplayDialog("Camera Tools", "Script successfully added.", "OK");
                }
                else if (selectedGO.Length == 3)
                {
                    EditorUtility.DisplayDialog("Camera Tools", "You can only select two GameObjects in the scene " +
                        "for this to work and the first selection needs to be a camera!", "OK");
                }
            }
            else
            {
                EditorUtility.DisplayDialog("Camera Tools", "You need to select a GameObject in the scene " +
                    "that has a Camera component assigned to it!", "OK");
            }

        }

        [MenuItem("Creos/Object/Pickable Item")]
        public static void CreatePickable()
        {
            GameObject[] selectedGO = Selection.gameObjects;
            int i = 0;
            if (selectedGO.Length > 0)
            {
                if (selectedGO.Length > 1)
                {
                    while (selectedGO.Length > i)
                    {
                        AttachPickScript(selectedGO[i].gameObject);
                        i++;
                    }
                    EditorUtility.DisplayDialog("Item Tools", "Script successfully added.", "OK");
                }
                if (selectedGO.Length < 2)
                {
                    AttachPickScript(selectedGO[0].gameObject);
                    EditorUtility.DisplayDialog("Item Tools", "Script successfully added." + "You can select more item.", "OK");

                }
            }
            else
            {
                EditorUtility.DisplayDialog("Item Tools", "You need to select a GameObject in the scene ", "OK");
            }

        }

        [MenuItem("Creos/Camera/Transparency")]
        public static void CreateTransparency()
        {
            GameObject[] selectedGO = Selection.gameObjects;
            if (selectedGO.Length > 0)
            {
                AttachTransparencyScript(selectedGO[0].gameObject);
                EditorUtility.DisplayDialog("Camera Tools", "Script successfully added.", "OK");
            }
            else
            {
                EditorUtility.DisplayDialog("Camera Tools", "You need to select a GameObject in the scene ", "OK");
            }
        }

        [MenuItem("Creos/Player/Character Movement")]
        public static void CreateMovement()
        {
            GameObject[] selectedGO = Selection.gameObjects;
            if (selectedGO.Length > 0)
            {
                AttachCharacterMovementScript(selectedGO[0].gameObject);
                EditorUtility.DisplayDialog("Player Tools", "Script successfully added.", "OK");
            }
            else
            {
                EditorUtility.DisplayDialog("Player Tools", "You need to select a GameObject in the scene ", "OK");
            }
        }

        [MenuItem("Creos/Player/Push")]
        public static void CreatePush()
        {
            GameObject[] selectedGO = Selection.gameObjects;
            if (selectedGO.Length > 0)
            {
                AttachPushScript(selectedGO[0].gameObject);
                EditorUtility.DisplayDialog("Player Tools", "Script successfully added. If you want to push to the object, don't forget to add a Pushable tag to the object.", "OK");
            }
            else
            {
                EditorUtility.DisplayDialog("Player Tools", "You need to select a GameObject in the scene ", "OK");
            }
        }
        #endregion

        #region Attach Script
        static void AttachCameraScript(GameObject aCamera, Transform aTarget)
        {
            //Assign Camera Settings Script to the camera
            CameraSettings cameraScript = null;
            if (aCamera)
            {
                cameraScript = aCamera.AddComponent<CameraSettings>();

                //Check to see if we have a Target and we have a script reference
                if (cameraScript && aTarget)
                {
                    cameraScript.m_Target = aTarget;
                }

                Selection.activeGameObject = aCamera;
            }
        }
        static void AttachPickScript(GameObject aPick)
        {
            //Assign Pickable Item Script to the camera
            PickableItem PickScript = null;
            if (aPick)
            {
                PickScript = aPick.AddComponent<PickableItem>();
                Selection.activeGameObject = aPick;
            }
        }
        static void AttachTransparencyScript(GameObject aTransparent)
        {
            //Assign Transparency Script to the camera
            Transparency TransparentScript = null;
            if (aTransparent)
            {
                TransparentScript = aTransparent.AddComponent<Transparency>();
                Selection.activeGameObject = aTransparent;
            }
        }
        static void AttachCharacterMovementScript(GameObject aMovement)
        {
            //Assign Character Movement Script to the Player
            CharacterMovement MovementScript = null;
            if (aMovement)
            {
                MovementScript = aMovement.AddComponent<CharacterMovement>();
                Selection.activeGameObject = aMovement;
            }
        }
        static void AttachPushScript(GameObject aPush)
        {
            //Assign Push Script to the Player
            Push PushScript = null;
            if (aPush)
            {
                PushScript = aPush.AddComponent<Push>();
                Selection.activeGameObject = aPush;
            }
        }

        #endregion
    }
}
