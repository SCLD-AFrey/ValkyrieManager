﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace ValkyrieData.Banking
{

    public partial class User : XPObject
    {
        string fUserName;
        public string UserName
        {
            get { return fUserName; }
            set { SetPropertyValue<string>(nameof(UserName), ref fUserName, value); }
        }
        string fPassword;
        [Size(SizeAttribute.Unlimited)]
        public string Password
        {
            get { return fPassword; }
            set { SetPropertyValue<string>(nameof(Password), ref fPassword, value); }
        }
        byte[] fPassSalt;
        [MemberDesignTimeVisibility(true)]
        public byte[] PassSalt
        {
            get { return fPassSalt; }
            set { SetPropertyValue<byte[]>(nameof(PassSalt), ref fPassSalt, value); }
        }
        bool fIsRoot;
        [ColumnDefaultValue(false)]
        public bool IsRoot
        {
            get { return fIsRoot; }
            set { SetPropertyValue<bool>(nameof(IsRoot), ref fIsRoot, value); }
        }
        [Association(@"UserHistoryReferencesUser")]
        public XPCollection<UserHistory> UserHistories { get { return GetCollection<UserHistory>(nameof(UserHistories)); } }
    }

}
