using UnityEngine;
using System.Linq;
using System.Collections.Generic;


namespace FirstShooter
{
    public sealed class BotController : BaseController, IExecute, IInitialization
    {
        #region Fields

        private readonly int _countBot = 3;
        private readonly HashSet<Bot> _botList = new HashSet<Bot>();
        
        #endregion


        #region IInitialization

        public void Initialization()
        {
            for (int index = 0; index < _countBot; index++)
            {
                var patrol = new Patrol(index + 1);
                var tempBot = Object.Instantiate(ServiceLocatorMonoBehaviour.GetService<ReferenceHolder>().Bot,
                                                 patrol.GetNextPointInPatrolPath(),
                                                 Quaternion.identity);
                tempBot.Agent.avoidancePriority = index;
                tempBot.PathIndex = index + 1;
                tempBot.Target = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform;
                AddBotToList(tempBot);
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

        private void AddBotToList(Bot bot)
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

