namespace FirstShooter
{
    public sealed class HealthController : BaseController, IInitialization
    {
        #region Fields
        
        private PlayerHealth _healthModel;
        private PlayerCurrentHpText _healthView; 

        #endregion


        #region Methods 
        
        public override void On()
        {
            base.On();

            _healthModel.OnHealthChange += ChangeHealth;
        }

        public override void Off()
        {
          base.Off();
          
          _healthModel.OnHealthChange -= ChangeHealth;
        }

        private void ChangeHealth(int curHp)
        {
            _healthView.Text = $"{curHp}";
        }

        #endregion


        #region IInitialization

        public void Initialization()
        {
            _healthModel = ServiceLocatorMonoBehaviour.GetService<PlayerHealth>();
            _healthView = UiInterface.PlayerCurrentHpText;
            
            _healthView.Text = $"{_healthModel.Hp}";
        }

        #endregion
    }
}