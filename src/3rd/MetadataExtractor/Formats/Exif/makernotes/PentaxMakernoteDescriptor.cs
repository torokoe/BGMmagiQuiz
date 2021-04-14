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

using System.Diagnostics.CodeAnalysis;


namespace MetadataExtractor.Formats.Exif.Makernotes
{
    /// <summary>
    /// Provides human-readable string representations of tag values stored in a <see cref="PentaxMakernoteDirectory"/>.
    /// <para />
    /// Some information about this makernote taken from here:
    /// http://www.ozhiker.com/electronics/pjmt/jpeg_info/pentax_mn.html
    /// </summary>
    /// <author>Drew Noakes https://drewnoakes.com</author>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public sealed class PentaxMakernoteDescriptor : TagDescriptor<PentaxMakernoteDirectory>
    {
        public PentaxMakernoteDescriptor( PentaxMakernoteDirectory directory)
            : base(directory)
        {
        }

        public override string GetDescription(int tagType)
        {
            switch (tagType)
            {
                case PentaxMakernoteDirectory.TagCaptureMode:
                    return GetCaptureModeDescription();
                case PentaxMakernoteDirectory.TagQualityLevel:
                    return GetQualityLevelDescription();
                case PentaxMakernoteDirectory.TagFocusMode:
                    return GetFocusModeDescription();
                case PentaxMakernoteDirectory.TagFlashMode:
                    return GetFlashModeDescription();
                case PentaxMakernoteDirectory.TagWhiteBalance:
                    return GetWhiteBalanceDescription();
                case PentaxMakernoteDirectory.TagDigitalZoom:
                    return GetDigitalZoomDescription();
                case PentaxMakernoteDirectory.TagSharpness:
                    return GetSharpnessDescription();
                case PentaxMakernoteDirectory.TagContrast:
                    return GetContrastDescription();
                case PentaxMakernoteDirectory.TagSaturation:
                    return GetSaturationDescription();
                case PentaxMakernoteDirectory.TagIsoSpeed:
                    return GetIsoSpeedDescription();
                case PentaxMakernoteDirectory.TagColour:
                    return GetColourDescription();
                default:
                    return base.GetDescription(tagType);
            }
        }

        
        public string GetColourDescription()
        {
            return GetIndexedDescription(PentaxMakernoteDirectory.TagColour,
                1,
                "Normal", "Black & White", "Sepia");
        }

        
        public string GetIsoSpeedDescription()
        {
            if (!Directory.TryGetInt32(PentaxMakernoteDirectory.TagIsoSpeed, out int value))
                return null;

            switch (value)
            {
                case 10:
                    // TODO there must be other values which aren't catered for here
                    return "ISO 100";
                case 16:
                    return "ISO 200";
                case 100:
                    return "ISO 100";
                case 200:
                    return "ISO 200";
                default:
                    return "Unknown (" + value + ")";
            }
        }

        
        public string GetSaturationDescription()
        {
            return GetIndexedDescription(PentaxMakernoteDirectory.TagSaturation,
                "Normal", "Low", "High");
        }

        
        public string GetContrastDescription()
        {
            return GetIndexedDescription(PentaxMakernoteDirectory.TagContrast,
                "Normal", "Low", "High");
        }

        
        public string GetSharpnessDescription()
        {
            return GetIndexedDescription(PentaxMakernoteDirectory.TagSharpness,
                "Normal", "Soft", "Hard");
        }

        
        public string GetDigitalZoomDescription()
        {
            if (!Directory.TryGetSingle(PentaxMakernoteDirectory.TagDigitalZoom, out float value))
                return null;
            return value == 0 ? "Off" : value.ToString("0.0###########");
        }

        
        public string GetWhiteBalanceDescription()
        {
            return GetIndexedDescription(PentaxMakernoteDirectory.TagWhiteBalance,
                "Auto", "Daylight", "Shade", "Tungsten", "Fluorescent", "Manual");
        }

        
        public string GetFlashModeDescription()
        {
            return GetIndexedDescription(PentaxMakernoteDirectory.TagFlashMode,
                1,
                "Auto", "Flash On", null, "Flash Off", null, "Red-eye Reduction");
        }

        
        public string GetFocusModeDescription()
        {
            return GetIndexedDescription(PentaxMakernoteDirectory.TagFocusMode,
                2,
                "Custom", "Auto");
        }

        
        public string GetQualityLevelDescription()
        {
            return GetIndexedDescription(PentaxMakernoteDirectory.TagQualityLevel,
                "Good", "Better", "Best");
        }

        
        public string GetCaptureModeDescription()
        {
            return GetIndexedDescription(PentaxMakernoteDirectory.TagCaptureMode,
                "Auto", "Night-scene", "Manual", null, "Multiple");
        }
    }
}
