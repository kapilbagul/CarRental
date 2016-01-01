using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.Web.Helpers
{
    public static class States
    {
        public static List<State> GetStates()
        {
            List<State> states = new List<State>
            {
                new State {Name="California" },
                new State {Name="Florida" }

            };

          
            return states;

        }
    }

    public class State
    {
        public string Name { get; set; }
    }
}