//
// MarkNSObbjects.cs
//
// Author:
//   Jb Evain (jbevain@novell.com)
//
// (C) 2009 Novell, Inc.
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Mono.Linker;
using Mono.Linker.Steps;

using Mono.Tuner;

using Mono.Cecil;

namespace MonoMac.Tuner {

	public class MarkNSObjects : BaseSubStep {

		const string ExportAttribute = "MonoMac.Foundation.ExportAttribute";
		const string MonoMacAssembly = "MonoMac";

		public override SubStepTargets Targets {
			get { return SubStepTargets.Type; }
		}

		public override void ProcessType (TypeDefinition type)
		{
			if (!type.IsNSObject () || !type.IsNativeObject ())
				return;

			if (!IsMonoMacType (type)) {
				Annotations.Mark (type);
				Annotations.SetPreserve (type, TypePreserve.All);
			} else
				PreserveMonoMacType (type);
		}

		void PreserveMonoMacType (TypeDefinition type)
		{
			PreserveIntPtrConstructor (type);
			PreserveExportedMethods (type);
		}

		void PreserveExportedMethods (TypeDefinition type)
		{
			if (!type.HasMethods)
				return;

			foreach (var method in type.GetMethods ()) {
				if (!IsExportedMethod (method))
					continue;

				if (!IsOverridenInUserCode (method))
					continue;

				PreserveMethod (type, method);
			}
		}

		bool IsOverridenInUserCode (MethodDefinition method)
		{
			if (!method.IsVirtual)
				return false;

			var overrides = Annotations.GetOverrides (method);
			if (overrides == null || overrides.Count == 0)
				return false;

			foreach (MethodDefinition @override in overrides)
				if (!IsMonoMacMethod (@override))
					return true;

			return false;
		}

		static bool IsExportedMethod (MethodDefinition method)
		{
			return HasExportAttribute (method);
		}

		static bool HasExportAttribute (ICustomAttributeProvider provider)
		{
			if (!provider.HasCustomAttributes)
				return false;

			foreach (CustomAttribute attribute in provider.CustomAttributes)
				if (attribute.AttributeType.FullName == ExportAttribute)
					return true;

			return false;
		}

		void PreserveIntPtrConstructor (TypeDefinition type)
		{
			if (!type.HasMethods)
				return;

			foreach (MethodDefinition constructor in type.GetConstructors ()) {
				if (!constructor.HasParameters)
					continue;

				if (constructor.Parameters.Count != 1 || constructor.Parameters [0].ParameterType.FullName != "System.IntPtr")
					continue;

				PreserveMethod (type, constructor);
			}
		}

		void PreserveMethod (TypeDefinition type, MethodDefinition method)
		{
			Annotations.AddPreservedMethod (type, method);
		}

		static bool IsMonoMacMethod (MethodDefinition method)
		{
			return IsMonoMacType (method.DeclaringType);
		}

		static bool IsMonoMacType (TypeDefinition type)
		{
			return type.Module.Assembly.Name.Name == MonoMacAssembly;
		}
	}
}
