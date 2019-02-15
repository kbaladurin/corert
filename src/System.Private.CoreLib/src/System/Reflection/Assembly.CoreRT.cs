// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Configuration.Assemblies;
using System.Runtime.Serialization;

using Internal.Reflection.Augments;
using Internal.Reflection.Core.NonPortable;

namespace System.Reflection
{
    public abstract partial class Assembly : ICustomAttributeProvider, ISerializable
    {
        public static Assembly GetEntryAssembly() => Internal.Runtime.CompilerHelpers.StartupCodeHelpers.GetEntryAssembly();

        [System.Runtime.CompilerServices.Intrinsic]
        public static Assembly GetExecutingAssembly() { throw NotImplemented.ByDesign; } //Implemented by toolchain. 

        public static Assembly GetCallingAssembly() { throw new PlatformNotSupportedException(); }

        public static Assembly Load(AssemblyName assemblyRef) => ReflectionAugments.ReflectionCoreCallbacks.Load(assemblyRef, throwOnFileNotFound: true);
        public static Assembly Load(byte[] rawAssembly, byte[] rawSymbolStore) => ReflectionAugments.ReflectionCoreCallbacks.Load(rawAssembly, rawSymbolStore);

        public static Assembly Load(string assemblyString)
        {
            if (assemblyString == null)
                throw new ArgumentNullException(nameof(assemblyString));

            AssemblyName name = new AssemblyName(assemblyString);
            return Load(name);
        }

        public bool IsRuntimeImplemented() => this is IRuntimeImplemented; // Not an api but needs to be public because of Reflection.Core/CoreLib divide.
        public static Assembly LoadFile(string path)
        {
            string name = "";
            if (path == "/usr/apps/com.samsung.tv.coba.source/shared/res/CompSource.dll")
            {
                name = "CompSource";
            }
            else if (path == "/usr/apps/com.samsung.tv.coba.search/shared/res/CompSearch.dll")
            {
                name = "CompSearch";
            }
            else if (path == "/usr/apps/com.samsung.tv.coba.apps/shared/res/CompApps.dll")
            {
                name = "CompApps";
            }
            else if (path == "/usr/apps/com.samsung.tv.coba.notification/shared/res/CompNotification.dll")
            {
                name = "CompNotification";
            }
            else if (path == "/usr/apps/com.samsung.tv.coba.livetv/shared/res/CompLiveTV.dll")
            {
                name = "CompLiveTV";
            }
            else if (path == "/usr/apps/com.samsung.tv.coba.advertise/shared/res/CompAdvertise.dll")
            {
                name = "CompAdvertise";
            }
            else if (path == "/usr/apps/com.samsung.tv.coba.internet/shared/res/CompInternet.dll")
            {
                name = "CompInternet";
            }
            else
            {
                throw new PlatformNotSupportedException(path); 
            }
            return Load(new AssemblyName(name));
        }
        public static Assembly LoadFrom(string assemblyFile) { return LoadFile(assemblyFile); }
        public static Assembly LoadFrom(string assemblyFile, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm) { throw new PlatformNotSupportedException(assemblyFile); }
    }
}
