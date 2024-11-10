using JDoddsNAIT.ObjectDetection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JDoddsNAIT.ObjectDetection
{
    [RequireComponent(typeof(Collider))]
    public class ObjectDetector : MonoBehaviour
    {
        protected enum DetectionMode { Continuous, Discrete }

        protected virtual Func<GameObject, bool> Condition { get; } = obj => obj != null && obj.activeInHierarchy;

        [SerializeField] private DetectionMode detectionMode;

        [SerializeField] private DetectionEvents events;
        public DetectionEvents DetectionEvents { get => events; set => events = value; }

        [SerializeField] private List<GameObject> detectedObjects = new();
        public List<GameObject> DetectedObjects { get => detectedObjects; set => detectedObjects = value; }

        protected void OnCollisionEnter(Collision collision) => ObjectEnter(collision.collider.gameObject);
        protected void OnCollisionEnter2D(Collision2D collision) => ObjectEnter(collision.collider.gameObject);

        protected void OnCollisionStay(Collision collision) => ObjectStay(collision.collider.gameObject);
        protected void OnCollisionStay2D(Collision2D collision) => ObjectStay(collision.collider.gameObject);

        protected void OnCollisionExit(Collision collision) => ObjectExit(collision.collider.gameObject);
        protected void OnCollisionExit2D(Collision2D collision) => ObjectExit(collision.collider.gameObject);

        protected void OnTriggerEnter(Collider other) => ObjectEnter(other.gameObject);
        protected void OnTriggerEnter2D(Collider2D other) => ObjectEnter(other.gameObject);

        protected void OnTriggerStay(Collider other) => ObjectStay(other.gameObject);
        protected void OnTriggerStay2D(Collider2D other) => ObjectStay(other.gameObject);

        protected void OnTriggerExit(Collider other) => ObjectExit(other.gameObject);
        protected void OnTriggerExit2D(Collider2D other) => ObjectExit(other.gameObject);

        private void ObjectEnter(GameObject queryObject)
        {
            if (Condition(queryObject))
            {
                DetectedObjects.Add(queryObject);
                DetectionEvents.OnEnterDetection.Invoke(queryObject);
            }
        }

        private void ObjectStay(GameObject queryObject)
        {
            if (detectionMode == DetectionMode.Continuous)
            {
                if (!DetectedObjects.Contains(queryObject) && Condition(queryObject))
                {
                    DetectedObjects.Add(queryObject);
                    DetectionEvents.OnEnterDetection.Invoke(queryObject);
                }
                else if (DetectedObjects.Contains(queryObject) && !Condition(queryObject))
                {
                    DetectedObjects.Remove(queryObject);
                    DetectionEvents.OnExitDetection.Invoke(queryObject);
                }
            }
        }

        private void ObjectExit(GameObject queryObject)
        {
            if (DetectedObjects.Contains(queryObject))
            {
                DetectedObjects.Remove(queryObject);
                DetectionEvents.OnExitDetection.Invoke(queryObject);
            }
        }
    }
}