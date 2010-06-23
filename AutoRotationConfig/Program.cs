using System;

using System.Collections.Generic;
using System.Windows.Forms;

namespace AutoRotationConfig
{
    static class Program
    {

        static RotationConfig config;
        internal static RotationConfig Config
        {
            get
            {
                return config;
            }
            set
            {
                config = value;
            }
        }


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            try
            {
                Config = RotationConfig.Create();
                System.Windows.Forms.Application.Run(new ControlPanel());
            }
            catch (NotSupportedException ex)
            {
                MessageBox.Show(ex.Message, "Auto-Rotate Config");
            }
        }


    }
}