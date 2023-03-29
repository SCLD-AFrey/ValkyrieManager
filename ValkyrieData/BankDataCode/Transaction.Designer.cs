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

    public partial class Transaction : XPObject
    {
        decimal fAmount;
        public decimal Amount
        {
            get { return fAmount; }
            set { SetPropertyValue<decimal>(nameof(Amount), ref fAmount, value); }
        }
        string fTransactionType;
        public string TransactionType
        {
            get { return fTransactionType; }
            set { SetPropertyValue<string>(nameof(TransactionType), ref fTransactionType, value); }
        }
        TransactionCategory fCategory;
        [Association(@"TransactionReferencesTransactionCategory")]
        public TransactionCategory Category
        {
            get { return fCategory; }
            set { SetPropertyValue<TransactionCategory>(nameof(Category), ref fCategory, value); }
        }
    }

}