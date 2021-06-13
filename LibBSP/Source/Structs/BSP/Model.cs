#if UNITY_3_4 || UNITY_3_5 || UNITY_4_0 || UNITY_4_0_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6 || UNITY_5 || UNITY_5_3_OR_NEWER
#define UNITY
#endif

using System;
using System.Collections.Generic;
using System.Reflection;

namespace LibBSP {
#if UNITY
	using Vector3 = UnityEngine.Vector3;
#elif GODOT
	using Vector3 = Godot.Vector3;
#else
	using Vector3 = System.Numerics.Vector3;
#endif

	/// <summary>
	/// A class containing all data needed for the models lump in any given BSP.
	/// </summary>
	public struct Model : ILumpObject {

		/// <summary>
		/// The <see cref="ILump"/> this <see cref="ILumpObject"/> came from.
		/// </summary>
		public ILump Parent { get; private set; }

		/// <summary>
		/// Array of <c>byte</c>s used as the data source for this <see cref="ILumpObject"/>.
		/// </summary>
		public byte[] Data { get; private set; }

		/// <summary>
		/// The <see cref="LibBSP.MapType"/> to use to interpret <see cref="Data"/>.
		/// </summary>
		public MapType MapType {
			get {
				if (Parent == null || Parent.Bsp == null) {
					return MapType.Undefined;
				}
				return Parent.Bsp.version;
			}
		}

		/// <summary>
		/// The version number of the <see cref="ILump"/> this <see cref="ILumpObject"/> came from.
		/// </summary>
		public int LumpVersion {
			get {
				if (Parent == null) {
					return 0;
				}
				return Parent.LumpInfo.version;
			}
		}

		/// <summary>
		/// Gets the head <see cref="Node"/> referenced by this <see cref="Model"/>.
		/// </summary>
		public Node HeadNode {
			get {
				return Parent.Bsp.nodes[HeadNodeIndex];
			}
		}

