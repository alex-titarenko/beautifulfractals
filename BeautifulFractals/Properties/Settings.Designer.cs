﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18051
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TAlex.BeautifulFractals.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(TAlex.Common.Configuration.XmlSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Fractals.xml")]
        public string FractalsCollectionPath {
            get {
                return ((string)(this["FractalsCollectionPath"]));
            }
            set {
                this["FractalsCollectionPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(TAlex.Common.Configuration.XmlSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool RandomOrder {
            get {
                return ((bool)(this["RandomOrder"]));
            }
            set {
                this["RandomOrder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(TAlex.Common.Configuration.XmlSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ExitOnMouseMove {
            get {
                return ((bool)(this["ExitOnMouseMove"]));
            }
            set {
                this["ExitOnMouseMove"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(TAlex.Common.Configuration.XmlSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("00:00:03")]
        public global::System.TimeSpan Delay {
            get {
                return ((global::System.TimeSpan)(this["Delay"]));
            }
            set {
                this["Delay"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(TAlex.Common.Configuration.XmlSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ShowFractalCaptions {
            get {
                return ((bool)(this["ShowFractalCaptions"]));
            }
            set {
                this["ShowFractalCaptions"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(TAlex.Common.Configuration.XmlSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#ff1e90ff")]
        public global::TAlex.BeautifulFractals.Rendering.Color PrimaryBackColor {
            get {
                return ((global::TAlex.BeautifulFractals.Rendering.Color)(this["PrimaryBackColor"]));
            }
            set {
                this["PrimaryBackColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(TAlex.Common.Configuration.XmlSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#ff000000")]
        public global::TAlex.BeautifulFractals.Rendering.Color SecondaryBackColor {
            get {
                return ((global::TAlex.BeautifulFractals.Rendering.Color)(this["SecondaryBackColor"]));
            }
            set {
                this["SecondaryBackColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(TAlex.Common.Configuration.XmlSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Arial")]
        public string CaptionFontFamily {
            get {
                return ((string)(this["CaptionFontFamily"]));
            }
            set {
                this["CaptionFontFamily"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(TAlex.Common.Configuration.XmlSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("12")]
        public double CaptionFontSize {
            get {
                return ((double)(this["CaptionFontSize"]));
            }
            set {
                this["CaptionFontSize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(TAlex.Common.Configuration.XmlSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#ff000000")]
        public global::TAlex.BeautifulFractals.Rendering.Color CaptionFontColor {
            get {
                return ((global::TAlex.BeautifulFractals.Rendering.Color)(this["CaptionFontColor"]));
            }
            set {
                this["CaptionFontColor"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Configuration.SettingsProviderAttribute(typeof(TAlex.Common.Configuration.XmlSettingsProvider))]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Vertical")]
        public global::TAlex.BeautifulFractals.ViewModels.BackgroundGradientType BackGradientType {
            get {
                return ((global::TAlex.BeautifulFractals.ViewModels.BackgroundGradientType)(this["BackGradientType"]));
            }
            set {
                this["BackGradientType"] = value;
            }
        }
    }
}
