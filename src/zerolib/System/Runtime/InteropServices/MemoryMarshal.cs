// bflat minimal runtime library
// Copyright (C) 2021-2022 Michal Strehovsky
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
    public static partial class MemoryMarshal
    {
        [Intrinsic]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T GetArrayDataReference<T>(T[] array) => ref Unsafe.As<byte, T>(ref Unsafe.As<RawArrayData>(array).Data);

       // I believe this is needed by the compiler for the C# inline array feature.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static System.Span<T> CreateSpan<T>(ref T reference, int length)
            => new System.Span<T>(Unsafe.AsPointer(ref reference), length);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static System.ReadOnlySpan<T> CreateReadOnlySpan<T>(ref T reference, int length)
            => new System.ReadOnlySpan<T>(Unsafe.AsPointer(ref reference), length);
    }
    // This is needed for the 'unmanaged' generic constraint of C#.
    public enum UnmanagedType { }
}
