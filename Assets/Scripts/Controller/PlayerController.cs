﻿namespace Geekbrains
{
	public class PlayerController : BaseController, IExecute
	{
        #region Fields

        private readonly IMotor _motor;

        #endregion


        #region ClassLifeCycle

        public PlayerController(IMotor motor)
		{
			_motor = motor;
		}

        #endregion


        #region IExecute

        public void Execute()
		{
			if(!IsActive) return;

			_motor.Move();
		}

        #endregion
    }
}