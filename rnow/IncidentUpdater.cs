using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using api.rightnow.com;
using obj.api.rightnow.com;
using System.Windows.Forms;

////////////////////////////////////////////////////////////////////////////////
// RightNow Connect Sample Code
//
// RightNow Confidential
//
// Copyright 2006 - 2009 RightNow Technologies.  All Rights Reserved.
//
// Sample code for training only. This sample code is provided "as is" with no warranties 
// of any kind express or implied. Use of this sample code is pursuant to the applicable
// non-disclosure agreement and or end user agreement and or partner agreement between
// you and RightNow Technologies. You acknowledge RightNow Technologies is the exclusive
// owner of the object code, source code, results, findings, ideas and any works developed
// in using this sample code.
//
// File: IncidentUpdater.cs
//
// Comments: An example of updating an incident in the RightNow using RightNow Connect Data Integration .NET
//
// Notes: This example demonstrates updating an incident record inside of RightNow, however the same
//        principles apply to all primary objects such as Contacts or Organizations.
//  
// Pre-Conditions: 
//          1. You must have II_CONNECT_ENABLED turned on for your site
//          2. You must have an account profile for the login staff account which has RightNow Connect enabled.
//          3. You must update the SITE_URL property in this file.
//
////////////////////////////////////////////////////////////////////////////////
namespace Example2___Update
{
    public class IncidentUpdater
    {
        private const String SITE_URL = "https://ENTER_A_VALID_RIGHTNOW_URL";
        private const String CONNECT_USER = "rnow";
        private const String CONNECT_PASSWORD = "password123";

      

       
       

        static void Main(string[] args)
        {


            frmMain main = new frmMain();
            main.ShowDialog(); 
           

           
        }
    }
}
