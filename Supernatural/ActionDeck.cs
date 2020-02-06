using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public class ActionDeck
    {
        /// <summary>
        /// Action Deck makes up extra actions that a player can do during their turn
        /// but also is the action economy of that player
        /// </summary>
        private List<Action> _Actions = new List<Action>();
        public List<Action> Actions { get { return _Actions; } set { value = _Actions; } }
        public ActionDeck()
        {
            var actions = Enum.GetValues(typeof(Action.Type));
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < actions.Length; j++)
                {
                    Action action = new Action();
                    action.Name = (Action.Type)j;
                    Actions.Add(action);
                }
            for (int i = 0; i < 8; i++)
            {
                Action action = new Action();
                action.Name = (Action.Type)4;
                Actions.Add(action);
            }
        }
        
    }
}
