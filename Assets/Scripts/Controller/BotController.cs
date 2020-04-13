using UnityEngine;
using System.Linq;
using System.Collections.Generic;


namespace FirstShooter
{
    public sealed class BotController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private readonly HashSet<Bot> _botList = new HashSet<Bot>();
        
        #endregion


        #region IInitialization

        public void Initialization()
        {
            var enemyTarget = Object.FindObjectOfType<CharacterController>().transform;
            var botList = Object.FindObjectsOfType<Bot>();
            
            for (int i = 0; i < botList.Length; i++)
            {
                var bot = botList[i];
                
                bot.PathIndex = i + 1;
                bot.Target = enemyTarget;
                AddBotToList(bot);
            }
        }

        #endregion


        #region IExecute

        public void Execute()
        {
            if (!IsActive) return;

            for (var i = 0; i < _botList.Count; i++)
            {
                var bot = _botList.ElementAt(i);
                bot.Execute();
            }
        }

        #endregion


        #region Methods

        public void AddBotToList(Bot bot)
        {
            if (!_botList.Contains(bot))
            {
                _botList.Add(bot);
                bot.OnDieChange += RemoveBotFromList;
            }
        }

        private void RemoveBotFromList(Bot bot)
        {
            if (!_botList.Contains(bot)) return;

            bot.OnDieChange -= RemoveBotFromList;
            _botList.Remove(bot);
        }

        #endregion
    }
}

