using System;

using System.Collections.Generic;
using System.Windows.Forms;

namespace AutoRotationConfig
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            try
            {
                Application.Run(new ControlPanel());
            }
            catch (NotSupportedException ex)
            {
                MessageBox.Show(ex.Message, "Auto-Rotate Config");
                return;
            }
        }
    }
}