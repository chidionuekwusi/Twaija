﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwaijaComposite.Modules.Common.AttributeTypes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false,AllowMultiple=false)]
    public class ScreenNamePropertyLocatorAttribute:Attribute,IPropertyLocator
    {
        public string TargetPropertyName
        {
            get;
            set;
        }
    }
}
