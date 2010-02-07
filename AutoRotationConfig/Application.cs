using System;

using System.Collections.Generic;
using System.Text;
using Tenor.Mobile.Diagnostics;

namespace AutoRotationConfig
{
    public class RunningApp
    {
        public string Title { get; set; }
        public Process Process { get; set; }
    }

    [Serializable]
    public class AppDetails
    {
        public AppDetails()
        {
            PossibleLocations = new List<string>();
        }
        public string Title { get; set; }
        public List<string> PossibleLocations { get; private set; }
    }
}
