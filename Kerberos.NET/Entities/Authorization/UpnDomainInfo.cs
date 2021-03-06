﻿using Kerberos.NET.Entities.Authorization;
using System.Text;

#pragma warning disable S2344 // Enumeration type names should not have "Flags" or "Enum" suffixes

namespace Kerberos.NET.Entities
{
    public enum UpnDomainFlags
    {
        U = 1
    }

    public class UpnDomainInfo
    {
        public UpnDomainInfo(byte[] data)
        {
            var pacStream = new NdrBinaryReader(data);

            UpnLength = pacStream.ReadShort();
            UpnOffset = pacStream.ReadShort();

            DnsDomainNameLength = pacStream.ReadShort();
            DnsDomainNameOffset = pacStream.ReadShort();

            Flags = (UpnDomainFlags)pacStream.ReadInt();

            pacStream.Align(8);

            Upn = Encoding.Unicode.GetString(pacStream.Read(UpnLength));

            pacStream.Align(8);

            Domain = Encoding.Unicode.GetString(pacStream.Read(DnsDomainNameLength));
        }

        public string Upn { get; private set; }

        public string Domain { get; private set; }

        [KerberosIgnore]
        public short UpnLength { get; private set; }

        [KerberosIgnore]
        public short UpnOffset { get; private set; }

        [KerberosIgnore]
        public short DnsDomainNameLength { get; private set; }

        [KerberosIgnore]
        public short DnsDomainNameOffset { get; private set; }

        public UpnDomainFlags Flags { get; private set; }
    }
}