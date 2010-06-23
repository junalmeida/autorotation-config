using System;

using System.Collections.Generic;
using System.Text;
using Tenor.Mobile.Diagnostics;

namespace AutoRotationConfig
{
    class RunningApp
    {
        public string Title { get; set; }
        public string ClassName { get; set; }
        public Process Process { get; set; }
    }

    [Serializable]
    class AppDetails
    {
        public AppDetails()
        {
            PossibleLocations = new List<string>();
        }
        public string Title { get; set; }
        public string ClassName { get; set; }
        public List<string> PossibleLocations { get; private set; }
    }
}
