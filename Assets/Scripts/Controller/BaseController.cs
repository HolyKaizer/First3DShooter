namespace FirstShooter
{
    public abstract class BaseController
    {
        #region Fields

        protected UiInterface _uiInterface;

        #endregion


        #region Properties

        public bool IsActive { get; private set; }

        #endregion


        #region ClassLifecycle

        protected BaseController()
        {
            _uiInterface = new UiInterface();
        }

        #endregion


        #region Methods

        public virtual void On()
        {
            On(null);
        }

        public virtual void On(params BaseObjectScene[] obj)
        {
            IsActive = true;
        }

        public virtual void Off()
        {
            IsActive = false;
        }

        public void Switch(params BaseObjectScene[] obj)
        {
            if (!IsActive)
            {
                On();
            }
            else
            {
                Off();
            }
        }

        #endregion
    }
}
