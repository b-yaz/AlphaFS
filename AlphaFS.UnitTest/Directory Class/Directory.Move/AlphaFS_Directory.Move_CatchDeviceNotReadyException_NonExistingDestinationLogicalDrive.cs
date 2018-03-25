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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AlphaFS.UnitTest
{
   public partial class Directory_MoveTest
   {
      // Pattern: <class>_<function>_<scenario>_<expected result>


      [TestMethod]
      public void AlphaFS_Directory_Move_CatchDeviceNotReadyException_NonExistingDestinationLogicalDrive_LocalAndNetwork_Success()
      {
         Directory_Move_CatchDeviceNotReadyException_NonExistingDestinationLogicalDrive(false);
         Directory_Move_CatchDeviceNotReadyException_NonExistingDestinationLogicalDrive(true);
      }


      private void Directory_Move_CatchDeviceNotReadyException_NonExistingDestinationLogicalDrive(bool isNetwork)
      {
         UnitTestConstants.PrintUnitTestHeader(isNetwork);
         Console.WriteLine();


         var gotException = false;


         var nonExistingDriveLetter = Alphaleonis.Win32.Filesystem.DriveInfo.GetFreeDriveLetter();
         
         var srcFolder = UnitTestConstants.SysDrive + @"\NonExisting Source Folder";
         var dstFolder = nonExistingDriveLetter + @":\NonExisting Destination Folder";

         if (isNetwork)
         {
            srcFolder = Alphaleonis.Win32.Filesystem.Path.LocalToUnc(srcFolder);
            dstFolder = Alphaleonis.Win32.Filesystem.Path.LocalToUnc(dstFolder);
         }

         Console.WriteLine("Src Directory Path: [{0}]", srcFolder);
         Console.WriteLine("Dst Directory Path: [{0}]", dstFolder);


         try
         {
            Alphaleonis.Win32.Filesystem.Directory.Move(srcFolder, dstFolder, Alphaleonis.Win32.Filesystem.MoveOptions.CopyAllowed);
         }
         catch (Exception ex)
         {
            var exType = ex.GetType();

            gotException = exType == typeof(Alphaleonis.Win32.Filesystem.DeviceNotReadyException);

            Console.WriteLine("\n\tCaught {0} Exception: [{1}] {2}", gotException ? "EXPECTED" : "UNEXPECTED", exType.Name, ex.Message);
         }


         Assert.IsTrue(gotException, "The exception is not caught, but is expected to.");

         Assert.IsFalse(System.IO.Directory.Exists(dstFolder), "The directory exists, but is expected not to.");
         

         Console.WriteLine();
      }
   }
}
