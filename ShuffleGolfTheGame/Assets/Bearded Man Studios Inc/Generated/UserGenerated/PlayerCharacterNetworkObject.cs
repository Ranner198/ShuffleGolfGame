using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0]")]
	public partial class PlayerCharacterNetworkObject : NetworkObject
	{
		public const int IDENTITY = 5;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private int _index;
		public event FieldEvent<int> indexChanged;
		public Interpolated<int> indexInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int index
		{
			get { return _index; }
			set
			{
				// Don't do anything if the value is the same
				if (_index == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_index = value;
				hasDirtyFields = true;
			}
		}

		public void SetindexDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_index(ulong timestep)
		{
			if (indexChanged != null) indexChanged(_index, timestep);
			if (fieldAltered != null) fieldAltered("index", _index, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			indexInterpolation.current = indexInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _index);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_index = UnityObjectMapper.Instance.Map<int>(payload);
			indexInterpolation.current = _index;
			indexInterpolation.target = _index;
			RunChange_index(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _index);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (indexInterpolation.Enabled)
				{
					indexInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					indexInterpolation.Timestep = timestep;
				}
				else
				{
					_index = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_index(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (indexInterpolation.Enabled && !indexInterpolation.current.UnityNear(indexInterpolation.target, 0.0015f))
			{
				_index = (int)indexInterpolation.Interpolate();
				//RunChange_index(indexInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public PlayerCharacterNetworkObject() : base() { Initialize(); }
		public PlayerCharacterNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public PlayerCharacterNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
