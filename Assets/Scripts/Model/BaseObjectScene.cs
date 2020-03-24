using UnityEngine;


namespace Geekbrains
{
    public abstract class BaseObjectScene : MonoBehaviour
    {
        #region Fields

        private int _layer;
        private Color _color;
        private Vector3 _position;
        private Quaternion _rotation;
        private Vector3 _scale;
        private GameObject _instanceObject;

        #endregion


        #region Properties

        public Rigidbody Rigidbody { get; private set; }
        public Transform Transform { get; private set; }
        public Material Material { get; private set; }
        public string Name { get; private set; }

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
            get
            {
                if (_instanceObject != null)
                {
                    _position = Transform.position;
                }
                return _position;
            }

            set
            {
                _position = value;

                if (_instanceObject != null)
                {
                    Transform.position = _position;
                }
            }
        }

        public Color Color
        {
            get { return _color; }

            set
            {
                _color = value;

                if(Material != null)
                {
                    Material.color = _color;
                    AskColor(Transform, _color);
                }
            }
        }

        public Quaternion Rotation
        {
            get
            {
                if (_instanceObject != null)
                {
                    _rotation = Transform.rotation;
                }

                return _rotation;
            }

            set
            {
                _rotation = value;

                if(_instanceObject != null)
                {
                    Transform.rotation = _rotation;
                }
            }
        }

        public Vector3 Scale
        {
            get
            {
                if (_instanceObject != null)
                {
                    _scale = Transform.localScale;
                }
                return _scale;
            }

            set
            {
                _scale = value;

                if(_instanceObject != null)
                {
                    Transform.localScale = _scale;
                }
            }
        }

        #endregion


        #region UnityMethods

        protected virtual void Awake()
        {
            _instanceObject = gameObject;
            Name = _instanceObject.name;

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
            var objMaterial = obj.gameObject.GetComponent<Material>();
            if(objMaterial != null)
            {
                objMaterial.color = color;
            }

            if (obj.childCount <= 0) return;

            foreach (Transform child in obj)
            {
                AskColor(child, color);
            }
        }

        #endregion
    }
}
