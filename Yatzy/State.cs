using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy
{
    public enum StateType
    {
        Active,
        Waiting
    }
    public class State  
    {
        public State(StateType type)
        {
            StateType ActiveState = type;
        }

    }
}
