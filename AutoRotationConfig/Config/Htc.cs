using System;

using System.Collections.Generic;
using System.Text;

namespace AutoRotationConfig
{
    class Htc : RotationConfig
    {
        public override Device Device
        {
            get { return Device.Htc; }
        }

        protected override Microsoft.Win32.RegistryKey GetKey(bool write)
        {
            throw new NotImplementedException();
        }

        public override AppDetails[] Applications
        {
            get { throw new NotImplementedException(); }
        }

        public override bool ReloadRotationSupport()
        {
            throw new NotImplementedException();
        }

        public override void AddApplication(RunningApp app)
        {
            throw new NotImplementedException();
        }

        public override void RemoveApplication(int index)
        {
            throw new NotImplementedException();
        }
    }
}
