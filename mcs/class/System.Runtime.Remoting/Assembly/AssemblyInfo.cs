//
// AssemblyInfo.cs
//
// Author:
//   Andreas Nahr (ClassDevelopment@A-SoftTech.com)
//
// (C) 2003 Ximian, Inc.  http://www.ximian.com
//

//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using System.Reflection;
using System.Resources;
using System.Security;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about the System.Runtime.Remoting assembly

#if NET_2_0
	[assembly: AssemblyTitle ("System.Runtime.Remoting.dll")]
	[assembly: AssemblyDescription ("System.Runtime.Remoting.dll")]
	[assembly: AssemblyDefaultAlias ("System.Runtime.Remoting.dll")]

	[assembly: AssemblyCompany (Consts.MonoCompany)]
	[assembly: AssemblyProduct (Consts.MonoProduct)]
	[assembly: AssemblyCopyright (Consts.MonoCopyright)]

	[assembly: AssemblyInformationalVersion (Consts.FxFileVersion)]
#endif

[assembly: AssemblyVersion (Consts.FxVersion)]
[assembly: SatelliteContractVersion (Consts.FxVersion)]

[assembly: NeutralResourcesLanguage ("en-US")]
[assembly: ComCompatibleVersion (1, 0, 3300, 0)]

#if !TARGET_JVM
	[assembly: AssemblyDelaySign (true)]
	[assembly: AssemblyKeyFile("../ecma.pub")]
#endif

#if NET_2_0
	[assembly: AssemblyFileVersion (Consts.FxFileVersion)]
	[assembly: CLSCompliant (false)]
	[assembly: ComVisible (false)]
	[assembly: CompilationRelaxations (CompilationRelaxations.NoStringInterning)]
	[assembly: Debuggable (DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
	[assembly: RuntimeCompatibility (WrapNonExceptionThrows = true)]
	[assembly: TypeLibVersion (2, 0)]
#elif NET_1_1
	[assembly: TypeLibVersion (1, 10)]
#elif NET_1_0
	[assembly: TypeLibVersion (1, 10)]
#endif
