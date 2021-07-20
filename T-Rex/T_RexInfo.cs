using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace T_Rex
{
    public class T_RexInfo : GH_AssemblyInfo
  {
    public override string Name
    {
        get
        {
            return "T-Rex";
        }
    }
    public override Bitmap Icon
    {
        get
        {
            return null;
        }
    }
    public override string Description
    {
        get
        {
            return "Reinforcement plugin for Grasshopper";
        }
    }
    public override Guid Id
    {
        get
        {
            return new Guid("9f8aa0b7-3d2b-475f-ac2a-a37039a0180a");
        }
    }

    public override string AuthorName
    {
        get
        {
            return "code-structures Wojciech Radaczynski";
        }
    }
    public override string AuthorContact
    {
        get
        {
            return "w.radaczynski@gmail.com";
        }
    }
    public override string Version
    {
        get
        {
            return "0.2.0.0";
        }
    }
}
}
