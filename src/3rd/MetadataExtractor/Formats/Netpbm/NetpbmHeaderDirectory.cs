#region License
//
// Copyright 2002-2017 Drew Noakes
// Ported from Java to C# by Yakov Danilov for Imazen LLC in 2014
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
// More information about this project is available at:
//
//    https://github.com/drewnoakes/metadata-extractor-dotnet
//    https://drewnoakes.com/code/exif/
//
#endregion

using System.Collections.Generic;

namespace MetadataExtractor.Formats.Netpbm
{
    /// <author>Drew Noakes https://drewnoakes.com</author>
    public sealed class NetpbmHeaderDirectory : Directory
    {
        public const int TagFormatType = 1;
        public const int TagWidth = 2;
        public const int TagHeight = 3;
        public const int TagMaximumValue = 4;

        private static readonly Dictionary<int, string> _tagNameMap = new Dictionary<int, string>
        {
            { TagFormatType, "Format Type" },
            { TagWidth, "Width" },
            { TagHeight, "Height" },
            { TagMaximumValue, "Maximum Value" }
        };

        public NetpbmHeaderDirectory()
        {
            SetDescriptor(new NetpbmHeaderDescriptor(this));
        }

        public override string Name => "Netpbm";

        protected override bool TryGetTagName(int tagType, out string tagName)
        {
            return _tagNameMap.TryGetValue(tagType, out tagName);
        }
    }
}