		/// <summary>
		/// Gets or sets the index of the head <see cref="Node"/> used by this <see cref="Model"/>.
		/// </summary>
		[Index("nodes")] public int HeadNodeIndex {
			get {
				switch (MapType) {
					case MapType.Nightfire: {
						return BitConverter.ToInt32(Data, 24);
					}
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake:
					case MapType.Quake2:
					case MapType.Daikatana:
					case MapType.SiN:
					case MapType.SoF:
					case MapType.Source17:
					case MapType.Source18:
					case MapType.Source19:
					case MapType.Source20:
					case MapType.Source21:
					case MapType.Source22:
					case MapType.Source23:
					case MapType.Source27:
					case MapType.L4D2:
					case MapType.TacticalInterventionEncrypted:
					case MapType.Vindictus: {
						return BitConverter.ToInt32(Data, 36);
					}
					case MapType.DMoMaM: {
						return BitConverter.ToInt32(Data, 40);
					}
					default: {
						return -1;
					}
				}
			}
			set {
				byte[] bytes = BitConverter.GetBytes(value);
				switch (MapType) {
					case MapType.Nightfire: {
						bytes.CopyTo(Data, 24);
						break;
					}
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake:
					case MapType.Quake2:
					case MapType.Daikatana:
					case MapType.SiN:
					case MapType.SoF:
					case MapType.Source17:
					case MapType.Source18:
					case MapType.Source19:
					case MapType.Source20:
					case MapType.Source21:
					case MapType.Source22:
					case MapType.Source23:
					case MapType.Source27:
					case MapType.L4D2:
					case MapType.TacticalInterventionEncrypted:
					case MapType.Vindictus: {
						bytes.CopyTo(Data, 36);
						break;
					}
					case MapType.DMoMaM: {
						bytes.CopyTo(Data, 40);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the index of the first head ClipNode used by this <see cref="Model"/>.
		/// </summary>
		public int HeadClipNode1Index {
			get {
				switch (MapType) {
					case MapType.Nightfire: {
						return BitConverter.ToInt32(Data, 28);
					}
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake: {
						return BitConverter.ToInt32(Data, 40);
					}
					default: {
						return -1;
					}
				}
			}
			set {
				byte[] bytes = BitConverter.GetBytes(value);
				switch (MapType) {
					case MapType.Nightfire: {
						bytes.CopyTo(Data, 28);
						break;
					}
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake: {
						bytes.CopyTo(Data, 40);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the index of the second head ClipNode used by this <see cref="Model"/>.
		/// </summary>
		public int HeadClipNode2Index {
			get {
				switch (MapType) {
					case MapType.Nightfire: {
						return BitConverter.ToInt32(Data, 32);
					}
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake: {
						return BitConverter.ToInt32(Data, 44);
					}
					default: {
						return -1;
					}
				}
			}
			set {
				byte[] bytes = BitConverter.GetBytes(value);
				switch (MapType) {
					case MapType.Nightfire: {
						bytes.CopyTo(Data, 32);
						break;
					}
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake: {
						bytes.CopyTo(Data, 44);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the index of the third head ClipNode used by this <see cref="Model"/>.
		/// </summary>
		public int HeadClipNode3Index {
			get {
				switch (MapType) {
					case MapType.Nightfire: {
						return BitConverter.ToInt32(Data, 36);
					}
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake: {
						return BitConverter.ToInt32(Data, 48);
					}
					default: {
						return -1;
					}
				}
			}
			set {
				byte[] bytes = BitConverter.GetBytes(value);
				switch (MapType) {
					case MapType.Nightfire: {
						bytes.CopyTo(Data, 36);
						break;
					}
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake: {
						bytes.CopyTo(Data, 48);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Enumerates the <see cref="Leaf"/>s used by this <see cref="Model"/>.
		/// </summary>
		public IEnumerable<Leaf> Leaves {
			get {
				for (int i = 0; i < NumLeaves; ++i) {
					yield return Parent.Bsp.leaves[FirstLeafIndex + i];
				}
			}
		}

		/// <summary>
		/// Gets or sets the index of the first <see cref="Leaf"/> used by this <see cref="Model"/>.
		/// </summary>
		[Index("leaves")] public int FirstLeafIndex {
			get {
				switch (MapType) {
					case MapType.Nightfire: {
						return BitConverter.ToInt32(Data, 40);
					}
					default: {
						return -1;
					}
				}
			}
			set {
				byte[] bytes = BitConverter.GetBytes(value);
				switch (MapType) {
					case MapType.Nightfire: {
						bytes.CopyTo(Data, 40);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the count of <see cref="Leaf"/> objects used by this <see cref="Model"/>.
		/// </summary>
		[Count("leaves")] public int NumLeaves {
			get {
				switch (MapType) {
					case MapType.Nightfire: {
						return BitConverter.ToInt32(Data, 44);
					}
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake: {
						return BitConverter.ToInt32(Data, 52);
					}
					default: {
						return -1;
					}
				}
			}
			set {
				byte[] bytes = BitConverter.GetBytes(value);
				switch (MapType) {
					case MapType.Nightfire: {
						bytes.CopyTo(Data, 44);
						break;
					}
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake: {
						bytes.CopyTo(Data, 52);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Enumerates the <see cref="Brush"/>es used by this <see cref="Model"/>.
		/// </summary>
		public IEnumerable<Brush> Brushes {
			get {
				for (int i = 0; i < NumBrushes; ++i) {
					yield return Parent.Bsp.brushes[FirstBrushIndex + i];
				}
			}
		}

		/// <summary>
		/// Gets or sets the index of the first <see cref="Brush"/> used by this <see cref="Model"/>.
		/// </summary>
		[Index("brushes")] public int FirstBrushIndex {
			get {
				switch (MapType) {
					case MapType.STEF2:
					case MapType.STEF2Demo:
					case MapType.Quake3:
					case MapType.MOHAA:
					case MapType.Raven:
					case MapType.FAKK: {
						return BitConverter.ToInt32(Data, 32);
					}
					case MapType.CoD:
					case MapType.CoD2:
					case MapType.CoD4: {
						return BitConverter.ToInt32(Data, 40);
					}
					default: {
						return -1;
					}
				}
			}
			set {
				byte[] bytes = BitConverter.GetBytes(value);
				switch (MapType) {
					case MapType.STEF2:
					case MapType.STEF2Demo:
					case MapType.Quake3:
					case MapType.MOHAA:
					case MapType.Raven:
					case MapType.FAKK: {
						bytes.CopyTo(Data, 32);
						break;
					}
					case MapType.CoD:
					case MapType.CoD2:
					case MapType.CoD4: {
						bytes.CopyTo(Data, 40);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the count of <see cref="Brush"/> objects referenced by this <see cref="Leaf"/>.
		/// </summary>
		[Count("brushes")] public int NumBrushes {
			get {
				switch (MapType) {
					case MapType.STEF2:
					case MapType.STEF2Demo:
					case MapType.Quake3:
					case MapType.MOHAA:
					case MapType.Raven:
					case MapType.FAKK: {
						return BitConverter.ToInt32(Data, 36);
					}
					case MapType.CoD:
					case MapType.CoD2:
					case MapType.CoD4: {
						return BitConverter.ToInt32(Data, 44);
					}
					default: {
						return -1;
					}
				}
			}
			set {
				byte[] bytes = BitConverter.GetBytes(value);
				switch (MapType) {
					case MapType.STEF2:
					case MapType.STEF2Demo:
					case MapType.Quake3:
					case MapType.MOHAA:
					case MapType.Raven:
					case MapType.FAKK: {
						bytes.CopyTo(Data, 36);
						break;
					}
					case MapType.CoD:
					case MapType.CoD2:
					case MapType.CoD4: {
						bytes.CopyTo(Data, 44);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Enumerates the <see cref="Face"/>s used by this <see cref="Model"/>.
		/// </summary>
		public IEnumerable<Face> Faces {
			get {
				for (int i = 0; i < NumFaces; ++i) {
					yield return Parent.Bsp.faces[FirstFaceIndex + i];
				}
			}
		}

		/// <summary>
		/// Gets or sets the index of the first <see cref="Face"/> used by this <see cref="Model"/>.
		/// </summary>
		[Index("faces")] public int FirstFaceIndex {
			get {
				switch (MapType) {
					case MapType.CoD4: {
						return BitConverter.ToInt16(Data, 24);
					}
					case MapType.CoD:
					case MapType.CoD2:
					case MapType.STEF2:
					case MapType.STEF2Demo:
					case MapType.Quake3:
					case MapType.MOHAA:
					case MapType.Raven:
					case MapType.FAKK: {
						return BitConverter.ToInt32(Data, 24);
					}
					case MapType.Quake2:
					case MapType.Daikatana:
					case MapType.SiN:
					case MapType.SoF:
					case MapType.Source17:
					case MapType.Source18:
					case MapType.Source19:
					case MapType.Source20:
					case MapType.Source21:
					case MapType.Source22:
					case MapType.Source23:
					case MapType.Source27:
					case MapType.L4D2:
					case MapType.TacticalInterventionEncrypted:
					case MapType.Vindictus: {
						return BitConverter.ToInt32(Data, 40);
					}
					case MapType.DMoMaM: {
						return BitConverter.ToInt32(Data, 44);
					}
					case MapType.Nightfire: {
						return BitConverter.ToInt32(Data, 48);
					}
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake: {
						return BitConverter.ToInt32(Data, 56);
					}
					default: {
						return -1;
					}
				}
			}
			set {
				byte[] bytes = BitConverter.GetBytes(value);
				switch (MapType) {
					case MapType.CoD4: {
						Data[24] = bytes[0];
						Data[25] = bytes[1];
						break;
					}
					case MapType.CoD:
					case MapType.CoD2:
					case MapType.STEF2:
					case MapType.STEF2Demo:
					case MapType.Quake3:
					case MapType.MOHAA:
					case MapType.Raven:
					case MapType.FAKK: {
						bytes.CopyTo(Data, 24);
						break;
					}
					case MapType.Quake2:
					case MapType.Daikatana:
					case MapType.SiN:
					case MapType.SoF:
					case MapType.Source17:
					case MapType.Source18:
					case MapType.Source19:
					case MapType.Source20:
					case MapType.Source21:
					case MapType.Source22:
					case MapType.Source23:
					case MapType.Source27:
					case MapType.L4D2:
					case MapType.TacticalInterventionEncrypted:
					case MapType.Vindictus: {
						bytes.CopyTo(Data, 40);
						break;
					}
					case MapType.DMoMaM: {
						bytes.CopyTo(Data, 44);
						break;
					}
					case MapType.Nightfire: {
						bytes.CopyTo(Data, 48);
						break;
					}
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake: {
						bytes.CopyTo(Data, 56);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the count of <see cref="Face"/> objects referenced by this <see cref="Leaf"/>.
		/// </summary>
		[Count("faces")] public int NumFaces {
			get {
				switch (MapType) {
					case MapType.CoD4: {
						return BitConverter.ToInt16(Data, 28);
					}
					case MapType.CoD:
					case MapType.CoD2:
					case MapType.STEF2:
					case MapType.STEF2Demo:
					case MapType.Quake3:
					case MapType.MOHAA:
					case MapType.Raven:
					case MapType.FAKK: {
						return BitConverter.ToInt32(Data, 28);
					}
					case MapType.Quake2:
					case MapType.Daikatana:
					case MapType.SiN:
					case MapType.SoF:
					case MapType.Source17:
					case MapType.Source18:
					case MapType.Source19:
					case MapType.Source20:
					case MapType.Source21:
					case MapType.Source22:
					case MapType.Source23:
					case MapType.Source27:
					case MapType.L4D2:
					case MapType.TacticalInterventionEncrypted:
					case MapType.Vindictus: {
						return BitConverter.ToInt32(Data, 44);
					}
					case MapType.DMoMaM: {
						return BitConverter.ToInt32(Data, 48);
					}
					case MapType.Nightfire: {
						return BitConverter.ToInt32(Data, 52);
					}
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake: {
						return BitConverter.ToInt32(Data, 60);
					}
					default: {
						return -1;
					}
				}
			}
			set {
				byte[] bytes = BitConverter.GetBytes(value);
				switch (MapType) {
					case MapType.CoD4: {
						Data[28] = bytes[0];
						Data[29] = bytes[1];
						break;
					}
					case MapType.CoD:
					case MapType.CoD2:
					case MapType.STEF2:
					case MapType.STEF2Demo:
					case MapType.Quake3:
					case MapType.MOHAA:
					case MapType.Raven:
					case MapType.FAKK: {
						bytes.CopyTo(Data, 28);
						break;
					}
					case MapType.Quake2:
					case MapType.Daikatana:
					case MapType.SiN:
					case MapType.SoF:
					case MapType.Source17:
					case MapType.Source18:
					case MapType.Source19:
					case MapType.Source20:
					case MapType.Source21:
					case MapType.Source22:
					case MapType.Source23:
					case MapType.Source27:
					case MapType.L4D2:
					case MapType.TacticalInterventionEncrypted:
					case MapType.Vindictus: {
						bytes.CopyTo(Data, 44);
						break;
					}
					case MapType.DMoMaM: {
						bytes.CopyTo(Data, 48);
						break;
					}
					case MapType.Nightfire: {
						bytes.CopyTo(Data, 52);
						break;
					}
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake: {
						bytes.CopyTo(Data, 60);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Enumerates the patch indices used by this <see cref="Model"/>.
		/// </summary>
		public IEnumerable<int> PatchIndices {
			get {
				for (int i = 0; i < NumPatchIndices; ++i) {
					yield return (int)Parent.Bsp.leafPatches[FirstPatchIndicesIndex + i];
				}
			}
		}

		/// <summary>
		/// Gets or sets the index of the first patch index for this <see cref="Model"/>.
		/// </summary>
		[Index("patchIndices")] public int FirstPatchIndicesIndex {
			get {
				switch (MapType) {
					case MapType.CoD: {
						return BitConverter.ToInt32(Data, 32);
					}
					default: {
						return -1;
					}
				}
			}
			set {
				byte[] bytes = BitConverter.GetBytes(value);
				switch (MapType) {
					case MapType.CoD: {
						bytes.CopyTo(Data, 32);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the count of patch indices referenced by this <see cref="Model"/>.
		/// </summary>
		[Count("patchIndices")] public int NumPatchIndices {
			get {
				switch (MapType) {
					case MapType.CoD: {
						return BitConverter.ToInt32(Data, 36);
					}
					default: {
						return -1;
					}
				}
			}
			set {
				byte[] bytes = BitConverter.GetBytes(value);
				switch (MapType) {
					case MapType.CoD: {
						bytes.CopyTo(Data, 36);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the bounding box minimums for this model.
		/// </summary>
		public Vector3 Minimums {
			get {
				switch (MapType) {
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake:
					case MapType.Quake2:
					case MapType.Quake3:
					case MapType.SiN:
					case MapType.SoF:
					case MapType.Raven:
					case MapType.Nightfire:
					case MapType.STEF2:
					case MapType.STEF2Demo:
					case MapType.MOHAA:
					case MapType.FAKK:
					case MapType.CoD:
					case MapType.CoD2:
					case MapType.CoD4:
					case MapType.Titanfall:
					case MapType.Daikatana:
					case MapType.Source17:
					case MapType.Source18:
					case MapType.Source19:
					case MapType.Source20:
					case MapType.Source21:
					case MapType.Source22:
					case MapType.Source23:
					case MapType.Source27:
					case MapType.L4D2:
					case MapType.TacticalInterventionEncrypted:
					case MapType.Vindictus:
					case MapType.DMoMaM: {
						return new Vector3(BitConverter.ToSingle(Data, 0), BitConverter.ToSingle(Data, 4), BitConverter.ToSingle(Data, 8));
					}
					default: {
						return new Vector3(0, 0, 0);
					}
				}
			}
			set {
				switch (MapType) {
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake:
					case MapType.Quake2:
					case MapType.Quake3:
					case MapType.SiN:
					case MapType.SoF:
					case MapType.Raven:
					case MapType.Nightfire:
					case MapType.STEF2:
					case MapType.STEF2Demo:
					case MapType.MOHAA:
					case MapType.FAKK:
					case MapType.CoD:
					case MapType.CoD2:
					case MapType.CoD4:
					case MapType.Titanfall:
					case MapType.Daikatana:
					case MapType.Source17:
					case MapType.Source18:
					case MapType.Source19:
					case MapType.Source20:
					case MapType.Source21:
					case MapType.Source22:
					case MapType.Source23:
					case MapType.Source27:
					case MapType.L4D2:
					case MapType.TacticalInterventionEncrypted:
					case MapType.Vindictus:
					case MapType.DMoMaM: {
						value.GetBytes().CopyTo(Data, 0);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the bounding box maximums for this model.
		/// </summary>
		public Vector3 Maximums {
			get {
				switch (MapType) {
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake:
					case MapType.Quake2:
					case MapType.Quake3:
					case MapType.Nightfire:
					case MapType.STEF2:
					case MapType.STEF2Demo:
					case MapType.MOHAA:
					case MapType.FAKK:
					case MapType.CoD:
					case MapType.CoD2:
					case MapType.CoD4:
					case MapType.Titanfall:
					case MapType.Daikatana:
					case MapType.Source17:
					case MapType.Source18:
					case MapType.Source19:
					case MapType.Source20:
					case MapType.Source21:
					case MapType.Source22:
					case MapType.Source23:
					case MapType.Source27:
					case MapType.L4D2:
					case MapType.TacticalInterventionEncrypted:
					case MapType.Vindictus:
					case MapType.DMoMaM: {
						return new Vector3(BitConverter.ToSingle(Data, 12), BitConverter.ToSingle(Data, 16), BitConverter.ToSingle(Data, 20));
					}
					default: {
						return new Vector3(0, 0, 0);
					}
				}
			}
			set {
				switch (MapType) {
					case MapType.TYPE_GOLDSRC:
					case MapType.Quake:
					case MapType.Quake2:
					case MapType.Quake3:
					case MapType.Nightfire:
					case MapType.STEF2:
					case MapType.STEF2Demo:
					case MapType.MOHAA:
					case MapType.FAKK:
					case MapType.CoD:
					case MapType.CoD2:
					case MapType.CoD4:
					case MapType.Titanfall:
					case MapType.Daikatana:
					case MapType.Source17:
					case MapType.Source18:
					case MapType.Source19:
					case MapType.Source20:
					case MapType.Source21:
					case MapType.Source22:
					case MapType.Source23:
					case MapType.Source27:
					case MapType.L4D2:
					case MapType.TacticalInterventionEncrypted:
					case MapType.Vindictus:
					case MapType.DMoMaM: {
						value.GetBytes().CopyTo(Data, 12);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the origin for this model.
		/// </summary>
		public Vector3 Origin {
			get {
				switch (MapType) {
					case MapType.Quake2:
					case MapType.Titanfall:
					case MapType.Daikatana:
					case MapType.Source17:
					case MapType.Source18:
					case MapType.Source19:
					case MapType.Source20:
					case MapType.Source21:
					case MapType.Source22:
					case MapType.Source23:
					case MapType.Source27:
					case MapType.L4D2:
					case MapType.SiN:
					case MapType.SoF:
					case MapType.TacticalInterventionEncrypted:
					case MapType.Vindictus:
					case MapType.DMoMaM: {
						return new Vector3(BitConverter.ToSingle(Data, 24), BitConverter.ToSingle(Data, 28), BitConverter.ToSingle(Data, 32));
					}
					default: {
						return new Vector3(0, 0, 0);
					}
				}
			}
			set {
				switch (MapType) {
					case MapType.Quake2:
					case MapType.Titanfall:
					case MapType.Daikatana:
					case MapType.Source17:
					case MapType.Source18:
					case MapType.Source19:
					case MapType.Source20:
					case MapType.Source21:
					case MapType.Source22:
					case MapType.Source23:
					case MapType.Source27:
					case MapType.L4D2:
					case MapType.SiN:
					case MapType.SoF:
					case MapType.TacticalInterventionEncrypted:
					case MapType.Vindictus:
					case MapType.DMoMaM: {
						value.GetBytes().CopyTo(Data, 24);
						break;
					}
				}
			}
		}

		/// <summary>
		/// Creates a new <see cref="Model"/> object from a <c>byte</c> array.
		/// </summary>
		/// <param name="data"><c>byte</c> array to parse.</param>
		/// <param name="parent">The <see cref="ILump"/> this <see cref="Model"/> came from.</param>
		/// <exception cref="ArgumentNullException"><paramref name="data"/> was <c>null</c>.</exception>
		public Model(byte[] data, ILump parent = null) {
			if (data == null) {
				throw new ArgumentNullException();
			}

			Data = data;
			Parent = parent;
		}

		/// <summary>
		/// Creates a new <see cref="Model"/> by copying the fields in <paramref name="source"/>, using
		/// <paramref name="parent"/> to get <see cref="LibBSP.MapType"/> and <see cref="LumpInfo.version"/>
		/// to use when creating the new <see cref="Model"/>.
		/// If the <paramref name="parent"/>'s <see cref="BSP"/>'s <see cref="LibBSP.MapType"/> is different from
		/// the one from <paramref name="source"/>, it does not matter, because fields are copied by name.
		/// </summary>
		/// <param name="source">The <see cref="Model"/> to copy.</param>
		/// <param name="parent">
		/// The <see cref="ILump"/> to use as the <see cref="Parent"/> of the new <see cref="Model"/>.
		/// Use <c>null</c> to use the <paramref name="source"/>'s <see cref="Parent"/> instead.
		/// </param>
		public Model(Model source, ILump parent = null) {
			Parent = parent;

			if (parent != null && parent.Bsp != null) {
				if (source.Parent != null && source.Parent.Bsp != null && source.Parent.Bsp.version == parent.Bsp.version && source.LumpVersion == parent.LumpInfo.version) {
					Data = new byte[source.Data.Length];
					Array.Copy(source.Data, Data, source.Data.Length);
					return;
				} else {
					Data = new byte[GetStructLength(parent.Bsp.version, parent.LumpInfo.version)];
				}
			} else {
				if (source.Parent != null && source.Parent.Bsp != null) {
					Data = new byte[GetStructLength(source.Parent.Bsp.version, source.Parent.LumpInfo.version)];
				} else {
					Data = new byte[GetStructLength(MapType.Undefined, 0)];
				}
			}
			
			HeadNodeIndex = source.HeadNodeIndex;
			HeadClipNode1Index = source.HeadClipNode1Index;
			HeadClipNode2Index = source.HeadClipNode2Index;
			HeadClipNode3Index = source.HeadClipNode3Index;
			FirstLeafIndex = source.FirstLeafIndex;
			NumLeaves = source.NumLeaves;
			FirstBrushIndex = source.FirstBrushIndex;
			NumBrushes = source.NumBrushes;
			FirstFaceIndex = source.FirstFaceIndex;
			NumFaces = source.NumFaces;
			FirstPatchIndicesIndex = source.FirstPatchIndicesIndex;
			NumPatchIndices = source.NumPatchIndices;
			Minimums = source.Minimums;
			Maximums = source.Maximums;
			Origin = source.Origin;
		}

		/// <summary>
		/// Factory method to parse a <c>byte</c> array into a <see cref="Lump{Model}"/>.
		/// </summary>
		/// <param name="data">The data to parse.</param>
		/// <param name="bsp">The <see cref="BSP"/> this lump came from.</param>
		/// <param name="lumpInfo">The <see cref="LumpInfo"/> associated with this lump.</param>
		/// <returns>A <see cref="Lump{Model}"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="data"/> parameter was <c>null</c>.</exception>
		public static Lump<Model> LumpFactory(byte[] data, BSP bsp, LumpInfo lumpInfo) {
			if (data == null) {
				throw new ArgumentNullException();
			}
			
			return new Lump<Model>(data, GetStructLength(bsp.version, lumpInfo.version), bsp, lumpInfo);
		}

		/// <summary>
		/// Gets the length of this struct's data for the given <paramref name="mapType"/> and <paramref name="lumpVersion"/>.
		/// </summary>
		/// <param name="mapType">The <see cref="LibBSP.MapType"/> of the BSP.</param>
		/// <param name="lumpVersion">The version number for the lump.</param>
		/// <returns>The length, in <c>byte</c>s, of this struct.</returns>
		/// <exception cref="ArgumentException">This struct is not valid or is not implemented for the given <paramref name="mapType"/> and <paramref name="lumpVersion"/>.</exception>
		public static int GetStructLength(MapType mapType, int lumpVersion = 0) {
			switch (mapType) {
				case MapType.Titanfall: {
					return 32;
				}
				case MapType.Quake3:
				case MapType.Raven:
				case MapType.STEF2:
				case MapType.STEF2Demo:
				case MapType.MOHAA:
				case MapType.FAKK: {
					return 40;
				}
				case MapType.Quake2:
				case MapType.Daikatana:
				case MapType.CoD:
				case MapType.CoD2:
				case MapType.CoD4:
				case MapType.SiN:
				case MapType.SoF:
				case MapType.Source17:
				case MapType.Source18:
				case MapType.Source19:
				case MapType.Source20:
				case MapType.Source21:
				case MapType.Source22:
				case MapType.Source23:
				case MapType.Source27:
				case MapType.L4D2:
				case MapType.TacticalInterventionEncrypted:
				case MapType.Vindictus: {
					return 48;
				}
				case MapType.DMoMaM: {
					return 52;
				}
				case MapType.Nightfire: {
					return 56;
				}
				case MapType.TYPE_GOLDSRC:
				case MapType.Quake: {
					return 64;
				}
				default: {
					throw new ArgumentException("Lump object " + MethodBase.GetCurrentMethod().DeclaringType.Name + " does not exist in map type " + mapType + " or has not been implemented.");
				}
			}
		}

		/// <summary>
		/// Gets the index for this lump in the BSP file for a specific map format.
		/// </summary>
		/// <param name="type">The map type.</param>
		/// <returns>Index for this lump, or -1 if the format doesn't have this lump.</returns>
		public static int GetIndexForLump(MapType type) {
			switch (type) {
				case MapType.Raven:
				case MapType.Quake3: {
					return 7;
				}
				case MapType.MOHAA:
				case MapType.FAKK:
				case MapType.Quake2:
				case MapType.SiN:
				case MapType.Daikatana:
				case MapType.SoF: {
					return 13;
				}
				case MapType.TYPE_GOLDSRC:
				case MapType.Quake:
				case MapType.Nightfire:
				case MapType.Vindictus:
				case MapType.TacticalInterventionEncrypted:
				case MapType.Source17:
				case MapType.Source18:
				case MapType.Source19:
				case MapType.Source20:
				case MapType.Source21:
				case MapType.Source22:
				case MapType.Source23:
				case MapType.Source27:
				case MapType.L4D2:
				case MapType.DMoMaM:
				case MapType.Titanfall: {
					return 14;
				}
				case MapType.STEF2:
				case MapType.STEF2Demo: {
					return 15;
				}
				case MapType.CoD: {
					return 27;
				}
				case MapType.CoD2: {
					return 35;
				}
				case MapType.CoD4: {
					return 37;
				}
				default: {
					return -1;
				}
			}
		}

	}
}
