using System;
using System.Collections.Generic;
using System.Text;

namespace Supernatural
{
    public class ActionDeck
    {
        private List<Action> _Actions = new List<Action>();
        public List<Action> Actions { get { return _Actions; } set { value = _Actions; } }
        public ActionDeck()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 2; j++)
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
