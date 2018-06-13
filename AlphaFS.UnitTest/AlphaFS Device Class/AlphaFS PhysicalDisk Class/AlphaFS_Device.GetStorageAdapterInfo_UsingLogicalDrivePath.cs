/*  Copyright (C) 2008-2018 Peter Palotas, Jeffrey Jangli, Alexandr Normuradov
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy 
 *  of this software and associated documentation files (the "Software"), to deal 
 *  in the Software without restriction, including without limitation the rights 
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 *  copies of the Software, and to permit persons to whom the Software is 
 *  furnished to do so, subject to the following conditions:
 *  
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 *  THE SOFTWARE. 
 */

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlphaFS.UnitTest
{
   public partial class AlphaFS_PhysicalDiskInfoTest
   {
      // Pattern: <class>_<function>_<scenario>_<expected result>


      [TestMethod]
      public void AlphaFS_Device_GetStorageAdapterInfo_UsingLogicalDrivePath_Success()
      {
         UnitTestConstants.PrintUnitTestHeader(false);
         
         var gotAdapter = false;
         var driveCount = 0;
         

         foreach (var driveInfo in System.IO.DriveInfo.GetDrives())
         {
            // Works with System.IO.DriveType.CDRom.

            // System.UnauthorizedAccessException: (5) Access is denied.
            if (driveInfo.DriveType == System.IO.DriveType.Network)
            {
               Console.WriteLine("#{0:000}\tSkipped Network drive: [{1}]\n", ++driveCount, driveInfo.Name);
               continue;
            }

            if (driveInfo.DriveType == System.IO.DriveType.NoRootDirectory)
            {
               Console.WriteLine("#{0:000}\tSkipped NoRootDirectory drive: [{1}]\n", ++driveCount, driveInfo.Name);
               continue;
            }

            Console.WriteLine("#{0:000}\tInput Logical Drive Path: [{1}]", ++driveCount, driveInfo.Name);


            var storageAdapterInfo = Alphaleonis.Win32.Device.Local.GetStorageAdapterInfo(driveInfo.Name);
            

            UnitTestConstants.Dump(storageAdapterInfo);
            
            Assert.IsNotNull(storageAdapterInfo);

            gotAdapter = true;
            
            Console.WriteLine();
         }


         Assert.IsTrue(gotAdapter);
      }
   }
}