﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lisa.Kiwi.Web.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Lisa.Kiwi.Web.Resources.ErrorMessages", typeof(ErrorMessages).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Vul een geldig e-mailadres in.
        /// </summary>
        public static string Email {
            get {
                return ResourceManager.GetString("Email", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Upload alleen geldige afbeeldingen.
        /// </summary>
        public static string FileExtension {
            get {
                return ResourceManager.GetString("FileExtension", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Upload alleen bestanden die samen niet groter zijn dan 50 MB.
        /// </summary>
        public static string FileSize {
            get {
                return ResourceManager.GetString("FileSize", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Onverwachte fout, probeer het opnieuw.
        /// </summary>
        public static string FileUnexpected {
            get {
                return ResourceManager.GetString("FileUnexpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wachtwoord of gebruikersnaam is onjuist.
        /// </summary>
        public static string IncorrectLogin {
            get {
                return ResourceManager.GetString("IncorrectLogin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Foutieve naam.
        /// </summary>
        public static string Name {
            get {
                return ResourceManager.GetString("Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Het wachtwoord moet minimaal 4 tekens lang zijn.
        /// </summary>
        public static string PasswordLength {
            get {
                return ResourceManager.GetString("PasswordLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Beide wachtwoorden moeten hetzelfde zijn..
        /// </summary>
        public static string PasswordRepeat {
            get {
                return ResourceManager.GetString("PasswordRepeat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Vul een geldig telefoonnummer in.
        /// </summary>
        public static string PhoneNumber {
            get {
                return ResourceManager.GetString("PhoneNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Vul een telefoonnummer of e-mailadres in.
        /// </summary>
        public static string PhoneOrEmail {
            get {
                return ResourceManager.GetString("PhoneOrEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dit is een verplicht veld.
        /// </summary>
        public static string Required {
            get {
                return ResourceManager.GetString("Required", resourceCulture);
            }
        }
    }
}
