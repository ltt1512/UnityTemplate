// Copyright (c) 2015 - 2023 Doozy Entertainment. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

//.........................
//.....Generated Class.....
//.........................
//.......Do not edit.......
//.........................

using System.Collections.Generic;
// ReSharper disable All
namespace Doozy.Runtime.UIManager.Components
{
    public partial class UITag
    {
        public static IEnumerable<UITag> GetTags(UITagId.UI id) => GetTags(nameof(UITagId.UI), id.ToString());
    }
}

namespace Doozy.Runtime.UIManager
{
    public partial class UITagId
    {
        public enum UI
        {
            Popup
        }    
    }
}
