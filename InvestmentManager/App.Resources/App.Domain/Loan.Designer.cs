﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App.Resources.App.Domain {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Loan {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        public Loan() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("App.Resources.App.Domain.Loan", typeof(Loan).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        public static string LoanName {
            get {
                return ResourceManager.GetString("LoanName", resourceCulture);
            }
        }
        
        public static string BorrowerName {
            get {
                return ResourceManager.GetString("BorrowerName", resourceCulture);
            }
        }
        
        public static string ContractNumber {
            get {
                return ResourceManager.GetString("ContractNumber", resourceCulture);
            }
        }
        
        public static string Collateral {
            get {
                return ResourceManager.GetString("Collateral", resourceCulture);
            }
        }
        
        public static string LoanDate {
            get {
                return ResourceManager.GetString("LoanDate", resourceCulture);
            }
        }
        
        public static string EndDate {
            get {
                return ResourceManager.GetString("EndDate", resourceCulture);
            }
        }
        
        public static string Amount {
            get {
                return ResourceManager.GetString("Amount", resourceCulture);
            }
        }
        
        public static string ScheduleType {
            get {
                return ResourceManager.GetString("ScheduleType", resourceCulture);
            }
        }
        
        public static string Interest {
            get {
                return ResourceManager.GetString("Interest", resourceCulture);
            }
        }
    }
}
