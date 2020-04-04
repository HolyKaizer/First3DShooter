using UnityEngine;


namespace FirstShooter
{
    public abstract class BaseObjectScene : MonoBehaviour
    {
        #region Fields

        private Color _color;
        private Vector3 _position;
        private Quaternion _rotation;
        private Vector3 _scale;
        private int _layer;
        private bool _isVisible;

        #endregion


        #region Properties

        [HideInInspector] public Rigidbody Rigidbody { get; private set; }
        [HideInInspector] public Transform Transform { get; private set; }
        [HideInInspector] public Material Material { get; private set; }
        [HideInInspector] public string Name { get; private set; }

        public int Layer
        {
            get => _layer;

            set
            {
                _layer = value;
                AskLayer(Transform, _layer);
            }
        }

        public Vector3 Position
        {
            get => Transform.position;

            set
            {
                _position = value;

                Transform.position = _position;
            }
        }

        public Color Color
        {
            get => _color;

            set
            {
                _color = value;

                AskColor(Transform, _color);
            }
        }

        public Quaternion Rotation
        {
            get => Transform.rotation;

            set
            {
                _rotation = value;
                Transform.rotation = _rotation;
            }
        }

        public Vector3 Scale
        {
            get => Transform.localScale;

            set
            {
                _scale = value;

                Transform.localScale = _scale;
            }
        }

        public bool IsVisible
        {
            get => _isVisible;

            set
            {
                _isVisible = value;
                RendererSetActive(transform);
                if (transform.childCount <= 0) return;

                foreach (Transform t in transform)
                {
                    RendererSetActive(t);
                }
            }
        }

        public bool ActiveInHierarchy
        {
            get => gameObject.activeInHierarchy;
            
        }

        #endregion


        #region UnityMethods

        protected virtual void Awake()
        {
            Name = gameObject.name;

            var renderer = GetComponent<Renderer>();
            if (renderer != null)
            {
                Material = renderer.material;
            }

            Rigidbody = GetComponent<Rigidbody>();
            Transform = transform;
        }

        #endregion


        #region Methods

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        private void AskLayer(Transform obj, int layer)
        {
            obj.gameObject.layer = layer;

            if (obj.childCount <= 0)return;

            foreach (Transform child in obj)
            {
                AskLayer(child, layer);
            }
        }

        private void AskColor(Transform obj, Color color)
        { 
            if(obj.gameObject.TryGetComponent<Renderer>(out var renderer))
            {
                foreach(var objMaterial in renderer.materials)
                {
                    objMaterial.color = color;
                }
            }

            if (obj.childCount <= 0) return;

            foreach (Transform child in obj)
            {
                AskColor(child, color);
            }
        }

        private void RendererSetActive(Transform renderer)
        {
            if (renderer.gameObject.TryGetComponent<Renderer>(out var component))
            {
                component.enabled = _isVisible;
            }
        }

        #endregion
    }
}
