﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CreateFont.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CreateFont.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///#include &quot;font.h&quot;
        ///
        ///static uint32_t UTF8_Decode(const char *str, uint8_t *utf8Count) {
        ///    if(*str &amp; 0x80) {
        ///        uint8_t byteCount;
        ///        for(byteCount = 1; byteCount &lt; 6; byteCount++) {
        ///            if(!(*str &amp; (0x80 &gt;&gt; byteCount)))
        ///                break;
        ///        }
        ///        *utf8Count = byteCount;
        ///
        ///        uint32_t code = *str &amp; (0xFF &gt;&gt; (byteCount + 1));
        ///        while(--byteCount) {
        ///            str++;
        ///            code &lt;&lt;= 6;
        ///            code |= *str &amp; 0x3F;
        ///        }
        ///        return code;
        ///    }
        ///    *ut [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string font_c {
            get {
                return ResourceManager.GetString("font_c", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to #ifndef __FONT_H
        ///#define __FONT_H
        ///
        ///#include &quot;stm32f1xx_hal.h&quot;
        ///
        ///typedef struct {
        ///    uint32_t unicode;
        ///    uint8_t width;
        ///    uint8_t height;
        ///    int8_t offset;
        ///    uint8_t data[];
        ///} Font_TypeDef;
        ///
        ///const Font_TypeDef *FONT_GetFont(const uint8_t *font, const char *str, uint8_t *utf8Count);
        ///
        ///#endif // __FONT_H
        ///.
        /// </summary>
        internal static string font_h {
            get {
                return ResourceManager.GetString("font_h", resourceCulture);
            }
        }
    }
}
