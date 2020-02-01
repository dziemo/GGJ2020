using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;
        Camera cam;
        LineRenderer lr;

        public Transform pos;

        private void Awake()
        {
            cam = Camera.main;
            lr = GetComponent<LineRenderer>();
        }

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
                var pointsToDraw = pathCreator.path.localPoints;
                lr.positionCount = pointsToDraw.Length;
                lr.SetPositions(pointsToDraw);
                Debug.Log(pathCreator.path.length);
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0.0f;
                distanceTravelled = speed;
                Vector3 relativePoint = pathCreator.path.GetPointAtDistance(speed, endOfPathInstruction);
                transform.position = relativePoint;
                //Debug.Log("DISTANCE BETWEEN: " + Vector3.Distance(mousePos, relativePoint));
                transform.rotation = pathCreator.path.GetRotation(speed, endOfPathInstruction);
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}