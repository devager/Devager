namespace Devager
{
    using System;
    using System.Management;
    using System.Windows.Forms;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Sistem
    {
        public static string MetodBilgisi(MySistem tableName, string methodName)
        {
            var mos = new ManagementObjectSearcher("Select * from Win32_" + tableName);
            foreach (var mo in mos.Get())
            {
                try
                {
                    return mo[methodName].ToString();
                }
                catch
                {
                    return "";
                }
            }
            return "";
        }

        public static List<string> GetSistemBilgileri(MySistem ms)
        {
            var s = new List<string>();
            try
            {
                var mc = new ManagementClass("Win32_" + ms);
                foreach (var pt in from ManagementBaseObject item in mc.GetInstances() select item.Properties)
                {
                    s.AddRange(from PropertyData item2 in pt where item2.Value != null select item2.Name + " - " + item2.Value.ToString());
                }
                return s;
            }
            catch
            {
                return s;
            }
        }

        public static string EkranCozunurlugu()
        {
            try
            {
                return Screen.PrimaryScreen.Bounds.Width + " x " +
                       Screen.PrimaryScreen.Bounds.Height + " - " +
                       Screen.PrimaryScreen.BitsPerPixel;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }

    public enum MySistem
    {
        BIOS,
        BootConfiguration,
        CDROMDrive,
        ComputerSystem,
        ComputerSystemProduct,
        Configuration,
        DesktopMonitor,
        DiskDrive,
        DiskPartition,
        Environment,
        Group,
        IDEController,
        LogicalDisk,
        NetworkAdaptor,
        NTDomain,
        NTEventLogFile,
        OperatingSystem,
        PhysicalMemory,
        PhysicalMemoryArray,
        Printer,
        Process,
        QuickFixEngineering,
        Registry,
        Service,
        Share,
        StartupCommand,
        SystemAccount,
        SystemDriver,
        SystemSlot,
        TimeBone,
        UserAccount,
        WMISetting
    }
}
