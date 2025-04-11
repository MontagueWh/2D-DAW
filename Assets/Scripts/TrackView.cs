using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Beats
{
    // Ensure the GameObject has required components
    [RequireComponent(typeof(VerticalLayoutGroup))]
    [RequireComponent(typeof(ContentSizeFitter))]
    [RequireComponent(typeof(RectTransform))]
    public class TrackView : MonoBehaviour
    {
        // Enum to represent different trigger states
        public enum Trigger { Missed, Right, Wrong }
        //[SerializeField] Track _track;

        // Serialized fields for different beat directions
        [SerializeField] RectTransform _left;
        [SerializeField] RectTransform _right;
        [SerializeField] RectTransform _up;
        [SerializeField] RectTransform _down;

        // Serialized field for an empty beat
        [SerializeField] RectTransform _empty;

        // Private fields for RectTransform and beat views
        RectTransform _rTransform;
        List<Image> _beatViews;

        // Private fields for position, beat view size, and spacing
        Vector2 _position;
        float _beatViewSize;
        float _spacing;

        // Property to get and set the y position of the track view
        public float position
        {
            get
            {
                return _position.y;
            }

            set
            {
                if (value != _position.y)
                {
                    _position.y = value;
                    _rTransform.anchoredPosition = _position;
                }
            }
        }

        // Initialise the track view with beats from the track
        public void Init(Track track)
        {
            // Get the RectTransform component and set initial position
            _rTransform = (RectTransform)transform;
            _position = _rTransform.anchoredPosition;

            // Set beat view size and spacing
            _beatViewSize = _empty.rect.height;
            _spacing = GetComponent<VerticalLayoutGroup>().spacing;

            // Initialize the list of beat views
            _beatViews = new List<Image>();

            // Loop through each beat in the track and instantiate corresponding beat views
            foreach (int b in track.beats)
            {
                GameObject g;
                switch (b)
                {
                    case 0:
                        g = _left.gameObject;
                        break;

                    case 1:
                        g = _down.gameObject;
                        break;

                    case 2:
                        g = _up.gameObject;
                        break;

                    case 3:
                        g = _right.gameObject;
                        break;

                    default:
                        g = _empty.gameObject;
                        break;
                }

                // Instantiate the beat view and set it as the first sibling
                Image view = GameObject.Instantiate(g, transform).GetComponent<Image>();
                view.transform.SetAsFirstSibling();

                // Add the beat view to the list
                _beatViews.Add(view);
            }
        }

        // Start method to initialize the track view with the current track
        void Start()
        {
            Init(GameplayController.Instance.track);
        }

        // Update method to move the track view based on the beats per second
        void Update()
        {
            position -= (_beatViewSize + _spacing) * Time.deltaTime * GameplayController.Instance.beatsPerSecond;
        }

        // Method to change the color of a beat view based on the trigger state
        public void TriggerBeatView(int index, Trigger trigger)
        {
            switch (trigger)
            {
                case Trigger.Missed:
                    _beatViews[index].color = Color.grey;
                    break;

                case Trigger.Right:
                    _beatViews[index].color = Color.yellow;
                    break;

                case Trigger.Wrong:
                    _beatViews[index].color = Color.black;
                    break;
            }
        }
    }
}
